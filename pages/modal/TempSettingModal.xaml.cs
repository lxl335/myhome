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
    /// TempSettingModal.xaml 的交互逻辑
    /// </summary>
    public partial class TempSettingModal : Window
    {
        public delegate void CallTempSetting(double temp, bool bHeat);
        public CallTempSetting callTempSetting;
        public TempSettingModal(CallTempSetting callTempSetting)
        {
            this.callTempSetting = callTempSetting;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            TSM_MW.Title = App.m_LangPackage.TSM_MW;
            LB_TSM_TB_TEMPSETTING.Content = App.m_LangPackage.LB_TSM_TB_TEMPSETTING;
            TSM_TB_STARTHEAT.Content = App.m_LangPackage.TSM_TB_STARTHEAT;
            TSM_TB_ENDHEAT.Content = App.m_LangPackage.TSM_TB_ENDHEAT;
        }

        private void TSM_TB_STARTHEAT_Click(object sender, RoutedEventArgs e)
        {
            double temp = 37;
            try
            {
                temp = double.Parse(TSM_TB_TEMPSETTING.Text.ToString());
                if (temp < 20 || temp > 55)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_TSM_TEMP_OUT_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
            }
            catch (Exception e1)
            {
                System.Console.Write(e1.ToString());
            }
            callTempSetting(temp, true);
            App.m_dlDefaultHeatTemp = temp;
            this.Close();
            
        }

        private void TSM_TB_ENDHEAT_Click(object sender, RoutedEventArgs e)
        {
            callTempSetting(37, false);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TSM_TB_TEMPSETTING.Text = App.m_dlDefaultHeatTemp.ToString();
        }
    }
}
