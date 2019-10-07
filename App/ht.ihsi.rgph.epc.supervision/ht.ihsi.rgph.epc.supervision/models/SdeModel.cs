using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
   public class SdeModel
    {
        public string SdeId { get; set; }
        public string CodeDistrict { get; set; }
        public string DeptId { get; set; }
        public string ComId { get; set; }
        public Nullable<long> AgentId { get; set; }
        public string NoOrdre { get; set; }
        public string VqseId { get; set; }
        public string Zone { get; set; }
        public String SdeName { get; set; }
        public string AgentName { get; set; }
    }
}
