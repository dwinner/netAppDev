/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Mon Nov 08 19:44:55 1999
 */
/* Compiler settings for C:\Atl\Labs\Chapter 12\RawConnObjServer\RawConnObjServer.idl:
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

#ifndef __RawConnObjServer_h__
#define __RawConnObjServer_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ISomeInterface_FWD_DEFINED__
#define __ISomeInterface_FWD_DEFINED__
typedef interface ISomeInterface ISomeInterface;
#endif 	/* __ISomeInterface_FWD_DEFINED__ */


#ifndef ___IOutBound_FWD_DEFINED__
#define ___IOutBound_FWD_DEFINED__
typedef interface _IOutBound _IOutBound;
#endif 	/* ___IOutBound_FWD_DEFINED__ */


#ifndef ___IOutBound_FWD_DEFINED__
#define ___IOutBound_FWD_DEFINED__
typedef interface _IOutBound _IOutBound;
#endif 	/* ___IOutBound_FWD_DEFINED__ */


#ifndef __CoSomeObject_FWD_DEFINED__
#define __CoSomeObject_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoSomeObject CoSomeObject;
#else
typedef struct CoSomeObject CoSomeObject;
#endif /* __cplusplus */

#endif 	/* __CoSomeObject_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ISomeInterface_INTERFACE_DEFINED__
#define __ISomeInterface_INTERFACE_DEFINED__

/* interface ISomeInterface */
/* [object][uuid] */ 


EXTERN_C const IID IID_ISomeInterface;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FA9D1723-6879-11d3-B929-0020781238D4")
    ISomeInterface : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE TriggerEvent( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISomeInterfaceVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ISomeInterface __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ISomeInterface __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ISomeInterface __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *TriggerEvent )( 
            ISomeInterface __RPC_FAR * This);
        
        END_INTERFACE
    } ISomeInterfaceVtbl;

    interface ISomeInterface
    {
        CONST_VTBL struct ISomeInterfaceVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISomeInterface_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ISomeInterface_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ISomeInterface_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ISomeInterface_TriggerEvent(This)	\
    (This)->lpVtbl -> TriggerEvent(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE ISomeInterface_TriggerEvent_Proxy( 
    ISomeInterface __RPC_FAR * This);


void __RPC_STUB ISomeInterface_TriggerEvent_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ISomeInterface_INTERFACE_DEFINED__ */


#ifndef ___IOutBound_INTERFACE_DEFINED__
#define ___IOutBound_INTERFACE_DEFINED__

/* interface _IOutBound */
/* [object][uuid] */ 


EXTERN_C const IID IID__IOutBound;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FA9D1721-6879-11d3-B929-0020781238D4")
    _IOutBound : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Test( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct _IOutBoundVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            _IOutBound __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            _IOutBound __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            _IOutBound __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Test )( 
            _IOutBound __RPC_FAR * This);
        
        END_INTERFACE
    } _IOutBoundVtbl;

    interface _IOutBound
    {
        CONST_VTBL struct _IOutBoundVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IOutBound_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define _IOutBound_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define _IOutBound_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define _IOutBound_Test(This)	\
    (This)->lpVtbl -> Test(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE _IOutBound_Test_Proxy( 
    _IOutBound __RPC_FAR * This);


void __RPC_STUB _IOutBound_Test_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* ___IOutBound_INTERFACE_DEFINED__ */



#ifndef __RawConnServer_LIBRARY_DEFINED__
#define __RawConnServer_LIBRARY_DEFINED__

/* library RawConnServer */
/* [uuid][version] */ 



EXTERN_C const IID LIBID_RawConnServer;

EXTERN_C const CLSID CLSID_CoSomeObject;

#ifdef __cplusplus

class DECLSPEC_UUID("FA9D1722-6879-11d3-B929-0020781238D4")
CoSomeObject;
#endif
#endif /* __RawConnServer_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
