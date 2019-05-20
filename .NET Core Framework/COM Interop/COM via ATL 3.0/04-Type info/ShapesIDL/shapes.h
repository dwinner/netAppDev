/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sat Jan 01 14:30:52 2000
 */
/* Compiler settings for C:\My Documents\ATL\Labs\Chapter 04\ShapesIDL\shapes.idl:
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

#ifndef __shapes_h__
#define __shapes_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IShapeEdit_FWD_DEFINED__
#define __IShapeEdit_FWD_DEFINED__
typedef interface IShapeEdit IShapeEdit;
#endif 	/* __IShapeEdit_FWD_DEFINED__ */


#ifndef __IDraw_FWD_DEFINED__
#define __IDraw_FWD_DEFINED__
typedef interface IDraw IDraw;
#endif 	/* __IDraw_FWD_DEFINED__ */


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

/* interface __MIDL_itf_shapes_0000 */
/* [local] */ 

// This is my custom enum
typedef /* [helpstring][v1_enum][uuid] */ 
enum FILLTYPE
    {	HATCH	= 0,
	SOLID	= 1,
	POLKADOT	= 2
    }	FILLTYPE;



extern RPC_IF_HANDLE __MIDL_itf_shapes_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_shapes_0000_v0_0_s_ifspec;

#ifndef __IShapeEdit_INTERFACE_DEFINED__
#define __IShapeEdit_INTERFACE_DEFINED__

/* interface IShapeEdit */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IShapeEdit;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("442F32E2-E7EE-11d2-B8D2-0020781238D4")
    IShapeEdit : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Fill( 
            /* [in] */ FILLTYPE fType) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Inverse( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Stretch( 
            /* [in] */ int factor) = 0;
        
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
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Fill )( 
            IShapeEdit __RPC_FAR * This,
            /* [in] */ FILLTYPE fType);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Inverse )( 
            IShapeEdit __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Stretch )( 
            IShapeEdit __RPC_FAR * This,
            /* [in] */ int factor);
        
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


#define IShapeEdit_Fill(This,fType)	\
    (This)->lpVtbl -> Fill(This,fType)

#define IShapeEdit_Inverse(This)	\
    (This)->lpVtbl -> Inverse(This)

#define IShapeEdit_Stretch(This,factor)	\
    (This)->lpVtbl -> Stretch(This,factor)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IShapeEdit_Fill_Proxy( 
    IShapeEdit __RPC_FAR * This,
    /* [in] */ FILLTYPE fType);


void __RPC_STUB IShapeEdit_Fill_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


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



#endif 	/* __IShapeEdit_INTERFACE_DEFINED__ */


#ifndef __IDraw_INTERFACE_DEFINED__
#define __IDraw_INTERFACE_DEFINED__

/* interface IDraw */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IDraw;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("4B475690-DE06-11d2-AAF4-00A0C9312D57")
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



#ifndef __ShapesLibrary_LIBRARY_DEFINED__
#define __ShapesLibrary_LIBRARY_DEFINED__

/* library ShapesLibrary */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ShapesLibrary;

EXTERN_C const CLSID CLSID_CoHexagon;

#ifdef __cplusplus

class DECLSPEC_UUID("442F32E3-E7EE-11d2-B8D2-0020781238D4")
CoHexagon;
#endif
#endif /* __ShapesLibrary_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
