using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Pharmacy.INST.DissolutionClient.common
{
    public static class StaticParam
    {
        //系统参数
        public static string AppName = "DissolutionClient";
        public static string AppFile = "DissolutionClient.exe";
        public static string AppIniProfile = "system.ini";
        public static string AppCalibrationFile = "Calibration.ini";
        public static string AppLogFile = "system.log";
        public static string AppResReletivePath = "\\res\\themes\\";
        public static string AppResReletivePathEng = "\\res\\themes-eng\\";
        public static string AppReportPath = "report\\";
        public static string DB_NAME = "DCDB.db";
        public static string DB_PWD = "root@byky";
        public static string DEF_ROOTLOGINNAME = "root";
        public static string DEF_ROOTPWD = "root@bjbyky888";
        public static string ROOT_ROLE = "超级管理员";
        public static string ROOT_LOGINNAME = "bjbyky123456";
        public static string ROOT_PWD = "bjbyky@123456";
        public static string SYSMANAGE_ROLE = "系统管理员";
        public static string SYSMANAGE_LOGINNAME = "rc";
        public static string SYSMANAGE_PWD = "rc@123456";
        public static string AppBackgroundFile = "main_bg.png";
        public static string AppBtnBackgroundFile = "button_bg.png";

        //资源
        public static SolidColorBrush GroupBoxBorderBrush = new SolidColorBrush(Color.FromRgb(0x36, 0xC9, 0x65));
        public static string LIGHT_GREEN_FILE = "light_green.png";
        public static string LIGHT_RED_FILE = "light_red.gif";
        //主控台
        public static string[] Startup_status_Arr = { "正常" , "异常" };
        public static string[] DissolutionMethod_Arr = { "桨 法", "篮 法", "小 杯 法", "转 筒 法", "桨 碟 法" };
        public static string[] Auto_fluid_infusion_Arr = { "是", "否" };
        public static string[] Speed_Mode_Arr = { "统一转速", "梯度转速" };
        public static string[] Electricmotor_Mode_Arr = { "单电机", "双电机" };
        public static string[] DilutionRatio_Arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
        //数据检索
        public static string[] m_TestDataListHeadColumn = { "状态", "起始时间", "终止时间", "账号", "名称", "批号", "详细信息" };
        public static string[] m_TestDataListHeadField = { "Status", "StartTime", "EndTime", "LoginName", "MethodName", "BatchNo", "GotoView" };
        public static int[] m_TestDataListHeadWidth = { 140, 210, 210, 140, 220, 200, 110 };
        //设备管理 
        public static string[] TempSetting_Arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "水箱温度标定", "全部" };
        public static string[] ExpStatusType = { "中途中止实验", "完整实验" };  //实验结果状态类型名
        //仪器验证
        public static string[] m_VerifyDataListHeadColumn = { "序号", "账号", "验证时间", "状态", "下次验证时间", "备注"};
        public static string[] m_VerifyDataListHeadField = { "", "LoginName", "VerifyTime", "Status", "NextVerifyTime", "Remark" };
        public static int[] m_VerifyDataListHeadWidth = { 100, 150, 260, 150, 260, 200};
        //键盘管理 
        public static string[] KeyBoardProgram = { "osk_7.exe", "osk_10.exe" };
        public enum ElectricMotor { SIGNLE,DOUBLE };
        public enum ExpStatus { PARTIAL, WHOLE };          //实验结果状态值
        //报告
        public static string[] ReportViewHeadColumn = { "", "文件名"};
        public static string[] ReportViewHeadField = { "Select", "Filename" };
        public static int[] ReportViewHeadWidth = { 30, 270 };
        public static string[] ReportSignType = { "未签名", "已签名" };

        //系统基础参数
        public static string NULLSING = "--------";
        public static string EXPNO = "RC{0}";             //实验编号规则
        public static int BUFFERSIZE = 0x40;              //485||232发送接收缓冲区大小
        public static int SAMPLE_TIME = 40;               //取样次数
        public static int SAMPLETUBE_MAXROW = 20;         //收集器取样试管行最大数
        public static int CUP_NUM = 12;                   //溶出仪杯数
        public static int TIMEOUT_TIMES = 4;              //报文发送超时次数
        public static int ROTATION_MAX_SPEED = 350;       //转桨最大转速
        public static double BASE_VOLUME = 25.0;          //取样注射器基准容量
        public static int BASE_PULSENUM = 0x02D500;       //取样注射器基准脉冲值
        public static int V1_E1_BASE_VOLUME = 400;        //取样针升降基准容量
        public static int V1_E1_BASE_PULSENUM = 0x00FF00; //取样针升降基准脉冲值
        public static int PUTPILL_ROTATE_TIME = 60;       //投药搅拌时间
        public static int SAMPLE_MIN_INTERVAL = 5;        //取样间隔最小时间（分钟）
        public static double DEFAULT_HEATTEMP = 37.0;     //默认初始加热温度
        public static double RATIO_PML = 2.57;            //小杯法/mL脉冲倍率
        public static int CONSOLE_TEMP_COLUMN_NUM = 2;    //主控台温度区域列数

        //系统管理
        public static string[] m_UserListHeadColumn = { "序号", "登录名", "姓名", "电话", "角色", "状态", "最近登录时间", "登录次数", "使用期限", "编辑", "删除" };
        public static string[] m_UserListHeadField = { "", "LoginName", "UserName", "Mobile","RoleName", "Status", "LastLoginTime", "LoginTimes", "ValidDate", "", "" };
        public static int[] m_UserListHeadWidth = { 70, 105, 120, 115,  100, 60, 180, 95, 110, 74, 74 };
        public static string[] EntitiesStatusArr = { "正常", "锁定", "失效" };
        public static string[] BaudArr = { "4800", "9600", "14400" };
    }
}