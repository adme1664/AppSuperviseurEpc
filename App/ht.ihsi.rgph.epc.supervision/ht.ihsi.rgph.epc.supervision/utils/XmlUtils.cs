using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ht.ihsi.rgph.epc.supervision.utils
{
    public class XmlUtils
    {
        XElement element;
        private string adrServer;
        private string uri;

        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public string AdrServer
        {
            get { return adrServer; }
            set { adrServer = value; }
        }
        public XmlUtils()
        {

        }
        public XmlUtils(string uri)
        {
            element = getRootElement(uri);
            this.uri = uri;
        }
        public XElement getRootElement(string uri)
        {
            if (uri != null)
            {
                return XElement.Load(uri);
            }
            return null;
        }

        public string getAdrServer()
        {
            if (element != null)
            {
                XElement adrServer = element.Element(Constant.XML_ELEMENT_ADR_SERVER);
                if (adrServer != null)
                {
                    return adrServer.Value;
                }
            }
            return null;
        }
        public string getString(string balise)
        {
            if (element != null)
            {
                XElement elmnt = element.Element(balise);
                if (elmnt != null)
                {
                    return elmnt.Value;
                }
            }
            return null;
        }
        public void UpdateAdrServer(string newAdress)
        {
            if (element != null)
            {
                XElement ele = element.Element(Constant.XML_ELEMENT_ADR_SERVER);
                if (ele != null)
                {
                    ele.Value = newAdress;
                    element.Save(uri);
                }
            }
        }
        public bool updateEnvironnementVariable(string variable)
        {
            if (element != null)
            {
                XElement element1 = element.Element(Constant.XML_ELEMENT_VARIABLE);
                if (element1 != null)
                {
                    element1.Value = variable;
                    element.Save(uri);
                    return true;
                }
                else
                {
                    element1.Value = variable;
                    element.Save(uri);
                    return true;
                }
            }
            return false;
        }
    }
}
