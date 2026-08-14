/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Mon Sep 27 15:02:19 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 14\AXCarServer\AXCarServer.idl:
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

#ifndef __AXCarServer_h__
#define __AXCarServer_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IAxCar_FWD_DEFINED__
#define __IAxCar_FWD_DEFINED__
typedef interface IAxCar IAxCar;
#endif 	/* __IAxCar_FWD_DEFINED__ */


#ifndef ___IAxCarEvents_FWD_DEFINED__
#define ___IAxCarEvents_FWD_DEFINED__
typedef interface _IAxCarEvents _IAxCarEvents;
#endif 	/* ___IAxCarEvents_FWD_DEFINED__ */


#ifndef __AxCar_FWD_DEFINED__
#define __AxCar_FWD_DEFINED__

#ifdef __cplusplus
typedef class AxCar AxCar;
#else
typedef struct AxCar AxCar;
#endif /* __cplusplus */

#endif 	/* __AxCar_FWD_DEFINED__ */


#ifndef __ConfigureCar_FWD_DEFINED__
#define __ConfigureCar_FWD_DEFINED__

#ifdef __cplusplus
typedef class ConfigureCar ConfigureCar;
#else
typedef struct ConfigureCar ConfigureCar;
#endif /* __cplusplus */

#endif 	/* __ConfigureCar_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

/* interface __MIDL_itf_AXCarServer_0000 */
/* [local] */ 

typedef /* [v1_enum][uuid] */ 
enum AnimVal
    {	Yes	= 1,
	No	= 0
    }	AnimVal;



extern RPC_IF_HANDLE __MIDL_itf_AXCarServer_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_AXCarServer_0000_v0_0_s_ifspec;

#ifndef __IAxCar_INTERFACE_DEFINED__
#define __IAxCar_INTERFACE_DEFINED__

/* interface IAxCar */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IAxCar;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("65A0EEBF-745F-11D3-B92E-0020781238D4")
    IAxCar : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SpeedUp( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateCar( 
            /* [in] */ BSTR petName,
            /* [in] */ short maxSp) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Animate( 
            /* [retval][out] */ AnimVal __RPC_FAR *newVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Animate( 
            /* [in] */ AnimVal newVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_MaxSpeed( 
            /* [retval][out] */ short __RPC_FAR *pVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_MaxSpeed( 
            /* [in] */ short newVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_PetName( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_PetName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Speed( 
            /* [retval][out] */ short __RPC_FAR *pVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Speed( 
            /* [in] */ short newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AboutBox( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IAxCarVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IAxCar __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IAxCar __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IAxCar __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *SpeedUp )( 
            IAxCar __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CreateCar )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ BSTR petName,
            /* [in] */ short maxSp);
        
        /* [requestedit][bindable][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Animate )( 
            IAxCar __RPC_FAR * This,
            /* [retval][out] */ AnimVal __RPC_FAR *newVal);
        
        /* [requestedit][bindable][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Animate )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ AnimVal newVal);
        
        /* [requestedit][bindable][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_MaxSpeed )( 
            IAxCar __RPC_FAR * This,
            /* [retval][out] */ short __RPC_FAR *pVal);
        
        /* [requestedit][bindable][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_MaxSpeed )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ short newVal);
        
        /* [requestedit][bindable][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_PetName )( 
            IAxCar __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [requestedit][bindable][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_PetName )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        /* [requestedit][bindable][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Speed )( 
            IAxCar __RPC_FAR * This,
            /* [retval][out] */ short __RPC_FAR *pVal);
        
        /* [requestedit][bindable][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Speed )( 
            IAxCar __RPC_FAR * This,
            /* [in] */ short newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *AboutBox )( 
            IAxCar __RPC_FAR * This);
        
        END_INTERFACE
    } IAxCarVtbl;

    interface IAxCar
    {
        CONST_VTBL struct IAxCarVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IAxCar_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IAxCar_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IAxCar_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IAxCar_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IAxCar_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IAxCar_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IAxCar_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IAxCar_SpeedUp(This)	\
    (This)->lpVtbl -> SpeedUp(This)

#define IAxCar_CreateCar(This,petName,maxSp)	\
    (This)->lpVtbl -> CreateCar(This,petName,maxSp)

#define IAxCar_get_Animate(This,newVal)	\
    (This)->lpVtbl -> get_Animate(This,newVal)

#define IAxCar_put_Animate(This,newVal)	\
    (This)->lpVtbl -> put_Animate(This,newVal)

#define IAxCar_get_MaxSpeed(This,pVal)	\
    (This)->lpVtbl -> get_MaxSpeed(This,pVal)

#define IAxCar_put_MaxSpeed(This,newVal)	\
    (This)->lpVtbl -> put_MaxSpeed(This,newVal)

#define IAxCar_get_PetName(This,pVal)	\
    (This)->lpVtbl -> get_PetName(This,pVal)

#define IAxCar_put_PetName(This,newVal)	\
    (This)->lpVtbl -> put_PetName(This,newVal)

#define IAxCar_get_Speed(This,pVal)	\
    (This)->lpVtbl -> get_Speed(This,pVal)

#define IAxCar_put_Speed(This,newVal)	\
    (This)->lpVtbl -> put_Speed(This,newVal)

#define IAxCar_AboutBox(This)	\
    (This)->lpVtbl -> AboutBox(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IAxCar_SpeedUp_Proxy( 
    IAxCar __RPC_FAR * This);


void __RPC_STUB IAxCar_SpeedUp_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IAxCar_CreateCar_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [in] */ BSTR petName,
    /* [in] */ short maxSp);


void __RPC_STUB IAxCar_CreateCar_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IAxCar_get_Animate_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [retval][out] */ AnimVal __RPC_FAR *newVal);


void __RPC_STUB IAxCar_get_Animate_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IAxCar_put_Animate_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [in] */ AnimVal newVal);


void __RPC_STUB IAxCar_put_Animate_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IAxCar_get_MaxSpeed_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [retval][out] */ short __RPC_FAR *pVal);


void __RPC_STUB IAxCar_get_MaxSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IAxCar_put_MaxSpeed_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [in] */ short newVal);


void __RPC_STUB IAxCar_put_MaxSpeed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IAxCar_get_PetName_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB IAxCar_get_PetName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IAxCar_put_PetName_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IAxCar_put_PetName_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IAxCar_get_Speed_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [retval][out] */ short __RPC_FAR *pVal);


void __RPC_STUB IAxCar_get_Speed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IAxCar_put_Speed_Proxy( 
    IAxCar __RPC_FAR * This,
    /* [in] */ short newVal);


void __RPC_STUB IAxCar_put_Speed_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IAxCar_AboutBox_Proxy( 
    IAxCar __RPC_FAR * This);


void __RPC_STUB IAxCar_AboutBox_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IAxCar_INTERFACE_DEFINED__ */



#ifndef __AXCARSERVERLib_LIBRARY_DEFINED__
#define __AXCARSERVERLib_LIBRARY_DEFINED__

/* library AXCARSERVERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_AXCARSERVERLib;

#ifndef ___IAxCarEvents_DISPINTERFACE_DEFINED__
#define ___IAxCarEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IAxCarEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__IAxCarEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("65A0EEC1-745F-11D3-B92E-0020781238D4")
    _IAxCarEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IAxCarEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            _IAxCarEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            _IAxCarEvents __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            _IAxCarEvents __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            _IAxCarEvents __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            _IAxCarEvents __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            _IAxCarEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            _IAxCarEvents __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        END_INTERFACE
    } _IAxCarEventsVtbl;

    interface _IAxCarEvents
    {
        CONST_VTBL struct _IAxCarEventsVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IAxCarEvents_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define _IAxCarEvents_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define _IAxCarEvents_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define _IAxCarEvents_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define _IAxCarEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define _IAxCarEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define _IAxCarEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IAxCarEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_AxCar;

#ifdef __cplusplus

class DECLSPEC_UUID("65A0EEC0-745F-11D3-B92E-0020781238D4")
AxCar;
#endif

EXTERN_C const CLSID CLSID_ConfigureCar;

#ifdef __cplusplus

class DECLSPEC_UUID("5D9514E6-7482-11D3-B92E-0020781238D4")
ConfigureCar;
#endif
#endif /* __AXCARSERVERLib_LIBRARY_DEFINED__ */

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
