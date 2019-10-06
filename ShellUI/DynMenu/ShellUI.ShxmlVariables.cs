using Shell.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Shell.UI
{
    partial class ShellXML 
    {
        public List<ShxmlVariable> Varlist;

        public ShellXML()
        {
            this.Varlist = new List<ShxmlVariable>();
            InitSuperglobals(); // init the superglobals
        }

        public void InitSuperglobals()
        {
            FileVersionInfo CoreVersion = ShellCore.GetVersion();
            FileVersionInfo Version = GetVersion();
            ShxmlInitVariable("ShellUIVer",$"{Version.FileMajorPart}.{Version.FileMinorPart}.{Version.FileBuildPart}.{Version.FilePrivatePart}");
            ShxmlInitVariable("ShellCoreVer", $"{CoreVersion.FileMajorPart}.{CoreVersion.FileMinorPart}.{CoreVersion.FileBuildPart}.{CoreVersion.FilePrivatePart}");
        }

        //TODO: Clean up this function. It blows ass and sucks dick right now.
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
                    //var = null;
                    ShellCore.ElmThrowException(11);
                    return xmlvar; // silently return if duplicate
                }
            }

            xmlvar.Name = var_name;

            //todo: sometimes for some reason (doubles) it doesn't set the variable
            // split this? -- for function parameters maybe??
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
                        ShxmlInitVariable(var_name, Convert.ToInt32(var_value));
                        return xmlvar;
                    case "str":
                    case "Str":
                    case "string":
                    case "String":
                        //xmlvar.Name = var_name;
                        ShxmlInitVariable(var_name, var_value);
                        return xmlvar;
                    case "letter":
                    case "char":
                    case "character":
                    case "Char":
                    case "Character":
                    case "Letter":
                        ShxmlInitVariable(var_name, Convert.ToChar(var_value));
                        return xmlvar;
                    case "double":
                    case "Double":
                    case "number":
                    case "Number":
                        ShxmlInitVariable(var_name, Convert.ToDouble(var_value));
                        return xmlvar;
                    case "float":
                    case "Float":
                        ShxmlInitVariable(var_name, Convert.ToSingle(var_value));
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
                        ShxmlInitVariable(var_name, Convert.ToBoolean(var_value));
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

        // Init variable overloads
        public ShxmlVariable ShxmlInitVariable(string Name, int value) // For ints
        {
            ShxmlVariable var = new ShxmlVariable();
            var.Name = Name;
            var.Type = 0;
            var.varint = value;

            Varlist.Add(var);
            return var;
        }

        public ShxmlVariable ShxmlInitVariable(string Name, string value) // For strings
        {
            ShxmlVariable var = new ShxmlVariable();
            var.Name = Name;
            var.Type = 1;
            var.varstring = value;

            Varlist.Add(var);
            return var;
        }

        public ShxmlVariable ShxmlInitVariable(string Name, char value) // For chars
        {
            ShxmlVariable var = new ShxmlVariable();
            var.Name = Name;
            var.Type = 2;
            var.varchar = value;

            Varlist.Add(var);
            return var;
        }

        public ShxmlVariable ShxmlInitVariable(string Name, double value) // For doubles
        {
            ShxmlVariable var = new ShxmlVariable();
            var.Name = Name;
            var.Type = 3;
            var.vardouble = value;

            Varlist.Add(var);
            return var;
        }

        public ShxmlVariable ShxmlInitVariable(string Name, float value) // For floats
        {
            ShxmlVariable var = new ShxmlVariable();
            var.Name = Name;
            var.Type = 4;
            var.varfloat = value;

            Varlist.Add(var);
            return var;
        }

        public ShxmlVariable ShxmlInitVariable(string Name, bool value) // For bools
        {
            ShxmlVariable var = new ShxmlVariable();
            var.Name = Name;
            var.Type = 5;
            var.varbool = value;

            Varlist.Add(var);
            return var;
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
