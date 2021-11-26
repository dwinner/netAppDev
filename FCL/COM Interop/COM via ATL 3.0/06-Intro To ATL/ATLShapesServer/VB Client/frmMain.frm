VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "ATL Shapes Tester"
   ClientHeight    =   2610
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3210
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2610
   ScaleWidth      =   3210
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.CommandButton Command4 
      Caption         =   "IErase"
      Height          =   375
      Left            =   840
      TabIndex        =   3
      Top             =   2040
      Width           =   1695
   End
   Begin VB.CommandButton Command3 
      Caption         =   "IShapeID"
      Height          =   375
      Left            =   840
      TabIndex        =   2
      Top             =   1440
      Width           =   1695
   End
   Begin VB.CommandButton Command2 
      Caption         =   "IShapeEdit"
      Height          =   375
      Left            =   840
      TabIndex        =   1
      Top             =   840
      Width           =   1695
   End
   Begin VB.CommandButton Command1 
      Caption         =   "IDraw"
      Height          =   375
      Left            =   840
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
Private o As New CoHexagon

Private Sub Command1_Click()
    o.Draw
End Sub

Private Sub Command2_Click()
    Dim iSE As IShapeEdit
    Set iSE = o
    iSE.Fill POLKADOT
    iSE.Inverse
    iSE.Stretch 3
End Sub

Private Sub Command3_Click()
    Dim isID As IShapeID
    Set isID = o
    isID.Name = "Snoopy"
    MsgBox isID.Name
End Sub

Private Sub Command4_Click()
    Dim iE As IErase
    Set iE = o
    iE.Erase
End Sub
