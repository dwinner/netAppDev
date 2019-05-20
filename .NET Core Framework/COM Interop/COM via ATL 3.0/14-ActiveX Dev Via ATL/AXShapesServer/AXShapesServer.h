/* this ALWAYS GENERATED file contains the definitions for the interfaces */


/* File created by MIDL compiler version 5.01.0164 */
/* at Sun Jan 02 21:44:06 2000
 */
/* Compiler settings for C:\My Documents\ATL\Labs\Chapter 14\AXShapesServer\AXShapesServer.idl:
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

#ifndef __AXShapesServer_h__
#define __AXShapesServer_h__

#ifdef __cplusplus
extern "C"{
#endif 

/* Forward Declarations */ 

#ifndef __IShapesControl_FWD_DEFINED__
#define __IShapesControl_FWD_DEFINED__
typedef interface IShapesControl IShapesControl;
#endif 	/* __IShapesControl_FWD_DEFINED__ */


#ifndef ___IShapesControlEvents_FWD_DEFINED__
#define ___IShapesControlEvents_FWD_DEFINED__
typedef interface _IShapesControlEvents _IShapesControlEvents;
#endif 	/* ___IShapesControlEvents_FWD_DEFINED__ */


#ifndef __ShapesControl_FWD_DEFINED__
#define __ShapesControl_FWD_DEFINED__

#ifdef __cplusplus
typedef class ShapesControl ShapesControl;
#else
typedef struct ShapesControl ShapesControl;
#endif /* __cplusplus */

#endif 	/* __ShapesControl_FWD_DEFINED__ */


#ifndef __CoCusomPage_FWD_DEFINED__
#define __CoCusomPage_FWD_DEFINED__

#ifdef __cplusplus
typedef class CoCusomPage CoCusomPage;
#else
typedef struct CoCusomPage CoCusomPage;
#endif /* __cplusplus */

#endif 	/* __CoCusomPage_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

void __RPC_FAR * __RPC_USER MIDL_user_allocate(size_t);
void __RPC_USER MIDL_user_free( void __RPC_FAR * ); 

/* interface __MIDL_itf_AXShapesServer_0000 */
/* [local] */ 

typedef /* [v1_enum][uuid] */ 
enum CURRENTSHAPE
    {	shapeCIRCLE	= 0,
	shapeSQUARE	= 1,
	shapeRECTANGLE	= 2
    }	CURRENTSHAPE;



extern RPC_IF_HANDLE __MIDL_itf_AXShapesServer_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_AXShapesServer_0000_v0_0_s_ifspec;

#ifndef __IShapesControl_INTERFACE_DEFINED__
#define __IShapesControl_INTERFACE_DEFINED__

/* interface IShapesControl */
/* [unique][helpstring][dual][uuid][object] */ 


EXTERN_C const IID IID_IShapesControl;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("03FBA31F-71D1-11D3-B92D-0020781238D4")
    IShapesControl : public IDispatch
    {
    public:
        virtual /* [requestedit][bindable][id][propput] */ HRESULT STDMETHODCALLTYPE put_BackColor( 
            /* [in] */ OLE_COLOR clr) = 0;
        
        virtual /* [requestedit][bindable][id][propget] */ HRESULT STDMETHODCALLTYPE get_BackColor( 
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr) = 0;
        
        virtual /* [requestedit][bindable][id][propputref] */ HRESULT STDMETHODCALLTYPE putref_Font( 
            /* [in] */ IFontDisp __RPC_FAR *pFont) = 0;
        
        virtual /* [requestedit][bindable][id][propput] */ HRESULT STDMETHODCALLTYPE put_Font( 
            /* [in] */ IFontDisp __RPC_FAR *pFont) = 0;
        
        virtual /* [requestedit][bindable][id][propget] */ HRESULT STDMETHODCALLTYPE get_Font( 
            /* [retval][out] */ IFontDisp __RPC_FAR *__RPC_FAR *ppFont) = 0;
        
        virtual /* [requestedit][bindable][id][propput] */ HRESULT STDMETHODCALLTYPE put_Caption( 
            /* [in] */ BSTR strCaption) = 0;
        
        virtual /* [requestedit][bindable][id][propget] */ HRESULT STDMETHODCALLTYPE get_Caption( 
            /* [retval][out] */ BSTR __RPC_FAR *pstrCaption) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ShapeType( 
            /* [retval][out] */ CURRENTSHAPE __RPC_FAR *pVal) = 0;
        
        virtual /* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ShapeType( 
            /* [in] */ CURRENTSHAPE newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AboutBox( void) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IShapesControlVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            IShapesControl __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            IShapesControl __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            IShapesControl __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        /* [requestedit][bindable][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_BackColor )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ OLE_COLOR clr);
        
        /* [requestedit][bindable][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_BackColor )( 
            IShapesControl __RPC_FAR * This,
            /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);
        
        /* [requestedit][bindable][id][propputref] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *putref_Font )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ IFontDisp __RPC_FAR *pFont);
        
        /* [requestedit][bindable][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Font )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ IFontDisp __RPC_FAR *pFont);
        
        /* [requestedit][bindable][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Font )( 
            IShapesControl __RPC_FAR * This,
            /* [retval][out] */ IFontDisp __RPC_FAR *__RPC_FAR *ppFont);
        
        /* [requestedit][bindable][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_Caption )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ BSTR strCaption);
        
        /* [requestedit][bindable][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_Caption )( 
            IShapesControl __RPC_FAR * This,
            /* [retval][out] */ BSTR __RPC_FAR *pstrCaption);
        
        /* [requestedit][bindable][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *get_ShapeType )( 
            IShapesControl __RPC_FAR * This,
            /* [retval][out] */ CURRENTSHAPE __RPC_FAR *pVal);
        
        /* [requestedit][bindable][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *put_ShapeType )( 
            IShapesControl __RPC_FAR * This,
            /* [in] */ CURRENTSHAPE newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *AboutBox )( 
            IShapesControl __RPC_FAR * This);
        
        END_INTERFACE
    } IShapesControlVtbl;

    interface IShapesControl
    {
        CONST_VTBL struct IShapesControlVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IShapesControl_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define IShapesControl_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define IShapesControl_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define IShapesControl_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define IShapesControl_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define IShapesControl_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define IShapesControl_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)


#define IShapesControl_put_BackColor(This,clr)	\
    (This)->lpVtbl -> put_BackColor(This,clr)

#define IShapesControl_get_BackColor(This,pclr)	\
    (This)->lpVtbl -> get_BackColor(This,pclr)

#define IShapesControl_putref_Font(This,pFont)	\
    (This)->lpVtbl -> putref_Font(This,pFont)

#define IShapesControl_put_Font(This,pFont)	\
    (This)->lpVtbl -> put_Font(This,pFont)

#define IShapesControl_get_Font(This,ppFont)	\
    (This)->lpVtbl -> get_Font(This,ppFont)

#define IShapesControl_put_Caption(This,strCaption)	\
    (This)->lpVtbl -> put_Caption(This,strCaption)

#define IShapesControl_get_Caption(This,pstrCaption)	\
    (This)->lpVtbl -> get_Caption(This,pstrCaption)

#define IShapesControl_get_ShapeType(This,pVal)	\
    (This)->lpVtbl -> get_ShapeType(This,pVal)

#define IShapesControl_put_ShapeType(This,newVal)	\
    (This)->lpVtbl -> put_ShapeType(This,newVal)

#define IShapesControl_AboutBox(This)	\
    (This)->lpVtbl -> AboutBox(This)

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [requestedit][bindable][id][propput] */ HRESULT STDMETHODCALLTYPE IShapesControl_put_BackColor_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [in] */ OLE_COLOR clr);


void __RPC_STUB IShapesControl_put_BackColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][id][propget] */ HRESULT STDMETHODCALLTYPE IShapesControl_get_BackColor_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [retval][out] */ OLE_COLOR __RPC_FAR *pclr);


void __RPC_STUB IShapesControl_get_BackColor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][id][propputref] */ HRESULT STDMETHODCALLTYPE IShapesControl_putref_Font_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [in] */ IFontDisp __RPC_FAR *pFont);


void __RPC_STUB IShapesControl_putref_Font_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][id][propput] */ HRESULT STDMETHODCALLTYPE IShapesControl_put_Font_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [in] */ IFontDisp __RPC_FAR *pFont);


void __RPC_STUB IShapesControl_put_Font_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][id][propget] */ HRESULT STDMETHODCALLTYPE IShapesControl_get_Font_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [retval][out] */ IFontDisp __RPC_FAR *__RPC_FAR *ppFont);


void __RPC_STUB IShapesControl_get_Font_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][id][propput] */ HRESULT STDMETHODCALLTYPE IShapesControl_put_Caption_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [in] */ BSTR strCaption);


void __RPC_STUB IShapesControl_put_Caption_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][id][propget] */ HRESULT STDMETHODCALLTYPE IShapesControl_get_Caption_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [retval][out] */ BSTR __RPC_FAR *pstrCaption);


void __RPC_STUB IShapesControl_get_Caption_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IShapesControl_get_ShapeType_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [retval][out] */ CURRENTSHAPE __RPC_FAR *pVal);


void __RPC_STUB IShapesControl_get_ShapeType_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [requestedit][bindable][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IShapesControl_put_ShapeType_Proxy( 
    IShapesControl __RPC_FAR * This,
    /* [in] */ CURRENTSHAPE newVal);


void __RPC_STUB IShapesControl_put_ShapeType_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IShapesControl_AboutBox_Proxy( 
    IShapesControl __RPC_FAR * This);


void __RPC_STUB IShapesControl_AboutBox_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IShapesControl_INTERFACE_DEFINED__ */



#ifndef __AXSHAPESSERVERLib_LIBRARY_DEFINED__
#define __AXSHAPESSERVERLib_LIBRARY_DEFINED__

/* library AXSHAPESSERVERLib */
/* [helpstring][version][uuid] */ 


EXTERN_C const IID LIBID_AXSHAPESSERVERLib;

#ifndef ___IShapesControlEvents_DISPINTERFACE_DEFINED__
#define ___IShapesControlEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IShapesControlEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__IShapesControlEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("03FBA321-71D1-11D3-B92D-0020781238D4")
    _IShapesControlEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IShapesControlEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *QueryInterface )( 
            _IShapesControlEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [iid_is][out] */ void __RPC_FAR *__RPC_FAR *ppvObject);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *AddRef )( 
            _IShapesControlEvents __RPC_FAR * This);
        
        ULONG ( STDMETHODCALLTYPE __RPC_FAR *Release )( 
            _IShapesControlEvents __RPC_FAR * This);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfoCount )( 
            _IShapesControlEvents __RPC_FAR * This,
            /* [out] */ UINT __RPC_FAR *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetTypeInfo )( 
            _IShapesControlEvents __RPC_FAR * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo __RPC_FAR *__RPC_FAR *ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE __RPC_FAR *GetIDsOfNames )( 
            _IShapesControlEvents __RPC_FAR * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR __RPC_FAR *rgszNames,
            /* [in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID __RPC_FAR *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE __RPC_FAR *Invoke )( 
            _IShapesControlEvents __RPC_FAR * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS __RPC_FAR *pDispParams,
            /* [out] */ VARIANT __RPC_FAR *pVarResult,
            /* [out] */ EXCEPINFO __RPC_FAR *pExcepInfo,
            /* [out] */ UINT __RPC_FAR *puArgErr);
        
        END_INTERFACE
    } _IShapesControlEventsVtbl;

    interface _IShapesControlEvents
    {
        CONST_VTBL struct _IShapesControlEventsVtbl __RPC_FAR *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IShapesControlEvents_QueryInterface(This,riid,ppvObject)	\
    (This)->lpVtbl -> QueryInterface(This,riid,ppvObject)

#define _IShapesControlEvents_AddRef(This)	\
    (This)->lpVtbl -> AddRef(This)

#define _IShapesControlEvents_Release(This)	\
    (This)->lpVtbl -> Release(This)


#define _IShapesControlEvents_GetTypeInfoCount(This,pctinfo)	\
    (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo)

#define _IShapesControlEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo)

#define _IShapesControlEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)

#define _IShapesControlEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IShapesControlEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_ShapesControl;

#ifdef __cplusplus

class DECLSPEC_UUID("03FBA320-71D1-11D3-B92D-0020781238D4")
ShapesControl;
#endif

EXTERN_C const CLSID CLSID_CoCusomPage;

#ifdef __cplusplus

class DECLSPEC_UUID("C489E472-7391-11D3-B92E-0020781238D4")
CoCusomPage;
#endif
#endif /* __AXSHAPESSERVERLib_LIBRARY_DEFINED__ */

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
