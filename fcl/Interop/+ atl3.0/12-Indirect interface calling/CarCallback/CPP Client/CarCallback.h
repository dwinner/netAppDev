/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Thu Sep 30 11:21:12 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 12\CarCallback\CarCallback.idl:
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

#ifndef __CarCallback_h__
#define __CarCallback_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IStats_FWD_DEFINED__
#define __IStats_FWD_DEFINED__
typedef interface IStats IStats;
#endif 	/* __IStats_FWD_DEFINED__ */


#ifndef __IEngine_FWD_DEFINED__
#define __IEngine_FWD_DEFINED__
typedef interface IEngine IEngine;
#endif 	/* __IEngine_FWD_DEFINED__ */


#ifndef __ICreateCar_FWD_DEFINED__
#define __ICreateCar_FWD_DEFINED__
typedef interface ICreateCar ICreateCar;
#endif 	/* __ICreateCar_FWD_DEFINED__ */


#ifndef __IEngineEvents_FWD_DEFINED__
#define __IEngineEvents_FWD_DEFINED__
typedef interface IEngineEvents IEngineEvents;
#endif 	/* __IEngineEvents_FWD_DEFINED__ */


#ifndef __IEstablishCommunications_FWD_DEFINED__
#define __IEstablishCommunications_FWD_DEFINED__
typedef interface IEstablishCommunications IEstablishCommunications;
#endif 	/* __IEstablishCommunications_FWD_DEFINED__ */


#ifndef __IEngineEvents_FWD_DEFINED__
#define __IEngineEvents_FWD_DEFINED__
typedef interface IEngineEvents IEngineEvents;
#endif 	/* __IEngineEvents_FWD_DEFINED__ */


#ifndef __CoCarCallBack_FWD_DEFINED__
#define __CoCarCallBack_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoCarCallBack CoCarCallBack;
#else
typedef struct CoCarCallBack CoCarCallBack;
#endif /* __cplusplus */

#endif 	/* __CoCarCallBack_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

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
        virtual HRESULT STDMETHODCALLTYPE DisplayStats( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetPetName( 
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
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DisplayStats )( 
            IStats __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetPetName )( 
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



HRESULT STDMETHODCALLTYPE IStats_DisplayStats_Proxy( 
    IStats __RPC_FAR * This);


void __RPC_STUB IStats_DisplayStats_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IStats_GetPetName_Proxy( 
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
        virtual HRESULT STDMETHODCALLTYPE SpeedUp( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetMaxSpeed( 
            /* [retval][out] */ int __RPC_FAR *maxSpeed) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetCurSpeed( 
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
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SpeedUp )( 
            IEngine __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetMaxSpeed )( 
            IEngine __RPC_FAR * This,
            /* [retval][out] */ int __RPC_FAR *maxSpeed);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetCurSpeed )( 
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



HRESULT STDMETHODCALLTYPE IEngine_SpeedUp_Proxy( 
    IEngine __RPC_FAR * This);


void __RPC_STUB IEngine_SpeedUp_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEngine_GetMaxSpeed_Proxy( 
    IEngine __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *maxSpeed);


void __RPC_STUB IEngine_GetMaxSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEngine_GetCurSpeed_Proxy( 
    IEngine __RPC_FAR * This,
    /* [retval][out] */ int __RPC_FAR *curSpeed);


void __RPC_STUB IEngine_GetCurSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEngine_INTERFACE_DEFINED__ */


#ifndef __ICreateCar_INTERFACE_DEFINED__
#define __ICreateCar_INTERFACE_DEFINED__

/* interface ICreateCar */
/* [helpstring][uuid][object] */ 


EXTERN_C const IID IID_ICreateCar;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A533DA32-D372-11d2-B8CF-0020781238D4")
    ICreateCar : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE SetPetName( 
            /* [in] */ BSTR petName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetMaxSpeed( 
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
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SetPetName )( 
            ICreateCar __RPC_FAR * This,
            /* [in] */ BSTR petName);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SetMaxSpeed )( 
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



HRESULT STDMETHODCALLTYPE ICreateCar_SetPetName_Proxy( 
    ICreateCar __RPC_FAR * This,
    /* [in] */ BSTR petName);


void __RPC_STUB ICreateCar_SetPetName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE ICreateCar_SetMaxSpeed_Proxy( 
    ICreateCar __RPC_FAR * This,
    /* [in] */ int maxSp);


void __RPC_STUB ICreateCar_SetMaxSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICreateCar_INTERFACE_DEFINED__ */


#ifndef __IEngineEvents_INTERFACE_DEFINED__
#define __IEngineEvents_INTERFACE_DEFINED__

/* interface IEngineEvents */
/* [object][uuid] */ 


EXTERN_C const IID IID_IEngineEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("893D8210-688A-11d3-B929-0020781238D4")
    IEngineEvents : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE AboutToExplode( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Exploded( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEngineEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IEngineEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IEngineEvents __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IEngineEvents __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *AboutToExplode )( 
            IEngineEvents __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Exploded )( 
            IEngineEvents __RPC_FAR * This);
        
        END_INTERFACE
    } IEngineEventsVtbl;

    interface IEngineEvents
    {
        CONST_VTBL struct IEngineEventsVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IEngineEvents_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IEngineEvents_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IEngineEvents_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IEngineEvents_AboutToExplode(This)	\
    (This)->lpVtbl -> AboutToExplode(This)

#define IEngineEvents_Exploded(This)	\
    (This)->lpVtbl -> Exploded(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IEngineEvents_AboutToExplode_Proxy( 
    IEngineEvents __RPC_FAR * This);


void __RPC_STUB IEngineEvents_AboutToExplode_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEngineEvents_Exploded_Proxy( 
    IEngineEvents __RPC_FAR * This);


void __RPC_STUB IEngineEvents_Exploded_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEngineEvents_INTERFACE_DEFINED__ */


#ifndef __IEstablishCommunications_INTERFACE_DEFINED__
#define __IEstablishCommunications_INTERFACE_DEFINED__

/* interface IEstablishCommunications */
/* [object][uuid] */ 


EXTERN_C const IID IID_IEstablishCommunications;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CB26A7F0-688B-11d3-B929-0020781238D4")
    IEstablishCommunications : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Advise( 
            /* [in] */ IEngineEvents __RPC_FAR *pCallMe) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Unadvise( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IEstablishCommunicationsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IEstablishCommunications __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IEstablishCommunications __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IEstablishCommunications __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Advise )( 
            IEstablishCommunications __RPC_FAR * This,
            /* [in] */ IEngineEvents __RPC_FAR *pCallMe);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Unadvise )( 
            IEstablishCommunications __RPC_FAR * This);
        
        END_INTERFACE
    } IEstablishCommunicationsVtbl;

    interface IEstablishCommunications
    {
        CONST_VTBL struct IEstablishCommunicationsVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IEstablishCommunications_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IEstablishCommunications_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IEstablishCommunications_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IEstablishCommunications_Advise(This,pCallMe)	\
    (This)->lpVtbl -> Advise(This,pCallMe)

#define IEstablishCommunications_Unadvise(This)	\
    (This)->lpVtbl -> Unadvise(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



HRESULT STDMETHODCALLTYPE IEstablishCommunications_Advise_Proxy( 
    IEstablishCommunications __RPC_FAR * This,
    /* [in] */ IEngineEvents __RPC_FAR *pCallMe);


void __RPC_STUB IEstablishCommunications_Advise_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


HRESULT STDMETHODCALLTYPE IEstablishCommunications_Unadvise_Proxy( 
    IEstablishCommunications __RPC_FAR * This);


void __RPC_STUB IEstablishCommunications_Unadvise_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IEstablishCommunications_INTERFACE_DEFINED__ */



#ifndef __CarCallBackServer_LIBRARY_DEFINED__
#define __CarCallBackServer_LIBRARY_DEFINED__

/* library CarCallBackServer */
/* [helpstring][version][uuid] */ 



EXTERN_C const IID LIBID_CarCallBackServer;

EXTERN_C const CLSID CLSID_CoCarCallBack;

#ifdef __cplusplus

class DECLSPEC_UUID("881552B0-688C-11d3-B929-0020781238D4")
CoCarCallBack;
#endif
#endif /* __CarCallBackServer_LIBRARY_DEFINED__ */

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
