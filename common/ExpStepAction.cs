using System;
using System.Threading;
using Pharmacy.INST.DissolutionClient.pages;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.common
{
    public static class ExpStepAction
    {
        #region 设备自行初始化
        //设备初始化，设备开机自动初始化
        public static bool Self_Initialize(MainView mainView)
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            BaseUtils.TurnOnRedLight(mainView.IMG_L_INIT);
            //查询溶出仪状态
            while (times++ < 3)
            {
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V_STATUS);
                if (buffer != null && buffer[0x03] == 0x01)
                {
                    App.SetSystemStatus("初始化完成");
                    BaseUtils.TurnOnGreenLight(mainView.IMG_L_INIT);
                    return true;
                }
                else if (buffer != null && buffer[0x03] == 0x00)
                {
                    if (Initialize())
                    {
                        App.SetSystemStatus("软件辅助初始化完成");
                        BaseUtils.TurnOnGreenLight(mainView.IMG_L_INIT);
                        return true;
                    }
                }
                Thread.Sleep(2000);
            }
            Thread.Sleep(1000);
            App.SetSystemStatus("初始化失败,请检查");
            BaseUtils.TurnOnGreenLight(mainView.IMG_L_INIT);
            return false;
        }
        #endregion
        #region 上位机单步初始化
        //设备初始化
        public static bool Initialize()
        {
            if (App.m_bSampleBeep)
            {
                ExpStepAction.Beep(false);    //关闭蜂鸣器
                Thread.Sleep(500);
            }
            //投药装置初始化
            if (App.m_bPutPillEnable) PillReset(null);
            //搅拌桨停止旋转
            Rotation(null, 0, StaticParam.ElectricMotor.SIGNLE);
            //第二电机旋转停止
            if (App.m_bDoubleMotor)
            {
                Thread.Sleep(500);
                Rotation(null, 0, StaticParam.ElectricMotor.DOUBLE);
            }
            //本体初始化
            if (App.m_bSamplePointEnable)
            {
                if (!MainFrameInitialize())
                {
                    App.SetSystemStatus("溶出仪本体初始化失败");
                    return false;
                }
            }
            if (App.m_bWholeModeEnable)
            {
                //收集器取样机构初始化
                if (!CollectFrameSampleOrganInitialize())
                {
                    App.SetSystemStatus("收集器取样机构初始化失败");
                    return false;
                }
                //收集器横臂运动机构初始化
                if (!CollectFrameExhibitOrganInitialize())
                {
                    App.SetSystemStatus("收集器横臂运动机构初始化失败");
                    return false;
                }
                //取样器四通阀初始化
                if (!SampleFrameValveInitialize())
                {
                    App.SetSystemStatus("取样器四通阀初始化失败");
                    return false;
                }
                //取样器注射器初始化
                if (!SampleFrameInjectorInitialize())
                {
                    App.SetSystemStatus("取样器注射器初始化失败");
                    return false;
                }
            }
            return true;
        }
        //溶出仪初始化
        public static bool MainFrameInitialize()
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            //查询溶出仪状态
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
            if (buffer == null) return false;
            if (buffer[0x04] != 0x13)   //溶出仪取样针不在最高位
            {
                //溶出仪初始化
                if (BaseUtils.SendYKDatagram(Datagram.V1_E1_UP))
                {
                    App.SetSystemStatus("溶出仪取样针初始化命令发功发送");
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                        if (buffer != null && buffer[0x04] == 0x13)
                        {
                            App.SetSystemStatus("溶出仪取样针初始化完成");
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                    App.SetSystemStatus("溶出仪取样针初始化超时失败");
                    return false;
                }
            }
            else
                App.SetSystemStatus("溶出仪取样针初始化完成");
            return true;
        }
        //取样器四通阀初始化
        public static bool SampleFrameValveInitialize()
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
            if (buffer == null) return false;
            if (buffer[0x04] != 0x14)
            {
                //取样器初始化
                if (BaseUtils.SendYKDatagram(Datagram.V2_E1_RESET))
                {
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                        if (buffer != null && buffer[0x04] == 0x14)
                        {
                            App.SetSystemStatus("取样器四通阀初始化完成");
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                    App.SetSystemStatus("取样器四通阀初始化超时失败");
                    return false;
                }
            }
            else
                App.SetSystemStatus("取样器四通阀初始化完成");
            return true;
        }
        //取样注射器初始化
        public static bool SampleFrameInjectorInitialize()
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
            if (buffer == null) return false;
            if (buffer[0x05] != 0x24)
            {
                //取样器初始化
                if (BaseUtils.SendYKDatagram(Datagram.V2_E2_DOWN))
                {
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                        if (buffer != null && buffer[0x05] == 0x24)
                        {
                            App.SetSystemStatus("取样注射器初始化完成");
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                    App.SetSystemStatus("取样注射器初始化超时失败");
                    return false;
                }
            }
            else
                App.SetSystemStatus("取样注射器初始化完成");
            return true;
        }
        //收集器取样机构初始化
        public static bool CollectFrameSampleOrganInitialize()
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            //查询收集器状态
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
            if (buffer == null) return false;
            if (buffer[0x05] != 0x23)
            {
                if (BaseUtils.SendYKDatagram(Datagram.V3_E2_UP))
                {
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                        if (buffer != null && buffer[0x05] == 0x23)
                        {
                            App.SetSystemStatus("收集器取样针初始化完成");
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                    App.SetSystemStatus("收集器取样机构初始化超时失败");
                    return false;
                }
            }
            else
                App.SetSystemStatus("收集器取样针初始化完成");

            return true;
        }
        //收集器投药装置初始化
        public static bool CollectFrameExhibitOrganInitialize()
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
            if (buffer == null) return false;
            if (buffer[0x04] != 0x13)
            {
                if (BaseUtils.SendYKDatagram(Datagram.V3_E1_RESET))
                {
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                        if (buffer != null && buffer[0x04] == 0x13)
                        {
                            App.SetSystemStatus("收集器取样针处于废液槽位");
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                    App.SetSystemStatus("收集器投药机构初始化超时失败");
                    return false;
                }
            }
            else
                App.SetSystemStatus("收集器取样针处于废液槽位");
            return true;
        }
        public static bool InitializeVerify()
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            try
            {
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                if (buffer == null) return false;
                byte v1_status = buffer[0x04];
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V2_E_STATUS);
                if (buffer == null) return false;
                byte v2_status = buffer[0x05];
                buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                if (buffer == null) return false;
                byte v3_status_l = buffer[0x04];
                byte v3_status_h = buffer[0x05];

                if (v1_status == 0x13 && v2_status == 0x24 && v3_status_l == 0x13 && v3_status_h == 0x23)
                {
                    App.SetSystemStatus("设备初始化完成");
                    return true;
                }
            }
            catch (Exception e)
            {
                App.WriteSystemLog("设备初始化时发生错误" + e.ToString());
            }
            return false;
        }
        #endregion

        //挂起温度采集
        public static void SuspendTempCollect()
        {
            if (App.m_bTempSurvey)
            {
                App.m_bTempSurveyThreadRunning = false;
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(3), App.GetBehavior(107),App.GetBehaviorRemark(107)));
                if (App.m_nTempSuspAndResuDelay > 0)
                    Thread.Sleep(App.m_nTempSuspAndResuDelay);
            }
        }
        //恢复温度采集
        public static void ResumeTempCollect()
        {
            if (App.m_bTempSurvey)
            {
                App.m_bTempSurveyThreadRunning = true;
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(106),App.GetBehaviorRemark(106)));
                if (App.m_nTempSuspAndResuDelay > 0)
                    Thread.Sleep(App.m_nTempSuspAndResuDelay);
            }
        }
        //开机回应
        public static bool Echo()
        {
            byte[] buf = BaseUtils.RecvStatusDatagram(Datagram.W_ECHO);
            if (buf == null)
                return false;
            return true;
        }
        //挂起小杯温度采集
        public static void SuspendCupTempCollect()
        {
            if (App.m_bTempSurvey)
            {
                App.m_bExpCupSurveryOn = false;
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(3), App.GetBehavior(109), App.GetBehaviorRemark(109)));
                if (App.m_nTempSuspAndResuDelay > 0)
                    Thread.Sleep(App.m_nTempSuspAndResuDelay);
            }
        }
        //恢复小杯温度采集
        public static void ResumeCupTempCollect()
        {
            if (App.m_bTempSurvey)
            {
                App.m_bExpCupSurveryOn = true;
                App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(3), App.GetBehavior(108), App.GetBehaviorRemark(108)));
                if (App.m_nTempSuspAndResuDelay > 0)
                    Thread.Sleep(App.m_nTempSuspAndResuDelay);
            }
        }
        //升降柱上升
        public static void LiftingUp()
        {
            if (BaseUtils.SendYKDatagram(Datagram.L_LIFTING_UP))
            {
                App.SetSystemStatus("升降柱上升");
            }
        }
        //升降柱停止
        public static void LiftingStop()
        {
            if (BaseUtils.SendYKDatagram(Datagram.L_LIFTING_STOP))
            {
                App.SetSystemStatus("升降柱停止");
            }
        }
        //升降柱下降
        public static void LiftingDown()
        {
            if (BaseUtils.SendYKDatagram(Datagram.L_LIFTING_DOWN))
            {
                App.SetSystemStatus("升降柱下降");
            }
        }
        //投药
        public static void PutPill(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_WAITPUTPILL);
            if (BaseUtils.SendYKDatagram(Datagram.H_PILL_PUT))
            {
                App.SetSystemStatus("开始投药");
                Thread.Sleep(App.m_nPutPillTime / 2);
            }
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_WAITPUTPILL);
        }
        //投药复位
        public static void PillReset(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_WAITPUTPILL);
            if (BaseUtils.SendYKDatagram(Datagram.H_PILL_RESET))
            {
                App.SetSystemStatus("投药机构复位");
                Thread.Sleep(App.m_nPutPillTime / 2);
            }
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_WAITPUTPILL);
        }
        /// <summary>
        /// 转桨以一定的转速进行旋转
        /// 0：停止；1-300为可设定的转速
        /// </summary>
        /// <param name="mainView"></param>
        /// <param name="nSwivel"></param>
        /// <param name="electricMotor"></param>
        public static void Rotation(MainView mainView, int nSwivel, StaticParam.ElectricMotor electricMotor)
        {
            try
            {
                if (nSwivel < 1)
                {
                    if (electricMotor == StaticParam.ElectricMotor.SIGNLE)
                    {
                        if (BaseUtils.SendYKDatagram(Datagram.V1_E2_STOP))
                        {
                            App.SetSystemStatus("前排电机搅拌桨停止");
                            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_IMPELLERTURN);
                        }
                    }
                    if (electricMotor == StaticParam.ElectricMotor.DOUBLE)
                    {
                        if (BaseUtils.SendYKDatagramNoConfirm(Datagram.V1_E3_STOP))
                        {
                            App.SetSystemStatus("后排电机搅拌桨停止");
                            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_IMPELLERTURN);
                        }
                    }

                }
                else if (nSwivel >= 1 && nSwivel <= StaticParam.ROTATION_MAX_SPEED)
                {
                    if (electricMotor == StaticParam.ElectricMotor.SIGNLE)
                    {
                        if (BaseUtils.SendYKDatagram(String.Format(Datagram.V1_E2_ROTATION, CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[1], CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[0])))
                        {
                            App.SetSystemStatus(string.Format("前排电机搅拌桨以{0}rpm开始旋转", nSwivel));
                            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_IMPELLERTURN);
                        }
                    }
                    if (electricMotor == StaticParam.ElectricMotor.DOUBLE)
                    {
                        if (BaseUtils.SendYKDatagramNoConfirm(String.Format(Datagram.V1_E3_ROTATION, CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[1], CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[0])))
                        {
                            App.SetSystemStatus(string.Format("后排电机搅拌桨以{0}rpm开始旋转", nSwivel));
                            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_IMPELLERTURN);
                        }
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 溶出仪取样针复位（回到最高位）
        /// </summary>
        public static bool SamplePointReset(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_DISSOLUTIONWORK);
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            if (buffer == null) return false;
            int times = 0;
            if (BaseUtils.SendYKDatagram(Datagram.V1_E1_UP))
            {
                App.SetSystemStatus("溶出仪复位命令发功发送");
                while (times++ < StaticParam.TIMEOUT_TIMES)
                {
                    buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                    if (buffer != null && buffer[0x04] == 0x13)
                    {
                        App.SetSystemStatus("溶出仪复位完成【最高位】");
                        Thread.Sleep(1000);
                        if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_DISSOLUTIONWORK);
                        return true;
                    }
                    Thread.Sleep(2000);
                }
            }
            App.SetSystemStatus("溶出仪复位超时失败");
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_DISSOLUTIONWORK);
            return false;
        }
        public static void SamplePointStop(MainView mainView)
        {
            if (BaseUtils.SendYKDatagram(Datagram.V1_E1_STOP))
            {
                App.SetSystemStatus("溶出仪取样针停止命令发功发送");
            }
        }
        /// 溶出仪取样针下降到指定容量刻度位置
        /// </summary>
        /// <param name="mainView"></param>
        /// <param name="postion"></param>
        public static void SamplePointDown(MainView mainView, double position, string methodname)
        {
            double realVolume;
            int pulsenum;
            if (!methodname.Equals(StaticParam.DissolutionMethod_Arr[2])
               )
            {
                realVolume = position * App.g_SamplePoint_K + App.g_SamplePoint_B;
                pulsenum = Convert.ToInt32(realVolume * App.m_dlSamplePointPulseML);
            }
            else
            {
                realVolume = position * App.g_SamplePoint_K_S + App.g_SamplePoint_B_S;
                pulsenum = Convert.ToInt32(realVolume * App.m_dlSamplePointPulseML * StaticParam.RATIO_PML);
            }
            
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
            App.SetSystemStatus(String.Format("检测溶出仪取样针位置"));
            if (buffer == null)
            {
                App.SetSystemStatus(String.Format("检测溶出仪取样针位置指令失败"));
                return;
            }
            if (buffer[0x04] == 0x13)   //溶出仪取样针在最高位
            {
                if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_DISSOLUTIONWORK);
                int times = 0;
                string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(pulsenum);
                if (BaseUtils.SendYKDatagram(String.Format(Datagram.V1_E1_DOWN, arr[0], arr[1], arr[2])))
                {
                    App.SetSystemStatus(String.Format("溶出仪取样针正在下降"));
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V1_E_STATUS);
                        if (buffer != null && buffer[0x04] == 0x10)
                        {
                            App.SetSystemStatus(String.Format("溶出仪取样针已降到{0}处", position));
                            Thread.Sleep(1000);             //延迟用于动作执行  
                            break;
                        }
                        App.Pause();
                    }
                    if (times == StaticParam.TIMEOUT_TIMES)
                    {
                        App.SetSystemStatus("溶出仪取样针下降读取状态超时失败");
                        return;
                    }
                }
                if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_DISSOLUTIONWORK);
            }
        }
        //换向阀复位
        public static void ValveReset(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_SAMPLINGWORK);
            if (BaseUtils.SendYKDatagram(Datagram.V2_E1_RESET))
            {
                App.SetSystemStatus("四通阀复位");
                Thread.Sleep(App.m_nValveStepTime * 3);//延迟用于动作执行,按最大步数计算
            }
            App.Pause();
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_SAMPLINGWORK);
        }
        //换向阀转到指定序号位置
        public static void ValveRotation(MainView mainView, int valveid, string desc)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_SAMPLINGWORK);
            string strDatagram = String.Format(Datagram.V2_E1_ROTATE, CommonLib.CommonLib.IntToHexByte(valveid));
            if (BaseUtils.SendYKDatagram(strDatagram))
            {
                App.SetSystemStatus(String.Format("四通阀运动{0}步至{1}", valveid, desc));
                Thread.Sleep(valveid * App.m_nValveStepTime);
            }
            App.Pause();
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_SAMPLINGWORK);
        }
        //注射器取指定容量的液体
        public static void InjectorPump(MainView mainView, double volume, bool expmode)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_SAMPLINGWORK);
            string datagrame = String.Format(Datagram.V2_E2_SUCK, "FF", "FF", "FF");
            string msg = "注射器吸液至上限位";
            double dlRealVolume = volume * App.g_Injector_K + App.g_Injector_B;
            if (!expmode) dlRealVolume = volume;                                //校准模式
            if (volume != 0)
            {
                string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(dlRealVolume, StaticParam.BASE_VOLUME, StaticParam.BASE_PULSENUM);
                msg = String.Format("注射器取液{0}mL", volume);
                datagrame = String.Format(Datagram.V2_E2_SUCK, arr[0], arr[1], arr[2]);
            }
            if (BaseUtils.SendYKDatagram(datagrame))
            {
                App.SetSystemStatus(msg);
            }
            if (volume == 0)
            {
                Thread.Sleep(25 * App.m_nInjectorMLTime);
            }
            else
            {
                Thread.Sleep(volume < 1 ? App.m_nInjectorMLTime : (int)volume * App.m_nInjectorMLTime);
            }
           
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_SAMPLINGWORK);
        }
        //注射器打出指定容量的液体
        public static void InjectDrain(MainView mainView, double volume, bool expmode)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_SAMPLINGWORK);
            string datagrame = Datagram.V2_E2_DOWN;
            string msg = "注射器打出所有液体";            //volume=0时，为打出最大量程液体
            double dlRealVolume = volume * App.g_Injector_K + App.g_Injector_B;
            if (!expmode) dlRealVolume = volume;                                //校准模式
            if (volume != 0)
            {
                msg = String.Format("注射器打出{0}mL液体", volume);
                string[] arr = CommonLib.CommonLib.IntToHex3ByteArr(dlRealVolume, StaticParam.BASE_VOLUME, StaticParam.BASE_PULSENUM);
                datagrame = String.Format(Datagram.V2_E2_OUT, arr[0], arr[1], arr[2]);
            }
            if (BaseUtils.SendYKDatagram(datagrame))
            {
                App.SetSystemStatus(string.Format(msg, volume));
            }
            Thread.Sleep(volume == 0 ? (int)App.g_CurrentInjectorSampleVolume * App.m_nInjectorMLTime : (int)volume * App.m_nInjectorMLTime);
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_SAMPLINGWORK);
        }
        //收集器出样针上升复位（最高位）
        public static void CollectFrameSampleOrganReset(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_COLLECTORWORK);
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            //查询收集器状态
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
            if (buffer == null) return;
            if (buffer[0x05] != 0x23)
            {
                if (BaseUtils.SendYKDatagram(Datagram.V3_E2_UP))
                {
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                        if (buffer == null) return;
                        if (buffer != null && buffer[0x05] == 0x23)
                        {
                            App.SetSystemStatus("收集器取样针上升完成");
                            Thread.Sleep(1000);
                            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_COLLECTORWORK);
                            return;
                        }
                        Thread.Sleep(1000);
                    }
                    App.SetSystemStatus("收集器取样机构初始化超时失败");
                    return;
                }
            }
            else
                App.SetSystemStatus("收集器取样针初始化完成");

        }
        //收集器出样针下降
        public static void CollectFrameSampleOrganDown(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_COLLECTORWORK);
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            if (buffer == null) return;
            int times = 0;
            if (BaseUtils.SendYKDatagram(Datagram.V3_E2_DOWN))
            {
                App.SetSystemStatus("收集器出样针下降指令成功发送");
                times = 0;
                while (times++ < StaticParam.TIMEOUT_TIMES)
                {
                    buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                    if (buffer == null) return;
                    if (buffer != null && buffer[0x05] == 0x24)
                    {
                        App.SetSystemStatus("收集器出样针下降到位");
                        Thread.Sleep(1000);
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_COLLECTORWORK);
        }
        //收集器横臂复位
        public static void CollectFrameExhibitOrganReset(MainView mainView)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_COLLECTORWORK);
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            int times = 0;
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
            if (buffer == null) return;
            if (buffer[0x05] == 0x23)
            {
                if (BaseUtils.SendYKDatagram(Datagram.V3_E1_RESET))
                {
                    App.SetSystemStatus("收集器支臂复位指令成功发送");
                    times = 0;
                    while (times++ < StaticParam.TIMEOUT_TIMES)
                    {
                        buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
                        if (buffer == null) return;
                        if (buffer != null && buffer[0x04] == 0x13)
                        {
                            App.SetSystemStatus("收集器支臂已复位");
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
            Thread.Sleep(1000);
            if (mainView != null) BaseUtils.TurnOnGreenLight(mainView.IMG_L_COLLECTORWORK);
        }
        //收集器横臂运行到指定位置
        public static void CollectFrameExhibitOrganMoveTo(MainView mainView, int nodeid)
        {
            if (mainView != null) BaseUtils.TurnOnRedLight(mainView.IMG_L_COLLECTORWORK);
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            buffer = BaseUtils.RecvStatusDatagram(Datagram.V3_E_STATUS);
            if (buffer == null) return;
            if (buffer[0x05] == 0x23)
            {
                string str = String.Format(Datagram.V3_E1_MOVE, String.Format("{0:X2}", nodeid));

                if (BaseUtils.SendYKDatagram(str))
                {
                    App.SetSystemStatus(String.Format("收集器支臂运动{0}计数位", nodeid));
                    Thread.Sleep(1000);
                }
                Thread.Sleep(nodeid * 250);
            }

        }
        //加热器按照设定的温度加热
        public static void HeatByTemp(double dTempSetting)
        {
            Byte[] buffer = new byte[StaticParam.BUFFERSIZE];
            //查询温度
            buffer = BaseUtils.RecvStatusDatagram(Datagram.T_BOXTEMP_READ);
            if (buffer == null) return;
            if (buffer != null && buffer[0x02] == 0x1B)
            {
                short t1 = (short)((buffer[0x03] << 8) + buffer[0x04]);
                double t = ((double)t1) / 100;
                string[] arr = CommonLib.CommonLib.IntToHexArr((int)(dTempSetting * 10));
                string datagram = string.Format(Datagram.T_HEAT_START, arr[1].Substring(1, 1), arr[0]);
                if (BaseUtils.SendYKDatagram(datagram))
                {
                    App.SetSystemStatus(String.Format("加热命令发送成功，以{0}℃为标准进行加热！", dTempSetting));
                }
                //如果温度不在有效范围内，则提示
                if (t < (dTempSetting - App.m_dlTempOffset) || t > (dTempSetting + App.m_dlTempOffset))
                    System.Windows.MessageBox.Show(App.m_LangPackage.TIP_TEMP_NOT_REACHED_STANDARD, App.m_LangPackage.TIP, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                //if (dTempSetting <= t)
                    
            }
        }
        //加热器停止加热
        public static void HeatHalt()
        {
            //停止加热
            if (BaseUtils.SendYKDatagram(Datagram.T_HEAT_STOP))
            {
                App.SetSystemStatus(String.Format("停止加热命令发送成功"));
            }
        }
        //启动/停止蜂鸣器
        public static void Beep(bool bRunning)
        {
            string strDatagram = Datagram.B_BEEP_OFF;
            if (bRunning) strDatagram = Datagram.B_BEEP_ON;
            byte[] sendbuf;
            sendbuf = CommonLib.CommonLib.HexStrToByteArr(strDatagram);
            int len = CommonLib.CommonLib.GetHexStrByteLen(strDatagram);
            iSerialCom.__SendData(sendbuf, len);
            //App.WriteSystemLog("下发：" + strDatagram);
            App.SetSystemStatus(string.Format("蜂鸣器{0}",bRunning?"启动":"关闭"));
        }
        //读取水箱温度
        public static void ReadWaterBoxTemp(dynamic wnd)
        {
            App.sendYCbuf = CommonLib.CommonLib.HexStrToByteArr(Datagram.T_BOXTEMP_READ);
            iSerialCom.__SendData(App.sendYCbuf, App.sendYCbuf.Length);
            Thread.Sleep(App.m_nTempSendRecvInterval);
            int nLen = iSerialCom.__RecvData(App.recvYCbuf);
            new DatagramParse().Parse(App.recvYCbuf, nLen, wnd);
        }
        //读取水杯温度
        public static void ReadCupTemp(dynamic wnd)
        {
            App.sendYCbuf = CommonLib.CommonLib.HexStrToByteArr(Datagram.T_CUPTEMP_READ);
            iSerialCom.__SendData(App.sendYCbuf, App.sendYCbuf.Length);
            Thread.Sleep(App.m_nTempSendRecvInterval);
            int nLen = iSerialCom.__RecvData(App.recvYCbuf);
            new DatagramParse().Parse(App.recvYCbuf, nLen, wnd);
        }
    }
}