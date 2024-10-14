using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using Pharmacy.INST.DissolutionClient.pages.modal;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.common;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Collections;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Page
    {
        //方法
        private DissolutionMethod m_EdittingDissolutionMethod;    //编辑中的方法
        private DissolutionMethod m_ProductionDissolutionMethod;  //产线方法
        private DissolutionMethod m_RuntimeDissolutionMethod;     //运行中方法
        private TestData m_TestData;                              //测试数据
        //动态组件
        private ImageButton m_imageButtonUP;
        private ImageButton m_imageButtonStop;
        private ImageButton m_imageButtonDown;
        private ImageButton m_imageSamplePointButtonUP;
        private ImageButton m_imageSamplePointButtonStop;
        private ImageButton m_imageSamplePointButtonDown;
        private ImageButton m_imageIimpellerButtonRun;
        private ImageButton m_imageIimpellerButtonStop;
        private ImageButton m_imagePullPillButton;
        private ImageButton m_imageClosePullPillButton;
        /// <summary>
        /// 取样线程相关参数
        /// </summary>
        public Thread m_PreworkExperimentThread;                  //实验准备工作句柄
        public Thread m_ExperimentThread;                         //实验线程句柄
        public Thread m_SamplePrepareProcThread;                  //取样准备线程句柄
        public Thread m_SampleProcThread;                         //取样线程句柄

        private int m_nRemainSecond;                              //倒计时秒
        private int m_nExperimentCounter;                         //实验过程计时器
        private DispatcherTimer m_SampleToRunDispatcherTimer;     //取样执行操作定时器
        private DispatcherTimer m_PutPillAndRotationTimer;        //投药和转桨搅拌定时器
        private int m_nCurrentSampleTimes;                        //取样次第
        private int m_nAlltimespan;                               //本次实验总时长
        private double[,] m_RunCupTempArr;                        //取样时杯中温度样本数组

        public MainView()
        {
            InitializeComponent();
            InitializeComponentEx();     //窗口类初始化时加载，避免切换Tab页时重复加载
            InitializeInterface();       //初始化界面语言
            InitializeSystemParam();     //初始化系统变量
            Task t = Task.Run(() =>
            {
                int times = 0;
                while (true && times < 30)
                {
                    if (!App.m_bLaunchInitialize)
                    {
                        if (App.m_bAutoHeatting && App.g_bHeating)
                        {
                            BaseUtils.TurnOnRedLight(IMG_L_UPTEMP);
                            break;
                        }
                    }
                    times++;
                    Thread.Sleep(1000);
                }
            });
        }
        #region 窗口事件
        //加载事件
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(2), App.GetBehaviorRemark(2)));
        }
        //转速设置按钮事件
        private void PARA2_BTN_SPEEDSETTING_Click(object sender, RoutedEventArgs e)
        {
            SpeedSettingModal speedSettingModal = new SpeedSettingModal(CallBack_SpeedSetting, m_EdittingDissolutionMethod.oSpeedModule);
            speedSettingModal.ShowDialog();
        }
        //取样时间设置按钮事件
        private void PARA2_BTN_SAMPLETIME_Click(object sender, RoutedEventArgs e)
        {
            SampleTimeModal sampleTimeModal = new SampleTimeModal(CallBack_SampleTime, m_EdittingDissolutionMethod.Sample_Time_Arr);
            sampleTimeModal.ShowDialog();
        }
        //是否稀释选择框点击事件
        private void PARA3_CHK_DILUTIONENABLED_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
                PARA3_COMBO_DILUTIONRATIO.IsEnabled = true;
            else
            {
                PARA3_COMBO_DILUTIONRATIO.Text = string.Empty;
                PARA3_COMBO_DILUTIONRATIO.IsEnabled = false;
            }
        }
        //是否稀释选择框选中事件
        private void PARA3_CHK_DILUTIONENABLED_Checked(object sender, RoutedEventArgs e)
        {
            PARA3_COMBO_DILUTIONRATIO.IsEnabled = ((CheckBox)sender).IsChecked == true ? true : false;
        }
        //是否稀释选择框反选中事件
        private void PARA3_CHK_DILUTIONENABLED_Unchecked(object sender, RoutedEventArgs e)
        {
            PARA3_COMBO_DILUTIONRATIO.IsEnabled = ((CheckBox)sender).IsChecked == true ? true : false;
        }
        //方法保存按钮事件
        private void Btn_SaveMethod_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(((Button)sender).Content.ToString(), true))
                SaveMethod();
        }
        //方法调用按钮事件
        private void Btn_CallMethod_Click(object sender, RoutedEventArgs e)
        {
            if (!CallMethod())
            {
                MessageBox.Show(App.m_LangPackage.TIP_LOAD_METHOD_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                //写系统日志
            }
        }
        //方法清除按钮事件
        private void BTN_CALLCLEARMETHOD_Click(object sender, RoutedEventArgs e)
        {
            m_EdittingDissolutionMethod.Reset();
            EmptyEditView();
        }
        //参数发送按钮事件
        private void Btn_LaunchParam_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                if (!LaunchParam())
                {
                    MessageBox.Show(App.m_LangPackage.TIP_PARAMS_SEND_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    //写系统日志
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(105), App.GetBehaviorRemark(105)));
                }
            }
        }
        //升降柱上 按钮 事件
        private void BTN_UP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.LiftingUp();
                    ExpStepAction.ResumeTempCollect();
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(87), App.GetBehaviorRemark(87)));
                });
            }
        }
        //升降柱停 按钮 事件
        private void BTN_STOP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.LiftingStop();
                    ExpStepAction.ResumeTempCollect();
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(88), App.GetBehaviorRemark(88)));
                });
            }
        }
        //升降柱下 按钮 事件
        private void BTN_DOWN_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.LiftingDown();
                    ExpStepAction.ResumeTempCollect();
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(89), App.GetBehaviorRemark(89)));
                });
            }
        }
        //取样针上 按钮 事件
        private void SamplePoint_BTN_UP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.SamplePointReset(this);
                    if (App.m_bCupTempEnable)
                        ExpStepAction.SuspendCupTempCollect();
                    ExpStepAction.ResumeTempCollect();
                });
            }
        }
        //取样针停止 按钮 事件
        private void SamplePoint_BTN_STOP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.SamplePointStop(this);
                    ExpStepAction.ResumeTempCollect();
                });
            }
        }
        //取样针下 按钮 事件
        private void SamplePoint_BTN_DOWN_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.SamplePointDown(this, 375, StaticParam.DissolutionMethod_Arr[0]);
                    if (App.m_bCupTempEnable) ExpStepAction.ResumeCupTempCollect();
                    ExpStepAction.ResumeTempCollect();
                });
            }
        }
        //转桨转 按钮 事件
        private void Iimpeller_BTN_RUN_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            //if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            //{
            //    Task t = Task.Run(() =>
            //    {
            //        ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
            //        ExpStepAction.Rotation(this, 100, StaticParam.ElectricMotor.SIGNLE);
            //        if (App.m_bDoubleMotor)
            //            ExpStepAction.Rotation(null, 100, StaticParam.ElectricMotor.DOUBLE);
            //        ExpStepAction.ResumeTempCollect();
            //    });
            //}
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                SetSpeedModal setSpeedModal = new SetSpeedModal(CallBackSelectSwivel);
                setSpeedModal.ShowDialog();
            }
        }
        //转桨停 按钮 事件
        private void Iimpeller_BTN_STOP_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.Rotation(this, 0, StaticParam.ElectricMotor.SIGNLE);
                    //第二电机旋转停止
                    if (App.m_bDoubleMotor)
                        ExpStepAction.Rotation(null, 0, StaticParam.ElectricMotor.DOUBLE);
                    ExpStepAction.ResumeTempCollect();
                });
            }
        }
        //投药器打开 按钮 事件
        private void PullPill_BTN_PULL_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.PutPill(this); //投药
                    ExpStepAction.ResumeTempCollect();
                });
            }
        }
        //投药器关闭 按钮 事件
        private void PullPill_BTN_CLOSE_PULL_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                Task t = Task.Run(() =>
                {
                    ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                    ExpStepAction.PillReset(this); //投药
                    ExpStepAction.ResumeTempCollect();
                });
            }
        }
        //实验开始按钮事件
        private void BTN_EXPSTART_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                if (!App.CheckMainframeStatus()) return;
                if (App.m_bExperimentRunning) return;
                if (RT_METHODNAME.Content.Equals("") || RT_METHODNAME.Content.Equals(StaticParam.NULLSING))
                {
                    MessageBox.Show(App.m_LangPackage.TIP_NOPARAMS_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AppointmentTimeModal appointmentTimeModal = new AppointmentTimeModal(CallBack_RecviceAppointmentTime);
                appointmentTimeModal.ShowDialog();
            }
        }
        //实验暂停 按钮事件
        private void BTN_EXPPAUSE_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                if (!App.m_bSamplingOn)
                {
                    if (App.m_bExperimentPause)
                    {
                        App.m_bExperimentPause = false;
                        BTN_EXPPAUSE.Content = App.m_LangPackage.BTN_EXPPAUSE;
                        App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(86), App.GetBehaviorRemark(86)));
                    }
                    else
                    {
                        App.m_bExperimentPause = true;
                        App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(27), App.GetBehaviorRemark(27)));
                        BTN_EXPPAUSE.Content = App.m_LangPackage.BTN_CONT;
                    }
                }
                else
                    MessageBox.Show(App.m_LangPackage.TIP_SAMPLING_NO_PAUSE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //实验结束 按钮 事件
        private void BTN_EXPEND_Click(object sender, RoutedEventArgs e)
        {
            if (BaseUtils.CheckFunctionPrivelege(App.GetFunctionName(18), true))
            {
                if (App.m_bExperimentRunning)
                {
                    if (MessageBox.Show(App.m_LangPackage.TIP_TERMINATE_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
                    UIOperator.SetButtonContent(BTN_EXPEND, App.m_LangPackage.BTN_EXPENDING);
                    TerminateExperiment(DateTime.Now, StaticParam.ExpStatusType[(int)StaticParam.ExpStatus.PARTIAL]);
                }
            }
        }
        //机头锁开 事件
        private void MainBodyOnSwitcher_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableManOperate(true);
        }
        //机头锁关 事件
        private void MainBodyOffSwitcher_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            EnableManOperate(false);
        }
        //启用/停用手动操控
        public void EnableManOperate(bool bValid)
        {
            SwitchManOperateView(bValid);
        }
        //关闭/打开界面操作
        private void SwitchUnParallelFunction(bool valid)
        {
            UIOperator.SetComponentValid(PARA1_METHODNAME, valid);
            UIOperator.SetComponentValid(PARA1_MANUFACTURER, valid);
            UIOperator.SetComponentValid(PARA1_BATCHNO, valid);
            UIOperator.SetComponentValid(PARA1_TESTDEPARTMENT, valid);
            UIOperator.SetComponentValid(PARA3_SAMPLEVOLUME, valid);
            UIOperator.SetComponentValid(PARA3_SAMPLETIMES, valid);
            UIOperator.SetComponentValid(PARA3_COMBO_AUTOSUPPLY, valid);
            UIOperator.SetComponentValid(PARA3_FIRSTFILTERVOLUME, valid);
            UIOperator.SetComponentValid(PARA2_COMBO_DISSOLUTIONMETHODNAME, valid);
            UIOperator.SetComponentValid(PARA2_TEMPSETTING, valid);
            UIOperator.SetComponentValid(PARA2_SOLUTIONVOLUME, valid);
            UIOperator.SetComponentValid(PARA2_ALLTIMESPAN, valid);
            UIOperator.SetComponentValid(PARA3_CHK_DILUTIONENABLED, valid);

            UIOperator.SetComponetValidAndOpacity(PARA2_BTN_SPEEDSETTING, valid);
            UIOperator.SetComponetValidAndOpacity(PARA2_BTN_SAMPLETIME, valid);
            UIOperator.SetComponetValidAndOpacity(BTN_EXPSTART, valid);
            UIOperator.SetComponetValidAndOpacity(BTN_SAVEMETHOD, valid);
            UIOperator.SetComponetValidAndOpacity(BTN_CALLMETHOD, valid);
            UIOperator.SetComponetValidAndOpacity(BTN_CALLCLEARMETHOD, valid);
            UIOperator.SetComponetValidAndOpacity(BTN_LAUNCHPARAM, valid);

            EnableManOperate(valid);

        }
        //关闭/打开手动操控
        private void SwitchManOperateView(bool valid)
        {
            UIOperator.SetComponetValidAndOpacity(m_imageButtonUP, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageButtonStop, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageButtonDown, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageSamplePointButtonUP, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageSamplePointButtonStop, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageSamplePointButtonDown, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageIimpellerButtonRun, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageIimpellerButtonStop, valid);
            UIOperator.SetComponetValidAndOpacity(m_imagePullPillButton, valid);
            UIOperator.SetComponetValidAndOpacity(m_imageClosePullPillButton, valid);
        }
        #endregion
        #region 功能实现
        //窗口组件初始化
        private void InitializeComponentEx()
        {
            //加载溶出方法下拉框
            UIOperator.ComboBoxBinder(PARA2_COMBO_DISSOLUTIONMETHODNAME, StaticParam.DissolutionMethod_Arr);
            //加载是否补液下拉框
            UIOperator.ComboBoxBinder(PARA3_COMBO_AUTOSUPPLY, StaticParam.Auto_fluid_infusion_Arr);
            //加载稀释倍数下拉框
            UIOperator.ComboBoxBinder(PARA3_COMBO_DILUTIONRATIO, StaticParam.DilutionRatio_Arr);
            //初始化流程灯CANVAS_INDICATE
            foreach (object c in CANVAS_INDICATE.Children)
            {
                if (c.GetType() == typeof(Image))
                {
                    UIOperator.LoadImage(App.g_ResourceDir + StaticParam.LIGHT_GREEN_FILE, (Image)c);
                }
            }
            //初始化时暂停，停止按钮均为无效状态
            UIOperator.SetComponetValidAndOpacity(BTN_EXPPAUSE, false);
            UIOperator.SetComponetValidAndOpacity(BTN_EXPEND, false);
            //初始化加载向上图标按钮
            m_imageButtonUP = new ImageButton(App.g_ResourceDir + "btn_up.png", App.g_ResourceDir + "btn_up_hover.png");
            m_imageButtonUP.ToolTip = "向上";
            m_imageButtonUP.SetValue(Canvas.LeftProperty, (double)15);
            m_imageButtonUP.SetValue(Canvas.TopProperty, (double)30);
            m_imageButtonUP.Width = (double)45;
            m_imageButtonUP.Height = (double)45;
            m_imageButtonUP.MouseLeftButtonUp += new MouseButtonEventHandler(BTN_UP_Click);
            ManOperatorView.Children.Add(m_imageButtonUP);
            //初始化加载停止图标按钮
            m_imageButtonStop = new ImageButton(App.g_ResourceDir + "btn_stop.png", App.g_ResourceDir + "btn_stop_hover.png");
            m_imageButtonStop.ToolTip = "停止";
            m_imageButtonStop.SetValue(Canvas.LeftProperty, (double)75);
            m_imageButtonStop.SetValue(Canvas.TopProperty, (double)30);
            m_imageButtonStop.Width = (double)45;
            m_imageButtonStop.Height = (double)45;
            m_imageButtonStop.MouseLeftButtonUp += new MouseButtonEventHandler(BTN_STOP_Click);
            ManOperatorView.Children.Add(m_imageButtonStop);
            //初始化加载向下图标按钮
            m_imageButtonDown = new ImageButton(App.g_ResourceDir + "btn_down.png", App.g_ResourceDir + "btn_down_hover.png");
            m_imageButtonDown.ToolTip = "向下";
            m_imageButtonDown.SetValue(Canvas.LeftProperty, (double)135);
            m_imageButtonDown.SetValue(Canvas.TopProperty, (double)30);
            m_imageButtonDown.Width = (double)45;
            m_imageButtonDown.Height = (double)45;
            m_imageButtonDown.MouseLeftButtonUp += new MouseButtonEventHandler(BTN_DOWN_Click);
            ManOperatorView.Children.Add(m_imageButtonDown);
            //初始化取样针下降向上图标按钮
            m_imageSamplePointButtonUP = new ImageButton(App.g_ResourceDir + "btn_up.png", App.g_ResourceDir + "btn_up_hover.png");
            m_imageSamplePointButtonUP.ToolTip = "向上";
            m_imageSamplePointButtonUP.SetValue(Canvas.LeftProperty, (double)15);
            m_imageSamplePointButtonUP.SetValue(Canvas.TopProperty, (double)125);
            m_imageSamplePointButtonUP.Width = (double)45;
            m_imageSamplePointButtonUP.Height = (double)45;
            m_imageSamplePointButtonUP.MouseLeftButtonUp += new MouseButtonEventHandler(SamplePoint_BTN_UP_Click);
            ManOperatorView.Children.Add(m_imageSamplePointButtonUP);
            if (!App.m_bSamplePointEnable) UIOperator.SetComponetValidAndOpacity(m_imageSamplePointButtonUP, false);
            //初始化取样针停止图标按钮
            m_imageSamplePointButtonStop = new ImageButton(App.g_ResourceDir + "btn_stop.png", App.g_ResourceDir + "btn_stop_hover.png");
            m_imageSamplePointButtonStop.ToolTip = "停止";
            m_imageSamplePointButtonStop.SetValue(Canvas.LeftProperty, (double)75);
            m_imageSamplePointButtonStop.SetValue(Canvas.TopProperty, (double)125);
            m_imageSamplePointButtonStop.Width = (double)45;
            m_imageSamplePointButtonStop.Height = (double)45;
            m_imageSamplePointButtonStop.MouseLeftButtonUp += new MouseButtonEventHandler(SamplePoint_BTN_STOP_Click);
            ManOperatorView.Children.Add(m_imageSamplePointButtonStop);
            if (!App.m_bSamplePointEnable) UIOperator.SetComponetValidAndOpacity(m_imageSamplePointButtonStop, false);
            //初始化取样针下降向下图标按钮
            m_imageSamplePointButtonDown = new ImageButton(App.g_ResourceDir + "btn_down.png", App.g_ResourceDir + "btn_down_hover.png");
            m_imageSamplePointButtonDown.ToolTip = "向下";
            m_imageSamplePointButtonDown.SetValue(Canvas.LeftProperty, (double)135);
            m_imageSamplePointButtonDown.SetValue(Canvas.TopProperty, (double)125);
            m_imageSamplePointButtonDown.Width = (double)45;
            m_imageSamplePointButtonDown.Height = (double)45;
            m_imageSamplePointButtonDown.MouseLeftButtonUp += new MouseButtonEventHandler(SamplePoint_BTN_DOWN_Click);
            ManOperatorView.Children.Add(m_imageSamplePointButtonDown);
            if (!App.m_bSamplePointEnable) UIOperator.SetComponetValidAndOpacity(m_imageSamplePointButtonDown, false);
            //初始化转桨转图标按钮
            m_imageIimpellerButtonRun = new ImageButton(App.g_ResourceDir + "btn_rotate.png", App.g_ResourceDir + "btn_rotate_hover.png");
            m_imageIimpellerButtonRun.ToolTip = "旋转";
            m_imageIimpellerButtonRun.SetValue(Canvas.LeftProperty, (double)210);
            m_imageIimpellerButtonRun.SetValue(Canvas.TopProperty, (double)125);
            m_imageIimpellerButtonRun.Width = (double)45;
            m_imageIimpellerButtonRun.Height = (double)45;
            m_imageIimpellerButtonRun.MouseLeftButtonUp += new MouseButtonEventHandler(Iimpeller_BTN_RUN_Click);
            ManOperatorView.Children.Add(m_imageIimpellerButtonRun);
            //初始化转桨停图标按钮
            m_imageIimpellerButtonStop = new ImageButton(App.g_ResourceDir + "btn_stop.png", App.g_ResourceDir + "btn_stop_hover.png");
            m_imageIimpellerButtonStop.ToolTip = "停止";
            m_imageIimpellerButtonStop.SetValue(Canvas.LeftProperty, (double)270);
            m_imageIimpellerButtonStop.SetValue(Canvas.TopProperty, (double)125);
            m_imageIimpellerButtonStop.Width = (double)45;
            m_imageIimpellerButtonStop.Height = (double)45;
            m_imageIimpellerButtonStop.MouseLeftButtonUp += new MouseButtonEventHandler(Iimpeller_BTN_STOP_Click);
            ManOperatorView.Children.Add(m_imageIimpellerButtonStop);
            //打开投药器图标按钮
            m_imagePullPillButton = new ImageButton(App.g_ResourceDir + "btn_pullpill.png", App.g_ResourceDir + "btn_pullpill_hover.png");
            m_imagePullPillButton.ToolTip = "打开投药器";
            m_imagePullPillButton.SetValue(Canvas.LeftProperty, (double)210);
            m_imagePullPillButton.SetValue(Canvas.TopProperty, (double)30);
            m_imagePullPillButton.Width = (double)45;
            m_imagePullPillButton.Height = (double)45;
            m_imagePullPillButton.MouseLeftButtonUp += new MouseButtonEventHandler(PullPill_BTN_PULL_Click);
            ManOperatorView.Children.Add(m_imagePullPillButton);
            if (!App.m_bPutPillEnable) UIOperator.SetComponetValidAndOpacity(m_imagePullPillButton, false);
            //关闭投药器图标按钮
            m_imageClosePullPillButton = new ImageButton(App.g_ResourceDir + "btn_stop.png", App.g_ResourceDir + "btn_stop_hover.png");
            m_imageClosePullPillButton.ToolTip = "关闭投药器";
            m_imageClosePullPillButton.SetValue(Canvas.LeftProperty, (double)270);
            m_imageClosePullPillButton.SetValue(Canvas.TopProperty, (double)30);
            m_imageClosePullPillButton.Width = (double)45;
            m_imageClosePullPillButton.Height = (double)45;
            m_imageClosePullPillButton.MouseLeftButtonUp += new MouseButtonEventHandler(PullPill_BTN_CLOSE_PULL_Click);
            ManOperatorView.Children.Add(m_imageClosePullPillButton);
            if (!App.m_bPutPillEnable) UIOperator.SetComponetValidAndOpacity(m_imageClosePullPillButton, false);
            //初始化小杯温度区域
            InitializeCupTempView();
        }
        //初始化界面
        public void InitializeInterface()
        {
            MainView_BasePara_GroupBox.Header = App.m_LangPackage.MainView_BasePara_GroupBox;
            LB_PARA1_METHODNAME.Content = App.m_LangPackage.LB_PARA1_METHODNAME;
            PARA1_METHODNAME.Tag = App.m_LangPackage.PARA1_METHODNAME;
            LB_PARA1_BATCHNO.Content = App.m_LangPackage.LB_PARA1_BATCHNO;
            PARA1_BATCHNO.Tag = App.m_LangPackage.PARA1_BATCHNO;
            LB_PARA1_MANUFACTURER.Content = App.m_LangPackage.LB_PARA1_MANUFACTURER;
            //PARA1_MANUFACTURER.Tag = App.m_LangPackage.PARA1_MANUFACTURER;
            LB_PARA1_TESTDEPARTMENT.Content = App.m_LangPackage.LB_PARA1_TESTDEPARTMENT;
            //PARA1_TESTDEPARTMENT.Tag = App.m_LangPackage.PARA1_TESTDEPARTMENT;

            MainView_DissolutionPara_GroupBox.Header = App.m_LangPackage.MainView_DissolutionPara_GroupBox;
            LB_PARA2_COMBO_DISSOLUTIONMETHODNAME.Content = App.m_LangPackage.LB_PARA2_COMBO_DISSOLUTIONMETHODNAME;
            PARA2_COMBO_DISSOLUTIONMETHODNAME.Tag = App.m_LangPackage.PARA2_COMBO_DISSOLUTIONMETHODNAME;
            LB_PARA2_SOLUTIONVOLUME.Content = App.m_LangPackage.LB_PARA2_SOLUTIONVOLUME;
            PARA2_SOLUTIONVOLUME.Tag = App.m_LangPackage.PARA2_SOLUTIONVOLUME;
            LB_PARA2_TEMPSETTING.Content = App.m_LangPackage.LB_PARA2_TEMPSETTING;
            PARA2_TEMPSETTING.Tag = App.m_LangPackage.PARA2_TEMPSETTING;
            LB_PARA2_BTN_SPEEDSETTING.Content = App.m_LangPackage.LB_PARA2_BTN_SPEEDSETTING;
            LB_PARA2_ALLTIMESPAN.Content = App.m_LangPackage.LB_PARA2_ALLTIMESPAN;
            PARA2_ALLTIMESPAN.Tag = App.m_LangPackage.PARA2_ALLTIMESPAN;
            LB_PARA2_BTN_SAMPLETIME.Content = App.m_LangPackage.LB_PARA2_BTN_SAMPLETIME;
            PARA2_BTN_SPEEDSETTING.Content = App.m_LangPackage.PARA2_BTN_SPEEDSETTING;
            PARA2_BTN_SAMPLETIME.Content = App.m_LangPackage.PARA2_BTN_SAMPLETIME;

            MainView_SamplePara_GroupBox.Header = App.m_LangPackage.MainView_SamplePara_GroupBox;
            LB_PARA3_SAMPLEVOLUME.Content = App.m_LangPackage.LB_PARA3_SAMPLEVOLUME;
            PARA3_SAMPLEVOLUME.Tag = App.m_LangPackage.PARA3_SAMPLEVOLUME;
            LB_PARA3_SAMPLETIMES.Content = App.m_LangPackage.LB_PARA3_SAMPLETIMES;
            PARA3_SAMPLETIMES.Tag = App.m_LangPackage.PARA3_SAMPLETIMES;
            LB_PARA3_SAMPLETIMES_UNIT.Content = App.m_LangPackage.LB_PARA3_SAMPLETIMES_UNIT;
            LB_PARA3_COMBO_AUTOSUPPLY.Content = App.m_LangPackage.LB_PARA3_COMBO_AUTOSUPPLY;
            LB_PARA3_FIRSTFILTERVOLUME.Content = App.m_LangPackage.LB_PARA3_FIRSTFILTERVOLUME;
            PARA3_FIRSTFILTERVOLUME.Tag = App.m_LangPackage.PARA3_FIRSTFILTERVOLUME;
            LB_PARA3_CHK_DILUTIONENABLED.Content = App.m_LangPackage.LB_PARA3_CHK_DILUTIONENABLED;
            LB_PARA3_COMBO_DILUTIONRATIO.Content = App.m_LangPackage.LB_PARA3_COMBO_DILUTIONRATIO;
            LB_PARA3_COMBO_DILUTIONRATIO_UNIT.Content = App.m_LangPackage.LB_PARA3_COMBO_DILUTIONRATIO_UNIT;

            MainView_Operator_GroupBox.Header = App.m_LangPackage.MainView_Operator_GroupBox;
            MainView_HDA_GroupBox.Header = App.m_LangPackage.MainView_HDA_GroupBox;
            MainView_SAMPLEPOINT_GroupBox.Header = App.m_LangPackage.MainView_SAMPLEPOINT_GroupBox;
            MainView_Proprotor_GroupBox.Header = App.m_LangPackage.MainView_Proprotor_GroupBox;
            MainView_FillTablet_GroupBox.Header = App.m_LangPackage.MainView_FillTablet_GroupBox;
            BTN_SAVEMETHOD.Content = App.m_LangPackage.BTN_SAVEMETHOD;
            BTN_LAUNCHPARAM.Content = App.m_LangPackage.BTN_LAUNCHPARAM;
            BTN_CALLMETHOD.Content = App.m_LangPackage.BTN_CALLMETHOD;
            BTN_CALLCLEARMETHOD.Content = App.m_LangPackage.BTN_CALLCLEARMETHOD;
            BTN_EXPSTART.Content = App.m_LangPackage.BTN_EXPSTART;
            BTN_EXPPAUSE.Content = App.m_LangPackage.BTN_EXPPAUSE;
            BTN_EXPEND.Content = App.m_LangPackage.BTN_EXPEND;

            MainView_RunningStatus_GroupBox.Header = App.m_LangPackage.MainView_RunningStatus_GroupBox;
            LB_RT_METHODNAME.Content = App.m_LangPackage.LB_RT_METHODNAME;
            LB_RT_BATCHNO.Content = App.m_LangPackage.LB_RT_BATCHNO;
            LB_RT_MANUFACTURER.Content = App.m_LangPackage.LB_RT_MANUFACTURER;
            LB_RT_WATERBOXTEMP.Content = App.m_LangPackage.LB_RT_WATERBOXTEMP;
            LB_RT_DISSOLUTIONMETHODNAME.Content = App.m_LangPackage.LB_RT_DISSOLUTIONMETHODNAME;
            LB_RT_TESTDEPARTMENT.Content = App.m_LangPackage.LB_RT_TESTDEPARTMENT;
            LB_RT_FRONTROW_SPEED_1.Content = App.m_LangPackage.LB_RT_FRONTROW_SPEED_1;
            LB_RT_FRONTROW_SPEED_2.Content = App.m_LangPackage.LB_RT_FRONTROW_SPEED_2;
            LB_RT_ALLTIMESPAN.Content = App.m_LangPackage.LB_RT_ALLTIMESPAN;
            LB_RT_BACKROW_SPEED_1.Content = App.m_LangPackage.LB_RT_BACKROW_SPEED_1;
            LB_RT_REMAINTIME.Content = App.m_LangPackage.LB_RT_REMAINTIME;
            LB_RT_BACKROW_SPEED_2.Content = App.m_LangPackage.LB_RT_BACKROW_SPEED_2;

            LB_RT_TEMPSETTING.Content = App.m_LangPackage.LB_RT_TEMPSETTING;
            LB_RT_NEXT_SAMPLE_TIME.Content = App.m_LangPackage.LB_RT_NEXT_SAMPLE_TIME;
            LB_RT_SAMPLEVOLUME.Content = App.m_LangPackage.LB_RT_SAMPLEVOLUME;
            LB_RT_SAMPLETIMES.Content = App.m_LangPackage.LB_RT_SAMPLETIMES;
            LB_RT_CURRENT_SAMPLE_TIME.Content = App.m_LangPackage.LB_RT_CURRENT_SAMPLE_TIME;
            LB_RT_SAMPLETIMES_1.Content = App.m_LangPackage.LB_RT_SAMPLETIMES_1;
            LB_RT_SAMPLETIMES_2.Content = App.m_LangPackage.LB_RT_SAMPLETIMES_2;
            LB_RT_SAMPLETIMES_3.Content = App.m_LangPackage.LB_RT_SAMPLETIMES_3;

            LB_RT_AUTOFLUIDINFUSION.Content = App.m_LangPackage.LB_RT_AUTOFLUIDINFUSION;
            LB_RT_FIRSTFILTERVOLUME.Content = App.m_LangPackage.LB_RT_FIRSTFILTERVOLUME;
            LB_RT_SOLUTIONVOLUME.Content = App.m_LangPackage.LB_RT_SOLUTIONVOLUME;
            MainView_CupTemp_GroupBox.Header = App.m_LangPackage.MainView_CupTemp_GroupBox;
            LB_RT_DILUTIONENABLED.Content = App.m_LangPackage.LB_RT_DILUTIONENABLED;
            LB_RT_DILUTIONRATIO.Content = App.m_LangPackage.LB_RT_DILUTIONRATIO;

            LB_TEST_STEP.Text = App.m_LangPackage.LB_TEST_STEP;
            LB_IMG_L_INIT.Text = App.m_LangPackage.LB_IMG_L_INIT;
            LB_IMG_L_UPTEMP.Text = App.m_LangPackage.LB_IMG_L_UPTEMP;
            LB_IMG_L_WAITPUTPILL.Text = App.m_LangPackage.LB_IMG_L_WAITPUTPILL;
            LB_IMG_L_IMPELLERTURN.Text = App.m_LangPackage.LB_IMG_L_IMPELLERTURN;
            LB_IMG_L_DISSOLUTIONWORK.Text = App.m_LangPackage.LB_IMG_L_DISSOLUTIONWORK;
            LB_IMG_L_SAMPLINGWORK.Text = App.m_LangPackage.LB_IMG_L_SAMPLINGWORK;
            LB_IMG_L_COLLECTORWORK.Text = App.m_LangPackage.LB_IMG_L_COLLECTORWORK;
            LB_IMG_L_SAMPLINGOVER.Text = App.m_LangPackage.LB_IMG_L_SAMPLINGOVER;
            LB_IMG_L_EXPERIMENTOVER.Text = App.m_LangPackage.LB_IMG_L_EXPERIMENTOVER;
            LB_TEST_STATUS.Text = App.m_LangPackage.LB_TEST_STATUS;
        }
        //初始化杯内温度显示区域
        private void InitializeCupTempView()
        {
            int colsnum = StaticParam.CONSOLE_TEMP_COLUMN_NUM; //列数
            int rowsnum = App.m_nCupNum / colsnum + App.m_nCupNum % colsnum; //行数
            int x = 9;
            int y = 5;
            int colwidth = 130;
            int rowheight = 158 / rowsnum;
            string componetName;

            for (int i = 0; i < rowsnum; i++)
            {
                for (int j = 0; j < colsnum; j++)
                {
                    Label label = new Label
                    {
                        Content = string.Format("{0}：", i * colsnum + j + 1),
                        HorizontalContentAlignment = HorizontalAlignment.Right,
                        Width = 40,
                        Height = 33
                    };
                    label.SetValue(Canvas.LeftProperty, (double)(x + (j * colwidth)));
                    label.SetValue(Canvas.TopProperty, (double)(y + (i * rowheight)));
                    RT_CUP_GROUP.Children.Add(label);

                    CheckBox checkBox = new CheckBox();
                    componetName = string.Format("RT_CUP_TEMP_{0}_CK", i * colsnum + j + 1);
                    checkBox.Name = componetName;
                    checkBox.SetValue(Canvas.LeftProperty, (double)(x + 33 + (j * colwidth)));
                    checkBox.SetValue(Canvas.TopProperty, (double)(y + 9 + (i * rowheight)));
                    checkBox.Click += CheckBox_Click;
                    checkBox.IsChecked = checkBox.IsEnabled = App.g_cuptempenabled[i * colsnum + j] = App.m_bCupTempEnable;
                    RT_CUP_GROUP.RegisterName(componetName, checkBox);
                    RT_CUP_GROUP.Children.Add(checkBox);

                    Label label_tmp = new Label();
                    componetName = string.Format("RT_CUP_TEMP_{0}", i * colsnum + j + 1);
                    label_tmp.Name = componetName;
                    label_tmp.Content = StaticParam.NULLSING;
                    label_tmp.SetValue(StyleProperty, FindResource("CupTempLabel"));
                    label_tmp.Width = 60;
                    label_tmp.SetValue(Canvas.LeftProperty, (double)(x + 58 + (j * colwidth)));
                    label_tmp.SetValue(Canvas.TopProperty, (double)(y + 4 + (i * rowheight)));
                    RT_CUP_GROUP.RegisterName(componetName, label_tmp);
                    RT_CUP_GROUP.Children.Add(label_tmp);
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = ((CheckBox)sender).Name.ToString();
                string[] strArr = name.Split("_");
                int index = int.Parse(strArr[3]) - 1;
                App.g_cuptempenabled[index] = (bool)((CheckBox)sender).IsChecked;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }

        }

        //系统变量和参数初始化
        private void InitializeSystemParam()
        {
            m_EdittingDissolutionMethod = new DissolutionMethod();
            m_ProductionDissolutionMethod = new DissolutionMethod();
            m_RuntimeDissolutionMethod = new DissolutionMethod();
        }
        //旋转设置回调
        private void CallBackSelectSwivel(int nSwivel)
        {
            Task t = Task.Run(() =>
            {
                ExpStepAction.SuspendTempCollect(); //挂起温度采集线程
                ExpStepAction.Rotation(this, nSwivel, StaticParam.ElectricMotor.SIGNLE);
                if (App.m_bDoubleMotor)
                    ExpStepAction.Rotation(null, nSwivel, StaticParam.ElectricMotor.DOUBLE);
                ExpStepAction.ResumeTempCollect();
            });
        }
        //转速设置回调
        private void CallBack_SpeedSetting(SpeedModule speedModule)
        {
            m_EdittingDissolutionMethod.oSpeedModule.FrontRowSpeed_1 = speedModule.FrontRowSpeed_1;
            m_EdittingDissolutionMethod.oSpeedModule.FrontRowSpeed_2 = speedModule.FrontRowSpeed_2;
            m_EdittingDissolutionMethod.oSpeedModule.FrontRowStartTime_1 = speedModule.FrontRowStartTime_1;
            m_EdittingDissolutionMethod.oSpeedModule.FrontRowStartTime_2 = speedModule.FrontRowStartTime_2;
            m_EdittingDissolutionMethod.oSpeedModule.BackRowSpeed_1 = speedModule.BackRowSpeed_1;
            m_EdittingDissolutionMethod.oSpeedModule.BackRowSpeed_2 = speedModule.BackRowSpeed_2;
            m_EdittingDissolutionMethod.oSpeedModule.BackRowStartTime_1 = speedModule.BackRowStartTime_1;
            m_EdittingDissolutionMethod.oSpeedModule.BackRowStartTime_2 = speedModule.BackRowStartTime_2;
            m_EdittingDissolutionMethod.oSpeedModule.SpeedMode = speedModule.SpeedMode;
            m_EdittingDissolutionMethod.oSpeedModule.ElectricmotorMmode = speedModule.ElectricmotorMmode;
            SettingSpeedStatus();    //设置转速设置状态
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(84), App.GetBehaviorRemark(84)));
        }
        //取样时间回调
        private void CallBack_SampleTime(SampleTime[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                m_EdittingDissolutionMethod.Sample_Time_Arr[i] = arr[i];
            }
            SettingSampleTimeStatus();  //设置取样时间状态
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(85), App.GetBehaviorRemark(85)));
        }
        //加载方法回调
        private void CallBack_LoadMethod(DissolutionMethod dissolutionMethod)
        {
            //调取方法作为编辑状态
            m_EdittingDissolutionMethod = Tools.Mapper<DissolutionMethod, DissolutionMethod>(dissolutionMethod);
            //调取模式绑定刷新UI
            BindModelToView();
            SettingSpeedStatus();      //设置转速设置状态
            SettingSampleTimeStatus(); //设置取样时间状态
            SettingDilutionEnabledStatus(); //设置稀释状态
        }
        //删除方法回调
        private bool CallBack_DelMethod(DissolutionMethod dissolutionMethod)
        {
            DataSet ds = new DataSet();
            //判断同名方法是否存在
            string strCmd = String.Format(sql.SQL.SQL_R_FINDMETHODBYNAME, dissolutionMethod.MethodName); //strMethodName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_METHOD);
            if (ds.Tables[sql.SQL.T_METHOD].Rows.Count == 1) //该方法名存在
            {
                strCmd = String.Format(sql.SQL.SQL_D_METHODBYMETHODNAME, dissolutionMethod.MethodName); //strMethodName 参数
                if (App.m_SQLiteDBUtils.ExecuteNonQuery(strCmd) > 0) return true;
            }
            return false;
        }
        //获取预约时间后回调
        private void CallBack_RecviceAppointmentTime(int nAppointmentTime)
        {
            if (nAppointmentTime != 0)
            {
                //运行倒计时，交由倒计时器执行
                CountdownModal countdownModal = new CountdownModal(nAppointmentTime, ExperimentStart);
                countdownModal.ShowDialog();
                return;
            }
            //立即开始实验
            ExperimentStart();
        }
        //绑定视图数据至溶出模型
        private bool BindViewToModel()
        {
            //判断输入参数是否为空
            if (!UIOperator.IsContentExsitEmpty(ET_BASEPARA, App.g_EngVer)
            ||
            !UIOperator.IsContentExsitEmpty(ET_SOLUTIONPARA, App.g_EngVer)
            ||
            !UIOperator.IsContentExsitEmpty(ET_SAMPLEPARA, App.g_EngVer)) return false;
            //判断设置温度值
            try
            {
                double tempsetting = double.Parse(PARA2_TEMPSETTING.Text.Trim());
                int nUpperlimit = 55;
                int nLowerLimit = 20;
                if (tempsetting < nLowerLimit || tempsetting > nUpperlimit)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_TEMP_SETTING_OUTLINE, nLowerLimit, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_TEMP_SETTING_FORMAT_FAILURE, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            //判断溶媒体积值
            try
            {
                double solutionvolume = double.Parse(PARA2_SOLUTIONVOLUME.Text.Trim());
                //根据溶出方法设置溶媒体积上下限
                int nUpperlimit = 1000;
                int nLowerLimit = 500;
                if (PARA2_COMBO_DISSOLUTIONMETHODNAME.Text.Equals(StaticParam.DissolutionMethod_Arr[2])
                   ) //小杯法
                {
                    nUpperlimit = 250;
                    nLowerLimit = 100;
                }
                if (solutionvolume < nLowerLimit || solutionvolume > nUpperlimit)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_SOLVENT_SETTING_OUTLINE, nLowerLimit, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_SOLVENT_SETTING_FORMAT_FAILURE, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            //判断取样体积值
            try
            {
                double.Parse(PARA3_SAMPLEVOLUME.Text.Trim()).Equals("");
                double samplevolume = double.Parse(PARA3_SAMPLEVOLUME.Text.Trim());
                int nUpperlimit = 20;
                int nLowerLimit = 1;
                if (samplevolume < nLowerLimit || samplevolume > nUpperlimit)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_SAMPLE_VOLUME_SETTING_OUTLINE, nLowerLimit, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_SAMPLE_VOLUME_SETTING_FORMAT_FAILURE, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int sampletimes;
            try
            {
                sampletimes = int.Parse(PARA3_SAMPLETIMES.Text.ToString().Trim());
                int nUpperlimit = 40;
                int nLowerLimit = 1;
                if (sampletimes < nLowerLimit || sampletimes > nUpperlimit)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_SAMPLE_TIMES_SETTING_OUTLINE, nLowerLimit, nUpperlimit), App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_SAMPLE_TIMES_SETTING_FORMAT_FAILURE, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            try
            {
                double firstfiltervolume = double.Parse(PARA3_FIRSTFILTERVOLUME.Text.Trim());
                int nUpperlimit = 20;
                int nLowerLimit = 0;
                if (firstfiltervolume < nLowerLimit || firstfiltervolume > nUpperlimit)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_FIRSTFILTER_SETTING_OUTLINE, nLowerLimit, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_FIRSTFILTER_SETTING_FORMAT_FAILURE, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            //根据次数判断一下取样时间是否有误
            for (int i = 0; i < sampletimes; i++)
            {
                if (m_EdittingDissolutionMethod.Sample_Time_Arr[i].TimeValue == 0)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_SAMPLE_TIME_SETTING_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            //判断总时长
            int alltime;
            try
            {
                alltime = int.Parse(PARA2_ALLTIMESPAN.Text.Trim());
                if (alltime > 99999)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_ALLTIME_SETTING_OUTLINE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //总时长不能小于最后一次取样结束的时间
                if (alltime < m_EdittingDissolutionMethod.Sample_Time_Arr[sampletimes - 1].TimeValue + StaticParam.SAMPLE_MIN_INTERVAL)
                {
                    MessageBox.Show(App.m_LangPackage.TIP_ALLTIME_SETTING_SCOPE_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_ALLTIME_SETTING_FORMAT_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            //判断转桨电机转速模式和电机模式
            if (string.IsNullOrEmpty(m_EdittingDissolutionMethod.oSpeedModule.SpeedMode)
                || string.IsNullOrEmpty(m_EdittingDissolutionMethod.oSpeedModule.ElectricmotorMmode))
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROTATESPEED_SETTING_EXSIT_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //判断转速和启动时间
            if (m_EdittingDissolutionMethod.oSpeedModule.FrontRowSpeed_1 == 0)
            {
                MessageBox.Show(App.m_LangPackage.TIP_ROTATESPEED_FRONTROW1_SETTING_NOTZORE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //判断转桨启动时间是不是在总时长范围内
            if (alltime < m_EdittingDissolutionMethod.oSpeedModule.FrontRowStartTime_2
                || alltime < m_EdittingDissolutionMethod.oSpeedModule.BackRowStartTime_2)
            {
                MessageBox.Show(App.m_LangPackage.TIP_ALLTIME_SETTING_MISMATCHING_BACKROW_STARTTIME, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            //判断梯度转速启动时间是否与取样时间

            //判断取样体积和初滤液体积之和是否大于23
            try
            {
                double samplevolume = double.Parse(PARA3_SAMPLEVOLUME.Text.Trim());
                double firstfiltervolume = double.Parse(PARA3_FIRSTFILTERVOLUME.Text.Trim());
                int nUpperlimit = 23;
                if (samplevolume + firstfiltervolume > nUpperlimit)
                {
                    MessageBox.Show(string.Format(App.m_LangPackage.TIP_SAMPLEVOLUME_ADD_FIRSTFILTER_OUTLINE, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception e1)
            {
                Console.Write(e1.ToString());
                MessageBox.Show(App.m_LangPackage.TIP_SAMPLEVOLUME_ADD_FIRSTFILTER_FORMAT_ERROR, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
            }


            //判断是否启用稀释
            bool bDilutionEnabled = false;
            int nDilutionRatio = 0;
            if (PARA3_CHK_DILUTIONENABLED.IsChecked == true)
            {
                bDilutionEnabled = true;
                if (!string.IsNullOrEmpty(PARA3_COMBO_DILUTIONRATIO.Text))
                    nDilutionRatio = int.Parse(PARA3_COMBO_DILUTIONRATIO.Text.ToString());
                else
                {
                    MessageBox.Show(App.m_LangPackage.TIP_DISOLUTION_RATIO_NULL, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //判断取样体积是否在【1-3】之间
                try
                {
                    double samplevolume = double.Parse(PARA3_SAMPLEVOLUME.Text.Trim());
                    int nUpperlimit = 3;
                    int nLowerLimit = 1;
                    if (samplevolume < nLowerLimit || samplevolume > nUpperlimit)
                    {
                        MessageBox.Show(string.Format(App.m_LangPackage.TIP_LAUNCH_DISOLUTION_SAMPLEVOLUME_SCOPE, nLowerLimit, nUpperlimit), App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    double dilutionratio = double.Parse(PARA3_COMBO_DILUTIONRATIO.Text.Trim());
                    if (samplevolume * (dilutionratio + 1) > 20)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_DISOLUTION_RATIO_OUTLINE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                catch (Exception e1)
                {
                    Console.Write(e1.ToString());
                    MessageBox.Show(App.m_LangPackage.TIP_SAMPLE_VOLUME_SETTING_FORMAT_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                //判断总时长可满足稀释时间要求
                try
                {
                    alltime = int.Parse(PARA2_ALLTIMESPAN.Text.Trim());
                    int dilutiontimes = sampletimes * 2;
                    if (alltime < dilutiontimes + m_EdittingDissolutionMethod.Sample_Time_Arr[sampletimes - 1].TimeValue + StaticParam.SAMPLE_MIN_INTERVAL)
                    {
                        MessageBox.Show(App.m_LangPackage.TIP_ALLTIME_SETTING_MISMATCHING_DISOLUTION, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                catch (Exception e1)
                {
                    Console.Write(e1.ToString());
                    MessageBox.Show(App.m_LangPackage.TIP_ALLTIME_SETTING_FORMAT_FAILURE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            try
            {
                //参数1
                m_EdittingDissolutionMethod.MethodName = PARA1_METHODNAME.Text.Trim();
                m_EdittingDissolutionMethod.Manufacturer = PARA1_MANUFACTURER.Text.Trim();
                m_EdittingDissolutionMethod.BatchNo = PARA1_BATCHNO.Text.Trim();
                m_EdittingDissolutionMethod.TestDepartment = PARA1_TESTDEPARTMENT.Text.Trim();
                //参数2
                m_EdittingDissolutionMethod.DissolutionMethodName = PARA2_COMBO_DISSOLUTIONMETHODNAME.Text.Trim();//溶出方法
                m_EdittingDissolutionMethod.TempSetting = double.Parse(PARA2_TEMPSETTING.Text.Trim());//温度设置
                m_EdittingDissolutionMethod.SolventVolume = double.Parse(PARA2_SOLUTIONVOLUME.Text.Trim());//溶液体积
                //参数3
                m_EdittingDissolutionMethod.SampleTimes = int.Parse(PARA3_SAMPLETIMES.Text.Trim());//取样次数
                m_EdittingDissolutionMethod.SampleVolume = double.Parse(PARA3_SAMPLEVOLUME.Text.Trim());//取样体积
                m_EdittingDissolutionMethod.Auto_Fluid_Infusion = PARA3_COMBO_AUTOSUPPLY.Text.ToString();//自动补液
                m_EdittingDissolutionMethod.First_filter_volume = double.Parse(PARA3_FIRSTFILTERVOLUME.Text.Trim());//初滤体积
                m_EdittingDissolutionMethod.AllTimespan = int.Parse(PARA2_ALLTIMESPAN.Text.Trim()); //总时长
                                                                                                    //m_EdittingDissolutionMethod.Sample_Time_Arr = m_SampleTimeArr; //取样时间数组
                m_EdittingDissolutionMethod.MethodTime = BaseUtils.GetCurrentDateTime();

                m_EdittingDissolutionMethod.DilutionEnabled = bDilutionEnabled;
                m_EdittingDissolutionMethod.DilutionRatio = nDilutionRatio;

            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return false;
            }

            return true;
        }
        //模型绑定数据至视图
        private bool BindModelToView()
        {
            PARA1_METHODNAME.Text = m_EdittingDissolutionMethod.MethodName;
            PARA1_MANUFACTURER.Text = m_EdittingDissolutionMethod.Manufacturer;
            PARA1_BATCHNO.Text = m_EdittingDissolutionMethod.BatchNo;
            PARA1_TESTDEPARTMENT.Text = m_EdittingDissolutionMethod.TestDepartment;

            PARA2_COMBO_DISSOLUTIONMETHODNAME.Text = m_EdittingDissolutionMethod.DissolutionMethodName;
            PARA2_TEMPSETTING.Text = m_EdittingDissolutionMethod.TempSetting.ToString();
            PARA2_SOLUTIONVOLUME.Text = m_EdittingDissolutionMethod.SolventVolume.ToString();
            PARA2_ALLTIMESPAN.Text = m_EdittingDissolutionMethod.AllTimespan.ToString();

            PARA3_SAMPLEVOLUME.Text = m_EdittingDissolutionMethod.SampleVolume.ToString();
            PARA3_SAMPLETIMES.Text = m_EdittingDissolutionMethod.SampleTimes.ToString();
            PARA3_COMBO_AUTOSUPPLY.Text = m_EdittingDissolutionMethod.Auto_Fluid_Infusion;
            PARA3_FIRSTFILTERVOLUME.Text = m_EdittingDissolutionMethod.First_filter_volume.ToString();

            return true;
        }
        //绑定数据到生产模型
        private bool BindDataToRuntimeModel()
        {
            RT_METHODNAME.Content = m_EdittingDissolutionMethod.MethodName;
            RT_MANUFACTURER.Content = m_EdittingDissolutionMethod.Manufacturer;
            RT_BATCHNO.Content = m_EdittingDissolutionMethod.BatchNo;
            RT_TESTDEPARTMENT.Content = m_EdittingDissolutionMethod.TestDepartment;
            RT_DISSOLUTIONMETHODNAME.Content = m_EdittingDissolutionMethod.DissolutionMethodName;
            RT_ALLTIMESPAN.Content = m_EdittingDissolutionMethod.AllTimespan.ToString();
            RT_SAMPLEVOLUME.Content = m_EdittingDissolutionMethod.SampleVolume.ToString();
            RT_SAMPLETIMES.Content = m_EdittingDissolutionMethod.SampleTimes.ToString();
            RT_AUTOFLUIDINFUSION.Content = m_EdittingDissolutionMethod.Auto_Fluid_Infusion;
            RT_FIRSTFILTERVOLUME.Content = m_EdittingDissolutionMethod.First_filter_volume.ToString();
            RT_TEMPSETTING.Content = m_EdittingDissolutionMethod.TempSetting.ToString();
            RT_SOLUTIONVOLUME.Content = m_EdittingDissolutionMethod.SolventVolume.ToString();
            RT_FRONTROW_SPEED_1.Content = m_EdittingDissolutionMethod.oSpeedModule.FrontRowSpeed_1.ToString();
            RT_FRONTROW_SPEED_2.Content = m_EdittingDissolutionMethod.oSpeedModule.FrontRowSpeed_2.ToString();
            RT_BACKROW_SPEED_1.Content = m_EdittingDissolutionMethod.oSpeedModule.BackRowSpeed_1.ToString();
            RT_BACKROW_SPEED_2.Content = m_EdittingDissolutionMethod.oSpeedModule.BackRowSpeed_2.ToString();
            RT_DILUTIONENABLED.Content = m_EdittingDissolutionMethod.DilutionEnabled ? (App.g_EngVer ? "Yes" : "是") : (App.g_EngVer ? "No" : "否");     //"是" : "否";
            RT_DILUTIONRATIO.Content = m_EdittingDissolutionMethod.DilutionEnabled ? m_EdittingDissolutionMethod.DilutionRatio.ToString() : "-----"; ;
            //将编辑模型设置为产线模型
            m_ProductionDissolutionMethod = Tools.XmlDeepCopy<DissolutionMethod>(m_EdittingDissolutionMethod);
            return true;
        }
        //检查是否设置了速度 
        private void SettingSpeedStatus()
        {
            if (!String.IsNullOrEmpty(m_EdittingDissolutionMethod.oSpeedModule.SpeedMode)
                || !String.IsNullOrEmpty(m_EdittingDissolutionMethod.oSpeedModule.ElectricmotorMmode))
            {
                ET_SPEED_SET.Visibility = Visibility.Visible;
            }
            else
                ET_SPEED_SET.Visibility = Visibility.Hidden;
            return;
        }
        //检查是否设置了取样时间
        private void SettingSampleTimeStatus()
        {
            bool bEmpty = false;
            for (int i = 0; i < m_EdittingDissolutionMethod.Sample_Time_Arr.Length; i++)
            {
                if (m_EdittingDissolutionMethod.Sample_Time_Arr[i].TimeValue != 0)
                {
                    bEmpty = true;
                    break;
                }
            }
            if (bEmpty)
                ET_SAMPLETIME_SET.Visibility = Visibility.Visible;
            else
                ET_SAMPLETIME_SET.Visibility = Visibility.Hidden;
            return;
        }
        //设置稀释状态
        private void SettingDilutionEnabledStatus()
        {
            PARA3_CHK_DILUTIONENABLED.IsChecked = m_EdittingDissolutionMethod.DilutionEnabled;
            PARA3_COMBO_DILUTIONRATIO.Text = (PARA3_CHK_DILUTIONENABLED.IsChecked == true) ?
                m_EdittingDissolutionMethod.DilutionRatio.ToString() : string.Empty;
        }
        //清空编辑区域
        private void EmptyEditView()
        {
            foreach (Control c in ET_BASEPARA.Children)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Text = String.Empty;
            }
            foreach (Control c in ET_SOLUTIONPARA.Children)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Text = String.Empty;
            }
            foreach (Control c in ET_SAMPLEPARA.Children)
            {
                if (c.GetType() == typeof(TextBox))
                    ((TextBox)c).Text = String.Empty;
            }
            PARA2_COMBO_DISSOLUTIONMETHODNAME.Text = null;
            PARA3_COMBO_AUTOSUPPLY.Text = null;
            PARA3_CHK_DILUTIONENABLED.IsChecked = false;
            PARA3_COMBO_DILUTIONRATIO.Text = null;
            ET_SPEED_SET.Visibility = Visibility.Hidden;
            ET_SAMPLETIME_SET.Visibility = Visibility.Hidden;
        }
        //清空运行区域
        private void EmptyRunningView()
        {
            foreach (Control c in RT_CONSOLE.Children)
            {
                if (c.GetType() == typeof(Label))
                {
                    if (c.Name.ToString().StartsWith("RT_"))
                    {
                        ((Label)c).Content = StaticParam.NULLSING;
                    }
                }
            }
        }
        //清空小杯温度区域
        public void EmptyCupView()
        {
            RT_CUP_GROUP.Dispatcher.Invoke(new Action(delegate
            {
                foreach (Control c in RT_CUP_GROUP.Children)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        if (c.Name.ToString().StartsWith("RT_CUP"))
                        {
                            UIOperator.SetLabelContent((Label)c, StaticParam.NULLSING);
                        }
                    }
                }
            }));
        }
        //方法存储
        private void SaveMethod()
        {
            //获取数据并绑定 
            if (!BindViewToModel()) return;
            DataSet ds = new DataSet();
            //判断同名方法是否存在
            string strCmd = String.Format(sql.SQL.SQL_R_FINDMETHODBYNAME, m_EdittingDissolutionMethod.MethodName); //strMethodName 参数
            App.m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_METHOD);
            if (ds.Tables[sql.SQL.T_METHOD].Rows.Count == 0) //该方法名不存在
            {
                //提示创建等待确认
                MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_CREATE_METHOD_CONFIRM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        new DataPersistent(m_EdittingDissolutionMethod, "CREATE"); //创建方法
                        App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(20), App.GetBehaviorRemark(20)));
                        break;
                }
            }
            else
            {
                //提示更新等待确认
                MessageBoxResult mBoxResult = MessageBox.Show(string.Format(App.m_LangPackage.TIP_UPDATE_METHOD_CONFIRM, m_EdittingDissolutionMethod.MethodName), App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (mBoxResult)
                {
                    case MessageBoxResult.Yes:
                        new DataPersistent(m_EdittingDissolutionMethod, "UPDATE"); //更新方法
                        App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(20), App.GetBehaviorRemark(20)));
                        break;
                }
            }
            return;
        }
        //方法调用
        private bool CallMethod()
        {
            LoadMethodModal loadMethodModal = new LoadMethodModal(CallBack_LoadMethod, CallBack_DelMethod);
            loadMethodModal.ShowDialog();
            return true;
        }
        //参数发送
        private bool LaunchParam()
        {
            if (!BindViewToModel()) return false;
            if (!BindDataToRuntimeModel()) return false;
            m_EdittingDissolutionMethod.Reset();
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(24), App.GetBehaviorRemark(24)));
            EmptyEditView();
            return true;
        }
        //实验前准备工作
        private bool PreworkExperiment()
        {
            if (App.m_bCupTempEnable)
            {
                //先判断温度
                if (!IsTempQualified(m_RuntimeDissolutionMethod.TempSetting))
                {
                    MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_TEMPINCUP_UNQUALIFIED, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (mBoxResult)
                    {
                        case MessageBoxResult.No:
                            {
                                double tempsetting = m_RuntimeDissolutionMethod.TempSetting;
                                App.GetMainWindow().HeatLightControl(true);
                                Task t = Task.Run(() =>
                                {
                                    App.GetMainWindow().Heat(tempsetting);
                                });
                                return false;
                            }
                    }
                }
            }
            else
            {
                //先判断温度
                if (!IsTempTankQualified(m_RuntimeDissolutionMethod.TempSetting))
                {
                    MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_TEMPINTANK_UNQUALIFIED, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (mBoxResult)
                    {
                        case MessageBoxResult.No:
                            {
                                double tempsetting = m_RuntimeDissolutionMethod.TempSetting;
                                App.GetMainWindow().HeatLightControl(true);
                                Task t = Task.Run(() =>
                                {
                                    App.GetMainWindow().Heat(tempsetting);
                                });
                                return false;
                            }
                    }
                }
            }
            return true;
        }
        //开始实验
        private void ExperimentStart()
        {
            //将产线模型设置为运行模型
            m_RuntimeDissolutionMethod = Tools.XmlDeepCopy<DissolutionMethod>(m_ProductionDissolutionMethod);
            if (!PreworkExperiment()) return;
            string strLog = String.Format(App.GetBehaviorRemark(200), m_RuntimeDissolutionMethod.MethodName, m_RuntimeDissolutionMethod.BatchNo);
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(3), App.GetBehavior(200), strLog));
            //转速对队清空
            App.m_SpeedSwitchQueue.Clear();
            //实验开始标志置位
            App.m_bExperimentRunning = true;
            //实验暂停标志置位
            App.m_bExperimentPause = false;
            //切换不可并行功能
            SwitchUnParallelFunction(false);
            //关闭手动操作按钮
            SwitchManOperateView(false);
            UIOperator.SetComponetValidAndOpacity(BTN_EXPPAUSE, false);
            UIOperator.SetComponetValidAndOpacity(BTN_EXPEND, true);
            //设置动画指示运行
            App.SetAnimationStatus(true);
            //创建实验记录
            CreateNewExperimentRecond();
            //分配杯内温度矩阵
            m_RunCupTempArr = new double[m_TestData.SampleTimes, StaticParam.CUP_NUM];
            //打开加热灯
            App.GetMainWindow().HeatLightControl(true);
            //实验初始化 
            m_ExperimentThread = new Thread(() => InitExperiment());
            m_ExperimentThread.Start();
            //初始化取样定时器进入就绪状态
            m_SampleToRunDispatcherTimer = new DispatcherTimer();
            m_SampleToRunDispatcherTimer.Tick += new EventHandler(SampleToRunTimer_Tick);
            m_SampleToRunDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            return;
        }
        //创建新的实验记录
        private void CreateNewExperimentRecond()
        {
            m_nCurrentSampleTimes = 0;             //取样次第归零
            m_nExperimentCounter = 0;              //实验秒计时
            m_nAlltimespan = m_RuntimeDissolutionMethod.AllTimespan * 60;  //总时长(分钟->秒）
            m_TestData = new TestData();
            try
            {
                m_TestData.LoginName = App.g_TSession.TTUser.LoginName;
                m_TestData.Manufacturer = m_RuntimeDissolutionMethod.Manufacturer;
                m_TestData.MethodName = m_RuntimeDissolutionMethod.MethodName;
                m_TestData.SampleTimes = m_RuntimeDissolutionMethod.SampleTimes;
                m_TestData.BatchNo = m_RuntimeDissolutionMethod.BatchNo;
                m_TestData.TestMethod = m_RuntimeDissolutionMethod.DissolutionMethodName;
                m_TestData.SampleVolume = m_RuntimeDissolutionMethod.SampleVolume;
                m_TestData.SolventVolume = m_RuntimeDissolutionMethod.SolventVolume;
                m_TestData.SpeedMode = m_RuntimeDissolutionMethod.oSpeedModule.SpeedMode;
                m_TestData.StartTime = BaseUtils.GetCurrentDateTime();
                m_TestData.EndTime = String.Empty;
                m_TestData.Status = StaticParam.ExpStatusType[0];
                m_TestData.TempSetting = m_RuntimeDissolutionMethod.TempSetting;
                m_TestData.TestDepartment = m_RuntimeDissolutionMethod.TestDepartment;
                m_TestData.FrontRowSpeed_1 = m_RuntimeDissolutionMethod.oSpeedModule.FrontRowSpeed_1;
                m_TestData.FrontRowSpeed_2 = m_RuntimeDissolutionMethod.oSpeedModule.FrontRowSpeed_2;
                m_TestData.BackRowSpeed_1 = m_RuntimeDissolutionMethod.oSpeedModule.BackRowSpeed_1;
                m_TestData.BackRowSpeed_2 = m_RuntimeDissolutionMethod.oSpeedModule.BackRowSpeed_2;
                m_TestData.FrontRowStartTime_1 = m_RuntimeDissolutionMethod.oSpeedModule.FrontRowStartTime_1;
                m_TestData.FrontRowStartTime_2 = m_RuntimeDissolutionMethod.oSpeedModule.FrontRowStartTime_2;
                m_TestData.BackRowStartTime_1 = m_RuntimeDissolutionMethod.oSpeedModule.BackRowStartTime_1;
                m_TestData.BackRowStartTime_2 = m_RuntimeDissolutionMethod.oSpeedModule.BackRowStartTime_2;
                m_TestData.AllTimeSpan = m_RuntimeDissolutionMethod.AllTimespan;
                m_TestData.First_Filter_Volume = m_RuntimeDissolutionMethod.First_filter_volume;
                m_TestData.Auto_Fluid_Infusion = m_RuntimeDissolutionMethod.Auto_Fluid_Infusion;
                m_TestData.ElectricmotorMode = m_RuntimeDissolutionMethod.oSpeedModule.ElectricmotorMmode;
                m_TestData.DilutionEnabled = m_RuntimeDissolutionMethod.DilutionEnabled;
                m_TestData.DilutionRatio = m_RuntimeDissolutionMethod.DilutionRatio;
                for (int i = 0; i < StaticParam.SAMPLE_TIME; i++)
                    m_TestData.Sample_Time_Arr[i] = m_RuntimeDissolutionMethod.Sample_Time_Arr[i];
                m_TestData.TempWaterBox = double.Parse(RT_WATERBOXTEMP.Content.ToString());
            }
            catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.ToString()); }

        }
        private void PutPillAndRotationTimer_Tick(object sender, EventArgs e)
        {
            if (m_nRemainSecond++ >= StaticParam.PUTPILL_ROTATE_TIME)
            {
                m_PutPillAndRotationTimer.Stop();
                m_PutPillAndRotationTimer = null;

                ExpStepAction.SuspendTempCollect(); //挂起温度采集
                ExpStepAction.Rotation(this, 0, StaticParam.ElectricMotor.SIGNLE);
                ExpStepAction.ResumeTempCollect(); //恢复温度采集

                App.SetSystemStatus("搅拌完成");
                App.Pause();
                m_nRemainSecond = 1;
                m_SampleToRunDispatcherTimer.Start();
                return;
            }
            App.SetCountStatus(m_nRemainSecond.ToString() + "s");
        }
        //取样定时任务
        private void SampleToRunTimer_Tick(object sender, EventArgs e)
        {
            if (App.m_bExperimentPause) return;
            if (m_nExperimentCounter < m_nAlltimespan)
            {
                //倒计时
                UIOperator.SetLabelContent(RT_REMAINTIME, BaseUtils.GetHHMMSSRemainTime(--m_nRemainSecond));
                //实时计时器
                App.SetCountStatus(BaseUtils.GetHHMMSSRemainTime(m_nExperimentCounter));

                //判断当前取样第次
                if (m_nExperimentCounter == (m_RuntimeDissolutionMethod.Sample_Time_Arr[m_nCurrentSampleTimes].TimeValue - 1) * 60)
                {
                    if (m_nCurrentSampleTimes < m_RuntimeDissolutionMethod.SampleTimes)
                    {
                        MotorSwivelGradient(m_nExperimentCounter);
                        App.SetSystemStatus(String.Format("执行第{0}次取样前期准备工作", m_nCurrentSampleTimes + 1));
                        App.m_bSamplingOn = true;
                        UIOperator.SetComponetValidAndOpacity(BTN_EXPPAUSE, false);//关闭暂停键
                        //创建线程执行取样准备工作流程
                        m_SamplePrepareProcThread = new Thread(() => SamplePrepareProc());
                        m_SamplePrepareProcThread.Start();
                    }
                }
                else if (m_nExperimentCounter == m_RuntimeDissolutionMethod.Sample_Time_Arr[m_nCurrentSampleTimes].TimeValue * 60)
                {
                    if (m_nCurrentSampleTimes < m_RuntimeDissolutionMethod.SampleTimes)
                    {
                        MotorSwivelGradient(m_nExperimentCounter);
                        App.SetSystemStatus(String.Format("执行第{0}次取样工作", m_nCurrentSampleTimes + 1));
                        //当前取样时间点
                        UIOperator.SetLabelContent(RT_CURRENT_SAMPLE_TIME, m_RuntimeDissolutionMethod.Sample_Time_Arr[m_nCurrentSampleTimes].TimeValue.ToString());
                        //下次取样时间点
                        if (m_nCurrentSampleTimes < m_RuntimeDissolutionMethod.SampleTimes - 1)
                            UIOperator.SetLabelContent(RT_NEXT_SAMPLE_TIME, m_RuntimeDissolutionMethod.Sample_Time_Arr[m_nCurrentSampleTimes + 1].TimeValue.ToString());
                        else
                            UIOperator.SetLabelContent(RT_NEXT_SAMPLE_TIME, "无");
                        //当前取样第次
                        UIOperator.SetLabelContent(RT_CURRENTSAMPLETIMES, (m_nCurrentSampleTimes + 1).ToString());
                        //小杯实时温度获取
                        if (App.m_bCupTempEnable)
                        {
                            try
                            {
                                for (int i = 0; i < App.m_nCupNum; i++)
                                {
                                    m_RunCupTempArr[m_nCurrentSampleTimes, i] = App.g_cuptemp[i];
                                    m_TestData.CupTemp_Arr[i] = App.g_cuptemp[i].ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                App.WriteSystemLog(ex.ToString());
                            }
                        }
                        m_nCurrentSampleTimes++;
                        //创建线程执行取样流程
                        double pump_volume = m_RuntimeDissolutionMethod.SampleVolume + m_RuntimeDissolutionMethod.First_filter_volume + 2;
                        if (m_RuntimeDissolutionMethod.SampleVolume <= 18) pump_volume += 3;
                        m_SampleProcThread = new Thread(() =>
                        SampleProc(m_RuntimeDissolutionMethod.SampleVolume,
                            pump_volume,
                            m_RuntimeDissolutionMethod.First_filter_volume,
                            m_RuntimeDissolutionMethod.Auto_Fluid_Infusion,
                            m_nCurrentSampleTimes));
                        m_SampleProcThread.Start();
                    }
                }
                else
                {
                    //判断当前时刻是否有电机启动事件，如果有加入队列
                    MotorSwivelGradient(m_nExperimentCounter);
                }

                m_nExperimentCounter++;   //实验运行计时秒数加1
            }
            else
            {
                //实验倒计时结束更新实验结束时间
                DateTime dt = DateTime.Parse(m_TestData.StartTime).AddMinutes(m_TestData.AllTimeSpan);
                TerminateExperiment(dt, StaticParam.ExpStatusType[(int)StaticParam.ExpStatus.WHOLE]);
            }
        }
        //电机梯度转进程
        public void MotorSwivelGradient(int time)
        {
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.FrontRowStartTime_1 * 60)
            {
                //前排电机转动时间1到
                ExpStepAction.Rotation(this, m_RuntimeDissolutionMethod.oSpeedModule.FrontRowSpeed_1, StaticParam.ElectricMotor.SIGNLE); //旋转         
                Thread.Sleep(500);
            }
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.BackRowStartTime_1 * 60)
            {
                //后排电机转动时间1到
                if (m_RuntimeDissolutionMethod.oSpeedModule.ElectricmotorMmode.Equals(StaticParam.Electricmotor_Mode_Arr[1])
                 ) //双电机
                {
                    ExpStepAction.Rotation(this, m_RuntimeDissolutionMethod.oSpeedModule.BackRowSpeed_1, StaticParam.ElectricMotor.DOUBLE); //旋转
                    Thread.Sleep(500);
                }
            }
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.FrontRowStartTime_2 * 60)
            {
                //前排电机转动时间2到
                if (m_RuntimeDissolutionMethod.oSpeedModule.SpeedMode.Equals(StaticParam.Speed_Mode_Arr[1])
                    ) //梯度转
                {
                    ExpStepAction.Rotation(this, m_RuntimeDissolutionMethod.oSpeedModule.FrontRowSpeed_2, StaticParam.ElectricMotor.SIGNLE); //旋转         
                    Thread.Sleep(500);
                }
            }
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.BackRowStartTime_2 * 60)
            {
                //后排电机转动时间2到
                if (m_RuntimeDissolutionMethod.oSpeedModule.ElectricmotorMmode.Equals(StaticParam.Electricmotor_Mode_Arr[1])
                   && m_RuntimeDissolutionMethod.oSpeedModule.SpeedMode.Equals(StaticParam.Speed_Mode_Arr[1])
                    ) //双电机,梯度转
                {
                    ExpStepAction.Rotation(this, m_RuntimeDissolutionMethod.oSpeedModule.BackRowSpeed_2, StaticParam.ElectricMotor.DOUBLE); //旋转
                    Thread.Sleep(500);
                }
            }
        }
        //电机梯度转进程
        public void MotorSwivelGradientQueue(int time)
        {
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.FrontRowStartTime_1 * 60)
            {
                //前排电机转动时间1到
                int nSwivel = m_RuntimeDissolutionMethod.oSpeedModule.FrontRowSpeed_1;
                string strDatagram = String.Format(Datagram.V1_E2_ROTATION, CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[1], CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[0]);
                App.m_SpeedSwitchQueue.Enqueue(strDatagram);
            }
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.BackRowStartTime_1 * 60)
            {
                //后排电机转动时间1到
                if (m_RuntimeDissolutionMethod.oSpeedModule.ElectricmotorMmode.Equals(StaticParam.Electricmotor_Mode_Arr[1])) //双电机
                {
                    int nSwivel = m_RuntimeDissolutionMethod.oSpeedModule.BackRowSpeed_1;
                    string strDatagram = String.Format(Datagram.V1_E3_ROTATION, CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[1], CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[0]);
                    App.m_SpeedSwitchQueue.Enqueue(strDatagram);
                }
            }
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.FrontRowStartTime_2 * 60)
            {
                //前排电机转动时间2到
                if (m_RuntimeDissolutionMethod.oSpeedModule.SpeedMode.Equals(StaticParam.Speed_Mode_Arr[1])
                    ) //梯度转
                {
                    int nSwivel = m_RuntimeDissolutionMethod.oSpeedModule.FrontRowSpeed_2;
                    string strDatagram = String.Format(Datagram.V1_E2_ROTATION, CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[1], CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[0]);
                    App.m_SpeedSwitchQueue.Enqueue(strDatagram);
                }
            }
            if (time == m_RuntimeDissolutionMethod.oSpeedModule.BackRowStartTime_2 * 60)
            {
                //后排电机转动时间2到
                if (m_RuntimeDissolutionMethod.oSpeedModule.ElectricmotorMmode.Equals(StaticParam.Electricmotor_Mode_Arr[1])
                   && m_RuntimeDissolutionMethod.oSpeedModule.SpeedMode.Equals(StaticParam.Speed_Mode_Arr[1])) //双电机,梯度转
                {
                    int nSwivel = m_RuntimeDissolutionMethod.oSpeedModule.BackRowSpeed_2;
                    string strDatagram = String.Format(Datagram.V1_E3_ROTATION, CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[1], CommonLib.CommonLib.IntToHexArr((nSwivel + App.m_nSpeedOffset) * App.m_nRotateRate)[0]);
                    App.m_SpeedSwitchQueue.Enqueue(strDatagram);
                }
            }
        }
        //实验线程
        public bool InitExperiment()
        {
            ExpStepAction.SuspendTempCollect(); //挂起温度采集
            //仪器初始化
            if (App.m_bCupTempEnable && App.m_bExperimentRunning)
                ExpStepAction.SuspendCupTempCollect();                //挂起杯内温度采集

            App.GetMainWindow().Heat(m_RuntimeDissolutionMethod.TempSetting);


            ExpStepAction.SuspendTempCollect(); //挂起温度采集
            //仪器初始化
            if (App.m_bCupTempEnable && App.m_bExperimentRunning)
                ExpStepAction.SuspendCupTempCollect();                //挂起杯内温度采集



            App.SetSystemStatus("开始初始化。。。");
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(3), App.GetBehavior(202), App.GetBehaviorRemark(202)));
            BaseUtils.TurnOnRedLight(this.IMG_L_INIT);

            bool bStatus;
            if (App.m_bSoftSelfCheck)  //软件自检
            {
                bStatus = ExpStepAction.Initialize();
            }
            else
            {
                bStatus = ExpStepAction.Self_Initialize(this);
            }

            if (!bStatus)
            {
                //初始化失败
                App.SetSystemStatus("初始化失败,请检查！");
                BaseUtils.TurnOnGreenLight(this.IMG_L_INIT);
                ExpStepAction.ResumeTempCollect(); //恢复温度采集
                return false;
            }
            else
            {
                BaseUtils.TurnOnGreenLight(this.IMG_L_INIT);
                App.SetSystemStatus("初始化完成！");
            }

            //投药
            if (!App.m_bExperimentRunning) return false;
            if (App.m_bPutPillEnable)
            {
                ExpStepAction.PutPill(this); //投药机构打开
            }
            //搅拌桨转
            if (!App.m_bExperimentRunning) return false;
            //投药机构复位
            if (App.m_bPutPillEnable)
            {
                ExpStepAction.PillReset(this); //投药机构复位
            }
            m_nRemainSecond = m_nAlltimespan;  //将剩余时间等于总时长
            App.m_bSamplingOn = true;          //设置取样中标志
            App.g_SamplePointPos = Formula.Calculate_SamplePointPosition(m_RuntimeDissolutionMethod.SolventVolume, m_RuntimeDissolutionMethod.DissolutionMethodName);
            m_SampleToRunDispatcherTimer.Start();  //取样线程启动

            ExpStepAction.ResumeTempCollect();     //恢复温度采集

            //更新实验开始时间
            m_TestData.StartTime = BaseUtils.GetCurrentDateTime();

            return true;

        }
        //实验结束
        public bool TerminateExperiment(DateTime dt, string expStatus)
        {
            App.m_bExperimentRunning = false;                    //实验运行中标志撤位
            m_nExperimentCounter = 99999999;
            //实验倒计时结束更新实验结束时间
            m_TestData.EndTime = BaseUtils.GetCurrentDateTime(dt);
            m_TestData.Status = expStatus;
            new DataPersistent(m_TestData);                      //持久化实验记录数据

            if (App.m_bCupTempEnable)
                new DataPersistent(m_TestData, m_RunCupTempArr);  //持久化各取样点杯内温度值

            ExpStepAction.SuspendTempCollect();                  //挂起温度采集

            ExpStepAction.Initialize();                          //恢复初始化状态
            BaseUtils.TurnOnRedLight(IMG_L_EXPERIMENTOVER);      //关闭桨转灯

            //关闭取样定时器                                                     
            if (m_SampleToRunDispatcherTimer != null)
            {
                m_SampleToRunDispatcherTimer.Stop();
                m_SampleToRunDispatcherTimer = null;
            }
            SwitchUnParallelFunction(true);

            EmptyRunningView();                                  //清空工作区域
            App.SetSystemStatus("实验结束！");
            BaseUtils.TurnOnGreenLight(IMG_L_IMPELLERTURN);

            //设置动画指示运行结束
            App.SetAnimationStatus(false);
            //取样标志关闭，暂停键关闭
            App.m_bSamplingOn = false;
            UIOperator.SetComponetValidAndOpacity(BTN_EXPPAUSE, false);
            UIOperator.SetComponetValidAndOpacity(BTN_EXPEND, false);
            //计时器清除
            App.SetCountStatus(String.Empty);
            UIOperator.SetButtonContent(BTN_EXPEND, App.m_LangPackage.BTN_EXPEND);
            //提示自定义报告参数
            NameReportModal nameReportModal = new NameReportModal(CallBackPrintReport, App.m_LangPackage.RPT_TITLE,
                string.Format("report_{0}", BaseUtils.GetDateTimeString(DateTime.Parse(m_TestData.StartTime))), null, null, null, null);
            nameReportModal.ShowDialog();

            BaseUtils.TurnOnGreenLight(IMG_L_EXPERIMENTOVER);

            string strLog = String.Format(App.GetBehaviorRemark(201), m_RuntimeDissolutionMethod.MethodName, m_RuntimeDissolutionMethod.BatchNo);
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(3), App.GetBehavior(201), strLog));
            ExpStepAction.ResumeTempCollect();                   //恢复温度采集
            return true;
        }
        //回调打印报告
        public void CallBackPrintReport(string reporttile, string reportname, string reviewer
            , string reviewerid, string content, string reportdatetime)
        {
            new Printer(m_TestData).PrintPDF(reporttile, reportname, reviewer, reviewerid, content, reportdatetime, m_RunCupTempArr);  //保存PDF报告
            m_RunCupTempArr = null;
        }
        //取样准备工作流程
        public bool SamplePrepareProc()
        {
            App.SetSystemStatus("取样前一分钟准备工作开始！");
            App.g_CurrentInjectorSampleVolume = 0;                  //注射器内溶液清零
            ExpStepAction.SuspendTempCollect();                     //挂起温度采集线程

            if (App.m_bSamplePointEnable && App.m_bExperimentRunning)   //取样针下降
                ExpStepAction.SamplePointDown(this, App.g_SamplePointPos, m_RuntimeDissolutionMethod.DissolutionMethodName);       //取样针下降到指定位置,position为杯中刻度

            if (App.m_bWholeModeEnable && App.m_bExperimentRunning)
                ExpStepAction.ValveRotation(this, 2, "取样位");      //换向阀转动到取样位

            if (App.m_bWholeModeEnable)
            {
                if (App.m_bExperimentRunning)
                {
                    App.g_CurrentInjectorSampleVolume = 20;
                    ExpStepAction.InjectorPump(this, App.g_CurrentInjectorSampleVolume, true);   //注射器开始取液20ml
                }
                if (App.m_bExperimentRunning)
                    ExpStepAction.ValveRotation(this, 3, "循环位");      //换向阀转动到循环位（1）
                if (App.m_bExperimentRunning)
                {
                    ExpStepAction.InjectDrain(this, 0, true);             //注射器打出液体20ml,此处全部打完
                    App.g_CurrentInjectorSampleVolume -= 20;
                }
            }

            if (App.m_bSampleBeep && App.m_bExperimentRunning) ExpStepAction.Beep(true);     //打开蜂鸣器

            if (App.m_bCupTempEnable && App.m_bExperimentRunning)
                ExpStepAction.ResumeCupTempCollect();                //恢复小杯温度采集
            ExpStepAction.ResumeTempCollect();                       //恢复温度采集线程

            App.SetSystemStatus("取样前准备工作完成，等待取样！");

            return true;
        }
        //取样工作流程
        public bool SampleProc(double sample_volume, double pump_volume, double first_filter_volume,
                               string auto_fluid_infusion, int curr_sample_times)
        {
            ExpStepAction.SuspendTempCollect();                         //挂起温度采集线程
            if (App.m_bSampleBeep && App.m_bExperimentRunning) ExpStepAction.Beep(false);           //关闭蜂鸣器
            Thread.Sleep(500);
            if (App.m_bCupTempEnable && App.m_bExperimentRunning)
            {
                ExpStepAction.SuspendCupTempCollect();                  //挂起小杯温度采集
                EmptyCupView();                                         //关闭小杯温度显示
            }

            if (App.m_bWholeModeEnable && App.m_bExperimentRunning)
                ExpStepAction.ValveRotation(this, 1, "取样位");          //由循环位起，换向阀转动到取样位（0），等待取样

            if (App.m_bWholeModeEnable && App.m_bExperimentRunning)
            {
                ExpStepAction.InjectorPump(this, pump_volume, true);    //注射器开始取液sample_volume ml
                App.g_CurrentInjectorSampleVolume = pump_volume;
            }

            if (App.m_bSamplePointEnable && App.m_bExperimentRunning)
            {
                if (!App.m_bWholeModeEnable)
                {
                    int time = 60;
                    while (time > 0)
                    {
                        time--;
                        App.Pause();
                    }
                }
                ExpStepAction.SamplePointReset(this);                    //取样针复位（回到最高处）
            }
            if (App.m_bCupTempEnable && App.m_bExperimentRunning)
            {
                ExpStepAction.SuspendCupTempCollect();                   //关闭小杯温度采集
                EmptyCupView();                                          //关闭小杯温度显示
            }
            if (App.m_bWholeModeEnable)
            {
                if (App.m_bExperimentRunning)
                    ExpStepAction.ValveRotation(this, 2, "出液位");              //由取样位起，换向阀转动到出液位（2）
                if (App.m_bExperimentRunning)
                    ExpStepAction.CollectFrameSampleOrganDown(this);            //收集器出样针头下降
                if (App.m_bExperimentRunning)
                {
                    ExpStepAction.InjectDrain(this, first_filter_volume + App.m_dlFirstFilterOffset, true);  //注射器打出设定初滤液体积
                    App.g_CurrentInjectorSampleVolume -= first_filter_volume;
                    App.g_CurrentInjectorSampleVolume -= App.m_dlFirstFilterOffset;
                }
                if (App.m_bExperimentRunning)
                {
                    ExpStepAction.InjectorPump(this, 0.1, true);                //注射器倒吸0.1ml
                    App.g_CurrentInjectorSampleVolume += 0.1;
                }
                if (App.m_bExperimentRunning)
                    ExpStepAction.CollectFrameSampleOrganReset(this);           //收集器出样针头上升复位
                //if (App.m_bExperimentRunning)
                //    ExpStepAction.CollectFrameExhibitOrganReset(this);          //收集器出样针头横移至废液槽(复位) 2022.12.14去除此处
                if (App.m_bExperimentRunning)
                    ExpStepAction.CollectFrameExhibitOrganMoveTo(this,
                    StaticParam.SAMPLETUBE_MAXROW - curr_sample_times + 1);     //收集器出样针横移至{0}号位
                if (App.m_bExperimentRunning)
                    ExpStepAction.CollectFrameSampleOrganDown(this);            //收集器出样针头下降
                if (App.m_bExperimentRunning)
                {
                    ExpStepAction.InjectDrain(this, sample_volume, true);        //注射器打出设定取样体积
                    App.g_CurrentInjectorSampleVolume -= sample_volume;
                }
                if (App.m_bExperimentRunning)
                    ExpStepAction.CollectFrameSampleOrganReset(this);          //收集器出样针头上升复位
                if (App.m_bExperimentRunning)
                {
                    ExpStepAction.InjectorPump(this, 3, true);                  //注射器倒吸3ml
                    App.g_CurrentInjectorSampleVolume += 3;
                }
                if (App.m_bExperimentRunning)
                    ExpStepAction.CollectFrameExhibitOrganReset(this);         //收集器出样针头横移至废液槽(复位)
                if (App.m_bExperimentRunning)
                    ExpStepAction.ValveRotation(this, 1, "循环位");            //由出液位起，换向阀转动到循环位（1）
                if (App.m_bExperimentRunning)
                    ExpStepAction.InjectDrain(this, 0, true);                  //打出所有液体
                if (auto_fluid_infusion.Equals("是") || auto_fluid_infusion.Equals("Yes"))
                {
                    if (App.m_bExperimentRunning)
                        ExpStepAction.ValveRotation(this, 2, "补液位");           //换向阀转动到补液位（3）
                    if (App.m_bExperimentRunning)
                    {
                        ExpStepAction.InjectorPump(this, pump_volume, true);    //注射器开始取液sample_volume ml
                        App.g_CurrentInjectorSampleVolume = pump_volume;
                    }
                    if (App.m_bExperimentRunning)
                        ExpStepAction.ValveRotation(this, 2, "循环位");        //换向阀转动到循环位（1）
                    if (App.m_bExperimentRunning)
                    {
                        ExpStepAction.InjectDrain(this, sample_volume + first_filter_volume, true);   //注射器打出补液体积为设定取样体积 + 初滤液体积
                        App.g_CurrentInjectorSampleVolume -= sample_volume + first_filter_volume;
                    }
                    if (curr_sample_times == 1)
                    {
                        if (App.m_bExperimentRunning)
                            ExpStepAction.ValveRotation(this, 3, "出液位");        //换向阀转动到出液位（3）
                        if (App.m_bExperimentRunning)
                            ExpStepAction.CollectFrameSampleOrganDown(this);      //收集器出样针头下降
                        if (App.m_bExperimentRunning)
                        {
                            ExpStepAction.InjectDrain(this, 0, true);             //打出所有液体
                            App.g_CurrentInjectorSampleVolume = 0;
                        }
                        if (App.m_bExperimentRunning)
                            ExpStepAction.CollectFrameSampleOrganReset(this);     //收集器出样针头上升复位
                    }
                    else
                    {
                        if (App.m_bExperimentRunning)
                            ExpStepAction.ValveRotation(this, 2, "补液位");        //换向阀转动到补液位（3）
                        if (App.m_bExperimentRunning)
                        {
                            ExpStepAction.InjectDrain(this, 0, true);             //打出所有液体
                            App.g_CurrentInjectorSampleVolume = 0;
                        }
                        if (App.m_bExperimentRunning)
                            ExpStepAction.ValveRotation(this, 1, "出液位");        //由补液位起，换向阀转动到出液位（2）
                    }
                }
                else
                {
                    if (App.m_bExperimentRunning)
                        ExpStepAction.ValveRotation(this, 3, "出液位");      //不补液，由循环位起，换向阀转动到出液位（2）
                }
            }

            if (App.m_bWholeModeEnable && App.m_bExperimentRunning)
                ExpStepAction.ValveReset(this);

            BaseUtils.TurnOnRedLight(IMG_L_SAMPLINGOVER);
            App.SetSystemStatus("本次取样完成！");

            if (App.m_bCupTempEnable)
                ExpStepAction.ResumeTempCollect();                         //恢复温度采集线程

            BaseUtils.TurnOnGreenLight(IMG_L_SAMPLINGOVER);

            App.m_bSamplingOn = false;
            UIOperator.SetComponetValidAndOpacity(BTN_EXPPAUSE, true);

            //判断是不是最后一次取样结束，稀释动作安排在所有取样结束后进行
            if (App.m_bWholeModeEnable && m_RuntimeDissolutionMethod.DilutionEnabled
                && curr_sample_times == m_RuntimeDissolutionMethod.SampleTimes)
            {
                double dilution_volume = m_RuntimeDissolutionMethod.DilutionRatio * m_RuntimeDissolutionMethod.SampleVolume;
                double dilution_first_filter_volume = 3;
                double dilution_pump_volume = dilution_volume + dilution_first_filter_volume + App.m_dlFirstFilterOffset;
                App.SetSystemStatus("开始稀释！");
                for (int k = 0; k < m_RuntimeDissolutionMethod.SampleTimes; k++)
                    DilutionProc(dilution_volume, dilution_pump_volume, dilution_first_filter_volume, auto_fluid_infusion, k + 1);
            }

            return true;
        }
        //根据稀释容量进行逐排稀释，周期2分钟
        public void DilutionProc(double dilution_volume, double pump_volume, double first_filter_volume,
                               string auto_fluid_infusion, int curr_dilution_times)
        {
            App.SetSystemStatus(String.Format("执行第{0}次稀释工作！", curr_dilution_times));
            if (App.m_bWholeModeEnable && App.m_bExperimentRunning)
                ExpStepAction.ValveRotation(this, 3, "补液位");                  //由出液位起，换向阀转动到补液位（3）

            if (App.m_bWholeModeEnable && App.m_bExperimentRunning)
            {
                ExpStepAction.InjectorPump(this, pump_volume, true);            //注射器开始取液pump_volume ml
                App.g_CurrentInjectorSampleVolume = pump_volume;
            }
            //阀转到出液位
            if (App.m_bExperimentRunning)
                ExpStepAction.ValveRotation(this, 1, "出液位");                  //由补液位->出液位 (3)                                                  
            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameSampleOrganDown(this);                //收集器出样针头下降
            if (App.m_bExperimentRunning)
            {
                ExpStepAction.InjectDrain(this, first_filter_volume + App.m_dlFirstFilterOffset, true);  //注射器打出设定初滤液体积
                App.g_CurrentInjectorSampleVolume -= first_filter_volume;
                App.g_CurrentInjectorSampleVolume -= App.m_dlFirstFilterOffset;
            }
            if (App.m_bExperimentRunning)
            {
                ExpStepAction.InjectorPump(this, 0.1, true);                    //注射器倒吸0.1ml
                App.g_CurrentInjectorSampleVolume += 0.1;
            }

            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameSampleOrganReset(this);               //收集器出样针头上升复位
            //if (App.m_bExperimentRunning)
            //    ExpStepAction.CollectFrameExhibitOrganReset(this);              //收集器出样针头横移至废液槽(复位) 2022.12.14去除此处

            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameExhibitOrganMoveTo(this,
                StaticParam.SAMPLETUBE_MAXROW - curr_dilution_times + 1);       //收集器出样针横移至{0}号位
            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameSampleOrganDown(this);                //收集器出样针头下降
            if (App.m_bExperimentRunning)
            {
                ExpStepAction.InjectDrain(this, dilution_volume, true);         //注射器打出设定稀释体积
                App.g_CurrentInjectorSampleVolume -= dilution_volume;
            }
            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameSampleOrganReset(this);               //收集器出样针头上升复位
            if (App.m_bExperimentRunning)
            {
                ExpStepAction.InjectorPump(this, 3, true);                      //注射器倒吸3ml
                App.g_CurrentInjectorSampleVolume += 3;
            }
            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameExhibitOrganReset(this);              //收集器出样针头横移至废液槽(复位)
            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameSampleOrganDown(this);                //收集器出样针头下降
            if (App.m_bExperimentRunning)
                ExpStepAction.InjectDrain(this, 0, true);                       //打出所有液体
            if (App.m_bExperimentRunning)
                ExpStepAction.CollectFrameSampleOrganReset(this);               //收集器出样针头上升复位
            App.SetSystemStatus("本次稀释完成！");

        }
        //判断当前杯中温度是否在要求温度的可控范围内
        public bool IsTempQualified(double settingTemp)
        {
            foreach (object obj in RT_CUP_GROUP.Children)
            {
                if (obj.GetType() == typeof(CheckBox))
                {
                    if (((CheckBox)obj).IsChecked == true)
                    {
                        string ckname = ((CheckBox)obj).Name;
                        string tempname = ckname.Substring(0, ckname.IndexOf("_CK"));

                        string[] arrSeg = ckname.Split("_");
                        int index = int.Parse(arrSeg[3]) - 1;
                        App.g_cuptempenabled[index] = true;

                        object o = RT_CUP_GROUP.FindName(tempname);
                        if (o.GetType() == typeof(Label))
                        {
                            try
                            {
                                double dTemp = double.Parse(((Label)o).Content.ToString());
                                if (dTemp < m_RuntimeDissolutionMethod.TempSetting - App.m_dlTempOffset
                                    || dTemp > m_RuntimeDissolutionMethod.TempSetting + App.m_dlTempOffset)
                                {
                                    return false;
                                }
                            }
                            catch (Exception e) { Console.WriteLine(e.ToString()); return false; }
                        }
                    }
                }
            }
            return true;
        }
        //判断水箱温度是否在要求温度的可控范围内
        public bool IsTempTankQualified(double settingTemp)
        {
            try
            {
                double dTemp = double.Parse(RT_WATERBOXTEMP.Content.ToString());
                if (dTemp < m_RuntimeDissolutionMethod.TempSetting - App.m_dlTempOffset
                    || dTemp > m_RuntimeDissolutionMethod.TempSetting + App.m_dlTempOffset)
                {
                    return false;
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); return false; }
            return true;
        }
        #endregion
    }
}