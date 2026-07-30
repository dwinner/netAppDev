
ATLEnumServerps.dll: dlldata.obj ATLEnumServer_p.obj ATLEnumServer_i.obj
	link /dll /out:ATLEnumServerps.dll /def:ATLEnumServerps.def /entry:DllMain dlldata.obj ATLEnumServer_p.obj ATLEnumServer_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLEnumServerps.dll
	@del ATLEnumServerps.lib
	@del ATLEnumServerps.exp
	@del dlldata.obj
	@del ATLEnumServer_p.obj
	@del ATLEnumServer_i.obj
