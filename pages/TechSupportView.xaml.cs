using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using Pharmacy.INST.DissolutionClient.common;
using com.ccg.GeckoKit;
using Pharmacy.INST.CommonLib;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// TechSupportView.xaml 的交互逻辑
    /// </summary>
    public partial class TechSupportView : Page
    {
        public bool m_bSwivel_Start = false;      //转轴测试是否运行中，false：否；true：是

        public byte[] m_SendBuf = new byte[StaticParam.BUFFERSIZE];
        public byte[] m_RecvBuf = new byte[StaticParam.BUFFERSIZE];
        public TechSupportView()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        //窗口组件初始化
        private void InitializeComponentEx()
        {
            //加载溶出方法下拉框
            UIOperator.ComboBoxBinder(TSV_COMBO_DISSOLUTIONMETHODNAME, StaticParam.DissolutionMethod_Arr);
        }
        //初始化界面
        public void InitializeInterface()
        {
            TSV_DISSOLUTION_GROUPBOX.Header = App.m_LangPackage.TSV_DISSOLUTION_GROUPBOX;
            LB_TSV_COMBO_DISSOLUTIONMETHODNAME.Content = App.m_LangPackage.LB_TSV_COMBO_DISSOLUTIONMETHODNAME;
            TSV_BTN_CALIBRATION_RESET.Content = App.m_LangPackage.TSV_BTN_CALIBRATION_RESET;
            LB_TB_OPERATE_HEIGHT.Content = App.m_LangPackage.LB_TB_OPERATE_HEIGHT;
            LB_TB_REALITY_HEIGHT.Content = App.m_LangPackage.LB_TB_REALITY_HEIGHT;
            BTN_CONFIRM_HEIGHT.Content = App.m_LangPackage.BTN_CONFIRM_HEIGHT;
            LB_GROUP_1.Content = App.m_LangPackage.LB_GROUP_1;
            LB_GROUP_2.Content = App.m_LangPackage.LB_GROUP_2;
            BTN_SAMPLEPOINTDOWN.Content = App.m_LangPackage.BTN_SAMPLEPOINTDOWN;
            BTN_SAMPLEPOINTUP.Content = App.m_LangPackage.BTN_SAMPLEPOINTUP;
            BTN_SAVE_KB.Content = App.m_LangPackage.BTN_SAVE_KB;
            LB_TB_CONTAINER_MARK.Content = App.m_LangPackage.LB_TB_CONTAINER_MARK;
            TSV_BTN_SAMPLEVERIFY.Content = App.m_LangPackage.TSV_BTN_SAMPLEVERIFY;

            LB_DISSOLUTION_DU.Content = App.m_LangPackage.LB_DISSOLUTION_DU;
            LB_TB_SWIVEL_TEST.Content = App.m_LangPackage.LB_TB_SWIVEL_TEST;
            BTN_TEST_SWIVEL_START.Content = App.m_LangPackage.BTN_TEST_SWIVEL_START;
            BTN_TEST_SWIVEL_END.Content = App.m_LangPackage.BTN_TEST_SWIVEL_END;
            LB_TB_UPDOWN_TEST.Content = App.m_LangPackage.LB_TB_UPDOWN_TEST;
            LB_UNIT_CI_1.Content = App.m_LangPackage.LB_UNIT_CI_1;
            BTN_TEST_UPDOWN_START.Content = App.m_LangPackage.BTN_TEST_UPDOWN_START;
            BTN_TEST_UPDOWN_END.Content = App.m_LangPackage.BTN_TEST_UPDOWN_END;
            LB_TB_SAMPLE_POINT.Content = App.m_LangPackage.LB_TB_SAMPLE_POINT;
            LB_UNIT_CI_2.Content = App.m_LangPackage.LB_UNIT_CI_2;
            BTN_TEST_SAMPLE_POINT_START.Content = App.m_LangPackage.BTN_TEST_SAMPLE_POINT_START;
            BTN_TEST_SAMPLE_POINT_END.Content = App.m_LangPackage.BTN_TEST_SAMPLE_POINT_END;

            TSV_SAMPLE_GROUPBOX.Header = App.m_LangPackage.TSV_SAMPLE_GROUPBOX;
            LB_SAMPLE_DU.Content = App.m_LangPackage.LB_SAMPLE_DU;
            LB_TB_PUSHSOLID_TEST.Content = App.m_LangPackage.LB_TB_PUSHSOLID_TEST;
            BTN_TEST_PUSHSOLID_START.Content = App.m_LangPackage.BTN_TEST_PUSHSOLID_START;
            BTN_TEST_PUSHSOLID_END.Content = App.m_LangPackage.BTN_TEST_PUSHSOLID_END;
            BTN_TEST_PULLSOLID_START.Content = App.m_LangPackage.BTN_TEST_PULLSOLID_START;
            LB_TB_VALVE_TEST.Content = App.m_LangPackage.LB_TB_VALVE_TEST;
            LB_UNIT_HAO_1.Content = App.m_LangPackage.LB_UNIT_HAO_1;
            BTN_TEST_VALVE_START.Content = App.m_LangPackage.BTN_TEST_VALVE_START;
            BTN_TEST_VALVE_END.Content = App.m_LangPackage.BTN_TEST_VALVE_END;

            TSV_GATHER_GROUPBOX.Header = App.m_LangPackage.TSV_GATHER_GROUPBOX;
            LB_GATHER_DU.Content = App.m_LangPackage.LB_GATHER_DU;
            LB_TB_OUTPOINT_TEST.Content = App.m_LangPackage.LB_TB_OUTPOINT_TEST;
            LB_UNIT_CI_3.Content = App.m_LangPackage.LB_UNIT_CI_3;
            BTN_TEST_OUTPOINT_START.Content = App.m_LangPackage.BTN_TEST_OUTPOINT_START;
            BTN_TEST_OUTPOINT_END.Content = App.m_LangPackage.BTN_TEST_OUTPOINT_END;
            LB_TB_SUPPORTARMPOS_TEST.Content = App.m_LangPackage.LB_TB_SUPPORTARMPOS_TEST;
            LB_UNIT_HAO_2.Content = App.m_LangPackage.LB_UNIT_HAO_2;
            BTN_TEST_SUPPORTARMPOS_START.Content = App.m_LangPackage.BTN_TEST_SUPPORTARMPOS_START;
            BTN_TEST_SUPPORTARMPOS_END.Content = App.m_LangPackage.BTN_TEST_SUPPORTARMPOS_END;
            LB_TB_SUPPORTARM_TEST.Content = App.m_LangPackage.LB_TB_SUPPORTARM_TEST;
            LB_UNIT_CI_4.Content = App.m_LangPackage.LB_UNIT_CI_4;
            BTN_TEST_SUPPORTARM_START.Content = App.m_LangPackage.BTN_TEST_SUPPORTARM_START;
            BTN_TEST_SUPPORTARM_END.Content = App.m_LangPackage.BTN_TEST_SUPPORTARM_END;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(8), App.GetBehaviorRemark(8)));
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }
        #region 溶出仪测试
        //取样针下降
        private void BTN_SAMPLEPOINTDOWN_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error); ;
                return;
            }
            int pulsenum;
            try
            {
                double volume = double.Parse(TB_OPERATE_HEIGHT.Text.Trim());
                //add by scott 2022.11.2 小杯法
                if (!TSV_COMBO_DISSOLUTIONMETHODNAME.Text.Equals(StaticParam.DissolutionMethod_Arr[2])
                    )
                {
                    if (volume > 1000 || volume < 500) return;
                    pulsenum = Convert.ToInt32(volume * App.m_dlSamplePointPulseML);
                }
                else
                {
                    if (volume > 250 || volume < 100) return;
                    pulsenum = Convert.ToInt32(volume * App.m_dlSamplePointPulseML * StaticParam.RATIO_PML);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_PARAMS_INPUT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] arr = new string[3];
            byte hByte = (byte)((pulsenum >> 16) & 0x000000FF);
            byte mByte = (byte)((pulsenum >> 8) & 0x000000FF);
            byte lByte = (byte)(pulsenum & 0x000000FF);
            arr[0] = string.Format("{0:X2}", hByte);
            arr[1] = string.Format("{0:X2}", mByte);
            arr[2] = string.Format("{0:X2}", lByte);

            ExpStepAction.SuspendTempCollect();
            if (ExpStepAction.MainFrameInitialize())
            {
                if (BaseUtils.SendYKDatagram(String.Format(Datagram.V1_E1_DOWN, arr[0], arr[1], arr[2])))
                {
                    App.SetSystemStatus(String.Format("溶出仪取样针正在下降!"));
                }
            }
            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //取样针上升
        private void BTN_SAMPLEPOINTUP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();

            if (BaseUtils.SendYKDatagram(Datagram.V1_E1_UP))
            {
                App.SetSystemStatus("取样针上升!");
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //取样针暂停
        private void BTN_SAMPLEPOINTPAUSE_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            ExpStepAction.SuspendTempCollect();

            if (BaseUtils.SendYKDatagram(Datagram.V1_E1_STOP))
            {
                App.SetSystemStatus("取样针停止!");
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
       
        //溶出仪转轴测试旋转开始 按钮事件
        private void BTN_TEST_SWIVEL_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                int nSwivel = int.Parse(TB_SWIVEL_TEST.Text.Trim());
                if (nSwivel < 1 || nSwivel > StaticParam.ROTATION_MAX_SPEED)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_TSV_ROTATE_SPEED_LIMIT, StaticParam.ROTATION_MAX_SPEED), App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                ExpStepAction.SuspendTempCollect();
                ExpStepAction.Rotation(null, nSwivel,StaticParam.ElectricMotor.SIGNLE);
                if (App.m_bDoubleMotor)
                    ExpStepAction.Rotation(null, nSwivel, StaticParam.ElectricMotor.DOUBLE);
                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        
        //溶出仪转轴测试旋转结束 按钮事件
        private void BTN_TEST_SWIVEL_END_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();
            ExpStepAction.Rotation(null, 0, StaticParam.ElectricMotor.SIGNLE);
            //第二电机旋转停止
            if (App.m_bDoubleMotor)
                ExpStepAction.Rotation(null, 0, StaticParam.ElectricMotor.DOUBLE);
            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //溶出仪升降测试开始 按钮事件
        private void BTN_TEST_UPDOWN_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();

            ExpStepAction.LiftingDown();
            Thread.Sleep(20000);
            ExpStepAction.LiftingUp();

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //溶出仪升降测试结束 按钮事件
        private void BTN_TEST_UPDOWN_END_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();

            ExpStepAction.LiftingStop();

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }
        //取样针测试开始 按钮事件
        private void BTN_TEST_SAMPLE_POINT_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //取样针上下运动为一次
            int nTestTimes = 1;
            try
            {
                nTestTimes = int.Parse(TB_SAMPLE_POINT.Text.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (nTestTimes < 1 || nTestTimes > 10)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_TESTTIMES_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect();

                //首先复位
                Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
                int times = 0;
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                if (buffer[0x04] != 0x13)   //溶出仪取样针不在最高位
                {
                    //溶出仪初始化
                    if (BaseUtils.SendYKDatagram(Datagram.V1_E1_UP))
                    {
                        App.SetSystemStatus("溶出仪取样针复位指令成功发送!");
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                            if (buffer[0x04] == 0x13)
                            {
                                App.SetSystemStatus("溶出仪取样针已复位");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("溶出仪取样针复位超时失败");
                            return;
                        }
                    }
                }
                Thread.Sleep(2000);

                for (int i = 0; i < nTestTimes; i++)
                {
                    //下降
                    string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(300, StaticParam.V1_E1_BASE_VOLUME, StaticParam.V1_E1_BASE_PULSENUM);
                    if (BaseUtils.SendYKDatagram(String.Format(Datagram.V1_E1_DOWN, arr[0], arr[1], arr[2])))
                    {
                        App.SetSystemStatus("溶出仪取样针下降指令成功发送");
                        times = 0;
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                            if (buffer[0x04] == 0x14)
                            {
                                App.SetSystemStatus("溶出仪取样针已降到最低端");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("溶出仪取样针下降读取状态超时失败");
                            return;
                        }
                    }
                    //强制停两秒
                    Thread.Sleep(1000);
                    //上升
                    if (BaseUtils.SendYKDatagram(Datagram.V1_E1_UP))
                    {
                        App.SetSystemStatus("溶出仪取样针上升指令成功发送");
                        times = 0;
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                            if (buffer[0x04] == 0x13)
                            {
                                App.SetSystemStatus("溶出仪取样针已上升到最顶端");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("溶出仪取样针上升读取状态超时失败");
                            return;
                        }
                    }
                    Thread.Sleep(2000);
                }

                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });
        }
        //取样针测试结束 按钮事件
        private void BTN_TEST_SAMPLE_POINT_END_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();

            if (BaseUtils.SendYKDatagram(Datagram.V1_E1_STOP))
            {
                App.SetSystemStatus("溶出仪取样针运动停止");
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
        }

        private void BTN_CONFIRM_HEIGHT_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrEmpty(TB_OPERATE_HEIGHT.Text.Trim()) || String.IsNullOrEmpty(TB_REALITY_HEIGHT.Text.Trim()))
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_INPUT_REALHEIGHT_OPHEIGHT, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                if (String.IsNullOrEmpty(TB_OPERATE_HEIGHT_1.Text.Trim()) || String.IsNullOrEmpty(TB_REALITY_HEIGHT_1.Text.Trim()))
                {
                    TB_OPERATE_HEIGHT_1.Text = TB_OPERATE_HEIGHT.Text.Trim();
                    TB_REALITY_HEIGHT_1.Text = TB_REALITY_HEIGHT.Text.Trim();
                }
                else
                {
                    TB_OPERATE_HEIGHT_2.Text = TB_OPERATE_HEIGHT.Text.Trim();
                    TB_REALITY_HEIGHT_2.Text = TB_REALITY_HEIGHT.Text.Trim();
                }
                TB_OPERATE_HEIGHT.Text = string.Empty;
                TB_REALITY_HEIGHT.Text = string.Empty;
            }
           
        }
        private void BTN_SAVE_KB_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double K = 0; double B = 0;
            Formula.Calculate_KB((double)Double.Parse(TB_OPERATE_HEIGHT_1.Text.ToString()),
                (double)Double.Parse(TB_OPERATE_HEIGHT_2.Text.ToString()),
                (double)Double.Parse(TB_REALITY_HEIGHT_1.Text.ToString()),
                (double)Double.Parse(TB_REALITY_HEIGHT_2.Text.ToString()), ref K, ref B);


            if (!TSV_COMBO_DISSOLUTIONMETHODNAME.Text.Equals(StaticParam.DissolutionMethod_Arr[2])
                )
            {
                App.m_ProfileUtils.WriteIniData("SamplePoint", "K", K.ToString(), App.g_CalibrationFileName);
                App.m_ProfileUtils.WriteIniData("SamplePoint", "B", B.ToString(), App.g_CalibrationFileName);
                App.g_SamplePoint_K = K;
                App.g_SamplePoint_B = B;
            }
            else
            {
                App.m_ProfileUtils.WriteIniData("SamplePoint", "K_S", K.ToString(), App.g_CalibrationFileName);
                App.m_ProfileUtils.WriteIniData("SamplePoint", "B_S", B.ToString(), App.g_CalibrationFileName);
                App.g_SamplePoint_K_S = K;
                App.g_SamplePoint_B_S = B;
            }

            

            TB_OPERATE_HEIGHT_1.Text = string.Empty;
            TB_REALITY_HEIGHT_1.Text = string.Empty;
            TB_OPERATE_HEIGHT_2.Text = string.Empty;
            TB_REALITY_HEIGHT_2.Text = string.Empty;

            MessageBox.Show(App.m_LangPackage.TIP_TSV_SAVE_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region 取样器测试

        #endregion
        //取液测试开始
        private void BTN_TEST_PUSHSOLID_START_Click(object sender, RoutedEventArgs e)
        {
            string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(25, StaticParam.BASE_VOLUME, StaticParam.BASE_PULSENUM);
            string datagrame = String.Format(Datagram.V2_E2_SUCK, arr[0], arr[1], arr[2]);

            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                double volume = 0;
                try
                {
                    TB_PUSHSOLID_TEST.Dispatcher.Invoke(new Action(delegate
                    {
                        volume = int.Parse(TB_PUSHSOLID_TEST.Text.ToString().Trim());
                        volume = volume * App.g_Injector_K + App.g_Injector_B;
                    }));

                    //if (volume > 20) return;
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.ToString());
                    return;
                }
                ExpStepAction.SuspendTempCollect();
          

                //首先复位
                Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
                int times = 0;
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                if (buffer[0x05] != 0x24)
                {
                    //取样器初始化
                    if (BaseUtils.SendYKDatagram(Datagram.V2_E2_DOWN))
                    {
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                            if (buffer[0x05] == 0x24)
                            {
                                App.SetSystemStatus("取样器初始化完成");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("取样器初始化超时失败");
                            return;
                        }
                    }
                }
                Thread.Sleep(2000);

                string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(volume, StaticParam.BASE_VOLUME, StaticParam.BASE_PULSENUM);
                string datagrame = String.Format(Datagram.V2_E2_SUCK, arr[0], arr[1], arr[2]);

                if (BaseUtils.SendYKDatagram(datagrame))
                {
                    App.SetSystemStatus("取液25ml");
                }


                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });
        }
        //取液测试停止
        private void BTN_TEST_PUSHSOLID_END_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();

            if (BaseUtils.SendYKDatagram(Datagram.V2_E2_STOP))
            {
                App.SetSystemStatus("注射器停止");
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
            return;
        }
        //出液测试开始
        private void BTN_TEST_PULLSOLID_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                double volume = 0;
                try
                {
                    TB_PUSHSOLID_TEST.Dispatcher.Invoke(new Action(delegate
                    {
                        volume = int.Parse(TB_PUSHSOLID_TEST.Text.ToString().Trim());
                        volume = volume * App.g_Injector_K + App.g_Injector_B;
                    }));

                    //if (volume > 20) return;
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.ToString());
                    return;
                }
                string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(volume, StaticParam.BASE_VOLUME, StaticParam.BASE_PULSENUM);
                string datagrame = String.Format(Datagram.V2_E2_OUT, arr[0], arr[1], arr[2]);
                ExpStepAction.SuspendTempCollect();

                if (BaseUtils.SendYKDatagram(datagrame))
                {
                    App.SetSystemStatus("出液25ml");
                }

                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });
        }
        //阀体测试开始
        private void BTN_TEST_VALVE_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int nPos = 1;
            try
            {
                nPos = int.Parse(TB_VALVE_TEST.Text.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (nPos < 1 || nPos > 4)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_VALVE_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect();
    
                //首先复位
                Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
                int times = 0;
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                if (buffer[0x04] != 0x14)   //不在复位点
                {
                    if (BaseUtils.SendYKDatagram(Datagram.V2_E1_RESET))
                    {
                        App.SetSystemStatus("阀复位指令成功发送");
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                            if (buffer[0x04] == 0x14)
                            {
                                App.SetSystemStatus("阀已复位");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("阀复位超时失败");
                            return;
                        }
                    }
                }
                Thread.Sleep(2000);

                string str = String.Format(Datagram.V2_E1_ROTATE, String.Format("{0:X2}", nPos));
                if (BaseUtils.SendYKDatagram(str))
                {
                    App.SetSystemStatus(String.Format("阀运动到{0}号计数位", 18));
                }

                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });

            }
        //阀体测试结束
        private void BTN_TEST_VALVE_END_Click(object sender, RoutedEventArgs e)
        {

        }
        //出样针测试
        private void BTN_TEST_OUTPOINT_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //出样针上下运动为一次
            int nTestTimes = 1;
            try
            {
                nTestTimes = int.Parse(TB_OUTPOINT_TEST.Text.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (nTestTimes < 1 || nTestTimes > 10)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_TEST_TIMES_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect();

                //首先复位
                Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
                int times = 0;
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                if (buffer[0x05] != 0x23)   //出样针不在最高位
                {
                    //出样针初始化
                    if (BaseUtils.SendYKDatagram(Datagram.V3_E2_UP))
                    {
                        App.SetSystemStatus("收集器出样针复位指令成功发送");
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                            if (buffer[0x05] == 0x23)
                            {
                                App.SetSystemStatus("收集器出样针已复位");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("收集器出样针复位超时失败");
                            return;
                        }
                    }
                }
                Thread.Sleep(2000);

                for (int i = 0; i < nTestTimes; i++)
                {
                    //下降
                    if (BaseUtils.SendYKDatagram(Datagram.V3_E2_DOWN))
                    {
                        App.SetSystemStatus("收集器出样针下降指令成功发送");
                        times = 0;
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                            if (buffer[0x05] == 0x24)
                            {
                                App.SetSystemStatus("收集器出样针已降到最低端");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("收集器出样针下降读取状态超时失败");
                            return;
                        }
                    }
                    //强制停两秒
                    Thread.Sleep(1000);
                    //上升
                    if (BaseUtils.SendYKDatagram(Datagram.V3_E2_UP))
                    {
                        App.SetSystemStatus("收集器出样针上升指令成功发送");
                        times = 0;
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                            if (buffer[0x05] == 0x23)
                            {
                                App.SetSystemStatus("收集器出样针已上升到最顶端");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("收集器出样针上升读取状态超时失败");
                            return;
                        }
                    }
                    Thread.Sleep(2000);
                }

                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });
        }
        //出样针测试停止
        private void BTN_TEST_OUTPOINT_END_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExpStepAction.SuspendTempCollect();

            if (BaseUtils.SendYKDatagram(Datagram.V3_E2_STOP))
            {
                App.SetSystemStatus("收集器出样针停止");
            }

            ExpStepAction.ResumeTempCollect(); //恢复温度采集
            return;
        }

        private void BTN_TEST_SUPPORTARMPOS_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int nPos = 1;
            try
            {
                nPos = int.Parse(TB_SUPPORTARMPOS_TEST.Text.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (nPos < 1 || nPos > 20)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_SAMPLING_TEST_TIMES_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect();

                //首先复位
                Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
                int times = 0;
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                if (buffer[0x05] != 0x23)   //出样针不在最高位
                {
                    //出样针初始化
                    if (BaseUtils.SendYKDatagram(Datagram.V3_E2_UP))
                    {
                        App.SetSystemStatus("收集器出样针复位指令成功发送");
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                            if (buffer[0x05] == 0x23)
                            {
                                App.SetSystemStatus("收集器出样针已复位");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("收集器出样针复位超时失败");
                            return;
                        }
                    }
                }
                Thread.Sleep(2000);

                //下降
                if (BaseUtils.SendYKDatagram(Datagram.V3_E1_RESET))
                {
                    App.SetSystemStatus("收集器支臂复位指令成功发送");
                    times = 0;
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                        if (buffer[0x04] == 0x13)
                        {
                            App.SetSystemStatus("收集器支臂已运行到最左端");
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                    if (times == StaticParam.TIMEOUT_TIMES)
                    {
                        App.SetSystemStatus("收集器支臂读取状态超时失败");
                        return;
                    }
                }
                
                string str = String.Format(Datagram.V3_E1_MOVE, String.Format("{0:X2}", nPos));
                if (BaseUtils.SendYKDatagram(str))
                {
                    App.SetSystemStatus(String.Format("收集器支臂向右运动{0}计数位",nPos));
                }

                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });

        }

        private void BTN_TEST_SUPPORTARMPOS_END_Click(object sender, RoutedEventArgs e)
        {
            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect();
                ExpStepAction.CollectFrameExhibitOrganReset(null);          //收集器出样针头横移至废液槽(复位)
                ExpStepAction.ResumeTempCollect();
            });
        }

        private void BTN_TEST_SUPPORTARM_START_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int nTestTimes = 1;
            try
            {
                nTestTimes = int.Parse(TB_SUPPORTARM_TEST.Text.Trim());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (nTestTimes < 1 || nTestTimes > 10)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_TEST_TIMES_LIMIT, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect();
               
                //首先复位
                Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
                int times = 0;
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                if (buffer[0x05] != 0x23)   //出样针不在最高位
                {
                    //出样针初始化
                    if (BaseUtils.SendYKDatagram(Datagram.V3_E2_UP))
                    {
                        App.SetSystemStatus("收集器出样针复位指令成功发送");
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                            if (buffer[0x05] == 0x23)
                            {
                                App.SetSystemStatus("收集器出样针已复位");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("收集器出样针复位超时失败");
                            return;
                        }
                    }
                }
                Thread.Sleep(2000);


                if (BaseUtils.SendYKDatagram(Datagram.V3_E1_RESET))
                {
                    App.SetSystemStatus("收集器支臂复位指令成功发送");
                    times = 0;
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                        if (buffer[0x04] == 0x13)
                        {
                            App.SetSystemStatus("收集器支臂已支行到最左端");
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                    if (times == StaticParam.TIMEOUT_TIMES)
                    {
                        App.SetSystemStatus("收集器支臂读取状态超时失败");
                        return;
                    }
                }

                for (int i = 0; i < nTestTimes; i++)
                {
                    string str = String.Format(Datagram.V3_E1_MOVE, String.Format("{0:X2}", 18));
                    if (BaseUtils.SendYKDatagram(str))
                    {
                        App.SetSystemStatus(String.Format("收集器支臂向右运动{0}计数位", 18));
                    }
                    Thread.Sleep(8000);
                    if (BaseUtils.SendYKDatagram(Datagram.V3_E1_RESET))
                    {
                        App.SetSystemStatus("收集器支臂复位指令成功发送");
                        times = 0;
                        while (times++ < StaticParam.TIMEOUT_TIMES)
                        {
                            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                            if (buffer[0x04] == 0x13)
                            {
                                App.SetSystemStatus("收集器支臂已支行到最左端");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        if (times == StaticParam.TIMEOUT_TIMES)
                        {
                            App.SetSystemStatus("收集器支臂读取状态超时失败");
                            return;
                        }
                    }
                }
                ExpStepAction.ResumeTempCollect(); //恢复温度采集
            });
        }
        //验证校准结果
        private void TSV_BTN_SAMPLEVERIFY_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.GetMainWindow().LB_MAIN_STATUS.Content.Equals(StaticParam.Startup_status_Arr[1]))
            {
                MessageBox.Show(App.m_LangPackage.TIP_MAINBOARD_COMM_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //add by scott 2022.11.2 小杯法
            if (TSV_COMBO_DISSOLUTIONMETHODNAME.Text == null)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_UNSELECTED_METHOD, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            double volume;
            try
            {
                volume = double.Parse(TB_CONTAINER_MARK.Text.Trim());
                //根据溶出方法设置溶媒体积上下限
                int nUpperlimit = 1000;
                int nLowerLimit = 500;
                if (TSV_COMBO_DISSOLUTIONMETHODNAME.Text.Equals(StaticParam.DissolutionMethod_Arr[2])
                    ) //小杯法
                {
                    nUpperlimit = 250;
                    nLowerLimit = 100;
                }
                if (volume < nLowerLimit || volume > nUpperlimit) 
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_TSV_SOLVENT_VOLUME_OUT_LIMIT, nLowerLimit, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_PARAMS_INPUT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            string methodname = TSV_COMBO_DISSOLUTIONMETHODNAME.Text;
            Task t = Task.Run(() =>
            {
                if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.SamplePointDown(null, volume, methodname);
                    ExpStepAction.ResumeTempCollect();
                }
            });
        }
        //取样针校准值重置
        private void TSV_BTN_CALIBRATION_RESET_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TSV_COMBO_DISSOLUTIONMETHODNAME.Text == null)
            {
                MessageBox.Show(App.m_LangPackage.TIP_TSV_METHOD_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!TSV_COMBO_DISSOLUTIONMETHODNAME.Text.Equals(StaticParam.DissolutionMethod_Arr[2])
                )
            {
                App.m_ProfileUtils.WriteIniData("SamplePoint", "K", "1", App.g_CalibrationFileName);
                App.m_ProfileUtils.WriteIniData("SamplePoint", "B", "0", App.g_CalibrationFileName);
                App.g_SamplePoint_K = 1;
                App.g_SamplePoint_B = 0;
            }
            else
            {
                App.m_ProfileUtils.WriteIniData("SamplePoint", "K_S", "1", App.g_CalibrationFileName);
                App.m_ProfileUtils.WriteIniData("SamplePoint", "B_S", "0", App.g_CalibrationFileName);
                App.g_SamplePoint_K_S = 1;
                App.g_SamplePoint_B_S = 0;
            }

           
            MessageBox.Show(App.m_LangPackage.TIP_TSV_CALI_RESET_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
