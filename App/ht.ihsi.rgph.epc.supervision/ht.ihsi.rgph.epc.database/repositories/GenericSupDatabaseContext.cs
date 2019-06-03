using ht.ihsi.rgph.epc.database.entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.database.repositories
{
    public class GenericSupDatabaseContext:DbContext
    {
        public GenericSupDatabaseContext(string connectionString)
            : base(connectionString)
        {

        }

        public virtual DbSet<Tbl_Utilisateur> Tbl_Utilisateur { get; set; }
        public virtual DbSet<Tbl_Sde> Tbl_Sde { get; set; }
        public virtual DbSet<Tbl_Agent> Tbl_Agent { get; set; }
    }
}
