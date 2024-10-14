using System.Windows;
using System.Threading;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// ReconnectModal.xaml 的交互逻辑
    /// </summary>
    public partial class ReconnectModal : Window
    {
        public MainWindow m_MainWindow;
        public ReconnectModal(MainWindow mainWindow)
        {
            m_MainWindow = mainWindow;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            RMM_MW.Title = App.m_LangPackage.RMM_MW;
            RCM_BTN_CONNECT.Content = App.m_LangPackage.RCM_BTN_CONNECT;
            RCM_BTN_EXIT.Content = App.m_LangPackage.RCM_BTN_EXIT;
        }
        private void RCM_BTN_CONNECT_Click(object sender, RoutedEventArgs e)
        {
            if (m_MainWindow.LB_MAIN_STATUS.Content.Equals(StaticParam.Startup_status_Arr[1]))
            {
                if (m_MainWindow.InitSerialsComm())
                {
                    Thread thread = new Thread(m_MainWindow.DeviceReset);
                    thread.Start();
                }
            }
            //m_MainWindow.SetPrinterCommStatus(m_MainWindow.InitPrinterComm());
            //m_MainWindow.InitializeStatusBar();
            this.Close();
        }

        private void RCM_BTN_EXIT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
