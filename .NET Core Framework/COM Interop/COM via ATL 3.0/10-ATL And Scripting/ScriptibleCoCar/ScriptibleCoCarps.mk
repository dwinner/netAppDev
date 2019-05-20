
ScriptibleCoCarps.dll: dlldata.obj ScriptibleCoCar_p.obj ScriptibleCoCar_i.obj
	link /dll /out:ScriptibleCoCarps.dll /def:ScriptibleCoCarps.def /entry:DllMain dlldata.obj ScriptibleCoCar_p.obj ScriptibleCoCar_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ScriptibleCoCarps.dll
	@del ScriptibleCoCarps.lib
	@del ScriptibleCoCarps.exp
	@del dlldata.obj
	@del ScriptibleCoCar_p.obj
	@del ScriptibleCoCar_i.obj
