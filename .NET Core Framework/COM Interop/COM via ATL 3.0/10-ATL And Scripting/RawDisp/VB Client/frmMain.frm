VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Raw IDispatch VB Tester"
   ClientHeight    =   1410
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4110
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1410
   ScaleWidth      =   4110
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnUseIDisp 
      Caption         =   "Use IDispatch"
      Height          =   495
      Left            =   2160
      TabIndex        =   1
      Top             =   360
      Width           =   1695
   End
   Begin VB.CommandButton btnGetIDisp 
      Caption         =   "Get IDispatch"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   1695
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' This will hold the IDispatch
Dim o As Object

Private Sub Form_Load()
    btnUseIDisp.Enabled = False
End Sub

Private Sub btnGetIDisp_Click()
    ' Go get IDispatch from the coclass
    Set o = CreateObject("RawDisp.CoSquiggle")
    btnUseIDisp.Enabled = True
End Sub

Private Sub btnUseIDisp_Click()
    On Error GoTo OOPS
    
    ' Call GetIDsOfNames and Invoke foe each.
    o.DrawASquiggle
    o.FlipASquiggle
    o.EraseASquiggle
    o.InvertACircle  '???? This will trigger an error!
Exit Sub

OOPS:
    ' Catch error here.
    MsgBox Err.Description, , "Error when using CoSquiggle's IDispatch!"
    Resume Next
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set o = Nothing
End Sub
