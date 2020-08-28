
ATLTearOffps.dll: dlldata.obj ATLTearOff_p.obj ATLTearOff_i.obj
	link /dll /out:ATLTearOffps.dll /def:ATLTearOffps.def /entry:DllMain dlldata.obj ATLTearOff_p.obj ATLTearOff_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLTearOffps.dll
	@del ATLTearOffps.lib
	@del ATLTearOffps.exp
	@del dlldata.obj
	@del ATLTearOff_p.obj
	@del ATLTearOff_i.obj
