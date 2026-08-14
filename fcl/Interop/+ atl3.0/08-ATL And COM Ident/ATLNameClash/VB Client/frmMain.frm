VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "ATL Name Clash"
   ClientHeight    =   1245
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3285
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1245
   ScaleWidth      =   3285
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btn3D 
      Caption         =   "3D"
      Height          =   495
      Left            =   2040
      TabIndex        =   1
      Top             =   360
      Width           =   855
   End
   Begin VB.CommandButton btn2D 
      Caption         =   "2D"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   855
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private obj As New Co3DHexagon

Private Sub btn2D_Click()
    obj.Draw
    obj.AnotherIDraw
End Sub

Private Sub btn3D_Click()
    Dim i As I3DRender
    Set i = obj
    i.Draw
    i.Another3dRender
End Sub
