using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.pages.modal;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// DataSearchView.xaml 的交互逻辑
    /// </summary>
    public partial class DataSearchView : Page
    {
        private int m_nRecordCount;
        private int m_nPageCount;
        private int m_nCurrentPage;
        private int m_nPageSize;
        private DataSet m_ds;
       

        public DataSearchView()
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
            WLV_PAGING.PageSize = App.m_nPageSize.ToString();
           
            m_ds = new DataSet();

            InitialzeDataListView();
        }
        //初始化界面
        public void InitializeInterface()
        {
            DSV_GROUPBOX.Header = App.m_LangPackage.DSV_GROUPBOX;

            LB_DSV_DP_METHODDATE_SATRT.Content = App.m_LangPackage.LB_DSV_DP_METHODDATE_SATRT;
            LB_DSV_DP_METHODDATE_END.Content = App.m_LangPackage.LB_DSV_DP_METHODDATE_END;
            DSV_CKB_DATETIMEENABLE.Content = App.m_LangPackage.DSV_CKB_DATETIMEENABLE;
            LB_DSV_CKB_STATUS.Content = App.m_LangPackage.LB_DSV_CKB_STATUS;
            DSV_CKB_PAUSEEXP.Content = App.m_LangPackage.DSV_CKB_PAUSEEXP;
            DSV_CKB_WHOLEEXP.Content = App.m_LangPackage.DSV_CKB_WHOLEEXP;
            LB_DSV_METHODNAME.Content = App.m_LangPackage.LB_DSV_METHODNAME;
            LB_DSV_MANUFACTURER.Content = App.m_LangPackage.LB_DSV_MANUFACTURER;
            LB_DSV_TB_ACCOUNT.Content = App.m_LangPackage.LB_DSV_TB_ACCOUNT;
            LB_DSV_BATCHNO.Content = App.m_LangPackage.LB_DSV_BATCHNO;
            LB_DSV_TESTDEPARTMENT.Content = App.m_LangPackage.LB_DSV_TESTDEPARTMENT;
            DSV_BTN_SEARCH.Content = App.m_LangPackage.DSV_BTN_SEARCH;
            DSV_BTN_EMPTY.Content = App.m_LangPackage.DSV_BTN_EMPTY;

            if (App.g_EngVer) WLV_PAGING.Lang = "ENG";


        }
        //初始化权限控制
        public void InitializePrivelege()
        {
            if (!App.g_TSession.IsManager() && !App.g_TSession.IsRootManager())
            {
                DSV_TB_ACCOUNT.Text = App.g_TSession.TTUser.LoginName;
                DSV_TB_ACCOUNT.IsEnabled = false;
            }
        }
        public void InitialzeDataListView()
        {
            GridView gridView = new GridView();
            GridViewColumn[] gvColumn = new GridViewColumn[StaticParam.m_TestDataListHeadColumn.Length];

            for (int i = 0; i < gvColumn.GetLength(0); i++)
            {
                gvColumn[i] = new GridViewColumn();
                if (i == gvColumn.GetLength(0) - 1)  //删除列
                {
                    DataTemplate template = new DataTemplate();
                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(StackPanel));
                    template.VisualTree = factory;
                    FrameworkElementFactory btnViewDetail = new FrameworkElementFactory(typeof(Button));
                    btnViewDetail.SetValue(Button.ContentProperty, App.g_EngVer ? "View" : "查看");
                    btnViewDetail.SetValue(Button.WidthProperty, 70.0);
                    btnViewDetail.SetValue(Button.HeightProperty, 40.0);
                    btnViewDetail.SetValue(Button.StyleProperty, Application.Current.Resources["DynamicButton"]);
                    factory.AppendChild(btnViewDetail);
                    gvColumn[i].CellTemplate = template;
                    gvColumn[i].Header = StaticParam.m_TestDataListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_TestDataListHeadWidth[i];
                    btnViewDetail.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnGotoViewDetail));
                }
                else
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath(StaticParam.m_TestDataListHeadField[i]);
                    gvColumn[i].DisplayMemberBinding = binding;
                    gvColumn[i].Header = StaticParam.m_TestDataListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_TestDataListHeadWidth[i];
                }
                gridView.Columns.Add(gvColumn[i]);
            }

            LV_TESTDATE.View = gridView;

        }
        //删除用户列表中选中的记录
        private void OnGotoViewDetail(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(4), true))
            {
                TestData tTestData = (TestData)((Button)sender).DataContext;
                ExperimentDataModal experimentDataModal = new ExperimentDataModal(tTestData);
                experimentDataModal.ShowDialog();
            }
        }
        //加载工作日志列表
        private void GetModel(String condition)
        {
            m_ds.Clear();
            UIOperator.EmptyListView(LV_TESTDATE);

            WLV_PAGING.RecordCount = "0";
            WLV_PAGING.PageCount = "0";
            WLV_PAGING.CurrentPage = "0";

            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_TESTDATA, condition), m_ds, sql.SQL.T_TESTDATA);
            m_nRecordCount = m_ds.Tables[sql.SQL.T_TESTDATA].Rows.Count;
            if (m_nRecordCount > 0)
            {
                m_nPageSize = int.Parse(WLV_PAGING.PageSize);
                m_nPageCount = m_nRecordCount / m_nPageSize + 1;
                if (m_nRecordCount % m_nPageSize == 0)
                    m_nPageCount = m_nRecordCount / m_nPageSize;
                m_nCurrentPage = 0;
                ModelToView(m_nRecordCount, m_nRecordCount / m_nPageSize + 1, m_nCurrentPage, m_ds, sql.SQL.T_TESTDATA);
            }
        }
        //数据绑定视图
        private void ModelToView(int RecordCount, int PageCount, int CurrentPage, DataSet ds, string srcTable)
        {
            UIOperator.EmptyListView(LV_TESTDATE);

            WLV_PAGING.RecordCount = RecordCount.ToString();
            WLV_PAGING.PageCount = PageCount.ToString();
            WLV_PAGING.CurrentPage = (CurrentPage + 1).ToString();

            for (int i = CurrentPage * m_nPageSize; i < (CurrentPage + 1) * m_nPageSize; i++)
            {
                if (i < ds.Tables[srcTable].Rows.Count)
                {
                    TestData testData = new TestData();
                    Tools.PutVal<TestData>(testData, ds.Tables[sql.SQL.T_TESTDATA].Rows[i]);
                    for (int j = 0; j < testData.SampleTimes;j++)
                    {
                        SampleTime sampleTime = new SampleTime();
                        sampleTime.Key = j + 1;
                        sampleTime.TimeValue = int.Parse(ds.Tables[sql.SQL.T_TESTDATA].Rows[i][string.Format("SampleTime_{0}", j + 1)].ToString());
                        testData.Sample_Time_Arr[j] = sampleTime;
                    }
                    
                    LV_TESTDATE.Items.Add(testData);
                }
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(3), App.GetBehaviorRemark(3)));
        }
        //检索 按钮 事件
        private void DSV_BTN_SEARCH_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(4), true))
                DataSearch();
        }
      
        //清空 按钮 事件
        private void DSV_BTN_EMPTY_Click(object sender, RoutedEventArgs e)
        {
            //清除view中编辑组件的内容
            DSV_METHODNAME.Text = String.Empty;
            DSV_MANUFACTURER.Text = String.Empty;
            DSV_TB_ACCOUNT.Text = String.Empty;
            DSV_BATCHNO.Text = String.Empty;
            DSV_TESTDEPARTMENT.Text = String.Empty;
            DSV_CKB_PAUSEEXP.IsChecked = true;
            DSV_CKB_WHOLEEXP.IsChecked = true;
            UIOperator.EmptyListView(LV_TESTDATE);
        }
        //启用结束时间选择框
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            DSV_DP_METHODDATE_END.IsEnabled = DSV_CKB_DATETIMEENABLE.IsChecked == true ? true : false;
        }

        //上一页 按钮 事件
        private void PageUp_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage > 0)
            {
                m_nCurrentPage--;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_TESTDATA);
            }
        }
        //下一页 按钮 事件
        private void PageDown_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage < m_nPageCount - 1)
            {
                m_nCurrentPage++;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_TESTDATA);
            }
        }
        //回到首页 按钮 事件
        private void PageGoHead_Click(object sender, RoutedEventArgs e)
        {
            m_nCurrentPage = 0;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_TESTDATA);
        }
        //回到尾页 按钮 事件
        private void PageGoTail_Click(object sender, RoutedEventArgs e)
        {
            m_nCurrentPage = m_nPageCount - 1;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_TESTDATA);
        }
        //改变页大小 下拉框 事件
        private void PageSize_Change(object sender, RoutedEventArgs e)
        {
            m_nPageSize = int.Parse(WLV_PAGING.PageSize);
            m_nPageCount = m_nRecordCount / m_nPageSize + 1;
            if (m_nRecordCount % m_nPageSize == 0)
                m_nPageCount = m_nRecordCount / m_nPageSize;
            m_nCurrentPage = 0;
            ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_TESTDATA);
        }
        private void DataSearch()
        {
            ////定制条件检索
            string strStartTime = UIOperator.ToDateTime(DSV_DP_METHODDATE_SATRT, App.m_strDateFormat, App.m_strTimeFormat);
            string strEndTime = Convert.ToDateTime(strStartTime).ToString("yyyy-MM-dd 23:59:59");
            string account_condition = "";
            string methodname_condition = "";
            string manufacturer_condition = "";
            string batchno_condition = "";
            string testdepartment_condition = "";
            string status_condition = "";
            string account_condition_add = String.Empty;

            if (DSV_CKB_DATETIMEENABLE.IsChecked == true)
            {
                strEndTime = UIOperator.ToDateTime(DSV_DP_METHODDATE_END, App.m_strDateFormat, App.m_strTimeFormat);
            }
            string datetime_condition = String.Format("datetime(method_time) between datetime('{0}') and datetime('{1}')", strStartTime, strEndTime);
            if (!String.IsNullOrEmpty(DSV_METHODNAME.Text))
            {
                methodname_condition = String.Format(" AND method_name='{0}'", DSV_METHODNAME.Text.Trim());
            }
            if (!String.IsNullOrEmpty(DSV_MANUFACTURER.Text))
            {
                manufacturer_condition = String.Format(" AND manufacturer='{0}'", DSV_MANUFACTURER.Text.Trim());
            }
            if (!String.IsNullOrEmpty(DSV_BATCHNO.Text))
            {
                batchno_condition = String.Format(" AND batch_no='{0}'", DSV_BATCHNO.Text.Trim());
            }
            if (!String.IsNullOrEmpty(DSV_TESTDEPARTMENT.Text))
            {
                testdepartment_condition = String.Format(" AND test_department='{0}'", DSV_TESTDEPARTMENT.Text.Trim());
            }
            if (!String.IsNullOrEmpty(DSV_TB_ACCOUNT.Text))
            {
                account_condition = String.Format(" AND login_name='{0}'", DSV_TB_ACCOUNT.Text.Trim());
            }
            if (!String.IsNullOrEmpty(DSV_TB_ACCOUNT.Text) && !BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(22), false))
            {
                account_condition = String.Format(" AND login_name='{0}'", DSV_TB_ACCOUNT.Text.Trim());
            }
            if (!App.g_TSession.IsRootManager())
            {
                account_condition_add = " AND login_name!='" + StaticParam.ROOT_LOGINNAME + "' AND login_name!='root'";

            }

            if (DSV_CKB_PAUSEEXP.IsChecked == true && DSV_CKB_WHOLEEXP.IsChecked == false)
                status_condition = String.Format(" AND status='{0}'", DSV_CKB_PAUSEEXP.Content);
            if (DSV_CKB_PAUSEEXP.IsChecked == false && DSV_CKB_WHOLEEXP.IsChecked == true)
                status_condition = String.Format(" AND status='{0}'", DSV_CKB_WHOLEEXP.Content);
            if (DSV_CKB_PAUSEEXP.IsChecked == false && DSV_CKB_WHOLEEXP.IsChecked == false)
                status_condition = String.Format(" AND status='{0}'", "全不选");
            string strSQLCondition = datetime_condition
                + methodname_condition
                + manufacturer_condition
                + batchno_condition
                + testdepartment_condition
                + account_condition
                + status_condition
                + account_condition_add;
            GetModel(strSQLCondition);
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(28),App.GetBehaviorRemark(28)));
        }
        //查看实验数据
        public void ViewTestData()
        {
            int index = LV_TESTDATE.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show(App.m_LangPackage.TIP_SELECT_FILE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            TestData testData = LV_TESTDATE.SelectedItem as TestData;
            DateTime dt = DateTime.Parse(testData.StartTime);
            string filepath = string.Format(App.g_AppDirectory + "report\\" + "report_{0}.pdf", BaseUtils.GetFileCurrentDateTime(dt));
            if (System.IO.File.Exists(filepath))
            {
                ReportModal reportModal = new ReportModal(filepath);
                reportModal.ShowDialog();
            }
            else
                MessageBox.Show(App.m_LangPackage.TIP_FILE_NOTEXSIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
        }



    }
}
