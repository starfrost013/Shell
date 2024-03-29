; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Shell SDK (Development Release 1)"
#define MyAppVersion "0.2.88.0"
#define MyAppPublisher "avant-gard� eyes"
#define MyAppExeName "Shl.exe"
#define ApplicationVersion "0.2.88.0"
[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{366B160F-59DC-43D3-87A8-E317ADD19ABD}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\Shell
DisableProgramGroupPage=yes
InfoBeforeFile=C:\Users\Cosmo\ShellProject\Shl\Preinstall.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\Cosmo\ShellProject\Shl\Setup
OutputBaseFilename=ShellSetup
SetupIconFile=C:\osmock\Desktop\Icons\Windows\RTM Releases\Windows Vista and 7\Important Icons\imageres_109.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Cosmo\ShellProject\Shl\Shl\bin\Debug\Shl.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Cosmo\ShellProject\Shl\Shl\bin\Debug\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\Cosmo\ShellProject\Shl\Help\*"; DestDir: "{app}\Help\"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\Cosmo\ShellProject\Shl\Releasenotes.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Cosmo\ShellProject\Shl\Shell.odt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Cosmo\ShellProject\Shl\ShellxmlV1.3.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Cosmo\ShellProject\Shl\Shl.sln"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Cosmo\ShellProject\Shl\Hakone\*"; DestDir: "{app}\Hakone"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

