using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// PrepareTimeModal.xaml 的交互逻辑
    /// </summary>
    public partial class PrepareTimeModal : Window
    {
        public delegate void CallBackRecvicePrepareTime(int nPrepareTime,bool bStauts);
        public CallBackRecvicePrepareTime callBackRecvicePrepareTime;

        public PrepareTimeModal(CallBackRecvicePrepareTime callBackRecvicePrepareTime)
        {
            this.callBackRecvicePrepareTime = callBackRecvicePrepareTime;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            PTM_MW.Title = App.m_LangPackage.PTM_MW;
            LB_PTM_TB_PREPARETIME.Content = App.m_LangPackage.LB_PTM_TB_PREPARETIME;
            LB_PTM_TB_PREPARETIME_UNIT.Content = App.m_LangPackage.LB_PTM_TB_PREPARETIME_UNIT;
            BTN_CONFRIM.Content = App.m_LangPackage.BTN_CONFRIM;
            BTN_CANCLE_PREPARETIME.Content = App.m_LangPackage.BTN_CANCLE_PREPARETIME;
            BTN_CANCLE.Content = App.m_LangPackage.BTN_CANCLE;
           
        }
        private void TB_PREPARETIME_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = BaseUtils.ControlInput0To9(e);
        }
        private void BTN_CONFRIM_Click(object sender, RoutedEventArgs e)
        {
            string strPrepareTime = TB_PREPARETIME.Text.Trim();

            if (strPrepareTime.Equals("") || strPrepareTime.Equals("0")) //不设置预约时间和预约设置为0，表示立即执行
            {
                MessageBox.Show(App.m_LangPackage.TIP_SET_TIMER, App.m_LangPackage.TIP, MessageBoxButton.OK,MessageBoxImage.Warning);  //接口 此处0表示立即执行
                return;
            }
            try
            {
                int nPrepareTime = int.Parse(strPrepareTime);
                if (nPrepareTime > 0)
                {
                    this.Close();
                    callBackRecvicePrepareTime(nPrepareTime * 60,true); //接口 此处分钟转为秒，并以秒为倒计时单位

                    return;
                }
            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
                return;
            }
            MessageBox.Show(App.m_LangPackage.TIP_SET_TIMER_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BTN_CANCLE_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //取消已经设置成功的倒计时
        private void BTN_CANCLE_PREPARETIME_Click(object sender, RoutedEventArgs e)
        {
            callBackRecvicePrepareTime(0, false);
            Close();
        }
    }
}
