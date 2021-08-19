/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Wed Sep 29 23:33:35 1999
 */
/* Compiler settings for E:\ATL\Labs\Chapter 13\ATLWindow\ATLWindowExample.idl:
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

#ifndef __ATLWindowExample_h__
#define __ATLWindowExample_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __ICoWindow_FWD_DEFINED__
#define __ICoWindow_FWD_DEFINED__
typedef interface ICoWindow ICoWindow;
#endif 	/* __ICoWindow_FWD_DEFINED__ */


#ifndef __CoWindow_FWD_DEFINED__
#define __CoWindow_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoWindow CoWindow;
#else
typedef struct CoWindow CoWindow;
#endif /* __cplusplus */

#endif 	/* __CoWindow_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

#ifndef __ICoWindow_INTERFACE_DEFINED__
#define __ICoWindow_INTERFACE_DEFINED__

/* interface ICoWindow */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_ICoWindow;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("1DF4E5EC-9509-11D2-AAD0-00A0C9312D57")
    ICoWindow : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateMyWindow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE KillMyWindow( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE DrawCircle( 
            /* [in] */ int top,
            /* [in] */ int left,
            /* [in] */ int bottom,
            /* [in] */ int right) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ChangeTheColor( 
            /* [in] */ OLE_COLOR newColor) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ChangeWindowText( 
            /* [in] */ BSTR newText) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICoWindowVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            ICoWindow __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            ICoWindow __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            ICoWindow __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *CreateMyWindow )( 
            ICoWindow __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *KillMyWindow )( 
            ICoWindow __RPC_FAR * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *DrawCircle )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ int top,
            /* [in] */ int left,
            /* [in] */ int bottom,
            /* [in] */ int right);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *ChangeTheColor )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ OLE_COLOR newColor);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *ChangeWindowText )( 
            ICoWindow __RPC_FAR * This,
            /* [in] */ BSTR newText);
        
        END_INTERFACE
    } ICoWindowVtbl;

    interface ICoWindow
    {
        CONST_VTBL struct ICoWindowVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICoWindow_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define ICoWindow_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define ICoWindow_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define ICoWindow_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define ICoWindow_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define ICoWindow_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define ICoWindow_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define ICoWindow_CreateMyWindow(This)	\
    (This)->lpVtbl -> CreateMyWindow(This)

#define ICoWindow_KillMyWindow(This)	\
    (This)->lpVtbl -> KillMyWindow(This)

#define ICoWindow_DrawCircle(This,top,left,bottom,right)	\
    (This)->lpVtbl -> DrawCircle(This,top,left,bottom,right)

#define ICoWindow_ChangeTheColor(This,newColor)	\
    (This)->lpVtbl -> ChangeTheColor(This,newColor)

#define ICoWindow_ChangeWindowText(This,newText)	\
    (This)->lpVtbl -> ChangeWindowText(This,newText)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoWindow_CreateMyWindow_Proxy( 
    ICoWindow __RPC_FAR * This);


void __RPC_STUB ICoWindow_CreateMyWindow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoWindow_KillMyWindow_Proxy( 
    ICoWindow __RPC_FAR * This);


void __RPC_STUB ICoWindow_KillMyWindow_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoWindow_DrawCircle_Proxy( 
    ICoWindow __RPC_FAR * This,
    /* [in] */ int top,
    /* [in] */ int left,
    /* [in] */ int bottom,
    /* [in] */ int right);


void __RPC_STUB ICoWindow_DrawCircle_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoWindow_ChangeTheColor_Proxy( 
    ICoWindow __RPC_FAR * This,
    /* [in] */ OLE_COLOR newColor);


void __RPC_STUB ICoWindow_ChangeTheColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ICoWindow_ChangeWindowText_Proxy( 
    ICoWindow __RPC_FAR * This,
    /* [in] */ BSTR newText);


void __RPC_STUB ICoWindow_ChangeWindowText_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __ICoWindow_INTERFACE_DEFINED__ */



#ifndef __ATLWINDOWEXAMPLELib_LIBRARY_DEFINED__
#define __ATLWINDOWEXAMPLELib_LIBRARY_DEFINED__

/* library ATLWINDOWEXAMPLELib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_ATLWINDOWEXAMPLELib;

EXTERN_C const CLSID CLSID_CoWindow;

#ifdef __cplusplus

class DECLSPEC_UUID("1DF4E5DF-9509-11D2-AAD0-00A0C9312D57")
CoWindow;
#endif
#endif /* __ATLWINDOWEXAMPLELib_LIBRARY_DEFINED__ */

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
