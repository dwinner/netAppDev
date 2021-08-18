VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Collection Tester"
   ClientHeight    =   6075
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3765
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6075
   ScaleWidth      =   3765
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnAdd 
      Caption         =   "Add This Squiggle"
      Height          =   375
      Left            =   120
      TabIndex        =   6
      Top             =   3720
      Width           =   1815
   End
   Begin VB.TextBox txtName 
      Height          =   375
      Left            =   1080
      TabIndex        =   4
      Top             =   3120
      Width           =   2175
   End
   Begin VB.CommandButton btnDraw 
      Caption         =   "Draw"
      Height          =   375
      Left            =   2280
      TabIndex        =   3
      Top             =   2160
      Width           =   1335
   End
   Begin VB.CommandButton btnGet 
      Caption         =   "Get Them!"
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   2160
      Width           =   1335
   End
   Begin VB.ListBox List1 
      Height          =   1230
      Left            =   120
      TabIndex        =   0
      Top             =   600
      Width           =   3375
   End
   Begin VB.Label Label3 
      Alignment       =   2  'Center
      Caption         =   "Double click on an entry to remove a squiggle."
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   18
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   1455
      Left            =   120
      TabIndex        =   7
      Top             =   4560
      Width           =   3495
   End
   Begin VB.Line Line2 
      X1              =   120
      X2              =   3600
      Y1              =   4320
      Y2              =   4320
   End
   Begin VB.Label Label2 
      Caption         =   "Name:"
      Height          =   255
      Left            =   120
      TabIndex        =   5
      Top             =   3120
      Width           =   855
   End
   Begin VB.Line Line1 
      X1              =   120
      X2              =   3600
      Y1              =   2760
      Y2              =   2760
   End
   Begin VB.Label Label1 
      Caption         =   "You Have NO squiggles"
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   120
      Width           =   3615
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private coll As BETTERSQUIGGLECOLLECTIONLib.SquiggleCollection2

Private Sub btnAdd_Click()
    If txtName.Text <> "" Then
        Dim o As New CoSquiggle
        o.Name = txtName
        coll.Add o
        RefreshGUI
    Else
        MsgBox "You did not enter a name for this squiggle..."
    End If
End Sub

Private Sub btnDraw_Click()
    ' Draw the selected squiggle.
    Dim i As Integer
    If List1.ListIndex <> -1 Then
        coll.Item(List1.ListIndex).Draw
    End If
End Sub

Private Sub btnGet_Click()
    ' Get all squiggles and put the name in
    ' the list box.
    RefreshGUI
End Sub

Private Sub Form_Load()
    Set coll = New SquiggleCollection2
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set coll = Nothing
End Sub

Private Sub List1_DblClick()
    ' Remove the one they selected.
    coll.Remove List1.ListIndex
    RefreshGUI
End Sub

' A helper function to refresh the listbox.
Private Sub RefreshGUI()
    List1.Clear
    Dim s As CoSquiggle
    For Each s In coll
        List1.AddItem s.Name
    Next
    Label1.Caption = "You Have " & coll.Count & " squiggles."
End Sub

