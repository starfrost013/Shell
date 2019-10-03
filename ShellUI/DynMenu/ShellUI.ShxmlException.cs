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
        public void ShxmlException(XmlNode token)
        {
            XmlAttributeCollection attributes = token.Attributes;
            int? id = null;

            foreach (XmlAttribute attribute in attributes)
            {
                switch (attribute.Name)
                {
                    case "ID":
                    case "Id":
                    case "id":
                        try
                        {
                            Convert.ToInt32(attribute.Value);
                        }
                        catch (FormatException)
                        {
                            ShellCore.ElmThrowException(52);
                        }
                        continue;
                }
            }

            if (id == null)
            {
                ShellCore.ElmThrowException(53);
            }
        }
    }
}
