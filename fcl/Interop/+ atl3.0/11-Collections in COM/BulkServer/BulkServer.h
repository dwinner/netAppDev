/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Thu Oct 28 20:51:37 1999
 */
/* Compiler settings for C:\Atl\Labs\Chapter 11\BulkServer\BulkServer.idl:
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

#ifndef __BulkServer_h__
#define __BulkServer_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ICoSub_FWD_DEFINED__
#define __ICoSub_FWD_DEFINED__
typedef interface ICoSub ICoSub;
#endif 	/* __ICoSub_FWD_DEFINED__ */


#ifndef __ICoBulkObject_FWD_DEFINED__
#define __ICoBulkObject_FWD_DEFINED__
typedef interface ICoBulkObject ICoBulkObject;
#endif 	/* __ICoBulkObject_FWD_DEFINED__ */


#ifndef __CoBulkObject_FWD_DEFINED__
#define __CoBulkObject_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoBulkObject CoBulkObject;
#else
typedef struct CoBulkObject CoBulkObject;
#endif /* __cplusplus */

#endif 	/* __CoBulkObject_FWD_DEFINED__ */


#ifndef __CoSub_FWD_DEFINED__
#define __CoSub_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoSub CoSub;
#else
typedef struct CoSub CoSub;
#endif /* __cplusplus */

#endif 	/* __CoSub_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

/* interface __MIDL_itf_BulkServer_0000 */
/* [local] */ 

typedef 
enum PET_TYPE
    {	petDog	= 0,
	petCat	= 1,
	petTick	= 2
    }	PET_TYPE;

typedef struct  MyPet
    {
    short Age;
    BSTR Name;
    PET_TYPE Type;
    }	MyPet;



extern RPC_IF_HANDLE __MIDL_itf_BulkServer_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_BulkServer_0000_v0_0_s_ifspec;

#ifndef __ICoSub_INTERFACE_DEFINED__
#define __ICoSub_INTERFACE_DEFINED__

/* interface ICoSub */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ICoSub;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("01AB5141-8D6E-11D3-A7DE-0000E885A202")
    ICoSub : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_DatumOne( 
            /* [retval][out] */ short __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_DatumOne( 
            /* [in] */ short newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICoSubVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICoSub __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICoSub __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICoSub __RPC_FAR * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_DatumOne )( 
            ICoSub __RPC_FAR * This,
            /* [retval][out] */ short __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_DatumOne )( 
            ICoSub __RPC_FAR * This,
            /* [in] */ short newVal);
        
        END_INTERFACE
    } ICoSubVtbl;

    interface ICoSub
    {
        CONST_VTBL struct ICoSubVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICoSub_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICoSub_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICoSub_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICoSub_get_DatumOne(This,pVal)	\
    (This)->lpVtbl -> get_DatumOne(This,pVal)

#define ICoSub_put_DatumOne(This,newVal)	\
    (This)->lpVtbl -> put_DatumOne(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE ICoSub_get_DatumOne_Proxy( 
    ICoSub __RPC_FAR * This,
    /* [retval][out] */ short __RPC_FAR *pVal);


void __RPC_STUB ICoSub_get_DatumOne_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE ICoSub_put_DatumOne_Proxy( 
    ICoSub __RPC_FAR * This,
    /* [in] */ short newVal);


void __RPC_STUB ICoSub_put_DatumOne_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICoSub_INTERFACE_DEFINED__ */


#ifndef __ICoBulkObject_INTERFACE_DEFINED__
#define __ICoBulkObject_INTERFACE_DEFINED__

/* interface ICoBulkObject */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ICoBulkObject;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FA5877CD-7E58-11D3-A7DE-0000E885A202")
    ICoBulkObject : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE UseTheseStrings( 
            /* [in] */ short size,
            /* [size_is][out][in] */ BSTR __RPC_FAR strings[  ]) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE UseThisSafeArrayOfStrings( 
            /* [in] */ VARIANT strings) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GiveMeASafeArrayOfString( 
            /* [retval][out] */ VARIANT __RPC_FAR *pStrings) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE WorkWithAPet( 
            /* [out][in] */ MyPet __RPC_FAR *p) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetSubObject( 
            /* [retval][out] */ ICoSub __RPC_FAR *__RPC_FAR *ppv) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICoBulkObjectVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICoBulkObject __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICoBulkObject __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICoBulkObject __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *UseTheseStrings )( 
            ICoBulkObject __RPC_FAR * This,
            /* [in] */ short size,
            /* [size_is][out][in] */ BSTR __RPC_FAR strings[  ]);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *UseThisSafeArrayOfStrings )( 
            ICoBulkObject __RPC_FAR * This,
            /* [in] */ VARIANT strings);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GiveMeASafeArrayOfString )( 
            ICoBulkObject __RPC_FAR * This,
            /* [retval][out] */ VARIANT __RPC_FAR *pStrings);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *WorkWithAPet )( 
            ICoBulkObject __RPC_FAR * This,
            /* [out][in] */ MyPet __RPC_FAR *p);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetSubObject )( 
            ICoBulkObject __RPC_FAR * This,
            /* [retval][out] */ ICoSub __RPC_FAR *__RPC_FAR *ppv);
        
        END_INTERFACE
    } ICoBulkObjectVtbl;

    interface ICoBulkObject
    {
        CONST_VTBL struct ICoBulkObjectVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICoBulkObject_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICoBulkObject_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICoBulkObject_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICoBulkObject_UseTheseStrings(This,size,strings)	\
    (This)->lpVtbl -> UseTheseStrings(This,size,strings)

#define ICoBulkObject_UseThisSafeArrayOfStrings(This,strings)	\
    (This)->lpVtbl -> UseThisSafeArrayOfStrings(This,strings)

#define ICoBulkObject_GiveMeASafeArrayOfString(This,pStrings)	\
    (This)->lpVtbl -> GiveMeASafeArrayOfString(This,pStrings)

#define ICoBulkObject_WorkWithAPet(This,p)	\
    (This)->lpVtbl -> WorkWithAPet(This,p)

#define ICoBulkObject_GetSubObject(This,ppv)	\
    (This)->lpVtbl -> GetSubObject(This,ppv)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE ICoBulkObject_UseTheseStrings_Proxy( 
    ICoBulkObject __RPC_FAR * This,
    /* [in] */ short size,
    /* [size_is][out][in] */ BSTR __RPC_FAR strings[  ]);


void __RPC_STUB ICoBulkObject_UseTheseStrings_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE ICoBulkObject_UseThisSafeArrayOfStrings_Proxy( 
    ICoBulkObject __RPC_FAR * This,
    /* [in] */ VARIANT strings);


void __RPC_STUB ICoBulkObject_UseThisSafeArrayOfStrings_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE ICoBulkObject_GiveMeASafeArrayOfString_Proxy( 
    ICoBulkObject __RPC_FAR * This,
    /* [retval][out] */ VARIANT __RPC_FAR *pStrings);


void __RPC_STUB ICoBulkObject_GiveMeASafeArrayOfString_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE ICoBulkObject_WorkWithAPet_Proxy( 
    ICoBulkObject __RPC_FAR * This,
    /* [out][in] */ MyPet __RPC_FAR *p);


void __RPC_STUB ICoBulkObject_WorkWithAPet_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE ICoBulkObject_GetSubObject_Proxy( 
    ICoBulkObject __RPC_FAR * This,
    /* [retval][out] */ ICoSub __RPC_FAR *__RPC_FAR *ppv);


void __RPC_STUB ICoBulkObject_GetSubObject_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICoBulkObject_INTERFACE_DEFINED__ */



#ifndef __BULKSERVERLib_LIBRARY_DEFINED__
#define __BULKSERVERLib_LIBRARY_DEFINED__

/* library BULKSERVERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_BULKSERVERLib;

EXTERN_C const CLSID CLSID_CoBulkObject;

#ifdef __cplusplus

class DECLSPEC_UUID("FA5877CE-7E58-11D3-A7DE-0000E885A202")
CoBulkObject;
#endif

EXTERN_C const CLSID CLSID_CoSub;

#ifdef __cplusplus

class DECLSPEC_UUID("01AB5142-8D6E-11D3-A7DE-0000E885A202")
CoSub;
#endif
#endif /* __BULKSERVERLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long __RPC_FAR *, unsigned long            , BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  BSTR_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, BSTR __RPC_FAR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long __RPC_FAR *, BSTR __RPC_FAR * ); 

unsigned long             __RPC_USER  VARIANT_UserSize(     unsigned long __RPC_FAR *, unsigned long            , VARIANT __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  VARIANT_UserMarshal(  unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, VARIANT __RPC_FAR * ); 
unsigned char __RPC_FAR * __RPC_USER  VARIANT_UserUnmarshal(unsigned long __RPC_FAR *, unsigned char __RPC_FAR *, VARIANT __RPC_FAR * ); 
void                      __RPC_USER  VARIANT_UserFree(     unsigned long __RPC_FAR *, VARIANT __RPC_FAR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif
