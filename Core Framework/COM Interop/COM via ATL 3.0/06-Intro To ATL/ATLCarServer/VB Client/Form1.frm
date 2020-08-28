VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "The Classic VB COM Tester"
   ClientHeight    =   3195
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command1 
      Caption         =   "DO IT"
      Height          =   2775
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   4335
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()
    Dim o As New ATLCoCar
    o.SetMaxSpeed 20
    o.SetPetName "Lisa"
    Dim itfOwner As IOwnerInfo
    Set itfOwner = o
    itfOwner.Name = "Manu"
    itfOwner.Address = "123 456 Lane"
    Dim itfEngine As IEngine
    Set itfEngine = o
    MsgBox itfEngine.GetCurSpeed, , "Current speed"
    MsgBox itfEngine.GetMaxSpeed, , "Max speed"
End Sub
