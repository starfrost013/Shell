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

        public ModuleInstaller(ShellCore ShellCore)
        {
            ShlCore = ShellCore;
            InitializeComponent();
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
