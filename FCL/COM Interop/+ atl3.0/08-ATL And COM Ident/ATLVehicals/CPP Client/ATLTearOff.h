/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sun Jul 18 17:12:27 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 08\ATLTearOff\ATLTearOff.idl:
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

#ifndef __ATLTearOff_h__
#define __ATLTearOff_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ICreateCar_FWD_DEFINED__
#define __ICreateCar_FWD_DEFINED__
typedef interface ICreateCar ICreateCar;
#endif 	/* __ICreateCar_FWD_DEFINED__ */


#ifndef __ICreate_FWD_DEFINED__
#define __ICreate_FWD_DEFINED__
typedef interface ICreate ICreate;
#endif 	/* __ICreate_FWD_DEFINED__ */


#ifndef __IStats_FWD_DEFINED__
#define __IStats_FWD_DEFINED__
typedef interface IStats IStats;
#endif 	/* __IStats_FWD_DEFINED__ */


#ifndef __IEngine_FWD_DEFINED__
#define __IEngine_FWD_DEFINED__
typedef interface IEngine IEngine;
#endif 	/* __IEngine_FWD_DEFINED__ */


#ifndef __IOwnerInfo_FWD_DEFINED__
#define __IOwnerInfo_FWD_DEFINED__
typedef interface IOwnerInfo IOwnerInfo;
#endif 	/* __IOwnerInfo_FWD_DEFINED__ */


#ifndef __ATLCoCar_FWD_DEFINED__
#define __ATLCoCar_FWD_DEFINED__

#ifdef __cplusplus
typedef class ATLCoCar ATLCoCar;
#else
typedef struct ATLCoCar ATLCoCar;
#endif /* __cplusplus */

#endif 	/* __ATLCoCar_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ICreateCar_INTERFACE_DEFINED__
#define __ICreateCar_INTERFACE_DEFINED__

/* interface ICreateCar */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ICreateCar;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CDBDECB9-331E-11D3-B904-0020781238D4")
    ICreateCar : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE SetPetName( 
            /* [in] */ BSTR petName) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE SetMaxSpeed( 
            /* [in] */ int maxSp) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICreateCarVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICreateCar __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICreateCar __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICreateCar __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SetPetName )( 
            ICreateCar __RPC_FAR * This,
            /* [in] */ BSTR petName);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SetMaxSpeed )( 
            ICreateCar __RPC_FAR * This,
            /* [in] */ int maxSp);
        
        END_INTERFACE
    } ICreateCarVtbl;

    interface ICreateCar
    {
        CONST_VTBL struct ICreateCarVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICreateCar_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICreateCar_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICreateCar_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICreateCar_SetPetName(This,petName)	\
    (This)->lpVtbl -> SetPetName(This,petName)

#define ICreateCar_SetMaxSpeed(This,maxSp)	\
    (This)->lpVtbl -> SetMaxSpeed(This,maxSp)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE ICreateCar_SetPetName_Proxy( 
    ICreateCar __RPC_FAR * This,
    /* [in] */ BSTR petName);


void __RPC_STUB ICreateCar_SetPetName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE ICreateCar_SetMaxSpeed_Proxy( 
    ICreateCar __RPC_FAR * This,
    /* [in] */ int maxSp);


void __RPC_STUB ICreateCar_SetMaxSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICreateCar_INTERFACE_DEFINED__ */


#ifndef __ICreate_INTERFACE_DEFINED__
#define __ICreate_INTERFACE_DEFINED__

/* interface ICreate */
/* [unique][helpstring][uuid][object] */ 


EXTERN_C const IID IID_ICreate;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("1FB21BF0-3CCE-11d3-B910-0020781238D4")
    ICreate : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE Create( 
            /* [in] */ BSTR ownerName,
            /* [in] */ BSTR ownerAddress,
            /* [in] */ BSTR petName,
            /* [in] */ int maxSp) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICreateVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICreate __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICreate __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICreate __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Create )( 
            ICreate __RPC_FAR * This,
            /* [in] */ BSTR ownerName,
            /* [in] */ BSTR ownerAddress,
            /* [in] */ BSTR petName,
            /* [in] */ int maxSp);
        
        END_INTERFACE
    } ICreateVtbl;

    interface ICreate
    {
        CONST_VTBL struct ICreateVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICreate_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICreate_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICreate_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICreate_Create(This,ownerName,ownerAddress,petName,maxSp)	\
    (This)->lpVtbl -> Create(This,ownerName,ownerAddress,petName,maxSp)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE ICreate_Create_Proxy( 
    ICreate __RPC_FAR * This,
    /* [in] */ BSTR ownerName,
    /* [in] */ BSTR ownerAddress,
    /* [in] */ BSTR petName,
    /* [in] */ int maxSp);


void __RPC_STUB ICreate_Create_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICreate_INTERFACE_DEFINED__ */


#ifndef __IStats_INTERFACE_DEFINED__
#define __IStats_INTERFACE_DEFINED__

/* interface IStats */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IStats;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A533DA31-D372-11d2-B8CF-0020781238D4")
    IStats : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE DisplayStats( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetPetName( 
            /* [retval][out] */ BSTR __RPC_FAR *petName) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IStatsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IStats __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IStats __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IStats __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DisplayStats )( 
            IStats __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetPetName )( 
            IStats __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *petName);
        
        END_INTERFACE
    } IStatsVtbl;

    interface IStats
    {
        CONST_VTBL struct IStatsVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IStats_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IStats_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IStats_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IStats_DisplayStats(This)	\
    (This)->lpVtbl -> DisplayStats(This)

#define IStats_GetPetName(This,petName)	\
    (This)->lpVtbl -> GetPetName(This,petName)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IStats_DisplayStats_Proxy( 
    IStats __RPC_FAR * This);


void __RPC_STUB IStats_DisplayStats_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IStats_GetPetName_Proxy( 
    IStats __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *petName);


void __RPC_STUB IStats_GetPetName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IStats_INTERFACE_DEFINED__ */


#ifndef __IEngine_INTERFACE_DEFINED__
#define __IEngine_INTERFACE_DEFINED__

/* interface IEngine */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IEngine;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A533DA30-D372-11d2-B8CF-0020781238D4")
    IEngine : public IUnknown
    {
    public:
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE SpeedUp( void) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetMaxSpeed( 
            /* [retval][out] */ int __RPC_FAR *maxSpeed) = 0;
        
        virtual /* [helpstring] */ HRESULT STDMETHODCALLTYPE GetCurSpeed( 
            /* [retval][out] */ int __RPC_FAR *curSpeed) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEngineVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IEngine __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IEngine __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IEngine __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SpeedUp )( 
            IEngine __RPC_FAR * This);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetMaxSpeed )( 
            IEngine __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *maxSpeed);
        
        /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetCurSpeed )( 
            IEngine __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *curSpeed);
        
        END_INTERFACE
    } IEngineVtbl;

    interface IEngine
    {
        CONST_VTBL struct IEngineVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IEngine_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IEngine_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IEngine_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IEngine_SpeedUp(This)	\
    (This)->lpVtbl -> SpeedUp(This)

#define IEngine_GetMaxSpeed(This,maxSpeed)	\
    (This)->lpVtbl -> GetMaxSpeed(This,maxSpeed)

#define IEngine_GetCurSpeed(This,curSpeed)	\
    (This)->lpVtbl -> GetCurSpeed(This,curSpeed)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring] */ HRESULT STDMETHODCALLTYPE IEngine_SpeedUp_Proxy( 
    IEngine __RPC_FAR * This);


void __RPC_STUB IEngine_SpeedUp_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IEngine_GetMaxSpeed_Proxy( 
    IEngine __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *maxSpeed);


void __RPC_STUB IEngine_GetMaxSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring] */ HRESULT STDMETHODCALLTYPE IEngine_GetCurSpeed_Proxy( 
    IEngine __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *curSpeed);


void __RPC_STUB IEngine_GetCurSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEngine_INTERFACE_DEFINED__ */


#ifndef __IOwnerInfo_INTERFACE_DEFINED__
#define __IOwnerInfo_INTERFACE_DEFINED__

/* interface IOwnerInfo */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_IOwnerInfo;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("530D7320-333E-11d3-B904-0020781238D4")
    IOwnerInfo : public IUnknown
    {
    public:
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Name( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE get_Address( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE put_Address( 
            /* [in] */ BSTR newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOwnerInfoVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IOwnerInfo __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IOwnerInfo __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IOwnerInfo __RPC_FAR * This);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Name )( 
            IOwnerInfo __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Name )( 
            IOwnerInfo __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Address )( 
            IOwnerInfo __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Address )( 
            IOwnerInfo __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        END_INTERFACE
    } IOwnerInfoVtbl;

    interface IOwnerInfo
    {
        CONST_VTBL struct IOwnerInfoVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOwnerInfo_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IOwnerInfo_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IOwnerInfo_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IOwnerInfo_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)

#define IOwnerInfo_put_Name(This,newVal)	\
    (This)->lpVtbl -> put_Name(This,newVal)

#define IOwnerInfo_get_Address(This,pVal)	\
    (This)->lpVtbl -> get_Address(This,pVal)

#define IOwnerInfo_put_Address(This,newVal)	\
    (This)->lpVtbl -> put_Address(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE IOwnerInfo_get_Name_Proxy( 
    IOwnerInfo __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IOwnerInfo_get_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE IOwnerInfo_put_Name_Proxy( 
    IOwnerInfo __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IOwnerInfo_put_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propget] */ HRESULT STDMETHODCALLTYPE IOwnerInfo_get_Address_Proxy( 
    IOwnerInfo __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IOwnerInfo_get_Address_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][propput] */ HRESULT STDMETHODCALLTYPE IOwnerInfo_put_Address_Proxy( 
    IOwnerInfo __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IOwnerInfo_put_Address_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IOwnerInfo_INTERFACE_DEFINED__ */



#ifndef __ATLTEAROFFLib_LIBRARY_DEFINED__
#define __ATLTEAROFFLib_LIBRARY_DEFINED__

/* library ATLTEAROFFLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLTEAROFFLib;

EXTERN_C const CLSID CLSID_ATLCoCar;

#ifdef __cplusplus

class DECLSPEC_UUID("E58B9C70-367D-11d3-B905-0020781238D4")
ATLCoCar;
#endif
#endif /* __ATLTEAROFFLib_LIBRARY_DEFINED__ */

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
