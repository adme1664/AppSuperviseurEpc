using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class RapportDeroulementModel
    {
        public long RapportId { get; set; }
        public string CodeDistrict { get; set; }
        public string DateRapport { get; set; }
        public string RapportName
        {
            get;
            set;
        }
        public List<DetailsRapportModel> RdcDetails { get; set; }
    }
}
