using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Registry API 
namespace Shell.Core
{
    partial class ShellCore
    {
		// ATTN: We are missing Registry.DynData, but DynData is ONLY ON WINDOWS 9X!!!!!! So do not add it.
		public bool WinCreateRegKey(RegistryHive root, string thePath, bool writable = true)
        {
            try
            {
                switch (root)
                {
                    case RegistryHive.ClassesRoot:
                        Registry.ClassesRoot.CreateSubKey(thePath, writable); // HKEY_CLASSES_ROOT
                        return true;
                    case RegistryHive.LocalMachine:
                        Registry.LocalMachine.CreateSubKey(thePath, writable); // HKEY_LOCAL_MACHINE
                        return true;
                    case RegistryHive.Users:
                        Registry.Users.CreateSubKey(thePath, writable); // HKEY_USERS
                        return true;
                    case RegistryHive.CurrentUser:
                        Registry.CurrentUser.CreateSubKey(thePath, writable); // HKEY_CURRENT_USER
                        return true;
                    case RegistryHive.CurrentConfig:
                        Registry.CurrentConfig.CreateSubKey(thePath, writable); // HKEY_CURRENT_CONFIG
                        return true;
                    case RegistryHive.PerformanceData:
                        Registry.PerformanceData.CreateSubKey(thePath, writable); // HKEY_PERFORMANCE_DATA
                        return true;
                    default:
                        ElmThrowException(34);
                        return false;
                }
            }
            catch (UnauthorizedAccessException)
            {
                ElmThrowException(36);
                return false;
            }
            catch (System.Security.SecurityException)
            {
                ElmThrowException(37);
                return false;
            }
            catch (ArgumentException)
            {
                ElmThrowException(39);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }
        }
        public bool WinCreateRegKey(RegistryKey root, string theName, bool writable = true) // overload
        {
            try
            {
                root.CreateSubKey(theName, writable);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                ElmThrowException(36);
                return false;
            }
            catch (System.Security.SecurityException)
            {
                ElmThrowException(37);
                return false;
            }
            catch (ArgumentException)
            {
                ElmThrowException(39);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }
        }

        public RegistryKey WinOpenRegKey(RegistryHive root, string thePath, bool writable = true)
        {
            RegistryKey theKey;
            try
            {
                switch (root)
                {
                    case RegistryHive.ClassesRoot:
                        theKey = Registry.ClassesRoot.OpenSubKey(thePath, writable); // HKEY_CLASSES_ROOT
                        return theKey;
                    case RegistryHive.LocalMachine:
                        theKey = Registry.LocalMachine.OpenSubKey(thePath, writable); // HKEY_LOCAL_MACHINE
                        return theKey;
                    case RegistryHive.Users:
                        theKey = Registry.Users.OpenSubKey(thePath, writable); // HKEY_USERS
                        return theKey;
                    case RegistryHive.CurrentUser:
                        theKey = Registry.CurrentUser.OpenSubKey(thePath, writable); // HKEY_CURRENT_USER
                        return theKey;
                    case RegistryHive.CurrentConfig:
                        theKey = Registry.CurrentConfig.OpenSubKey(thePath, writable); // HKEY_CURRENT_CONFIG
                        return theKey;
                    case RegistryHive.PerformanceData:
                        theKey = Registry.PerformanceData.OpenSubKey(thePath, writable); // HKEY_PERFORMANCE_DATA
                        return theKey;
                    default:
                        ElmThrowException(35);
                        return null;
                }
            }
            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return null;
            }
            catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return null;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(38);
                return null;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return null;
            }
        }

        public RegistryKey WinOpenRegKey(RegistryKey key, string theName, bool writable = true) // opens a subkey.
        {
            try
            {
                key = key.OpenSubKey(theName, writable);
                return key;
                
            }
            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return null;
            }
            catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return null;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(38);
                return null;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return null;
            }

        }
        public bool WinDeleteRegKey(RegistryHive root, string thePath)
        {
            try
            {
                switch (root)
                {
                    case RegistryHive.ClassesRoot:
                        Registry.ClassesRoot.DeleteSubKey(thePath); // HKEY_CLASSES_ROOT
                        return true;
                    case RegistryHive.LocalMachine:
                        Registry.LocalMachine.DeleteSubKey(thePath); // HKEY_LOCAL_MACHINE
                        return true;
                    case RegistryHive.Users:
                        Registry.Users.DeleteSubKey(thePath); // HKEY_USERS
                        return true;
                    case RegistryHive.CurrentUser:
                        Registry.CurrentUser.DeleteSubKey(thePath); // HKEY_CURRENT_USER
                        return true;
                    case RegistryHive.CurrentConfig:
                        Registry.CurrentConfig.DeleteSubKey(thePath); // HKEY_CURRENT_CONFIG
                        return true;
                    case RegistryHive.PerformanceData:
                        Registry.PerformanceData.DeleteSubKey(thePath); // HKEY_PERFORMANCE_DATA
                        return true;
                    default:
                        ElmThrowException(35);
                        return false;
                }
            }
			catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return false;
            }
			catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return false;
            }
			catch (ArgumentException) // argument exception
            {
                ElmThrowException(38);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }

        }

        public bool WinDeleteRegKey(RegistryKey key, string theName, bool deltree = false)
        {
            try
            {
                if (deltree == false)
                {
                    key.DeleteSubKey(theName);
                    return true;
                }
                else
                {
                    key.DeleteSubKeyTree(theName);
                    return true;
                }
            }
            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return false;
            }
            catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return false;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(38);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }

        }

        public string[] WinListSubkeys(RegistryKey theKey) // 7.0.29+. Gets the children of a key.
        {
            try
            {
                string[] theNames = theKey.GetSubKeyNames();
                return theNames;
            }

            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return null;
            }
            catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return null;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(42);
                return null;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return null;
            }
        }

        public object WinGetRegValue(RegistryKey key, string name, string value)
        {
            try
            {
                object RegValue = key.GetValue(name, value);
                return RegValue;
            }

            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return false;
            }
            catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return false;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(42);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }
        }

        public bool WinSetRegValue(RegistryKey key, string name, object value)
        {
            try
            {
                key.SetValue(name, value); // set the registry value
                return true;
            }

            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return false;
            }
            catch (System.Security.SecurityException) // security exception
            {
                ElmThrowException(37);
                return false;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(42);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }
        }


        public bool WinDeleteRegValue(RegistryKey theKey, string valueName)
        {
            try
            {
                theKey.DeleteValue(valueName);
                return true;

            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(42);
                return false;
            }
            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return false;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return false;
            }

        }
        public string[] WinListRegValues(RegistryKey theKey)
        {
            try
            {
                string[] theValues = theKey.GetValueNames();
                return theValues;
            }
            catch (ArgumentException) // argument exception
            {
                ElmThrowException(42);
                return null;
            }
            catch (UnauthorizedAccessException) // access denied by OS
            {
                ElmThrowException(36);
                return null;
            }
            catch (IOException)
            {
                ElmThrowException(43);
                return null;
            }
        }
    }
}
