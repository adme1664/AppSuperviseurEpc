using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
   public class UtilisateurModel
    {
        public int UtilisateurId { get; set; }
        public string CodeUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string MotDePasse { get; set; }
        public Nullable<byte> Statut { get; set; }
        public int ProfileId { get; set; }
        private string SdeId { get; set; }
    }
}
