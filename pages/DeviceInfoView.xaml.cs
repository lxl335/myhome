using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Pharmacy.INST.DissolutionClient.common;
using System.Threading;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// DeviceInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfoView : Page
    {
        public double m_dRealWaterTemp = 0;

        public DeviceInfoView()
        {
            InitializeComponent();
            InitializeInterface();
            DisableAllButton();
        }
        //初始化界面
        public void InitializeInterface()
        {
            DIV_TEMPCALI_GROUPBOX.Header = App.m_LangPackage.DIV_TEMPCALI_GROUPBOX;
            DIV_GATHER_GROUPBOX.Header = App.m_LangPackage.DIV_GATHER_GROUPBOX;
            LB_DIV_WATERBOXTEMP.Content = App.m_LangPackage.LB_DIV_WATERBOXTEMP;
            LB_DIV_TB_WATERBOXTEMP_CALI.Content = App.m_LangPackage.LB_DIV_TB_WATERBOXTEMP_CALI;
            DIV_BTN_GATH_WATERBOX_TEMP.Content = App.m_LangPackage.DIV_BTN_GATH_WATERBOX_TEMP;
            DIV_BTN_CALIBRATION_WATERBOX_RESET.Content = App.m_LangPackage.DIV_BTN_CALIBRATION_WATERBOX_RESET;
            DIV_BTN_CALIBRATION_WATERBOX.Content = App.m_LangPackage.DIV_BTN_CALIBRATION_WATERBOX;
            LB_DIV_CUPTEMP.Content = App.m_LangPackage.LB_DIV_CUPTEMP;
            LB_DIV_GATHERVAL_1.Content = App.m_LangPackage.LB_DIV_GATHERVAL;
            LB_DIV_SURVEYVAL_1.Content = App.m_LangPackage.LB_DIV_SURVEYVAL;
            LB_DIV_GATHERVAL_2.Content = App.m_LangPackage.LB_DIV_GATHERVAL;
            LB_DIV_SURVEYVAL_2.Content = App.m_LangPackage.LB_DIV_SURVEYVAL;
            DIV_BTN_GATHERTMP.Content = App.m_LangPackage.DIV_BTN_GATHERTMP;
            DIV_BTN_CALIBRATION_CUP_RESET.Content = App.m_LangPackage.DIV_BTN_CALIBRATION_CUP_RESET;
            DIV_BTN_CALIBRATION_CUP.Content = App.m_LangPackage.DIV_BTN_CALIBRATION_CUP;
            DIV_BTN_VOLUMECALIBRATION_RESET.Content = App.m_LangPackage.DIV_BTN_VOLUMECALIBRATION_RESET;
            DIV_BTN_FIRSTSTEP.Content = App.m_LangPackage.DIV_BTN_FIRSTSTEP;
            DIV_BTN_PUSHOUTLETWATER.Content = App.m_LangPackage.DIV_BTN_PUSHOUTLETWATER;
            DIV_BTN_SUCK.Content = App.m_LangPackage.DIV_BTN_SUCK;
            DIV_BTN_PUTVOLUME.Content = App.m_LangPackage.DIV_BTN_PUTVOLUME;
            DIV_BTN_OVER.Content = App.m_LangPackage.DIV_BTN_OVER;
            TIP_DIV_CALI_RESET.Content = App.m_LangPackage.TIP_DIV_CALI_RESET;
            TIP_DIV_SUCK_MAX.Content = App.m_LangPackage.TIP_DIV_SUCK_MAX;
            TIP_DIV_0P1ML.Content = App.m_LangPackage.TIP_DIV_0P1ML;
            OP_VOLUME_INPUT.Content = App.m_LangPackage.OP_VOLUME_INPUT;
            REAL_VOLUME_INPUT.Content = App.m_LangPackage.REAL_VOLUME_INPUT;
            TIP_DIV_GROUP1.Content = App.m_LangPackage.TIP_DIV_GROUP1;
            TIP_DIV_GROUP2.Content = App.m_LangPackage.TIP_DIV_GROUP2;
            TIP_DIV_STANDARD_COE.Content = App.m_LangPackage.TIP_DIV_STANDARD_COE;
            DIV_BTN_VOLUMECALIBRATION.Content = App.m_LangPackage.DIV_BTN_VOLUMECALIBRATION;
            NOTE_TITLE.Text = App.m_LangPackage.NOTE_TITLE;
            NOTE_STEP_1.Text = App.m_LangPackage.NOTE_STEP_1;
            NOTE_STEP_2.Text = App.m_LangPackage.NOTE_STEP_2;
            NOTE_STEP_3.Text = App.m_LangPackage.NOTE_STEP_3;
            NOTE_STEP_4.Text = App.m_LangPackage.NOTE_STEP_4;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(7), App.GetBehaviorRemark(7)));
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }
        #region 回调
        //刷新水箱温度
        public void RefreshBoxTemp(short t1, short t2)
        {
            string strTempC1 = ((double)t1 / 100).ToString("F");
            m_dRealWaterTemp = double.Parse(strTempC1);
            //string strTempC2 = ((double)t2 / 100).ToString("F");       //第二通道值，暂未使用
            DIV_WATERBOXTEMP.Dispatcher.Invoke(new Action(delegate
            {
                DIV_WATERBOXTEMP.Content = strTempC1;
            }));
            //DIV_WATERBOXTEMP_2.Dispatcher.Invoke(new Action(delegate
            //{
            //    DIV_WATERBOXTEMP_2.Content = strTempC2;
            //}));
        }
        //刷新小杯温度
        public void RefreshCupTemp(short[] tArr)
        {
            Label[] t_cup = new Label[App.m_nCupNum];

            for (int i = 0; i < App.m_nCupNum; i++)
            {
                t_cup[i] = (Label)FindName(String.Format("DIV_CUP_TEMP_{0}", i + 1));
                if (t_cup[i] != null)
                {
                    t_cup[i].Dispatcher.Invoke(new Action(delegate
                    {
                        t_cup[i].Content = ((double)tArr[i] / 100).ToString("F");
                    }));
                }
            }
        }
        #endregion
        #region 水箱温度
        //获取水箱温度
        private void DIV_BTN_GATH_WATERBOX_TEMP__Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();
            ExpStepAction.ReadWaterBoxTemp(this);
            ExpStepAction.ResumeTempCollect();
        }
        //重置水箱温度校准值
        private void DIV_BTN_CALIBRATION_WATERBOX_RESET_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            ExpStepAction.SuspendTempCollect();
            string strDatagramReset = Datagram.T_TEMP_CALI_RESET;
            if (BaseUtils.SendYKDatagram(strDatagramReset))
            {
                App.SetSystemStatus("水箱温度校准值重置完成！");
                ExpStepAction.ReadWaterBoxTemp(this);
                MessageBox.Show(App.m_LangPackage.TIP_DIV_TANK_TEMPCALI_RESET_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            ExpStepAction.ResumeTempCollect();
        }
        //标定水箱温度
        private void DIV_BTN_CALIBRATION_WATERBOX_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrEmpty(DIV_TB_WATERBOXTEMP_CALI.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_DIV_CALI_ISNULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double real_temp_val;
            double collect_temp_val;
            double cali_val;
            string strDatagram = String.Empty;
            try
            {
                real_temp_val = double.Parse(DIV_WATERBOXTEMP.Content.ToString());
                collect_temp_val = double.Parse(DIV_TB_WATERBOXTEMP_CALI.Text);
                cali_val = collect_temp_val - real_temp_val;
                if (cali_val < -2.5 || cali_val > 2.5)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_DIV_TANK_CALI_OUT_OF_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string strCmd = String.Format(sql.SQL.SQL_U_TEMPCALI, cali_val, 1, 0);
                App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);

                string Key = string.Format("K_{0}", 1);
                App.m_ProfileUtils.WriteIniData("TankTemp", Key, cali_val.ToString(), App.g_CalibrationFileName);

                ExpStepAction.SuspendTempCollect();

                string value = ((byte)(Math.Abs(cali_val) * 100)).ToString("X2");
                strDatagram = String.Format(Datagram.T_TEMP_CALI, "1", cali_val >= 0 ? "1" : "2", value);
                if (BaseUtils.SendYKDatagram(strDatagram))
                {
                    App.SetSystemStatus("水箱温度校准值已下发！");
                    MessageBox.Show(App.m_LangPackage.TIP_DIV_TANK_CALI_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            }
            catch (Exception e1) { System.Console.Write(e1.ToString()); }
        }
        #endregion
        #region 杯内温度
        //获取杯内温度
        private void DIV_BTN_GATHERCUPTMP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();
            ExpStepAction.ReadCupTemp(this);
            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //重置杯内温度校准值
        private void DIV_BTN_CALIBRATION_CUP_RESET_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string str = String.Empty;
            string strDatagram = String.Empty;
            for (int i = 0; i <= StaticParam.CUP_NUM; i++)
            {
                try
                {
                    double cali_val = 0;
                    string strCmd = String.Format(sql.SQL.SQL_U_TEMPCALI, cali_val, i + 1, 1);
                    App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);

                    string Key = string.Format("K_{0}", i + 1);
                    App.m_ProfileUtils.WriteIniData("CupTemp", Key, cali_val.ToString(), App.g_CalibrationFileName);




                    string value = ((short)(cali_val * 100)).ToString("X3");
                    string status = string.Format("{0}{1}", cali_val >= 0 ? "1" : "2", value);
                    status = status.Substring(0, 2) + " " + status.Substring(2, 2);
                    str += status + " ";

                }
                catch (Exception e1)
                {
                    System.Console.Write(e1.ToString());
                    MessageBox.Show(App.m_LangPackage.TIP_DIV_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ExpStepAction.SuspendTempCollect();

            strDatagram = string.Format(Datagram.T_CUPTEMP_CALI, str.TrimEnd());
            if (BaseUtils.SendYKDatagram(strDatagram))
            {
                App.SetSystemStatus("水杯温度校准值已重置！");
                MessageBox.Show(App.m_LangPackage.TIP_DIV_CUP_TEMPCALI_RESET_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //标定水杯温度
        private void DIV_BTN_CALIBRATION_CUP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string str = String.Empty;
            string strDatagram = String.Empty;
            for (int i = 0; i <= StaticParam.CUP_NUM; i++)
            {
                TextBox t_cup = (TextBox)FindName(String.Format("DIV_TB_CUP_TEMP_CALI_{0}", i + 1));
                Label t_coll_cup = (Label)FindName(String.Format("DIV_CUP_TEMP_{0}", i + 1));
                if (String.IsNullOrEmpty(t_cup.Text) && i < App.m_nCupNum)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_DIV_CUP_TEMP_EXSIT_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                double real_temp_val;
                double collect_temp_val;
                double cali_val;
                try
                {
                    if (i > App.m_nCupNum - 1)
                    {
                        cali_val = 0;
                    }
                    else
                    {
                        real_temp_val = double.Parse(t_cup.Text);
                        collect_temp_val = double.Parse(t_coll_cup.Content.ToString());
                        cali_val = real_temp_val - collect_temp_val;
                    }

                    string strCmd = String.Format(sql.SQL.SQL_U_TEMPCALI, cali_val, i + 1, 1);
                    App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd);

                    string Key = string.Format("K_{0}", i + 1);
                    App.m_ProfileUtils.WriteIniData("CupTemp", Key, cali_val.ToString(), App.g_CalibrationFileName);

                    string value = ((short)(Math.Abs(cali_val) * 100)).ToString("X3");
                    string status = string.Format("{0}{1}", cali_val >= 0 ? "1" : "2", value);
                    status = status.Substring(0, 2) + " " + status.Substring(2, 2);
                    str += status + " ";

                }
                catch (Exception e1)
                {
                    System.Console.Write(e1.ToString());
                    MessageBox.Show(App.m_LangPackage.TIP_DIV_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            ExpStepAction.SuspendTempCollect();

            strDatagram = string.Format(Datagram.T_CUPTEMP_CALI, str.TrimEnd());
            if (BaseUtils.SendYKDatagram(strDatagram))
            {
                App.SetSystemStatus("水杯温度校准值已下发！");
                MessageBox.Show(App.m_LangPackage.TIP_DIV_CUP_CALI_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集

        }

        #endregion
        #region 体积校准
        //设置按钮状态
        private void DisableAllButton()
        {
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_FIRSTSTEP, false);
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_PUSHOUTLETWATER, false);
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_SUCK, false);
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_PUTVOLUME, false);
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_SUCK_AGAIN, false);
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_PUSHWATER, false);
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_OVER, false);
        }
        //重置体积校准值
        private void DIV_BTN_VOLUMECALIBRATION_RESET_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            App.m_ProfileUtils.WriteIniData("Injector", "K", "1", App.g_CalibrationFileName);
            App.m_ProfileUtils.WriteIniData("Injector", "B", "0", App.g_CalibrationFileName);
            App.g_Injector_K = 1;
            App.g_Injector_B = 0;
            DisableAllButton();
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_FIRSTSTEP, true);
            MessageBox.Show(App.m_LangPackage.TIP_VOLUME_CALI_RESET, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        //第一步 吸液
        private void DIV_BTN_FIRSTSTEP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                DisableAllButton();
                ExpStepAction.SuspendTempCollect();
                ExpStepAction.ValveReset(null);
                ExpStepAction.ValveRotation(null, 3, "补液位");      //换向阀转动到补液位
                ExpStepAction.InjectorPump(null, 0, false);         //吸到最大
                ExpStepAction.ResumeTempCollect(); //恢复温度采集
                UIOperator.SetComponetValidAndOpacity(DIV_BTN_PUSHOUTLETWATER, true);
            });
        }
        //第二步 打废液
        private void DIV_BTN_PUSHOUTLETWATER_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int volume = 0;
            try
            {
                volume = int.Parse(DIV_TB_OUTLETWATER.Text.ToString());
            }
            catch (Exception e2)
            {
                MessageBox.Show(App.m_LangPackage.TIP_DIV_INPUT_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(e2.ToString());
                return;
            }          
            Task t = Task.Run(() =>
            {
                DisableAllButton();

                ExpStepAction.SuspendTempCollect();
                ExpStepAction.ValveReset(null);
                ExpStepAction.CollectFrameExhibitOrganReset(null);          //收集器横臂复位
                ExpStepAction.CollectFrameSampleOrganReset(null);           //收集器出样针复位
                ExpStepAction.CollectFrameSampleOrganDown(null);            //收集器出样针头下降
                try
                {
                    if (volume < 0 || volume > 20)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_WASTE_LIQUID_VOLUME_OUT_OF_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ExpStepAction.InjectDrain(null, volume, false);
                }
                catch (Exception e1)
                {
                    System.Diagnostics.Debug.WriteLine(e1.ToString());
                }
                ExpStepAction.CollectFrameSampleOrganReset(null);           //收集器出样针复位
                ExpStepAction.ResumeTempCollect(); //恢复温度采集

                
                UIOperator.SetComponetValidAndOpacity(DIV_BTN_SUCK, true);
            });
        }
        // 第三步 倒吸
        private void DIV_BTN_SUCK_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                DisableAllButton();
                ExpStepAction.SuspendTempCollect();
                ExpStepAction.InjectorPump(null, 0.1, false);
                ExpStepAction.ResumeTempCollect(); //恢复温度采集

                UIOperator.SetComponetValidAndOpacity(DIV_BTN_PUTVOLUME, true);
            });
        }
        //第四步 打操作体积
        private void DIV_BTN_PUTVOLUME_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double volume = 0;
            try
            {
                volume = double.Parse(DIV_TB_VOLUME.Text.ToString());
                if (volume < 0 || volume > 20)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_OPERATING_VOLUME_OUT_OF_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception e1) { System.Console.Write(e1.ToString());  }

            Task t = Task.Run(() =>
            {
                DisableAllButton();
                ExpStepAction.SuspendTempCollect();
                ExpStepAction.ValveReset(null);
                ExpStepAction.CollectFrameSampleOrganReset(null);           //收集器出样针头上升复位
                ExpStepAction.CollectFrameExhibitOrganReset(null);          //收集器出样针头横移至废液槽(复位)
                ExpStepAction.CollectFrameExhibitOrganMoveTo(null, 20);
                ExpStepAction.CollectFrameSampleOrganDown(null);            //收集器出样针头下降
                try
                {
                    volume = volume * App.g_Injector_K + App.g_Injector_B;
                    ExpStepAction.InjectDrain(null, volume, false);
                }
                catch (Exception e1)
                {
                    System.Diagnostics.Debug.WriteLine(e1.ToString());
                }

                ExpStepAction.CollectFrameSampleOrganReset(null);           //收集器出样针头上升复位
                ExpStepAction.InjectorPump(null, 2, false);
                ExpStepAction.CollectFrameExhibitOrganReset(null);          //收集器出样针头横移至废液槽(复位)
                ExpStepAction.CollectFrameSampleOrganDown(null);            //收集器出样针头下降
                ExpStepAction.InjectDrain(null, 0, false);                  //打出所有液体
                Thread.Sleep(15000);
                ExpStepAction.CollectFrameSampleOrganReset(null);           //收集器出样针头上升复位
                ExpStepAction.ResumeTempCollect(); //恢复温度采集

                UIOperator.SetComponetValidAndOpacity(DIV_BTN_OVER, true);
            });
        }
        //第五步 完成
        private void DIV_BTN_OVER_Click(object sender, RoutedEventArgs e)
        {
            DisableAllButton();
            UIOperator.SetComponetValidAndOpacity(DIV_BTN_FIRSTSTEP, true);
        }
        //第五步 倒吸再次
        private void DIV_BTN_SUCK_AGAIN_Click(object sender, RoutedEventArgs e)
        {
        //    if (!App.CheckMainframeStatus()) return;
        //    if (App.m_bExperimentRunning)
        //    {
        //        MessageBox.Show("实验中，无法操作！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return;
        //    }
        //    Task t = Task.Run(() =>
        //    {
        //        ExpStepAction.SuspendTempCollect();
        //        ExpStepAction.InjectorPump(null, 0.1, false);
        //        ExpStepAction.ResumeTempCollect(); //恢复温度采集

        //        DisableAllButton();
        //        UIOperator.SetComponetValidAndOpacity(DIV_BTN_PUSHWATER, true);
        //    });
        }
        //第六步 打液体
        private void DIV_BTN_PUSHWATER_Click(object sender, RoutedEventArgs e)
        {
        //    if (!App.CheckMainframeStatus()) return;
        //    if (App.m_bExperimentRunning)
        //    {
        //        MessageBox.Show("实验中，无法操作！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return;
        //    }
        //    Task t = Task.Run(() =>
        //    {
        //        ExpStepAction.SuspendTempCollect();
        //        ExpStepAction.InjectDrain(null, 0, false);       //打出所有液体
        //        ExpStepAction.ResumeTempCollect(); //恢复温度采集

        //        DisableAllButton();
        //        UIOperator.SetComponetValidAndOpacity(DIV_BTN_FIRSTSTEP, true);
        //    });

        }
        //体积校准 按钮 事件
        private void DIV_BTN_VOLUMECALIBRATION_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double K = 0; double B = 0;
            if (string.IsNullOrEmpty(DIV_TB_VOLUME_1.Text) || string.IsNullOrEmpty(DIV_TB_VOLUME_2.Text)
                || string.IsNullOrEmpty(DIV_TB_VOLUME_REALITY_1.Text) || string.IsNullOrEmpty(DIV_TB_VOLUME_REALITY_2.Text))
            {
                MessageBox.Show(App.m_LangPackage.TIP_CALI_ISNOT_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Formula.Calculate_KB_Ex((double)Double.Parse(DIV_TB_VOLUME_1.Text.ToString()),
                (double)Double.Parse(DIV_TB_VOLUME_2.Text.ToString()),
                (double)Double.Parse(DIV_TB_VOLUME_REALITY_1.Text.ToString()),
                (double)Double.Parse(DIV_TB_VOLUME_REALITY_2.Text.ToString()), ref K, ref B);

            App.m_ProfileUtils.WriteIniData("Injector", "K", K.ToString(), App.g_CalibrationFileName);
            App.m_ProfileUtils.WriteIniData("Injector", "B", B.ToString(), App.g_CalibrationFileName);
            App.g_Injector_K = K;
            App.g_Injector_B = B;
            DIV_TB_INJECTOR_K.Text = K.ToString();
            DIV_TB_INJECTOR_B.Text = B.ToString();

            DIV_TB_VOLUME_1.Text = string.Empty;
            DIV_TB_VOLUME_2.Text = string.Empty;
            DIV_TB_VOLUME_REALITY_1.Text = string.Empty;
            DIV_TB_VOLUME_REALITY_2.Text = string.Empty;

            MessageBox.Show(App.m_LangPackage.TIP_CALI_VOLUME_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
      
    }
}