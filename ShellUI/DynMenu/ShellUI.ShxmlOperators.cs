// ShellSystem Operation/Statement code. this is SHITTY.

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
        // Operation code

        public List<string> OperandList;

        public List<char> OpList;

        string[] cleancut;

        public ShxmlVariable ShxmlEvaluateStatement(XmlNode token, int mode = 0)
        {

            int count = 0;
            double result = 0;

            XmlAttributeCollection attributes = token.Attributes;
            ShxmlVariable svar = new ShxmlVariable();
            string x = null;
            OperandList = new List<string>();
            OpList = new List<char>();
            foreach (XmlAttribute attribute in attributes)
            {
                switch (attribute.Name)
                {
                    case "op":
                    case "Op":
                    case "operation":
                    case "Operation":
                        x = attribute.Value;
                        char[] thebits = attribute.Value.ToCharArray();

                        // This works because the first operand is after the first part of the string. Thus we get the correct operands.
                        foreach (char bit in thebits)
                        {
                            switch (bit)
                            {
                                case '+':
                                case '-':
                                case '*':
                                case '/':
                                case '%':
                                    OpList.Add(bit);
                                    continue;
                            }
                            count++;
                        }
                        continue;
                    case "var":
                    case "Var":
                    case "variable":
                    case "Variable":
                        svar.Name = attribute.Value;
                        svar.Type = 3; // double by default
                        continue;
                    default:
                        ShellCore.ElmThrowException(19);
                        return svar;


                }
            }

            if (svar.Name == null)
            {
                ShellCore.ElmThrowException(40);
            }

            if (OpList.Count == 0)
            {
                ShellCore.ElmThrowException(41);
            }
                    char[] OperatorListA = OpList.ToArray(); // convert to array
                    cleancut = x.Split(OperatorListA);

                    count = 0; // reset the count

                    foreach (string bit2 in cleancut)
                    {
                        foreach (ShxmlVariable shvar in Varlist)
                        {
                            if (cleancut[count] == shvar.Name)
                            {
                                if (shvar.Type == 1)
                                {
                                    ShellCore.ElmThrowException(14); // Can't do stuff to strings...yet. 
                                }

                                else if (shvar.Type == 5)
                                {
                                    ShellCore.ElmThrowException(15); // Can't add booleans.
                                }

                                else
                                {
                                    OperandList.Add(Convert.ToString(shvar.vardouble)); // convert it to string
                                    count++;
                                    continue;
                                }
                            }

                        }
                        OperandList.Add(bit2);
                        count++;
                    }

            



                List<ShxmlVariable> vars = new List<ShxmlVariable>();
                List<double> bit3 = new List<double>();
                List<double> bit3c = new List<double>();

                count = 0; //set count back to 0
                foreach (string bit2 in cleancut)
                {
                    string bit2a = bit2.Trim();

                    int isVar = 0;
                    foreach (ShxmlVariable sxvar in Varlist)
                    {
                        if (sxvar.Name == bit2a & bit2a != svar.Name)
                        {
                            isVar = 1;
                            vars.Add(sxvar);
                            bit3.Add(sxvar.vardouble);
                        }

                    }
                    if (isVar == 0)
                    {
                        try
                        {
                            if (bit2a != svar.Name)
                            {
                                double bit2b = Convert.ToDouble(bit2a);
                                bit3.Add(bit2b);
                            }
                        }
                        catch (FormatException)
                        {
                            // error 12 removed from here due to changes in how this works in Shell Development Release
                        }
                    }
                    isVar = 0;

                }

                // so it's easier
                double[] bit3b = bit3.ToArray();

                ShxmlVariable[] varsb = vars.ToArray(); // local list converted to an array.

                // jesus fucking christ this code is getting worse!

                foreach (ShxmlVariable varsb2 in varsb)
                {
                    bit3c.Add(varsb2.vardouble);
                }

                // this actually parses the stuff
                foreach (double bit3a in bit3b)
                {

                    // This is awaiting changes to adding an overload in ShellCore to perform operations on a single int instead of a collection.
                    switch (OpList[count])
                    {
                        case '+':
                            result = bit3a + bit3b[count + 1];
                            continue;
                        case '-':
                            result = bit3a - bit3b[count + 1];
                            continue;
                        case '*':
                            result = bit3a * bit3b[count + 1];
                            continue;
                        case '/':
                            result = bit3a / bit3b[count + 1];
                            continue;
                        case '%':
                            result = bit3a % bit3b[count + 1];
                            continue;
                    }
                    count++;
                }

                count = 0;

                    foreach (ShxmlVariable ShxVar in varsb)
                    {
                        if (OpList.Count - count > 1)
                        {
                            switch (OpList[count])
                            {
                                case '+':
                                    result = result + bit3c[count];
                                    count++;
                                    continue;
                                case '-':
                                    result = result - bit3c[count];
                                    count++;
                                    continue;
                                case '*':
                                    result = result * bit3c[count];
                                    count++;
                                    continue;
                                case '/':
                                    result = result / bit3c[count];
                                    count++;
                                    continue;
                                case '%':
                                    result = result % bit3c[count];
                                    count++;
                                    continue;
                            }
                            count++;
                        }
                    }

                count = 0;
                //update the main list
              
            
            svar.vardouble = result;
            return svar;
        }

        public ShxmlVariable ShxmlPerformOperation(XmlNode token, int mode = 0)
        {
            ShxmlVariable svar = ShxmlEvaluateStatement(token);
            Varlist.Add(svar);
            return svar;
        }
    }
}
