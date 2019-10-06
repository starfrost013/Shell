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

            XmlDocument script = ShellCore.XmlOpenFile(path);

            XmlNode script_root = ShellCore.XmlGetRootNode(script, true, "ShellXML", 3);

            XmlNodeList ScriptTokens;

            bool result = ShellCore.XmlCheckForChildNodes(script, true, 4);

            if (result == true)
            {
                ScriptTokens = ShellCore.XmlGetChildren(script_root);
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
                        ShxmlException(token);
                        continue;
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
