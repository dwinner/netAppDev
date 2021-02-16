VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Collection Tester Version 1"
   ClientHeight    =   2910
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3795
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2910
   ScaleWidth      =   3795
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnDraw 
      Caption         =   "Draw"
      Height          =   375
      Left            =   2280
      TabIndex        =   3
      Top             =   2160
      Width           =   1335
   End
   Begin VB.CommandButton btnGet 
      Caption         =   "Get Them!"
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   2160
      Width           =   1335
   End
   Begin VB.ListBox List1 
      Height          =   1230
      Left            =   120
      TabIndex        =   0
      Top             =   600
      Width           =   3375
   End
   Begin VB.Label Label1 
      Caption         =   "Here are all the Squiggles"
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   3615
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private o As CoSquiggles

Private Sub btnDraw_Click()
    ' Draw the selected squiggle.
    Dim i As Integer
    o.Item(List1.ListIndex).Draw
End Sub

Private Sub btnGet_Click()
    ' Get all squiggles and put the name in
    ' the list box.
    Set o = New CoSquiggles
    Dim s As CoSquiggle
    For Each s In o
        List1.AddItem s.Name
    Next
End Sub

