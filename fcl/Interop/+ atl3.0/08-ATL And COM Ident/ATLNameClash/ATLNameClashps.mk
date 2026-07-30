
ATLNameClashps.dll: dlldata.obj ATLNameClash_p.obj ATLNameClash_i.obj
	link /dll /out:ATLNameClashps.dll /def:ATLNameClashps.def /entry:DllMain dlldata.obj ATLNameClash_p.obj ATLNameClash_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLNameClashps.dll
	@del ATLNameClashps.lib
	@del ATLNameClashps.exp
	@del dlldata.obj
	@del ATLNameClash_p.obj
	@del ATLNameClash_i.obj
