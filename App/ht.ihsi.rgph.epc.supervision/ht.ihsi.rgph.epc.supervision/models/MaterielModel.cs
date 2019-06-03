using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
   public class MaterielModel
    {
        public long MaterielId { get; set; }
        public string Imei { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Agent { get; set; }
        public string Version { get; set; }
        public Nullable<long> AgentId { get; set; }
        public string DateAssignation { get; set; }
        public Nullable<long> IsConfigured { get; set; }
        public string LastSynchronisation { get; set; }
    }
}
