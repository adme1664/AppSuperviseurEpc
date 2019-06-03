using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.utils
{
    public class StringValueAttribute : System.Attribute
    {
        private string _value;

        public string Value
        {
            get { return _value; }
        }
        public StringValueAttribute(string value)
        {
            _value = value;
        }

    }
}
