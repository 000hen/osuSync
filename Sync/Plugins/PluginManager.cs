﻿using Sync.Client;
using Sync.Command;
using Sync.Source;
using Sync.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static Sync.Tools.DefaultI18n;

namespace Sync.Plugins
{
    /// <summary>
    /// A flag for plugin event type
    /// </summary>
    public interface IPluginEvent : IBaseEvent { }

    /// <summary>
    /// Base plugin events
    /// </summary>
    public class PluginEvents : BaseEventDispatcher<IPluginEvent>
    {
        /// <summary>
        /// Fire when init plugin
        /// </summary>
        public struct InitPluginEvent : IPluginEvent
        {
            public Plugin Plugin { get; private set; }

            public InitPluginEvent(Plugin plugin)
            {
                this.Plugin = plugin;
            }
        }

        /// <summary>
        /// Fire when init source
        /// </summary>
        public struct InitSourceEvent : IPluginEvent
        {
            public SourceManager Sources { get; private set; }

            public InitSourceEvent(SourceManager source)
            {
                Sources = source;
            }
        }

        /// <summary>
        /// Fire when init filter
        /// </summary>
        public struct InitFilterEvent : IPluginEvent
        {
            public FilterManager Filters { get; private set; }

            public InitFilterEvent(FilterManager filters)
            {
                Filters = filters;
            }
        }

        /// <summary>
        /// Fire when init command
        /// </summary>
        public struct InitCommandEvent : IPluginEvent
        {
            public CommandManager Commands { get; private set; }

            public InitCommandEvent(CommandManager commands)
            {
                Commands = commands;
            }
        }

        /// <summary>
        /// Fire when init clients
        /// </summary>
        public struct InitClientEvent : IPluginEvent
        {
            public ClientManager Clients { get; private set; }

            public InitClientEvent(ClientManager clients)
            {
                Clients = clients;
            }
        }

        /// <summary>
        /// Fire when init source warpper
        /// </summary>
        public struct InitSourceWarpperEvent : IPluginEvent
        {
            public SourceWorkWrapper SourceWrapper { get; private set; }

            public InitSourceWarpperEvent(SourceWorkWrapper wrapper)
            {
                SourceWrapper = wrapper;
            }
        }

        /// <summary>
        /// Fire when init client warpper
        /// </summary>
        public struct InitClientWarpperEvent : IPluginEvent
        {
            public ClientWorkWrapper ClientWrapper { get; private set; }

            public InitClientWarpperEvent(ClientWorkWrapper wrapper)
            {
                ClientWrapper = wrapper;
            }
        }

        /// <summary>
        /// Fire when load complete
        /// </summary>
        public struct LoadCompleteEvent : IPluginEvent
        {
            public SyncHost Host { get; private set; }

            public LoadCompleteEvent(SyncHost host)
            {
                Host = host;
            }
        }

        public struct ConfigurationChange : IPluginEvent
        {
        }

        /// <summary>
        /// Fire when ready
        /// </summary>
        public struct ProgramReadyEvent : IPluginEvent
        {
            //public SyncManager Manager { get; private set; }
            //public SyncManagerCompleteEvent()
            //{
            //    Manager = Program.host.SyncInstance;
            //}
        }

        public static readonly PluginEvents Instance = new PluginEvents();

        private PluginEvents()
        {
            EventDispatcher.Instance.RegisterNewDispatcher(GetType());
        }
    }

    /// <summary>
    /// Plugins Manager
    /// <para>Load plugins from Plugins foldere and Initial plugin</para>
    /// </summary>
    public class PluginManager
    {
        private List<Plugin> pluginList;
        private List<Assembly> asmList;
        private LinkedList<Type> loadedList;
        private List<Type> allList;

        internal PluginManager()
        {
        }

        /// <summary>
        /// Raise global <see cref="PluginEvents.InitCommandEvent"/> to all plugin
        /// </summary>
        /// <returns>Return commands count</returns>
        internal int LoadCommnads()
        {
            PluginEvents.Instance.RaiseEvent(new PluginEvents.InitCommandEvent(SyncHost.Instance.Commands));
            return SyncHost.Instance.Commands.Dispatch.count;
        }

        /// <summary>
        /// Raise global <see cref="PluginEvents.InitSourceEvent"/> to all plugin
        /// </summary>
        /// <returns>Return source count</returns>
        internal int LoadSources()
        {
            PluginEvents.Instance.RaiseEvent(new PluginEvents.InitSourceEvent(SyncHost.Instance.Sources));
            return SyncHost.Instance.Sources.SourceList.Count();
        }

        /// <summary>
        /// Raise global <see cref="PluginEvents.InitFilterEvent"/> to all plugin
        /// </summary>
        /// <returns>Return filter count</returns>
        internal int LoadFilters()
        {
            PluginEvents.Instance.RaiseEvent(new PluginEvents.InitFilterEvent(SyncHost.Instance.Filters));
            return SyncHost.Instance.Filters.Count;
        }

        /// <summary>
        /// Raise global <see cref="PluginEvents.InitClientEvent"/> to all plugin
        /// </summary>
        /// <returns></returns>
        internal int LoadClients()
        {
            PluginEvents.Instance.RaiseEvent(new PluginEvents.InitClientEvent(SyncHost.Instance.Clients));
            return SyncHost.Instance.Clients.Count;
        }

        /// <summary>
        /// Raise global <see cref="PluginEvents.ProgramReadyEvent"/> to all plugin
        /// </summary>
        internal void ReadySync()
        {
            PluginEvents.Instance.RaiseEvent(new PluginEvents.ProgramReadyEvent());
        }

        /// <summary>
        /// Get a <see cref="IEnumerable{T}"/> for plugin list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Plugin> GetPlugins()
        {
            return pluginList;
        }

        /// <summary>
        /// Internal get plugin list
        /// </summary>
        /// <returns></returns>
        internal List<Plugin> GetPluginList()
        {
            return pluginList;
        }

        /// <summary>
        /// Raise a <see cref="PluginEvents.LoadCompleteEvent"/> to all plugin
        /// </summary>
        internal void ReadyProgram()
        {
            PluginEvents.Instance.RaiseEvent(new PluginEvents.LoadCompleteEvent(SyncHost.Instance));
        }

        /// <summary>
        /// Initial and load all support Plugin in 'Plugins' folder
        /// </summary>
        /// <returns>Plugins count</returns>
        internal int LoadPlugins()
        {
            //Combine search path
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");

            //Current plugins list
            pluginList = new List<Plugin>();
            //Loaded assembly
            asmList = new List<Assembly>();

            //Pre-add all assemblies in current AppDomain
            asmList.AddRange(AppDomain.CurrentDomain.GetAssemblies());

            //create Plugins folder if not exist
            if (!Directory.Exists(path)) 
            {
                try
                {
                    Directory.CreateDirectory(path);
                    IO.CurrentIO.WriteColor($"Created default Plugins folder : {path}", ConsoleColor.Green);
                }
                catch (Exception e)
                {
                    IO.CurrentIO.WriteColor($"Create default Plugins folder ({path}) failed : {e.Message} ", ConsoleColor.Yellow);
                }
            }

            //Change directiory to Plugins
            Directory.SetCurrentDirectory(path);

            //create cache folder
            var rootCache = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");
            string cache = Path.Combine(rootCache, $"cache_{(new Random()).Next().ToString("x8")}");

            try
            {
                Directory.Delete(rootCache, true);
                Directory.CreateDirectory(rootCache);
            } catch { }

            try
            {
                if (Directory.Exists(cache))
                {
                    Directory.Delete(cache, true);
                }
                Directory.CreateDirectory(cache);
            }
            catch (Exception e)
            {
                IO.CurrentIO.WriteColor($"Create temporary cache folder ({cache}) failed : {e.Message} ", ConsoleColor.Yellow);
                throw e;
            }

            new DirectoryInfo(rootCache)
            {
                Attributes = FileAttributes.Normal
            };

            //error extra notify mark
            bool got_locked_error = false;

            //Search all .dll files in directory(include sub directory)
            foreach (string file in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    if (asmList.Any(a => a.Location == file))
                        continue;
                    //Load assembly directly
                    string temp = Path.Combine(cache, Path.GetFileName(file));
                    File.Copy(file, temp);
                    Assembly asm = Assembly.LoadFrom(temp);

                    asmList.Add(asm);
                }
                catch (BadImageFormatException)
                {
                    /*
                     * Do not output anything when the file is not a
                     * .Net assembly.
                     * This can reduces unnecessary output when
                     * unmanaged assembly is needed.
                     */
                }
                catch (Exception e)
                {
                    //Not a .NET Assembly DLL
                    IO.CurrentIO.WriteColor(String.Format(LANG_LoadPluginErr, file, e.Message), ConsoleColor.Red);

                    if (e.Message.Contains("0x80131515"))
                        got_locked_error = true;
                }
            }

            if (got_locked_error)
                IO.CurrentIO.WriteColor(String.Format("糟糕! 看起來附加元件的dll檔案被系統禁止了! 你可以透過 對該dll檔案點擊右鍵 -> 內容 -> 解除封鎖 -> 套用 來解決問題"), ConsoleColor.Red);

            loadedList = new LinkedList<Type>();

            //To slove plugin dependency,
            List<Type> lazylist = new List<Type>();
            allList = new List<Type>();

            //Load all plugins first

            foreach (Assembly asm in asmList)
            {
                try
                {
                    foreach (Type item in asm.GetExportedTypes())
                    {
                        Type it = asm.GetType(item.FullName);
                        if (it == null ||
                            !it.IsClass || !it.IsPublic ||
                            !typeof(Plugin).IsAssignableFrom(it) ||
                            typeof(Plugin) == it)
                            continue;
                        allList.Add(it);
                    }
                }
                catch (FileNotFoundException e)
                {
                    //CheckUnknownDependency(e.FileName.Substring(0, e.FileName.IndexOf(",") - 1));
                }
                catch (Exception e)
                {
                    //Not up to date
                    IO.CurrentIO.WriteColor(String.Format(LANG_LoadPluginErr, asm.FullName, e.Message), ConsoleColor.Red);
                    continue;
                }
            }

            lazylist = allList.ToList();
            //looping add for resolve dependency
            do
            {
                lazylist = layerLoader(lazylist);
            } while (lazylist.Count != 0);

            return pluginList.Count;
        }

        /// <summary>
        /// Load plugins
        /// <para>Load dependencies plugin first</para>
        /// </summary>
        /// <param name="asmList">All <see cref="T"/> : <see cref="Plugin"/></param>
        /// <returns>Not loaded plugins</returns>
        private List<Type> layerLoader(IList<Type> asmList)
        {
            List<Type> nextLoad = new List<Type>();
            foreach (Type it in asmList)
            {
                try
                {
                    var hardDeps = it.GetCustomAttributes<SyncPluginDependency>();

                    foreach (var item in hardDeps)
                    {
                        var target = allList.Select(p => p.GetCustomAttribute<SyncPluginID>())?.FirstOrDefault(p => p?.GUID == item.GUID);
                        if (item.Version == null) continue;
                    }

                    bool isMissingDeps = false;
                    var softDeps = it.GetCustomAttributes<SyncSoftRequirePlugin>();
                    foreach (var item in softDeps)
                    {
                        foreach (var dep in item.RequirePluguins)
                        {
                            if (!allList.Any(p => p.Name.Contains(dep) || dep.Contains(p.Name)))
                            {
                                IO.CurrentIO.WriteColor(String.Format(LANG_MissingPlugin, it.Name, dep), ConsoleColor.Red);
                                isMissingDeps = true;
                            }
                        }
                    }
                    if (isMissingDeps) continue;

                    if (LateLoad(it))
                    {
#if (DEBUG)
                        IO.CurrentIO.WriteColor($"Lazy load [{it.Name}]", ConsoleColor.Green);
#endif
                        nextLoad.Add(it);
                        //Dependency load at this time
                        //Lazy load this plugin at next time
                        continue;
                    }

                    //no dependencies or dependencies all was loaded
                    if (!it.IsSubclassOf(typeof(Plugin))) continue;
                    else
                    {
                        LoadPluginFormType(it);
                        loadedList.AddLast(it);
                    }
                }
                catch (Exception e)
                {
                    IO.CurrentIO.WriteColor(String.Format(LANG_NotPluginErr, it.Name, e.Message), ConsoleColor.Red);
                    IO.CurrentIO.WriteColor(e.StackTrace, ConsoleColor.Red);
                    continue;
                }
            }

            return nextLoad;
        }

        private bool LateLoad(Type a)
        {
            //SyncRequirePlugin requireAttr = a.GetCustomAttribute<SyncRequirePlugin>();
            SyncSoftRequirePlugin softRequirePlugin = a.GetCustomAttribute<SyncSoftRequirePlugin>();
            IEnumerable<SyncPluginDependency> deps = a.GetCustomAttributes<SyncPluginDependency>();
            SyncPluginID pid = a.GetCustomAttribute<SyncPluginID>();
            if (deps != null)
            {
                foreach (var item in deps)
                {
                    if (loadedList.Any(p => p.GetCustomAttribute<SyncPluginID>()?.GUID == item.GUID)) continue;
                    else
                    {
                        if (CheckIsReferenceTo(allList.FirstOrDefault(p => p.GetCustomAttribute<SyncPluginID>()?.GUID == item.GUID), pid?.GUID)) return false;
                        else return true;
                    }
                }
            }

            /*if (requireAttr != null)
            {
                foreach (var item in requireAttr.RequirePluguins)
                {
                    //Dependency was been loaded
                    if (loadedList.Contains(item)) continue;
                    else
                    {
                        //Check cycle reference
                        if (CheckIsReferenceTo(item, a)) return false;
                        else return true;
                    }
                }
            }*/

            if (softRequirePlugin != null)
            {
                foreach (var item in softRequirePlugin.RequirePluguins)
                {
                    Type s = allList.FirstOrDefault(p => p.Name == item);
                    if (s == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (CheckIsReferenceTo(s, a)) return false;
                        if (!loadedList.Contains(s)) return true;
                    }
                }
            }

            return false;
        }

        private bool CheckIsReferenceTo(Type a, string b)
        {
            var result = a.GetCustomAttributes<SyncPluginDependency>()?.Any(p => p.GUID == b);
            if (result.HasValue) return result.Value;
            else return false;
        }

        private bool CheckIsReferenceTo(Type a, Type b)
        {
            return CheckIsHardReferenceTo(a, b) || CheckIsSoftReferenceTo(a, b.Name);
        }

        private bool CheckIsHardReferenceTo(Type a, Type b)
        {
            SyncRequirePlugin refRequireCheck = a.GetCustomAttribute<SyncRequirePlugin>();
            if (refRequireCheck == null) return false;
            return refRequireCheck.RequirePluguins.Contains(b);
        }

        private bool CheckIsSoftReferenceTo(Type a, string b)
        {
            SyncSoftRequirePlugin refRequireCheck = a.GetCustomAttribute<SyncSoftRequirePlugin>();
            if (refRequireCheck == null) return false;
            return refRequireCheck.RequirePluguins.Contains(b);
        }

        private Plugin LoadPluginFormType(Type it)
        {
            object pluginTest = it.Assembly.CreateInstance(it.FullName);
            if (pluginTest == null)
            {
                throw new NullReferenceException("Create instance fail!");
            }

            Plugin plugin = (Plugin)pluginTest;
            IO.CurrentIO.WriteColor(String.Format(LANG_LoadingPlugin, plugin.Name), ConsoleColor.White);

            pluginList.Add(plugin);
            plugin.OnEnable();
            PluginEvents.Instance.RaiseEventAsync(new PluginEvents.InitPluginEvent(plugin));
            return plugin;
        }
    }

    public class SyncMissingPluginException : Exception
    {
        public SyncMissingPluginException(string msg) : base(msg)
        {
        }
    }

    public class SyncPluginOutdateException : Exception
    {
        public SyncPluginOutdateException(string msg) : base(msg)
        {
        }
    }

    /// <summary>
    /// Using this attribute when you want load some plugin before your plugin.
    /// </summary>
    public class SyncRequirePlugin : Attribute
    {
        public IReadOnlyList<Type> RequirePluguins;

        public SyncRequirePlugin(params Type[] types)
        {
            RequirePluguins = new List<Type>(types);
        }
    }

    /// <summary>
    /// Using this attribute when you dependence some plugin without hard link.
    /// </summary>
    public class SyncSoftRequirePlugin : Attribute
    {
        public IReadOnlyList<string> RequirePluguins;

        public SyncSoftRequirePlugin(params string[] types)
        {
            RequirePluguins = new List<string>(types);
        }
    }

    public class SyncPluginID : Attribute
    {
        public string GUID { get; }

        /// <summary>
        /// Major.Minjor.Reversion
        /// <para>e.g: 1.4.5</para>
        /// </summary>
        public string Version { get; }

        public SyncPluginID(string GUID, string Version)
        {
            this.Version = Version;
            this.GUID = GUID;
        }
    }

    public class SyncPluginDependency : Attribute
    {
        public string GUID { get; }
        public string Version { get; set; }
        public bool Require { get; set; }

        public SyncPluginDependency(string guid) => GUID = guid;
    }
}
