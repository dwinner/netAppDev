/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Tue Dec 28 21:41:59 1999
 */
/* Compiler settings for C:\My Documents\ATL\Labs\Chapter 08\RawNested\rawNested.idl:
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

#ifndef __rawNested_h__
#define __rawNested_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IDraw_FWD_DEFINED__
#define __IDraw_FWD_DEFINED__
typedef interface IDraw IDraw;
#endif 	/* __IDraw_FWD_DEFINED__ */


#ifndef __IShapeID_FWD_DEFINED__
#define __IShapeID_FWD_DEFINED__
typedef interface IShapeID IShapeID;
#endif 	/* __IShapeID_FWD_DEFINED__ */


#ifndef __CoHexagon_FWD_DEFINED__
#define __CoHexagon_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoHexagon CoHexagon;
#else
typedef struct CoHexagon CoHexagon;
#endif /* __cplusplus */

#endif 	/* __CoHexagon_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IDraw_INTERFACE_DEFINED__
#define __IDraw_INTERFACE_DEFINED__

/* interface IDraw */
/* [uuid][object] */ 


EXTERN_C const IID IID_IDraw;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A533DA31-D372-11d2-B8CF-0020781238D4")
    IDraw : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
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
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Draw )( 
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

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IDraw_Draw_Proxy( 
    IDraw __RPC_FAR * This);


void __RPC_STUB IDraw_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IDraw_INTERFACE_DEFINED__ */


#ifndef __IShapeID_INTERFACE_DEFINED__
#define __IShapeID_INTERFACE_DEFINED__

/* interface IShapeID */
/* [uuid][object] */ 


EXTERN_C const IID IID_IShapeID;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("DD900363-2C02-11d3-B901-0020781238D4")
    IShapeID : public IUnknown
    {
    public:
        virtual /* [propput][helpstring] */ HRESULT STDMETHODCALLTYPE put_ID( 
            /* [in] */ int ShapeId) = 0;
        
        virtual /* [propget][helpstring] */ HRESULT STDMETHODCALLTYPE get_ID( 
            /* [retval][out] */ int __RPC_FAR *ShapeId) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IShapeIDVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IShapeID __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IShapeID __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IShapeID __RPC_FAR * This);
        
        /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_ID )( 
            IShapeID __RPC_FAR * This,
            /* [in] */ int ShapeId);
        
        /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_ID )( 
            IShapeID __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *ShapeId);
        
        END_INTERFACE
    } IShapeIDVtbl;

    interface IShapeID
    {
        CONST_VTBL struct IShapeIDVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IShapeID_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IShapeID_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IShapeID_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IShapeID_put_ID(This,ShapeId)	\
    (This)->lpVtbl -> put_ID(This,ShapeId)

#define IShapeID_get_ID(This,ShapeId)	\
    (This)->lpVtbl -> get_ID(This,ShapeId)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [propput][helpstring] */ HRESULT STDMETHODCALLTYPE IShapeID_put_ID_Proxy( 
    IShapeID __RPC_FAR * This,
    /* [in] */ int ShapeId);


void __RPC_STUB IShapeID_put_ID_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [propget][helpstring] */ HRESULT STDMETHODCALLTYPE IShapeID_get_ID_Proxy( 
    IShapeID __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *ShapeId);


void __RPC_STUB IShapeID_get_ID_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IShapeID_INTERFACE_DEFINED__ */



#ifndef __RawNested_LIBRARY_DEFINED__
#define __RawNested_LIBRARY_DEFINED__

/* library RawNested */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_RawNested;

EXTERN_C const CLSID CLSID_CoHexagon;

#ifdef __cplusplus

class DECLSPEC_UUID("AD454EA0-2C15-11d3-B901-0020781238D4")
CoHexagon;
#endif
#endif /* __RawNested_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
