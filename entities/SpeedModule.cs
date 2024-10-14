using System;

namespace Pharmacy.INST.DissolutionClient.entities
{
    [Serializable]
    public class SpeedModule
    {
        private string m_SpeedMode;             //转速模式
        private string m_ElectricmotorMmode;    //电机模式
        private int m_FrontRowSpeed_1;          //前排转速1
        private int m_FrontRowSpeed_2;          //前排转速2
        private int m_FrontRowStartTime_1;   //前排启动时间1
        private int m_FrontRowStartTime_2;   //前排启动时间2

        private int m_BackRowSpeed_1;           //后排转速1
        private int m_BackRowSpeed_2;           //后排转速2
        private int m_BackRowStartTime_1;    //后排启动时间1
        private int m_BackRowStartTime_2;    //后排启动时间2

        public string SpeedMode
        {
            get { return m_SpeedMode; }
            set { m_SpeedMode = value; }
        }
        public string ElectricmotorMmode
        {
            get { return m_ElectricmotorMmode; }
            set { m_ElectricmotorMmode = value; }
        }
        public int FrontRowSpeed_1
        {
            get { return m_FrontRowSpeed_1; }
            set { m_FrontRowSpeed_1 = value; }
        }
        public int FrontRowSpeed_2
        {
            get { return m_FrontRowSpeed_2; }
            set { m_FrontRowSpeed_2 = value; }
        }
        public int FrontRowStartTime_1
        {
            get { return m_FrontRowStartTime_1; }
            set { m_FrontRowStartTime_1 = value; }
        }
        public int FrontRowStartTime_2
        {
            get { return m_FrontRowStartTime_2; }
            set { m_FrontRowStartTime_2 = value; }
        }
        public int BackRowSpeed_1
        {
            get { return m_BackRowSpeed_1; }
            set { m_BackRowSpeed_1 = value; }
        }
        public int BackRowSpeed_2
        {
            get { return m_BackRowSpeed_2; }
            set { m_BackRowSpeed_2 = value; }
        }
        public int BackRowStartTime_1
        {
            get { return m_BackRowStartTime_1; }
            set { m_BackRowStartTime_1 = value; }
        }
        public int BackRowStartTime_2
        {
            get { return m_BackRowStartTime_2; }
            set { m_BackRowStartTime_2 = value; }
        }
    }
}
