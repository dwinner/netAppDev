
ATLVehicalsps.dll: dlldata.obj ATLVehicals_p.obj ATLVehicals_i.obj
	link /dll /out:ATLVehicalsps.dll /def:ATLVehicalsps.def /entry:DllMain dlldata.obj ATLVehicals_p.obj ATLVehicals_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLVehicalsps.dll
	@del ATLVehicalsps.lib
	@del ATLVehicalsps.exp
	@del dlldata.obj
	@del ATLVehicals_p.obj
	@del ATLVehicals_i.obj
