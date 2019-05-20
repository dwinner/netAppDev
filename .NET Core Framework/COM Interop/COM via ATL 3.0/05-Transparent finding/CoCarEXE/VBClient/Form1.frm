VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Local EXE Tester"
   ClientHeight    =   1050
   ClientLeft      =   2010
   ClientTop       =   1725
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1050
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command1 
      Caption         =   "Do It"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   4095
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()
    Dim o As New CoCar
    o.SetPetName "Fred"
    Dim itfStats As IStats
    Set itfStats = o
    itfStats.DisplayStats
    Set itfStats = Nothing
    Set o = Nothing
End Sub
