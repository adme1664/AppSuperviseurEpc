using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ht.ihsi.rgph.epc.supervision.utils
{
    public class ReponseModel
    {
        public string CodeUniqueReponse { get; set; }
        public string CodeReponse { get; set; }
        public string LibelleReponse { get; set; }
        public string CodeQuestion { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }

        public ReponseModel()
        {

        }
        public ReponseModel(string code, string libelle)
        {
            CodeReponse = code;
            LibelleReponse = libelle;
        }
    }
}
