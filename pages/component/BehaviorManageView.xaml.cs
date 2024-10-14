using System;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.component
{
    /// <summary>
    /// BehaviorManageView.xaml 的交互逻辑
    /// </summary>
    public partial class BehaviorManageView : Page
    {
        public BehaviorManageView()
        {
            InitializeComponent();
            InitializeComponentEx();
            BindLogTypeToView();
            BindBehaviorToView();
        }
        //窗口组件初始化
        private void InitializeComponentEx()
        {

            UIOperator.LoadImage(App.g_ResourceDir + "img_assosiation.png", SMV_IMG_ASSOSIATION);



        }
        //加载日志类型列表
        private void BindLogTypeToView()
        {
            //加载日志类型列表框
            string strCmd = String.Format(sql.SQL.SQL_R_LOGTYPE); //strMethodName 参数
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_LOGTYPE);
            if (ds.Tables[sql.SQL.T_LOGTYPE].Rows.Count > 0) //
                UIOperator.ListBoxBinder(SMV_LB_LOGTYPE, ds.Tables[sql.SQL.T_LOGTYPE], "LogTypeName", "ID");
        }
        //加载行为类型列表
        private void BindBehaviorToView()
        {
            int index = SMV_LB_LOGTYPE.SelectedIndex;
            if (index == -1) return;
            int nLogID = int.Parse(SMV_LB_LOGTYPE.SelectedValue.ToString());
            SMV_LB_BEHAVIOR.ItemsSource = null;
            //加载行为类型列表框
            string strCmd = String.Format(sql.SQL.SQL_R_BEHAVIORBYLOGID, nLogID); //strMethodName 参数
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_BEHAVIOR);
            if (ds.Tables[sql.SQL.T_BEHAVIOR].Rows.Count > 0) //
                UIOperator.ListBoxBinder(SMV_LB_BEHAVIOR, ds.Tables[sql.SQL.T_BEHAVIOR], "BehaviorName", "ID");
        }
        //添加日志类型
        private void AddNewLogType()
        {
            string strLogType = SMV_TB_LOGTYPE.Text.Trim();
            if (String.IsNullOrEmpty(strLogType))
            {
                MessageBox.Show("类型输入为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataSet ds = new DataSet();
            //判断同名方法是否存在
            string strCmd = String.Format(sql.SQL.SQL_R_LOGTYPEBYNAME, strLogType); //strMethodName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_LOGTYPE);
            if (ds.Tables[sql.SQL.T_LOGTYPE].Rows.Count > 0) //登录名存在 
            {
                MessageBox.Show("该日志类型名已存在，请更改后重试！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MessageBoxResult mBoxResult = MessageBox.Show("确定添加该日志类型吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        strCmd = String.Format(sql.SQL.SQL_C_LOGTYPE, strLogType, "");
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
                MessageBox.Show("保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                SMV_TB_LOGTYPE.Text = "";
                BindLogTypeToView();
            }
            else
            {
                MessageBox.Show("保存失败，请检查！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //删除日志类型
        private void DeleteLogType()
        {

        }
        //添加行为
        private void AddNewBehavior()
        {
            int index = SMV_LB_LOGTYPE.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("请选择日志类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int nLogType = int.Parse(SMV_LB_LOGTYPE.SelectedValue.ToString());
            string strBehavior = SMV_TB_BEHAVIOR.Text.Trim();
            if (String.IsNullOrEmpty(strBehavior))
            {
                MessageBox.Show("行为输入为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataSet ds = new DataSet();
            //判断是否同名
            string strCmd = String.Format(sql.SQL.SQL_R_BEHAVIORBYNAME, strBehavior); //strMethodName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_BEHAVIOR);
            if (ds.Tables[sql.SQL.T_BEHAVIOR].Rows.Count > 0) //登录名存在 
            {
                MessageBox.Show("该行为名已存在，请更改后重试！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                MessageBoxResult mBoxResult = MessageBox.Show("确定添加该行为名吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        strCmd = String.Format(sql.SQL.SQL_C_BEHAVIOR, strBehavior, nLogType, "");
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
                MessageBox.Show("保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                //App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(78), App.GetBehaviorRemark(78) + string.Format("成功新建行为{0}", strBehavior)));
                SMV_TB_BEHAVIOR.Text = "";
                BindBehaviorToView();
            }
            else
            {
                MessageBox.Show("保存失败，请检查！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //删除行为
        private void DeleteBehavior()
        {

        }
        //添加 日志类型 按钮 事件
        private void SMV_BTN_LOGTYPE_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(20), true))
                AddNewLogType();
        }
        //删除 日志类型 按钮事件
        private void SMV_BTN_LOGTYPE_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(20), true))
                DeleteLogType();
        }
        //日志类型选择改选 事件
        private void SMV_LB_LOGTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SMV_LB_BEHAVIOR.ItemsSource = null;

            int nLogType = int.Parse(SMV_LB_LOGTYPE.SelectedValue.ToString());
            //加载行为类型列表框
            string strCmd = String.Format(sql.SQL.SQL_R_BEHAVIORBYLOGID, nLogType); //strMethodName 参数
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_BEHAVIOR);
            if (ds.Tables[sql.SQL.T_BEHAVIOR].Rows.Count > 0) //
            {
                UIOperator.ListBoxBinder(SMV_LB_BEHAVIOR, ds.Tables[sql.SQL.T_BEHAVIOR], "BehaviorName", "ID");
            }
        }
        //添加行为 按钮 事件
        private void SMV_BTN_BEHAVIOR_ADD_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(20), true))
                AddNewBehavior();
        }
        private void SMV_BTN_BEHAVIOR_DELETE_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(20), true))
                DeleteBehavior();
        }
    }

}
