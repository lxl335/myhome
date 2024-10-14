using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.IO;
using System.Data;
using System.Diagnostics;
using com.ccg.GeckoKit;
using com.ccg.GeckoKit.api;
using Pharmacy.INST.DissolutionClient.common;
using Pharmacy.INST.DissolutionClient.pages;
using Pharmacy.INST.DissolutionClient.pages.modal;
using com.ccg.PrivelegeKit;
using System.Collections;

namespace Pharmacy.INST.DissolutionClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    ///
    public partial class App : Application
    {
        public static string g_AppDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();         //应用程序目录
        public static string g_IniSysFileName = g_AppDirectory + StaticParam.AppIniProfile;             //应用程序系统配置文件
        public static string g_CalibrationFileName = g_AppDirectory + StaticParam.AppCalibrationFile;   //校准配置文件
        public static string g_LogFileName = g_AppDirectory + StaticParam.AppLogFile;                   //应用程序系统日志文件
        public static string g_ResourceDir = g_AppDirectory + StaticParam.AppResReletivePath;           //应用程序资源目录
        public static string DB_PASSWORD = StaticParam.DB_PWD;                                          //应用程序数据库口令
        public static string g_DateLimit;
        public static double g_CurrentInjectorSampleVolume = 0;                                         //实时记录当前注射器内液体体积
        public static string g_AppName = StaticParam.AppName;
        public static double g_SamplePoint_K;
        public static double g_SamplePoint_B;
        public static double g_SamplePoint_K_S;
        public static double g_SamplePoint_B_S;
        public static double g_Injector_K;
        public static double g_Injector_B;
        /// <summary>
        /// 系统参数
        /// </summary>
        //[App]
        public static string m_strAppTitle;
        public static string m_strAppVersion;
        public static string m_strAppOwner;
        public static string m_strSerialNo;
        public static string m_strDefAccount;
        public static string m_strExpireTime;
        //[LocalBase]
        public static string m_strDBProvider;
        public static string m_strDataSource;
        //[Device]
        public static string m_strModel;
        public static string m_strCom;
        public static int m_nBps;
        //[Printer]
        public static string m_strPrinter;
        public static string m_strLpt;
        public static int m_nPrintBps;
        //[SysPara]
        public static string m_strDateFormat;
        public static string m_strTimeFormat;
        public static string m_strFileDateTimeFormat;
        public static int m_nTimeout;
        public static int m_nLogFileSize;
        public static int m_nPageSize;
        public static bool m_bAutoStart;                                    //是否开机自启
        public static bool m_bSoftSelfCheck;                                //是否软件自检
        public static int m_nPwdValitionDays;                               //口令有效期
        public static int m_nLogWritePeriod;                                //日志缓存持久化周期
        public static bool m_bSampleBeep;                                   //取样前是否蜂鸣
        public static int m_nRecvSendInterval;                              //收发间隔（毫秒）
        public static int m_nPutPillTime;                                   //投药机构工作时间（开合一次）（毫秒）
        public static int m_nInjectorMLTime;                                //注射器每毫升移动的时间（毫秒）
        public static int m_nValveStepTime;                                 //电磁阀步进时间（毫秒）
        public static double m_dlSamplePointPulseML;                        //取样针每毫升脉冲值
        public static int m_nInjectorPulseML;                               //注射器每毫升脉冲值
        public static bool m_bTempSurvey;                                   //是否采集温度
        public static int m_nTempSendRecvInterval;                          //温度收发延时
        public static int m_nTempSuspAndResuDelay;                          //温度挂起恢复延迟
        public static double m_dlDefaultHeatTemp;                           //默认加热温度
        public static int m_nRotateRate;                                    //转速倍率
        public static double m_dlFirstFilterOffset;                         //初滤液补偿值

        public static bool m_bWholeModeEnable;                              //是否为精简模式运行
        public static bool m_bPutPillEnable;                                //是否启用投药装置
        public static bool m_bSamplePointEnable;                            //是否启用取样针装置
        public static bool m_bCupTempEnable;                                //是否启用小杯温度监测装置
        public static int m_nCupNum;                                        //杯子数量
        public static double m_dlTempOffset;                                //温度偏差绝对值
        public static bool m_bDoubleMotor;                                  //是否为双电机配置
        public static bool m_bAutoHeatting;                                 //软件启动后是否自动加热
        public static string m_strLanguage;                                 //语言
        public static int m_nSpeedOffset;                                   //转速补偿值

        //全局服务类
        public static ProfileUtils m_ProfileUtils;
        public static SQLiteDBUtils m_SQLiteDBUtils;
        public static LogUtils m_LogUtils;                                  //日志服务
        public static DispatcherTimer m_WorkLogTimer;                       //系统日志定时器
        public static TSession g_TSession;                                  //全局session
        public static bool g_DBStatus = false;                              //全局数据库状态
        public static bool g_EngVer = false;                                 //是否英文版
        public static string m_DBConnectionString;
        public static string m_strSQLiteDBFileName;
        public static int m_nRunningExperimentID;                           //运行中的实验记录ID
        public static bool m_bExperimentRunning;                            //实验运行中
        public static bool m_bSamplingOn;                                   //取样中
        public static bool m_bExperimentPause;                              //实验暂停中
        //全局线程
        public static Thread m_TempSurveyThread;                            //温度监测线程句柄
        public static bool m_bLaunchInitialize = false;                     //开机初始化中
        public static bool m_bTempSurveryOn;                                //温度监测启停
        public static bool m_bTempSurveyThreadRunning;                      //温度监测线程运行标识符
        public static bool m_bExpCupSurveryOn;                              //小杯温度监测启停标识符
        public static byte[] sendYCbuf = new byte[StaticParam.BUFFERSIZE];  //测量数据发送缓冲区
        public static byte[] recvYCbuf = new byte[StaticParam.BUFFERSIZE];  //测量数据接收缓冲区
        public static byte[] sendYKbuf = new byte[StaticParam.BUFFERSIZE];  //控制数据发送缓冲区
        public static byte[] recvYKbuf = new byte[StaticParam.BUFFERSIZE];  //控制数据接收缓冲区
        public static double g_SamplePointPos;                              //实验取样针下降位置

        public static bool g_bHeating;                                      //正在加热？是：否
        public static bool g_bWashing;                                      //正在清洗？是：否

        public static double[] g_waterboxtempcali = new double[2];          //水箱温度 0-值 1-预留值（备用）
        public static double[] g_cuptemp = new double[StaticParam.CUP_NUM];
        public static double[] g_cuptempcali = new double[StaticParam.CUP_NUM + 1];
        public static bool[] g_cuptempenabled = new bool[StaticParam.CUP_NUM];

        private static MainWindow __MainWindow;                             //主框架句柄
        public static ImageBrush ButtonImageBrush;
        public static string strAppBackGroundPath;
        public static string strBtnBackGroundPath;

        public static Queue m_SpeedSwitchQueue = new Queue();               //转速设置队列

        public static DataSet g_FunDataSet;

        public static LangPackage m_LangPackage;                            //语言包

        public App()
        {
            Init();
            InitProfile();
            if (!InitSystemPara()) Environment.Exit(0);

            if (g_DBStatus = InitDBServer())  //数据库连接成功
            {
                //初始化功能模块集
                g_FunDataSet = new DataSet();
                m_SQLiteDBUtils.ExecuteQuery(sql.SQL.SQL_R_FUNCTiON, g_FunDataSet, sql.SQL.T_FUNCTION);
                m_SQLiteDBUtils.ExecuteQuery(sql.SQL.SQL_R_LOGTYPE, g_FunDataSet, sql.SQL.T_LOGTYPE);
                m_SQLiteDBUtils.ExecuteQuery(sql.SQL.SQL_R_BEHAVIOR, g_FunDataSet, sql.SQL.T_BEHAVIOR);

                //初始化全局日志更新线程
                SetTimer();
                //初始化水箱温度标定
                if (!InitializeWaterBoxTempCali())
                {
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(g_TSession.TTUser.LoginName, App.GetLogType(2), App.GetBehavior(150),App.GetBehaviorRemark(150)));
                    MessageBox.Show(m_LangPackage.TIP_TANK_TEMP_CALI_ERROR, m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //初始化水杯温度标定
                if (!InitializeCupTempCali())
                {
                    App.m_LogUtils.WorkLogList.Add(new WorkLog(g_TSession.TTUser.LoginName, App.GetLogType(2), App.GetBehavior(151), App.GetBehaviorRemark(151)));
                    MessageBox.Show(m_LangPackage.TIP_CUP_TEMP_CALI_ERROR, m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
        }
        ~App() { }

        public static string GetFunctionName(int index)
        {
            return g_FunDataSet.Tables[sql.SQL.T_FUNCTION].Rows[index]["name"].ToString().Trim();
        }
        public static string GetLogType(int index)
        {
            return g_FunDataSet.Tables[sql.SQL.T_LOGTYPE].Rows[index]["LogTypeName"].ToString().Trim();
        }
        public static string GetBehavior(int index)
        {
            return g_FunDataSet.Tables[sql.SQL.T_BEHAVIOR].Rows[index]["BehaviorName"].ToString().Trim();
        }
        public static string GetBehaviorRemark(int index)
        {
            return g_FunDataSet.Tables[sql.SQL.T_BEHAVIOR].Rows[index]["remark"].ToString().Trim();
        }

        //退出系统
        protected override void OnExit(ExitEventArgs e)
        {
            if (m_WorkLogTimer != null)
                m_WorkLogTimer.Stop();

            if (m_SQLiteDBUtils != null)
            {
                if (m_SQLiteDBUtils.IsOpen())
                    m_SQLiteDBUtils.Close();
            }
        }
        //初始化防止多个启动
        private void Init()
        {
            Process[] processes = Process.GetProcessesByName(g_AppName);
            if (processes.Length > 1)
            {
                MessageBox.Show(App.m_LangPackage.TIP_APPLICATION_RUNNING, App.m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Warning);
                Process.GetCurrentProcess().Kill();
            }
        }
        //初始化读配置文件信息
        private void InitProfile()
        {
            try
            {
                m_ProfileUtils = new ProfileUtils();
                //系统配置参数
                m_strAppTitle = m_ProfileUtils.ReadIniData("App", "Title", "", g_IniSysFileName);
                m_strAppVersion = m_ProfileUtils.ReadIniData("App", "version", "", g_IniSysFileName);
                m_strAppOwner = m_ProfileUtils.ReadIniData("App", "Owner", "", g_IniSysFileName);
                m_strSerialNo = m_ProfileUtils.ReadIniData("App", "SN", "", g_IniSysFileName);
                m_strDefAccount = m_ProfileUtils.ReadIniData("App", "DefAccount", "", g_IniSysFileName);

                m_strDBProvider = m_ProfileUtils.ReadIniData("LocalBase", "Provider", "", g_IniSysFileName);
                m_strDataSource = m_ProfileUtils.ReadIniData("LocalBase", "DataSource", "DCDB.db", g_IniSysFileName);
                m_strModel = m_ProfileUtils.ReadIniData("Device", "Model", "", g_IniSysFileName);
                m_strCom = m_ProfileUtils.ReadIniData("Device", "SerialsPort", "COM2", g_IniSysFileName);
                m_nBps = Int32.Parse(m_ProfileUtils.ReadIniData("Device", "Baud", "9600", g_IniSysFileName));
                m_strPrinter = m_ProfileUtils.ReadIniData("Printer", "Printer", "", g_IniSysFileName);
                m_strLpt = m_ProfileUtils.ReadIniData("Printer", "Lpt", "LPT1", g_IniSysFileName);
                m_nPrintBps = Int32.Parse(m_ProfileUtils.ReadIniData("Printer", "PrintBaud", "9600", g_IniSysFileName));
                m_nTimeout = Int32.Parse(m_ProfileUtils.ReadIniData("Printer", "Timeout", "5000", g_IniSysFileName));
                m_nLogFileSize = Int32.Parse(m_ProfileUtils.ReadIniData("Printer", "LogFileSize", "1024", g_IniSysFileName));

                m_strDateFormat = m_ProfileUtils.ReadIniData("SysPara", "DateFormat", "", g_IniSysFileName);
                m_strTimeFormat = m_ProfileUtils.ReadIniData("SysPara", "TimeFormat", "", g_IniSysFileName);
                m_strFileDateTimeFormat = m_ProfileUtils.ReadIniData("SysPara", "FileDateTimeFormat", "", g_IniSysFileName);
                m_nPageSize = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "PageSize", "", g_IniSysFileName));

                m_bAutoStart = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "AutoStart", "True", g_IniSysFileName));
                m_bSoftSelfCheck = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "SoftSelfCheck", "False", g_IniSysFileName));
                m_nPwdValitionDays = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "PwdValitionDays", "90", g_IniSysFileName));
                m_nLogWritePeriod = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "LogWritePeriod", "60", g_IniSysFileName));
                m_bSampleBeep = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "SampleBeep", "False", g_IniSysFileName));
                m_nRecvSendInterval = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "RecvSendInterval", "1000", g_IniSysFileName));
                m_nPutPillTime = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "PutPillTime", "8000", g_IniSysFileName));
                m_nValveStepTime = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "ValveStepTime", "1000", g_IniSysFileName));
                m_nInjectorMLTime = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "InjectorMLTime", "600", g_IniSysFileName));
                m_dlSamplePointPulseML = double.Parse(m_ProfileUtils.ReadIniData("SysPara", "SamplePointPulseML", "62.5", g_IniSysFileName));
                m_nInjectorPulseML = int.Parse(m_ProfileUtils.ReadIniData("SysPara", "InjectorPulseML", "7424", g_IniSysFileName));
                m_bTempSurvey = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "TempSurvey", "True", g_IniSysFileName));
                m_dlDefaultHeatTemp = double.Parse(m_ProfileUtils.ReadIniData("SysPara", "DefaultHeatTemp", "37", g_IniSysFileName));
                m_nTempSuspAndResuDelay = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "TempSuspAndResuDelay", "1000", g_IniSysFileName));
                m_nTempSendRecvInterval = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "TempSendRecvInterval", "500", g_IniSysFileName));
                m_bWholeModeEnable = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "WholeModeEnable", "False", g_IniSysFileName));
                m_bPutPillEnable = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "PutPillEnable", "True", g_IniSysFileName));
                m_bSamplePointEnable = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "SamplePointEnable", "True", g_IniSysFileName));
                m_bCupTempEnable = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "CupTempEnable", "True", g_IniSysFileName));
                m_nCupNum = int.Parse(m_ProfileUtils.ReadIniData("SysPara", "CupNum", "12", g_IniSysFileName));
                m_dlTempOffset = double.Parse(m_ProfileUtils.ReadIniData("SysPara", "TempOffset", "0.5", g_IniSysFileName));
                m_bDoubleMotor = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "DoubleMotor", "False", g_IniSysFileName));
                m_bAutoHeatting = bool.Parse(m_ProfileUtils.ReadIniData("SysPara", "AutoHeatting", "False", g_IniSysFileName));
                m_nRotateRate = Int32.Parse(m_ProfileUtils.ReadIniData("SysPara", "RotateRate", "1", g_IniSysFileName));
                m_dlFirstFilterOffset = double.Parse(m_ProfileUtils.ReadIniData("SysPara", "FirstFilterOffset", "2", g_IniSysFileName));
                m_strLanguage = m_ProfileUtils.ReadIniData("SysPara", "Language", "CHS", g_IniSysFileName);
                m_nSpeedOffset = int.Parse(m_ProfileUtils.ReadIniData("SysPara", "SpeedOffset", "0", g_IniSysFileName));

                //校准值配置参数
                g_SamplePoint_K = double.Parse(m_ProfileUtils.ReadIniData("SamplePoint", "K", "0", g_CalibrationFileName));
                g_SamplePoint_B = double.Parse(m_ProfileUtils.ReadIniData("SamplePoint", "B", "0", g_CalibrationFileName));
                g_Injector_K = double.Parse(m_ProfileUtils.ReadIniData("Injector", "K", "0", g_CalibrationFileName));
                g_Injector_B = double.Parse(m_ProfileUtils.ReadIniData("Injector", "B", "0", g_CalibrationFileName));
                //add by scott 2022.11.2
                g_SamplePoint_K_S = double.Parse(m_ProfileUtils.ReadIniData("SamplePoint", "K_S", "0", g_CalibrationFileName));
                g_SamplePoint_B_S = double.Parse(m_ProfileUtils.ReadIniData("SamplePoint", "B_S", "0", g_CalibrationFileName));

                //语言版本
                if (m_strLanguage.Equals("ENG")) g_EngVer = true;
                m_LangPackage = new LangPackage(g_EngVer);
                m_LangPackage.Load();

                string AppResReletivePath = StaticParam.AppResReletivePath;
                if (g_EngVer) AppResReletivePath = StaticParam.AppResReletivePathEng;
                g_ResourceDir = string.Format("{0}{1}", g_AppDirectory, AppResReletivePath);

                if (g_EngVer)
                {
                    m_strAppTitle = "AI Dissolution Client";
                    m_strAppOwner = "Beijing BeiYan";
                    try
                    {
                        string[] arr = m_strDataSource.Split('.');
                        string dbsource = string.Format("{0}-eng.{1}", arr[0], arr[1]);
                        m_strDataSource = dbsource;
                    }
                    catch (Exception e) 
                    {
                        WriteSystemLog(e.ToString());
                        MessageBox.Show(m_LangPackage.TIP_GETDBFILE_ERROR, m_LangPackage.ERROR,
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    
                }

                strAppBackGroundPath = string.Format(g_AppDirectory + AppResReletivePath + "{0}", StaticParam.AppBackgroundFile);
                strBtnBackGroundPath = string.Format(g_ResourceDir + "{0}", StaticParam.AppBtnBackgroundFile);
                ButtonImageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(strBtnBackGroundPath, UriKind.RelativeOrAbsolute))
                };


            }
            catch (Exception e)
            {
                MessageBox.Show(m_LangPackage.TIP_INIT_FILE_ERROR + e.ToString());
            }
        }
        //初始化系统参数
        private Boolean InitSystemPara()
        {
            m_DBConnectionString = string.Format("data source={0}", m_strDataSource);
            g_TSession = new TSession(); //初始化全局session
            m_LogUtils = new LogUtils(); //初始化日志工具
            m_LogUtils.SetLogFileMaxSize(App.m_nLogFileSize);
            BaseUtils.CheckDirectory(App.g_AppDirectory + StaticParam.AppReportPath);  //检查报告目录
                                                                                       // Resources["imgBrush"] = StaticParam.ButtonImageBrush;                      //全局button样式初始化
            Resources = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/CommonLib;component/Themes.xaml")
            };
            Resources["imgBrush"] = ButtonImageBrush;
            Resources["GroupBoxBorderBrush"] = StaticParam.GroupBoxBorderBrush;

            string SerialNo = m_strSerialNo;
            if (!m_strSerialNo.EndsWith("=BJ-BY-KY"))
                SerialNo += "=BJ-BY-KY";
            else
            {
                m_strSerialNo = m_strSerialNo.Replace("=BJ-BY-KY", "");
                m_ProfileUtils.WriteIniData("App", "SN", m_strSerialNo, g_IniSysFileName);
            }
            g_DateLimit = CommonLib.CommonLib.DateLimit(SerialNo);

            m_nRunningExperimentID = 0;                      //运行中的实验记录ID
            m_bExperimentRunning = false;                    //实验运行中
            m_bSamplingOn = false;                           //取样中标识初始化
            m_bExperimentPause = false;                      //实验暂停标识初始化
            m_bTempSurveryOn = true;                         //温度监测初始化
            m_bTempSurveyThreadRunning = true;              //温度监测线程运行标识符
            m_bExpCupSurveryOn = false;                       //小杯温度监测启停初始化
            g_bHeating = false;
            g_bWashing = false;
            m_LogUtils.CleanLogFile();
            return true;
        }
        //初始化数据库
        private bool InitDBServer()
        {
            m_SQLiteDBUtils = new SQLiteDBUtils(m_DBConnectionString);
            return m_SQLiteDBUtils.Open(DB_PASSWORD);
        }
        //初始化水箱温度标定值
        public bool InitializeWaterBoxTempCali()
        {
            //DataSet ds = new DataSet();
            //string strCmd = string.Format(sql.SQL.SQL_R_TEMPCALI, 0);
            //m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_TEMPCALI);
            //if (ds.Tables[sql.SQL.T_TEMPCALI].Rows.Count < 0) return false;

            //for (int i = 0; i < ds.Tables[sql.SQL.T_TEMPCALI].Rows.Count; i++)
            //{
            //    //g_waterboxtempcali[i] = double.Parse(ds.Tables[sql.SQL.T_TEMPCALI].Rows[i]["Cali"].ToString());
            //}
            for (int i = 0; i < 2; i++)
            {
                string Key = string.Format("K_{0}", i + 1);
                g_waterboxtempcali[i] = double.Parse(m_ProfileUtils.ReadIniData("TankTemp", Key, "0", g_CalibrationFileName));
            }

            
            return true;
        }
        //初始化水杯温度标定值
        public bool InitializeCupTempCali()
        {
            //DataSet ds = new DataSet();
            //string strCmd = string.Format(sql.SQL.SQL_R_TEMPCALI, 1);
            //m_SQLiteDBUtils.ExecuteQuery(strCmd, ds, sql.SQL.T_TEMPCALI);
            //if (ds.Tables[sql.SQL.T_TEMPCALI].Rows.Count < 0) return false;

            //for (int i = 0; i < ds.Tables[sql.SQL.T_TEMPCALI].Rows.Count; i++)
            //{
            //    g_cuptempcali[i] = double.Parse(ds.Tables[sql.SQL.T_TEMPCALI].Rows[i]["Cali"].ToString());
            //}
            for (int i = 0; i <= StaticParam.CUP_NUM; i++)
            {
                string Key = string.Format("K_{0}", i + 1);
                g_cuptempcali[i] = double.Parse(m_ProfileUtils.ReadIniData("CupTemp", Key, "0", g_CalibrationFileName));
            }
            return true;
        }
        //设置系统时钟
        public void SetTimer()//工作日志定时器
        {
            m_WorkLogTimer = new DispatcherTimer();
            m_WorkLogTimer.Tick += new EventHandler(OnWorkLogTimer);
            m_WorkLogTimer.Interval = new TimeSpan(0, 0, 0, m_nLogWritePeriod);
            m_WorkLogTimer.Start();
        }
        //定时读缓存写日志
        private void OnWorkLogTimer(object sender, EventArgs e)
        {
            WriteWorkLog();
        }
        //写工作日志
        public static void WriteWorkLog()
        {
            lock (m_LogUtils.WorkLogList)
            {
                if (m_LogUtils.WorkLogList.Count > 0) //有日志产生了
                {
                    //写入库
                    for (int i = 0; i < m_LogUtils.WorkLogList.Count;)
                    {
                        WorkLog workLog = m_LogUtils.WorkLogList[i++] as WorkLog;
                        m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_C_WORKLOG, workLog.LoginName,
                            workLog.LogType, workLog.Behavior, workLog.ActionTime, workLog.Remark));
                    }
                    m_LogUtils.WorkLogList.Clear();
                }
            }
        }
        /// <summary>
        /// 写系统日志
        /// </summary>
        public static void WriteSystemLog(string log)
        {
            if (!File.Exists(g_LogFileName))
            {
                File.Create(g_LogFileName).Close();
            }
            using (StreamWriter sw = new StreamWriter(g_LogFileName, true))
            {
                sw.Write("\r\n" + BaseUtils.GetCurrentDateTime() + "  " + log);
   
                sw.Close();
            }
            return;
        }
        //全局设置主窗口句柄
        public static void SetMainWindow(MainWindow mainWindow)
        {
            __MainWindow = mainWindow;
        }
        //全局获取主窗口句柄
        public static MainWindow GetMainWindow()
        {
            return __MainWindow;
        }
        //设置系统状态栏信息
        public static void SetSystemStatus(string msg)
        {
            __MainWindow.LB_SYS_STATUS.Dispatcher.Invoke(new Action(delegate
            {
                __MainWindow.LB_SYS_STATUS.Content = msg;
            }));
            WriteSystemLog(msg);
        }
        //设置系统状态栏信息并不保存日志
        public static void SetSystemStatusWithoutLog(string msg)
        {
            __MainWindow.LB_SYS_STATUS.Dispatcher.Invoke(new Action(delegate
            {
                __MainWindow.LB_SYS_STATUS.Content = msg;
            }));
        }
        //设置计数器状态
        public static void SetCountStatus(string msg)
        {
            __MainWindow.LB_COUNT_STATUS.Dispatcher.Invoke(new Action(delegate
            {
                __MainWindow.LB_COUNT_STATUS.Content = msg;
            }));
        }
        //设置动画状态
        public static void SetAnimationStatus(bool bRunning)
        {
            __MainWindow.IMG_RUNNINGPIC.Dispatcher.Invoke(new Action(delegate
            {
                __MainWindow.IMG_RUNNINGPIC.Visibility = bRunning ? Visibility.Visible : Visibility.Hidden;
            }));
            __MainWindow.IMG_RUNNINGTEXT.Dispatcher.Invoke(new Action(delegate
            {
                __MainWindow.IMG_RUNNINGTEXT.Visibility = bRunning ? Visibility.Visible : Visibility.Hidden;
            }));
        }
        //启用软键盘
        public static void LaunchSoftKeyboard()
        {
            string kbFileName = StaticParam.KeyBoardProgram[0];
            string strOSVersion = Environment.OSVersion.Version.ToString();
            string strOSVersionNo = strOSVersion.Substring(0, strOSVersion.IndexOf("."));
            if (strOSVersionNo.Equals("10")) kbFileName = StaticParam.KeyBoardProgram[1];
            string filepath = g_AppDirectory + kbFileName;
            if (File.Exists(filepath))
            {
                Process pr = new Process();
                try
                {
                    pr.StartInfo.WorkingDirectory = App.g_AppDirectory;
                    pr.StartInfo.UseShellExecute = true;
                    pr.StartInfo.FileName = kbFileName;
                    pr.StartInfo.Verb = "runas";
                    bool b = pr.HasExited;
                    if (b) pr.Start();
                    else pr.Kill();
                    return;
                }
                catch (Exception e1)
                {
                    Debug.WriteLine(e1.ToString());
                    pr.Start();
                }
            }
        }
        //系统暂停1秒
        public static void Pause()
        {
            Thread.Sleep(1000);
        }
        //保存注册码
        public static void SaveSerial(string serialno)
        {
            try
            {
                g_DateLimit = CommonLib.CommonLib.DateLimit(serialno);
                m_ProfileUtils.WriteIniData("App", "SN", serialno, g_IniSysFileName);
                m_LogUtils.WorkLogList.Add(new WorkLog(g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(78),App.GetBehaviorRemark(78)));
                MessageBox.Show(m_LangPackage.TIP_REGISTER_SUCCESS, m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                m_ProfileUtils.WriteIniData("App", "Dt", "", App.g_IniSysFileName);
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception e)
            {
                m_LogUtils.WorkLogList.Add(new WorkLog(g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(79), App.GetBehaviorRemark(79)));
                MessageBox.Show(string.Format(m_LangPackage.TIP_REGISTER_FAILURE, e.ToString()), m_LangPackage.ERROR,
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //检查程序有效期
        public static bool CheckAppValidTime()
        {
            if (g_DateLimit == null)
            {
                MessageBox.Show("注册码无效！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            DateTime dt = DateTime.Now;
            DateTime dt_limit = DateTime.Parse(g_DateLimit);
            int days = dt_limit.Subtract(dt).Days;


            if (!string.IsNullOrEmpty(m_strExpireTime))
            {
                DateTime dt1 = DateTime.Parse(m_strExpireTime);
                if (dt < dt1)
                {
                    MessageBox.Show(m_LangPackage.TIP_SYSTEM_TIME_ERROR, App.m_LangPackage.WARNING, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }


            if (!string.IsNullOrEmpty(m_strExpireTime) || days < 0)
            {
                MessageBox.Show(m_LangPackage.TIP_SYSTEM_REGISTER_EXPIRE, m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
                m_ProfileUtils.WriteIniData("App", "Dt", g_DateLimit, App.g_IniSysFileName);
                InputSerialNoModal inputSerialNoModal = new InputSerialNoModal(SaveSerial, days);
                bool? bClose = inputSerialNoModal.ShowDialog();
                if (bClose == false) return false;
            }
            if (days < 30)//小于30日提醒
            {
                InputSerialNoModal inputSerialNoModal = new InputSerialNoModal(SaveSerial, days);
                bool? bClose = inputSerialNoModal.ShowDialog();
                if (bClose == false) return true;
            }
            return true;
        }
        //检查主机通信状态返回
        public static bool CheckMainframeStatus()
        {
            if (GetMainWindow().LB_MAIN_STATUS.Content.Equals(StaticParam.Startup_status_Arr[1]))
            {
                MessageBox.Show(m_LangPackage.TIP_MAINBOARD_COMM_ERROR, m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (App.m_bLaunchInitialize)
            {
                MessageBox.Show(m_LangPackage.TIP_INITIALIZING, m_LangPackage.ERROR, MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
       }
        //清理数据
        public static void ClearDatabase()
        {
            int nRecord = 0;
            //清理实验数据表
            nRecord += m_SQLiteDBUtils.ExecuteNonQuery(sql.SQL.SQL_D_TESTDAT);
            m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_U_SEQBYTABLE, sql.SQL.T_TESTDATA));
            //清理实验温度数据表
            nRecord += m_SQLiteDBUtils.ExecuteNonQuery(sql.SQL.SQL_D_EXPCUPTEMP);
            m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_U_SEQBYTABLE, sql.SQL.T_EXPCUPTEMP));
            //清理方法表
            nRecord += m_SQLiteDBUtils.ExecuteNonQuery(sql.SQL.SQL_D_METHOD);
            m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_U_SEQBYTABLE, sql.SQL.T_METHOD));
            //清理工作日志表
            nRecord += m_SQLiteDBUtils.ExecuteNonQuery(sql.SQL.SQL_D_WORKLOG);
            m_SQLiteDBUtils.ExecuteNonQuery(String.Format(sql.SQL.SQL_U_SEQBYTABLE, sql.SQL.T_WORKLOG));
            //清理用户表
            nRecord += m_SQLiteDBUtils.ExecuteNonQuery(sql.SQL.SQL_D_ALLACCOUNT);
            //恢复内置系统管理员账户
            m_SQLiteDBUtils.ExecuteNonQuery(sql.SQL.SQL_U_RETRIVERSYSMANAGE);

            //记录恢复出厂化日志
            App.m_LogUtils.WorkLogList.Clear();
            App.m_LogUtils.WorkLogList.Add(new WorkLog(App.g_TSession.TTUser.LoginName, App.GetLogType(1), App.GetBehavior(77), String.Format(App.GetBehaviorRemark(77), nRecord)));
            if (App.m_SQLiteDBUtils != null) App.WriteWorkLog();   //将缓存日志写入库
            MessageBox.Show(String.Format(App.m_LangPackage.TIP_CLEANDB_OVER, nRecord), m_LangPackage.TIP, MessageBoxButton.OK, MessageBoxImage.Information);
            Current.Shutdown();
        }
    }
}
