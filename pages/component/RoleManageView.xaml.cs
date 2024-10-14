using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Pharmacy.INST.CommonLib;
using Pharmacy.INST.DissolutionClient.common;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.component
{
    /// <summary>
    /// RoleManageView.xaml 的交互逻辑
    /// </summary>
    public partial class RoleManageView : Page
    {
        public RoleManageView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
            InitialzeModuleCheckBoxList();
            InitialzeFunctionCheckBoxList();
        }
        //窗口组件初始化
        private void InitializeComponentEx()
        {
            //加载角色方法下拉框
            BindRoleToView();
        }
        public void InitializeInterface()
        {
            RMV_ROLELIST_GROUPBOX.Header = App.m_LangPackage.RMV_ROLELIST_GROUPBOX;
            SMV_BTN_SAVEPRIVELEGE.Content = App.m_LangPackage.SMV_BTN_SAVEPRIVELEGE;
            SMV_BTN_RETRIVERPRIVELEGE.Content = App.m_LangPackage.SMV_BTN_RETRIVERPRIVELEGE;
            SMV_BTN_ROLE_ADD.Content = App.m_LangPackage.SMV_BTN_ROLE_ADD;
            SMV_BTN_ROLE_DELETE.Content = App.m_LangPackage.SMV_BTN_ROLE_DELETE;
            RMV_MODULELIST_GROUPBOX.Header = App.m_LangPackage.RMV_MODULELIST_GROUPBOX;
            RMV_FUNCTION_GROUPBOX.Header = App.m_LangPackage.RMV_FUNCTION_GROUPBOX;
            SMV_CHK_MODULE_ALL.Content = App.m_LangPackage.SMV_CHK_MODULE_ALL;
            SMV_CHK_FUNCTION_ALL.Content = App.m_LangPackage.SMV_CHK_FUNCTION_ALL;
        }
        //初始化模块列表
        private void InitialzeModuleCheckBoxList()
        {
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(sql.SQL.SQL_R_MODULE, ds, sql.SQL.T_MODULE);
            int nCount = ds.Tables[sql.SQL.T_MODULE].Rows.Count;
            for (int i = 0; i < nCount / 2 + 1; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i * 2 + j < nCount)
                    {
                        CheckBox ck = new CheckBox();
                        ck.Content = ds.Tables[sql.SQL.T_MODULE].Rows[i * 2 + j]["name"].ToString();
                        ck.Name = String.Format("SMV_CHK_MOD_{0}", i * 2 + j);
                        ck.SetValue(Canvas.LeftProperty, (double)(j * 190));
                        ck.SetValue(Canvas.TopProperty, (double)(50 + i * 35));
                        ck.Width = 150;
                        ck.Height = 40;
                        ck.FontSize = 16;
                        ModuleListGroupView.Children.Add(ck);
                    }
                }
            }
        }
        //初始化权限功能列表
        private void InitialzeFunctionCheckBoxList()
        {
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(sql.SQL.SQL_R_FUNCTiON, ds, sql.SQL.T_FUNCTION);
            int nCount = ds.Tables[sql.SQL.T_FUNCTION].Rows.Count;
            for (int i = 0; i < nCount / 2 + 1; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i * 2 + j < nCount)
                    {
                        CheckBox ck = new CheckBox();
                        ck.Content = ds.Tables[sql.SQL.T_FUNCTION].Rows[i * 2 + j]["name"].ToString();
                        ck.Name = String.Format("SMV_CHK_FUN_{0}", i * 2 + j);
                        ck.SetValue(Canvas.LeftProperty, (double)(j * 190));
                        ck.SetValue(Canvas.TopProperty, (double)(50 + i * 35));
                        ck.Width = 160;
                        ck.Height = 40;
                        ck.FontSize = 16;
                        FunctionListGroupView.Children.Add(ck);
                    }
                }
            }
        }
        //撤消设置
        private void SMV_RETRIVERPRIVELEGE_Click(object sender, RoutedEventArgs e)
        {
            int index = SMV_LB_ROLE_MAMAGE.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show(App.m_LangPackage.TIP_SELECT_ROLE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }

            DataSet ds = new DataSet();
            int role_id = int.Parse(SMV_LB_ROLE_MAMAGE.SelectedValue.ToString());
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_PRIVELEGE, role_id), ds, sql.SQL.T_PRIVELEGE);

            foreach (Control c in FunctionListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                ck.IsChecked = false;
            }
            foreach (Control c in ModuleListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                ck.IsChecked = false;
            }

            foreach (Control c in ModuleListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                string privelege_name = ck.Content.ToString();
                for (int i = 0; i < ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count; i++)
                {
                    if (privelege_name.Equals(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString()))
                    {
                        ck.IsChecked = true;
                        break;
                    }
                }
            }


            foreach (Control c in FunctionListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                string privelege_name = ck.Content.ToString();
                for (int i = 0; i < ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count; i++)
                {
                    if (privelege_name.Equals(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString()))
                    {
                        ck.IsChecked = true;
                        break;
                    }
                }
            }
        }
        //添加角色 按钮事件 
        private void SMV_BTN_ROLE_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(14), true))
                AddNewRole();
        }
        //删除角色 按钮事件
        private void SMV_BTN_ROLE_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(15), true))
                DeleteRole();
        }
        //功能列表选择框全选 Checkbox事件
        private void SMV_CHK_FUNCTION_ALL_Click(object sender, RoutedEventArgs e)
        {
            if (SMV_CHK_FUNCTION_ALL.IsChecked == true)
            {
                foreach (Control c in FunctionListGroupView.Children)
                {

                    CheckBox ck = c as CheckBox;
                    ck.IsChecked = true;
                }
            }
            else
            {
                foreach (Control c in FunctionListGroupView.Children)
                {
                    CheckBox ck = c as CheckBox;
                    ck.IsChecked = false;
                }
            }
        }
        private void SMV_CHK_MODULE_ALL_Click(object sender, RoutedEventArgs e)
        {
            if (SMV_CHK_MODULE_ALL.IsChecked == true)
            {
                foreach (Control c in ModuleListGroupView.Children)
                {
                    CheckBox ck = c as CheckBox;
                    ck.IsChecked = true;
                }
            }
            else
            {
                foreach (Control c in ModuleListGroupView.Children)
                {
                    CheckBox ck = c as CheckBox;
                    ck.IsChecked = false;
                }
            }
        }
        //角色下拉框变化响应事件
        private void SMV_LB_ROLE_MAMAGE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = SMV_LB_ROLE_MAMAGE.SelectedIndex;
            if (index == -1)
            {
                return;
            }

            DataSet ds = new DataSet();
            int role_id = int.Parse(SMV_LB_ROLE_MAMAGE.SelectedValue.ToString());
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_PRIVELEGE, role_id), ds, sql.SQL.T_PRIVELEGE);

            foreach (Control c in FunctionListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                ck.IsChecked = false;
            }
            foreach (Control c in ModuleListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                ck.IsChecked = false;
            }

            foreach (Control c in ModuleListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                string privelege_name = ck.Content.ToString();
                for (int i = 0; i < ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count; i++)
                {
                    if (privelege_name.Equals(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString()))
                    {
                        ck.IsChecked = true;
                        break;
                    }
                }
            }


            foreach (Control c in FunctionListGroupView.Children)
            {
                CheckBox ck = c as CheckBox;
                string privelege_name = ck.Content.ToString();
                for (int i = 0; i < ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count; i++)
                {
                    if (privelege_name.Equals(ds.Tables[sql.SQL.T_PRIVELEGE].Rows[i]["PrivelegeName"].ToString()))
                    {
                        ck.IsChecked = true;
                        break;
                    }
                }
            }
        }
        //保存或更新功能权限
        private void SMV_SAVEPRIVELEGE_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(16), true))
                SavePrivelege();
        }
        //修改保存权限
        private void SavePrivelege()
        {
            int index = SMV_LB_ROLE_MAMAGE.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show(App.m_LangPackage.TIP_SELECT_ROLE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Question);
                return;
            }
            MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_ROLE_REVIEW_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (mBoxResult)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        DataRowView drv = SMV_LB_ROLE_MAMAGE.SelectedItem as DataRowView;
                        string strRoleName = drv["name"].ToString();
                        if (strRoleName.Equals("系统管理员") || strRoleName.Equals("System Administrator"))
                        {
                            MessageBox.Show(string.Format(App.m_LangPackage.TIP_ROLE_NO_REVIEW, strRoleName) , App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        SaveRolePrivelege(int.Parse(SMV_LB_ROLE_MAMAGE.SelectedValue.ToString()), strRoleName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }
        //添加角色
        private void AddNewRole()
        {
            string strRoleName = SMV_TB_ROLE.Text.Trim();
            if (String.IsNullOrEmpty(strRoleName))
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_INPUT, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataSet ds = new DataSet();
            //判断同名方法是否存在
            string strCmd = String.Format(sql.SQL.SQL_R_FINDROLEBYNAME, strRoleName); //strMethodName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_ROLE);
            if (ds.Tables[sql.SQL.T_ROLE].Rows.Count > 0) //登录名存在 
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_EXSITROLE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_ROLE_ADD_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        strCmd = String.Format(sql.SQL.SQL_C_ROLE, strRoleName, BaseUtils.GetCurrentDateTime(), "");
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
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_SAVE_RESTART, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(73), string.Format(App.GetBehaviorRemark(73), strRoleName)));
                SMV_TB_ROLE.Text = "";
                BindRoleToView();
            }
            else
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_SAVE_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //删除已有角色
        private void DeleteRole()
        {
            int index = SMV_LB_ROLE_MAMAGE.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show(App.m_LangPackage.TIP_SELECT_ROLE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int nRoleID = int.Parse(SMV_LB_ROLE_MAMAGE.SelectedValue.ToString());
            DataRowView drv = SMV_LB_ROLE_MAMAGE.SelectedItem as DataRowView;
            string strRoleName = drv["name"].ToString();
            string strCmd = string.Empty;
            strCmd = string.Format(sql.SQL.SQL_R_USERBYROLEID, nRoleID);
            DataSet ds = new();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_USER);
            if (ds.Tables[sql.SQL.T_USER].Rows.Count > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_EXSITUSER, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_ROLE_DEL_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        strCmd = String.Format(sql.SQL.SQL_D_ROLE, nRoleID);
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
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_DEL_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(74), string.Format(App.GetBehaviorRemark(74), strRoleName)));
                BindRoleToView();
            }
            else
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_DEL_FAILURE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool SaveRolePrivelege(int role_id, string rolename)
        {
            try
            {
                DataSet ds = new DataSet();
                App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_PRIVELEGE, role_id), ds, sql.SQL.T_PRIVELEGE);
                if (ds.Tables[sql.SQL.T_PRIVELEGE].Rows.Count > 0)  //存在该角色，保存
                {
                    App.m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_D_PRIVELEGE, role_id));
                }
                foreach (Control c in ModuleListGroupView.Children)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.IsChecked == true)
                    {
                        string privelege_name = ck.Content.ToString();
                        App.m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_C_PRIVELEGE, role_id, privelege_name, 0));
                    }
                }
                foreach (Control c in FunctionListGroupView.Children)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.IsChecked == true)
                    {
                        string privelege_name = ck.Content.ToString();
                        App.m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_C_PRIVELEGE, role_id, privelege_name, 1));
                    }
                }
                MessageBox.Show(App.m_LangPackage.TIP_ROLE_SAVE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(75), string.Format(App.GetBehaviorRemark(75), rolename)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
        ////加载角色列表
        private void BindRoleToView()
        {
            SMV_LB_ROLE_MAMAGE.ItemsSource = null;
            //加载角色方法下拉框
            string strCmd = String.Format(sql.SQL.SQL_Q_ROLE); //strMethodName 参数
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_ROLE);
            if (ds.Tables[sql.SQL.T_ROLE].Rows.Count > 0) //
            {
                UIOperator.ListBoxBinder(SMV_LB_ROLE_MAMAGE, ds.Tables[sql.SQL.T_ROLE], "name", "id");
            }
        }
    }
}
