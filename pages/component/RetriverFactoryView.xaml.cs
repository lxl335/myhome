using System;
using System.Windows;
using System.Windows.Controls;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.CommonLib;

namespace Pharmacy.INST.DissolutionClient.pages.component
{
    /// <summary>
    /// RetriverFactoryView.xaml 的交互逻辑
    /// </summary>
    public partial class RetriverFactoryView : Page
    {
        public RetriverFactoryView()
        {
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            RFV_LB_WARNING.Text = App.m_LangPackage.RFV_LB_WARNING;
            SMV_BTN_RETRIVERFACTORY.Content = App.m_LangPackage.SMV_BTN_RETRIVERFACTORY;
        }
        private void SMV_BTN_RETRIVERFACTORY_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(22), true))
            {
                try
                {
                    MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_RFV_RETRIVERFACTORY_CONFIRM, App.m_LangPackage.WARNING, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    switch (mBoxResult)
                    {
                        case MessageBoxResult.Yes:
                            App.ClearDatabase();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }
    }
}