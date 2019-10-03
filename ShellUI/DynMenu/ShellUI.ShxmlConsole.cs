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
        public void ShxmlConsoleOutput(XmlNode token) // pass
        {
            string text = "";
            StringBuilder buildit = new StringBuilder(); // the final string.
            string[] text_s = null;
            int posx = -0xd15ea5e;
            int posy = -0xd15ea5e;

            XmlAttributeCollection attributes = token.Attributes;

            foreach (XmlAttribute attribute in attributes)
            {
                // check for what we want
                switch (attribute.Name)
                { 
                    case "text":
                    
                        text = attribute.Value;
                        int count = 0;

                        text_s = text.Split('+');
                        foreach (string text_t in text_s)
                        {
                            foreach (ShxmlVariable xvar in Varlist) 
                                { 
                                    if (text_t == xvar.Name)
                                    {
                                        if (text_s.Count() - count > 0) {
                                        switch (xvar.Type)
                                        {
                                            // convert to string and add
                                            case 0:
                                                text_s[count] = Convert.ToString(xvar.varint);
                                                continue;
                                            case 1:
                                                text_s[count] = xvar.varstring;
                                                continue;
                                            case 2:
                                                text_s[count] = Convert.ToString(xvar.varchar);
                                                continue;
                                            case 3:
                                                text_s[count] = Convert.ToString(xvar.vardouble);
                                                continue;
                                            case 4:
                                                text_s[count] = Convert.ToString(xvar.varfloat);
                                                continue;
                                            case 5:
                                                text_s[count] = Convert.ToString(xvar.varbool);
                                                continue;
                                        }
                                    }
                                    
                                }
                                
                            }
                            count++;
                        }
                        
                        foreach (string text_t in text_s)
                        {
                            buildit.Append(text_t);
                        }
                    
                        continue;
                    case "posx":
                        try
                        {
                            if (posx == -0xd15ea5e)
                            {
                                posx = Convert.ToInt32(attribute.Value); // x position (in char cells from 0)
                            }
                        }

                        catch (FormatException)
                        {
                            ShellCore.ElmThrowException(16);
                        }

                        if (posx == -0xd15ea5e)
                        {
                            ShellCore.ElmThrowException(18);
                        }

                        continue;
                    case "posy":
                        try
                        {
                            if (posy == -0xd15ea5e)
                            {
                                posy = Convert.ToInt32(attribute.Value); // y position (in char cells from 0)
                            }
                        }

                        catch (FormatException)
                        {
                            ShellCore.ElmThrowException(17);
                        }

                        if (posy == -0xd15ea5e)
                        {
                            ShellCore.ElmThrowException(18);
                        }


                        continue;
                    default:
                        ShellCore.ElmThrowException(19);
                        continue;
                        

                }

            }

            if (posx != -0xd15ea5e & posy != -0xd15ea5e)
            {
                UiSetCursorPosition(posx, posy); // yeah
            }

            //todo: + to separate variables in <Input>??

            // Should've used delegates here. Oh well.

            Console.WriteLine(buildit); // since we're not done...



            return;
        }

        public void ShxmlConsoleInput(XmlNode Token)
        {
            string text = "";
            int posx = -0xd15ea5e;
            int posy = -0xd15ea5e;
            char chr = ' ';
            ShxmlVariable svar = new ShxmlVariable(); //TODO: Clean up the code.
            svar.Name = "ddddddddddddddddddddddddddddddddddddddddddddddddddd";
            XmlAttributeCollection attributes = Token.Attributes;

            foreach (XmlAttribute attribute in attributes)
            {
                switch (attribute.Name)
                {
                    case "text":
                        text = attribute.Value;
                        continue;
                    case "posx":
                        try
                        {
                            posx = Convert.ToInt32(attribute.Value);
                        }

                        catch (FormatException)
                        {
                            ShellCore.ElmThrowException(16);
                        }

                        if (posx == -0xd15ea5e)
                        {
                            ShellCore.ElmThrowException(18);
                        }
                        continue;
                    case "posy":
                        try
                        {
                            posy = Convert.ToInt32(attribute.Value);
                        }

                        catch (FormatException)
                        {
                            ShellCore.ElmThrowException(17);
                        }

                        if (posx == -0xd15ea5e)
                        {
                            ShellCore.ElmThrowException(18);
                        }

                        continue;
                    case "char":
                        try
                        {
                            chr = Convert.ToChar(attribute.Value);
                        }
                        catch (FormatException)
                        {
                            ShellCore.ElmThrowException(20);
                        }
                        
                        continue;
                    case "variable":
                    case "var":
        
                        svar.Name = attribute.Value;

                        if (svar.Name == "")
                        {
                            ShellCore.ElmThrowException(21);
                        }

                        svar.Type = 1; // string
                        

                        continue;
                    

                }

            }

            if (svar.Name == "ddddddddddddddddddddddddddddddddddddddddddddddddddd")
            {
                ShellCore.ElmThrowException(22); // exception 22 - no variable
            }
            
            if (posx != -0xd15ea5e & posy != 0xd15ea5e)
            {
                UiSetCursorPosition(posx, posy); // set the cursor position if it is not a default value
            }

            Console.Write($"{text} {chr}");
            svar.varstring = Console.ReadLine();
            Varlist.Add(svar); // add it to the global master variable listash

        }

    }
}
