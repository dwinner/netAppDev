#ifndef _AXSHAPESSERVERCP_H_
#define _AXSHAPESSERVERCP_H_

template <class T>
class CProxy_IShapesControlEvents : public IConnectionPointImpl<T, &DIID__IShapesControlEvents, CComDynamicUnkArray>
{
	//Warning this class may be recreated by the wizard.
public:
	HRESULT Fire_ClickedControl(SHORT X, SHORT Y)
	{
		CComVariant varResult;
		T* pT = static_cast<T*>(this);
		int nConnectionIndex;
		CComVariant* pvars = new CComVariant[2];
		int nConnections = m_vec.GetSize();
		
		for (nConnectionIndex = 0; nConnectionIndex < nConnections; nConnectionIndex++)
		{
			pT->Lock();
			CComPtr<IUnknown> sp = m_vec.GetAt(nConnectionIndex);
			pT->Unlock();
			IDispatch* pDispatch = reinterpret_cast<IDispatch*>(sp.p);
			if (pDispatch != NULL)
			{
				VariantClear(&varResult);
				pvars[1] = X;
				pvars[0] = Y;
				DISPPARAMS disp = { pvars, NULL, 2, 0 };
				pDispatch->Invoke(0x1, IID_NULL, LOCALE_USER_DEFAULT, DISPATCH_METHOD, &disp, &varResult, NULL, NULL);
			}
		}
		delete[] pvars;
		return varResult.scode;
	
	}
};
#endif