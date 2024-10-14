using System;
using System.Windows;
using System.Windows.Controls;
using Pharmacy.INST.CommonLib;
using Pharmacy.INST.DissolutionClient.common;
using System.Data;
using com.ccg.GeckoKit;
using com.ccg.GeckoKit.api;
using com.ccg.PrivelegeKit;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// ReviewPwdModal.xaml 的交互逻辑
    /// </summary>
    public partial class SignLoginModal : Window
    {
        private dynamic m_TestData;
        private double[,] m_RunCupTempArr;                        //实验温度样本数组

        public ExperimentDataModal m_ExperimentDataModal;
        public SignLoginModal(dynamic testData, ExperimentDataModal experimentDataModal, double[,] arr)
        {
            InitializeComponent();
            InitializeInterface();
            m_TestData = testData;
            m_RunCupTempArr = arr;
            m_ExperimentDataModal = experimentDataModal;
        }
        public void InitializeInterface()
        {
            SLM_MW.Title = App.m_LangPackage.SLM_MW;
            LB_SLM_TB_LOGINNAME.Content = App.m_LangPackage.LB_SLM_TB_LOGINNAME;
            LB_SLM_PB_LOGINPWD.Content = App.m_LangPackage.LB_SLM_PB_LOGINPWD;
            LB_SLM_TB_SIGNCONTENT.Content = App.m_LangPackage.LB_SLM_TB_SIGNCONTENT;
            LB_SLM_ONEKEYINPUT.Content = App.m_LangPackage.LB_SLM_ONEKEYINPUT;
            SLM_BTN_PASS.Content = App.m_LangPackage.SLM_BTN_PASS;
            SLM_BTN_FAIL.Content = App.m_LangPackage.SLM_BTN_FAIL;
            SLM_BTN_CONFIRM.Content = App.m_LangPackage.SLM_BTN_CONFIRM;
            SLM_BTN_CANCEL.Content = App.m_LangPackage.SLM_BTN_CANCEL;
           
        }
        private void SignLogin(string strAccount, string strPassword, string strReviewContent)
        {
            if (string.IsNullOrEmpty(strAccount) || string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SLM_INPUT_ACCOUNTORPASSWORD, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!UIOperator.LoginNameRegex(strAccount))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SLM_ACCOUNT_CHAR_UNNORMAL, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!CheckValidation(strAccount))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SLM_ACCOUNT_NOTEXSIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!CheckAccountExpire(strAccount))
            {
                string strCmd = string.Format(sql.SQL.SQL_U_USERSTATUS, StaticParam.EntitiesStatusArr[2], strAccount);
                App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);

                MessageBox.Show(App.m_LangPackage.TIP_SLM_ACCOUNT_EXPIRE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!CheckPwdValidation(strAccount, CommonLib.CommonLib.EcryptPassword(strPassword)))
            {
                SLM_PB_LOGINPWD.Text = "";
                MessageBox.Show(App.m_LangPackage.TIP_SLM_PASSWORD_EXPIRE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                ReviewPwdModal reviewPwdModal = new ReviewPwdModal(strAccount, strPassword);
                reviewPwdModal.ShowDialog();
                return;
            }
            string strReviewer = strAccount;
            string strReviewerName = string.Empty;
            if (CheckAuthority(strAccount, CommonLib.CommonLib.EcryptPassword(strPassword), App.GetFunctionName(19), ref strReviewerName))
            {
                // 提示更新等待确认
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_SLM_UPDATE_SIGN_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.No: return;
                }
                
                string strReviewTime = BaseUtils.GetCurrentDateTime();
                string strSignReportName = string.Format("report_{0}.sign", BaseUtils.GetDateTimeString(DateTime.Parse(m_TestData.StartTime)));
                new NameReportModal(CallBackPrintReport, App.m_LangPackage.RPT_TITLE, strSignReportName, strReviewerName,strReviewer, strReviewContent, strReviewTime).ShowDialog();
                App.m_LogUtils.WorkLogList.Add(new WorkLog(strAccount, App.GetLogType(1), App.GetBehavior(101), App.GetBehaviorRemark(101)));
                Close();
                return;
            }
            App.m_LogUtils.WorkLogList.Add(new WorkLog(strAccount, App.GetLogType(1), App.GetBehavior(102), App.GetBehaviorRemark(102)));
            MessageBox.Show(App.m_LangPackage.TIP_SLM_SIGN_LOGIN_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        //回调打印报告
        public void CallBackPrintReport(string reporttile, string reportname, string reviewer
            , string reviewerid, string content, string reportdatetime)
        {
            new Printer(m_TestData).PrintPDF(reporttile, reportname, reviewer,reviewerid,content,reportdatetime, m_RunCupTempArr);  //保存PDF报告
            MessageBox.Show(App.m_LangPackage.TIP_SLM_SIGNED_REPORT_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// 检查账户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckValidation(string account)
        {
            DataSet ds = new DataSet();
            string strTable = "tmp_user_exsit";
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_ACCOUNTIFEXIST, account), ds, strTable);
            if (ds.Tables[strTable].Rows.Count == 1) //存在该账户
            {
                if (ds.Tables[strTable].Rows[0]["status"].ToString().Equals(StaticParam.EntitiesStatusArr[0]))
                    return true;
            }
            return false;
        }
        private bool CheckAccountExpire(string account)
        {
            DataSet ds = new DataSet();
            string strTable = "tmp_user_expire";
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_ACCOUNTIFEXPIRE, account), ds, strTable);
            if (ds.Tables[strTable].Rows.Count == 1) //存在该账户
            {
                string strValidDate = ds.Tables[strTable].Rows[0]["valid_date"].ToString();
                DateTime dtValidDate = DateTime.Parse(strValidDate);
                DateTime dtCurrentTime = DateTime.Now;
                TimeSpan ts = dtCurrentTime.Subtract(dtValidDate);
                if (ts.Days <= 0)// 没到期
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查用户权限
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckAuthority(string account, string password, string functionname, ref string username)
        {
            if (account.Equals(StaticParam.ROOT_LOGINNAME) && CommonLib.CommonLib.DeEcryptPassword(password).Equals(StaticParam.ROOT_PWD))
            {
                username = "超级管理员";
                return true;
            }
            if (account.Equals(StaticParam.DEF_ROOTLOGINNAME) && CommonLib.CommonLib.DeEcryptPassword(password).Equals(StaticParam.DEF_ROOTPWD))
            {
                username = "超级管理员";
                return true;
            }
            try
            {
                DataSet ds = new();
                App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_USERBYACCOUNT, account, password), ds, sql.SQL.T_USER);
                if (ds.Tables[sql.SQL.T_USER].Rows.Count > 0)
                {
                    int RoleID = int.Parse(ds.Tables[sql.SQL.T_USER].Rows[0]["RoleID"].ToString());
                    username = ds.Tables[sql.SQL.T_USER].Rows[0]["UserName"].ToString();
                    TSession tSession = new TSession();
                    Tools.PutVal<TUser>(tSession.TTUser, ds.Tables[sql.SQL.T_USER].Rows[0]);
                    App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_PRIVELEGE, RoleID), ds, sql.SQL.T_PRIVELEGE);
                    if (ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count; i++)
                        {
                            string strPrivelegeName = ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString();
                            if (strPrivelegeName.Equals(functionname))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteLogFile(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 检查用户口令是否过期
        /// </summary>
        /// <returns></returns>
        private bool CheckPwdValidation(string account, string password)
        {
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_USERBYACCOUNT, account, password), ds, sql.SQL.T_USER);
            try
            {
                if (ds.Tables[sql.SQL.T_USER].Rows.Count > 0)
                {
                    string PwdReviewDateTime = ds.Tables[sql.SQL.T_USER].Rows[0]["PwdReviewDatetime"].ToString();
                    DateTime dtPwdReviewDateTime = DateTime.Parse(PwdReviewDateTime);
                    DateTime dtCurrentTime = DateTime.Now;
                    TimeSpan ts = dtCurrentTime.Subtract(dtPwdReviewDateTime);
                    if (ts.Days > App.m_nPwdValitionDays)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return true;
        }
        //通过
        private void SLM_BTN_PASS_Click(object sender, RoutedEventArgs e)
        {
            SLM_TB_SIGNCONTENT.Text = ((Button)sender).Content.ToString();
        }
        //不通过
        private void SLM_BTN_FAIL_Click(object sender, RoutedEventArgs e)
        {
            SLM_TB_SIGNCONTENT.Text = ((Button)sender).Content.ToString();
        }
        //确认
        private void SLM_BTN_CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SLM_TB_LOGINNAME.Text) || string.IsNullOrEmpty(SLM_PB_LOGINPWD.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SLM_ACCOUNT_ISNOTNULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(SLM_TB_SIGNCONTENT.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SLM_REVISION_ISNOTNULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //提示自定义报告参数
            if (SLM_TB_SIGNCONTENT.Text.Equals(String.Empty))
            {
                MessageBox.Show(App.m_LangPackage.TIP_SLM_PLEASE_INPUT_SIGN, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SignLogin(SLM_TB_LOGINNAME.Text, SLM_PB_LOGINPWD.Text, SLM_TB_SIGNCONTENT.Text);
        }
       
        //取消
        private void SLM_BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
