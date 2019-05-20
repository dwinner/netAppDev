
AXCarServerps.dll: dlldata.obj AXCarServer_p.obj AXCarServer_i.obj
	link /dll /out:AXCarServerps.dll /def:AXCarServerps.def /entry:DllMain dlldata.obj AXCarServer_p.obj AXCarServer_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del AXCarServerps.dll
	@del AXCarServerps.lib
	@del AXCarServerps.exp
	@del dlldata.obj
	@del AXCarServer_p.obj
	@del AXCarServer_i.obj
