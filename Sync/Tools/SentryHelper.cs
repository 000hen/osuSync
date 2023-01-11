using SharpRaven;
using SharpRaven.Data;
using Sync.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Tools
{
    /// <summary>
    /// Sentry helper for error reporter
    /// </summary>
    internal class SentryHelper
    {
        private Dictionary<Plugins.Plugin, object> registedErrorReporter = new Dictionary<Plugins.Plugin, object>();

        private SentryHelper()
        {
        }

        internal void Error(string logger, string version, Exception e, bool silent = false)
        {
            var error = new SentryEvent(e);
            Console.WriteLine("糟糕了! 看來 osu!Sync 發生了嚴重錯誤! 我們建議您擷取這些錯誤資訊並提交給開發者們!");
            Console.WriteLine("錯誤報告將會顯示，您可以將此錯誤報告提交至我們的 Github 上");
            Console.WriteLine(error.Message.ToString());
            Console.WriteLine($"Scope: {logger}, Version: {version}");
            Console.WriteLine("- 錯誤追蹤 -");
            Console.WriteLine(e.StackTrace);
        }

        internal void RepoterError(Exception e, bool silent = false) => Error("Sync", Assembly.GetEntryAssembly().GetName().Version.ToString(), e, silent);

        internal static readonly SentryHelper Instance = new SentryHelper();
    }
}
