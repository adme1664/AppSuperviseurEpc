﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
   public  class AncienMembreModel
    {
        public long ancienMembreId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public string sdeId { get; set; }
        public Nullable<long> q1NoOrdre { get; set; }
        public string qp2APrenom { get; set; }
        public string qp2BNom { get; set; }
        public Nullable<long> qp4Sexe { get; set; }
        public Nullable<long> q5EstMortOuQuitter { get; set; }
        public Nullable<long> q6HabiteDansMenage { get; set; }
        public Nullable<long> q7DateQuitterMenageJour { get; set; }
        public Nullable<long> q7DateQuitterMenageMois { get; set; }
        public Nullable<long> q7DateQuitterMenageAnnee { get; set; }
        public Nullable<long> q7bDateMouriJour { get; set; }
        public Nullable<long> q7bDateMouriMois { get; set; }
        public Nullable<long> q7bDateMouriAnnee { get; set; }
        public Nullable<long> q8DateNaissanceJour { get; set; }
        public Nullable<long> q8DateNaissanceMois { get; set; }
        public Nullable<long> q8DateNaissanceAnnee { get; set; }
        public Nullable<long> q9Age { get; set; }
        public Nullable<long> q10LienDeParente { get; set; }
        public Nullable<long> q11Nationalite { get; set; }
        public string q11PaysNationalite { get; set; }
        public Nullable<long> q12NiveauEtude { get; set; }
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
    }
}