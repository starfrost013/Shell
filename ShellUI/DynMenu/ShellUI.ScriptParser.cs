using Shell;
using Shell.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace Shell.UI
{
    partial class ShellUI : IShell
    {
        public ShellXML ShXML { get; set; }
    }

    public partial class ShellXML : ShellUI
    {

        public int XmlParseScript(string path, int isFunction = 0, int isStatement = 0)
        {

            if (isFunction != 0) // call it 
            {
                Console.WriteLine($"Parsing ShellXML function at {path}...\n");
            }
            else
            {
                Console.WriteLine($"Parsing ShellXML script at {path}...\n");
            }

            XmlDocument script = new XmlDocument();

            try
            {
                script.Load(path);
            }

            catch (FileNotFoundException)
            {
                ShellCore.ElmThrowException(13);
            }

            catch (DirectoryNotFoundException)
            {
                ShellCore.ElmThrowException(13);
            }

            catch (XmlException)
            {
                ShellCore.ElmThrowException(10);
            }


            XmlNode script_root = script.FirstChild;

            if (script_root.Name != "ShellXML")
            {
                ShellCore.ElmThrowException(3); // throw an exception if the shellxml node isn't there
            }

            XmlNodeList ScriptTokens;

            if (!script_root.HasChildNodes)
            {
                ShellCore.ElmThrowException(4); // nothing
                return 5;

            }
            else
            {
                ScriptTokens = script_root.ChildNodes;
                XmlParseScriptNodes(ScriptTokens);
            }

            return 0;
        }

        public void XmlParseScriptNodes(XmlNodeList children)
        {
            foreach (XmlNode token in children)
            {
                switch (token.Name)
                {
                    default:
                        ShellCore.ElmThrowException(0);
                        return;
                    case "#document":
                        continue;
                    case "#comment":
                        continue;
                    case "shellxml":
                    case "Shellxml":
                    case "shellXML":
                    case "ShellXML":
                        ShellCore.ElmThrowException(3);
                        return;
                    case "var":
                    case "Var":
                        ShxmlDeclareVariable(token);
                        continue;
                    case "operation":
                    case "Operation":
                    case "op":
                    case "Op":
                        ShxmlPerformOperation(token);
                        continue;
                    case "input":
                    case "Input":
                        ShxmlConsoleInput(token);
                        continue;
                    case "output":
                    case "Output":
                        ShxmlConsoleOutput(token);
                        continue;
                    case "function":
                    case "Function":
                        string fpath = ShxmlRunFunction(token);
                        XmlParseScript(fpath, 1);
                        continue;
                    case "exception":
                    case "Exception":
                    case "if":
                    case "If":
                        ShxmlHandleIfStatement(token);
                        continue;
                        /* Coming Soon
                        case "Else":*/

                }
            }
        }
    }


}
