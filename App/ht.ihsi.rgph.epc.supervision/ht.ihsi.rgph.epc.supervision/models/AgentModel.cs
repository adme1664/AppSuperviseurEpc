using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class AgentModel
    {
        public long AgentId { get; set; }
        public string CodeUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }
        public string Sexe { get; set; }
        public string Nif { get; set; }
        public string Cin { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string AgentName { get; set; }
    }
}
