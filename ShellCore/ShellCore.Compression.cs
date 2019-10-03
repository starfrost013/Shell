using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Compression APIs
namespace Shell.Core
{
    partial class ShellCore
    {
        public bool? CompressZipFile(string sourceDirectory, string destinationFile)
        {
            try
            {
                ZipFile.CreateFromDirectory(sourceDirectory, destinationFile);
                return true;
            } // errors
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return null;
            }
            catch (ArgumentException)
            {
                ElmThrowException(58);
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return null;
            }
            catch (IOException)
            {
                ElmThrowException(29);
                return null;
            }
        }

        public bool? ExtractZipFile(string source, string extractDirectory)
        {
            try
            {
                ZipFile.ExtractToDirectory(source, extractDirectory);
                return true;
            } // errors
            catch (FileNotFoundException)
            {
                ElmThrowException(31);
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                ElmThrowException(32);
                return null;
            }
            catch (ArgumentException)
            {
                ElmThrowException(58);
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                ElmThrowException(30);
                return null;
            }
            catch (IOException)
            {
                ElmThrowException(29);
                return null;
            }
        }




    }
}
