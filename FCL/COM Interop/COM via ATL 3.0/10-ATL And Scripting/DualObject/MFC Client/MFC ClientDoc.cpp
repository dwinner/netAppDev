// MFC ClientDoc.cpp : implementation of the CMFCClientDoc class
//

#include "stdafx.h"
#include "MFC Client.h"

#include "MFC ClientDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMFCClientDoc

IMPLEMENT_DYNCREATE(CMFCClientDoc, CDocument)

BEGIN_MESSAGE_MAP(CMFCClientDoc, CDocument)
	//{{AFX_MSG_MAP(CMFCClientDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

BEGIN_DISPATCH_MAP(CMFCClientDoc, CDocument)
	//{{AFX_DISPATCH_MAP(CMFCClientDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//      DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_DISPATCH_MAP
END_DISPATCH_MAP()

// Note: we add support for IID_IMFCClient to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .ODL file.

// {FDC42191-7837-11D3-A7DE-0000E885A202}
static const IID IID_IMFCClient =
{ 0xfdc42191, 0x7837, 0x11d3, { 0xa7, 0xde, 0x0, 0x0, 0xe8, 0x85, 0xa2, 0x2 } };

BEGIN_INTERFACE_MAP(CMFCClientDoc, CDocument)
	INTERFACE_PART(CMFCClientDoc, IID_IMFCClient, Dispatch)
END_INTERFACE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMFCClientDoc construction/destruction

CMFCClientDoc::CMFCClientDoc()
{
	// TODO: add one-time construction code here

	EnableAutomation();

	AfxOleLockApp();
}

CMFCClientDoc::~CMFCClientDoc()
{
	AfxOleUnlockApp();
}

BOOL CMFCClientDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CMFCClientDoc serialization

void CMFCClientDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CMFCClientDoc diagnostics

#ifdef _DEBUG
void CMFCClientDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CMFCClientDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CMFCClientDoc commands
