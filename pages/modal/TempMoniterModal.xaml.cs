using System;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// TempMoniterModal.xaml 的交互逻辑
    /// </summary>
    public partial class TempMoniterModal : Window
    {
        private int index = 0;
        private MainWindow m_MainWindow;
        public TempMoniterModal(MainWindow mainWindow)
        {
            InitializeComponent();
            InitializeInterface();
            this.m_MainWindow = mainWindow;
            Task.Factory.StartNew(RecordData);
        }
        public void InitializeInterface()
        {
            TMM_MW.Title = App.m_LangPackage.TMM_MW;
            LowLimit.Content = App.m_LangPackage.LowLimit;
            NormalTEMP.Content = App.m_LangPackage.NormalTEMP;
            UpperLimit.Content = App.m_LangPackage.UpperLimit;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void RecordData()
        {
            // 持续生成随机数，模拟数据处理过程
            while (true)
            {
                Thread.Sleep(1000);
                // 更新图表数据
                double yMin = m_MainWindow.dRealWaterTemp - 0.5;
                double yMax = m_MainWindow.dRealWaterTemp + 0.5;

                TempCurver.yValue = (float)m_MainWindow.dRealWaterTemp;
                string color = "Yellow";
                if (m_MainWindow.dRealWaterTemp < 37) 
                    color = "Blue";
                if (m_MainWindow.dRealWaterTemp > 38)
                    color = "Red";
                TempCurver.Dispatcher.Invoke(new Action(delegate
                {
                    TempCurver.SetBoundY(yMin, yMax);
                    TempCurver.SetStrokeColor(color);
                }));


                
                
                TempCurver.Index = index++;
            }
        }
    }
}