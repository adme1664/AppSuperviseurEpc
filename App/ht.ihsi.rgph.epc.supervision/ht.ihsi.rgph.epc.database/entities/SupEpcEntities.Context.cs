﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SupEpcContext : DbContext
    {
        public SupEpcContext()
            : base("name=SupEpcContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tbl_Agent> Tbl_Agent { get; set; }
        public virtual DbSet<Tbl_Materiel> Tbl_Materiel { get; set; }
        public virtual DbSet<Tbl_RapportPersonnel> Tbl_RapportPersonnel { get; set; }
        public virtual DbSet<Tbl_Sde> Tbl_Sde { get; set; }
        public virtual DbSet<Tbl_Utilisateur> Tbl_Utilisateur { get; set; }
        public virtual DbSet<Tbl_DetailsRapport> Tbl_DetailsRapport { get; set; }
        public virtual DbSet<Tbl_RprtDeroulement> Tbl_RprtDeroulement { get; set; }
    }
}
