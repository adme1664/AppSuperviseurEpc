using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
   public class RapportFlagModel
    {
        public int ParentID { get; set; }
        public int ID { get; set; }
        public string Flag;
        public string Type { get; set; }
        public int Total { get; set; }
        public IndividuModel Individu { get; set; }
    }
}
