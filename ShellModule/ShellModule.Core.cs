using Shell.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xaml;


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

        public FileVersionInfo GetVersion() // Gets the version.
        {
            Assembly ShlModule = Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(ShlModule.Location);
            return fileVersionInfo;
        }

        public void PrintVersion() // Prints the version. ShellCore 8.0.95+ / ShellModule 1.0.41+
        {
            FileVersionInfo version = GetVersion();
            Console.WriteLine(version.FileVersion);

        }

        public void InstallModule(bool quiet, string uri)
        {
            if (quiet == true)
            {
                Module module = GetModule(uri, "Module.zip");
            }
            else
            {
                Module module = GetModule(uri, "Module.zip");
                ModuleInstaller moduleInstaller = new ModuleInstaller(ShellCore);
                moduleInstaller.Width = 810; // for some reason it fucks up
                moduleInstaller.ShowDialog();

                if (ShellCore.ModuleInstallationAllowed == true)
                {
                    InstallModule(module);
                }
                else
                {
                    // clean up and return
                    ShellCore.DeleteFileEx(module.Dll);
                    ShellCore.DeleteFileEx("Modules/Module.xml");
                    ShellCore.DeleteFileEx("Module.zip");
                    return;

                }
            }
        }


        public Module GetModule(string uri, string path)
        {
            Console.WriteLine($"Installing module from the Internet at {uri}..."); // displays a message
            ShellCore.DownloadFileEx(uri, path);
            ShellCore.ExtractZipFile("Module.zip", "Modules");
            XmlDocument ModuleXml = ShellCore.XmlOpenFile("Modules/Module.xml");
            Console.WriteLine($"Reading Module XML file...");
            XmlNode moduleRoot = ModuleXml.FirstChild;
            string Name = moduleRoot.Name;

            if (Name == "#comment")
            {
                while (Name == "#comment")
                {
                    moduleRoot = moduleRoot.NextSibling; // get the next node. 
                    Name = moduleRoot.Name; // get the next node until we have an actual node.
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
                        string trimmedExtends = attribute.InnerText.Trim();
                        switch (trimmedExtends) 
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
                                return Module;
                        }
                }
            }
            return Module;


        }

        public void InstallModule(Module module)
        {
            Console.WriteLine("Adding to Module List");
            XmlDocument ModuleXml = ShellCore.XmlOpenFile("Modules.xml");
            XmlNode moduleRoot = ShellCore.XmlGetRootNode(ModuleXml, true, "Modules", 58);
            ModuleXml = ShellCore.XmlAddNode(ModuleXml, moduleRoot, "Module");
            XmlNode moduleModule = ShellCore.XmlGetNode(moduleRoot, "Module");
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Name", module.Name.Trim());
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Author", module.Author.Trim());
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Version", module.Version.Trim());
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Copyright", module.Copyright.Trim());
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Website", module.Website.Trim());
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Dll", module.Dll.Trim());
            ModuleXml = ShellCore.XmlAddAttribute(ModuleXml, moduleModule, "Extends", module.Extends.ToString());
            ModuleXml = ShellCore.XmlSaveFile(ModuleXml, "Modules.xml");
            ShellCore.DeleteFileEx("Modules/Module.xml");
            ShellCore.DeleteFileEx("Module.zip");
        }
    }
}
