VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Catch the error"
   ClientHeight    =   1470
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3555
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1470
   ScaleWidth      =   3555
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnDoIt 
      Caption         =   "Make it so..."
      Height          =   615
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   2895
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub btnDoIt_Click()
On Error GoTo OOPS

    ' Create a new car
    Dim o As New CoCarErrors
    o.SetMaxSpeed 30
    ' Get IEngine interface.
    Dim i As IEngine
    Set i = o
    
    ' This will kill the car for sure!
    i.SpeedUp
    i.SpeedUp
    i.SpeedUp
    i.SpeedUp
    i.SpeedUp
    i.SpeedUp

Exit Sub

' Error trap
OOPS:
    MsgBox Err.Description, , "Message from the Car"
End Sub


