
ATLWindowExampleps.dll: dlldata.obj ATLWindowExample_p.obj ATLWindowExample_i.obj
	link /dll /out:ATLWindowExampleps.dll /def:ATLWindowExampleps.def /entry:DllMain dlldata.obj ATLWindowExample_p.obj ATLWindowExample_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLWindowExampleps.dll
	@del ATLWindowExampleps.lib
	@del ATLWindowExampleps.exp
	@del dlldata.obj
	@del ATLWindowExample_p.obj
	@del ATLWindowExample_i.obj
