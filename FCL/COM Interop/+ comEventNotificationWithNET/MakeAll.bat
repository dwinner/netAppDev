tlbimp AirlineNotify.tlb /out:AirlineNotifyMetadata.dll
echo ------------------ 
csc /target:winexe /r:System.dll,System.WinForms.dll,System.Data.DLL,System.Drawing.dll,Microsoft.Win32.Interop.dll,AirlineNotifyMetadata.dll /out:AirlineNotifyClient.exe  AirlineNotifyForm.cs
echo ------------------- 
csc /target:winexe /r:System.dll,System.WinForms.dll,System.Data.DLL,System.Drawing.dll,Microsoft.Win32.Interop.dll,AirlineNotifyMetadata.dll /out:DelegateTest.exe  DelegateTest.cs
echo ------------------- 
csc /target:exe /out:HelloWorldDelegate.exe HelloWorld.cs
echo ------------------- 