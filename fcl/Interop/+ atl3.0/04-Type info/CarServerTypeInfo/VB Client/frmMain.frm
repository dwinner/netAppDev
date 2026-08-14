VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "VB CoCar Tester..."
   ClientHeight    =   3060
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   6195
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3060
   ScaleWidth      =   6195
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.ListBox lstSpeed 
      Height          =   450
      Left            =   2760
      TabIndex        =   7
      Top             =   2280
      Width           =   3135
   End
   Begin VB.CommandButton btnSpeed 
      Caption         =   "&Speed Up"
      Height          =   375
      Left            =   240
      TabIndex        =   4
      Top             =   2520
      Width           =   1935
   End
   Begin VB.CommandButton btnStats 
      Caption         =   "Get &Stats"
      Height          =   375
      Left            =   240
      TabIndex        =   3
      Top             =   1920
      Width           =   1935
   End
   Begin VB.TextBox txtMaxSpeed 
      Height          =   495
      Left            =   2520
      TabIndex        =   2
      Top             =   960
      Width           =   1335
   End
   Begin VB.TextBox txtName 
      Height          =   495
      Left            =   2520
      TabIndex        =   1
      Top             =   240
      Width           =   1335
   End
   Begin VB.CommandButton btnCreate 
      Caption         =   "&Create a Car"
      Height          =   375
      Left            =   4200
      TabIndex        =   0
      Top             =   600
      Width           =   1575
   End
   Begin VB.Label Label3 
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2760
      TabIndex        =   8
      Top             =   1920
      Width           =   3135
   End
   Begin VB.Label Label2 
      Caption         =   "Max Speed"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   120
      TabIndex        =   6
      Top             =   960
      Width           =   1935
   End
   Begin VB.Label Label1 
      Caption         =   "Pet Name "
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   120
      TabIndex        =   5
      Top             =   240
      Width           =   1215
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' The coclass exposing ICreateCar
Private mCar As CarServer.CoCar

' Extra interfaces on the cocar
Private itfEngine As CarServer.IEngine
Private itfStats As CarServer.IStats

' Create the car enable other buttons
Private Sub btnCreate_Click()
    Set mCar = New CoCar
    
    ' Set car via ICreateCar
    mCar.SetMaxSpeed CLng(txtMaxSpeed)
    mCar.SetPetName txtName
    
    btnStats.Enabled = True
    btnSpeed.Enabled = True
    
    ' Get IStats
    Set itfStats = mCar
    Label3.Caption = "Speed of: " & itfStats.GetPetName
    Set itfStats = Nothing
End Sub

Private Sub btnStats_Click()
    ' Get IStats & extract coclass state
    Set itfStats = mCar
    itfStats.DisplayStats
    Set itfStats = Nothing
End Sub

Private Sub btnSpeed_Click()
    Set itfEngine = mCar
    Set itfStats = mCar
    
    ' Rev that engine!
    Do While itfEngine.GetCurSpeed <= itfEngine.GetMaxSpeed
         itfEngine.SpeedUp
         lstSpeed.AddItem itfEngine.GetCurSpeed
    Loop
    
    ' Tell user car is dead.
    MsgBox itfStats.GetPetName & " has blown up!  Lead foot!"
        
    ' Release all references to cocar.
    Set mCar = Nothing
    Set itfEngine = Nothing
    Set itfStats = Nothing
    
    btnSpeed.Enabled = False
    btnStats.Enabled = False
End Sub

Private Sub Form_Load()
    ' Prep the form
    txtName = "No Name"
    txtMaxSpeed = 10
    btnSpeed.Enabled = False
    btnStats.Enabled = False
End Sub

