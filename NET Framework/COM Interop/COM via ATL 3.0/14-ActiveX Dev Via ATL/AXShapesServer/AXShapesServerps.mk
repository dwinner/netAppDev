
AXShapesServerps.dll: dlldata.obj AXShapesServer_p.obj AXShapesServer_i.obj
	link /dll /out:AXShapesServerps.dll /def:AXShapesServerps.def /entry:DllMain dlldata.obj AXShapesServer_p.obj AXShapesServer_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del AXShapesServerps.dll
	@del AXShapesServerps.lib
	@del AXShapesServerps.exp
	@del dlldata.obj
	@del AXShapesServer_p.obj
	@del AXShapesServer_i.obj
