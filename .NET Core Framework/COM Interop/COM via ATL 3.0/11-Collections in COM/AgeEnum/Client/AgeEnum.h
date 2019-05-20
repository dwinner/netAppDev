/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Tue Oct 26 08:52:22 1999
 */
/* Compiler settings for D:\WINNT\Profiles\IIAWT.001\Desktop\Labs\Chapter 11\AgeEnum\AgeEnum.idl:
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

#ifndef __AgeEnum_h__
#define __AgeEnum_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IEnumAge_FWD_DEFINED__
#define __IEnumAge_FWD_DEFINED__
typedef interface IEnumAge IEnumAge;
#endif 	/* __IEnumAge_FWD_DEFINED__ */


#ifndef __CoRawAgeHolder_FWD_DEFINED__
#define __CoRawAgeHolder_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoRawAgeHolder CoRawAgeHolder;
#else
typedef struct CoRawAgeHolder CoRawAgeHolder;
#endif /* __cplusplus */

#endif 	/* __CoRawAgeHolder_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IEnumAge_INTERFACE_DEFINED__
#define __IEnumAge_INTERFACE_DEFINED__

/* interface IEnumAge */
/* [unique][uuid][object] */ 


EXTERN_C const IID IID_IEnumAge;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("ED5DCBD5-61B5-11d3-B929-0020781238D4")
    IEnumAge : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Next( 
            /* [in] */ ULONG celt,
            /* [length_is][size_is][out] */ ULONG __RPC_FAR *rgVar,
            /* [out] */ ULONG __RPC_FAR *pCeltFetched) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Skip( 
            /* [in] */ ULONG celt) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Reset( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Clone( 
            /* [out] */ IEnumAge __RPC_FAR *__RPC_FAR *ppEnum) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEnumAgeVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IEnumAge __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IEnumAge __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IEnumAge __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Next )( 
            IEnumAge __RPC_FAR * This,
            /* [in] */ ULONG celt,
            /* [length_is][size_is][out] */ ULONG __RPC_FAR *rgVar,
            /* [out] */ ULONG __RPC_FAR *pCeltFetched);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Skip )( 
            IEnumAge __RPC_FAR * This,
            /* [in] */ ULONG celt);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Reset )( 
            IEnumAge __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Clone )( 
            IEnumAge __RPC_FAR * This,
            /* [out] */ IEnumAge __RPC_FAR *__RPC_FAR *ppEnum);
        
        END_INTERFACE
    } IEnumAgeVtbl;

    interface IEnumAge
    {
        CONST_VTBL struct IEnumAgeVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IEnumAge_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IEnumAge_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IEnumAge_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IEnumAge_Next(This,celt,rgVar,pCeltFetched)	\
    (This)->lpVtbl -> Next(This,celt,rgVar,pCeltFetched)

#define IEnumAge_Skip(This,celt)	\
    (This)->lpVtbl -> Skip(This,celt)

#define IEnumAge_Reset(This)	\
    (This)->lpVtbl -> Reset(This)

#define IEnumAge_Clone(This,ppEnum)	\
    (This)->lpVtbl -> Clone(This,ppEnum)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IEnumAge_Next_Proxy( 
    IEnumAge __RPC_FAR * This,
    /* [in] */ ULONG celt,
    /* [length_is][size_is][out] */ ULONG __RPC_FAR *rgVar,
    /* [out] */ ULONG __RPC_FAR *pCeltFetched);


void __RPC_STUB IEnumAge_Next_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumAge_Skip_Proxy( 
    IEnumAge __RPC_FAR * This,
    /* [in] */ ULONG celt);


void __RPC_STUB IEnumAge_Skip_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumAge_Reset_Proxy( 
    IEnumAge __RPC_FAR * This);


void __RPC_STUB IEnumAge_Reset_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumAge_Clone_Proxy( 
    IEnumAge __RPC_FAR * This,
    /* [out] */ IEnumAge __RPC_FAR *__RPC_FAR *ppEnum);


void __RPC_STUB IEnumAge_Clone_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEnumAge_INTERFACE_DEFINED__ */



#ifndef __RAWAgeLib_LIBRARY_DEFINED__
#define __RAWAgeLib_LIBRARY_DEFINED__

/* library RAWAgeLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_RAWAgeLib;

EXTERN_C const CLSID CLSID_CoRawAgeHolder;

#ifdef __cplusplus

class DECLSPEC_UUID("ED5DCBD4-61B5-11d3-B929-0020781238D4")
CoRawAgeHolder;
#endif
#endif /* __RAWAgeLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
