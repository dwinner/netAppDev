/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Fri Oct 01 23:22:52 1999
 */
/* Compiler settings for C:\ATL\Labs\Chapter 10\DualWithTypeInfo\dualObjWithTI.idl:
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

#ifndef __dualObjWithTI_h__
#define __dualObjWithTI_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ISquiggleTI_FWD_DEFINED__
#define __ISquiggleTI_FWD_DEFINED__
typedef interface ISquiggleTI ISquiggleTI;
#endif 	/* __ISquiggleTI_FWD_DEFINED__ */


#ifndef __CoDualSquiggleTI_FWD_DEFINED__
#define __CoDualSquiggleTI_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoDualSquiggleTI CoDualSquiggleTI;
#else
typedef struct CoDualSquiggleTI CoDualSquiggleTI;
#endif /* __cplusplus */

#endif 	/* __CoDualSquiggleTI_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ISquiggleTI_INTERFACE_DEFINED__
#define __ISquiggleTI_INTERFACE_DEFINED__

/* interface ISquiggleTI */
/* [dual][object][uuid] */ 


EXTERN_C const IID IID_ISquiggleTI;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("99E314A7-58C2-11d3-B926-0020781238D4")
    ISquiggleTI : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE DrawASquiggle( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE FlipASquiggle( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE EraseASquiggle( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Explode( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISquiggleTIVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ISquiggleTI __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ISquiggleTI __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ISquiggleTI __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            ISquiggleTI __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            ISquiggleTI __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            ISquiggleTI __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            ISquiggleTI __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DrawASquiggle )( 
            ISquiggleTI __RPC_FAR * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *FlipASquiggle )( 
            ISquiggleTI __RPC_FAR * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *EraseASquiggle )( 
            ISquiggleTI __RPC_FAR * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Explode )( 
            ISquiggleTI __RPC_FAR * This);
        
        END_INTERFACE
    } ISquiggleTIVtbl;

    interface ISquiggleTI
    {
        CONST_VTBL struct ISquiggleTIVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISquiggleTI_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISquiggleTI_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISquiggleTI_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISquiggleTI_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ISquiggleTI_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ISquiggleTI_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ISquiggleTI_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ISquiggleTI_DrawASquiggle(This)	\
    (This)->lpVtbl -> DrawASquiggle(This)

#define ISquiggleTI_FlipASquiggle(This)	\
    (This)->lpVtbl -> FlipASquiggle(This)

#define ISquiggleTI_EraseASquiggle(This)	\
    (This)->lpVtbl -> EraseASquiggle(This)

#define ISquiggleTI_Explode(This)	\
    (This)->lpVtbl -> Explode(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggleTI_DrawASquiggle_Proxy( 
    ISquiggleTI __RPC_FAR * This);


void __RPC_STUB ISquiggleTI_DrawASquiggle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggleTI_FlipASquiggle_Proxy( 
    ISquiggleTI __RPC_FAR * This);


void __RPC_STUB ISquiggleTI_FlipASquiggle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggleTI_EraseASquiggle_Proxy( 
    ISquiggleTI __RPC_FAR * This);


void __RPC_STUB ISquiggleTI_EraseASquiggle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id] */ HRESULT STDMETHODCALLTYPE ISquiggleTI_Explode_Proxy( 
    ISquiggleTI __RPC_FAR * This);


void __RPC_STUB ISquiggleTI_Explode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISquiggleTI_INTERFACE_DEFINED__ */



#ifndef __DualWithTypeInfo_LIBRARY_DEFINED__
#define __DualWithTypeInfo_LIBRARY_DEFINED__

/* library DualWithTypeInfo */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_DualWithTypeInfo;

EXTERN_C const CLSID CLSID_CoDualSquiggleTI;

#ifdef __cplusplus

class DECLSPEC_UUID("99E314AB-58C2-11d3-B926-0020781238D4")
CoDualSquiggleTI;
#endif
#endif /* __DualWithTypeInfo_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
