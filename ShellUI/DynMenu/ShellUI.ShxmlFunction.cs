using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shell.UI
{
    partial class ShellXML
    {
        string path;
        public string ShxmlRunFunction(XmlNode node)
        {
            XmlAttributeCollection attributes = node.Attributes;

            foreach (XmlAttribute attribute in attributes)
            {
                switch (attribute.Name)
                {
                    case "path":
                        path = attribute.Value;
                        return path;
                }
            }

            if (path == null)
            {
                ShellCore.ElmThrowException(26);
                return Convert.ToString("27"); // this code never runs so we don't have a problem!!!! yay!!!!
            }

            return "";

        }
    }
}
