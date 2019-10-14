//using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Windows Forms-Shell Glue for ShellCore v6+
// © 2019 Cosmo

namespace Shell.Core
{
    partial class ShellCore
    {
        public Color WinColourDialog() // Wincolour Dialog
        {
            ColorDialog ColourDialog = new ColorDialog(); // create a dialog 
            ColourDialog.ShowDialog(); // show the dialog
            Color Colour = ColourDialog.Color; // get a color 
            return Colour; // return the color
        }

        public void WinMsgBox(string caption = null, string text = null, MessageBoxButtons msgButtons = MessageBoxButtons.OK, MessageBoxIcon msgIcon = MessageBoxIcon.None) // Shows a (basic) winforms message box.
        {
            if (text == null)
            {
                ElmThrowException(27); //can't not provide it obviously
            }

            else if (caption == null)
            {
                ElmThrowException(28);
            }

            //switch statement maybe?
            MessageBox.Show(text, caption, msgButtons, msgIcon);

            return;
        }
        
        public string WinOpenDialog(string defaultFileName, string defaultExtension, string filter, string title = null, bool checkPathExists = false, bool allowMultipleFiles = false)
        {
            
            OpenFileDialog OpenDlg = new OpenFileDialog();
            OpenDlg.FileName = defaultFileName;
            OpenDlg.DefaultExt = defaultExtension;
            OpenDlg.Filter = filter;

            if (title != null)
            {
                //set the title if we provided one
                OpenDlg.Title = title;
            }

            if (checkPathExists != false)
            {
                OpenDlg.CheckFileExists = checkPathExists;
            }

            Nullable<bool> result = Convert.ToBoolean(OpenDlg.ShowDialog());

            if (result == true) // yes we have a filename!!!oneoneoneone
            {
                string fname = OpenDlg.FileName;
                return fname;
            }
            else
            {
                return null;
            }

            // get the result from the user
        }

        public string WinSaveDialog(string defaultFileName, string defaultExtension, string filter, string title = null, bool checkPathExists = false, bool allowMultipleFiles = false)
        {

            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.FileName = defaultFileName;
            SaveDlg.DefaultExt = defaultExtension;
            SaveDlg.Filter = filter;

            if (title != null)
            {
                //set the title if we provided one
                SaveDlg.Title = title;
            }

            if (checkPathExists != false)
            {
                SaveDlg.CheckFileExists = checkPathExists;
            }

            Nullable<bool> result = Convert.ToBoolean(SaveDlg.ShowDialog());

            if (result == true) // yes we have a filename!!!oneoneoneone
            {
                string fname = SaveDlg.FileName;
                return fname;
            }
            else
            {
                return null;
            }

            // get the result from the user
        }

    }

}
