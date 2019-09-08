Cosmo's Shell
VERSION 0.1.5.0
Documentation Version 2.2

Documentation Version History:
Version	Date		Purpose
V1.0	2019-08-16	First version for Useful Library 2.0
V2.0	2019-08-26	Updated for Version 4.0 + ShellUI v0.2.0
V2.1	2019-08-26	Updated for Version 4.2 + ShellUI v0.2.1
V2.3	2019-09-02	Updated for Version 4.2.3 + ShellUI v0.3.9

Library Versions
ShellCore (previously Cosmo's Useful Library and even before that Blaze.API) - 4.2.3.0
ShellUI - 0.3.9.0

---README---

This is a TUI (text user interface)-based shell I am coding in C# for fun and learning purposes. It is designed for nothing more.

Files:
ShellCore.dll - This is the main and most developed library. Version 4.0. 
ShellUI.dll - This is what will handle the TUI. Only 3 functions & basic XML parsing right now. Version 0.3.9.
Shl.exe - This is the main shell. Do not use - in progress. Version 0.1.5.

Documentation:
--ShellCore--
There is only one class now, unlike version 2. There is an unfinished and unused interface that no class implements called IUsefulLibrary, a hangover from version 3.x (which was never released). It will be cleaned up in the future. 

Class ShellCore()

C#

Int[] GetVersion - Returns the version of the shell.
Array elements:
0 = major version
1 = minor version
2 = build
3 = revision

ExceptionsLite Manager (still ShellCore):

ExceptionsLite is a very tiny and small exception manager designed for ShellCore/Useful Library v4.0 and later applications. 

Properties:
List<Exception> ExceptionsList - A list of all exceptions that can be thrown. Will be populated at shell start. Use a foreach loop in c# to loop through these and get
the individual exceptions. 

Structs:
Exception
	string exceptionName - The name of the exception (e.g. FileNotFoundOhFuck)
	int exceptionId - The ID of the exception. IDs below 10,000 are not allowed and are reserved by the shell for errors that occur within the shell.
	string exceptionMessage - The exception message.
	int exceptionSeverity - Exception severity. 0 = simply returns the exceptions' exceptionId as an int. 1 = exits program, using exceptionId as the exit code.
Both of these print the exception message.

ElmRegisterExceptionP(string exceptionName, int exceptionId, string exceptionMessage, int exceptionSeverity)
Registers an exception with the parameters specified in the arguments.

ElmUnregisterExceptionP(int exceptionId)
Unregisters an exception with the exception exceptionId. It must be registered.

ElmThrowException(int exceptionId)
Throws an exception. The exception has to be previously registered with ElmRegisterExceptionP()


Returns the exceptionId if severity is 0, otherwise exits the program with an exit code correspondent to the value of exceptionId.

ShellCore File IO:
bool CreateFileEx(string path, string initialContent=null, bool verifyThatNotAlreadyExist = false)
Extended File.Create(). Creates a file, optionally writes initial content to it, and then closes it. Also optionally verifies that the file does not already exist
If initialContent is not null, it will add content to the file on creation. If verifyThatNotAlreadyExist is true, it will not create the file if it already exists.
public bool DeleteFileEx(string path, bool askUserConsoleOnly = false, string confirmationMessage="Are you sure you want to delete")
Deletes a file. Optionally asks the user (console only) if they want to delete it. 
If askUserConsoleOnly is true, it will ask confirmationMessage. The filename and ? are added by the function itself. If the user presses no, the file is not deleted and the function returns false. Otherwise it returns true.
public bool CopyFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully copied to")
Copies the file at oldpath to newpath. Optionally tells the user (console only) if the operation was successful.
If operationSuccessfulMessageConsoleOnly is true, it will show operationSuccessfulMessage if the operation completed successfully.. The filename and ? are added by the function itself. 
public bool MoveFileEx(string oldpath, string newpath, bool operationSuccessfulMessageConsoleOnly = false, string operationSuccessfulMessage = "The file was successfully moved to")
Moves the file at oldpath to newpath. Optionally tells the user (console only) if the operation was successful.
If operationSuccessfulMessageConsoleOnly is true, it will show operationSuccessfulMessage if the operation completed successfully.. The filename and ? are added by the function itself. Returns false if error.

public bool VerifyExistence(string path)
Checks if a file exists. Returns true if it does exist, false if it does not. 

--ShellUI--
DOCUMENTATION COMING SOON
--Shl--
It does nothing.
