VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Raw Connection Tester"
   ClientHeight    =   1230
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3945
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1230
   ScaleWidth      =   3945
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command2 
      Caption         =   "Trigger Event"
      Height          =   615
      Left            =   2040
      TabIndex        =   1
      Top             =   240
      Width           =   1575
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Make Object"
      Height          =   615
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   1575
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private WithEvents rawObj As RawConnServer.CoSomeObject
Attribute rawObj.VB_VarHelpID = -1

Private Sub Command1_Click()
    Set rawObj = New CoSomeObject
    Command2.Enabled = True
    Command1.Enabled = False
End Sub

Private Sub Command2_Click()
    rawObj.TriggerEvent
End Sub

Private Sub Form_Load()
    Command2.Enabled = False
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set rawObj = Nothing
End Sub

Private Sub rawObj_Test()
    MsgBox "The object just called me!", , "Wow!  Cool man..."
End Sub
