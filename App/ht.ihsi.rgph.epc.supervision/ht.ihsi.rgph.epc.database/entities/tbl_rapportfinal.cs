//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ht.ihsi.rgph.epc.database.entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_rapportfinal
    {
        public long rapportFinalId { get; set; }
        public Nullable<long> batimentId { get; set; }
        public Nullable<long> logeId { get; set; }
        public Nullable<long> menageId { get; set; }
        public Nullable<long> repondantPrincipalId { get; set; }
        public Nullable<long> aE_EsKeGenMounKiEde { get; set; }
        public Nullable<long> aE_IsVivreDansMenage { get; set; }
        public Nullable<long> aE_RepondantQuiAideId { get; set; }
        public Nullable<long> f_EsKeGenMounKiEde { get; set; }
        public Nullable<long> f_IsVivreDansMenage { get; set; }
        public Nullable<long> f_RepondantQuiAideId { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public Nullable<long> dureeSaisie { get; set; }
        public Nullable<long> isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
    }
}
