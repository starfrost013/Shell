using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Shell.UI
{
    partial class ShellXML
    {
        public List<ShxmlVariable> Varlist;

        public int InitShellXML()
        {
            Varlist = new List<ShxmlVariable>();
            return 0;
        }

        public ShxmlVariable ShxmlDeclareVariable(XmlNode node) // Automatically determines the type. Internal?
        {
            ShxmlVariable xmlvar = new ShxmlVariable();
            XmlAttributeCollection XmlAttrs = node.Attributes;

            string var_name = null;
            string var_type = null;
            string var_value = null;

            foreach (XmlAttribute Attribute in XmlAttrs)
            {

                if (Attribute.Name == "name")
                {
                    var_name = Attribute.Value; 
                }

                // get it!
                if (Attribute.Name == "type")
                {
                    var_type = Attribute.Value;
                }

                if (Attribute.Name == "value")
                {
                    var_value = Attribute.Value;
                }
                
            }


            foreach (ShxmlVariable var in Varlist)
            {
                //Console.WriteLine(var.Name);
                if (var.Name == var_name)
                {
                    //xmlvar = null;
                    ShellCore.ElmThrowException(11);
                    return xmlvar; // silently return if duplicate
                }
            }

            xmlvar.Name = var_name;

            //todo: sometimes for some reason (doubles) it doesn't set the variable
            try
            {
                switch (var_type)
                {
                    case null:
                        xmlvar.Type = -1;

                        try
                        {
                            xmlvar.Type = 3;
                            xmlvar.vardouble = Convert.ToDouble(var_value); // this deliberately triggers an exception 
                        }

                        catch (FormatException) // use an exception handler to set the type
                        {
                            xmlvar.Type = 1; // ITS A STRING
                            xmlvar.varstring = var_value;
                        }

                        if (xmlvar.Type == -1)
                        {
                            ShellCore.ElmThrowException(5);
                            return xmlvar;
                        }

                        if (var_value == "true" || var_value == "false")
                        {
                            xmlvar.Type = 5;
                            xmlvar.varbool = Convert.ToBoolean(var_value);
                        }

                        Varlist.Add(xmlvar);
                        return xmlvar;

                    case "int":
                    case "Int":
                        xmlvar.Type = 0;
                        xmlvar.varint = Convert.ToInt32(var_value);
                        //Console.WriteLine(var_value);
                        Varlist.Add(xmlvar);
                        return xmlvar;
                    case "str":
                    case "Str":
                    case "string":
                    case "String":
                        //xmlvar.Name = var_name;
                        xmlvar.Type = 1;
                        xmlvar.varstring = var_value;
                        Varlist.Add(xmlvar);
                        return xmlvar;
                    case "letter":
                    case "char":
                    case "character":
                    case "Char":
                    case "Character":
                    case "Letter":
                        //xmlvar.Name = var_name;
                        xmlvar.Type = 2;
                        xmlvar.varchar = Convert.ToChar(var_value);
                        Varlist.Add(xmlvar);
                        return xmlvar;
                    case "double":
                    case "Double":
                    case "number":
                    case "Number":
                        xmlvar.Type = 3;
                        xmlvar.vardouble = Convert.ToDouble(var_value);
                        Varlist.Add(xmlvar);
                        return xmlvar;
                    case "float":
                    case "Float":
                        xmlvar.Type = 4;
                        xmlvar.varfloat = Convert.ToSingle(var_value);
                        Varlist.Add(xmlvar);
                        return xmlvar;
                    case "bool":
                    case "Bool":
                    case "BOOL":
                    case "Boolean":
                    case "boolean":
                    case "truefalse":
                    case "true/false":
                    case "true false":
                    case "falsetrue":
                    case "false/true":
                    case "false or true":
                    case "true or false":
                        xmlvar.Type = 5;
                        xmlvar.varbool = Convert.ToBoolean(var_value);
                        Varlist.Add(xmlvar);
                        return xmlvar;
                    // floats not supported
                    default:
                        ShellCore.ElmThrowException(5);
                        return xmlvar;
                        
                }

                
            }
            catch (FormatException)
            {
                ShellCore.ElmThrowException(7);
                return xmlvar;
            }
            //return xmlvar;
            
        }


    }

    public struct ShxmlVariable
    {
        public string Name { get; set; }

        public int Type { get; set; }

        public int varint { get; set; } // this could be done better (delegates?)

        public string varstring { get; set; }

        public float varfloat { get; set; }

        public double vardouble { get; set; }

        public char varchar { get; set; } 

        public bool varbool { get; set; }

    }

}
