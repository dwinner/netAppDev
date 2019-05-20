/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Fri Oct 29 15:09:14 1999
 */
/* Compiler settings for C:\ATL\Labs\Chapter 08\RawNameClash\rawNameClash.idl:
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

#ifndef __rawNameClash_h__
#define __rawNameClash_h__

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

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IDraw_INTERFACE_DEFINED__
#define __IDraw_INTERFACE_DEFINED__

/* interface IDraw */
/* [uuid][object] */ 


EXTERN_C const IID IID_IDraw;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("24FBC683-8D95-11d3-A7DE-0000E885A202")
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


#ifndef __I3DRender_INTERFACE_DEFINED__
#define __I3DRender_INTERFACE_DEFINED__

/* interface I3DRender */
/* [uuid][object] */ 


EXTERN_C const IID IID_I3DRender;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("24FBC686-8D95-11d3-A7DE-0000E885A202")
    I3DRender : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
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
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Draw )( 
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

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE I3DRender_Draw_Proxy( 
    I3DRender __RPC_FAR * This);


void __RPC_STUB I3DRender_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __I3DRender_INTERFACE_DEFINED__ */



#ifndef __RawNameClash_LIBRARY_DEFINED__
#define __RawNameClash_LIBRARY_DEFINED__

/* library RawNameClash */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_RawNameClash;

EXTERN_C const CLSID CLSID_Co3DHexagon;

#ifdef __cplusplus

class DECLSPEC_UUID("4443F080-3C9A-11d3-B910-0020781238D4")
Co3DHexagon;
#endif
#endif /* __RawNameClash_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
