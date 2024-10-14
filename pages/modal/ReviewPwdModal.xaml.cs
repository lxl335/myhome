using System;
using System.Windows;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.CommonLib;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// ReviewPwdModal.xaml 的交互逻辑
    /// </summary>
    public partial class ReviewPwdModal : Window
    {
        private string account;
        private string pwd;
        public ReviewPwdModal(string account,string pwd)
        {
            this.account = account;
            this.pwd = pwd;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            RPM_MW.Title = App.m_LangPackage.RPM_MW;
            LB_RPM_PB_LOGINPWD.Content = App.m_LangPackage.LB_RPM_PB_LOGINPWD;
            LB_RPM_PB_LOGINPWD_TWICE.Content = App.m_LangPackage.LB_RPM_PB_LOGINPWD_TWICE;
            RPM_BTN_CONFIRM.Content = App.m_LangPackage.RPM_BTN_CONFIRM;
            RPM_BTN_CANCEL.Content = App.m_LangPackage.RPM_BTN_CANCEL;
        }
        private void RPM_BTN_CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            if (!RPM_PB_LOGINPWD.Password.Equals(RPM_PB_LOGINPWD_TWICE.Password))
            {
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_INCONSISTENCY, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!UIOperator.LoginPwdRegex(RPM_PB_LOGINPWD.Password.ToString()))
            {
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (RPM_PB_LOGINPWD.Password.Equals(pwd))
            {
                MessageBox.Show(App.m_LangPackage.TIP_RPM_TWICE_PASSWORD_CONSISTENT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string strCmd = String.Format(sql.SQL.SQL_U_USERPWDBYACCOUNT, CommonLib.CommonLib.EcryptPassword(RPM_PB_LOGINPWD_TWICE.Password),
                            BaseUtils.GetCurrentDateTime(),this.account, CommonLib.CommonLib.EcryptPassword(this.pwd));
            if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd)>0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_RPM_PASSWORD_REVIEW_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }

        }

        private void RPM_BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
