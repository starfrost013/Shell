using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    partial class ShellCore 
    {

        public bool CreateFileEx(string path, string initialContent=null, bool verifyThatNotAlreadyExist = false) // Create File for 2.6 and up
        {
 
            if (verifyThatNotAlreadyExist == true)
            {
                if (!File.Exists(path))
                {
                    var fx = File.Create(path);
                    fx.Close();
                }
            }
            else
            {
                var fx = File.Create(path);
                fx.Close();
            }

            if (initialContent != null) // if we want to create the file
            {
                try
                {
                    var fx = File.OpenWrite(path);
                    var ft = File.AppendText(initialContent);
                    fx.Close(); 
                }
                catch (FileNotFoundException err)
                {
                    Console.WriteLine($"{err} - Somehow we couldn't find the file we just made, oh shit.");
                    return false; // we failed
                }
                catch (DirectoryNotFoundException err)
                {
                    Console.WriteLine($"{err} - Or, the directory wasn't found for creating.");
                    return false; // we failed
                }
                catch (IOException err)
                {
                    Console.WriteLine(err);
                    return false;
                }

            }
            // actually get rid of this shit
            return true;
            }
            
        public bool DeleteFileEx(string path, bool askUserConsoleOnly = false, string confirmationMessage="Are you sure you want to delete") // Delete File for 2.6+
        {
            if (askUserConsoleOnly == true)
            {
                bool loopAsk = true;
                while (loopAsk == true) // deleteFile
                {
                    Console.WriteLine($"{confirmationMessage} {path} [Y/N]?");
                    string result = Console.ReadLine();
                    switch (result)
                    {
                        case "n":
                            return false;
                        case "N":
                            return false;
                        case "y":
                        case "Y":
                            loopAsk = false;
                            break; // stop the looping!
   
                    }

                }

                
            }

            File.Delete(path); // actually delete the file!
            return true;
        }

        public bool CopyFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully copied to")
        {   try
            {
                
                File.Copy(oldpath, newpath);
                //fc.Close();
            }

            catch (FileNotFoundException err)
            {
                return false; // we failed
            }

            catch (DirectoryNotFoundException err)
            {
                return false; // we failed
            }

            catch (UnauthorizedAccessException err)
            {
                return false; // we failed
            }

            catch (IOException err)
            {

                Console.WriteLine($"{err} \n\n\n- Or, that file already exists.");
            }

            if (operationSuccessfulMessageConsoleOnly == false)
            {
                Console.WriteLine($"{operationSuccessfulMessage} {newpath} ");
            }

            return true;
        }

        public bool MoveFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully moved to")
        {
            try
            {
                File.Move(oldpath, newpath);
            }

            catch (FileNotFoundException err)
            {
                return false; // we failed
            }

            catch (DirectoryNotFoundException err)
            {
                return false; // we failed
            }

            catch (UnauthorizedAccessException err)
            {
                return false; // we failed
            }

            catch (IOException err)
            {
                
                Console.WriteLine($"{err} \n\n\n- Or, that file already exists.");
            }

            if (operationSuccessfulMessageConsoleOnly == false)
            {
                Console.WriteLine($"{operationSuccessfulMessage} {newpath} ");
            }

            return true;
        }
        public bool VerifyExistence(string path) // Checks if a file exists
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
