
ATLEventsps.dll: dlldata.obj ATLEvents_p.obj ATLEvents_i.obj
	link /dll /out:ATLEventsps.dll /def:ATLEventsps.def /entry:DllMain dlldata.obj ATLEvents_p.obj ATLEvents_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLEventsps.dll
	@del ATLEventsps.lib
	@del ATLEventsps.exp
	@del dlldata.obj
	@del ATLEvents_p.obj
	@del ATLEvents_i.obj
