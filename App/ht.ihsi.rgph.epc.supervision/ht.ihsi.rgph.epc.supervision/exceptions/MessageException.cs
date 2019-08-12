using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.exceptions
{
    public class MessageException:Exception
    {
        public String Message { get; set; }
        public MessageException()
        {

        }
        public MessageException(string message)
        {
            this.Message = message;
        }
    }
}
