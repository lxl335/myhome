using System.Collections.Generic;

namespace DissolutionPharmacy.INST.DissolutionClientClient.entities
{
    public class InstructionsTempl
    {
        public List<string> m_InstructionsList;
        public InstructionsTempl()
        {
            m_InstructionsList = new List<string>();
        }
        ~InstructionsTempl()
        {
            m_InstructionsList = null;
        }
        public bool Add(string instruction)
        {
            m_InstructionsList.Add(instruction);
            return true;
        }
    }
}
