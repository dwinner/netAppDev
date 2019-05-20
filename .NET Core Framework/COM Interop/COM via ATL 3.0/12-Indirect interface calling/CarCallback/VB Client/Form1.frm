VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Callback Tester"
   ClientHeight    =   1500
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   2850
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1500
   ScaleWidth      =   2850
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnBurnRubber 
      Caption         =   "Burn Rubber"
      Height          =   495
      Left            =   480
      TabIndex        =   0
      Top             =   840
      Width           =   1935
   End
   Begin VB.Label Label1 
      Caption         =   "Curr Speed:"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   2535
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' This client implements the callback interface
Implements IEngineEvents

Private i As IEstablishCommunications
Private o As New CoCarCallBack

Private Sub btnBurnRubber_Click()
    Dim e As IEngine
    Set e = o
    e.SpeedUp
    Label1.Caption = "Curr Speed: " & e.GetCurSpeed
End Sub

Private Sub IEngineEvents_AboutToExplode()
    MsgBox "Prepair to meet they doom..."
End Sub

Private Sub IEngineEvents_Exploded()
        MsgBox "Your car has exploded!"
End Sub

Private Sub Form_Load()
    o.SetMaxSpeed 80
    o.SetPetName "Brenner"
    Set i = o
    i.Advise Me
End Sub

Private Sub Form_Unload(Cancel As Integer)
    i.Unadvise
End Sub
