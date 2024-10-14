using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using Pharmacy.INST.DissolutionClient.entities;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;
using Pharmacy.INST.CommonLib;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// InstrumentVerifyView.xaml 的交互逻辑
    /// </summary>
    public partial class InstrumentVerifyView : Page
    {
        private DataSet m_ds;
        private int m_nRecordCount;
        private int m_nPageCount;
        private int m_nCurrentPage;
        private int m_nPageSize;
        public InstrumentVerifyView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        public void InitializeComponentEx()
        {
            WLV_PAGING.PageSize = App.m_nPageSize.ToString();
            m_ds = new DataSet();
            InitialzeDataListView();
        }
        //初始化界面
        public void InitializeInterface()
        {
            IVV_GROUPBOX.Header = App.m_LangPackage.IVV_GROUPBOX;

            LB_IVV_RB_VERIFY.Content = App.m_LangPackage.LB_IVV_RB_VERIFY;
            LB_IVV_DP_VERIFYDATE.Content = App.m_LangPackage.LB_IVV_DP_VERIFYDATE;
            LB_IVV_TB_VALIDDAYS.Content = App.m_LangPackage.LB_IVV_TB_VALIDDAYS;
            IVV_UNIT_DAYS.Content = App.m_LangPackage.IVV_UNIT_DAYS;
            LB_IVV_TB_REMARK.Content = App.m_LangPackage.LB_IVV_TB_REMARK;
            IVV_RB_VERIFY.Content = App.m_LangPackage.IVV_RB_VERIFY;
            IVV_BTN_CONFIRM.Content = App.m_LangPackage.IVV_BTN_CONFIRM;
            IVV_BTN_SEARCH.Content = App.m_LangPackage.IVV_BTN_SEARCH;
            IVV_BTN_EMPTY.Content = App.m_LangPackage.IVV_BTN_EMPTY;
            if (App.g_EngVer) WLV_PAGING.Lang = "ENG";
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(5), App.GetBehaviorRemark(5)));
        }
        //验证
        private void IVV_BTN_CONFIRM_Click(object sender, RoutedEventArgs e)
        {
            if (IVV_DP_VERIFYDATE.SelectedDate == null)
            {
                MessageBox.Show(App.m_LangPackage.TIP_IVV_NOSET_VERIFYTIME, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (IVV_RB_VERIFY.IsChecked == false)
            {
                MessageBox.Show(App.m_LangPackage.TIP_IVV_NOSET_VERIFYSTATUS, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ////定制条件检索
            string strAccount = App.g_TSession.TTUser.LoginName;
            string strVerifyTime = IVV_DP_VERIFYDATE.SelectedDate.Value.ToString(String.Format("{0}", App.m_strDateFormat));
            string verify = (IVV_RB_VERIFY.IsChecked == true) ? App.m_LangPackage.TIP_VERIFY_STATUS : App.m_LangPackage.TIP_UNVERIFY_STATUS;
            DateTime dt = DateTime.Parse(IVV_DP_VERIFYDATE.SelectedDate.ToString());
            dt = dt.AddDays(int.Parse(IVV_TB_VALIDDAYS.Text.ToString()));
            string strValidDate = BaseUtils.GetCurrentDate(dt);
            string strRemark = IVV_TB_REMARK.Text.ToString();

            string strCmd = string.Format(sql.SQL.SQL_C_VERIFYDATA, strAccount, strVerifyTime, strValidDate, verify, strRemark);
            if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_IVV_VERIFY_SUCCESS,App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                DataSearch();
                return;
            }
            return;
        }
        //清空 按钮 事件
        private void IVV_BTN_EMPTY_Click(object sender, RoutedEventArgs e)
        {
            IVV_RB_VERIFY.IsChecked = false;
            IVV_DP_VERIFYDATE.SelectedDate = null;
            IVV_TB_REMARK.Text = String.Empty;
        }
        //按索 按钮 事件
        private void IVV_BTN_SEARCH_Click(object sender, RoutedEventArgs e)
        {
            DataSearch();
        }
        public void InitialzeDataListView()
        {
            GridView gridView = new GridView();
            GridViewColumn[] gvColumn = new GridViewColumn[StaticParam.m_VerifyDataListHeadColumn.Length];

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
                    gvColumn[i].Header = StaticParam.m_VerifyDataListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_VerifyDataListHeadWidth[i];
                }
                else
                {
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath(StaticParam.m_VerifyDataListHeadField[i]);
                    gvColumn[i].DisplayMemberBinding = binding;
                    gvColumn[i].Header = StaticParam.m_VerifyDataListHeadColumn[i];
                    gvColumn[i].Width = StaticParam.m_VerifyDataListHeadWidth[i];
                }
                gridView.Columns.Add(gvColumn[i]);
            }

            LV_VERIFYDATE.View = gridView;

        }
        private void DataSearch()
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(32), App.GetBehaviorRemark(32)));
            ////定制条件检索
            string strSQLCondition = "";
            GetModel(strSQLCondition);
        }
        private void GetModel(String condition)
        {
            m_ds.Clear();
            UIOperator.EmptyListView(LV_VERIFYDATE);

            WLV_PAGING.RecordCount = "0";
            WLV_PAGING.PageCount = "0";
            WLV_PAGING.CurrentPage = "0";
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_VERIFYDATA, condition), m_ds, sql.SQL.T_VERIFYDATA);
            m_nRecordCount = m_ds.Tables[sql.SQL.T_VERIFYDATA].Rows.Count;
            if (m_nRecordCount > 0)
            {
                m_nPageSize = int.Parse(WLV_PAGING.PageSize);
                m_nPageCount = m_nRecordCount / m_nPageSize + 1;
                m_nCurrentPage = 0;
                ModelToView(m_nRecordCount, m_nRecordCount / m_nPageSize + 1, m_nCurrentPage, m_ds, sql.SQL.T_VERIFYDATA);
            }
        }
        //数据绑定视图
        private void ModelToView(int RecordCount, int PageCount, int CurrentPage, DataSet ds, string srcTable)
        {
            UIOperator.EmptyListView(LV_VERIFYDATE);

            WLV_PAGING.RecordCount = RecordCount.ToString();
            WLV_PAGING.PageCount = PageCount.ToString();
            WLV_PAGING.CurrentPage = (CurrentPage + 1).ToString();

            for (int i = CurrentPage * m_nPageSize; i < (CurrentPage + 1) * m_nPageSize; i++)
            {
                if (i < ds.Tables[srcTable].Rows.Count)
                {
                    VerifyData verifyData = new VerifyData();
                    Tools.PutVal<VerifyData>(verifyData, ds.Tables[sql.SQL.T_VERIFYDATA].Rows[i]);
                    LV_VERIFYDATE.Items.Add(verifyData);
                }
            }
        }
        //上一页 按钮 事件
        private void PageUp_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage > 0)
            {
                m_nCurrentPage--;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_VERIFYDATA);
            }
        }
        //下一页 按钮 事件
        private void PageDown_Click(object sender, RoutedEventArgs e)
        {
            if (m_nCurrentPage < m_nPageCount - 1)
            {
                m_nCurrentPage++;
                ModelToView(m_nRecordCount, m_nPageCount, m_nCurrentPage, m_ds, sql.SQL.T_VERIFYDATA);
            }
        }
    }
}
