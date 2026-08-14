/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sat Jul 24 14:58:35 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 08\ATLVehicals\ATLVehicals.idl:
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

#ifndef __ATLVehicals_h__
#define __ATLVehicals_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IDragRace_FWD_DEFINED__
#define __IDragRace_FWD_DEFINED__
typedef interface IDragRace IDragRace;
#endif 	/* __IDragRace_FWD_DEFINED__ */


#ifndef __ICreateMiniVan_FWD_DEFINED__
#define __ICreateMiniVan_FWD_DEFINED__
typedef interface ICreateMiniVan ICreateMiniVan;
#endif 	/* __ICreateMiniVan_FWD_DEFINED__ */


#ifndef __CoMiniVan_FWD_DEFINED__
#define __CoMiniVan_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoMiniVan CoMiniVan;
#else
typedef struct CoMiniVan CoMiniVan;
#endif /* __cplusplus */

#endif 	/* __CoMiniVan_FWD_DEFINED__ */


#ifndef __CoHotRod_FWD_DEFINED__
#define __CoHotRod_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoHotRod CoHotRod;
#else
typedef struct CoHotRod CoHotRod;
#endif /* __cplusplus */

#endif 	/* __CoHotRod_FWD_DEFINED__ */


#ifndef __CoJeep_FWD_DEFINED__
#define __CoJeep_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoJeep CoJeep;
#else
typedef struct CoJeep CoJeep;
#endif /* __cplusplus */

#endif 	/* __CoJeep_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "ATLTearOff.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __IDragRace_INTERFACE_DEFINED__
#define __IDragRace_INTERFACE_DEFINED__

/* interface IDragRace */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_IDragRace;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("DDF18627-24F6-11D3-B8F9-0020781238D4")
    IDragRace : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE DragRace( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IDragRaceVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IDragRace __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IDragRace __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IDragRace __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DragRace )( 
            IDragRace __RPC_FAR * This);
        
        END_INTERFACE
    } IDragRaceVtbl;

    interface IDragRace
    {
        CONST_VTBL struct IDragRaceVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDragRace_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IDragRace_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IDragRace_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IDragRace_DragRace(This)	\
    (This)->lpVtbl -> DragRace(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IDragRace_DragRace_Proxy( 
    IDragRace __RPC_FAR * This);


void __RPC_STUB IDragRace_DragRace_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IDragRace_INTERFACE_DEFINED__ */


#ifndef __ICreateMiniVan_INTERFACE_DEFINED__
#define __ICreateMiniVan_INTERFACE_DEFINED__

/* interface ICreateMiniVan */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ICreateMiniVan;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("E8A4D620-3E44-11d3-B910-0020781238D4")
    ICreateMiniVan : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE CreateMiniVan( 
            /* [in] */ BSTR petName,
            /* [in] */ int maxSpeed,
            /* [in] */ BSTR ownerName,
            /* [in] */ BSTR ownerAddress,
            /* [in] */ int numberOfDoors) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_NumberOfDoors( 
            /* [retval][out] */ short __RPC_FAR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICreateMiniVanVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICreateMiniVan __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICreateMiniVan __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICreateMiniVan __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CreateMiniVan )( 
            ICreateMiniVan __RPC_FAR * This,
            /* [in] */ BSTR petName,
            /* [in] */ int maxSpeed,
            /* [in] */ BSTR ownerName,
            /* [in] */ BSTR ownerAddress,
            /* [in] */ int numberOfDoors);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_NumberOfDoors )( 
            ICreateMiniVan __RPC_FAR * This,
            /* [retval][out] */ short __RPC_FAR *pVal);
        
        END_INTERFACE
    } ICreateMiniVanVtbl;

    interface ICreateMiniVan
    {
        CONST_VTBL struct ICreateMiniVanVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICreateMiniVan_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICreateMiniVan_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICreateMiniVan_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICreateMiniVan_CreateMiniVan(This,petName,maxSpeed,ownerName,ownerAddress,numberOfDoors)	\
    (This)->lpVtbl -> CreateMiniVan(This,petName,maxSpeed,ownerName,ownerAddress,numberOfDoors)

#define ICreateMiniVan_get_NumberOfDoors(This,pVal)	\
    (This)->lpVtbl -> get_NumberOfDoors(This,pVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE ICreateMiniVan_CreateMiniVan_Proxy( 
    ICreateMiniVan __RPC_FAR * This,
    /* [in] */ BSTR petName,
    /* [in] */ int maxSpeed,
    /* [in] */ BSTR ownerName,
    /* [in] */ BSTR ownerAddress,
    /* [in] */ int numberOfDoors);


void __RPC_STUB ICreateMiniVan_CreateMiniVan_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE ICreateMiniVan_get_NumberOfDoors_Proxy( 
    ICreateMiniVan __RPC_FAR * This,
    /* [retval][out] */ short __RPC_FAR *pVal);


void __RPC_STUB ICreateMiniVan_get_NumberOfDoors_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICreateMiniVan_INTERFACE_DEFINED__ */



#ifndef __ATLVEHICALSLib_LIBRARY_DEFINED__
#define __ATLVEHICALSLib_LIBRARY_DEFINED__

/* library ATLVEHICALSLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLVEHICALSLib;

EXTERN_C const CLSID CLSID_CoMiniVan;

#ifdef __cplusplus

class DECLSPEC_UUID("DDF18626-24F6-11D3-B8F9-0020781238D4")
CoMiniVan;
#endif

EXTERN_C const CLSID CLSID_CoHotRod;

#ifdef __cplusplus

class DECLSPEC_UUID("DDF18628-24F6-11D3-B8F9-0020781238D4")
CoHotRod;
#endif

EXTERN_C const CLSID CLSID_CoJeep;

#ifdef __cplusplus

class DECLSPEC_UUID("6CE19AE7-34D8-11D3-B904-0020781238D4")
CoJeep;
#endif
#endif /* __ATLVEHICALSLib_LIBRARY_DEFINED__ */

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
