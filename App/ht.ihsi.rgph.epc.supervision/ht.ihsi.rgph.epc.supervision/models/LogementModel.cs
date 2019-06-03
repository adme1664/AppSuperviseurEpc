using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class LogementModel
    {
        public long logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> qlCategLogement { get; set; }
        public Nullable<long> qlin1NumeroOrdre { get; set; }
        public Nullable<long> qlin2StatutOccupation { get; set; }
        public Nullable<long> qlin3TypeLogement { get; set; }
        public Nullable<long> qlin4IsHaveIndividuDepense { get; set; }
        public Nullable<long> qlin5NbreTotalMenage { get; set; }
        public Nullable<long> statut { get; set; }
        public Nullable<long> isValidated { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public Nullable<long> nbrTentative { get; set; }
        public string codeAgentRecenceur { get; set; }
        public Nullable<long> isVerified { get; set; }
        public String LogementName { get; set; }
        public bool IsValidated { get; set; }
    }
}
