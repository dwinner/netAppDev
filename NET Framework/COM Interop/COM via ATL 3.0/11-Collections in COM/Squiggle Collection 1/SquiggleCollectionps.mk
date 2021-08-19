
SquiggleCollectionps.dll: dlldata.obj SquiggleCollection_p.obj SquiggleCollection_i.obj
	link /dll /out:SquiggleCollectionps.dll /def:SquiggleCollectionps.def /entry:DllMain dlldata.obj SquiggleCollection_p.obj SquiggleCollection_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del SquiggleCollectionps.dll
	@del SquiggleCollectionps.lib
	@del SquiggleCollectionps.exp
	@del dlldata.obj
	@del SquiggleCollection_p.obj
	@del SquiggleCollection_i.obj
