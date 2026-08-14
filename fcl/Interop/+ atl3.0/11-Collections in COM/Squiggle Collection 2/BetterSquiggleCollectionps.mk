
BetterSquiggleCollectionps.dll: dlldata.obj BetterSquiggleCollection_p.obj BetterSquiggleCollection_i.obj
	link /dll /out:BetterSquiggleCollectionps.dll /def:BetterSquiggleCollectionps.def /entry:DllMain dlldata.obj BetterSquiggleCollection_p.obj BetterSquiggleCollection_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del BetterSquiggleCollectionps.dll
	@del BetterSquiggleCollectionps.lib
	@del BetterSquiggleCollectionps.exp
	@del dlldata.obj
	@del BetterSquiggleCollection_p.obj
	@del BetterSquiggleCollection_i.obj
