// MFC ClientView.h : interface of the CMFCClientView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_MFCCLIENTVIEW_H__FDC4219D_7837_11D3_A7DE_0000E885A202__INCLUDED_)
#define AFX_MFCCLIENTVIEW_H__FDC4219D_7837_11D3_A7DE_0000E885A202__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CMFCClientView : public CView
{
protected: // create from serialization only
	CMFCClientView();
	DECLARE_DYNCREATE(CMFCClientView)

// Attributes
public:
	CMFCClientDoc* GetDocument();

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMFCClientView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CMFCClientView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CMFCClientView)
	afx_msg void OnSquig();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in MFC ClientView.cpp
inline CMFCClientDoc* CMFCClientView::GetDocument()
   { return (CMFCClientDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MFCCLIENTVIEW_H__FDC4219D_7837_11D3_A7DE_0000E885A202__INCLUDED_)
