using System;
using System.Data;
using System.Windows;
using Pharmacy.INST.DissolutionClient.entities;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.common
{
    public class DataPersistent
    {
        public DataPersistent(TestData testData)
        {
            string strCmd = String.Format(sql.SQL.SQL_C_TESTDATA,
                     testData.Status, testData.StartTime, testData.EndTime, testData.LoginName
                     , testData.MethodName, testData.Manufacturer, testData.BatchNo
                     , testData.TestDepartment, testData.TestMethod, testData.TempWaterBox, testData.TempWaterBox
                     , testData.CupTemp_Arr[0], testData.CupTemp_Arr[1], testData.CupTemp_Arr[2], testData.CupTemp_Arr[3]
                     , testData.CupTemp_Arr[4], testData.CupTemp_Arr[5], testData.CupTemp_Arr[6], testData.CupTemp_Arr[7]
                     , testData.CupTemp_Arr[8], testData.CupTemp_Arr[9], testData.CupTemp_Arr[10], testData.CupTemp_Arr[11]
                     , StaticParam.NULLSING, StaticParam.NULLSING, StaticParam.NULLSING, StaticParam.NULLSING   //13-16杯暂不提供
                     , testData.FrontRowSpeed_1, testData.FrontRowSpeed_2
                     , testData.BackRowSpeed_1, testData.BackRowSpeed_2
                     , testData.AllTimeSpan, testData.TempSetting, testData.SolventVolume
                     , testData.Sample_Time_Arr[0].TimeValue, testData.Sample_Time_Arr[1].TimeValue, testData.Sample_Time_Arr[2].TimeValue
                     , testData.Sample_Time_Arr[3].TimeValue, testData.Sample_Time_Arr[4].TimeValue, testData.Sample_Time_Arr[5].TimeValue
                     , testData.Sample_Time_Arr[6].TimeValue, testData.Sample_Time_Arr[7].TimeValue, testData.Sample_Time_Arr[8].TimeValue
                     , testData.Sample_Time_Arr[9].TimeValue, testData.Sample_Time_Arr[10].TimeValue, testData.Sample_Time_Arr[11].TimeValue
                     , testData.Sample_Time_Arr[12].TimeValue, testData.Sample_Time_Arr[13].TimeValue, testData.Sample_Time_Arr[14].TimeValue
                     , testData.Sample_Time_Arr[15].TimeValue, testData.Sample_Time_Arr[16].TimeValue, testData.Sample_Time_Arr[17].TimeValue
                     , testData.Sample_Time_Arr[18].TimeValue, testData.Sample_Time_Arr[19].TimeValue, testData.Sample_Time_Arr[20].TimeValue
                     , testData.Sample_Time_Arr[21].TimeValue, testData.Sample_Time_Arr[22].TimeValue, testData.Sample_Time_Arr[23].TimeValue
                     , testData.Sample_Time_Arr[24].TimeValue, testData.Sample_Time_Arr[25].TimeValue, testData.Sample_Time_Arr[26].TimeValue
                     , testData.Sample_Time_Arr[27].TimeValue, testData.Sample_Time_Arr[28].TimeValue, testData.Sample_Time_Arr[29].TimeValue
                     , testData.Sample_Time_Arr[30].TimeValue, testData.Sample_Time_Arr[31].TimeValue, testData.Sample_Time_Arr[32].TimeValue
                     , testData.Sample_Time_Arr[33].TimeValue, testData.Sample_Time_Arr[34].TimeValue, testData.Sample_Time_Arr[35].TimeValue
                     , testData.Sample_Time_Arr[36].TimeValue, testData.Sample_Time_Arr[37].TimeValue, testData.Sample_Time_Arr[38].TimeValue
                     , testData.Sample_Time_Arr[39].TimeValue
                     , testData.SampleTimes, testData.SampleVolume, testData.Auto_Fluid_Infusion, testData.First_Filter_Volume
                     , BaseUtils.GetCurrentDateTime(), testData.SpeedMode, testData.ElectricmotorMode
                     , testData.FrontRowStartTime_1,testData.FrontRowStartTime_2,testData.BackRowStartTime_1,testData.BackRowStartTime_2
                     , testData.DilutionEnabled, testData.DilutionRatio
                     );
            if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) > 0) //执行SQL
            {
                DataSet ds = new DataSet();
                App.m_SQLiteDBUtils.ExecuteQuery(String.Format(sql.SQL.SQL_R_TESTDATABYSTARTTIME, testData.StartTime), ds, sql.SQL.T_TESTDATA);
                if (ds.Tables[sql.SQL.T_TESTDATA].Rows.Count > 0)
                {
                    App.m_nRunningExperimentID = int.Parse(ds.Tables[sql.SQL.T_TESTDATA].Rows[0]["id"].ToString());
                    App.SetSystemStatus("创建实验记录成功");
                }
            }
            else
            {
                App.SetSystemStatus("创建实验记录失败");
            }
        }
        public DataPersistent(DissolutionMethod dissolutionMethod, string sqltype)
        {
            string strCmd = String.Empty;
            switch (sqltype)
            {
                case "CREATE":
                        strCmd = String.Format(sql.SQL.SQL_C_CREATEMETHOD
                        , dissolutionMethod.MethodName, dissolutionMethod.Manufacturer, dissolutionMethod.BatchNo, dissolutionMethod.TestDepartment
                        , dissolutionMethod.DissolutionMethodName, dissolutionMethod.TempSetting, dissolutionMethod.SolventVolume
                        , dissolutionMethod.oSpeedModule.FrontRowSpeed_1, dissolutionMethod.oSpeedModule.FrontRowSpeed_2
                        , dissolutionMethod.oSpeedModule.FrontRowStartTime_1, dissolutionMethod.oSpeedModule.FrontRowStartTime_2
                        , dissolutionMethod.oSpeedModule.BackRowSpeed_1, dissolutionMethod.oSpeedModule.BackRowSpeed_2
                        , dissolutionMethod.oSpeedModule.BackRowStartTime_1, dissolutionMethod.oSpeedModule.BackRowStartTime_2
                        , dissolutionMethod.SampleTimes, dissolutionMethod.SampleVolume, dissolutionMethod.Auto_Fluid_Infusion, dissolutionMethod.First_filter_volume, dissolutionMethod.AllTimespan
                        , dissolutionMethod.Sample_Time_Arr[0].TimeValue, dissolutionMethod.Sample_Time_Arr[1].TimeValue, dissolutionMethod.Sample_Time_Arr[2].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[3].TimeValue, dissolutionMethod.Sample_Time_Arr[4].TimeValue, dissolutionMethod.Sample_Time_Arr[5].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[6].TimeValue, dissolutionMethod.Sample_Time_Arr[7].TimeValue, dissolutionMethod.Sample_Time_Arr[8].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[9].TimeValue, dissolutionMethod.Sample_Time_Arr[10].TimeValue, dissolutionMethod.Sample_Time_Arr[11].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[12].TimeValue, dissolutionMethod.Sample_Time_Arr[13].TimeValue, dissolutionMethod.Sample_Time_Arr[14].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[15].TimeValue, dissolutionMethod.Sample_Time_Arr[16].TimeValue, dissolutionMethod.Sample_Time_Arr[17].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[18].TimeValue, dissolutionMethod.Sample_Time_Arr[19].TimeValue, dissolutionMethod.Sample_Time_Arr[20].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[21].TimeValue, dissolutionMethod.Sample_Time_Arr[22].TimeValue, dissolutionMethod.Sample_Time_Arr[23].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[24].TimeValue, dissolutionMethod.Sample_Time_Arr[25].TimeValue, dissolutionMethod.Sample_Time_Arr[26].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[27].TimeValue, dissolutionMethod.Sample_Time_Arr[28].TimeValue, dissolutionMethod.Sample_Time_Arr[29].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[30].TimeValue, dissolutionMethod.Sample_Time_Arr[31].TimeValue, dissolutionMethod.Sample_Time_Arr[32].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[33].TimeValue, dissolutionMethod.Sample_Time_Arr[34].TimeValue, dissolutionMethod.Sample_Time_Arr[35].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[36].TimeValue, dissolutionMethod.Sample_Time_Arr[37].TimeValue, dissolutionMethod.Sample_Time_Arr[38].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[39].TimeValue, dissolutionMethod.MethodTime, dissolutionMethod.oSpeedModule.SpeedMode, dissolutionMethod.oSpeedModule.ElectricmotorMmode
                        , dissolutionMethod.DilutionEnabled, dissolutionMethod.DilutionRatio);
                    break;
                case "UPDATE":
                        strCmd = String.Format(sql.SQL.SQL_U_UPDATEMETHOD,
                        dissolutionMethod.Manufacturer, dissolutionMethod.BatchNo, dissolutionMethod.TestDepartment
                        , dissolutionMethod.DissolutionMethodName, dissolutionMethod.TempSetting, dissolutionMethod.SolventVolume
                        , dissolutionMethod.oSpeedModule.FrontRowSpeed_1, dissolutionMethod.oSpeedModule.FrontRowSpeed_2
                        , dissolutionMethod.oSpeedModule.FrontRowStartTime_1, dissolutionMethod.oSpeedModule.FrontRowStartTime_2
                        , dissolutionMethod.oSpeedModule.BackRowSpeed_1, dissolutionMethod.oSpeedModule.BackRowSpeed_2
                        , dissolutionMethod.oSpeedModule.BackRowStartTime_1, dissolutionMethod.oSpeedModule.BackRowStartTime_2
                        , dissolutionMethod.SampleTimes, dissolutionMethod.SampleVolume, dissolutionMethod.Auto_Fluid_Infusion, dissolutionMethod.First_filter_volume, dissolutionMethod.AllTimespan
                        , dissolutionMethod.Sample_Time_Arr[0].TimeValue, dissolutionMethod.Sample_Time_Arr[1].TimeValue, dissolutionMethod.Sample_Time_Arr[2].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[3].TimeValue, dissolutionMethod.Sample_Time_Arr[4].TimeValue, dissolutionMethod.Sample_Time_Arr[5].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[6].TimeValue, dissolutionMethod.Sample_Time_Arr[7].TimeValue, dissolutionMethod.Sample_Time_Arr[8].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[9].TimeValue, dissolutionMethod.Sample_Time_Arr[10].TimeValue, dissolutionMethod.Sample_Time_Arr[11].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[12].TimeValue, dissolutionMethod.Sample_Time_Arr[13].TimeValue, dissolutionMethod.Sample_Time_Arr[14].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[15].TimeValue, dissolutionMethod.Sample_Time_Arr[16].TimeValue, dissolutionMethod.Sample_Time_Arr[17].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[18].TimeValue, dissolutionMethod.Sample_Time_Arr[19].TimeValue, dissolutionMethod.Sample_Time_Arr[20].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[21].TimeValue, dissolutionMethod.Sample_Time_Arr[22].TimeValue, dissolutionMethod.Sample_Time_Arr[23].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[24].TimeValue, dissolutionMethod.Sample_Time_Arr[25].TimeValue, dissolutionMethod.Sample_Time_Arr[26].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[27].TimeValue, dissolutionMethod.Sample_Time_Arr[28].TimeValue, dissolutionMethod.Sample_Time_Arr[29].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[30].TimeValue, dissolutionMethod.Sample_Time_Arr[31].TimeValue, dissolutionMethod.Sample_Time_Arr[32].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[33].TimeValue, dissolutionMethod.Sample_Time_Arr[34].TimeValue, dissolutionMethod.Sample_Time_Arr[35].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[36].TimeValue, dissolutionMethod.Sample_Time_Arr[37].TimeValue, dissolutionMethod.Sample_Time_Arr[38].TimeValue
                        , dissolutionMethod.Sample_Time_Arr[39].TimeValue, dissolutionMethod.MethodTime, dissolutionMethod.oSpeedModule.SpeedMode, dissolutionMethod.oSpeedModule.ElectricmotorMmode
                        , dissolutionMethod.DilutionEnabled,dissolutionMethod.DilutionRatio
                        , dissolutionMethod.MethodName);
                    break;
            }
            //执行SQL
            if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) > 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_SAVE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(20),App.GetBehaviorRemark(20)));
            }
            else
            {
                MessageBox.Show(App.m_LangPackage.TIP_ACCOUNT_SAVE_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(81), App.GetBehaviorRemark(81)));
            }
        }

        public DataPersistent(TestData testData, double[,] arr)
        {
            string strCmd = String.Empty;
            string strCupFields = "";
            for (int j = 0; j < App.m_nCupNum; j++)
            {
                strCupFields += string.Format(",temp_cup_{0}", j + 1);
            }
            for (int i = 0; i < testData.SampleTimes; i++)
            {
                string strCupTemp = "";
                for (int k = 0; k < App.m_nCupNum; k++)
                {
                    if (!App.g_cuptempenabled[k])
                        strCupTemp += string.Format(",'{0}'", "-");
                    else
                        strCupTemp += string.Format(",'{0}'", arr[i, k].ToString("F"));
                }
                strCmd = string.Format(sql.SQL.SQL_C_EXPCUPTEMP,
                    strCupFields, App.m_nRunningExperimentID, testData.Sample_Time_Arr[i].Key,
                    testData.Sample_Time_Arr[i].TimeValue, strCupTemp);
                //执行SQL
                if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) < 1)
                {
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(2), App.GetBehavior(152),App.GetBehaviorRemark(152)));
                }

            }
        }
    }
}
