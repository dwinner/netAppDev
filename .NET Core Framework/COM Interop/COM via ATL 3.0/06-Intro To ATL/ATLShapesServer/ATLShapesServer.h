/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sat Oct 30 13:10:23 1999
 */
/* Compiler settings for C:\Atl\Labs\Chapter 06\ATLShapesServer\ATLShapesServer.idl:
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

#ifndef __ATLShapesServer_h__
#define __ATLShapesServer_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IDraw_FWD_DEFINED__
#define __IDraw_FWD_DEFINED__
typedef interface IDraw IDraw;
#endif 	/* __IDraw_FWD_DEFINED__ */


#ifndef __IErase_FWD_DEFINED__
#define __IErase_FWD_DEFINED__
typedef interface IErase IErase;
#endif 	/* __IErase_FWD_DEFINED__ */


#ifndef __IShapeID_FWD_DEFINED__
#define __IShapeID_FWD_DEFINED__
typedef interface IShapeID IShapeID;
#endif 	/* __IShapeID_FWD_DEFINED__ */


#ifndef __IShapeEdit_FWD_DEFINED__
#define __IShapeEdit_FWD_DEFINED__
typedef interface IShapeEdit IShapeEdit;
#endif 	/* __IShapeEdit_FWD_DEFINED__ */


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
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

/* interface __MIDL_itf_ATLShapesServer_0000 */
/* [local] */ 

typedef /* [v1_enum][uuid] */ 
enum FILLTYPE
    {	HATCH	= 0,
	SOLID	= 1,
	POLKADOT	= 2
    }	FILLTYPE;



extern RPC_IF_HANDLE __MIDL_itf_ATLShapesServer_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_ATLShapesServer_0000_v0_0_s_ifspec;

#ifndef __IDraw_INTERFACE_DEFINED__
#define __IDraw_INTERFACE_DEFINED__

/* interface IDraw */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IDraw;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FF730D01-6705-11D3-B929-0020781238D4")
    IDraw : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
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



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IDraw_Draw_Proxy( 
    IDraw __RPC_FAR * This);


void __RPC_STUB IDraw_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IDraw_INTERFACE_DEFINED__ */


#ifndef __IErase_INTERFACE_DEFINED__
#define __IErase_INTERFACE_DEFINED__

/* interface IErase */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IErase;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3ABAE610-670A-11d3-B929-0020781238D4")
    IErase : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Erase( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEraseVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IErase __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IErase __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IErase __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Erase )( 
            IErase __RPC_FAR * This);
        
        END_INTERFACE
    } IEraseVtbl;

    interface IErase
    {
        CONST_VTBL struct IEraseVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IErase_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IErase_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IErase_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IErase_Erase(This)	\
    (This)->lpVtbl -> Erase(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IErase_Erase_Proxy( 
    IErase __RPC_FAR * This);


void __RPC_STUB IErase_Erase_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IErase_INTERFACE_DEFINED__ */


#ifndef __IShapeID_INTERFACE_DEFINED__
#define __IShapeID_INTERFACE_DEFINED__

/* interface IShapeID */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IShapeID;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("520BD370-670C-11d3-B929-0020781238D4")
    IShapeID : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Name( 
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
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Name )( 
            IShapeID __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Name )( 
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


#define IShapeID_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)

#define IShapeID_put_Name(This,newVal)	\
    (This)->lpVtbl -> put_Name(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE IShapeID_get_Name_Proxy( 
    IShapeID __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IShapeID_get_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE IShapeID_put_Name_Proxy( 
    IShapeID __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IShapeID_put_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IShapeID_INTERFACE_DEFINED__ */


#ifndef __IShapeEdit_INTERFACE_DEFINED__
#define __IShapeEdit_INTERFACE_DEFINED__

/* interface IShapeEdit */
/* [unique][helpstring][oleautomation][uuid][object] */ 


EXTERN_C const IID IID_IShapeEdit;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("34579870-6708-11d3-B929-0020781238D4")
    IShapeEdit : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Inverse( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Stretch( 
            /* [in] */ int factor) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Fill( 
            /* [in] */ FILLTYPE fType) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IShapeEditVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IShapeEdit __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IShapeEdit __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IShapeEdit __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Inverse )( 
            IShapeEdit __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Stretch )( 
            IShapeEdit __RPC_FAR * This,
            /* [in] */ int factor);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Fill )( 
            IShapeEdit __RPC_FAR * This,
            /* [in] */ FILLTYPE fType);
        
        END_INTERFACE
    } IShapeEditVtbl;

    interface IShapeEdit
    {
        CONST_VTBL struct IShapeEditVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IShapeEdit_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IShapeEdit_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IShapeEdit_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IShapeEdit_Inverse(This)	\
    (This)->lpVtbl -> Inverse(This)

#define IShapeEdit_Stretch(This,factor)	\
    (This)->lpVtbl -> Stretch(This,factor)

#define IShapeEdit_Fill(This,fType)	\
    (This)->lpVtbl -> Fill(This,fType)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IShapeEdit_Inverse_Proxy( 
    IShapeEdit __RPC_FAR * This);


void __RPC_STUB IShapeEdit_Inverse_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IShapeEdit_Stretch_Proxy( 
    IShapeEdit __RPC_FAR * This,
    /* [in] */ int factor);


void __RPC_STUB IShapeEdit_Stretch_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IShapeEdit_Fill_Proxy( 
    IShapeEdit __RPC_FAR * This,
    /* [in] */ FILLTYPE fType);


void __RPC_STUB IShapeEdit_Fill_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IShapeEdit_INTERFACE_DEFINED__ */



#ifndef __ATLSHAPESSERVERLib_LIBRARY_DEFINED__
#define __ATLSHAPESSERVERLib_LIBRARY_DEFINED__

/* library ATLSHAPESSERVERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLSHAPESSERVERLib;

EXTERN_C const CLSID CLSID_CoHexagon;

#ifdef __cplusplus

class DECLSPEC_UUID("DDF18644-24F6-11D3-B8F9-0020781238D4")
CoHexagon;
#endif
#endif /* __ATLSHAPESSERVERLib_LIBRARY_DEFINED__ */

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
