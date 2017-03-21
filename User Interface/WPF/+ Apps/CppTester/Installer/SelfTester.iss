[Code]

function GetVersion(Param: String): String;
begin
  Result := '1.00';
end;

function GetInstallDir(S: String): String;
begin
  Result := ExpandConstant('{app}');
end;

function NextButtonClick(CurPageID: Integer): Boolean; 
var
  ErrorCode: Integer;
  Text, CMDArgs: String;
begin
  Result := True;
  if (CurPageID = wpWelcome)
    then
    begin      
      if (not ShellExec('', '7z','', '', SW_HIDE, ewWaitUntilTerminated, ErrorCode)) then
      begin
       MsgBox('Error accessing 7z.exe! PATH environmental variable must contain 7z.exe installation directory', mbInformation, mb_Ok);
       Result:= False;
      end
    else
      begin
        Result := True;
      end;
    end
end;

{---------------------------------------------------------------------------------}

[Setup]
OutputBaseFilename=SelfTester_Setup
AppName=SelfTester
AppVerName=SelfTester {code:GetVersion}
AppPublisher=OOO "Program Verification Systems"
AppCopyright=Copyright (c) 2008-2014 OOO "Program Verification Systems"
VersionInfoVersion=1.0.0.0
AppPublisherURL=http://www.viva64.com
AppSupportURL=http://www.viva64.com
AppUpdatesURL=http://www.viva64.com
DefaultDirName=D:\SelfTester
DefaultGroupName=SelfTester
SolidCompression=false
DirExistsWarning=auto
UserInfoPage=false
InternalCompressLevel=none
Compression=lzma
ShowLanguageDialog=no
DisableDirPage=false
AlwaysShowDirOnReadyPage=true
AlwaysShowGroupOnReadyPage=true
MinVersion=0,5.0.2195sp4
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=none
SignTool=Standard

[Messages]
FinishedLabel=Setup has finished installing [name] on your computer. The application may be launched from Visual Studio.

[Types]
Name: "custom"; Description: "Custom installation"; Flags: iscustom
Name: "compact"; Description: "Compact installation"
Name: "full"; Description: "Full installation"

[Components]
Name: "Core"; Description: "Core application files"; Types: full custom compact; Flags: disablenouninstallwarning;
Name: "etalons"; Description: "Etalon Logs"; Types: full custom; Flags: disablenouninstallwarning; 
Name: "DailyTests"; Description: "Test archive for daily launches (src-for-SelfTester.7z)"; Types: full; ExtraDiskSpaceRequired: 955252736; Flags: disablenouninstallwarning;

[Files]
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\SelfTester.exe"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\SelfTester.UI.dll"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\SelfTester.Model.dll"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\WPFToolkit.dll"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\System.Windows.Controls.Input.Toolkit.dll"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\SelfTester.exe.config"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\ProgramVerificationSystems.SelfTester.Runner\bin\Release\Pics\*"; DestDir: {code:GetInstallDir}\Pics\; Flags: replacesameversion; Components: core
Source: "..\..\PVS-Studio Settings\Settings.xml.Viva64SelfTester"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\..\PVS-Studio Settings\PVS-Studio.lic"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "..\..\PVS-Studio Settings\Settings.xml.CppCat.Tester"; DestDir: {code:GetInstallDir}; Flags: replacesameversion; Components: core
Source: "restore_src.cmd"; DestDir: {code:GetInstallDir}; Flags: replacesameversion
Source: "{src}\SelfTester_setup\src-for-SelfTester.7z"; DestDir: {code:GetInstallDir}; Flags: external replacesameversion; Components: DailyTests
Source: "{src}\SelfTester_setup\EtalonLogs\*"; DestDir: "{code:GetInstallDir}\Logs\EtalonLogs\"; Flags: external replacesameversion; Components: etalons

[Icons]
Name: {group}\{cm:UninstallProgram,SelfTester}; Filename: {uninstallexe}; Flags: createonlyiffileexists; Components: core
Name: {group}\SelfTester; Filename: {code:GetInstallDir}\SelfTester.exe; WorkingDir: "{code:GetInstallDir}"; Components: core

[Run]
Filename: "{sys}\cmd.exe"; Parameters: "/c cd ./src && DEL /S /Q *.vcxproj"; WorkingDir:{code:GetInstallDir}; Flags: runhidden; StatusMsg: "Cleaning old sources"; Components: DailyTests;
Filename: "{sys}\cmd.exe"; Parameters: "/c 7z x src-for-SelfTester.7z -y"; WorkingDir:{code:GetInstallDir}; Flags: runhidden; StatusMsg: "Unpacking sources from src-for-SelfTester.7z"; Components: DailyTests;
Filename: "{sys}\cmd.exe"; Parameters: "/c ROBOCOPY %CD%\src %CD%\src_etalon *.sln *.vcproj *.vcxproj *.props *.vsprops *.cbproj *.groupproj /s /IS"; WorkingDir:{code:GetInstallDir}; Flags: runhidden; StatusMsg: "Making a copy of project files"; Components: DailyTests;
Filename: "{sys}\cmd.exe"; Parameters: "/c del /f /q src-for-SelfTester.7z"; WorkingDir:{code:GetInstallDir}; Flags: runhidden; Components: DailyTests;

[UninstallDelete]
Type: filesandordirs; Name: "{code:GetInstallDir}\src"
Type: filesandordirs; Name: "{code:GetInstallDir}\src_etalon"
Type: dirifempty; Name: "{code:GetInstallDir}"