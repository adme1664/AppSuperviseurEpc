using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class BatimentModel
    {
        public long batimentId { get; set; }
        public string deptId { get; set; }
        public string comId { get; set; }
        public string vqseId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> zone { get; set; }
        public string disctrictId { get; set; }
        public string qhabitation { get; set; }
        public string qlocalite { get; set; }
        public string qadresse { get; set; }
        public string qrec { get; set; }
        public string qepc { get; set; }
        public Nullable<long> qb1Etat { get; set; }
        public Nullable<long> qb2Type { get; set; }
        public Nullable<long> qb3StatutOccupation { get; set; }
        public Nullable<long> qb4NbreLogeIndividuel { get; set; }
        public Nullable<long> statut { get; set; }
        public string dateEnvoi { get; set; }
        public Nullable<long> isValidated { get; set; }
        public Nullable<long> isSynchroToAppSup { get; set; }
        public Nullable<long> isSynchroToCentrale { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string codeAgentRecenceur { get; set; }
        public string BatimentName { get; set; }
        public Nullable<long> isVerified { get; set; }
        public bool isChecked { get; set; }
        public bool IsMalRempli { get; set; }
        public bool IsFinished { get; set; }
        public bool IsNotFinished { get; set; }
        public bool IsValidated { get; set; }

    }

}
