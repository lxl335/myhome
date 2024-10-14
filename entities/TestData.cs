using System;
using System.ComponentModel.DataAnnotations;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.entities
{
    [Serializable]
    public partial class TestData
    {
        #region private
        private int m_ID;
        private string m_Status;
        private string m_StartTime;
        private string m_EndTime;
        private string m_LoginName;
        private string m_MethodName;
        private string m_Manufacturer;
        private string m_BatchNo;
        private string m_TestDepartment;
        private string m_TestMethod;
        private double m_TempWaterbox;
        private double m_TempSetting;            //温度设置
        private double m_SolventVolume;          //溶媒体积
        private int m_AllTimeSpan;               //总时长
        private string m_SpeedMode;              //转速模式
        private string m_ElectricmotorMode;      //电机模式
        private int m_FrontRowSpeed_1;           //前排转速1
        private int m_FrontRowSpeed_2;           //前排转速2
        private int m_BackRowSpeed_1;            //后排转速1
        private int m_BackRowSpeed_2;            //后排转速2
        private int m_SampleTimes;               //取样次数
        private double m_SampleVolume;           //取样容量
        private string m_Auto_fluid_infusion;    //是否自动补液
        private double m_First_filter_volume;    //初滤体积
        private SampleTime[] m_Sample_Time_Arr = new SampleTime[StaticParam.SAMPLE_TIME];  //取样时间数组
        private string[] m_CupTemp_Arr = new string[StaticParam.CUP_NUM];  //小杯温度数组
        private int m_SampleTime_1;//取样时间1
        private int m_SampleTime_2;//取样时间2
        private int m_SampleTime_3;//取样时间1
        private int m_SampleTime_4;//取样时间1
        private int m_SampleTime_5;//取样时间1
        private int m_SampleTime_6;//取样时间1
        private int m_SampleTime_7;//取样时间1
        private int m_SampleTime_8;//取样时间1
        private int m_SampleTime_9;//取样时间1
        private int m_SampleTime_10;//取样时间1
        private int m_SampleTime_11;//取样时间1
        private int m_SampleTime_12;//取样时间1
        private int m_SampleTime_13;//取样时间1
        private int m_SampleTime_14;//取样时间1
        private int m_SampleTime_15;//取样时间1
        private int m_SampleTime_16;//取样时间1
        private int m_SampleTime_17;//取样时间1
        private int m_SampleTime_18;//取样时间1
        private int m_SampleTime_19;//取样时间1
        private int m_SampleTime_20;//取样时间1
        private int m_SampleTime_21;//取样时间1
        private int m_SampleTime_22;//取样时间1
        private int m_SampleTime_23;//取样时间1
        private int m_SampleTime_24;//取样时间1
        private int m_SampleTime_25;//取样时间1
        private int m_SampleTime_26;//取样时间1
        private int m_SampleTime_27;//取样时间1
        private int m_SampleTime_28;//取样时间1
        private int m_SampleTime_29;//取样时间1
        private int m_SampleTime_30;//取样时间1
        private int m_SampleTime_31;//取样时间1
        private int m_SampleTime_32;//取样时间1
        private int m_SampleTime_33;//取样时间1
        private int m_SampleTime_34;//取样时间1
        private int m_SampleTime_35;//取样时间1
        private int m_SampleTime_36;//取样时间1
        private int m_SampleTime_37;//取样时间1
        private int m_SampleTime_38;//取样时间1
        private int m_SampleTime_39;//取样时间1
        private int m_SampleTime_40;//取样时间1
        private int m_FrontRowStartTime_1;   //前排启动时间1
        private int m_FrontRowStartTime_2;   //前排启动时间2
        private int m_BackRowStartTime_1;    //后排启动时间1
        private int m_BackRowStartTime_2;    //后排启动时间2
        private bool m_DilutionEnabled;          //是否稀释
        private int m_DilutionRatio;             //稀释倍数
        #endregion

        public TestData()
        {
            for (int i = 0; i < StaticParam.SAMPLE_TIME; i++)
            {
                m_Sample_Time_Arr[i] = new SampleTime();//必须分别开辟空间
                m_Sample_Time_Arr[i].Key = i + 1;
            }
        }
        [Key]
        public int ID { get => m_ID; set => m_ID = value; }
        public string Status { get => m_Status; set => m_Status = value; }
        public string StartTime { get => m_StartTime; set => m_StartTime = value; }
        public string EndTime { get => m_EndTime; set => m_EndTime = value; }
        public string LoginName { get => m_LoginName; set => m_LoginName = value; }
        public string MethodName { get => m_MethodName; set => m_MethodName = value; }
        public string Manufacturer { get => m_Manufacturer; set => m_Manufacturer = value; }
        public string BatchNo { get => m_BatchNo; set => m_BatchNo = value; }
        public string TestDepartment { get => m_TestDepartment; set => m_TestDepartment = value; }
        public string TestMethod { get => m_TestMethod; set => m_TestMethod = value; }
        public double TempWaterBox { get => m_TempWaterbox; set => m_TempWaterbox = value; }
        public double TempSetting { get => m_TempSetting; set => m_TempSetting = value; }
        public double SolventVolume { get => m_SolventVolume; set => m_SolventVolume = value; }
        public int AllTimeSpan { get => m_AllTimeSpan; set => m_AllTimeSpan = value; }
        public string SpeedMode { get => m_SpeedMode; set => m_SpeedMode = value; }
        public string ElectricmotorMode { get => m_ElectricmotorMode; set => m_ElectricmotorMode = value; }
        public int FrontRowSpeed_1 { get => m_FrontRowSpeed_1; set => m_FrontRowSpeed_1 = value; }
        public int FrontRowSpeed_2 { get => m_FrontRowSpeed_2; set => m_FrontRowSpeed_2 = value; }
        public int BackRowSpeed_1 { get => m_BackRowSpeed_1; set => m_BackRowSpeed_1 = value; }
        public int BackRowSpeed_2 { get => m_BackRowSpeed_2; set => m_BackRowSpeed_2 = value; }
        public int FrontRowStartTime_1 { get => m_FrontRowStartTime_1; set => m_FrontRowStartTime_1 = value; }
        public int FrontRowStartTime_2 { get => m_FrontRowStartTime_2; set => m_FrontRowStartTime_2 = value; }
        public int BackRowStartTime_1 { get => m_BackRowStartTime_1; set => m_BackRowStartTime_1 = value; }
        public int BackRowStartTime_2 { get => m_BackRowStartTime_2; set => m_BackRowStartTime_2 = value; }
        
        public int SampleTimes { get => m_SampleTimes; set => m_SampleTimes = value; }
        public double SampleVolume { get => m_SampleVolume; set => m_SampleVolume = value; }
        public SampleTime[] Sample_Time_Arr
        {
            get { return m_Sample_Time_Arr; }
            set { m_Sample_Time_Arr = value; }
        }
        public string[] CupTemp_Arr
        {
            get { return m_CupTemp_Arr; }
            set { m_CupTemp_Arr = value; }
        }
        public int SampleTime_1 { get => m_SampleTime_1; set => m_SampleTime_1 = value; }
        public int SampleTime_2 { get => m_SampleTime_2; set => m_SampleTime_2 = value; }
        public int SampleTime_3 { get => m_SampleTime_3; set => m_SampleTime_3 = value; }
        public int SampleTime_4 { get => m_SampleTime_4; set => m_SampleTime_4 = value; }
        public int SampleTime_5 { get => m_SampleTime_5; set => m_SampleTime_5 = value; }
        public int SampleTime_6 { get => m_SampleTime_6; set => m_SampleTime_6 = value; }
        public int SampleTime_7 { get => m_SampleTime_7; set => m_SampleTime_7 = value; }
        public int SampleTime_8 { get => m_SampleTime_8; set => m_SampleTime_8 = value; }
        public int SampleTime_9 { get => m_SampleTime_9; set => m_SampleTime_9 = value; }
        public int SampleTime_10 { get => m_SampleTime_10; set => m_SampleTime_10 = value; }
        public int SampleTime_11 { get => m_SampleTime_11; set => m_SampleTime_11 = value; }
        public int SampleTime_12 { get => m_SampleTime_12; set => m_SampleTime_12 = value; }
        public int SampleTime_13 { get => m_SampleTime_13; set => m_SampleTime_13 = value; }
        public int SampleTime_14 { get => m_SampleTime_14; set => m_SampleTime_14 = value; }
        public int SampleTime_15 { get => m_SampleTime_15; set => m_SampleTime_15 = value; }
        public int SampleTime_16 { get => m_SampleTime_16; set => m_SampleTime_16 = value; }
        public int SampleTime_17 { get => m_SampleTime_17; set => m_SampleTime_17 = value; }
        public int SampleTime_18 { get => m_SampleTime_18; set => m_SampleTime_18 = value; }
        public int SampleTime_19 { get => m_SampleTime_19; set => m_SampleTime_19 = value; }
        public int SampleTime_20 { get => m_SampleTime_20; set => m_SampleTime_20 = value; }
        public int SampleTime_21 { get => m_SampleTime_21; set => m_SampleTime_21 = value; }
        public int SampleTime_22 { get => m_SampleTime_22; set => m_SampleTime_22 = value; }
        public int SampleTime_23 { get => m_SampleTime_23; set => m_SampleTime_23 = value; }
        public int SampleTime_24 { get => m_SampleTime_24; set => m_SampleTime_24 = value; }
        public int SampleTime_25 { get => m_SampleTime_25; set => m_SampleTime_25 = value; }
        public int SampleTime_26 { get => m_SampleTime_26; set => m_SampleTime_26 = value; }
        public int SampleTime_27 { get => m_SampleTime_27; set => m_SampleTime_27 = value; }
        public int SampleTime_28 { get => m_SampleTime_28; set => m_SampleTime_28 = value; }
        public int SampleTime_29 { get => m_SampleTime_29; set => m_SampleTime_29 = value; }
        public int SampleTime_30 { get => m_SampleTime_30; set => m_SampleTime_30 = value; }
        public int SampleTime_31 { get => m_SampleTime_31; set => m_SampleTime_31 = value; }
        public int SampleTime_32 { get => m_SampleTime_32; set => m_SampleTime_32 = value; }
        public int SampleTime_33 { get => m_SampleTime_33; set => m_SampleTime_33 = value; }
        public int SampleTime_34 { get => m_SampleTime_34; set => m_SampleTime_34 = value; }
        public int SampleTime_35 { get => m_SampleTime_35; set => m_SampleTime_35 = value; }
        public int SampleTime_36 { get => m_SampleTime_36; set => m_SampleTime_36 = value; }
        public int SampleTime_37 { get => m_SampleTime_37; set => m_SampleTime_37 = value; }
        public int SampleTime_38 { get => m_SampleTime_38; set => m_SampleTime_38 = value; }
        public int SampleTime_39 { get => m_SampleTime_39; set => m_SampleTime_39 = value; }
        public int SampleTime_40 { get => m_SampleTime_40; set => m_SampleTime_40 = value; }

        public string Auto_Fluid_Infusion { get => m_Auto_fluid_infusion; set => m_Auto_fluid_infusion = value; }
        public double First_Filter_Volume { get => m_First_filter_volume; set => m_First_filter_volume = value; }
        public bool DilutionEnabled
        {
            get { return m_DilutionEnabled; }
            set { m_DilutionEnabled = value; }
        }
        public int DilutionRatio
        {
            get { return m_DilutionRatio; }
            set { m_DilutionRatio = value; }
        }
    }
}
