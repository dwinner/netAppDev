VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "VB Window Driver!"
   ClientHeight    =   3795
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7590
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3795
   ScaleWidth      =   7590
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame1 
      Caption         =   "Color Selection"
      Height          =   1815
      Left            =   5040
      TabIndex        =   13
      Top             =   240
      Width           =   1575
      Begin VB.OptionButton OptionBlue 
         Caption         =   "Blue"
         Height          =   375
         Left            =   120
         TabIndex        =   16
         Top             =   1320
         Width           =   1335
      End
      Begin VB.OptionButton OptionGreen 
         Caption         =   "Green"
         Height          =   375
         Left            =   120
         TabIndex        =   15
         Top             =   840
         Width           =   1215
      End
      Begin VB.OptionButton OptionRed 
         Caption         =   "Red"
         Height          =   375
         Left            =   120
         TabIndex        =   14
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.TextBox txtRight 
      Height          =   375
      Left            =   5520
      TabIndex        =   8
      Text            =   "0"
      Top             =   3120
      Width           =   1095
   End
   Begin VB.TextBox txtBottom 
      Height          =   375
      Left            =   5520
      TabIndex        =   7
      Text            =   "0"
      Top             =   2520
      Width           =   1095
   End
   Begin VB.TextBox txtLeft 
      Height          =   375
      Left            =   3240
      TabIndex        =   6
      Text            =   "0"
      Top             =   3120
      Width           =   1095
   End
   Begin VB.TextBox txtTop 
      Height          =   375
      Left            =   3240
      TabIndex        =   5
      Text            =   "0"
      Top             =   2520
      Width           =   1095
   End
   Begin VB.CommandButton btnDraw 
      Caption         =   "Draw"
      Height          =   495
      Left            =   240
      TabIndex        =   4
      Top             =   2400
      Width           =   1575
   End
   Begin VB.TextBox Text1 
      Height          =   495
      Left            =   2640
      TabIndex        =   3
      Top             =   1440
      Width           =   1815
   End
   Begin VB.CommandButton btnKill 
      Caption         =   "Kill Window"
      Height          =   495
      Left            =   1800
      TabIndex        =   1
      Top             =   360
      Width           =   1335
   End
   Begin VB.CommandButton btnLoad 
      Caption         =   "Load Window"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   1335
   End
   Begin VB.Label Label5 
      Caption         =   "Right"
      Height          =   255
      Left            =   4560
      TabIndex        =   12
      Top             =   3120
      Width           =   735
   End
   Begin VB.Label Label4 
      Caption         =   "Bottom"
      Height          =   375
      Left            =   4560
      TabIndex        =   11
      Top             =   2520
      Width           =   735
   End
   Begin VB.Label Label3 
      Caption         =   "Left"
      Height          =   375
      Left            =   2280
      TabIndex        =   10
      Top             =   3120
      Width           =   615
   End
   Begin VB.Label Label2 
      Caption         =   "Top"
      Height          =   255
      Left            =   2280
      TabIndex        =   9
      Top             =   2520
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "Change Window Text here"
      Height          =   375
      Left            =   360
      TabIndex        =   2
      Top             =   1440
      Width           =   2175
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private w As ATLWINDOWEXAMPLELib.CoWindow

Private Sub btnDraw_Click()
    w.DrawCircle CInt(txtTop), CInt(txtLeft), _
                              CInt(txtBottom), CInt(txtRight)
End Sub

Private Sub btnKill_Click()
    w.KillMyWindow
    btnLoad.Enabled = True
    btnKill.Enabled = False
End Sub

Private Sub btnLoad_Click()
    w.CreateMyWindow
    btnLoad.Enabled = False
    btnKill.Enabled = True
End Sub

Private Sub Form_Load()
    Set w = New CoWindow
    btnLoad.Enabled = True
    btnKill.Enabled = False
End Sub


Private Sub OptionBlue_Click()
    Dim c As Long
    c = RGB(0, 0, 255)
    w.ChangeTheColor c
End Sub

Private Sub OptionGreen_Click()
    Dim c As Long
    c = RGB(0, 255, 0)
    w.ChangeTheColor c
End Sub

Private Sub OptionRed_Click()
    Dim c As Long
    c = RGB(255, 0, 0)
    w.ChangeTheColor c
End Sub

Private Sub Text1_Change()
    w.ChangeWindowText Text1.Text
End Sub
