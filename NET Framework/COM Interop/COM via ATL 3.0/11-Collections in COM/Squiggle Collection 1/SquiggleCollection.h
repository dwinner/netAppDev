/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Mon Sep 06 20:25:48 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 11\SquiggleCollection\SquiggleCollection.idl:
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

#ifndef __SquiggleCollection_h__
#define __SquiggleCollection_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ICoSquiggles_FWD_DEFINED__
#define __ICoSquiggles_FWD_DEFINED__
typedef interface ICoSquiggles ICoSquiggles;
#endif 	/* __ICoSquiggles_FWD_DEFINED__ */


#ifndef __ICoSquiggle_FWD_DEFINED__
#define __ICoSquiggle_FWD_DEFINED__
typedef interface ICoSquiggle ICoSquiggle;
#endif 	/* __ICoSquiggle_FWD_DEFINED__ */


#ifndef __CoSquiggles_FWD_DEFINED__
#define __CoSquiggles_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoSquiggles CoSquiggles;
#else
typedef struct CoSquiggles CoSquiggles;
#endif /* __cplusplus */

#endif 	/* __CoSquiggles_FWD_DEFINED__ */


#ifndef __CoSquiggle_FWD_DEFINED__
#define __CoSquiggle_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoSquiggle CoSquiggle;
#else
typedef struct CoSquiggle CoSquiggle;
#endif /* __cplusplus */

#endif 	/* __CoSquiggle_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ICoSquiggles_INTERFACE_DEFINED__
#define __ICoSquiggles_INTERFACE_DEFINED__

/* interface ICoSquiggles */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_ICoSquiggles;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("9FB9E734-59BA-11D3-B926-0020781238D4")
    ICoSquiggles : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Item( 
            /* [in] */ VARIANT index,
            /* [retval][out] */ VARIANT __RPC_FAR *pItem) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Count( 
            /* [retval][out] */ long __RPC_FAR *pVal) = 0;
        
        virtual /* [id][restricted][propget] */ HRESULT STDMETHODCALLTYPE get__NewEnum( 
            /* [retval][out] */ LPUNKNOWN __RPC_FAR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICoSquigglesVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICoSquiggles __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICoSquiggles __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICoSquiggles __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            ICoSquiggles __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            ICoSquiggles __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            ICoSquiggles __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            ICoSquiggles __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Item )( 
            ICoSquiggles __RPC_FAR * This,
            /* [in] */ VARIANT index,
            /* [retval][out] */ VARIANT __RPC_FAR *pItem);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Count )( 
            ICoSquiggles __RPC_FAR * This,
            /* [retval][out] */ long __RPC_FAR *pVal);
        
        /* [id][restricted][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get__NewEnum )( 
            ICoSquiggles __RPC_FAR * This,
            /* [retval][out] */ LPUNKNOWN __RPC_FAR *pVal);
        
        END_INTERFACE
    } ICoSquigglesVtbl;

    interface ICoSquiggles
    {
        CONST_VTBL struct ICoSquigglesVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICoSquiggles_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICoSquiggles_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICoSquiggles_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICoSquiggles_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ICoSquiggles_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ICoSquiggles_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ICoSquiggles_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ICoSquiggles_Item(This,index,pItem)	\
    (This)->lpVtbl -> Item(This,index,pItem)

#define ICoSquiggles_get_Count(This,pVal)	\
    (This)->lpVtbl -> get_Count(This,pVal)

#define ICoSquiggles_get__NewEnum(This,pVal)	\
    (This)->lpVtbl -> get__NewEnum(This,pVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoSquiggles_Item_Proxy( 
    ICoSquiggles __RPC_FAR * This,
    /* [in] */ VARIANT index,
    /* [retval][out] */ VARIANT __RPC_FAR *pItem);


void __RPC_STUB ICoSquiggles_Item_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ICoSquiggles_get_Count_Proxy( 
    ICoSquiggles __RPC_FAR * This,
    /* [retval][out] */ long __RPC_FAR *pVal);


void __RPC_STUB ICoSquiggles_get_Count_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [id][restricted][propget] */ HRESULT STDMETHODCALLTYPE ICoSquiggles_get__NewEnum_Proxy( 
    ICoSquiggles __RPC_FAR * This,
    /* [retval][out] */ LPUNKNOWN __RPC_FAR *pVal);


void __RPC_STUB ICoSquiggles_get__NewEnum_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICoSquiggles_INTERFACE_DEFINED__ */


#ifndef __ICoSquiggle_INTERFACE_DEFINED__
#define __ICoSquiggle_INTERFACE_DEFINED__

/* interface ICoSquiggle */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_ICoSquiggle;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("9FB9E736-59BA-11D3-B926-0020781238D4")
    ICoSquiggle : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Draw( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Name( 
            /* [retval][out] */ BSTR __RPC_FAR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Name( 
            /* [in] */ BSTR newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICoSquiggleVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICoSquiggle __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICoSquiggle __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICoSquiggle __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            ICoSquiggle __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            ICoSquiggle __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            ICoSquiggle __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            ICoSquiggle __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Draw )( 
            ICoSquiggle __RPC_FAR * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Name )( 
            ICoSquiggle __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Name )( 
            ICoSquiggle __RPC_FAR * This,
            /* [in] */ BSTR newVal);
        
        END_INTERFACE
    } ICoSquiggleVtbl;

    interface ICoSquiggle
    {
        CONST_VTBL struct ICoSquiggleVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICoSquiggle_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICoSquiggle_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICoSquiggle_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICoSquiggle_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ICoSquiggle_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ICoSquiggle_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ICoSquiggle_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ICoSquiggle_Draw(This)	\
    (This)->lpVtbl -> Draw(This)

#define ICoSquiggle_get_Name(This,pVal)	\
    (This)->lpVtbl -> get_Name(This,pVal)

#define ICoSquiggle_put_Name(This,newVal)	\
    (This)->lpVtbl -> put_Name(This,newVal)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoSquiggle_Draw_Proxy( 
    ICoSquiggle __RPC_FAR * This);


void __RPC_STUB ICoSquiggle_Draw_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE ICoSquiggle_get_Name_Proxy( 
    ICoSquiggle __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pVal);


void __RPC_STUB ICoSquiggle_get_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE ICoSquiggle_put_Name_Proxy( 
    ICoSquiggle __RPC_FAR * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB ICoSquiggle_put_Name_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICoSquiggle_INTERFACE_DEFINED__ */



#ifndef __SQUIGGLECOLLECTIONLib_LIBRARY_DEFINED__
#define __SQUIGGLECOLLECTIONLib_LIBRARY_DEFINED__

/* library SQUIGGLECOLLECTIONLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_SQUIGGLECOLLECTIONLib;

EXTERN_C const CLSID CLSID_CoSquiggles;

#ifdef __cplusplus

class DECLSPEC_UUID("9FB9E735-59BA-11D3-B926-0020781238D4")
CoSquiggles;
#endif

EXTERN_C const CLSID CLSID_CoSquiggle;

#ifdef __cplusplus

class DECLSPEC_UUID("9FB9E737-59BA-11D3-B926-0020781238D4")
CoSquiggle;
#endif
#endif /* __SQUIGGLECOLLECTIONLib_LIBRARY_DEFINED__ */

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
