#pragma unmanaged
#include <stdio.h>
#include <cor.h>
#include "corjit.h"

// Delegates.
typedef ULONG_PTR *(__stdcall *GetJitHandler)();
typedef void (__stdcall *ClearCacheHandler)(ULONG_PTR classthis);
typedef int (__stdcall *CompileMethodHandler)(ULONG_PTR classthis, ICorJitInfo *comp, CORINFO_METHOD_INFO *info, unsigned flags,BYTE **nativeEntry, ULONG  *nativeSizeOfCode);

// Forward declarations
int __stdcall debugCompileMethod(ULONG_PTR classthis, ICorJitInfo *comp, CORINFO_METHOD_INFO *info, unsigned flags,BYTE **nativeEntry, ULONG  *nativeSizeOfCode);
void SwapMethod(bool orig);
struct JIT
{
	CompileMethodHandler compileMethod;
};


// Static function pointers.
CompileMethodHandler originalMethod;
CompileMethodHandler newMethod;


// static variables
bool isInit = false;				
bool isHooked = false;
JIT* jitInstance;
int jitCount = 0;


bool InitInject()
{
	// Set isinit to true
	if ( isInit )
	{
		return false;
	}
	isInit = true;

	// Load the module.
	HMODULE jitLib = LoadLibrary(L"mscorjit.dll");
	if (!jitLib){
		return false;
	}

	// Get the method.
	GetJitHandler getJitMethod = (ULONG_PTR *(__stdcall *)()) GetProcAddress(jitLib, "getJit");
	if ( !getJitMethod ){
		return false;
	}

	// Get the jit and the address of the original method.
	jitInstance = (JIT *) *((ULONG_PTR *) getJitMethod());
	originalMethod = jitInstance->compileMethod;
	newMethod = &debugCompileMethod;
	return true;

};


void HookJIT()
{
	InitInject();
	SwapMethod(false);
};
void UnhookJit()
{
	if ( !isHooked )
	{
		return;
	}
	SwapMethod(true);
}


void SwapMethod (bool orig)
{
	CompileMethodHandler method = orig ? originalMethod : newMethod;
	DWORD oldProtect;

	VirtualProtect(jitInstance, sizeof (ULONG_PTR), PAGE_READWRITE, &oldProtect);
	jitInstance->compileMethod = method;
	VirtualProtect(jitInstance, sizeof (ULONG_PTR), oldProtect, &oldProtect);
};



int __stdcall debugCompileMethod(ULONG_PTR classthis, ICorJitInfo *comp,
							   CORINFO_METHOD_INFO *info, unsigned flags,         
							   BYTE **nativeEntry, ULONG  *nativeSizeOfCode)
{
	// in case somebody hooks us (x86 only)
#ifdef _M_IX86
	__asm 
	{
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
			nop
	}
#endif
	const char *szMethodName = NULL;
	const char *szClassName = NULL;


	// Get the method and class name.
	szMethodName = comp->getMethodName(info->ftn, &szClassName);
	

	printf("\tJIT : \t0x%x %s.%s\n",(ULONG)info->ILCode,szClassName,szMethodName);	
	

	// call original method
	// not using the naked + jmp approach to avoid x64 incompatibilities and naked does not work with /clr
	int result = originalMethod(classthis, comp, info, flags, nativeEntry, nativeSizeOfCode);
	
	// debug jit count.
	jitCount++;	
	
	return result;
};


