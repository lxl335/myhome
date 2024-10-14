using System;
using System.Windows;
using System.Data;
using Pharmacy.INST.DissolutionClient.common;
using com.ccg.PrivelegeKit;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// UserModal.xaml 的交互逻辑
    /// </summary>
    public partial class UserModal : Window
    {
        public delegate void CallBackRefreshUserList();
        public CallBackRefreshUserList callBackRefreshUserList;
        private TUser m_TUser;
        public UserModal(TUser tUser, CallBackRefreshUserList callBackRefreshUserList)
        {
            m_TUser = tUser;
            this.callBackRefreshUserList = callBackRefreshUserList;
            InitializeComponent();
            InitializeComponentEx();
        }
        public UserModal(TUser tUser)
        {
            m_TUser = tUser;
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        //窗口组件初始化
        private void InitializeComponentEx()
        {
            //加载角色方法下拉框
            string strCmd = String.Format(sql.SQL.SQL_Q_ROLE); //strMethodName 参数
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_ROLE);
            if (ds.Tables[sql.SQL.T_ROLE].Rows.Count > 0) //
            {
                UIOperator.ComboBoxBinder(UM_COMBO_ROLE, ds.Tables[sql.SQL.T_ROLE], "name", "id");
            }
            UIOperator.ComboBoxBinder(UM_COMBO_STATUS, StaticParam.EntitiesStatusArr);

            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(12), true))
            {
                UM_PB_LOGINPWD.IsEnabled = true;
                UM_PB_LOGINPWD_TWICE.IsEnabled = true;
                UM_TB_USERNAME.IsEnabled = true;
                UM_TB_MOBILE.IsEnabled = true;
                UM_TB_EMAIL.IsEnabled = true;
                if (App.g_TSession.IsManager() || App.g_TSession.IsRootManager())
                {
                    UM_COMBO_STATUS.IsEnabled = true;
                    UM_DP_VALIDDATE.IsEnabled = true;
                }
                if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(16), false))
                    UM_COMBO_ROLE.IsEnabled = true;
                
                UM_BTN_SAVE.IsEnabled = true;
            }
            else
            {
                UM_PB_LOGINPWD.IsEnabled = true;
                UM_PB_LOGINPWD_TWICE.IsEnabled = true;
                UM_TB_USERNAME.IsEnabled = true;
                UM_TB_MOBILE.IsEnabled = true;
                UM_TB_EMAIL.IsEnabled = true;
                UM_BTN_SAVE.IsEnabled = true;
            }
        }
        public void InitializeInterface()
        {
            UserModalView.Title = App.m_LangPackage.UserModalView;
            UM_ACCOUNTINFO_GROUPBOX.Header = App.m_LangPackage.UM_ACCOUNTINFO_GROUPBOX;
            LB_UM_TB_LOGINNAME.Content = App.m_LangPackage.LB_UM_TB_LOGINNAME;
            LB_UM_PB_LOGINPWD.Content = App.m_LangPackage.LB_UM_PB_LOGINPWD;
            LB_UM_PB_LOGINPWD_TWICE.Content = App.m_LangPackage.LB_UM_PB_LOGINPWD_TWICE;
            LB_UM_COMBO_ROLE.Content = App.m_LangPackage.LB_UM_COMBO_ROLE;
            LB_UM_COMBO_STATUS.Content = App.m_LangPackage.LB_UM_COMBO_STATUS;
            LB_UM_DP_VALIDDATE.Content = App.m_LangPackage.LB_UM_DP_VALIDDATE;
            LB_UM_TB_USERNAME.Content = App.m_LangPackage.LB_UM_TB_USERNAME;
            LB_UM_TB_MOBILE.Content = App.m_LangPackage.LB_UM_TB_MOBILE;
            LB_UM_TB_EMAIL.Content = App.m_LangPackage.LB_UM_TB_EMAIL;
            UM_BTN_SAVE.Content = App.m_LangPackage.BTN_CONFRIM;
            UM_BTN_CANCEL.Content = App.m_LangPackage.BTN_CANCEL;
        }
        //窗口加载事件
        private void UserModalView_Loaded(object sender, RoutedEventArgs e)
        {
            BindModelToView(m_TUser);
        }
        //确认 按钮事件
        private void UM_BTN_SAVE_Click(object sender, RoutedEventArgs e)
        {
            UpdateUser(m_TUser);
        }
        //取消 按钮事件
        private void UM_BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //用户数据模型绑定视图Model to View
        private bool BindModelToView(TUser tUser)
        {
            UM_TB_LOGINNAME.Text = tUser.LoginName;
            UM_PB_LOGINPWD.Password = CommonLib.CommonLib.DeEcryptPassword(tUser.LoginPwd);
            UM_PB_LOGINPWD_TWICE.Password = CommonLib.CommonLib.DeEcryptPassword(tUser.LoginPwd);
            UM_TB_USERNAME.Text = tUser.UserName;
            UM_TB_MOBILE.Text = tUser.Mobile;
            UM_TB_EMAIL.Text = tUser.Email;
            UM_COMBO_ROLE.SelectedValue = tUser.RoleID;
            UM_COMBO_STATUS.Text = tUser.Status;
            UM_DP_VALIDDATE.SelectedDate = DateTime.Parse(tUser.ValidDate);
            return true;
        }

        //更新用户信息
        private bool UpdateUser(TUser tUser)
        {
            string oldLoginName = tUser.LoginName;
            string oldLoginPwd = CommonLib.CommonLib.DeEcryptPassword(tUser.LoginPwd);
            string oldUserName = tUser.UserName;
            string oldMobile = tUser.Mobile;
            string oldEmail = tUser.Email;
            string oldStatus = tUser.Status;
            string oldValidDate = tUser.ValidDate;
            tUser.LoginName = UM_TB_LOGINNAME.Text.Trim();
            tUser.UserName = UM_TB_USERNAME.Text.Trim();
            if (!UM_PB_LOGINPWD.Password.Equals(UM_PB_LOGINPWD_TWICE.Password))
            {
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_INCONSISTENCY, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);;
                return false;
            }
            if (!UIOperator.LoginPwdRegex(UM_PB_LOGINPWD.Password.ToString()))
            {
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_FORMAT_ERROR, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (UM_DP_VALIDDATE.SelectedDate == null)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NOTSET_VALIDDATE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            DateTime dtSelectedDate = DateTime.Parse(UM_DP_VALIDDATE.SelectedDate.ToString());
            DateTime dtCurrentTime = DateTime.Now;
            TimeSpan ts = dtCurrentTime.Subtract(dtSelectedDate);
            if (ts.Days > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_VALIDDATE_LESSTHAN_CURRENTDATE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            tUser.LoginPwd = CommonLib.CommonLib.EcryptPassword(UM_PB_LOGINPWD.Password);
      
            tUser.Mobile = UM_TB_MOBILE.Text;
            tUser.Email = UM_TB_EMAIL.Text;
            if (UM_COMBO_ROLE.SelectedValue == null)
            {
                if (!UM_TB_LOGINNAME.Text.Equals(StaticParam.ROOT_LOGINNAME) && !UM_TB_LOGINNAME.Text.Equals(StaticParam.DEF_ROOTLOGINNAME))
                {
                    MessageBox.Show(App.m_LangPackage.TIP_ROLE_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    tUser.RoleID = 1;
                }
            }
            else
                tUser.RoleID = int.Parse(UM_COMBO_ROLE.SelectedValue.ToString());
            if (UM_TB_LOGINNAME.Equals(StaticParam.ROOT_LOGINNAME))
                tUser.RoleID = 1;
            tUser.Status = UM_COMBO_STATUS.Text.Trim();
            tUser.CreateTime = BaseUtils.GetCurrentDateTime();
            tUser.ValidDate = UM_DP_VALIDDATE.SelectedDate.Value.ToString(String.Format("{0}", App.m_strDateFormat));
            tUser.PwdReviewDatetime = BaseUtils.GetCurrentDateTime();
            tUser.LoginTimes = 0;
            if (tUser.Status.Equals("正常") || tUser.Status.Equals("Normal"))
                tUser.PwdErrorTimes = 0;

           

            DataSet ds = new DataSet();
            //判断用户是否存在
            string strCmd = String.Format(sql.SQL.SQL_Q_FINDUSERBYKEY, tUser.LoginName); //strMethodName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.SQL_Q_FINDUSERBYKEY);
            if (ds.Tables[sql.SQL.SQL_Q_FINDUSERBYKEY].Rows.Count > 0) //登录名存在 
            {
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_UPDATE_USER_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        strCmd = String.Format(sql.SQL.SQL_U_USER, tUser.LoginName, tUser.LoginPwd, tUser.UserName,
                            tUser.Mobile, tUser.Email, tUser.RoleID.ToString(), tUser.Status, tUser.ValidDate, tUser.PwdErrorTimes,
                            tUser.ID);
                        break;
                    case MessageBoxResult.No:
                        return false;
                }
            }
            //执行SQL
            if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_UPDATE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                string reviewinfo = string.Empty;
                if (!oldUserName.Equals(tUser.UserName)) reviewinfo += "姓名 ";
                if (!oldLoginPwd.Equals(tUser.LoginPwd)) reviewinfo += "口令 ";
                if (!oldMobile.Equals(tUser.Mobile)) reviewinfo += "电话 ";
                if (!oldEmail.Equals(tUser.Email)) reviewinfo += "邮箱 ";
                if (!oldStatus.Equals(tUser.Status)) reviewinfo += string.Format("状态（修改为{0}） ", tUser.Status);
                if (!oldValidDate.Equals(tUser.ValidDate)) reviewinfo += string.Format("有效期（修改为{0}） ", tUser.ValidDate);
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1),App.GetBehavior(103) , string.Format(App.GetBehaviorRemark(103), tUser.LoginName, reviewinfo)));
                //回调刷新用户列表
                if (callBackRefreshUserList != null)
                    callBackRefreshUserList();
                Close();
                //回调刷新用户列表
                if (callBackRefreshUserList != null)
                    callBackRefreshUserList();
                this.Close();
            }
            else
            {
                MessageBox.Show(App.m_LangPackage.TIP_SAVE_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
