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
    
    public partial class Tbl_DetailsRapport
    {
        public long DetailsRapportId { get; set; }
        public Nullable<long> RapportId { get; set; }
        public long Domaine { get; set; }
        public long SousDomaine { get; set; }
        public Nullable<long> Probleme { get; set; }
        public Nullable<long> Solution { get; set; }
        public string Precisions { get; set; }
        public string Suggestions { get; set; }
        public string Suivi { get; set; }
        public string Commentaire { get; set; }
    }
}
