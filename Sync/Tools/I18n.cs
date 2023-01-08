using Sync.Tools.ConfigurationAttribute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sync.Tools
{
    public class DefaultI18n : I18nProvider
    {
        public static LanguageElement LANG_Loading = "讀取中....";
        public static LanguageElement LANG_Plugins = "已載入 {0:D} 個插件";
        public static LanguageElement LANG_Sources = "已載入 {0:D} 個直播來源";
        public static LanguageElement LANG_Client = "已載入 {0:D} 個客戶端";
        public static LanguageElement LANG_Error = "無法初始化連接器，請確認是否已安裝直播來源!";
        public static LanguageElement LANG_Commands = "已載入 {0:D} 個命令";
        public static LanguageElement LANG_Filters = "已載入 {0:D} 個過濾器";
        public static LanguageElement LANG_Ready = "準備就緒!";

        public static LanguageElement LANG_RqueireLogin = "請登入至RnW的帳戶";
        public static LanguageElement LANG_AccountName = "帳戶名稱:";
        public static LanguageElement LANG_AccountPw = "密碼:";
        public static LanguageElement LANG_AccountSave = "帳號保存成功! 將連接至伺服器";

        public static LanguageElement LANG_Start = "開始執行....";
        public static LanguageElement LANG_Stopping = "停止執行...";
        public static LanguageElement LANG_Restarting = "重新啟動工作...";

        public static LanguageElement LANG_LoadingPlugin = "載入 {0:S} 中...";
        public static LanguageElement LANG_LoadPluginErr = "無法載入 {0:S} ({1:S})";
        public static LanguageElement LANG_NotPluginErr = "附加元件 {0:S} 並非 osu!Sync 之附加元件 ({1:S})";

        public static LanguageElement LANG_NotConfig = "請先修改 'config.ini' 之後再開始進行同步操作。";
        public static LanguageElement LANG_NoSource = "無法找到任何直播來源! 請先安裝一個直播來源。";
        public static LanguageElement LANG_MissSource = "找不到預設匹配的直播來源，將使用第一個來源。";
        public static LanguageElement LANG_SetSource = "設定 {0:S} 為直播彈幕來源";
        public static LanguageElement LANG_SupportSend = "提示: 目前彈幕來源支持遊戲內發送至彈幕來源的功能，請輸入 login [帳號名稱] [密碼] 進行登入! (帳號名稱與密碼可選登入)";
        public static LanguageElement LANG_CertLength = "憑證長度: {0:D}";
        public static LanguageElement LANG_CertExist = "提示: 現階段已有登入憑證的紀錄，若您想覆蓋紀錄，請輸入 login [帳號名稱] [密碼] 來覆蓋紀錄！（帳號名稱與密碼可選登入）";
        public static LanguageElement LANG_SendNotReady = "現階段戶用端並未標記彈幕發送可用，請嘗試 login 登入";

        public static LanguageElement LANG_UnknowCommand = "未知指令! 請輸入 help 查看指令列表";
        public static LanguageElement LANG_CommandFail = "指令執行失敗! 請輸入 help 查看指令列表";

        public static LanguageElement LANG_ConfigFile = "設定文件";

        public static LanguageElement LANG_UserCount = "用戶總數更動: {0:D}";
        public static LanguageElement LANG_UserCount_Change = "直播觀看人數從 {0:S}人 至 {1:D}人";
        public static LanguageElement LANG_UserCount_Change_Increase = "增加";
        public static LanguageElement LANG_UserCount_Change_Decrease = "減少";

        public static LanguageElement LANG_Source_Disconnecting = "正在中斷與彈幕來源伺服器的連線...";
        public static LanguageElement LANG_Source_Disconnected = "與伺服器連線中斷! 3秒後嘗試重新連線!";
        public static LanguageElement LANG_Source_Disconnected_Succ = "與伺服器中斷連線成功!";
        public static LanguageElement LANG_Source_Connect = "正在連線至彈幕來源伺服器....";
        public static LanguageElement LANG_Source_Connected_Succ = "連接至彈幕來源伺服器成功!";

        public static LanguageElement LANG_Current_Online = "目前上線人數: {0:D}";
        public static LanguageElement LANG_Gift_Sent = "我送給你{O:D}份{1:S}!";

        public static LanguageElement LANG_Config = "設定文件: ";
        public static LanguageElement LANG_Config_Status_OK = "成功, 房間ID: {0}";
        public static LanguageElement LANG_Config_Status_Fail = "設定失敗";

        public static LanguageElement LANG_Source = "來源{0:S}: ";
        public static LanguageElement LANG_IRC = "客戶端: ";
        public static LanguageElement LANG_Danmaku = "彈幕發送: ";
        public static LanguageElement LANG_Status_Connected = "已連線";
        public static LanguageElement LANG_Status_NotConenct = "未連線";

        public static LanguageElement LANG_Loading_Config = @"正在載入設定文件....\n";

        public static LanguageElement LANG_Welcome = "歡迎使用 osu!Sync ver.{0:S} (Rebuild by Muisnow) ";
        public static LanguageElement LANG_Help = @"輸入 help 取得指令列表\n\n";
        public static LanguageElement LANG_Command = "指令";
        public static LanguageElement LANG_Command_Description = "描述";

        public static LanguageElement LANG_MsgMgr_Limit = "目前訊息管理工具「開始」管制訊息，僅有 ?send 指令的內容才會發送至IRC頻道";
        public static LanguageElement LANG_MsgMgr_Free = "目前訊息管理工具「解除」管制訊息，所有內容皆可直接發送至IRC頻道";
        public static LanguageElement LANG_Plugin_Cycle_Reference = "發現部分附加元件產生循環引用之錯誤，附加元件 {0:S} 可能將不會依照原本開發者所指定的方式載入";


        //from default plugin
        public static LanguageElement LANG_COMMANDS_LOGIN = "login <user> [pass] 登入至目標的彈幕來源，並啟用彈幕發送功能";
        public static LanguageElement LANG_COMMANDS_EXIT = "離開 osu!Sync";
        public static LanguageElement LANG_COMMANDS_CLEAR = "清空狀態";
        public static LanguageElement LANG_COMMANDS_STATUS = "取得現階段連線狀態";
        public static LanguageElement LANG_COMMANDS_STOP = "中斷當前的連線";
        public static LanguageElement LANG_COMMANDS_START = "開始同步";
        public static LanguageElement LANG_COMMANDS_HELP = "顯示幫助訊息";
        public static LanguageElement LANG_COMMANDS_SOURCEMSG = "danmaku <message> 發送測試彈幕";
        public static LanguageElement LANG_COMMANDS_CLIENTMSG = "chat <message> 發送測試IRC訊息";
        public static LanguageElement LANG_COMMANDS_CLIENTUSERMSG = "chatuser <username> <message> 發送測試訊息至username";
        public static LanguageElement LANG_COMMANDS_EXIT_DONE = "程式已結束執行，您可以關閉此視窗。";
        public static LanguageElement LANG_COMMANDS_SOURCES = "取得目前的所有彈幕來源表";
        public static LanguageElement LANG_COMMANDS_MSGMGR = "查看或設定訊息控制器相關的內容，使用 --help 參數取得幫助";
        public static LanguageElement LANG_COMMANDS_FILTERS = "列表所有当前可用消息过滤器";
        public static LanguageElement LANG_COMMANDS_DISABLE = "向插件发送禁用消息 disable (插件名称)";
        public static LanguageElement LANG_COMMANDS_SWITCH_CLIENT = "切换到指定Client实例，不带名称则为获取Client列表";
        public static LanguageElement LANG_COMMANDS_SOURCELOGIN = "登录到弹幕源 sourcelogin [用户名] [密码]";
        public static LanguageElement LANG_COMMANDS_RESTART = "重新启动应用程序";
        public static LanguageElement LANG_COMMANDS_LANG = "lang [cultureName] Get/Set language";
        public static LanguageElement LANG_COMMANDS_LISTLANG = "listlang [--all] List (supported/all) languages";
        public static LanguageElement LANG_COMMANDS_FILTERS_ITEM = "过滤项";
        public static LanguageElement LANG_COMMANDS_FILTERS_OBJ = "过滤器";
        public static LanguageElement LANG_COMMANDS_CLIENT_NAME = "Client";
        public static LanguageElement LANG_COMMANDS_CLIENT_AUTHOR = "作者";
        public static LanguageElement LANG_COMMANDS_SOURCES_NAME = "弹幕源";
        public static LanguageElement LANG_COMMANDS_SOURCES_AUTHOR = "作者";
        public static LanguageElement LANG_COMMANDS_CURRENT = "当前设置为 {0:S}";
        public static LanguageElement LANG_COMMANDS_DANMAKU_NOT_SUPPORT = @"提示：当前弹幕源不支持发送弹幕，请更换弹幕源！\n";
        public static LanguageElement LANG_COMMANDS_CHAT_IRC_NOTCONNECT = "osu! irc 尚未连接，您还不能发送消息。";
        public static LanguageElement LANG_COMMANDS_DANMAKU_REQUIRE_LOGIN = "你必须登录才能发送弹幕!";
        public static LanguageElement LANG_COMMANDS_START_ALREADY_RUN = "同步实例已经在运行。";
        public static LanguageElement LANG_COMMANDS_ARGUMENT_WRONG = "参数不正确";
        public static LanguageElement LANG_COMMANDS_MSGMGR_HELP = @"\n--status :查看当前消息管理器的信息\n--limit <数值> :是设置限制发送信息的等级，越低就越容易触发管控\n--option <名称> :是设置管控的方式，其中Auto是自动管控，ForceAll强行全都发送,ForceLimit是仅发送使用?send命令的消息,DisableAll是拦截任何管道内的信息";
        public static LanguageElement LANG_COMMANDS_MSGMGR_LIMIT = "限制中...";
        public static LanguageElement LANG_COMMANDS_MSGMGR_FREE = "无限制";
        public static LanguageElement LANG_COMMANDS_MSGMGR_STATUS = "MessageManager mode:{4:S},status:{0:D},queueCount/limitCount/recoverTime:{1}/{2}/{3}";
        public static LanguageElement LANG_COMMANDS_MSGMGR_LIMIT_SPEED_SET = "设置限制发送速度等级为{0}";
        public static LanguageElement LANG_COMMANDS_MSGMGR_LIMIT_STYPE_SET = "设置消息管理器的管制方式为{0}";

        public static LanguageElement LANG_COMMANDS_START_NO_SOURCE = "还未钦定任何一个接收源";
        public static LanguageElement LANG_COMMANDS_START_NO_CLIENT = "还未钦定任何一个发送源";
        public static LanguageElement LANG_COMMANDS_CURRENT_LANG = "当前语言: {0:S}\t{1:S}";
        public static LanguageElement LANG_COMMANDS_LANG_SWITCHED = "成功切换语言至 {1:S}({0:S})";
        public static LanguageElement LANG_COMMANDS_LANG_NOT_FOUND = "切换语言失败,请检查语言代码参数是否正确";

        public static LanguageElement LANG_UPDATE_DONE = "更新完成,是否重启软件";
        public static LanguageElement LANG_INSTALL_DONE = "下载完成,是否重启软件";
        public static LanguageElement LANG_PLUGIN_NOT_FOUND = "插件 {0} 不存在";
        public static LanguageElement LANG_REMOVE_DONE = "删除成功,是否重启软件";
        public static LanguageElement LANG_VERSION_LATEST_OR_CANEL = "{0} 已是最新,或者已被用户取消更新操作";
        public static LanguageElement LANG_UPDATE_CHECK_ERROR = "无法根据 [{0}] 检查更新 :  {1} : {2}";
        public static LanguageElement LANG_UPDATE_ERROR = "无法更新 :  {0} : {1}";

        public static LanguageElement LANG_SOURCE_NOT_SUPPORT_SEND = "接收源 {0} 并不支持发送功能";
        public static LanguageElement LANG_NO_PLUGIN_SELECT = "还未钦定插件名称";
        public static LanguageElement LANG_PLUGIN_DISABLED = "已禁用 ";

        public static LanguageElement LANG_NO_ANY_SOURCE = "没有任何弹幕接收源,请检查Plugins目录或使用\"plugins install DefaultPlugin\"来安装默认插件";
        public static LanguageElement LANG_Instance_Exist = "只能存在一个Sync进程，等待上一个Sync结束";
    }

    public interface I18nProvider
    {
    }

    public struct LanguageElement
    {
        private string value;

        public LanguageElement(string defaultVal)
        {
            value = defaultVal;
        }

        public static implicit operator LanguageElement(string val)
        {
            return new LanguageElement(val);
        }

        public static implicit operator string(LanguageElement element)
        {
            return element.value;
        }

        public override string ToString()
        {
            return value;
        }
    }

    /// <summary>
    /// I18n Manager
    /// </summary>
    public class I18n
    {
        public static string CurrentSystemLang { get => System.Globalization.CultureInfo.CurrentCulture.Name; }
        private string Base { get => AppDomain.CurrentDomain.BaseDirectory; }
        public string LangFolder { get => Path.Combine(Base, "Language"); }
        public string SelectLangFolder { get => Path.Combine(LangFolder, CurrentLanguage); }
        public string CurrentLanguage;

        private static I18n instance;

        private static List<I18nProvider> ApplyedProvider = new List<I18nProvider>();

        public static I18n Instance
        {
            get
            {
                if (instance == null)
                {
                    if (DefaultConfiguration.Instance.Language == DefaultConfiguration.DEFAULT_LANGUAGE || DefaultConfiguration.Instance.Language.ToString().Length == 0)
                    {
                        instance = new I18n(CurrentSystemLang);
                        DefaultConfiguration.Instance.Language = CurrentSystemLang;
                    }
                    else
                    {
                        instance = new I18n(DefaultConfiguration.Instance.Language);
                    }
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public static void SwitchToCulture(string CultureName)
        {
            Instance = new I18n(CultureName);
            foreach (var item in ApplyedProvider)
            {
                Instance.ApplyLanguage(item);
            }
        }

        private I18n()
        {
        }

        /// <summary>
        ///  Constructor for initial one language
        /// </summary>
        /// <param name="CultureName">Cultura name</param>
        private I18n(string CultureName)
        {
            CurrentLanguage = CultureName;
            if (!Directory.Exists(LangFolder)) Directory.CreateDirectory(LangFolder);
            if (!Directory.Exists(SelectLangFolder)) Directory.CreateDirectory(SelectLangFolder);
        }

        public void ApplyLanguage(I18nProvider instance)
        {
            if (!ApplyedProvider.Exists(p => p == instance)) ApplyedProvider.Add(instance);
            string LangFile = Path.Combine(SelectLangFolder, instance.GetType().FullName) + ".lang";
            foreach (FieldInfo item in instance.GetType().GetFields())
            {
                if (item.FieldType.Equals(typeof(LanguageElement)))
                {
                    string value = ConfigurationIO.IniReadValue(LangFile, item.Name, CurrentLanguage);
                    if (value == "")
                    {
                        value = (LanguageElement)item.GetValue(instance);
                        ConfigurationIO.IniWriteValue(LangFile, item.Name, value, CurrentLanguage);
                    }
                    item.SetValue(instance, new LanguageElement(value));
                }
                else if (item.FieldType.Equals(typeof(GuiLanguageElement)))
                {
                    string value = ConfigurationIO.IniReadValue(LangFile, item.Name, CurrentLanguage);
                    if (value == "")
                    {
                        value = (GuiLanguageElement)item.GetValue(instance);
                        ConfigurationIO.IniWriteValue(LangFile, item.Name, value, CurrentLanguage);
                    }
                    item.SetValue(instance, new GuiLanguageElement(value));
                }
            }
        }

        public override string ToString() => $"CurrentLanguage={CurrentLanguage}";
    }
}