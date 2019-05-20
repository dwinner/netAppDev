/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sat Jul 17 18:51:41 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 08\ATLNameClash\ATLNameClash.idl:
    Oicf (OptLev=i2), W1, Zp8, env=Win32, ms_ext, c_ext
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

#ifndef __ATLNameClash_h__
#define __ATLNameClash_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IDraw_FWD_DEFINED__
#define __IDraw_FWD_DEFINED__
typedef interface IDraw IDraw;
#endif 	/* __IDraw_FWD_DEFINED__ */


#ifndef __I3DRender_FWD_DEFINED__
#define __I3DRender_FWD_DEFINED__
typedef interface I3DRender I3DRender;
#endif 	/* __I3DRender_FWD_DEFINED__ */


#ifndef __Co3DHexagon_FWD_DEFINED__
#define __Co3DHexagon_FWD_DEFINED__

#ifdef __cplusplus
typedef class Co3DHexagon Co3DHexagon;
#else
typedef struct Co3DHexagon Co3DHexagon;
#endif /* __cplusplus */

#endif 	/* __Co3DHexagon_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IDraw_INTERFACE_DEFINED__
#define __IDraw_INTERFACE_DEFINED__

/* interface IDraw */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IDraw;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("8945C9C7-2C00-11D3-B901-0020781238D4")
    IDraw : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE AnotherIDraw( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IDrawVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IDraw __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IDraw __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IDraw __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Draw )( 
            IDraw __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *AnotherIDraw )( 
            IDraw __RPC_FAR * This);
        
        END_INTERFACE
    } IDrawVtbl;

    interface IDraw
    {
        CONST_VTBL struct IDrawVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDraw_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IDraw_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IDraw_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IDraw_Draw(This)	\
    (This)->lpVtbl -> Draw(This)

#define IDraw_AnotherIDraw(This)	\
    (This)->lpVtbl -> AnotherIDraw(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IDraw_Draw_Proxy( 
    IDraw __RPC_FAR * This);


void __RPC_STUB IDraw_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IDraw_AnotherIDraw_Proxy( 
    IDraw __RPC_FAR * This);


void __RPC_STUB IDraw_AnotherIDraw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IDraw_INTERFACE_DEFINED__ */


#ifndef __I3DRender_INTERFACE_DEFINED__
#define __I3DRender_INTERFACE_DEFINED__

/* interface I3DRender */
/* [uuid][object] */ 


EXTERN_C const IID IID_I3DRender;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("DD900360-2C02-11d3-B901-0020781238D4")
    I3DRender : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Another3dRender( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct I3DRenderVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            I3DRender __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            I3DRender __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            I3DRender __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Draw )( 
            I3DRender __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Another3dRender )( 
            I3DRender __RPC_FAR * This);
        
        END_INTERFACE
    } I3DRenderVtbl;

    interface I3DRender
    {
        CONST_VTBL struct I3DRenderVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define I3DRender_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define I3DRender_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define I3DRender_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define I3DRender_Draw(This)	\
    (This)->lpVtbl -> Draw(This)

#define I3DRender_Another3dRender(This)	\
    (This)->lpVtbl -> Another3dRender(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE I3DRender_Draw_Proxy( 
    I3DRender __RPC_FAR * This);


void __RPC_STUB I3DRender_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE I3DRender_Another3dRender_Proxy( 
    I3DRender __RPC_FAR * This);


void __RPC_STUB I3DRender_Another3dRender_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __I3DRender_INTERFACE_DEFINED__ */



#ifndef __ATLNAMECLASHLib_LIBRARY_DEFINED__
#define __ATLNAMECLASHLib_LIBRARY_DEFINED__

/* library ATLNAMECLASHLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLNAMECLASHLib;

EXTERN_C const CLSID CLSID_Co3DHexagon;

#ifdef __cplusplus

class DECLSPEC_UUID("8945C9C8-2C00-11D3-B901-0020781238D4")
Co3DHexagon;
#endif
#endif /* __ATLNAMECLASHLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
