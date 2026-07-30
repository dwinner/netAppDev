VERSION 5.00
Object = "{03FBA313-71D1-11D3-B92D-0020781238D4}#1.0#0"; "AXSHAPESSERVER.DLL"
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "VB ShapesControl Tester"
   ClientHeight    =   5160
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4680
   BeginProperty Font 
      Name            =   "MS Sans Serif"
      Size            =   18
      Charset         =   0
      Weight          =   700
      Underline       =   0   'False
      Italic          =   -1  'True
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5160
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command1 
      Caption         =   "?"
      Height          =   735
      Left            =   3120
      TabIndex        =   8
      Top             =   2880
      Width           =   1335
   End
   Begin VB.TextBox Text1 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   240
      TabIndex        =   7
      Top             =   4320
      Width           =   3975
   End
   Begin VB.Frame Frame1 
      Caption         =   "Shape?"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1815
      Left            =   2400
      TabIndex        =   3
      Top             =   240
      Width           =   2055
      Begin VB.OptionButton Option3 
         Caption         =   "Rectangle"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   435
         Left            =   120
         TabIndex        =   6
         Top             =   1200
         Width           =   1575
      End
      Begin VB.OptionButton Option2 
         Caption         =   "Square"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   435
         Left            =   120
         TabIndex        =   5
         Top             =   780
         Width           =   1575
      End
      Begin VB.OptionButton Option1 
         Caption         =   "Circle"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   435
         Left            =   120
         TabIndex        =   4
         Top             =   360
         Width           =   1815
      End
   End
   Begin AXSHAPESSERVERLibCtl.ShapesControl ShapesControl1 
      Height          =   1575
      Left            =   240
      TabIndex        =   0
      Top             =   240
      Width           =   1935
      _cx             =   3413
      _cy             =   2778
      BackColor       =   65280
      Caption         =   "COM!"
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Verdana"
         Size            =   14.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ShapeType       =   1
   End
   Begin VB.Label LabelY 
      Caption         =   "Y Pos:"
      Height          =   495
      Left            =   120
      TabIndex        =   2
      Top             =   3360
      Width           =   2655
   End
   Begin VB.Label LabelX 
      Caption         =   "X Pos:"
      Height          =   495
      Left            =   120
      TabIndex        =   1
      Top             =   2520
      Width           =   2535
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()
    ShapesControl1.AboutBox
End Sub

Private Sub Option1_Click()
    ShapesControl1.ShapeType = shapeCIRCLE
End Sub

Private Sub Option2_Click()
    ShapesControl1.ShapeType = shapeSQUARE
End Sub

Private Sub Option3_Click()
    ShapesControl1.ShapeType = shapeRECTANGLE
End Sub

Private Sub ShapesControl1_ClickedControl(ByVal X As Integer, ByVal Y As Integer)
    LabelX.Caption = "X Pos: " & X
    LabelY.Caption = "Y Pos: " & Y
End Sub

Private Sub Text1_Change()
    ShapesControl1.Caption = Text1.Text
End Sub
