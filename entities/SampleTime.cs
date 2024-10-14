using System;

namespace Pharmacy.INST.DissolutionClient.entities
{
    [Serializable]
    public class SampleTime
    {
        private int m_Key;
        private int m_TmVal;
     
        public SampleTime()
        {
        }
        public int Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }
        public int TimeValue
        {
            get { return m_TmVal; }
            set { m_TmVal = value; }
        }
    }
}
