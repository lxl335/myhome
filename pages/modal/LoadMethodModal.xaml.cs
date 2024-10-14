using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// LoadMethodModal.xaml 的交互逻辑
    /// </summary>
    public partial class LoadMethodModal : Window
    {
        public delegate void CallBackLoadMethod(DissolutionMethod dissolutionMethod);
        public CallBackLoadMethod callBackLoadMethod;
        public delegate bool CallBackDelMethod(DissolutionMethod dissolutionMethod);
        public CallBackDelMethod callBackDelMethod;
        private DataSet m_ds = new DataSet();  //数据集用于检索数据
        private int m_nRecordCount;
        public LoadMethodModal(CallBackLoadMethod callBackLoadMethod, CallBackDelMethod callBackDelMethod)
        {
            this.callBackLoadMethod = callBackLoadMethod;
            this.callBackDelMethod = callBackDelMethod;
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            LMM_MW.Title = App.m_LangPackage.LMM_MW;
            LMM_SEARCH_COND_GROUPBOX.Header = App.m_LangPackage.LMM_SEARCH_COND_GROUPBOX;
            LB_DP_METHODDATE.Content = App.m_LangPackage.LB_DP_METHODDATE;
            CKB_DATETIMEENABLE.Content = App.m_LangPackage.CKB_DATETIMEENABLE;
            LB_LMM_METHODNAME.Content = App.m_LangPackage.LB_LMM_METHODNAME;
            LB_LMM_MANUFACTURER.Content = App.m_LangPackage.LB_LMM_MANUFACTURER;
            LB_LMM_BATCHNO.Content = App.m_LangPackage.LB_LMM_BATCHNO;
            LB_LMM_TESTDEPARTMENT.Content = App.m_LangPackage.LB_LMM_TESTDEPARTMENT;
            BTN_SEARCH.Content = App.m_LangPackage.LMM_BTN_SEARCH;
            BTN_EMPTY.Content = App.m_LangPackage.LMM_BTN_EMPTY;
            BTN_CALLMETHOD.Content = App.m_LangPackage.LMM_BTN_CALLMETHOD;
            BTN_DELMETHOD.Content = App.m_LangPackage.LMM_BTN_DELMETHOD;
            BTN_CANCEL.Content = App.m_LangPackage.LMM_BTN_CANCEL;

            LMM_GV_MethodName.Header = App.m_LangPackage.LMM_GV_MethodName;
            LMM_GV_Manufacturer.Header = App.m_LangPackage.LMM_GV_Manufacturer;
            LMM_GV_BatchNo.Header = App.m_LangPackage.LMM_GV_BatchNo;
            LMM_GV_TestDepartment.Header = App.m_LangPackage.LMM_GV_TestDepartment;
            LMM_GV_DissolutionMethodName.Header = App.m_LangPackage.LMM_GV_DissolutionMethodName;
            LMM_GV_TempSetting.Header = App.m_LangPackage.LMM_GV_TempSetting;
            LMM_GV_SolutionVolume.Header = App.m_LangPackage.LMM_GV_SolutionVolume;
            LMM_GV_FrontRowSpeed_1.Header = App.m_LangPackage.LMM_GV_FrontRowSpeed_1;
            LMM_GV_FrontRowSpeed_2.Header = App.m_LangPackage.LMM_GV_FrontRowSpeed_2;
            LMM_GV_FrontRowStartTime_1.Header = App.m_LangPackage.LMM_GV_FrontRowStartTime_1;
            LMM_GV_FrontRowStartTime_2.Header = App.m_LangPackage.LMM_GV_FrontRowStartTime_2;
            LMM_GV_BackRowSpeed_1.Header = App.m_LangPackage.LMM_GV_BackRowSpeed_1;
            LMM_GV_BackRowSpeed_2.Header = App.m_LangPackage.LMM_GV_BackRowSpeed_2;
            LMM_GV_BackRowStartTime_1.Header = App.m_LangPackage.LMM_GV_BackRowStartTime_1;
            LMM_GV_BackRowStartTime_2.Header = App.m_LangPackage.LMM_GV_BackRowStartTime_2;
            LMM_GV_SampleTimes.Header = App.m_LangPackage.LMM_GV_SampleTimes;
            LMM_GV_SampleVolume.Header = App.m_LangPackage.LMM_GV_SampleVolume;
            LMM_GV_Auto_Fluid_Infusion.Header = App.m_LangPackage.LMM_GV_Auto_Fluid_Infusion;
            LMM_GV_First_filter_volume.Header = App.m_LangPackage.LMM_GV_First_filter_volume;
            LMM_GV_AllTimespan.Header = App.m_LangPackage.LMM_GV_AllTimespan;
            LMM_GV_TimeValue1.Header = App.m_LangPackage.LMM_GV_TimeValue1;
            LMM_GV_TimeValue2.Header = App.m_LangPackage.LMM_GV_TimeValue2;
            LMM_GV_TimeValue3.Header = App.m_LangPackage.LMM_GV_TimeValue3;
            LMM_GV_TimeValue4.Header = App.m_LangPackage.LMM_GV_TimeValue4;
            LMM_GV_TimeValue5.Header = App.m_LangPackage.LMM_GV_TimeValue5;
            LMM_GV_TimeValue6.Header = App.m_LangPackage.LMM_GV_TimeValue6;
            LMM_GV_TimeValue7.Header = App.m_LangPackage.LMM_GV_TimeValue7;
            LMM_GV_TimeValue8.Header = App.m_LangPackage.LMM_GV_TimeValue8;
            LMM_GV_TimeValue9.Header = App.m_LangPackage.LMM_GV_TimeValue9;
            LMM_GV_TimeValue10.Header = App.m_LangPackage.LMM_GV_TimeValue10;
            LMM_GV_TimeValue11.Header = App.m_LangPackage.LMM_GV_TimeValue11;
            LMM_GV_TimeValue12.Header = App.m_LangPackage.LMM_GV_TimeValue12;
            LMM_GV_TimeValue13.Header = App.m_LangPackage.LMM_GV_TimeValue13;
            LMM_GV_TimeValue14.Header = App.m_LangPackage.LMM_GV_TimeValue14;
            LMM_GV_TimeValue15.Header = App.m_LangPackage.LMM_GV_TimeValue15;
            LMM_GV_TimeValue16.Header = App.m_LangPackage.LMM_GV_TimeValue16;
            LMM_GV_TimeValue17.Header = App.m_LangPackage.LMM_GV_TimeValue17;
            LMM_GV_TimeValue18.Header = App.m_LangPackage.LMM_GV_TimeValue18;
            LMM_GV_TimeValue19.Header = App.m_LangPackage.LMM_GV_TimeValue19;
            LMM_GV_TimeValue20.Header = App.m_LangPackage.LMM_GV_TimeValue20;
            LMM_GV_TimeValue21.Header = App.m_LangPackage.LMM_GV_TimeValue21;
            LMM_GV_TimeValue22.Header = App.m_LangPackage.LMM_GV_TimeValue22;
            LMM_GV_TimeValue23.Header = App.m_LangPackage.LMM_GV_TimeValue23;
            LMM_GV_TimeValue24.Header = App.m_LangPackage.LMM_GV_TimeValue24;
            LMM_GV_TimeValue25.Header = App.m_LangPackage.LMM_GV_TimeValue25;
            LMM_GV_TimeValue26.Header = App.m_LangPackage.LMM_GV_TimeValue26;
            LMM_GV_TimeValue27.Header = App.m_LangPackage.LMM_GV_TimeValue27;
            LMM_GV_TimeValue28.Header = App.m_LangPackage.LMM_GV_TimeValue28;
            LMM_GV_TimeValue29.Header = App.m_LangPackage.LMM_GV_TimeValue29;
            LMM_GV_TimeValue30.Header = App.m_LangPackage.LMM_GV_TimeValue30;
            LMM_GV_TimeValue31.Header = App.m_LangPackage.LMM_GV_TimeValue31;
            LMM_GV_TimeValue32.Header = App.m_LangPackage.LMM_GV_TimeValue32;
            LMM_GV_TimeValue33.Header = App.m_LangPackage.LMM_GV_TimeValue33;
            LMM_GV_TimeValue34.Header = App.m_LangPackage.LMM_GV_TimeValue34;
            LMM_GV_TimeValue35.Header = App.m_LangPackage.LMM_GV_TimeValue35;
            LMM_GV_TimeValue36.Header = App.m_LangPackage.LMM_GV_TimeValue36;
            LMM_GV_TimeValue37.Header = App.m_LangPackage.LMM_GV_TimeValue37;
            LMM_GV_TimeValue38.Header = App.m_LangPackage.LMM_GV_TimeValue38;
            LMM_GV_TimeValue39.Header = App.m_LangPackage.LMM_GV_TimeValue39;
            LMM_GV_TimeValue40.Header = App.m_LangPackage.LMM_GV_TimeValue40;
            LMM_GV_MethodTime.Header = App.m_LangPackage.LMM_GV_MethodTime;
            LMM_GV_SpeedMode.Header = App.m_LangPackage.LMM_GV_SpeedMode;
            LMM_GV_ElectricmotorMmode.Header = App.m_LangPackage.LMM_GV_ElectricmotorMmode;

        }
        //窗口加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MethodSearch();
        }
        #region
        ////////窗口事件
        //按钮事件，调用选中的方法
        private void BTN_CALLMETHOD_Click(object sender, RoutedEventArgs e)
        {
            LoadMethod();
        }
        //按钮事件，删除选中的方法
        private void BTN_DELMETHOD_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(2), true))
                DeleteMethod();
        }
        //按钮事件，取消调用，退出窗口
        private void BTN_CANCEL_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void CKB_DATETIMEENABLE_Click(object sender, RoutedEventArgs e)
        {
            if (CKB_DATETIMEENABLE.IsChecked == true)
            {
                DP_METHODDATE.IsEnabled = true;
            }
            else
                DP_METHODDATE.IsEnabled = false;

        }
        //调用方法
        private void LoadMethod()
        {
            int index = LV_METHOD.SelectedIndex;
            if (index != -1)
            {
                try
                {
                    DissolutionMethod dissolutionMethod = LV_METHOD.SelectedItem as DissolutionMethod;
                    callBackLoadMethod(dissolutionMethod);
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(21), App.GetBehaviorRemark(21)));
                    this.Close();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

            }
        }
        //删除方法
        private void DeleteMethod()
        {
            int index = LV_METHOD.SelectedIndex;
            if (index != -1)
            {
                try
                {
                    MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_DELETE_METHOD_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (mBoxResult)
                    {
                        case MessageBoxResult.Yes:
                            DissolutionMethod dissolutionMethod = LV_METHOD.SelectedItem as DissolutionMethod;
                            if (callBackDelMethod(dissolutionMethod))
                            {
                                LV_METHOD.Items.RemoveAt(index);
                                MessageBox.Show(App.m_LangPackage.TIP_DELETE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(22), App.GetBehaviorRemark(22)));
                            }
                            else
                            {
                                MessageBox.Show(App.m_LangPackage.TIP_DELETE_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(99), App.GetBehaviorRemark(99)));
                            }
                            break;
                        case MessageBoxResult.No:
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }

        private void BTN_SEARCH_Click(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(98), App.GetBehaviorRemark(98)));
            MethodSearch();
        }
        //根据条件加载方法
        private void MethodSearch()
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(98), App.GetBehaviorRemark(98)));
            ////定制条件检索
            string id_condition = "id > 0";
            string methodname_condition = String.Empty;
            string manufacture_condition = String.Empty;
            string banchno_condition = String.Empty;
            string testdepartment_condition = String.Empty;

            if (!String.IsNullOrEmpty(LMM_METHODNAME.Text))
            {
                methodname_condition = String.Format(" AND method_name='{0}'", LMM_METHODNAME.Text.Trim());
            }
            if (!String.IsNullOrEmpty(LMM_MANUFACTURER.Text))
            {
                manufacture_condition = String.Format(" AND manufacturer='{0}'", LMM_MANUFACTURER.Text);
            }
            if (!String.IsNullOrEmpty(LMM_BATCHNO.Text))
            {
                banchno_condition = String.Format(" AND batch_number='{0}'", LMM_BATCHNO.Text);
            }
            if (!String.IsNullOrEmpty(LMM_TESTDEPARTMENT.Text))
            {
                testdepartment_condition = String.Format(" AND test_department='{0}'", LMM_TESTDEPARTMENT.Text);
            }

            GetModel(String.Format("{0}{1}{2}{3}{4}", id_condition, methodname_condition, manufacture_condition, banchno_condition, testdepartment_condition));
        }
        //根据检索条件加载方法到列表
        private void GetModel(String condition)
        {
            m_ds.Clear();
            UIOperator.EmptyListView(LV_METHOD);
            App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_METHOD, condition), m_ds, sql.SQL.T_METHOD);
            m_nRecordCount = m_ds.Tables[sql.SQL.T_METHOD].Rows.Count;
            if (m_nRecordCount > 0)
            {
                ModelToView(m_nRecordCount, 0, 0, m_ds, sql.SQL.T_METHOD);
            }
        }
        //根据分页信息显示列表
        private void ModelToView(int RecordCount, int PageCount, int CurrentPage, DataSet ds, string srcTable)
        {

            for (int i = 0; i < ds.Tables[sql.SQL.T_METHOD].Rows.Count; i++)
            {
                DissolutionMethod dissolutionMethod = new DissolutionMethod();
                dissolutionMethod.MethodID = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["id"].ToString());
                dissolutionMethod.MethodName = ds.Tables[sql.SQL.T_METHOD].Rows[i]["method_name"].ToString();
                dissolutionMethod.Manufacturer = ds.Tables[sql.SQL.T_METHOD].Rows[i]["manufacturer"].ToString();
                dissolutionMethod.BatchNo = ds.Tables[sql.SQL.T_METHOD].Rows[i]["batch_number"].ToString();
                dissolutionMethod.TestDepartment = ds.Tables[sql.SQL.T_METHOD].Rows[i]["test_department"].ToString();
                dissolutionMethod.DissolutionMethodName = ds.Tables[sql.SQL.T_METHOD].Rows[i]["test_method"].ToString();
                dissolutionMethod.TempSetting = double.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["temp_setting"].ToString());
                dissolutionMethod.SolventVolume = double.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["solvent_volume"].ToString());
                dissolutionMethod.oSpeedModule.FrontRowSpeed_1 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["frontrow_speed_1"].ToString());
                dissolutionMethod.oSpeedModule.FrontRowSpeed_2 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["frontrow_speed_2"].ToString());
                dissolutionMethod.oSpeedModule.FrontRowStartTime_1 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["frontrow_starttime_1"].ToString());
                dissolutionMethod.oSpeedModule.FrontRowStartTime_2 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["frontrow_starttime_2"].ToString());
                dissolutionMethod.oSpeedModule.BackRowSpeed_1 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["backrow_speed_1"].ToString());
                dissolutionMethod.oSpeedModule.BackRowSpeed_2 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["backrow_speed_2"].ToString());
                dissolutionMethod.oSpeedModule.BackRowStartTime_1 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["backrow_starttime_1"].ToString());
                dissolutionMethod.oSpeedModule.BackRowStartTime_2 = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["backrow_starttime_2"].ToString());
                dissolutionMethod.SampleTimes = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["sample_times"].ToString());//取样次数
                dissolutionMethod.SampleVolume = double.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["sample_volume"].ToString());//取样体积
                dissolutionMethod.Auto_Fluid_Infusion = ds.Tables[sql.SQL.T_METHOD].Rows[i]["auto_fluid_infusion"].ToString();//自动补液
                dissolutionMethod.First_filter_volume = double.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["first_filter_volume"].ToString());//初滤体
                dissolutionMethod.AllTimespan = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["all_timespan"].ToString());
                for (int j = 1; j <= StaticParam.SAMPLE_TIME; j++)
                {
                    string SegmentName = String.Format("sample_time_{0}", j);
                    dissolutionMethod.Sample_Time_Arr[j - 1].TimeValue = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i][SegmentName].ToString());
                }
                dissolutionMethod.MethodTime = ds.Tables[sql.SQL.T_METHOD].Rows[i]["method_time"].ToString();
                dissolutionMethod.oSpeedModule.SpeedMode = ds.Tables[sql.SQL.T_METHOD].Rows[i]["speed_mode"].ToString();
                dissolutionMethod.oSpeedModule.ElectricmotorMmode = ds.Tables[sql.SQL.T_METHOD].Rows[i]["electricmotor_mode"].ToString();
                dissolutionMethod.DilutionEnabled = bool.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["dilution_enabled"].ToString());
                dissolutionMethod.DilutionRatio = int.Parse(ds.Tables[sql.SQL.T_METHOD].Rows[i]["dilution_ratio"].ToString());
                LV_METHOD.Items.Add(dissolutionMethod);
            }
        }
    }
}
