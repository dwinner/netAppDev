
ATLShapesps.dll: dlldata.obj ATLShapes_p.obj ATLShapes_i.obj
	link /dll /out:ATLShapesps.dll /def:ATLShapesps.def /entry:DllMain dlldata.obj ATLShapes_p.obj ATLShapes_i.obj \
		kernel32.lib rpcndr.lib rpcns4.lib rpcrt4.lib oleaut32.lib uuid.lib \

.c.obj:
	cl /c /Ox /DWIN32 /D_WIN32_WINNT=0x0400 /DREGISTER_PROXY_DLL \
		$<

clean:
	@del ATLShapesps.dll
	@del ATLShapesps.lib
	@del ATLShapesps.exp
	@del dlldata.obj
	@del ATLShapes_p.obj
	@del ATLShapes_i.obj
