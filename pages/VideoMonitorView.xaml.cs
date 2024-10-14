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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pharmacy.INST.DissolutionClient.entities;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// VideoMonitorView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoMonitorView : Page
    {
        public VideoMonitorView()
        {
            InitializeComponent();
            InitializeComponentEx();
        }

        public void InitializeComponentEx()
        {
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(9), App.GetBehaviorRemark(9)));
        }
    }
}
