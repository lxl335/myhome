using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// WorkLogView.xaml 的交互逻辑
    /// </summary>
    public partial class WorkLogView : Page
    {
        //////分页变量
        private int m_nRecordCount;
        private int m_nPageCount;
        private int m_nCurrentPage;
        private int m_nPageSize;

        private DataSet m_ds = new DataSet();  //数据集用于检索数据
        public WorkLogView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
            //初始化权限控制
            InitializePrivelege(); 
        }
        //初始化组件
        public void InitializeComponentEx()
        {
            //日志类型下拉框初始化
            InitializeLogTypeComboBox();
            //分页组件初始化
            InitializePagingCtrl();      

        }
        //初始化界面
        public void InitializeInterface()
        {
            WLV_GROUPBOX.Header = App.m_LangPackage.WLV_GROUPBOX;

            LB_WLV_DP_METHODDATE_SATRT.Content = App.m_LangPackage.LB_WLV_DP_METHODDATE_SATRT;
            LB_WLV_DP_METHODDATE_END.Content = App.m_LangPackage.LB_WLV_DP_METHODDATE_END;
            WLV_CKB_DATETIMEENABLE.Content = App.m_LangPackage.WLV_CKB_DATETIMEENABLE;
            LB_WLV_TB_ACCOUNT.Content = App.m_LangPackage.LB_WLV_TB_ACCOUNT;
            LB_WLV_COMBO_LOGTYPE.Content = App.m_LangPackage.LB_WLV_COMBO_LOGTYPE;
            LB_WLV_COMBO_BEHAVIOR.Content = App.m_LangPackage.LB_WLV_COMBO_BEHAVIOR;
            WLV_BTN_SEARCH.Content = App.m_LangPackage.WLV_BTN_SEARCH;
            WLV_BTN_EMPTY.Content = App.m_LangPackage.WLV_BTN_EMPTY;
            WLV_BTN_EXPORT.Content = App.m_LangPackage.WLV_BTN_EXPORT;

            LV_WORKLOG_COL_ID.Header = App.m_LangPackage.LV_WORKLOG_COL_ID;
            LV_WORKLOG_COL_USER.Header = App.m_LangPackage.LV_WORKLOG_COL_USER;
            LV_WORKLOG_COL_LOGTYPE.Header = App.m_LangPackage.LV_WORKLOG_COL_LOGTYPE;
            LV_WORKLOG_COL_BEHAVIOR.Header = App.m_LangPackage.LV_WORKLOG_COL_BEHAVIOR;
            LV_WORKLOG_COL_TIME.Header = App.m_LangPackage.LV_WORKLOG_COL_TIME;
            LV_WORKLOG_COL_NOTE.Header = App.m_LangPackage.LV_WORKLOG_COL_NOTE;
            if (App.g_EngVer) WLV_PAGING.Lang = "ENG";
        }
        public void InitializePrivelege()
        {
            if (!App.g_TSession.IsManager() && !App.g_TSession.IsRootManager()) 
            {
                WLV_TB_ACCOUNT.Text = App.g_TSession.TTUser.LoginName;
                WLV_TB_ACCOUNT.IsEnabled = false;
            }
        }
        #region 初始化或数据绑定
        //初始化日志类型下拉框
        public void InitializeLogTypeComboBox()
        {
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_LOGTYPE), ds, sql.SQL.T_LOGTYPE);
            UIOperator.ComboBoxBinder(WLV_COMBO_LOGTYPE, ds.Tables[sql.SQL.T_LOGTYPE], "LogTypeName", "ID");
        }
        //分页组件初始化
        public void InitializePagingCtrl()
        {
            WLV_PAGING.PageSize = App.m_nPageSize.ToString();
            WLV_PAGING.RecordCount = "0";
            WLV_PAGING.PageCount = "0";
            WLV_PAGING.CurrentPage = "0";
        }
        //根据日志类型绑定行业下拉框列表
        public void BindBehaviorComboBox(int logtype)
        {
            if (logtype != -1)
            {
                WLV_COMBO_BEHAVIOR.ItemsSource = null;
                DataSet ds = new DataSet();
                App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_BEHAVIORBYLOGID,logtype), ds, sql.SQL.T_BEHAVIOR);
                UIOperator.ComboBoxBinder(WLV_COMBO_BEHAVIOR, ds.Tables[sql.SQL.T_BEHAVIOR], "BehaviorName", "ID");
            }
        }
        //根据检索条件加载工作日志到列表
        private void GetModel(String condition)
        {
            m_ds.Clear();
            InitializePagingCtrl();
            UIOperator.EmptyListView(LV_WORKLOG);
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_WORKLOG, condition), m_ds, sql.SQL.T_WORKLOG);
            m_nRecordCount = m_ds.Tables[sql.SQL.T_WORKLOG].Rows.Count;
            if (m_nRecordCount > 0)
            {
                //洗数据
                for (int i = 0; i < m_nRecordCount; i++)
                    m_ds.Tables[sql.SQL.T_WORKLOG].Rows[i][0] = i+1;
                m_nPageSize = int.Parse(WLV_PAGING.PageSize);
                m_nPageCount = m_nRecordCount / m_nPageSize + 1;
                if (m_nRecordCount % m_nPageSize == 0)
                    m_nPageCount = m_nRecordCount / m_nPageSize;
                m_nCurrentPage = 0;
                ModelToView(m_nRecordCount, m_nRecordCount / m_nPageSize + 1, m_nCurrentPage, m_ds, sql.SQL.T_WORKLOG);
            }
            else
            {
                InitializePagingCtrl();
            }
        }
        //根据分页信息显示列表
        private void ModelToView(int RecordCount,int PageCount,int CurrentPage,DataSet ds,string srcTable)
        {
            if (RecordCount == 0) return;
            UIOperator.EmptyListView(LV_WORKLOG);

            WLV_PAGING.RecordCount = RecordCount.ToString();
            WLV_PAGING.PageCount = PageCount.ToString();
            WLV_PAGING.CurrentPage = (CurrentPage + 1).ToString();

            for (int i = CurrentPage * m_nPageSize; i < (CurrentPage +1) * m_nPageSize; i++)
            {
                if (i < ds.Tables[srcTable].Rows.Count)
                {
                    WorkLog workLog = new WorkLog();
                    Tools.PutVal<WorkLog>(workLog, ds.Tables[sql.SQL.T_WORKLOG].Rows[i]);
                    LV_WORKLOG.Items.Add(workLog);
                }
            }
        }
        #endregion
        #region 窗口事件
        //窗口加载时执行
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(4), App.GetBehaviorRemark(4)));
        }
        //日志类型 下拉框 选择 事件
        private void WLV_COMBO_LOGTYPE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = WLV_COMBO_LOGTYPE.SelectedIndex;
            if (index == -1)
            {
                
                return;
            }
            BindBehaviorComboBox(int.Parse(WLV_COMBO_LOGTYPE.SelectedValue.ToString()));
        }
        //检索 按钮 事件
        private void WLV_BTN_SEARCH_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(7), true))
                LogSearch();
        }
        //清空 按钮 事件
        private void WLV_BTN_EMPTY_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(7), true))
            {
                //清除view中编辑组件的内容
                WLV_TB_ACCOUNT.Text = String.Empty;
                WLV_CKB_DATETIMEENABLE.IsChecked = false;
                WLV_DP_METHODDATE_END.IsEnabled = false;
                WLV_COMBO_LOGTYPE.Text = null;
                WLV_COMBO_BEHAVIOR.ItemsSource = null;
                UIOperator.EmptyListView(LV_WORKLOG);
            }
        }
        //导出 按钮 事件
        private void WLV_BTN_EXPORT_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(7), true))
            {
                if (m_ds == null || m_ds.Tables.Count == 0 || m_ds.Tables[sql.SQL.T_WORKLOG].Rows.Count == 0)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_NODATA_EXPORT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                DataTable dt = m_ds.Tables[sql.SQL.T_WORKLOG].Copy();
                dt.Columns.Remove("ID");
                GridView gridView = (GridView)LV_WORKLOG.View;
                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    dt.Columns[i].Caption = gridView.Columns[i].Header.ToString();
                }

                string title = App.m_LangPackage.TIP_EXPORTFILE_TITLE;
                string subtitle = String.Empty;
                string reportdatetime = UIOperator.ToDateTime(WLV_DP_METHODDATE_SATRT, App.m_strDateFormat, App.m_strTimeFormat)
                    + "-" + UIOperator.ToDateTime(WLV_DP_METHODDATE_END, App.m_strDateFormat, App.m_strTimeFormat);
                string tailer = "";
                string strExportFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                    + "\\" + title + "_" + BaseUtils.GetFileCurrentDateTime() + ".html";

                if (UIOperator.ExportHtmlReport(strExportFile, title, subtitle, reportdatetime, tailer, dt))
                    MessageBox.Show(App.m_LangPackage.TIP_EXPORT_LOG_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show(App.m_LangPackage.TIP_EXPORT_LOG_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //启用结束时间选择框
        private void WLV_CKB_DATETIMEENABLE_Click(object sender, RoutedEventArgs e)
        {
            WLV_DP_METHODDATE_END.IsEnabled = WLV_CKB_DATETIMEENABLE.IsChecked == true ? true : false;
        }
        //上一页 按钮 事件
        private void PageUp_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage > 0)
            {
                m_nCurrentPage--;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_WORKLOG);
            }
        }
        //下一页 按钮 事件
        private void PageDown_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage < m_nPageCount - 1)
            {
                m_nCurrentPage++;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_WORKLOG);
            }
        }
        //回到首页 按钮 事件
        private void PageGoHead_Click(object sender, RoutedEventArgs e)
        {
            m_nCurrentPage = 0;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_WORKLOG);
        }
        //回到尾页 按钮 事件
        private void PageGoTail_Click(object sender, RoutedEventArgs e)
        {
            m_nCurrentPage = m_nPageCount - 1;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_WORKLOG);
        }
        //改变页大小 下拉框 事件
        private void PageSize_Change(object sender, RoutedEventArgs e)
        {
            m_nPageSize = int.Parse(WLV_PAGING.PageSize);
            m_nPageCount = m_nRecordCount / m_nPageSize + 1;
            if (m_nRecordCount % m_nPageSize == 0)
                m_nPageCount = m_nRecordCount / m_nPageSize;
            m_nCurrentPage = 0;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_WORKLOG);
        }
        #endregion

        private void LogSearch()
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(29),App.GetBehaviorRemark(29)));
            ////定制条件检索
            string strStartTime;
            string strEndTime;
            string account_condition = String.Empty;
            string logtype_condition = String.Empty;
            string behavior_condition = String.Empty;
            string account_condition_add = String.Empty;

            strStartTime = UIOperator.ToDateTime(WLV_DP_METHODDATE_SATRT, App.m_strDateFormat, App.m_strTimeFormat);
            strEndTime = Convert.ToDateTime(strStartTime).ToString("yyyy-MM-dd 23:59:59");
            if (WLV_CKB_DATETIMEENABLE.IsChecked == true)
            {
                strEndTime = UIOperator.ToDateTime(WLV_DP_METHODDATE_END, App.m_strDateFormat, App.m_strTimeFormat);
            }
            string datetime_condition = String.Format("datetime(actiontime) between datetime('{0}') and datetime('{1}')", strStartTime, strEndTime);
            if (!String.IsNullOrEmpty(WLV_TB_ACCOUNT.Text) && !BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(22), false))
            {
                account_condition = String.Format(" AND account='{0}'", WLV_TB_ACCOUNT.Text.Trim());
            }
            if (!String.IsNullOrEmpty(WLV_COMBO_LOGTYPE.Text))
            {
                logtype_condition = String.Format(" AND type='{0}'", WLV_COMBO_LOGTYPE.Text);
            }
            if (!String.IsNullOrEmpty(WLV_COMBO_BEHAVIOR.Text))
            {
                behavior_condition = String.Format(" AND behavior='{0}'", WLV_COMBO_BEHAVIOR.Text);
            }
            if (!App.g_TSession.IsRootManager())
            {
                if (App.g_TSession.IsManager())
                    account_condition_add = " AND account!='" + StaticParam.ROOT_LOGINNAME + "' AND account!='root'";
                else
                    account_condition_add = " AND account NOT in ('" + StaticParam.ROOT_LOGINNAME + "','root','" + StaticParam.SYSMANAGE_LOGINNAME + "')";
            }
            string strSQLCondition = String.Format("{0}{1}{2}{3}{4}", datetime_condition, account_condition, logtype_condition, behavior_condition, account_condition_add);
            GetModel(strSQLCondition);
        }

    }
}