using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.beans
{
    public class Utilisateur
    {
        public string username { get; set; }
        public string password { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string profileId { get; set; } 
    }
}
