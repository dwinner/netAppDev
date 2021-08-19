/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sat Oct 09 23:32:13 1999
 */
/* Compiler settings for C:\ATL\Labs\Chapter 09\ATLShapes\ATLShapes.idl:
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

#ifndef __ATLShapes_h__
#define __ATLShapes_h__

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


#ifndef __IOSBuffer_FWD_DEFINED__
#define __IOSBuffer_FWD_DEFINED__
typedef interface IOSBuffer IOSBuffer;
#endif 	/* __IOSBuffer_FWD_DEFINED__ */


#ifndef __CoHexagon_FWD_DEFINED__
#define __CoHexagon_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoHexagon CoHexagon;
#else
typedef struct CoHexagon CoHexagon;
#endif /* __cplusplus */

#endif 	/* __CoHexagon_FWD_DEFINED__ */


#ifndef __CoLine_FWD_DEFINED__
#define __CoLine_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoLine CoLine;
#else
typedef struct CoLine CoLine;
#endif /* __cplusplus */

#endif 	/* __CoLine_FWD_DEFINED__ */


#ifndef __OSBuffer_FWD_DEFINED__
#define __OSBuffer_FWD_DEFINED__

#ifdef __cplusplus
typedef class OSBuffer OSBuffer;
#else
typedef struct OSBuffer OSBuffer;
#endif /* __cplusplus */

#endif 	/* __OSBuffer_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

/* interface __MIDL_itf_ATLShapes_0000 */
/* [local] */ 




extern RPC_IF_HANDLE __MIDL_itf_ATLShapes_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_ATLShapes_0000_v0_0_s_ifspec;

#ifndef __IDraw_INTERFACE_DEFINED__
#define __IDraw_INTERFACE_DEFINED__

/* interface IDraw */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IDraw;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("00D7BA39-20FF-11D3-B8F7-0020781238D4")
    IDraw : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetOSBuffer( 
            /* [retval][out] */ IOSBuffer __RPC_FAR *__RPC_FAR *pBuffer) = 0;
        
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
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetOSBuffer )( 
            IDraw __RPC_FAR * This,
            /* [retval][out] */ IOSBuffer __RPC_FAR *__RPC_FAR *pBuffer);
        
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

#define IDraw_GetOSBuffer(This,pBuffer)	\
    (This)->lpVtbl -> GetOSBuffer(This,pBuffer)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IDraw_Draw_Proxy( 
    IDraw __RPC_FAR * This);


void __RPC_STUB IDraw_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IDraw_GetOSBuffer_Proxy( 
    IDraw __RPC_FAR * This,
    /* [retval][out] */ IOSBuffer __RPC_FAR *__RPC_FAR *pBuffer);


void __RPC_STUB IDraw_GetOSBuffer_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IDraw_INTERFACE_DEFINED__ */


#ifndef __IShapeID_INTERFACE_DEFINED__
#define __IShapeID_INTERFACE_DEFINED__

/* interface IShapeID */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IShapeID;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("88A294F0-2127-11d3-B8F7-0020781238D4")
    IShapeID : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_ShapeName( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_ShapeName( 
            /* [in] */ BSTR newVal) = 0;
        
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
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_ShapeName )( 
            IShapeID __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_ShapeName )( 
            IShapeID __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
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


#define IShapeID_get_ShapeName(This,pVal)	\
    (This)->lpVtbl -> get_ShapeName(This,pVal)

#define IShapeID_put_ShapeName(This,newVal)	\
    (This)->lpVtbl -> put_ShapeName(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE IShapeID_get_ShapeName_Proxy( 
    IShapeID __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IShapeID_get_ShapeName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE IShapeID_put_ShapeName_Proxy( 
    IShapeID __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IShapeID_put_ShapeName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IShapeID_INTERFACE_DEFINED__ */


#ifndef __IOSBuffer_INTERFACE_DEFINED__
#define __IOSBuffer_INTERFACE_DEFINED__

/* interface IOSBuffer */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IOSBuffer;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("00D7BA3A-20FF-11D3-B8F7-0020781238D4")
    IOSBuffer : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE RenderToMemory( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOSBufferVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IOSBuffer __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IOSBuffer __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IOSBuffer __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *RenderToMemory )( 
            IOSBuffer __RPC_FAR * This);
        
        END_INTERFACE
    } IOSBufferVtbl;

    interface IOSBuffer
    {
        CONST_VTBL struct IOSBufferVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOSBuffer_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOSBuffer_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOSBuffer_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOSBuffer_RenderToMemory(This)	\
    (This)->lpVtbl -> RenderToMemory(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IOSBuffer_RenderToMemory_Proxy( 
    IOSBuffer __RPC_FAR * This);


void __RPC_STUB IOSBuffer_RenderToMemory_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOSBuffer_INTERFACE_DEFINED__ */



#ifndef __ATLSHAPESLib_LIBRARY_DEFINED__
#define __ATLSHAPESLib_LIBRARY_DEFINED__

/* library ATLSHAPESLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLSHAPESLib;

EXTERN_C const CLSID CLSID_CoHexagon;

#ifdef __cplusplus

class DECLSPEC_UUID("00D7BA27-20FF-11D3-B8F7-0020781238D4")
CoHexagon;
#endif

EXTERN_C const CLSID CLSID_CoLine;

#ifdef __cplusplus

class DECLSPEC_UUID("00D7BA29-20FF-11D3-B8F7-0020781238D4")
CoLine;
#endif

EXTERN_C const CLSID CLSID_OSBuffer;

#ifdef __cplusplus

class DECLSPEC_UUID("00D7BA3B-20FF-11D3-B8F7-0020781238D4")
OSBuffer;
#endif
#endif /* __ATLSHAPESLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long __RPC_FAR *, unsigned long            , BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long __RPC_FAR *, BSTR __RPC_FAR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
