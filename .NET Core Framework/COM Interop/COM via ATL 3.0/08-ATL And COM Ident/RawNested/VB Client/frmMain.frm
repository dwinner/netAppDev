VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Testing the nested classes..."
   ClientHeight    =   2595
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4470
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2595
   ScaleWidth      =   4470
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnID 
      Caption         =   "Get ID of the Hex"
      Height          =   495
      Left            =   2280
      TabIndex        =   4
      Top             =   1320
      Width           =   1935
   End
   Begin VB.CommandButton btnDraw 
      Caption         =   "Draw the Hex"
      Height          =   495
      Left            =   240
      TabIndex        =   3
      Top             =   1800
      Width           =   1695
   End
   Begin VB.TextBox txtID 
      Height          =   375
      Left            =   2040
      TabIndex        =   2
      Text            =   "1"
      Top             =   240
      Width           =   2055
   End
   Begin VB.CommandButton btnCreate 
      Caption         =   "Create the Hex"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   1080
      Width           =   1695
   End
   Begin VB.Label Label1 
      Caption         =   "ID of this Hexagon"
      Height          =   375
      Left            =   240
      TabIndex        =   1
      Top             =   240
      Width           =   1575
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mTheHex As CoHexagon
Private mItfShapeID As IShapeID

Private Sub btnCreate_Click()
    ' Make the Hex.
    Set mTheHex = New CoHexagon
    ' Set ID.
    Set mItfShapeID = mTheHex
    mItfShapeID.ID = CInt(txtID.Text)
    
    ' Enable buttons
    btnDraw.Enabled = True
    btnID.Enabled = True
End Sub

Private Sub btnDraw_Click()
    ' draw the hex.
    mTheHex.Draw
End Sub

Private Sub btnID_Click()
    ' Echo ID.
    MsgBox mItfShapeID.ID, , "The ID is..."
End Sub

Private Sub Form_Load()
    btnDraw.Enabled = False
    btnID.Enabled = False
End Sub
