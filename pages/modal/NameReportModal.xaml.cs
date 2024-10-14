using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// NameReport.xaml 的交互逻辑
    /// </summary>
    public partial class NameReportModal : Window
    {
        public delegate void CallBackPrintReport(string reporttile, string reportname, string reviewer
            , string reviewerid, string content, string reportdatetime);
        public CallBackPrintReport callBackPrintReport;

        private string m_ReportTitle;
        private string m_ReportFileName;
        private string m_Reviewer;
        private string m_ReviewerID;
        private string m_Content;
        private string m_ReportDateTime;

        public NameReportModal(CallBackPrintReport callBackPrintReport,string reporttitle, string reportfilename, string reviewer
            , string reviewerid, string content, string reportdatetime)
        {
            this.callBackPrintReport = callBackPrintReport;
            m_ReportTitle = reporttitle;
            m_ReportFileName = reportfilename;
            m_Reviewer = reviewer;
            m_ReviewerID = reviewerid;
            m_Content = content;
            m_ReportDateTime = reportdatetime;
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }

        public void InitializeComponentEx()
        {
            NRM_REPORTTITLE.Text = m_ReportTitle;
            NRM_REPORTNAME.Text = m_ReportFileName;
        }
        public void InitializeInterface()
        {
            NRM_MW.Title = App.m_LangPackage.NRM_MW;
            NRM_SELFDEFPARAMS_GROUPBOX.Header = App.m_LangPackage.NRM_SELFDEFPARAMS_GROUPBOX;
            LB_NRM_REPORTTITLE.Content = App.m_LangPackage.LB_NRM_REPORTTITLE;
            LB_NRM_REPORTNAME.Content = App.m_LangPackage.LB_NRM_REPORTNAME;
            BTN_CONFIRM.Content = App.m_LangPackage.BTN_CONFIRM;
            BTN_CANCEL.Content = App.m_LangPackage.BTN_CANCEL;
        }
        //确认报告标题及文件名事件
        private void BTN_CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            if (NRM_REPORTTITLE.Equals(String.Empty) || NRM_REPORTNAME.Equals(String.Empty))
            {
                MessageBox.Show(App.m_LangPackage.TIP_TITLE_NAME_ISNULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            callBackPrintReport(NRM_REPORTTITLE.Text.Trim(), NRM_REPORTNAME.Text.Trim(), m_Reviewer,m_ReviewerID,m_Content,m_ReportDateTime);
            Close();
        }
        //关闭取消
        private void BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
