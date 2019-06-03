using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.models
{
    public class QuestionModule
    {
        public string ordre { get; set; }
        public string codeModule { get; set; }
        public string codeQuestion { get; set; }
        public Nullable<long> estDebut { get; set; }
    }
}
