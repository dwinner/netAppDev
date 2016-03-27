"C:\Program Files (x86)\WiX Toolset v3.9\bin\candle.exe" -sw1026   -ext "C:\Program Files (x86)\WiX Toolset v3.9\bin\WixIIsExtension.dll" "setup.wxs"
"C:\Program Files (x86)\WiX Toolset v3.9\bin\light.exe" -sw1076 -sw1079  "setup.wixobj"  -ext "C:\Program Files (x86)\WiX Toolset v3.9\bin\WixIIsExtension.dll" -cultures:en-US
pause