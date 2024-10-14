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
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.pages.modal;
using System.Data;
using com.ccg.PrivelegeKit;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;
using com.ccg.GeckoKit.api;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        private double m_PosX;
        private double m_PosY;
        private String m_strBackgroundImage;
        private ImageBrush m_imageBrush;
        private ImageButton m_btnImageExitApp;
        private ImageButton m_btnImageLogin;
        private ImageSwitchButton m_btnPwdVisibleSwitch;
        private TextBox m_tbAccount;
        private TextBox m_pbPassword;
        private Label m_AppVersionIcon;

        private MainWindow m_MainWindow;

        public LoginView()
        {
            InitializeComponent();
            InitializeComponentEx();
        }
        public void InitializeComponentEx()
        {
            m_PosX = 410;
            m_PosY = 230;

            m_imageBrush = new ImageBrush();
            m_strBackgroundImage = App.g_ResourceDir + "login_bg.png";
            m_imageBrush.ImageSource = new BitmapImage(new Uri(m_strBackgroundImage, UriKind.RelativeOrAbsolute));
            this.Background = m_imageBrush;
            //加载退出系统图标
            m_btnImageExitApp = new ImageButton(App.g_ResourceDir + "btn_close.png", App.g_ResourceDir + "btn_close_hover.png");
            m_btnImageExitApp.SetValue(Canvas.LeftProperty, (double)m_PosX + 415);
            m_btnImageExitApp.SetValue(Canvas.TopProperty, (double)m_PosY - 55);
            m_btnImageExitApp.Width = (double)30;
            m_btnImageExitApp.Height = (double)30;
            m_btnImageExitApp.MouseLeftButtonUp += new MouseButtonEventHandler(m_btnImageExitApp_Click);
            MainView.Children.Add(m_btnImageExitApp);

            //加载Account
            m_tbAccount = new TextBox();
            m_tbAccount.Text = "";
            InputMethod.SetPreferredImeState(m_tbAccount, InputMethodState.Off);
            m_tbAccount.SetValue(StyleProperty, Application.Current.Resources["TxtVisible"]);
            m_tbAccount.SetValue(Canvas.LeftProperty, (double)m_PosX + 120);
            m_tbAccount.SetValue(Canvas.TopProperty, (double)m_PosY + 18);
            m_tbAccount.Width = 240;
            m_tbAccount.Height = 30;
            MainView.Children.Add(m_tbAccount);
            //加载password box
            m_pbPassword = new TextBox();
            m_pbPassword.SetValue(StyleProperty, Application.Current.Resources["TxtPwd"]);
            m_pbPassword.SetValue(Canvas.LeftProperty, (double)m_PosX + 120);
            m_pbPassword.SetValue(Canvas.TopProperty, (double)(m_PosY + 85));
            m_pbPassword.Width = 240;
            m_pbPassword.Height = 30;
            MainView.Children.Add(m_pbPassword);
            //加载口令明文/加密切换按钮
            m_btnPwdVisibleSwitch = new ImageSwitchButton(App.g_ResourceDir + "pw_hidden.png", App.g_ResourceDir + "pw_visible.png");
            m_btnPwdVisibleSwitch.SetValue(Canvas.LeftProperty, (double)m_PosX + 360);
            m_btnPwdVisibleSwitch.SetValue(Canvas.TopProperty, (double)m_PosY + 85);
            m_btnPwdVisibleSwitch.Width = (double)28;
            m_btnPwdVisibleSwitch.Height = (double)28;
            m_btnPwdVisibleSwitch.MouseLeftButtonUp += new MouseButtonEventHandler(imagebtn_PwdVisibleSwitch_Click);
            MainView.Children.Add(m_btnPwdVisibleSwitch);

            //加载登录对话框
            m_btnImageLogin = new ImageButton(App.g_ResourceDir + "botton_dl.png", App.g_ResourceDir + "botton_dl_hover.png");
            m_btnImageLogin.SetValue(Canvas.LeftProperty, (double)m_PosX + 28);
            m_btnImageLogin.SetValue(Canvas.TopProperty, (double)m_PosY + 138);
            m_btnImageLogin.Width = (double)382;
            m_btnImageLogin.Height = (double)82;
            m_btnImageLogin.MouseLeftButtonUp += new MouseButtonEventHandler(imagebtn_Login_Click);
            MainView.Children.Add(m_btnImageLogin);

            m_AppVersionIcon = new Label();
            m_AppVersionIcon.FontSize = 16;
            m_AppVersionIcon.Content = string.Format("{0}{1}", App.m_LangPackage.AppVersionIcon , App.m_strAppVersion);
            m_AppVersionIcon.SetValue(Canvas.LeftProperty, (double)(Width - 130));
            m_AppVersionIcon.SetValue(Canvas.TopProperty, (double)(Height - 40));
            m_AppVersionIcon.Width = 200;
            m_AppVersionIcon.Height = 30;
            MainView.Children.Add(m_AppVersionIcon);
            if (App.m_strDefAccount.Equals(StaticParam.ROOT_LOGINNAME))
            {
                m_tbAccount.Text = StaticParam.ROOT_LOGINNAME;
                m_pbPassword.Text = StaticParam.ROOT_PWD;
            }
            //账号输入框设置为起始焦点
            //m_tbAccount.Focus(); 阻止平板打开时即弹出键盘

        }
        #region 窗口事件
        //显示口令/关闭口令
        private void imagebtn_PwdVisibleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (((ImageSwitchButton)sender).Status)
            {
                m_pbPassword.SetValue(TextBox.StyleProperty, Application.Current.Resources["TxtVisible"]);
            }
            else
            {
                m_pbPassword.SetValue(TextBox.StyleProperty, Application.Current.Resources["TxtPwd"]);
            }

        }
        //登录系统 按钮事件
        private void imagebtn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckAppValidTime()) Login();
        }
        //退出系统
        private void m_btnImageExitApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
        //登录
        private void Login()
        {
            string strAccount = m_tbAccount.Text.Trim();
            string strPassword = m_pbPassword.Text.Trim();

            if (string.IsNullOrEmpty(strAccount) || string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show(App.m_LangPackage.TIP_INPUT_ACCOUNT, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!UIOperator.LoginNameRegex(strAccount))
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_EXSIT_ERRORCHAR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!CheckValidation(strAccount))
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_NOTEXSIT_INVALID, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!CheckAccountExpire(strAccount))
            {
                string strCmd = string.Format(sql.SQL.SQL_U_USERSTATUS, StaticParam.EntitiesStatusArr[2], strAccount);
                App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);

                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_EXPIRE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!CheckPwdValidation(strAccount, CommonLib.CommonLib.EcryptPassword(strPassword)))
            {
                m_pbPassword.Text = "";
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_EXPIRE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                ReviewPwdModal reviewPwdModal = new ReviewPwdModal(strAccount, strPassword);
                reviewPwdModal.ShowDialog();
                return;
            }

            if (CheckAuthority(strAccount, CommonLib.CommonLib.EcryptPassword(strPassword)))
            {
                //查看是否为已注册状态，如果已注册进入系统，如果没有注册提示联网注册
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(0), App.GetBehaviorRemark(0)));
                //刷新该账户最近登录的时间
                App.m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_U_USERLASTLOGINTIME, BaseUtils.GetCurrentDateTime(),
                        strAccount, CommonLib.CommonLib.EcryptPassword(strPassword)));
                //刷新该账户登录次数
                App.m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_U_USERLOGINTIMES,
                        strAccount, CommonLib.CommonLib.EcryptPassword(strPassword)));

                m_MainWindow = new MainWindow();
                m_MainWindow.Show();
                this.Close();
            }
            else
            {
                string strCmd = String.Format("SELECT pwd_error_times FROM tbl_user WHERE login_name='{0}'", strAccount);
                DataSet ds1 = new DataSet();
                App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds1, "temp");
                if (ds1.Tables["temp"].Rows.Count == 1)
                {
                    int errortimes = int.Parse(ds1.Tables["temp"].Rows[0][0].ToString());
                    if (errortimes == 5)
                    {
                        //锁定账户
                        strCmd = String.Format("UPDATE tbl_user SET status='{0}' WHERE login_name='{1}'", App.g_EngVer? "Locked" : "锁定", strAccount);
                        App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);
                        MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_LOCKED + StaticParam.SYSMANAGE_ROLE, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                        MessageBox.Show(String.Format(App.m_LangPackage.TIP_HAVE_NTIMES_PASSWORD, errortimes), App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
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
        private bool CheckAuthority(string account, string password)
        {
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_USERBYACCOUNT, account, password), ds, sql.SQL.T_USER);
            try
            {
                if (ds.Tables[sql.SQL.T_USER].Rows.Count > 0)
                {
                    try
                    {
                        int RoleID = int.Parse(ds.Tables[sql.SQL.T_USER].Rows[0]["RoleID"].ToString());
                        Tools.PutVal<TUser>(App.g_TSession.TTUser, ds.Tables[sql.SQL.T_USER].Rows[0]);
                        App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_PRIVELEGE, RoleID), ds, sql.SQL.T_PRIVELEGE);
                        if (ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count; i++)
                            {
                                int type = int.Parse(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["Type"].ToString());
                                if (type == 0)
                                    App.g_TSession.TModuleList.Add(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString().Trim());
                                if (type == 1)
                                    App.g_TSession.TFunctionList.Add(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString().Trim());
                            }
                        }
                        string strCmd = String.Format("UPDATE tbl_user set pwd_error_times = 0 WHERE login_name='{0}'", account);
                        App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    return true;
                }
                else
                {
                    //口令错误计数
                    string strCmd = String.Format("UPDATE tbl_user set pwd_error_times = pwd_error_times + 1 WHERE login_name='{0}'", account);
                    App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);
                }
            }
            catch (Exception ex)
            {
                ds.Dispose();
                ds = null;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (App.CheckAppValidTime()) Login();
            }
        }
    }
}
