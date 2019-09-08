using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Core
{
    partial class ShellCore // ExceptionsLite Manager methods
    {
        List<Exception> ExceptionList { get; set; } // List of exception

        public int ElmInitialized { get; set; }
        // master list of exceptions
        // ExceptionsLite
        // For practice, maybe, or just doing cool things.

        public void ElmInitExceptionManager()
        {
            if (ElmInitialized == 0)
            {
                ExceptionList = new List<Exception>();
                // Register system exceptions

                ElmRegisterException("KeywordNotFoundException", 0, "A non-existent keyword / statement was found. Consult the help file for the extant statements.", 1);
                ElmRegisterException("KeywordUnimplementedException", 1, "A keyword was found that is currently unimplemented. This may be included in a later version of the Shell.", 1);
                ElmRegisterException("TooManyRootElementsException", 2, "There is more than one ShellXML element.", 1);
                ElmRegisterException("NoRootElementsException", 3, "There is no <ShellXML> Element or it is malformed.", 1);
                ElmRegisterException("EmptyDocumentException", 4, "The script file is empty (there is only a ShellXML node).", 1);
                ElmRegisterException("IncorrectVariableTypeException", 5, "An incorrect variable type was specified.", 1);
                ElmRegisterException("InvalidTypeSpecifiedException", 6, "An invalid type was specified.", 1);
                ElmRegisterException("IncorrectValueFormatException", 7, "An incorrect format was used (for example - letters used in an int-type variable)", 1);
                ElmRegisterException("ShutdownDialogNotFirstArgumentException", 8, "When executing shutdown, /i must be the first argument. Sorry, blame Microsoft. ", 1); // 0 severity?
                ElmRegisterException("BackslashNotAllowedException", 9, "The backslash is not allowed in calls to ExecuteFileEx.", 1);
                ElmRegisterException("IncorrectlyFormattedXmlTagsException", 10, "A ShellXML tag was incorrectly formatted. Tags must begin with <> and end with </>.", 1);
                ElmRegisterException("DuplicateVariableNameException", 11, "Warning: Two variables cannot have the same name. The second variable will be ignored.", 0);
                ElmRegisterException("FloatRemovedException", 12, "Floats have been removed and are not supported in ShellXML.", 1);

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
            if (exceptionId < 10000) // 10,000 and below will be used by the shell
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
            if (exceptionId > 10000)
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
                    Console.WriteLine($"{_.exceptionName} #{_.exceptionId}: {_.exceptionMessage}");

                    switch (_.exceptionSeverity)
                    {
                        case 0:
                            return _.exceptionId;
                        case 1:
                            Console.ReadKey();
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
