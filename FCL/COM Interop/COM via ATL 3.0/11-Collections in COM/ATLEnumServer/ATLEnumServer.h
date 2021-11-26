/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Mon Nov 08 15:19:49 1999
 */
/* Compiler settings for C:\Atl\Labs\Chapter 11\ATLEnumServer\ATLEnumServer.idl:
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

#ifndef __ATLEnumServer_h__
#define __ATLEnumServer_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IPerson_FWD_DEFINED__
#define __IPerson_FWD_DEFINED__
typedef interface IPerson IPerson;
#endif 	/* __IPerson_FWD_DEFINED__ */


#ifndef __IEnumPerson_FWD_DEFINED__
#define __IEnumPerson_FWD_DEFINED__
typedef interface IEnumPerson IEnumPerson;
#endif 	/* __IEnumPerson_FWD_DEFINED__ */


#ifndef __IPeopleHolder_FWD_DEFINED__
#define __IPeopleHolder_FWD_DEFINED__
typedef interface IPeopleHolder IPeopleHolder;
#endif 	/* __IPeopleHolder_FWD_DEFINED__ */


#ifndef __Person_FWD_DEFINED__
#define __Person_FWD_DEFINED__

#ifdef __cplusplus
typedef class Person Person;
#else
typedef struct Person Person;
#endif /* __cplusplus */

#endif 	/* __Person_FWD_DEFINED__ */


#ifndef __PeopleHolder_FWD_DEFINED__
#define __PeopleHolder_FWD_DEFINED__

#ifdef __cplusplus
typedef class PeopleHolder PeopleHolder;
#else
typedef struct PeopleHolder PeopleHolder;
#endif /* __cplusplus */

#endif 	/* __PeopleHolder_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IPerson_INTERFACE_DEFINED__
#define __IPerson_INTERFACE_DEFINED__

/* interface IPerson */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IPerson;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FE41A71F-53E5-11D3-AB20-00A0C9312D57")
    IPerson : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Name( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_ID( 
            /* [retval][out] */ short __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_ID( 
            /* [in] */ short newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IPersonVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IPerson __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IPerson __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IPerson __RPC_FAR * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Name )( 
            IPerson __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Name )( 
            IPerson __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_ID )( 
            IPerson __RPC_FAR * This,
            /* [retval][out] */ short __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_ID )( 
            IPerson __RPC_FAR * This,
            /* [in] */ short newVal);
        
        END_INTERFACE
    } IPersonVtbl;

    interface IPerson
    {
        CONST_VTBL struct IPersonVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IPerson_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IPerson_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IPerson_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IPerson_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)

#define IPerson_put_Name(This,newVal)	\
    (This)->lpVtbl -> put_Name(This,newVal)

#define IPerson_get_ID(This,pVal)	\
    (This)->lpVtbl -> get_ID(This,pVal)

#define IPerson_put_ID(This,newVal)	\
    (This)->lpVtbl -> put_ID(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE IPerson_get_Name_Proxy( 
    IPerson __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IPerson_get_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE IPerson_put_Name_Proxy( 
    IPerson __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IPerson_put_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE IPerson_get_ID_Proxy( 
    IPerson __RPC_FAR * This,
    /* [retval][out] */ short __RPC_FAR *pVal);


void __RPC_STUB IPerson_get_ID_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE IPerson_put_ID_Proxy( 
    IPerson __RPC_FAR * This,
    /* [in] */ short newVal);


void __RPC_STUB IPerson_put_ID_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IPerson_INTERFACE_DEFINED__ */


#ifndef __IEnumPerson_INTERFACE_DEFINED__
#define __IEnumPerson_INTERFACE_DEFINED__

/* interface IEnumPerson */
/* [unique][uuid][object] */ 


EXTERN_C const IID IID_IEnumPerson;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B5F10D50-53F1-11d3-AB20-00A0C9312D57")
    IEnumPerson : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Next( 
            /* [in] */ ULONG celt,
            /* [length_is][size_is][out] */ IPerson __RPC_FAR *__RPC_FAR *rgVar,
            /* [out] */ ULONG __RPC_FAR *pCeltFetched) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Skip( 
            /* [in] */ ULONG celt) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Reset( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Clone( 
            /* [out] */ IEnumPerson __RPC_FAR *__RPC_FAR *ppEnum) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEnumPersonVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IEnumPerson __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IEnumPerson __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IEnumPerson __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Next )( 
            IEnumPerson __RPC_FAR * This,
            /* [in] */ ULONG celt,
            /* [length_is][size_is][out] */ IPerson __RPC_FAR *__RPC_FAR *rgVar,
            /* [out] */ ULONG __RPC_FAR *pCeltFetched);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Skip )( 
            IEnumPerson __RPC_FAR * This,
            /* [in] */ ULONG celt);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Reset )( 
            IEnumPerson __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Clone )( 
            IEnumPerson __RPC_FAR * This,
            /* [out] */ IEnumPerson __RPC_FAR *__RPC_FAR *ppEnum);
        
        END_INTERFACE
    } IEnumPersonVtbl;

    interface IEnumPerson
    {
        CONST_VTBL struct IEnumPersonVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IEnumPerson_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IEnumPerson_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IEnumPerson_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IEnumPerson_Next(This,celt,rgVar,pCeltFetched)	\
    (This)->lpVtbl -> Next(This,celt,rgVar,pCeltFetched)

#define IEnumPerson_Skip(This,celt)	\
    (This)->lpVtbl -> Skip(This,celt)

#define IEnumPerson_Reset(This)	\
    (This)->lpVtbl -> Reset(This)

#define IEnumPerson_Clone(This,ppEnum)	\
    (This)->lpVtbl -> Clone(This,ppEnum)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IEnumPerson_Next_Proxy( 
    IEnumPerson __RPC_FAR * This,
    /* [in] */ ULONG celt,
    /* [length_is][size_is][out] */ IPerson __RPC_FAR *__RPC_FAR *rgVar,
    /* [out] */ ULONG __RPC_FAR *pCeltFetched);


void __RPC_STUB IEnumPerson_Next_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumPerson_Skip_Proxy( 
    IEnumPerson __RPC_FAR * This,
    /* [in] */ ULONG celt);


void __RPC_STUB IEnumPerson_Skip_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumPerson_Reset_Proxy( 
    IEnumPerson __RPC_FAR * This);


void __RPC_STUB IEnumPerson_Reset_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEnumPerson_Clone_Proxy( 
    IEnumPerson __RPC_FAR * This,
    /* [out] */ IEnumPerson __RPC_FAR *__RPC_FAR *ppEnum);


void __RPC_STUB IEnumPerson_Clone_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEnumPerson_INTERFACE_DEFINED__ */


#ifndef __IPeopleHolder_INTERFACE_DEFINED__
#define __IPeopleHolder_INTERFACE_DEFINED__

/* interface IPeopleHolder */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IPeopleHolder;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("FE41A722-53E5-11D3-AB20-00A0C9312D57")
    IPeopleHolder : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetPersonEnum( 
            /* [out] */ IEnumPerson __RPC_FAR *__RPC_FAR *ppEnumPerson) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IPeopleHolderVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IPeopleHolder __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IPeopleHolder __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IPeopleHolder __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetPersonEnum )( 
            IPeopleHolder __RPC_FAR * This,
            /* [out] */ IEnumPerson __RPC_FAR *__RPC_FAR *ppEnumPerson);
        
        END_INTERFACE
    } IPeopleHolderVtbl;

    interface IPeopleHolder
    {
        CONST_VTBL struct IPeopleHolderVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IPeopleHolder_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IPeopleHolder_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IPeopleHolder_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IPeopleHolder_GetPersonEnum(This,ppEnumPerson)	\
    (This)->lpVtbl -> GetPersonEnum(This,ppEnumPerson)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IPeopleHolder_GetPersonEnum_Proxy( 
    IPeopleHolder __RPC_FAR * This,
    /* [out] */ IEnumPerson __RPC_FAR *__RPC_FAR *ppEnumPerson);


void __RPC_STUB IPeopleHolder_GetPersonEnum_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IPeopleHolder_INTERFACE_DEFINED__ */



#ifndef __ATLENUMSERVERLib_LIBRARY_DEFINED__
#define __ATLENUMSERVERLib_LIBRARY_DEFINED__

/* library ATLENUMSERVERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLENUMSERVERLib;

EXTERN_C const CLSID CLSID_Person;

#ifdef __cplusplus

class DECLSPEC_UUID("FE41A720-53E5-11D3-AB20-00A0C9312D57")
Person;
#endif

EXTERN_C const CLSID CLSID_PeopleHolder;

#ifdef __cplusplus

class DECLSPEC_UUID("FE41A723-53E5-11D3-AB20-00A0C9312D57")
PeopleHolder;
#endif
#endif /* __ATLENUMSERVERLib_LIBRARY_DEFINED__ */

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
