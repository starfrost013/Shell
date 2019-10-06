// ExceptionsLite Manager V1.1 (For ShellUI 0.5.6+)
// "A better exceptions manager." - v0.5.6

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    partial class ShellCore // ExceptionsLite Manager methods
    {
        List<Exception> ExceptionList { get; set; } // master list of exceptions

        public int ElmInitialized { get; set; }

        public void ElmInitExceptionManager()
        {
            if (ElmInitialized == 0)
            {
                ExceptionList = new List<Exception>();
                // Register system exceptions

                ElmRegisterException("KeywordNotFoundException", 0, "A non-existent keyword / statement was found. Consult the help file for the extant statements.", 3);
                ElmRegisterException("KeywordUnimplementedException", 1, "A keyword was found that is currently unimplemented. This may be included in a later version of the Shell.", 3);
                ElmRegisterException("TooManyRootElementsException", 2, "There is more than one ShellXML element.", 3);
                ElmRegisterException("NoRootElementsException", 3, "There is no <ShellXML> Element or it is malformed.", 3);
                ElmRegisterException("EmptyDocumentException", 4, "The script file is empty (there is only a ShellXML node).", 3);
                ElmRegisterException("IncorrectVariableTypeException", 5, "An incorrect variable type was specified.", 3);
                ElmRegisterException("InvalidTypeSpecifiedException", 6, "An invalid type was specified.", 3);
                ElmRegisterException("IncorrectValueFormatException", 7, "An incorrect format was used (for example - letters used in an int-type variable)", 3);
                ElmRegisterException("ShutdownDialogNotFirstArgumentException", 8, "When executing shutdown, /i must be the first argument. Sorry, blame Microsoft. ", 3); // 0 severity?
                ElmRegisterException("BackslashNotAllowedException", 9, "The backslash is not allowed in calls to ExecuteFileEx.", 3);
                ElmRegisterException("IncorrectlyFormattedXmlTagsException", 10, "A ShellXML tag was incorrectly formatted. Tags must begin with <> and end with </>.", 3);
                ElmRegisterException("DuplicateVariableNameException", 11, "Warning: Two variables cannot have the same name. The second variable will be ignored.", 1);
                ElmRegisterException("UnspecifiedErrorException", 12, "We have literally no idea what happened. Sorry!", 3);
                ElmRegisterException("ShellXmlFileNotFoundException", 13, "Shell attempted to load a ShellXML file that didn't exist. Specify another path.", 2);
                ElmRegisterException("AddingStringsNotAllowedYetException", 14, "Operations cannot be applied to strings yet.", 2);
                ElmRegisterException("AddingBooleansNotAllowedException", 15, "Operations cannot be applied to booleans. Seriously, what are you thinking?", 2);
                ElmRegisterException("PosXIncorrectlyDefinedException", 16, "<Output> element: the posx attribute has an incorrect or invalid character. (e.g. numbers in a letter/string type variable)", 3);
                ElmRegisterException("PosYIncorrectlyDefinedException", 17, "<Output> element: the posy attribute has an incorrect or invalid character. (e.g. numbers in a letter/string type variable)", 3);
                ElmRegisterException("InputAttributeErrorException", 18, "There was an error parsing an <Output> element. Either you left an attribute blank, or we screwed up.", 3);
                ElmRegisterException("InvalidAttributeException", 19, "An invalid attribute was specified for an element.", 3);
                ElmRegisterException("NotACharException", 20, "Only one character can be specified for an <Input> element's var attribute, as it is a char type. ", 2);
                ElmRegisterException("NoVarNameImplicitException", 21, "No name was defined for the var attribute of an <Input> statement. Check your <Input> statements.", 2);
                ElmRegisterException("NoVarImplicitException", 22, "No var attribute was found in an <Input> statement.", 3);
                ElmRegisterException("UnspecifiedWarningException", 23, "Unknown Warning: We have no idea. This is a non-fatal warning.", 1);
                ElmRegisterException("ConfigPathNotReadyYetException", 24, "Sorry, Shell Configuration is not available yet. ", 3);
                ElmRegisterException("IfNotReadyException", 25, "<If> statements are not available yet. Please wait until ShellUI v0.5.6.", 0);
                ElmRegisterException("NoPathAttributeInFunctionException", 26, "There was no path attribute in the Function element.", 3);
                ElmRegisterException("MessageBoxNoTextException", 27, "No text was specified for a MessageBox when calling WinMsgBox().", 1);
                ElmRegisterException("MessageBoxNoCaptionException", 28, "No caption/window title was specified for a WinMsgBox().", 0);
                ElmRegisterException("DotNetIoException", 29, ".NET threw an IoException - a file may already exist and you are attempting to create it, move to it, or unzip to it.", 2);
                ElmRegisterException("UnauthorizedAccessException", 30, "Access to a file was denied when attempting to perform an operation on it.", 2);
                ElmRegisterException("FileNotFoundException", 31, "The file could not be found.", 2);
                ElmRegisterException("DirectoryNotFoundException", 32, "The directory/folder could not be found.", 2);
                ElmRegisterException("EmptyIfStatementException", 33, "An empty if statement was found.", 1); // severity 1?
                ElmRegisterException("CreateInvalidRegistryKeyException", 34, "When calling WinRegCreateKey, an invalid root parameter was specified.", 2);
                ElmRegisterException("DeleteInvalidRegistryKeyException", 35, "When calling WinRegDeleteKey, an invalid root parameter was specified.", 2);
                ElmRegisterException("RegistryAccessDeniedException", 36, "When calling a registry-related function, access to the registry was denied by the operating system. You most likely opened the key as read only. If adding writable = true to the end of your open key statement doesn't work, try running Shell as administrator. If that doesn't work, we are stupid and we shouldn't have released this.", 2);
                ElmRegisterException("RegistrySecurityException", 37, "When calling WinRegCreateKey or WinRegDeleteKey, WinRegGetValue, or WinRegSetValue, a SecurityException was thrown. Most likely, you attempted to edit a system-critical registry key.", 2);
                ElmRegisterException("CreateRegistryArgumentException", 38, "When calling WinRegCreateKey, somehow the wrong arguments were passed. This is most likely a bug, and we screwed up.", 3);
                ElmRegisterException("DeleteRegistryArgumentException", 39, "When calling WinRegDeleteKey, somehow the wrong arguments were passed. This is most likely a bug, and we screwed up.", 3);
                ElmRegisterException("NoVarException", 40, "No var attribute was found in a <Op> element. Please add it.", 2);
                ElmRegisterException("NoOpException", 41, "No op attribute was found in a <Op> element. Please add it.", 2);
                ElmRegisterException("CreateValueRegistryArgumentException", 42, "When calling WinRegSetValue, somehow the wrong arguments were passed. This is most likely a bug, and we screwed up.", 2);
                ElmRegisterException("RegIncorrectParameterException", 43, "An IOException occurred when we were trying to access the registry. It does appear that we have royally FUCKED UP.", 2);
                ElmRegisterException("InvalidOperationIdException", 44, "An invalid operationId parameter was specified in a call to BaseMath().", 2);
                ElmRegisterException("BaseMathOutOfRangeException", 45, "BaseMath() went out of the range of a double.", 1);
                ElmRegisterException("InvalidWindowPositionException", 46, "When calling UiDrawMultiple, UiDrawBlankSpace, UiSetWindowSize, UiSetCursorSize, UiSetCursorPosition, or UiSetWindowPosition, one of the parameters was negative.", 3);
                ElmRegisterException("ConsolePosOrSizeWouldOverrunBufferException", 47, "When calling UiSetWindowPosition, UiSetWindowSize, the console window would be larger than the console buffer. ", 3);
                ElmRegisterException("ConsoleHeightTooHighException", 48, "The console height cannot be above 63 columns.", 3);
                ElmRegisterException("ConsoleTitleArgumentNullException", 49, "When calling UiSetWindowTitle, a parameter was null. Please rectify this issue.", 3);
                ElmRegisterException("NoConditionException", 50, "No condition attribute was found in an <If> statement.", 3);
                ElmRegisterException("TimeoutException", 51, "The operation timed out.", 2);
                ElmRegisterException("ExceptionFuckedUpInputException", 52, "The id attribute was incorrectly specified in an <Exception> element - letters/strings may have been used.", 3);
                ElmRegisterException("ExceptionNoIdException", 53, "No id attribute was found in an <Exception> element.", 3);
                ElmRegisterException("ErrorDownloadingFileException", 54, "An error occurred when DownloadFileEx was downloading a file.", 2);
                ElmRegisterException("ModuleXmlInvalidRootNode", 55, "Fatal error loading module: An invalid root node was found in a Module.xml file. Please change the root node to <Module></Module>", 3);
                ElmRegisterException("ModuleXmlInvalid", 56, "Fatal error loading module: An error occurred while loading the module's Module.xml.", 3);
                ElmRegisterException("ModuleXmlInvalidExtends", 57, "Fatal error loading module: An invalid <Extends> node was found in a Module XML Definition File.", 3);
                ElmRegisterException("ModulesXmlCriticalErrorException", 58, "The Modules.xml file is corrupted.", 4);

                ElmInitialized = 1;
                return;
            } 

        }
       
        internal int ElmRegisterException(string exceptionName, int exceptionId, string exceptionMessage, int exceptionSeverity) // internal exception registration (pseudo-constructor for Exception struct)
        {

            Exception _ = new Exception(); // create temporary struct object for configuring the exception and then add it to the list and abandon the variable forever, sending it to the orphanage in the process, where it will then proceed to being killed at the hands of the GC. Rip baby variable.

            if (ExceptionList == null)
            {
                ShlHardError("ExceptionList fucked",3);
            }

            _.exceptionName = exceptionName;
            _.exceptionId = exceptionId;
            _.exceptionMessage = exceptionMessage;
            _.exceptionSeverity = exceptionSeverity;
            ExceptionList.Add(_);
            // _ dies here
            return 0;
        }

        public int ElmRegisterExceptionP(string exceptionName, int exceptionId, string exceptionMessage, int exceptionSeverity) // public exception registration
        {
            if (exceptionId < 3000) // 10,000 and below will be used by the shell
            {
                return 1;
            }

            ElmRegisterException(exceptionName, exceptionId, exceptionMessage, exceptionSeverity);
            return 0;
        }

        internal int ElmUnregisterException(int exceptionId)
        {
            foreach (Exception _ in ExceptionList)
            {
                if (_.exceptionId == exceptionId)
                {
                    ExceptionList.Remove(_); // remove the temporary object from the list
                    return 0;
                }
            }
            return 1; // exception ID not found

        }

        public int ElmUnregisterExceptionP(int exceptionId)
        {
            if (exceptionId >= 3000)
            {
                ElmUnregisterException(exceptionId);
                return 0;
            }
            return 2; // invalid exception id
        }

        public int ElmThrowException(int exceptionId) // Throws an exception.
        {
            foreach (Exception _ in ExceptionList )
            {
                if (_.exceptionId == exceptionId)
                {
                    // ExceptionsLite 
                    
                    switch (_.exceptionSeverity)
                    {
                        case 0:
                            Console.WriteLine($"{_.exceptionName} #{_.exceptionId}: {_.exceptionMessage}");
                            ShlWriteLog("exceptions.log", $"{_.exceptionName} Warning #{_.exceptionId}: {_.exceptionMessage}.");
                            return _.exceptionId;
                        case 1:
                            Console.WriteLine($"{_.exceptionName} Warning #{_.exceptionId}: {_.exceptionMessage}. Press Enter to continue.");
                            ShlWriteLog("exceptions.log", $"{_.exceptionName} Warning #{_.exceptionId}: {_.exceptionMessage}.");
                            return _.exceptionId;
                        case 2:
                            Console.WriteLine($"{_.exceptionName} Error #{_.exceptionId}: {_.exceptionMessage}. Press Enter to continue.");
                            ShlWriteLog("exceptions.log", $"{_.exceptionName} Warning #{_.exceptionId}: {_.exceptionMessage}.");
                            return _.exceptionId;
                        case 3:
                            Console.WriteLine($"{_.exceptionName} Fatal Error #{_.exceptionId}: {_.exceptionMessage}");
                            ShlWriteLog("exceptions.log", $"{_.exceptionName} Warning #{_.exceptionId}: {_.exceptionMessage}.");
                            Console.Read();
                            Environment.Exit(_.exceptionId); // ruh oh
                            break;
                        case 4:
                            Console.WriteLine($"{_.exceptionName} Catastrophic Error #{_.exceptionId}: {_.exceptionMessage}");
                            ShlWriteLog("exceptions.log", $"{_.exceptionName} Warning #{_.exceptionId}: {_.exceptionMessage}.");
                            Console.Read();
                            Environment.Exit(_.exceptionId); // ruh oh
                            break;
                    }
                    
                    
                }
            }
            Console.WriteLine("Error Code CRITICAL-002: Attempted to throw nonexistent exception. Must exit.");
            Environment.Exit(0xD15EA5E + 1); // 0Xd15ea5f
            return 0; // error: exception id not found (todo?: throw an exception)
        }

    }
    public struct Exception
    {
        public string exceptionName { get; set; } // The name of the exception.
        public int exceptionId { get; set; } // The ID of the exception. < 10000 is reserved for the shell.
        public string exceptionMessage { get; set; } // The message of the exception.
        public int exceptionSeverity { get; set; } // The severity. 0 = return from function with code exceptionId, 1 = exit with exitcode exceptionId.
    }
    // Oh Shit
}
