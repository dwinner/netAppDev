VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Raw Name Clash"
   ClientHeight    =   2505
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   2490
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2505
   ScaleWidth      =   2490
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btn3D 
      Caption         =   "Draw in 3D"
      Height          =   495
      Left            =   360
      TabIndex        =   2
      Top             =   1680
      Width           =   1695
   End
   Begin VB.CommandButton btn2D 
      Caption         =   "Draw in 2D"
      Height          =   495
      Left            =   360
      TabIndex        =   1
      Top             =   960
      Width           =   1695
   End
   Begin VB.CommandButton btnCreate 
      Caption         =   "Create 3D Hex"
      Height          =   495
      Left            =   360
      TabIndex        =   0
      Top             =   240
      Width           =   1695
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private m3DHex As Co3DHexagon
Private mitf3D As I3DRender

Private Sub btn2D_Click()
    m3DHex.Draw
End Sub

Private Sub btn3D_Click()
    Set mitf3D = m3DHex
    mitf3D.Draw
    Set mitf3D = Nothing
End Sub

Private Sub btnCreate_Click()
    Set m3DHex = New Co3DHexagon
    btn2D.Enabled = True
    btn3D.Enabled = True
    btnCreate.Enabled = False
End Sub

Private Sub Form_Load()
    btn2D.Enabled = False
    btn3D.Enabled = False
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set m3DHex = Nothing
End Sub
