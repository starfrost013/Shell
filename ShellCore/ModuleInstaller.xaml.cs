using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shell.Core
{
    /// <summary>
    /// Interaction logic for ModuleInstallre.xaml
    /// </summary>
    public partial class ModuleInstaller : Window
    {

        public ShellCore ShlCore;

        public ModuleInstaller(ShellCore ShellCore, string moduleName, string moduleAuthor, string moduleVersion, string moduleCopyright)
        {
            ShlCore = ShellCore;
            InitializeComponent();
            Label_Info_Name.Text = $"{Label_Info_Name.Text} {moduleName}"; // set module name
            Label_Info_Author.Text = $"{Label_Info_Author.Text} {moduleAuthor}"; // set module author
            Label_Info_Description.Text = $"{Label_Info_Description.Text} {moduleVersion}"; // set module description/version
            Label_Info_Copyright.Text = $"{Label_Info_Copyright.Text} {moduleVersion}"; // set module copyright.
            // update the labels
            
        }

        private void Button_Install_Click(object sender, RoutedEventArgs e)
        {
            ShlCore.ModuleInstallationAllowed = true;
            this.Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ShlCore.ModuleInstallationAllowed = false;
            this.Close();
        }
    }
}
