VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Late and Early Binding"
   ClientHeight    =   2040
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   2925
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2040
   ScaleWidth      =   2925
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnEarly 
      Caption         =   "Get ISquiggle"
      Height          =   495
      Left            =   360
      TabIndex        =   1
      Top             =   1080
      Width           =   2175
   End
   Begin VB.CommandButton btnLate 
      Caption         =   "Get IDispatch"
      Height          =   495
      Left            =   360
      TabIndex        =   0
      Top             =   240
      Width           =   2175
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim objLate As Object
Dim objEarly As CoDualSquiggleTI

Private Sub btnEarly_Click()
    Set objEarly = New CoDualSquiggleTI
    objEarly.DrawASquiggle
    objEarly.EraseASquiggle
    objEarly.FlipASquiggle
    objEarly.Explode
    Set objEarly = Nothing
End Sub

Private Sub btnLate_Click()
    Set objLate = CreateObject("DualObjectWithTypeInfo.CoDualSquiggleTI")
    objLate.DrawASquiggle
    objLate.EraseASquiggle
    objLate.FlipASquiggle
    objLate.Explode
    Set objLate = Nothing
End Sub

