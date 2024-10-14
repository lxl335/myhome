using System;
using System.Windows;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// InputSerialNoModal.xaml 的交互逻辑
    /// </summary>
    public partial class InputSerialNoModal : Window
    {
        public delegate void CallBackSaveSerialNo(string serialno);
        public CallBackSaveSerialNo callBackSaveSerialNo;
        public int m_RemainDays;
        public InputSerialNoModal(CallBackSaveSerialNo callBackSaveSerialNo)
        {
            this.callBackSaveSerialNo = callBackSaveSerialNo;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            IM_MW.Title = App.m_LangPackage.IM_MW;
            LB_INM_TB_SERIALNO.Content = App.m_LangPackage.LB_INM_TB_SERIALNO;
            INM_REGISTER.Content = App.m_LangPackage.INM_REGISTER;
            INM_CLOSE.Content = App.m_LangPackage.INM_CLOSE;
        }
        public InputSerialNoModal(CallBackSaveSerialNo callBackSaveSerialNo, int days)
        {
            InitializeComponent();
            this.callBackSaveSerialNo = callBackSaveSerialNo;
            m_RemainDays = days;
            string tip = string.Format("软件有效期剩余{0}天", m_RemainDays);
            if (m_RemainDays < 0) tip = string.Format("软件已过期{0}天", -m_RemainDays);
            INM_VALIDDATE.Content = tip;
        }
        //注册序列号
        private void INM_REGISTER_Click(object sender, RoutedEventArgs e)
        {
            if (!INM_TB_SERIALNO.Text.Equals(String.Empty))
            {
                string SerialNo = INM_TB_SERIALNO.Text.ToString();
                if (!SerialNo.EndsWith("=BJ-BY-KY"))
                    SerialNo += "=BJ-BY-KY";
                bool result = CommonLib.CommonLib.IsOverTime(SerialNo);
                if (!result)
                {
                    //callBackSaveSerialNo(INM_TB_SERIALNO.Text.ToString());
                    callBackSaveSerialNo(SerialNo);
                    DialogResult = true;
                    Close();
                }
                else
                    MessageBox.Show(App.m_LangPackage.TIP_INM_SN_INVALID, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //关闭窗口
        private void INM_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
