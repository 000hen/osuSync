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
        public static LanguageElement LANG_NotPluginErr = "附加元件 {0:S} 無法啟用 ({1:S})";
        public static LanguageElement LANG_MissingPlugin = "附加元件 {0:$} 要求使用 {1:$} 但您尚未安裝，請先安裝該插件以啟用此插件";

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
        public static LanguageElement LANG_COMMANDS_FILTERS = "顯示目前可用的訊息過濾器";
        public static LanguageElement LANG_COMMANDS_DISABLE = "禁用附加元件的發送功能 disable <附加元件名稱>";
        public static LanguageElement LANG_COMMANDS_SWITCH_CLIENT = "切換至指定的戶用端，無名稱則顯示戶用端列表";
        public static LanguageElement LANG_COMMANDS_SOURCELOGIN = "登入至彈幕來源 sourcelogin <user> [pass]";
        public static LanguageElement LANG_COMMANDS_RESTART = "重新啟動 osu!Sync";
        public static LanguageElement LANG_COMMANDS_LANG = "lang [cultureName] 取得或設定語言";
        public static LanguageElement LANG_COMMANDS_LISTLANG = "listlang [--all] 取得可用語言";
        public static LanguageElement LANG_COMMANDS_FILTERS_ITEM = "過濾選項";
        public static LanguageElement LANG_COMMANDS_FILTERS_OBJ = "過濾器";
        public static LanguageElement LANG_COMMANDS_CLIENT_NAME = "戶用端";
        public static LanguageElement LANG_COMMANDS_CLIENT_AUTHOR = "作者";
        public static LanguageElement LANG_COMMANDS_SOURCES_NAME = "彈幕來源";
        public static LanguageElement LANG_COMMANDS_SOURCES_AUTHOR = "開發者";
        public static LanguageElement LANG_COMMANDS_CURRENT = "目前設定為 {0:S}";
        public static LanguageElement LANG_COMMANDS_DANMAKU_NOT_SUPPORT = @"提示: 已選彈幕來源不支持發送彈幕，請更換來源!\n";
        public static LanguageElement LANG_COMMANDS_CHAT_IRC_NOTCONNECT = "osu! IRC頻道尚未連線，現階段無法發送訊息。";
        public static LanguageElement LANG_COMMANDS_DANMAKU_REQUIRE_LOGIN = "你必須登入才能發送彈幕!";
        public static LanguageElement LANG_COMMANDS_START_ALREADY_RUN = "osu!Sync 已經啟動了。";
        public static LanguageElement LANG_COMMANDS_ARGUMENT_WRONG = "參數錯誤";
        public static LanguageElement LANG_COMMANDS_MSGMGR_HELP = @"\n--status :顯示目前訊息管理器的資訊\n--limit <數字> :設定的等級越低，將有更高的機率觸發管制\n--option <名稱> :為設定管制模式，其中 Auto 為自動管制，ForceAll 強制全部發送，ForceLimit 限制僅能使用 ?send 指令發送，DisableAll 為封鎖任何管道的訊息";
        public static LanguageElement LANG_COMMANDS_MSGMGR_LIMIT = "限制中...";
        public static LanguageElement LANG_COMMANDS_MSGMGR_FREE = "無限制";
        public static LanguageElement LANG_COMMANDS_MSGMGR_STATUS = "訊息管理器目前模式: {4:S}，狀態: {0:D}，列隊數量/上限數量/回復時間: {1}/{2}/{3}";
        public static LanguageElement LANG_COMMANDS_MSGMGR_LIMIT_SPEED_SET = "設定訊息管理器發送速度限制等級至 {0}";
        public static LanguageElement LANG_COMMANDS_MSGMGR_LIMIT_STYPE_SET = "設定訊息管理器管制模式至 {0}";

        public static LanguageElement LANG_COMMANDS_START_NO_SOURCE = "尚未設定一個接收來源";
        public static LanguageElement LANG_COMMANDS_START_NO_CLIENT = "尚未設定一個發送來源";
        public static LanguageElement LANG_COMMANDS_CURRENT_LANG = "目前語言: {0:S}\t{1:S}";
        public static LanguageElement LANG_COMMANDS_LANG_SWITCHED = "更改語言至 {1:S}({0:S})";
        public static LanguageElement LANG_COMMANDS_LANG_NOT_FOUND = "無法更改語言，請檢查語言代碼是否正規";

        public static LanguageElement LANG_UPDATE_DONE = "更新成功! 是否重新啟動 osu!Sync";
        public static LanguageElement LANG_INSTALL_DONE = "下載成功! 是否重新啟動 osu!Sync";
        public static LanguageElement LANG_PLUGIN_NOT_FOUND = "找不到附加元件 {0}";
        public static LanguageElement LANG_REMOVE_DONE = "刪除成功! 是否重新啟動 osu!Sync";
        public static LanguageElement LANG_VERSION_LATEST_OR_CANEL = "{0} 為最新版，或已被使用者取消更新";
        public static LanguageElement LANG_UPDATE_CHECK_ERROR = "無法依據 [{0}] 檢查新: {1} : {2}";
        public static LanguageElement LANG_UPDATE_ERROR = "無法更新: {0} : {1}";

        public static LanguageElement LANG_SOURCE_NOT_SUPPORT_SEND = "接收來源 {0} 不支持發送功能";
        public static LanguageElement LANG_NO_PLUGIN_SELECT = "尚未定義附加元件名稱";
        public static LanguageElement LANG_PLUGIN_DISABLED = "已禁用";

        public static LanguageElement LANG_NO_ANY_SOURCE = "沒有任何彈幕接收來源，請檢查 Plugins 資料夾";
        public static LanguageElement LANG_Instance_Exist = "有超過一個 osu!Sync 正在執行中，請關閉其他的 osu!Sync";
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