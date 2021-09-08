using System.Collections.Generic;

namespace IFSAero.Models
{
    public class Candidate
    {
        public Candidate()
        {
            experience = new List<Experience>();
        }

        public string candidateId { get; set; }
        public string fullName { get; set; }
        public List<Experience> experience { get; set; }
    }
}
