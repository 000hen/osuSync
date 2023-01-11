using Sync.Command;
using Sync.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using static Sync.Tools.DefaultI18n;

namespace Sync.Tools.Builtin
{
    internal sealed class PluginCommand
    {
        #region SingleInstance
        private static PluginCommand instance = null;
        internal static PluginCommand Instance => instance ?? (instance = new PluginCommand());
        #endregion

        public bool Plugins(Arguments arg)
        {
            if (arg.Count == 0) return Help();
            switch (arg[0])
            {
                case "list":
                    return List();

                case "remove":
                    return Remove(arg[1]);

                default:
                    return Help();
            }
        }

        private bool Remove(string name)
        {
            var type = SyncHost.Instance.EnumPluings().FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
            if (type == null)
            {
                IO.CurrentIO.WriteColor(string.Format(LANG_PLUGIN_NOT_FOUND, name), ConsoleColor.Red);
                return false;
            }
            else
            {
                var target = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins", Path.GetFileName(name));
                if (File.Exists(target)) File.Delete(target);

                RequireRestart(LANG_REMOVE_DONE);
                return true;
            }
        }

        private bool List()
        {
            var list = SyncHost.Instance.EnumPluings();
            foreach (var item in list)
            {
                IO.CurrentIO.WriteColor("Name", ConsoleColor.Cyan, false, false);
                IO.CurrentIO.WriteColor(item.Name.PadRight(25), ConsoleColor.White, false, false);
                IO.CurrentIO.WriteColor("Author", ConsoleColor.DarkCyan, false, false);
                IO.CurrentIO.WriteColor(item.Author.PadRight(20), ConsoleColor.White, false, false);
                var info = item.GetType().GetCustomAttribute<SyncPluginID>();
                IO.CurrentIO.WriteColor("Support Update:", ConsoleColor.DarkCyan, false, false);
                if (info != null)
                {
                    IO.CurrentIO.WriteColor("Yes".PadRight(15), ConsoleColor.White, false, false);
                    IO.CurrentIO.WriteColor("Ver:", ConsoleColor.DarkCyan, false, false);
                    IO.CurrentIO.WriteColor(info.Version.PadRight(15), ConsoleColor.White, true, false);
                }
                else
                {
                    IO.CurrentIO.WriteColor("No", ConsoleColor.White, true, false);
                }
            }
            return true;
        }

        private bool Help()
        {
            IO.CurrentIO.Write("plugins 指令的幫助:");
            IO.CurrentIO.WriteHelp("remove", "remove [name] 移除附加元件");
            IO.CurrentIO.WriteHelp("list", "顯示已安裝的附加元件");
            return true;
        }

        private void RequireRestart(string msg)
        {
            IO.CurrentIO.WriteColor($"{msg}? (Y/N):", ConsoleColor.Green, false);
            var result = IO.CurrentIO.ReadCommand();
            if (result.ToLower().StartsWith("y")) SyncHost.Instance.RestartSync();
        }
    }
}
