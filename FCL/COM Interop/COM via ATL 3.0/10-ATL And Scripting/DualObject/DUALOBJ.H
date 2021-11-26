/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sun Nov 21 10:06:16 1999
 */
/* Compiler settings for C:\ATL\Labs\Chapter 10\DualObject\dualobj.idl:
    Os (OptLev=s), W1, Zp8, env=Win32, ms_ext, c_ext
    error checks: allocation ref bounds_check enum stub_data 
*/
//@@MIDL_FILE_HEADING(  )


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 440
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __dualobj_h__
#define __dualobj_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ISquiggle_FWD_DEFINED__
#define __ISquiggle_FWD_DEFINED__
typedef interface ISquiggle ISquiggle;
#endif 	/* __ISquiggle_FWD_DEFINED__ */


#ifndef __CoDualSquiggle_FWD_DEFINED__
#define __CoDualSquiggle_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoDualSquiggle CoDualSquiggle;
#else
typedef struct CoDualSquiggle CoDualSquiggle;
#endif /* __cplusplus */

#endif 	/* __CoDualSquiggle_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ISquiggle_INTERFACE_DEFINED__
#define __ISquiggle_INTERFACE_DEFINED__

/* interface ISquiggle */
/* [dual][object][uuid] */ 


EXTERN_C const IID IID_ISquiggle;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("BDFD41A0-580A-11d3-B926-0020781238D4")
    ISquiggle : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE DrawASquiggle( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE FlipASquiggle( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE EraseASquiggle( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISquiggleVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ISquiggle __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ISquiggle __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ISquiggle __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            ISquiggle __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            ISquiggle __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            ISquiggle __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            ISquiggle __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DrawASquiggle )( 
            ISquiggle __RPC_FAR * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FlipASquiggle )( 
            ISquiggle __RPC_FAR * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *EraseASquiggle )( 
            ISquiggle __RPC_FAR * This);
        
        END_INTERFACE
    } ISquiggleVtbl;

    interface ISquiggle
    {
        CONST_VTBL struct ISquiggleVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISquiggle_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISquiggle_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISquiggle_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISquiggle_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISquiggle_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISquiggle_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISquiggle_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISquiggle_DrawASquiggle(This)	\
    (This)->lpVtbl -> DrawASquiggle(This)

#define ISquiggle_FlipASquiggle(This)	\
    (This)->lpVtbl -> FlipASquiggle(This)

#define ISquiggle_EraseASquiggle(This)	\
    (This)->lpVtbl -> EraseASquiggle(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggle_DrawASquiggle_Proxy( 
    ISquiggle __RPC_FAR * This);


void __RPC_STUB ISquiggle_DrawASquiggle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggle_FlipASquiggle_Proxy( 
    ISquiggle __RPC_FAR * This);


void __RPC_STUB ISquiggle_FlipASquiggle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggle_EraseASquiggle_Proxy( 
    ISquiggle __RPC_FAR * This);


void __RPC_STUB ISquiggle_EraseASquiggle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISquiggle_INTERFACE_DEFINED__ */



#ifndef __DualObject_LIBRARY_DEFINED__
#define __DualObject_LIBRARY_DEFINED__

/* library DualObject */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_DualObject;

EXTERN_C const CLSID CLSID_CoDualSquiggle;

#ifdef __cplusplus

class DECLSPEC_UUID("BDFD41A2-580A-11d3-B926-0020781238D4")
CoDualSquiggle;
#endif
#endif /* __DualObject_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
