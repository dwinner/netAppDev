
ATLCarServerps.dll: dlldata.obj ATLCarServer_p.obj ATLCarServer_i.obj
	link /dll /out:ATLCarServerps.dll /def:ATLCarServerps.def /entry:DllMain dlldata.obj ATLCarServer_p.obj ATLCarServer_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLCarServerps.dll
	@del ATLCarServerps.lib
	@del ATLCarServerps.exp
	@del dlldata.obj
	@del ATLCarServer_p.obj
	@del ATLCarServer_i.obj
