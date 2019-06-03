using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class MenageModel
    {
        public long menageId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> qm1NoOrdre { get; set; }
        public Nullable<long> qm2TotalIndividuVivant { get; set; }
        public Nullable<long> qm22IsHaveAncienMembre { get; set; }
        public Nullable<long> qm22TotalAncienMembre { get; set; }
        public Nullable<long> statut { get; set; }
        public Nullable<long> isValidated { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
        public Nullable<long> isVerified { get; set; }

        public string MenageName { get; set; }
    }
}
