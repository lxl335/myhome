using System.Windows;
using System.Windows.Controls;
using System.IO;
using System;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.DissolutionClient.pages.modal;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// AboutView.xaml 的交互逻辑
    /// </summary>
    public partial class AboutView : Page
    {
        private string m_strInstructionFile;
        private bool m_bLoaded;
        public AboutView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        //初始化组件
        public void InitializeComponentEx()
        {
            AV_APPNAME.Content = App.m_strAppTitle;
            AV_APPVERSION.Content = App.m_strAppVersion;
            AV_APPOWNER.Content = App.m_strAppOwner;
            //FileInfo fi = new FileInfo(App.g_AppDirectory + StaticParam.AppFile);
            FileInfo fi = new FileInfo(App.g_AppDirectory + "ref");
            AV_APPINSTALLDATE.Content = BaseUtils.GetDate(fi.CreationTime);
            AV_TIMELIMIT.Content = App.g_DateLimit;
            AV_APPDBSIZE.Content = BaseUtils.GetFileSizeInfo(string.Format(App.g_AppDirectory + "{0}", App.m_strDataSource));
            AV_APPLOGSIZE.Content = BaseUtils.GetFileSizeInfo(string.Format(App.g_AppDirectory + "{0}", "system.log"));
            AV_APPREPORTCOUNT.Content = BaseUtils.GetReportInfo();
            AV_APPSERIALNO.Text = App.m_strSerialNo;
            m_strInstructionFile = string.Format(App.g_AppDirectory + "{0}", App.m_strAppTitle + "_用户手册.pdf");
            AV_AuthorizedTo.Content = Environment.UserName;
        }
        public void InitializeInterface()
        {
            AV_SYSINFO_GROUPBOX.Header = App.m_LangPackage.AV_SYSINFO_GROUPBOX;
            AV_AUTHORIZEDINFO_GROUPBOX.Header = App.m_LangPackage.AV_AUTHORIZEDINFO_GROUPBOX;
            AV_HELP_GROUPBOX.Header = App.m_LangPackage.AV_HELP_GROUPBOX;
            LB_AV_APPNAME.Content = App.m_LangPackage.LB_AV_APPNAME;
            LB_AV_APPVERSION.Content = App.m_LangPackage.LB_AV_APPVERSION;
            LB_AV_APPOWNER.Content = App.m_LangPackage.LB_AV_APPOWNER;
            LB_AV_APPINSTALLDATE.Content = App.m_LangPackage.LB_AV_APPINSTALLDATE;
            LB_AV_APPDBSIZE.Content = App.m_LangPackage.LB_AV_APPDBSIZE;
            LB_AV_APPLOGSIZE.Content = App.m_LangPackage.LB_AV_APPLOGSIZE;
            LB_AV_APPREPORTCOUNT.Content = App.m_LangPackage.LB_AV_APPREPORTCOUNT;
            LB_AV_AuthorizedTo.Content = App.m_LangPackage.LB_AV_AuthorizedTo;
            LB_AV_TIMELIMIT.Content = App.m_LangPackage.LB_AV_TIMELIMIT;
            LB_AV_TIMELIMIT_UNIT.Content = App.m_LangPackage.LB_AV_TIMELIMIT_UNIT;
            LB_AV_BTN_INPUTSERIALNO.Content = App.m_LangPackage.LB_AV_BTN_INPUTSERIALNO;
            AV_BTN_INPUTSERIALNO.Content = App.m_LangPackage.AV_BTN_INPUTSERIALNO;
        }
        //主视图加载
        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            if (!m_bLoaded)
            {
                if (File.Exists(m_strInstructionFile))
                {
                    moonPdfPanel.OpenFile(m_strInstructionFile);
                    m_bLoaded = true;
                }
            }
        }
        private void AV_BTN_INPUTSERIALNO_Click(object sender, RoutedEventArgs e)
        {
            InputSerialNoModal inputSerialNoModal = new InputSerialNoModal(App.SaveSerial);
            inputSerialNoModal.ShowDialog();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(12), App.GetBehaviorRemark(12)));
        }
    }
}
