﻿using ht.ihsi.rgph.epc.database.entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsi.Rgph.DataAccess.Repositories
{
    public class GenericDatabaseContext:DbContext    
    {
        public GenericDatabaseContext(string connectionString)
            : base(connectionString)
        {

        }
        public virtual DbSet<tbl_batiment> tbl_batiment { get; set; }
        public virtual DbSet<tbl_AncienMembre> tbl_emigre { get; set; }
        public virtual DbSet<tbl_individu> tbl_individu { get; set; }
        public virtual DbSet<tbl_logement> tbl_logement { get; set; }
        public virtual DbSet<tbl_menage> tbl_menage { get; set; }
        public virtual DbSet<tbl_personnel> tbl_personnel { get; set; }
        public virtual DbSet<tbl_categorie_question> tbl_categorie_question { get; set; }
        public virtual DbSet<tbl_commune> tbl_commune { get; set; }
        public virtual DbSet<tbl_departement> tbl_departement { get; set; }
        public virtual DbSet<tbl_domaine_etude> tbl_domaine_etude { get; set; }
        public virtual DbSet<tbl_module> tbl_module { get; set; }
        public virtual DbSet<tbl_pays> tbl_pays { get; set; }
        public virtual DbSet<tbl_question> tbl_question { get; set; }
        public virtual DbSet<tbl_question_module> tbl_question_module { get; set; }
        public virtual DbSet<tbl_question_reponse> tbl_question_reponse { get; set; }
        public virtual DbSet<tbl_vqse> tbl_vqse { get; set; }
    }
}
