using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Data;
using com.ccg.PrivelegeKit;
using Pharmacy.INST.DissolutionClient.common;
using System.Windows.Media;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.component
{
    /// <summary>
    /// UserManageView.xaml 的交互逻辑
    /// </summary>
    public partial class UserManageView : Page
    {
        //////分页变量
        private int m_nRecordCount;
        private int m_nPageCount;
        private int m_nCurrentPage;
        private int m_nPageSize;
        private DataSet m_ds = new DataSet();  //数据集用于检索数据
        private ImageSwitchButton m_btnPwdVisibleSwitch;
        public UserManageView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
            GetModel();
        }
        //窗口组件初始化
        private void InitializeComponentEx()
        {
            //加载口令明文/加密切换按钮
            m_btnPwdVisibleSwitch = new ImageSwitchButton(App.g_ResourceDir + "pw_hidden.png", App.g_ResourceDir + "pw_visible.png");
            m_btnPwdVisibleSwitch.SetValue(Canvas.LeftProperty, (double)SMV_PB_LOGINPWD.GetValue(Canvas.LeftProperty)
                                           + (double)SMV_PB_LOGINPWD.GetValue(Canvas.WidthProperty) + 30);
            m_btnPwdVisibleSwitch.SetValue(Canvas.TopProperty, (double)SMV_PB_LOGINPWD.GetValue(Canvas.TopProperty));
            m_btnPwdVisibleSwitch.Width = (double)28;
            m_btnPwdVisibleSwitch.Height = (double)28;
            m_btnPwdVisibleSwitch.MouseLeftButtonUp += new MouseButtonEventHandler(imagebtn_PwdVisibleSwitch_Click);
            MainView.Children.Add(m_btnPwdVisibleSwitch);

            SMV_PAGING.PageSize = App.m_nPageSize.ToString();
            //加载角色方法下拉框
            BindRoleToView();
            //加载状态下拉框
            UIOperator.ComboBoxBinder(SMV_COMBO_STATUS, StaticParam.EntitiesStatusArr);
            //加载用户列表
            InitialzeUserListView();
        }
        public void InitializeInterface()
        {
            UMV_NEWUSER_GROUPBOX.Header = App.m_LangPackage.UMV_NEWUSER_GROUPBOX;
            LB_SMV_TB_LOGINNAME.Content = App.m_LangPackage.LB_SMV_TB_LOGINNAME;
            LB_SMV_PB_LOGINPWD.Content = App.m_LangPackage.LB_SMV_PB_LOGINPWD;
            LB_SMV_PB_LOGINPWD_TWICE.Content = App.m_LangPackage.LB_SMV_PB_LOGINPWD_TWICE;
            LB_SMV_COMBO_ROLE.Content = App.m_LangPackage.LB_SMV_COMBO_ROLE;
            LB_SMV_COMBO_STATUS.Content = App.m_LangPackage.LB_SMV_COMBO_STATUS;
            LB_SMV_DP_VALIDDATE.Content = App.m_LangPackage.LB_SMV_DP_VALIDDATE;
            LB_SMV_TB_USERNAME.Content = App.m_LangPackage.LB_SMV_TB_USERNAME;
            LB_SMV_TB_MOBILE.Content = App.m_LangPackage.LB_SMV_TB_MOBILE;
            LB_SMV_TB_EMAIL.Content = App.m_LangPackage.LB_SMV_TB_EMAIL;
            SMV_BTN_ADD.Content = App.m_LangPackage.SMV_BTN_ADD;
            SMV_BTN_EMPTY.Content = App.m_LangPackage.SMV_BTN_EMPTY;
            LB_SMV_REQUIRED_FIELD.Content = App.m_LangPackage.LB_SMV_REQUIRED_FIELD;
            UMV_USERLIST_GROUPBOX.Header = App.m_LangPackage.UMV_USERLIST_GROUPBOX;
            if (App.g_EngVer) SMV_PAGING.Lang = "ENG";

        }
        //显示口令/关闭口令 图片按钮 事件
        private void imagebtn_PwdVisibleSwitch_Click(object sender, RoutedEventArgs e)
        {
            if (((ImageSwitchButton)sender).Status)
            {
                SMV_PB_LOGINPWD.SetValue(TextBox.StyleProperty, Application.Current.Resources["DefaultTextBox"]);
                SMV_PB_LOGINPWD_TWICE.SetValue(TextBox.StyleProperty, Application.Current.Resources["DefaultTextBox"]);
            }
            else
            {
                SMV_PB_LOGINPWD.SetValue(TextBox.StyleProperty, Application.Current.Resources["DefaultPasswordBox"]);
                SMV_PB_LOGINPWD_TWICE.SetValue(TextBox.StyleProperty, Application.Current.Resources["DefaultPasswordBox"]);
            }
        }
        //添加 按钮事件 保存新建的用户信息
        private void SMV_BTN_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(11), true))
                AddNewAccount();
        }
        //清空 按钮事件 正在编辑的信息
        private void SMV_BTN_EMPTY_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_EDIT_EMPTY_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (mBoxResult)
            {
                case MessageBoxResult.Yes:
                    EmptyEditView();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }
        //回到首页 按钮 事件
        private void PageGoHead_Click(object sender, RoutedEventArgs e)
        {
            m_nCurrentPage = 0;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_USER);
        }
        //上一页 按钮 事件
        private void PageUp_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage > 0)
            {
                m_nCurrentPage--;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_USER);
            }
        }
        //下一页 按钮 事件
        private void PageDown_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage < m_nPageCount - 1)
            {
                m_nCurrentPage++;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_USER);
            }
        }
        //回到尾页 按钮 事件
        private void PageGoTail_Click(object sender, RoutedEventArgs e)
        {
            m_nCurrentPage = m_nPageCount - 1;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_USER);
        }
        //改变页大小 下拉框 事件
        private void PageSize_Change(object sender, RoutedEventArgs e)
        {
            m_nPageSize = int.Parse(SMV_PAGING.PageSize);
            m_nPageCount = m_nRecordCount / m_nPageSize + 1;
            if (m_nRecordCount % m_nPageSize == 0)
                m_nPageCount = m_nRecordCount / m_nPageSize;
            m_nCurrentPage = 0;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_USER);
        }
        //加载角色列表
        private void BindRoleToView()
        {
            SMV_COMBO_ROLE.ItemsSource = null;
            //加载角色方法下拉框
            string strCmd = String.Format(sql.SQL.SQL_Q_ROLE); //strMethodName 参数
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_ROLE);
            if (ds.Tables[sql.SQL.T_ROLE].Rows.Count > 0) //
            {
                UIOperator.ComboBoxBinder(SMV_COMBO_ROLE, ds.Tables[sql.SQL.T_ROLE], "name", "id");
            }
        }
        //用户列表初始化，添加编辑和删除按钮
        public void InitialzeUserListView()
        {
            GridView gridView = new GridView();
            GridViewColumn[] gvColumn = new GridViewColumn[11];
            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromRgb(0xC5, 0xF3, 0xC5));

            for (int i = 0; i < gvColumn.GetLength(0); i++)
            {
                gvColumn[i] = new GridViewColumn();
                if (i == 0)      //序号列
                {
                    RelativeSource rs = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ListViewItem), 1);
                    IndexConverter icv = new IndexConverter();
                    Binding binding = new Binding()
                    {
                        RelativeSource = rs,
                        Converter = icv
                    };
                    gvColumn[i].DisplayMemberBinding = binding;
                    gvColumn[i].Header = StaticParam.m_UserListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_UserListHeadWidth[i];
                }
                else if (i == gvColumn.GetLength(0) - 2)  //编辑列
                {
                    DataTemplate template = new DataTemplate();
                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(StackPanel));
                    template.VisualTree = factory;
                    FrameworkElementFactory btnGotoPay = new FrameworkElementFactory(typeof(Button));
                    btnGotoPay.SetValue(Button.ContentProperty, StaticParam.m_UserListHeadColumn[i]);
                    btnGotoPay.SetValue(Button.WidthProperty, 60.0);
                    btnGotoPay.SetValue(Button.HeightProperty, 40.0);
                    btnGotoPay.SetValue(Button.StyleProperty, Application.Current.Resources["DynamicButton"]);
                    btnGotoPay.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGotoEdit));
                    factory.AppendChild(btnGotoPay);
                    gvColumn[i].CellTemplate = template;
                    gvColumn[i].Header = StaticParam.m_UserListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_UserListHeadWidth[i];
                }
                else if (i == gvColumn.GetLength(0) - 1)  //删除列
                {
                    DataTemplate template = new DataTemplate();
                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(StackPanel));
                    template.VisualTree = factory;
                    FrameworkElementFactory btnDeleteSuspend = new FrameworkElementFactory(typeof(Button));
                    btnDeleteSuspend.SetValue(Button.ContentProperty, StaticParam.m_UserListHeadColumn[i]);
                    btnDeleteSuspend.SetValue(Button.WidthProperty, 60.0);
                    btnDeleteSuspend.SetValue(Button.HeightProperty, 40.0);
                    btnDeleteSuspend.SetValue(Button.StyleProperty, Application.Current.Resources["DynamicButton"]);
                    factory.AppendChild(btnDeleteSuspend);
                    gvColumn[i].CellTemplate = template;
                    gvColumn[i].Header = StaticParam.m_UserListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_UserListHeadWidth[i];
                    btnDeleteSuspend.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGotoDelete));
                }
                else
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath(StaticParam.m_UserListHeadField[i]);
                    gvColumn[i].DisplayMemberBinding = binding;
                    gvColumn[i].Header = StaticParam.m_UserListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_UserListHeadWidth[i];
                }

                gridView.Columns.Add(gvColumn[i]);
            }

            LV_USERLIST.View = gridView;

        }
        //编辑用户列表中选中的记录
        private void OnGotoEdit(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(12), true))
            {
                TUser tUserSelected = (TUser)((Button)sender).DataContext;
                EditAccount(tUserSelected);
            }
        }
        //删除用户列表中选中的记录
        private void OnGotoDelete(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(13), true))
            {
                TUser tUserSelected = (TUser)((Button)sender).DataContext;
                DeleteAccount(tUserSelected);
            }
        }
        //添加新账户
        private void AddNewAccount()
        {
            TUser tUser = new TUser();
            tUser.LoginName = SMV_TB_LOGINNAME.Text.Trim();
            tUser.UserName = SMV_TB_USERNAME.Text.Trim();
            if (!SMV_PB_LOGINPWD.Text.Equals(SMV_PB_LOGINPWD_TWICE.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_INCONSISTENCY, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (!UIOperator.LoginNameRegex(SMV_TB_LOGINNAME.Text.Trim()))
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!UIOperator.LoginPwdRegex(SMV_PB_LOGINPWD.Text.ToString()))
            {
                MessageBox.Show(App.m_LangPackage.TIP_PASSWORD_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (SMV_DP_VALIDDATE.SelectedDate == null)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NOTSET_VALIDDATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DateTime dtSelectedDate = DateTime.Parse(SMV_DP_VALIDDATE.SelectedDate.ToString());
            DateTime dtCurrentTime = DateTime.Now;
            TimeSpan ts = dtCurrentTime.Subtract(dtSelectedDate);
            if (ts.Days > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_VALIDDATE_LESSTHAN_CURRENTDATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                tUser.LoginPwd = SMV_PB_LOGINPWD.Text;
                tUser.Mobile = SMV_TB_MOBILE.Text;
                tUser.Email = SMV_TB_EMAIL.Text;
                tUser.RoleID = int.Parse(SMV_COMBO_ROLE.SelectedValue.ToString());
                tUser.Status = SMV_COMBO_STATUS.Text.Trim();
                tUser.CreateTime = BaseUtils.GetCurrentDateTime();
                tUser.ValidDate = SMV_DP_VALIDDATE.SelectedDate.Value.ToString(String.Format("{0}", App.m_strDateFormat));
                tUser.LoginTimes = 0;
            }
            catch (Exception e2)
            {
                App.WriteSystemLog(e2.ToString());
                MessageBox.Show("必填项可能为空或异常，请检查！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataSet ds = new DataSet();
            //判断同名用户是否存在
            string strCmd = String.Format(sql.SQL.SQL_Q_FINDUSERBYKEY, tUser.LoginName); //LoginName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.SQL_Q_FINDUSERBYKEY);
            if (ds.Tables[sql.SQL.SQL_Q_FINDUSERBYKEY].Rows.Count > 0) //登录名存在 
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_EXSIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_ADD_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        strCmd = String.Format(sql.SQL.SQL_C_USER, tUser.LoginName, CommonLib.CommonLib.EcryptPassword(tUser.LoginPwd), tUser.UserName,
                            tUser.Mobile, tUser.Email, tUser.RoleID.ToString(), tUser.Status, tUser.CreateTime, tUser.ValidDate,
                            tUser.LastLoginTime, tUser.LoginTimes, 0, tUser.CreateTime);
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            //执行SQL
            if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_SAVE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information); ;
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(71), string.Format(App.GetBehaviorRemark(71), tUser.LoginName)));
                EmptyEditView();
                GetModel();
            }
            else
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_SAVE_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //编辑修改账户
        private void EditAccount(TUser tUser)
        {
            new modal.UserModal(tUser, GetModel).Show();
        }
        //删除账户
        private void DeleteAccount(TUser tUser)
        {
            MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_DEL_CONFIRM, App.m_LangPackage.WARNING, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (mBoxResult)
            {
                case MessageBoxResult.Yes:
                    {
                        if (App.m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_D_ACCOUNTBYID, tUser.ID.ToString())) > 0)
                        {
                            LV_USERLIST.Items.Remove(tUser);
                            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(83), string.Format(App.GetBehaviorRemark(83), tUser.LoginName)));
                        }
                        else
                            MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_DEL_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }
        //获取用户数据
        private void GetModel()
        {
            m_ds.Clear();
            InitializePagingCtrl();
            UIOperator.EmptyListView(LV_USERLIST);
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_USER), m_ds, sql.SQL.T_USER);
            m_nRecordCount = m_ds.Tables[sql.SQL.T_USER].Rows.Count;
            if (m_nRecordCount > 0)
            {
                m_nPageSize = int.Parse(SMV_PAGING.PageSize);
                m_nPageCount = m_nRecordCount / m_nPageSize + 1;
                if (m_nRecordCount % m_nPageSize == 0)
                    m_nPageCount = m_nRecordCount / m_nPageSize;
                m_nCurrentPage = 0;
                ModelToView(m_nRecordCount, m_nRecordCount / m_nPageSize + 1, m_nCurrentPage, m_ds, sql.SQL.T_USER);
            }
        }
        //初始化分页组件
        public void InitializePagingCtrl()
        {
            SMV_PAGING.PageSize = App.m_nPageSize.ToString();
            SMV_PAGING.RecordCount = "0";
            SMV_PAGING.PageCount = "0";
            SMV_PAGING.CurrentPage = "0";
        }
        //加载用户列表
        private void ModelToView(int RecordCount, int PageCount, int CurrentPage, DataSet ds, string srcTable)
        {
            if (RecordCount == 0) return;
            UIOperator.EmptyListView(LV_USERLIST);

            SMV_PAGING.RecordCount = RecordCount.ToString();
            SMV_PAGING.PageCount = PageCount.ToString();
            SMV_PAGING.CurrentPage = (CurrentPage + 1).ToString();

            for (int i = CurrentPage * m_nPageSize; i < (CurrentPage + 1) * m_nPageSize; i++)
            {
                if (i < ds.Tables[srcTable].Rows.Count)
                {
                    TUser tUser = new TUser();
                    Tools.PutVal<TUser>(tUser, ds.Tables[srcTable].Rows[i]);
                    LV_USERLIST.Items.Add(tUser);
                }
            }
        }
        //清除view中编辑组件的内容
        private void EmptyEditView()
        {
            SMV_TB_LOGINNAME.Text = "";
            SMV_PB_LOGINPWD.Text = "";
            SMV_PB_LOGINPWD_TWICE.Text = "";
            SMV_TB_USERNAME.Text = "";
            SMV_TB_MOBILE.Text = "";
            SMV_TB_EMAIL.Text = "";
            SMV_COMBO_ROLE.SelectedIndex = -1;
            SMV_COMBO_STATUS.SelectedIndex = -1;
            SMV_DP_VALIDDATE.SelectedDate = null;
        }
    }
}
