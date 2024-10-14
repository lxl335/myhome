using System;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.INST.DissolutionClient.entities
{
    [Serializable]
    public partial class VerifyData
    {
        #region private
        private int m_ID;
        private string m_LoginName;                 //账户名
        private string m_VerifyTime;                //校验时间
        private string m_NextVerifyTime;            //下一次校验日期
        private string m_Status;                    //是否已验证
        private string m_Remark;                    //备注
        private int m_expid;                        //实验ID，关联实验

        #endregion
        public VerifyData()
        {        
        }
        [Key]
        public int ID { get => m_ID; set => m_ID = value; }
        public string LoginName { get => m_LoginName; set => m_LoginName = value; }
        public string VerifyTime { get => m_VerifyTime; set => m_VerifyTime = value; }
        public string NextVerifyTime { get => m_NextVerifyTime; set => m_NextVerifyTime = value; }
        public string Status { get => m_Status; set => m_Status = value; }
        public string Remark { get => m_Remark; set => m_Remark = value; }
        public int ExpId { get => m_expid; set => m_expid = value; }
    }
}
