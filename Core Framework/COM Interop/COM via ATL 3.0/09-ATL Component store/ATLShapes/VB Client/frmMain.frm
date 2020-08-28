VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "VB Shapes Tester"
   ClientHeight    =   4170
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5295
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4170
   ScaleWidth      =   5295
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnGetLineOSB 
      Caption         =   "Get Line's OS Buffer"
      Height          =   495
      Left            =   240
      TabIndex        =   7
      Top             =   1080
      Width           =   1935
   End
   Begin VB.CommandButton btnGetHexOSB 
      Caption         =   "Get Hex's OS Buffer"
      Height          =   495
      Left            =   240
      TabIndex        =   6
      Top             =   3240
      Width           =   1935
   End
   Begin VB.TextBox txtHex 
      Height          =   495
      Left            =   2880
      TabIndex        =   3
      Top             =   2880
      Width           =   1815
   End
   Begin VB.TextBox txtLine 
      Height          =   495
      Left            =   2760
      TabIndex        =   2
      Top             =   720
      Width           =   1815
   End
   Begin VB.CommandButton btnGetHex 
      Caption         =   "Make A Hex"
      Height          =   495
      Left            =   240
      TabIndex        =   1
      Top             =   2520
      Width           =   1935
   End
   Begin VB.CommandButton btnGetLine 
      Caption         =   "Make A Line"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   1935
   End
   Begin VB.Label Label2 
      Caption         =   "Name of your new CoHexagon"
      Height          =   255
      Left            =   2880
      TabIndex        =   5
      Top             =   2400
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "Name of your new CoLine"
      Height          =   255
      Left            =   2760
      TabIndex        =   4
      Top             =   360
      Width           =   2535
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private mHex As CoHexagon
Private mLine As CoLine
Private osb As OSBuffer
Private itfID As IShapeID

Private Sub btnGetHex_Click()
    Set mHex = New CoHexagon
    mHex.Draw
    Set itfID = mLine
    itfID.ShapeName = txtHex.Text
    MsgBox itfID.ShapeName, , "New Hex"
End Sub

Private Sub btnGetHexOSB_Click()
    Set osb = mHex.GetOSBuffer
    osb.RenderToMemory
    Set osb = Nothing
End Sub

Private Sub btnGetLine_Click()
    Set mLine = New CoLine
    mLine.Draw
    Set itfID = mLine
    itfID.ShapeName = txtLine.Text
    MsgBox itfID.ShapeName, , "New Line"
End Sub

Private Sub btnGetLineOSB_Click()
    Set osb = mLine.GetOSBuffer
    osb.RenderToMemory
    Set osb = Nothing
End Sub

