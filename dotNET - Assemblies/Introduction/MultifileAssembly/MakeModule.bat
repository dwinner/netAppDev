csc.exe /t:module ufo.cs
csc.exe /t:library /addmodule:ufo.netmodule /out:airvehicles.dll helicopter.cs
csc.exe Client.cs
Client