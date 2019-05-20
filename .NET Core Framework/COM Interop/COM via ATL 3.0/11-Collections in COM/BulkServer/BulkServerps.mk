
BulkServerps.dll: dlldata.obj BulkServer_p.obj BulkServer_i.obj
	link /dll /out:BulkServerps.dll /def:BulkServerps.def /entry:DllMain dlldata.obj BulkServer_p.obj BulkServer_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del BulkServerps.dll
	@del BulkServerps.lib
	@del BulkServerps.exp
	@del dlldata.obj
	@del BulkServer_p.obj
	@del BulkServer_i.obj
