using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    partial class ShellCore 
    {
        public enum FileIOArgs { VerifyExistence=0, AskUserConsoleOnly, OperationSuccessfulMessage}

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
                catch (FileNotFoundException)
                {
                    ElmThrowException(31);
                    return false; // we failed
                }
                catch (DirectoryNotFoundException)
                {
                    ElmThrowException(32);
                    return false; // we failed
                }
                catch (IOException)
                {
                    ElmThrowException(29);
                    return false;
                }

            }
            // actually get rid of this shit
            return true;
            }
            
        public bool DeleteFileEx(string path, bool askUserConsoleOnly = false, string confirmationMessage="Are you sure you want to delete") // Delete File for 2.6+
        {
            try
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
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return false; // we failed
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return false; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return false;
            }

            catch (IOException)
            {
                ElmThrowException(29);
                return false;
            }
            File.Delete(path); // actually delete the file!
            return true;
        }

        public bool CopyFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully copied to")
        {   try
            {
                File.Copy(oldpath, newpath);
            }

            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return false;
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return false; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return false;
            }

            catch (IOException)
            {
                ElmThrowException(29);
                return false;
            }

            if (operationSuccessfulMessageConsoleOnly == true)
            {
                Console.WriteLine($"{operationSuccessfulMessage} {newpath}.");
            }

            return true;
        }

        public bool MoveFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully moved to")
        {
            try
            {
                File.Move(oldpath, newpath);
            }

            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return false; // we failed
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return false; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return false;
            }

            catch (IOException)
            { 
                ElmThrowException(29);
                return false;
            }

            if (operationSuccessfulMessageConsoleOnly == true)
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

        public bool DownloadFileEx(string url, string destination)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(new Uri(url), destination);
                return true;
            }
            catch (WebException)
            {
                ElmThrowException(54);
                return false;
            }
            catch (UriFormatException)
            {
                ElmThrowException(72);
                return false;
            }

        }
        public bool DownloadFileExAsync(string url, string destination)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFileAsync(new Uri(url), destination);
                return true;
            }
            catch (WebException)
            {
                ElmThrowException(54);
                return false;
            }
        }

        public bool SetFileHiddenEx(string filePath) // set hidden
        {
            bool result = SetFileAttributeEx(filePath, FileAttributes.Hidden);
            return result;
        }

        public bool SetFileReadOnlyEx(string filePath)
        {
            bool result = SetFileAttributeEx(filePath, FileAttributes.ReadOnly);
            return result;
        }

        public bool SetFileTemporaryEx(string filePath)
        {
            bool result = SetFileAttributeEx(filePath, FileAttributes.Temporary);
            return result;
        }

        public bool SetFileSystemEx(string filePath)
        {
            bool result = SetFileAttributeEx(filePath, FileAttributes.System);
            return result;
        }

        public bool RemoveAttributesEx(string filePath)
        {
            bool result = SetFileAttributeEx(filePath, FileAttributes.Normal);
            return result;
        }

        public bool SetFileAttributeEx(string filePath, FileAttributes attribute)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, attribute);
                    return true;
                }
                else
                {
                    ElmThrowException(31); // create the file instead?
                    return false;
                }
            }
            catch (IOException)
            {
                ElmThrowException(29);
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return false;
            }
        }

        public bool? CreateFolderEx(string folderPath)
        {
            try
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return false;
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return false; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return false;
            }

            catch (IOException)
            {
                ElmThrowException(29);
                return false;
            }
        }

        public bool? DeleteFolderEx(string folderPath)
        {
            try
            {
                Directory.Delete(folderPath);
                return true;
            }
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return false;
            }

            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return false; // we failed
            }

            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return false;
            }

            catch (IOException)
            {
                ElmThrowException(29);
                return false;
            }

        }
    }
}
