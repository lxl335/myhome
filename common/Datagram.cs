namespace Pharmacy.INST.DissolutionClient.common
{
    public class Datagram
    {
        //探测心跳
        public static string W_ECHO               = "A5 04 20 01 5A";                        //心跳帧
        //仪器
        public static string V_STATUS             = "A5 03 25 5A";                           //开机查询复位状态

        //溶出仪本体 V1电机驱动器通信协议
        public static string V1_E_STATUS          = "A5 04 20 01 5A";                        //询问运动部件状态
        public static string V1_E1_UP             = "A5 0B 10 01 00 12 00 00 FF FF FF 5A";   //1号步进电机以200rpm/min 的速度上升
        public static string V1_E1_DOWN           = "A5 0B 10 01 00 11 00 00 {0} {1} {2} 5A";//1号步进电机以200rpm/min 的速度下降"A5 0B 10 01 00 11 00 00 00 FF 00 5A";
        public static string V1_E1_DOWN_BOTTOM    = "A5 0B 10 01 00 11 00 00 02 0d 00 5A";   //1号步进电机以200rpm/min 的速度下降到底;
        public static string V1_E1_STOP           = "A5 0B 10 01 00 10 00 00 00 FF 00 5A";   //1号步进电机停止
        public static string V1_E2_ROTATION       = "A5 0B 10 01 00 21 {0} {1} FF FF FF 5A"; //2号步进电机以{0}{1} Hex rpm/min的速度连续运动
        public static string V1_E2_STOP           = "A5 0B 10 01 00 20 00 00 FF FF FF 5A";   //2号步进电机停止
        public static string V1_E3_ROTATION       = "A5 0B 10 04 00 21 {0} {1} FF FF FF 5A"; //3号步进电机以{0}{1} Hex rpm/min的速度连续运动 双电机 
        public static string V1_E3_STOP           = "A5 0B 10 04 00 20 00 00 FF FF FF 5A";   //3号步进电机停止

        //取样器 V2电动机驱动器通信协议
        public static string V2_E_STATUS          = "A5 04 20 02 5A";                        //询问运动部件状态
        public static string V2_E1_RESET          = "A5 0B 10 02 00 11 00 00 00 FF FF 5A";   //取样器四通阀复位
        public static string V2_E1_ROTATE         = "A5 0B 10 02 {0} 12 00 00 00 5F FF 5A";  //1号步进电机控制四通阀运行{0}步
        public static string V2_E2_UP             = "A5 0B 10 02 00 21 00 00 0F FF FF 5A";   //2号步进电机上升运动到底
        public static string V2_E2_DOWN           = "A5 0B 10 02 00 22 00 00 0F FF FF 5A";   //2号步进电机下降运动到底
        public static string V2_E2_SUCK           = "A5 0B 10 02 00 21 00 00 {0} {1} {2} 5A";//吸液
        public static string V2_E2_OUT            = "A5 0B 10 02 00 22 00 00 {0} {1} {2} 5A";//出液
        public static string V2_E2_STOP           = "A5 0B 10 02 00 20 00 00 00 FF FF 5A";   //2号步进电机停止

        //收集器 V3电动机驱动器通信协议
        public static string V3_E_STATUS          = "A5 04 20 03 5A";                        //询问运动部件状态
        public static string V3_E1_RESET          = "A5 0B 10 03 00 11 00 00 01 FF FF 5A";   //1号步进电机控制滑杆复位到原点
        public static string V3_E1_MOVE           = "A5 0B 10 03 {0} 12 00 00 01 FF FF 5A";  //1号步进电机控制滑杆从复位点开始运行到第{0}个计数位置停止
        public static string V3_E1_STOP           = "A5 0B 10 03 00 10 00 00 0F FF FF 5A";   //1号步进电机停止
        public static string V3_E2_UP             = "A5 0B 10 03 00 21 00 00 00 FF FF 5A";   //2号步进电机以固定的速度上升运动
        public static string V3_E2_DOWN           = "A5 0B 10 03 00 22 00 00 00 FF F0 5A";   //2号步进电机以固定的速度下降运动
        public static string V3_E2_STOP           = "A5 0B 10 03 00 20 00 00 00 FF F0 5A";   //2号步进电机停止 

        //加热装置通信协议
        public static string T_HEAT_START         = "A5 05 13 8{0} {1} 5A";                  //控制加热器按照设定温度（12bit Hex）开始加热
        public static string T_HEAT_STOP          = "A5 05 13 00 00 5A";                     //控制加热器停止加热
        public static string T_TEMP_CALI          = "A5 05 14 {0}{1} {2} 5A";                //温度采集校准
        public static string T_TEMP_CALI_RESET    = "A5 05 14 11 00 5A";                     //温度采集校准清空
        public static string T_CUPTEMP_CALI       = "A5 1D 1F {0} 5A";                       //水杯温度校准  
        public static string T_BOXTEMP_READ       = "A5 03 1A 5A";                           //读取当前水温值
        public static string T_CUPTEMP_READ       = "A5 03 1D 5A";                           //读取当前杯内温度
        public static string T_BOXTEMP_CALI_RESET = "A5 05 14 {0}1 00 5A";                   //重置通道{0}温度标定值为0 
        public static string T_BOXTEMP_CALI_READ  = "A5 03 18 5A";                           //读取当前水箱温度标定值
        public static string T_CUP_CALI_READ      = "A5 03 21 5A";                           //读取水杯温度标定值

        //升降机
        public static string L_LIFTING_UP         = "A5 04 11 01 5A";                        //升降机上升
        public static string L_LIFTING_STOP       = "A5 04 11 00 5A";                        //升降机停止
        public static string L_LIFTING_DOWN       = "A5 04 11 02 5A";                        //升降机下降

        //投药装置
        public static string H_PILL_STOP          = "A5 04 12 00 5A";                        //投药机构停止
        public static string H_PILL_PUT           = "A5 04 12 01 5A";                        //投药
        public static string H_PILL_RESET         = "A5 04 12 02 5A";                        //投药机构复位

        //蜂鸣装置
        public static string B_BEEP_ON            = "A5 04 27 01 5A";                        //启动蜂鸣器
        public static string B_BEEP_OFF           = "A5 04 27 00 5A";                        //关闭蜂鸣器

    }
}