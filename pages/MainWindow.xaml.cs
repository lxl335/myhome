using Pharmacy.INST.DissolutionClient.common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Pharmacy.INST.DissolutionClient.entities;
using Pharmacy.INST.DissolutionClient.pages.modal;
using System.Threading;
using System.Diagnostics;
using Pharmacy.INST.CommonLib;
using com.ccg.GeckoKit.api;
using com.ccg.GeckoKit;
using System.Data;

namespace Pharmacy.INST.DissolutionClient.pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageButton m_imageCurrentUser;
        private ImageButton m_imageHeat;
        private ImageButton m_imageWashing;
        private ImageButton m_imageKeyboard;
        private ImageButton m_imageTempDetect;
        private ImageButton m_imageReport;
        private ImageButton m_imageTimer;
        private ImageButton m_imageMin;
        private ImageButton imageBtnExit;

        private Label m_labelLoginNameTip;
        private Label m_labelLoginName;
        private Label m_labelRoleNameTip;
        private Label m_labelRoleName;

        //系统时钟
        private Label m_labelDate;
        private Label m_labelTime;
        private DispatcherTimer sysDateTime;

        public static bool m_bMainBoardComm = false;                //主机串口状态
        iSerialCom.RecvDataCallBack recvDataCallBack;           //C++ dll 串口回调函数

        public double dRealWaterTemp = 0;
        private int m_nCountDownTimeSecond;
        DispatcherTimer m_PrepareTimeDispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponentEx();
            InitializeInterface();
        }
        //初始化组件
        public void InitializeComponentEx()
        {
            //将句柄交给应用程序 
            App.SetMainWindow(this);
            //初始化主框架背景样式
            //初始化主框架背景样式
            MainView.Background = Tools.GDIPlusBrush(App.strAppBackGroundPath);
            //初始化头部标题栏
            InitializeTitleBar();
            //初始化菜单栏
            InitializeMenuBar();
            //初始化导航栏
            InitializeNavBar();
            //初始化模块列表
            InitializeModule();
            //初始化打印机
            InitPrinterComm();
            //初始化底部系统栏
            InitializeStatusBar();
            //初始化串口并打开
            if (InitSerialsComm())
            {
                App.m_bLaunchInitialize = true;
                Thread thread = new Thread(DeviceReset);
                thread.Start();
            }
        }
        //初始化界面语言
        public void InitializeInterface()
        {
            LB_SYSTEM_TITLE.Content = App.m_LangPackage.LB_SYSTEM_TITLE;
            m_labelLoginNameTip.Content = App.m_LangPackage.m_labelLoginNameTip;
            m_labelRoleNameTip.Content = App.m_LangPackage.m_labelRoleNameTip;

            LB_SYS_STATUS_TIP.Content = App.m_LangPackage.LB_SYS_STATUS_TIP;
            LB_DB_STATUS_TIP.Content = App.m_LangPackage.LB_DB_STATUS_TIP;
            LB_PRINTER_STATUS_TIP.Content = App.m_LangPackage.LB_PRINTER_STATUS_TIP;
            LB_COUNT_STATUS_TIP.Content = App.m_LangPackage.LB_COUNT_STATUS_TIP;
        }
        #region 事件
        //主窗口加载事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTimer();
        }
        //主窗口关闭时事件
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //提示更新等待确认
            MessageBoxResult mBoxResult = MessageBox.Show(App.m_LangPackage.TIP_EXIT_CONFRIM, App.m_LangPackage.TIP, MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (mBoxResult)
            {
                case MessageBoxResult.No:
                    {
                        e.Cancel = true;
                    }
                    break;
                case MessageBoxResult.Yes:
                    {
                        //bool b = api.iSerialCom.__Close();
                        //if (App.g_bHeating)
                        StopHeat();
                        HeatLightControl(false);
                        App.m_bTempSurveryOn = false;
                        App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(0), App.GetBehavior(1), App.GetBehaviorRemark(1)));
                        App.m_WorkLogTimer.Stop();
                        if (App.m_SQLiteDBUtils != null)
                            App.WriteWorkLog();   //将缓存日志写入库
                        sysDateTime.Stop();
                        Process.GetCurrentProcess().Kill();
                    }
                    break;
            }
        }
        //点击用户头像操作事件
        private void imageCurrentUser_Click(object sender, RoutedEventArgs e)
        {
            UserModal userModal = new UserModal(App.g_TSession.TTUser);
            userModal.Show();
        }
        //退出点击操作事件
        private void imageBtnExit_App_Click(object sender, RoutedEventArgs e)
        {
            //是否正在实验
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_EXIT, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            this.Close();
        }
        //加热 按钮 事件
        public void imageHeat_Click(object sender, RoutedEventArgs e)
        {
            if (!CommonCheck()) return;
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TempSettingModal tempSettingModal = new TempSettingModal(HeatByTemp);
            tempSettingModal.Show();
            return;
        }
        //按照设定温度加热
        public void HeatByTemp(double temp, bool bHeat)
        {
            if (bHeat)
            {
                HeatLightControl(bHeat);
                Heat(temp);
            }
            else
            {
                HeatLightControl(bHeat);
                StopHeat();
            }
        }
        //清洗 按钮 事件
        public void imageWashing_Click(object sender, RoutedEventArgs e)
        {
            if (!CommonCheck()) return;
            if (!App.m_bWholeModeEnable)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_WASHINGSET, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!App.g_bWashing)
            {
                WashingTimesModal washingTimesModal = new WashingTimesModal();
                washingTimesModal.ShowDialog();
            }
            else
                MessageBox.Show(App.m_LangPackage.TIP_WASHING, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        //键盘 按钮 事件
        public void imageKeyboard_Click(object sender, RoutedEventArgs e)
        {
            App.LaunchSoftKeyboard();
        }
        //温度监测 按钮 事件
        public void imageTempDetect_Click(object sender, RoutedEventArgs e)
        {
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TempMoniterModal tempMoniterModal = new TempMoniterModal(this);
            tempMoniterModal.ShowDialog();
        }
        //报告 按钮 事件
        public void imageReport_Click(object sender, RoutedEventArgs e)
        {
            ReportModal reportModal = new ReportModal();
            reportModal.ShowDialog();
        }
        //定时 按钮 事件
        public void imageTimer_Click(object sender, RoutedEventArgs e)
        {
            if (!CommonCheck()) return;
            if (!App.CheckMainframeStatus()) return;
            if (App.m_bExperimentRunning)
            {
                MessageBox.Show(App.m_LangPackage.TIP_NO_OPERATE, App.m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            PrepareTimeModal prepareTimeModal = new PrepareTimeModal(CallBack_RecvicePrepareTime);
            prepareTimeModal.ShowDialog();
        }
        //最小化 按钮 事件
        public void imageMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        //通信重连 事件
        private void LB_MAIN_STATUS_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (((Label)sender).Content.Equals(StaticParam.Startup_status_Arr[1]))
            {
                ReconnectModal reconnectModal = new ReconnectModal(this);
                reconnectModal.ShowDialog();
            }
        }
        #endregion
        #region 初始化事件

        //初始化标题栏
        private void InitializeTitleBar()
        {
            //初始化系统标题
            LB_SYSTEM_TITLE.Content = App.m_strAppTitle;
            //初始化用户头像
            m_imageCurrentUser = new ImageButton(App.g_ResourceDir + "sysmanage_icon.png", App.g_ResourceDir + "sysmanage_hover_icon.png");
            m_imageCurrentUser.SetValue(Canvas.LeftProperty, (double)300);
            m_imageCurrentUser.SetValue(Canvas.TopProperty, (double)17);
            m_imageCurrentUser.Width = (double)25;
            m_imageCurrentUser.Height = (double)25;
            m_imageCurrentUser.MouseLeftButtonUp += new MouseButtonEventHandler(imageCurrentUser_Click);
            MainView.Children.Add(m_imageCurrentUser);
            //初始化用户名称标题
            m_labelLoginNameTip = new Label();
            m_labelLoginNameTip.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            m_labelLoginNameTip.FontSize = (double)12;
            m_labelLoginNameTip.FontFamily = new System.Windows.Media.FontFamily("verdener");
            m_labelLoginNameTip.FontWeight = FontWeights.Normal;
            m_labelLoginNameTip.SetValue(Canvas.LeftProperty, (double)(330));
            m_labelLoginNameTip.SetValue(Canvas.TopProperty, (double)10);
            m_labelLoginNameTip.Content = "账户：";
            m_labelLoginNameTip.Width = 40;
            MainView.Children.Add(m_labelLoginNameTip);
            //初始化用户名称
            m_labelLoginName = new Label();
            m_labelLoginName.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            m_labelLoginName.FontSize = (double)14;
            m_labelLoginName.FontFamily = new System.Windows.Media.FontFamily("verdener");
            m_labelLoginName.FontWeight = FontWeights.Normal;
            m_labelLoginName.SetValue(Canvas.LeftProperty, (double)(370));
            m_labelLoginName.SetValue(Canvas.TopProperty, (double)7);
            m_labelLoginName.Content = App.g_TSession.TTUser.LoginName;
            m_labelLoginName.Width = 150;
            MainView.Children.Add(m_labelLoginName);
            //初始化用户角色标题
            m_labelRoleNameTip = new Label();
            m_labelRoleNameTip.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            m_labelRoleNameTip.FontSize = (double)12;
            m_labelRoleNameTip.FontFamily = new System.Windows.Media.FontFamily("verdener");
            m_labelRoleNameTip.FontWeight = FontWeights.Normal;
            m_labelRoleNameTip.SetValue(Canvas.LeftProperty, (double)(330));
            m_labelRoleNameTip.SetValue(Canvas.TopProperty, (double)27);
            m_labelRoleNameTip.Content = "角色：";
            m_labelRoleNameTip.Width = 40;
            MainView.Children.Add(m_labelRoleNameTip);
            //初始化当前用户角色
            m_labelRoleName = new Label();
            m_labelRoleName.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            m_labelRoleName.FontSize = (double)12;
            m_labelRoleName.FontFamily = new System.Windows.Media.FontFamily("verdener");
            m_labelRoleName.FontWeight = FontWeights.Normal;
            m_labelRoleName.SetValue(Canvas.LeftProperty, (double)(370));
            m_labelRoleName.SetValue(Canvas.TopProperty, (double)27);
            m_labelRoleName.Content = App.g_TSession.TTUser.RoleName;
            m_labelRoleName.Width = 150;
            MainView.Children.Add(m_labelRoleName);

            UIOperator.LoadImage(App.g_ResourceDir + "running_pic.gif", IMG_RUNNINGPIC);
            UIOperator.LoadImage(App.g_ResourceDir + "running_text.gif", IMG_RUNNINGTEXT);

            //初始化时间
            m_labelTime = new Label();
            m_labelTime.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            m_labelTime.FontSize = (double)14;
            m_labelTime.FontFamily = new System.Windows.Media.FontFamily("verdener");
            m_labelTime.FontWeight = FontWeights.Normal;
            m_labelTime.SetValue(Canvas.LeftProperty, (double)(1110));
            m_labelTime.SetValue(Canvas.TopProperty, (double)7);
            m_labelTime.Width = 150;
            MainView.Children.Add(m_labelTime);
            //初始化日期
            m_labelDate = new Label();
            m_labelDate.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xff, 0xff, 0xff, 0xff));
            m_labelDate.FontSize = (double)14;
            m_labelDate.FontFamily = new System.Windows.Media.FontFamily("verdener");
            m_labelDate.FontWeight = FontWeights.Normal;
            m_labelDate.SetValue(Canvas.LeftProperty, (double)(1100));
            m_labelDate.SetValue(Canvas.TopProperty, (double)28);
            m_labelDate.Width = 150;
            MainView.Children.Add(m_labelDate);
            //初始化退出按钮
            imageBtnExit = new ImageButton(App.g_ResourceDir + "top_btn_tc.png", App.g_ResourceDir + "top_btn_tc_hover.png");
            imageBtnExit.SetValue(Canvas.LeftProperty, (double)(Width - 90));
            imageBtnExit.SetValue(Canvas.TopProperty, (double)0);
            imageBtnExit.Width = (double)90;
            imageBtnExit.Height = (double)58;
            imageBtnExit.MouseLeftButtonUp += new MouseButtonEventHandler(imageBtnExit_App_Click);
            MainView.Children.Add(imageBtnExit);
        }
        //初始化菜单栏
        private void InitializeMenuBar()
        {
            int pos = 600;
            //初始化加热按钮
            m_imageHeat = new ImageButton(App.g_ResourceDir + "btn_heat.png", App.g_ResourceDir + "btn_heat_hover.png");
            m_imageHeat.SetValue(Canvas.LeftProperty, (double)pos);
            m_imageHeat.SetValue(Canvas.TopProperty, (double)10);
            m_imageHeat.Width = (double)45;
            m_imageHeat.Height = (double)45;
            m_imageHeat.MouseLeftButtonUp += new MouseButtonEventHandler(imageHeat_Click);
            MainView.Children.Add(m_imageHeat);
            //初始化清洗按钮
            m_imageWashing = new ImageButton(App.g_ResourceDir + "btn_washing.png", App.g_ResourceDir + "btn_washing_hover.png");
            m_imageWashing.SetValue(Canvas.LeftProperty, (double)pos + 1 * 70);
            m_imageWashing.SetValue(Canvas.TopProperty, (double)10);
            m_imageWashing.Width = (double)45;
            m_imageWashing.Height = (double)45;
            m_imageWashing.MouseLeftButtonUp += new MouseButtonEventHandler(imageWashing_Click);
            MainView.Children.Add(m_imageWashing);
            //初始化键盘按钮
            m_imageKeyboard = new ImageButton(App.g_ResourceDir + "btn_keyboard.png", App.g_ResourceDir + "btn_keyboard_hover.png");
            m_imageKeyboard.SetValue(Canvas.LeftProperty, (double)pos + 2 * 70);
            m_imageKeyboard.SetValue(Canvas.TopProperty, (double)10);
            m_imageKeyboard.Width = (double)45;
            m_imageKeyboard.Height = (double)45;
            m_imageKeyboard.MouseLeftButtonUp += new MouseButtonEventHandler(imageKeyboard_Click);
            MainView.Children.Add(m_imageKeyboard);
            //初始化温度监测按钮
            m_imageTempDetect = new ImageButton(App.g_ResourceDir + "btn_tempdetect.png", App.g_ResourceDir + "btn_tempdetect_hover.png");
            m_imageTempDetect.SetValue(Canvas.LeftProperty, (double)pos + 3 * 70);
            m_imageTempDetect.SetValue(Canvas.TopProperty, (double)10);
            m_imageTempDetect.Width = (double)45;
            m_imageTempDetect.Height = (double)45;
            m_imageTempDetect.MouseLeftButtonUp += new MouseButtonEventHandler(imageTempDetect_Click);
            MainView.Children.Add(m_imageTempDetect);
            //初始化报告按钮
            m_imageReport = new ImageButton(App.g_ResourceDir + "btn_report.png", App.g_ResourceDir + "btn_report_hover.png");
            m_imageReport.SetValue(Canvas.LeftProperty, (double)pos + 4 * 70);
            m_imageReport.SetValue(Canvas.TopProperty, (double)10);
            m_imageReport.Width = (double)45;
            m_imageReport.Height = (double)45;
            m_imageReport.MouseLeftButtonUp += new MouseButtonEventHandler(imageReport_Click);
            MainView.Children.Add(m_imageReport);
            //初始化定时按钮
            m_imageTimer = new ImageButton(App.g_ResourceDir + "btn_timer.png", App.g_ResourceDir + "btn_timer_hover.png");
            m_imageTimer.SetValue(Canvas.LeftProperty, (double)pos + 5 * 70);
            m_imageTimer.SetValue(Canvas.TopProperty, (double)10);
            m_imageTimer.Width = (double)45;
            m_imageTimer.Height = (double)45;
            m_imageTimer.MouseLeftButtonUp += new MouseButtonEventHandler(imageTimer_Click);
            MainView.Children.Add(m_imageTimer);
            //初始化最小化按钮
            m_imageMin = new ImageButton(App.g_ResourceDir + "btn_min.png", App.g_ResourceDir + "btn_min_hover.png");
            m_imageMin.SetValue(Canvas.LeftProperty, (double)pos + 6 * 70);
            m_imageMin.SetValue(Canvas.TopProperty, (double)10);
            m_imageMin.Width = (double)45;
            m_imageMin.Height = (double)45;
            m_imageMin.MouseLeftButtonUp += new MouseButtonEventHandler(imageMin_Click);
            MainView.Children.Add(m_imageMin);
        }
        //初始化导航栏
        private void InitializeNavBar()
        {
            DataSet ds = new();
           
            App.m_SQLiteDBUtils.ExecuteQuery(sql.SQL.SQL_R_MODULEALL, ds, sql.SQL.T_MODULE);
            if (ds.Tables[sql.SQL.T_MODULE].Rows.Count == 11)
            {
                Tab_MainView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[0]["name"] + "  ";
                Tab_DataSearchView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[1]["name"] + "  ";
                Tab_WorkLogView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[2]["name"] + "  ";
                Tab_InstrumentVerifyView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[3]["name"] + "  ";
                Tab_DataBackupView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[4]["name"] + "  ";
                Tab_DeviceInfoView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[5]["name"] + "  ";
                Tab_TechSupportView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[6]["name"] + "  ";
                Tab_VideoMonitorView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[7]["name"] + "  ";
                Tab_FilterHeadExchView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[8]["name"] + "  ";
                Tab_SystemManageView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[9]["name"] + "  ";
                Tab_AboutView.Header = "  " + ds.Tables[sql.SQL.T_MODULE].Rows[10]["name"] + "  ";
            }
        }
        //初始化模块列表
        public void InitializeModule()
        {
            if (!App.g_TSession.IsRootManager())
            {
                int i = 0;
                for (; i < MainTabControl.Items.Count;)
                {
                    TabItem tabItem = MainTabControl.Items[i] as TabItem;
                    string strHeader = tabItem.Header.ToString().Trim();
                    bool bExsit = false;
                    foreach (string str in App.g_TSession.TModuleList)
                    {
                        if (str.Equals(strHeader))
                        {
                            bExsit = true;
                            i++;
                            break;
                        }
                    }
                    if (!bExsit)
                    {
                        MainTabControl.Items.Remove(tabItem);
                    }
                }
            }
        }
        //初始化打开串口
        public bool InitSerialsComm()
        {
            string strCom = string.Format("\\\\.\\{0}", App.m_strCom);
            m_bMainBoardComm = iSerialCom.__Open(strCom, App.m_nBps, App.m_nTimeout);
            recvDataCallBack = RecvDataGram;  //回调函数
            iSerialCom.__SetCallBackFunc("RecvDataCallBack", recvDataCallBack);

            m_bMainBoardComm = ExpStepAction.Echo();
            if (m_bMainBoardComm == false)
                iSerialCom.__Close();
            return m_bMainBoardComm;
        }
        public void DeviceReset()
        {
            if (ExpStepAction.Initialize())
            {
                InitSystemThread();
                if (App.m_bAutoHeatting)
                {
                    Heat(App.m_dlDefaultHeatTemp);
                }
                App.SetSystemStatus("启动初始化完成");
                UIOperator.SetLabelContent(LB_MAIN_STATUS, StaticParam.Startup_status_Arr[0]); //正常
                UIOperator.SetLabelBgColor(LB_MAIN_STATUS, null);

                App.m_bLaunchInitialize = false;
            }
            else
                App.SetSystemStatus("初始化仪器失败");
        }
        //初始化打开打印机串口
        private bool InitPrinterComm()
        {
            return true;
        }
        private void InitializeStatusBar()
        {
            new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    PGB_SELFCHECK.Dispatcher.Invoke(new Action(delegate
                    {
                        if (i > 0 && i < 51)
                        {
                            LB_SYS_STATUS.Content = App.m_LangPackage.TIP_MAINBOARD_CHECKING;
                        }
                        if (i > 50 && i < 91)
                        {
                            LB_SYS_STATUS.Content = App.m_LangPackage.TIP_DB_CHECKING;
                        }
                        if (i > 90)
                        {
                            LB_SYS_STATUS.Content = App.m_LangPackage.TIP_PRINT_CHECKING;
                        }
                        PGB_SELFCHECK.Value = i;
                    }));
                    Thread.Sleep(20);
                }

                LB_MAIN_STATUS.Dispatcher.Invoke(new Action(delegate
                {
                    if (m_bMainBoardComm)
                        LB_MAIN_STATUS.Content = StaticParam.Startup_status_Arr[0];
                    else
                    {
                        LB_MAIN_STATUS.Content = StaticParam.Startup_status_Arr[1];
                        LB_MAIN_STATUS.Background = Brushes.Red;
                    }
                    if (App.g_DBStatus)
                        LB_DB_STATUS.Content = StaticParam.Startup_status_Arr[0];
                    else
                    {
                        LB_DB_STATUS.Content = StaticParam.Startup_status_Arr[1]; ;
                        LB_DB_STATUS.Background = Brushes.Red;
                    }
                    if (InitPrinterComm())
                        LB_PRINTCOM_STATUS.Content = StaticParam.Startup_status_Arr[0];
                    else
                    {
                        LB_PRINTCOM_STATUS.Content = StaticParam.Startup_status_Arr[1];
                        LB_PRINTCOM_STATUS.Background = Brushes.Red;
                    }
                }));


                PGB_SELFCHECK.Dispatcher.Invoke(new Action(delegate
                {
                    LB_SYS_STATUS.Content = String.Empty;
                    PGB_SELFCHECK.Visibility = Visibility.Hidden;
                }));
            }).Start();

        }
        //初始化温度采集线程
        private void InitSystemThread()
        {
            //启动温度监测线程
            if (App.m_bTempSurvey)
            {
                App.m_TempSurveyThread = new Thread(() => TempSurveyThread());
                App.m_TempSurveyThread.Start();
            }
        }
        #endregion
        //设置系统时钟
        public void SetTimer()
        {
            //时钟定时器
            m_labelDate.Content = BaseUtils.GetCurrentDate();
            m_labelTime.Content = BaseUtils.GetCurrentTime();
            sysDateTime = new DispatcherTimer();
            sysDateTime.Tick += new EventHandler(OnSystemTimer);
            sysDateTime.Interval = new TimeSpan(0, 0, 0, 1);
            sysDateTime.Start();
        }
        //系统时钟动作
        private void OnSystemTimer(object sender, EventArgs e)
        {
            m_labelDate.Dispatcher.Invoke(new Action(delegate
            {
                m_labelDate.Content = BaseUtils.GetCurrentDate();
            }));
            m_labelTime.Dispatcher.Invoke(new Action(delegate
            {
                m_labelTime.Content = BaseUtils.GetCurrentTime();
            }));
        }
        //收到主机串口发来的数据
        public void RecvDataGram(IntPtr pData, int nLen)
        {
            new DatagramParse(this).Parse(pData, nLen);
        }
        private bool CommonCheck()
        {
            if (MainTabControl.SelectedIndex != 0) //主控台
            {
                MessageBox.Show(App.m_LangPackage.TIP_OPERATE_IN_CONSOLE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        //加热灯开关
        public void HeatLightControl(bool on)
        {
            Image img = UIOperator.GetComponentByName<Image>(MainTabControl, "IMG_L_UPTEMP");
            if (on) BaseUtils.TurnOnRedLight(img);
            else BaseUtils.TurnOnGreenLight(img);
        }
        //加热
        public void Heat(double dTempSetting)
        {
            ExpStepAction.SuspendTempCollect();     //挂起温度采集线程
            ExpStepAction.HeatByTemp(dTempSetting); //加热
            App.g_bHeating = true;
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(96), App.GetBehaviorRemark(96)));
            ExpStepAction.ResumeTempCollect();      //恢复温度采集线程
        }
        private void StopHeat()
        {
            //停止加热
            ExpStepAction.SuspendTempCollect();     //挂起温度采集线程
            ExpStepAction.HeatHalt();
            App.g_bHeating = false;
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(97), App.GetBehaviorRemark(97)));
            ExpStepAction.ResumeTempCollect();      //恢复温度采集线程
        }
        //温度监测线程
        private void TempSurveyThread()
        {
            while (App.m_bTempSurveryOn)
            {
                if (App.m_bTempSurveyThreadRunning)
                {
                    int nLen = 0;
                    byte[] buffer = BaseUtils.SendRecvTempDatagram(Datagram.T_BOXTEMP_READ, ref nLen);
                    new DatagramParse(this).Parse(buffer, nLen, this);
                }
                if (App.m_bTempSurveyThreadRunning)
                {
                    if (App.m_bCupTempEnable && App.m_bExpCupSurveryOn)
                    {
                        int nLen = 0;
                        byte[] buffer = BaseUtils.SendRecvTempDatagram(Datagram.T_CUPTEMP_READ, ref nLen);
                        new DatagramParse(this).Parse(buffer, nLen, this);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        //刷新水箱温度
        public void RefreshBoxTemp(short t1, short t2)
        {
            string strTempC1 = ((double)t1 / 100).ToString("F");
            dRealWaterTemp = double.Parse(strTempC1);
            string strTempC2 = ((double)t2 / 100).ToString("F");       //第二通道值，暂未使用

            MainTabControl.Dispatcher.Invoke(new Action(delegate
            {
                Label RT_WATERBOXTEMP = UIOperator.GetComponentByName<Label>(MainTabControl, "RT_WATERBOXTEMP");
                if (RT_WATERBOXTEMP != null)
                {
                    RT_WATERBOXTEMP.Dispatcher.Invoke(new Action(delegate
                    {
                        RT_WATERBOXTEMP.Content = strTempC1;
                    }));
                }
            }));
        }
        //刷新小杯温度
        public void RefreshCupTemp(short[] tArr)
        {
            Label[] t_cup = new Label[App.m_nCupNum];
            MainTabControl.Dispatcher.Invoke(new Action(delegate
            {
                for (int i = 0; i < App.m_nCupNum; i++)
                {
                    t_cup[i] = UIOperator.GetComponentByName<Label>(MainTabControl, String.Format("RT_CUP_TEMP_{0}", i + 1));
                    if (t_cup[i] != null)
                    {
                        App.g_cuptemp[i] = ((double)tArr[i] / 100) + App.g_cuptempcali[i];
                        if (App.g_cuptempenabled[i])
                        {
                            t_cup[i].Content = App.g_cuptemp[i].ToString("F");
                        }
                        else
                        {
                            t_cup[i].Content = "--------";
                        }
                    }

                }
            }));
        }
        //定时启动加热回调
        private void CallBack_RecvicePrepareTime(int nPrepareTime, bool bStatus)
        {
            if (bStatus)
            {
                HeatByTemp(App.m_dlDefaultHeatTemp, false);
                MessageBox.Show(App.m_LangPackage.TIP_SETTING_SUCCESS, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                m_nCountDownTimeSecond = nPrepareTime;
                m_PrepareTimeDispatcherTimer = new DispatcherTimer();
                m_PrepareTimeDispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                m_PrepareTimeDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                m_PrepareTimeDispatcherTimer.Start();
            }
            else
            {
                if (m_PrepareTimeDispatcherTimer != null)
                {
                    m_PrepareTimeDispatcherTimer.Stop();
                    m_PrepareTimeDispatcherTimer = null;
                    App.SetSystemStatusWithoutLog("定时器取消成功");
                    MessageBox.Show(App.m_LangPackage.TIP_TIMER_REMOVE, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        //定时执行
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            m_nCountDownTimeSecond--;
            if (m_nCountDownTimeSecond < 0)
            {
                m_PrepareTimeDispatcherTimer.Stop();
                Heat(StaticParam.DEFAULT_HEATTEMP);

                MainTabControl.Dispatcher.Invoke(new Action(delegate
                {
                    HeatLightControl(true);
                }));
                return;
            }
            App.SetSystemStatusWithoutLog(BaseUtils.GetHHMMSSRemainTime(m_nCountDownTimeSecond) + "后，启动加热");
        }
    }
}