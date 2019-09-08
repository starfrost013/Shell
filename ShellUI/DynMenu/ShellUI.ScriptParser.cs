using Shell;
using Shell.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace Shell.UI
{
    partial class ShellUI : IShell
    {

        public int XmlParseScript(string path)
        {
            ShellXML = new ShellXML();
            ShellXML.Varlist = new List<ShxmlVariable>();
            Console.WriteLine("Parsing ShellXML script...\n\n");
            Console.WriteLine("WARNING: This is heavily under construction and will almost certainly not work.");
            XmlDocument script = new XmlDocument();
            try
            {
                script.Load(path);
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

            } else
            {
                ScriptTokens = script_root.ChildNodes;
            }
            

            //int rootElementCount = 0;

            foreach (XmlNode token in ScriptTokens)
            {
                //Console.WriteLine($"DEBUG: [token.] {token.Name}");
                //foreach (Keyword keyword in ShellXML.KeywordList) // HOPEFULLY this works!
                //{
                    // probably crappy and slow code
                    switch (token.Name)
                        {
                        default:
                           ShellCore.ElmThrowException(0);
                           return 2;
                        case "#document":
                           continue;
                        case "#comment":
                           continue;
                        case "ShellXML":
                            ShellCore.ElmThrowException(3);
                            return 4;
                        case "Var":
                            ShellXML.ShxmlDeclareVariable(token);
                            continue;
                        //case "Op":
                           //ShellXML.
                        }
                //}
            }
            return 0;
        }
    }

    public partial class ShellXML : ShellUI
    {
        public List<Keyword> KeywordList { get; set; }

        public ShellXML()
        {
            // constructor for 4.3/0.3+
            // keywords

            KeywordList = new List<Keyword>();

            string[] attributes_Doc = { };
            string[] attributes_ShellXML = { };
            string[] attributes_App = { "author", "description", "notes", "shellminimum", "version"};
            string[] attributes_Op = { "op", "var" }; // op attributes
            string[] attributes_Var = { "name", "value", "type" };
            string[] attributes_If = { "condition " };
            string[] attributes_Else = { };
            string[] attributes_While = { "condition", "infinite" };
            string[] attributes_For = { "start", "end", "step" };
            string[] attributes_Event = { "event" }; // will have special handling
            string[] attributes_Text = { "text, button, posx, posy" };
            string[] attributes_Button = { };
            string[] attributes_OpenDialog = { "var" };
            string[] attributes_SaveDialog = { "var" };
            string[] attributes_Exit = { "exitcode" };


            //string[] attributes_
            Keyword _ = UiScAddKeyword("#document", attributes_Doc); // test

            if (_.keywordName == null) // error checking
            {
                Console.WriteLine("");
            }

            UiScAddKeyword("ShellXML", attributes_ShellXML);
            UiScAddKeyword("App", attributes_App);
            UiScAddKeyword("Op",attributes_Op);
            UiScAddKeyword("Var", attributes_Var);
            UiScAddKeyword("If", attributes_If);
            UiScAddKeyword("Else", attributes_Else);
            UiScAddKeyword("While", attributes_While);
            UiScAddKeyword("For", attributes_For);
            UiScAddKeyword("Event", attributes_Event);
            UiScAddKeyword("Text", attributes_Text);
            UiScAddKeyword("Button", attributes_Button);
            UiScAddKeyword("OpenDialog", attributes_OpenDialog);
            UiScAddKeyword("SaveDialog", attributes_SaveDialog);
            UiScAddKeyword("Exit", attributes_Exit);

        }

        public Keyword UiScAddKeyword(string keywordName, string[] attributes) // add a keyword
        {
            Keyword _ = new Keyword();

            if (KeywordList == null)
            {
                return _; 
            }

            _.keywordName = keywordName;
            _.Attributes = attributes;
            KeywordList.Add(_);
            return _;
        }


    }

    public struct Keyword
    {
        public string keywordName { get; set; }

        public string[] Attributes { get; set; }

    }

}
