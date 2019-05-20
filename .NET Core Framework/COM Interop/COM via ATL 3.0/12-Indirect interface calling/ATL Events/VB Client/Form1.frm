VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "As The years go by..."
   ClientHeight    =   3195
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   6375
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3195
   ScaleWidth      =   6375
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command1 
      Caption         =   "Make Kid"
      Height          =   615
      Left            =   3360
      TabIndex        =   5
      Top             =   480
      Width           =   1815
   End
   Begin VB.ListBox List1 
      Height          =   1035
      Left            =   240
      TabIndex        =   4
      Top             =   1680
      Width           =   5415
   End
   Begin VB.TextBox Text2 
      Height          =   495
      Left            =   1320
      TabIndex        =   3
      Text            =   "1"
      Top             =   840
      Width           =   1815
   End
   Begin VB.TextBox Text1 
      Height          =   495
      Left            =   1320
      TabIndex        =   1
      Text            =   "Fred"
      Top             =   120
      Width           =   1815
   End
   Begin VB.Timer Timer1 
      Interval        =   500
      Left            =   5640
      Top             =   960
   End
   Begin VB.Label Label2 
      Caption         =   "Kids's Age"
      Height          =   495
      Left            =   120
      TabIndex        =   2
      Top             =   840
      Width           =   1095
   End
   Begin VB.Label Label1 
      Caption         =   "Kids's Name"
      Height          =   495
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   1095
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim WithEvents kid As ATLEVENTSLib.CoChild
Attribute kid.VB_VarHelpID = -1

Private Sub Command1_Click()
' Create the kid.
' NOTE!  You cannot combine the 'withevents'
' and 'new' keyword in the declaration!
' Must 'new' after...

    Set kid = New CoChild
    
    ' Set kid attributes.
    kid.Name = Text1.Text
    kid.Age = CInt(Text2.Text)
    
    'tell user.
    MsgBox "Name is: " & kid.Name & vbLf & "Age is: " & _
           kid.Age, , "Making a new Kid!"
           
    ' enable timer.
    Timer1.Enabled = True
    
    'disable button
    Command1.Enabled = False
End Sub

Private Sub Form_Load()
Timer1.Enabled = False
End Sub

Private Sub kid_ChildSays(ByVal msg As String)
    ' The responce to the question is...
    Dim s As String
    s = kid.Name & " is " & kid.Age & _
        " and says: " & msg
        
    List1.AddItem s, 0  ' Add new string to top of listbox.
            
End Sub


Private Sub Timer1_Timer()
    ' Ask kid a question.
    kid.AskChildQuestion
    
    ' Raise the kids age.
    kid.Age = kid.Age + 1
    
    Text2 = kid.Age
    
End Sub
