using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht.ihsi.rgph.epc.supervision.utils
{
    public interface IsqliteDataWriter
    {
        bool validate<T>(T obj, string sdeId);
        bool verified<T>(T obj, string sdeId);
        bool changeStatus<T>(T obj, string sdeId);
        bool changeToVerified<T>(T obj, string sdeId, string path);
    }
}
