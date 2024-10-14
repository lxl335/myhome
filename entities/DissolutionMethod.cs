using System;
using Pharmacy.INST.CommonLib;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.entities
{
    [Serializable]
    public class DissolutionMethod : BaseMethod
    {
        private string m_DissolutionMethodName;  //溶出方法
        private double m_TempSetting;            //温度设置
        private double m_SolventVolume;          //溶媒体积
        private SpeedModule m_SpeedModule;       //转速模型
        private string m_Auto_fluid_infusion;    //是否自动补液
        private double m_First_filter_volume;    //初滤体积
        private bool m_DilutionEnabled;          //是否稀释
        private int m_DilutionRatio;             //稀释倍数
        private SampleTime[] m_Sample_Time_Arr = new SampleTime[StaticParam.SAMPLE_TIME];  //取样时间数组

        public DissolutionMethod()
        {
            m_SpeedModule = new SpeedModule();
            for (int i = 0; i < StaticParam.SAMPLE_TIME; i++)
            {
                m_Sample_Time_Arr[i] = new SampleTime();//必须分别开辟空间
                m_Sample_Time_Arr[i].Key = i + 1;
            }
        }
        public string DissolutionMethodName
        {
            get { return m_DissolutionMethodName; }
            set { m_DissolutionMethodName = value; }
        }
        public double TempSetting
        {
            get { return m_TempSetting; }
            set { m_TempSetting = value; }
        }
        public double SolventVolume
        {
            get { return m_SolventVolume; }
            set { m_SolventVolume = value; }
        }
        public SpeedModule oSpeedModule
        {
            get { return m_SpeedModule; }
            set { m_SpeedModule = value; }
        }

        public string Auto_Fluid_Infusion
        {
            get { return m_Auto_fluid_infusion; }
            set { m_Auto_fluid_infusion = value; }
        }
        public double First_filter_volume
        {
            get { return m_First_filter_volume; }
            set { m_First_filter_volume = value; }
        }
        public SampleTime[] Sample_Time_Arr
        {
            get { return m_Sample_Time_Arr; }
            set { m_Sample_Time_Arr = value; }
        }
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
        public string CheckFieldValidation()
        {
            string fieldName = "";
            if (base.MethodName.Equals("")) return "方法名";
            if (base.Manufacturer.Equals("")) return "生产商";
            if (base.BatchNo.Equals("")) return "批号";
            if (base.TestDepartment.Equals("")) return "检测单位";
            if (this.m_DissolutionMethodName.Equals("")) return "溶出方法"; 
            return fieldName;
        }

        public override void Reset()
        {
            base.Reset();
            m_DissolutionMethodName = "";  //溶出方法
            m_TempSetting = 0;            //温度设置
            m_SolventVolume = 0;         //溶媒体积
            
            m_SpeedModule.FrontRowSpeed_1 = 0;
            m_SpeedModule.FrontRowSpeed_2 = 0;
            m_SpeedModule.FrontRowStartTime_1 = 0;
            m_SpeedModule.FrontRowStartTime_2 = 0;
            m_SpeedModule.BackRowSpeed_1 = 0;
            m_SpeedModule.BackRowSpeed_2 = 0;
            m_SpeedModule.BackRowStartTime_1 = 0;
            m_SpeedModule.BackRowStartTime_2 = 0;
            m_Auto_fluid_infusion = "";
            m_First_filter_volume = 0;
            m_DilutionEnabled = false;
            m_DilutionRatio = 0;

            for (int i = 0; i < StaticParam.SAMPLE_TIME; i++)
            {
                m_Sample_Time_Arr[i].TimeValue = 0;
            }

            m_SpeedModule.SpeedMode = "";
            m_SpeedModule.ElectricmotorMmode = "";
            return;
        }
    }
}
