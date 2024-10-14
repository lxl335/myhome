using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// AppointmentTimeModal.xaml 的交互逻辑
    /// </summary>
    public partial class AppointmentTimeModal : Window
    {

        public delegate void CallBackRecviceAppointmentTime(int nAppointmentTime);
        public CallBackRecviceAppointmentTime callBackRecviceAppointmentTime;

        public AppointmentTimeModal(CallBackRecviceAppointmentTime callBackRecviceAppointmentTime)
        {
            this.callBackRecviceAppointmentTime = callBackRecviceAppointmentTime;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            ATM_WM.Title = App.m_LangPackage.ATM_WM;
            LB_TB_APPOINTMENTTIME.Content = App.m_LangPackage.LB_TB_APPOINTMENTTIME;
            LB_TB_APPOINTMENTTIME_UNIT.Content = App.m_LangPackage.LB_TB_APPOINTMENTTIME_UNIT;
            BTN_CONFRIM.Content = App.m_LangPackage.BTN_CONFRIM;
            BTN_CANCLE.Content = App.m_LangPackage.BTN_CANCLE;
        }
        private void TB_APPOINTMENTTIME_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = BaseUtils.ControlInput0To9(e);
        }
        private void BTN_CONFRIM_Click(object sender, RoutedEventArgs e)
        {
            string strAppointmentTime = TB_APPOINTMENTTIME.Text.Trim();

            if (strAppointmentTime.Equals("")|| strAppointmentTime.Equals("0")) //不设置预约时间和预约设置为0，表示立即执行
            {
                callBackRecviceAppointmentTime(0);  //接口 此处0表示立即执行
                this.Close();
                return;
            }
            try
            {
                int nAppointmentTime = int.Parse(strAppointmentTime);
                if (nAppointmentTime > 0)  
                {
                    this.Close();
                    callBackRecviceAppointmentTime(nAppointmentTime * 60); //接口 此处分钟转为秒，并以秒为倒计时单位
                    
                    return;
                }
            }
            catch (Exception ex)
            {
               
                Console.Write(ex.ToString());
                return;
            }
            MessageBox.Show(App.m_LangPackage.TIP_SET_TIMER_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        private void BTN_CANCLE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
