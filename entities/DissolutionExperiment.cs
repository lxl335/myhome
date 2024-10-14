using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.INST.DissolutionClient.entities
{
    [Serializable]
    public partial class DissolutionExperiment : DissolutionMethod
    {
        private int m_Id;                             //实验ID
        private string m_ExpNo;                       //实验编号
        private string m_Status;                      //实验状态
        private string m_StartTime;                   //实验开始时间
        private string m_EndTime;                     //实验结束时间
        private string m_LoginName;                   //实验人账号ID
        private double m_TempWaterbox;                //水箱温度
        private List<SampleTime> m_SampleTimeList;    //取样时间点列表
        private List<List<string>> m_CupTempList;     //小杯温度列表

        public DissolutionExperiment()
        {
        }
        [Key]
        public int ID { get => m_Id; set => m_Id = value; }
        public string ExpNo { get => m_ExpNo; set => m_ExpNo = value; }
        public string Status { get => m_Status; set => m_Status = value; }
        public string StartTime { get => m_StartTime; set => m_StartTime = value; }
        public string EndTime { get => m_EndTime; set => m_EndTime = value; }
        public string LoginName { get => m_LoginName; set => m_LoginName = value; }
        public double TempWaterBox { get => m_TempWaterbox; set => m_TempWaterbox = value; }
        public List<SampleTime> SampleTimeList { get => m_SampleTimeList; set => m_SampleTimeList = value; }
        public List<List<string>> CupTempList { get => m_CupTempList; set => m_CupTempList = value; }
    }
}
