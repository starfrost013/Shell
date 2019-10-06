using Shell.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shell.Module
{
    public partial class ShellModule : IShell // pass shellcore  to an init function
    {
        public ShellCore ShellCore { get; set; }
        public string ModxmlPath { get; set; }

        public void InitShellModule(ShellCore ShlCore)
        {
            ShellCore = ShlCore;
        }

        public void InstallModule(bool quiet, string uri)
        {
            if (quiet == true)
            {
                DownloadModule(uri, "Module.zip");
            }
        }
        public void DownloadModule(string uri, string path)
        {
            ShellCore.DownloadFileEx(uri, path);
            ShellCore.ExtractZipFile("Module.zip", "Modules");
            GetModule();
            return;
        }

        public void GetModule()
        {

            XmlDocument ModuleXml = ShellCore.XmlOpenFile("Modules/Module.xml");
            XmlNode moduleRoot = ModuleXml.FirstChild;
            string Name = moduleRoot.Name;

            if (Name == "#comment")
            {
                while (Name == "#comment")
                {
                    Name = moduleRoot.NextSibling.Name; // get the next node until we have an actual node.
                }
            }

            if (Name != "Module")
            {
                ShellCore.ElmThrowException(55);
            }

            Module Module = new Module();

            foreach (XmlNode attribute in moduleRoot.ChildNodes)
            {
                switch (attribute.Name)
                {
                    case "Name": // module name
                        Module.Name = attribute.InnerText;
                        continue;
                    case "Author": // module author
                        Module.Author = attribute.InnerText;
                        continue;
                    case "Version": // module version
                        Module.Version = attribute.InnerText;
                        continue;
                    case "Copyright": // module copyright
                        Module.Copyright = attribute.InnerText;
                        continue;
                    case "Website": // module website
                        Module.Website = attribute.InnerText;
                        continue;
                    case "Dll": // module dll
                        Module.Dll = attribute.InnerText;
                        continue;
                    case "Extends": // which dll does this module extend
                        switch (attribute.Value) 
                        {
                            case "shlcore":
                            case "Shlcore":
                            case "shlCore":
                            case "ShlCore":
                            case "shellcore":
                            case "Shellcore":
                            case "shellCore":
                            case "ShellCore":
                                Module.Extends = Extends.ShellCore;
                                continue;
                            case "shlui":
                            case "Shlui":
                            case "shlUi":
                            case "ShlUi":
                            case "ShlUI":
                            case "shellui":
                            case "ShellUi":
                            case "shellUI":
                            case "ShellUI":
                            case "shellUi":
                            case "Shellui":
                                Module.Extends = Extends.ShellUI;
                                continue;
                            default:
                                ShellCore.ElmThrowException(57);
                                return;
                        }
                        continue;
                        


                }
            }

            try
            {
                ModuleXml.Load("Modules.xml"); // shellmodule ss
            }
            catch (XmlException)
            {
                // error loading xml
                ShellCore.ElmThrowException(58); // hmm
            }

            moduleRoot = ModuleXml.FirstChild;
            Name = moduleRoot.Name;

            if (Name == "#comment")
            {
                while (Name == "#comment")
                {
                    Name = moduleRoot.NextSibling.Name; // get the next node until we have an actual node.
                }
            }

            if (Name != "Modules")
            {
                ShellCore.ElmThrowException(58);
            }
            ShellCore.DeleteFileEx("Modules/Module.xml");

        }
    }
}
