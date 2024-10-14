using System;
using System.Windows;
using System.Data;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.pages.modal
{
    /// <summary>
    /// ExperimentDataModal.xaml 的交互逻辑
    /// </summary>
    public partial class ExperimentDataModal : Window
    {
        private TestData m_TestData;
        private DataTable m_DataTable;
        private double[,] m_RunCupTempArr;                        //实验温度样本数组
        public ExperimentDataModal(TestData testData)
        {
            m_TestData = testData;
            m_RunCupTempArr = new double[m_TestData.SampleTimes, App.m_nCupNum];
            InitializeComponent();
            InitializeInterface();
        }
        public void InitializeInterface()
        {
            EDM_MW.Title = App.m_LangPackage.EDM_MW;
            LB_EDM_EXP_STATUS.Content = App.m_LangPackage.LB_EDM_EXP_STATUS;
            LB_EDM_LOGINNAME.Content = App.m_LangPackage.LB_EDM_LOGINNAME;
            LB_EDM_STARTTIME.Content = App.m_LangPackage.LB_EDM_STARTTIME;
            LB_EDM_ENDTIME.Content = App.m_LangPackage.LB_EDM_ENDTIME;
            LB_EDM_METHODNAME.Content = App.m_LangPackage.LB_EDM_METHODNAME;
            LB_EDM_MANUFACTURER.Content = App.m_LangPackage.LB_EDM_MANUFACTURER;
            LB_EDM_BANCHNO.Content = App.m_LangPackage.LB_EDM_BANCHNO;
            LB_EDM_TESTDEPARTMENT.Content = App.m_LangPackage.LB_EDM_TESTDEPARTMENT;
            LB_EDM_DISSOLUTIONMETHODNAME.Content = App.m_LangPackage.LB_EDM_DISSOLUTIONMETHODNAME;
            LB_EDM_WATERBOXTEMP.Content = App.m_LangPackage.LB_EDM_WATERBOXTEMP;
            LB_EDM_SOLUTIONVOLUME.Content = App.m_LangPackage.LB_EDM_SOLUTIONVOLUME;
            LB_EDM_ALLTIMESPAN.Content = App.m_LangPackage.LB_EDM_ALLTIMESPAN;
            LB_EDM_FRONTSPEED_1.Content = App.m_LangPackage.LB_EDM_FRONTSPEED_1;
            LB_EDM_FRONTSPEED_2.Content = App.m_LangPackage.LB_EDM_FRONTSPEED_2;
            LB_EDM_BACKSPEED_1.Content = App.m_LangPackage.LB_EDM_BACKSPEED_1;
            LB_EDM_BACKSPEED_2.Content = App.m_LangPackage.LB_EDM_BACKSPEED_2;
            LB_EDM_FRONTSTARTTIME_1.Content = App.m_LangPackage.LB_EDM_FRONTSTARTTIME_1;
            LB_EDM_FRONTSTARTTIME_2.Content = App.m_LangPackage.LB_EDM_FRONTSTARTTIME_2;
            LB_EDM_BACKSTARTTIME_1.Content = App.m_LangPackage.LB_EDM_BACKSTARTTIME_1;
            LB_EDM_BACKSTARTTIME_2.Content = App.m_LangPackage.LB_EDM_BACKSTARTTIME_2;
            LB_EDM_SAMPLEVOLUME.Content = App.m_LangPackage.LB_EDM_SAMPLEVOLUME;
            LB_EDM_SAMPLETIMES.Content = App.m_LangPackage.LB_EDM_SAMPLETIMES;
            LB_EDM_AUTOSUPPLY.Content = App.m_LangPackage.LB_EDM_AUTOSUPPLY;
            LB_EDM_FIRSTFILTERVOLUME.Content = App.m_LangPackage.LB_EDM_FIRSTFILTERVOLUME;
            LB_EDM_Speed_Mode.Content = App.m_LangPackage.LB_EDM_Speed_Mode;
            LB_EDM_Electricmotor_Mode.Content = App.m_LangPackage.LB_EDM_Electricmotor_Mode;
            LB_EDM_TEMPSETTING.Content = App.m_LangPackage.LB_EDM_TEMPSETTING;
            LB_EDM_DILUTIONENABLED.Content = App.m_LangPackage.LB_EDM_DILUTIONENABLED;
            LB_EDM_DILUTIONRATIO.Content = App.m_LangPackage.LB_EDM_DILUTIONRATIO;
            EDM_BTN_CREATESIGNEDREPORT.Content = App.m_LangPackage.EDM_BTN_CREATESIGNEDREPORT;
            EDM_BTN_CLOSE.Content = App.m_LangPackage.EDM_BTN_CLOSE;

        }
        public void InitializeGridView()
        {
            m_DataTable = new DataTable("CUPTEMPTABLE");
            m_DataTable.Columns.Add(new DataColumn()
            {
                DataType = Type.GetType("System.Int32"),
                ColumnName = string.Format("Key")
            });
            m_DataTable.Columns.Add(new DataColumn()
            {
                DataType = Type.GetType("System.Int32"),
                ColumnName = string.Format("TimeValue")
            });
            for (int i = 0; i < App.m_nCupNum; i++)
            {
                m_DataTable.Columns.Add(new DataColumn()
                {
                    DataType = Type.GetType("System.String"),
                    ColumnName = string.Format("temp_cup_{0}", i + 1)
                });
            }
        }
        //绑定实验数据到视图
        private void BindToView()
        {
            EDM_EXP_STATUS.Content = m_TestData.Status;
            EDM_LOGINNAME.Content = m_TestData.LoginName;
            EDM_STARTTIME.Content = m_TestData.StartTime;
            EDM_ENDTIME.Content = m_TestData.EndTime;
            EDM_METHODNAME.Content = m_TestData.MethodName;
            EDM_MANUFACTURER.Content = m_TestData.Manufacturer;
            EDM_BANCHNO.Content = m_TestData.BatchNo;
            EDM_TESTDEPARTMENT.Content = m_TestData.TestDepartment;
            EDM_DISSOLUTIONMETHODNAME.Content = m_TestData.TestMethod;
            EDM_WATERBOXTEMP.Content = m_TestData.TempWaterBox;
            EDM_SOLUTIONVOLUME.Content = m_TestData.SolventVolume;
            EDM_ALLTIMESPAN.Content = m_TestData.AllTimeSpan;
            EDM_FRONTSPEED_1.Content = m_TestData.FrontRowSpeed_1;
            EDM_FRONTSPEED_2.Content = m_TestData.FrontRowSpeed_2;
            EDM_FRONTSTARTTIME_1.Content = m_TestData.FrontRowStartTime_1;
            EDM_FRONTSTARTTIME_2.Content = m_TestData.FrontRowStartTime_2;
            EDM_BACKSTARTTIME_1.Content = m_TestData.BackRowStartTime_1;
            EDM_BACKSTARTTIME_2.Content = m_TestData.BackRowStartTime_2;

            EDM_BACKSPEED_1.Content = m_TestData.BackRowSpeed_1;
            EDM_BACKSPEED_2.Content = m_TestData.BackRowSpeed_2;
            EDM_SAMPLEVOLUME.Content = m_TestData.SampleVolume;
            EDM_SAMPLETIMES.Content = m_TestData.SampleTimes;
            EDM_AUTOSUPPLY.Content = m_TestData.Auto_Fluid_Infusion;
            EDM_FIRSTFILTERVOLUME.Content = m_TestData.First_Filter_Volume;
            EDM_Speed_Mode.Content = m_TestData.SpeedMode;
            EDM_Electricmotor_Mode.Content = m_TestData.ElectricmotorMode;
            EDM_TEMPSETTING.Content = m_TestData.TempSetting;
            EDM_DILUTIONENABLED.Content = m_TestData.DilutionEnabled ? "是" : "否";
            EDM_DILUTIONRATIO.Content = m_TestData.DilutionEnabled ? m_TestData.DilutionRatio.ToString() : string.Empty;
        }
        //绑定取样信息到视图
        private void BindDataToGridView()
        {
            string cmdText = string.Format(sql.SQL.SQL_R_EXPCUPTEMPBYEXPID, m_TestData.ID);
            DataSet ds = new DataSet();
            App.m_SQLiteDBUtils.ExecuteQuery(cmdText, ds, sql.SQL.T_EXPCUPTEMP);
            try
            {
                for (int i = 0; i < ds.Tables[sql.SQL.T_EXPCUPTEMP].Rows.Count; i++)
                {
                    DataRow dr = m_DataTable.NewRow();
                    dr["Key"] = ds.Tables[sql.SQL.T_EXPCUPTEMP].Rows[i]["sampletime"];
                    dr["TimeValue"] = ds.Tables[sql.SQL.T_EXPCUPTEMP].Rows[i]["sampletimepoint"];
                    for (int j = 0; j < App.m_nCupNum; j++)
                    {
                        string strCup = string.Format("temp_cup_{0}", j + 1);
                        dr[strCup] = ds.Tables[sql.SQL.T_EXPCUPTEMP].Rows[i][strCup];
                        if (ds.Tables[sql.SQL.T_EXPCUPTEMP].Rows[i][strCup].ToString().Equals("-"))
                            m_RunCupTempArr[i, j] = -1;
                        else 
                           m_RunCupTempArr[i, j] = double.Parse(ds.Tables[sql.SQL.T_EXPCUPTEMP].Rows[i][strCup].ToString());
                    }
                    m_DataTable.Rows.Add(dr);
                }
            }
            catch (Exception e)
            {
                App.WriteSystemLog(e.ToString());
                return;
            }
           
            EDM_SAMPLEGRID.ItemsSource = m_DataTable.DefaultView;
            EDM_SAMPLEGRID.Columns[0].Header = App.m_LangPackage.EDM_SAMPLE_TIMES;
            EDM_SAMPLEGRID.Columns[0].Width = 85;
            EDM_SAMPLEGRID.Columns[1].Header = App.m_LangPackage.EDM_SAMPLE_POINT_TIME;
            EDM_SAMPLEGRID.Columns[1].Width = 150;
            for (int i = 0; i < App.m_nCupNum; i++)
            {
                EDM_SAMPLEGRID.Columns[2 + i].Header = string.Format(App.m_LangPackage.EDM_CUP_ID, i + 1 );
                EDM_SAMPLEGRID.Columns[2 + i].Width = 55;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindToView();
            InitializeGridView();
            BindDataToGridView();
        }
        //生成签名报告，如果报告存在，提示更新 
        private void EDM_BTN_CREATESIGNEDREPORT_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(19), true))
            {
                SignLoginModal signLoginModal = new SignLoginModal(m_TestData, this, m_RunCupTempArr);
                signLoginModal.ShowDialog();
            }
        }
        //关闭窗口
        private void EDM_BTN_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
