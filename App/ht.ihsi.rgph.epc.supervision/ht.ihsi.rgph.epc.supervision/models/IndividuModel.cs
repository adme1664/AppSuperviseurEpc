using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class IndividuModel
    {
        public long individuId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> q1NoOrdre { get; set; }
        public string qp2APrenom { get; set; }
        public string qp2BNom { get; set; }
        public Nullable<long> qp4Sexe { get; set; }
        public Nullable<long> q5HabiteDansMenage { get; set; }
        public Nullable<long> q6aMembreMenageDepuisQuand { get; set; }
        public Nullable<long> q6bDateMembreMenageJour { get; set; }
        public Nullable<long> q6bDateMembreMenageMois { get; set; }
        public Nullable<long> q6bDateMembreMenageAnnee { get; set; }
        public Nullable<long> q7DateNaissanceJour { get; set; }
        public Nullable<long> q7DateNaissanceMois { get; set; }
        public Nullable<long> q7DateNaissanceAnnee { get; set; }
        public Nullable<long> q8Age { get; set; }
        public Nullable<long> q9LienDeParente { get; set; }
        public Nullable<long> q10Nationalite { get; set; }
        public string q10PaysNationalite { get; set; }
        public Nullable<long> q11NiveauEtude { get; set; }
        public Nullable<long> q12StatutMatrimonial { get; set; }
        public Nullable<long> statut { get; set; }
        public Nullable<long> isValidated { get; set; }
        public Nullable<long> isFieldAllFilled { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
        public Nullable<long> isVerified { get; set; }
        public string Raison { get; set; }
    }
}
