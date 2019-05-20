VERSION 5.00
Object = "{65A0EEB3-745F-11D3-B92E-0020781238D4}#1.0#0"; "AXCARSERVER.DLL"
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "VB AxCar Tester"
   ClientHeight    =   4995
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5355
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4995
   ScaleWidth      =   5355
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin AXCARSERVERLibCtl.AxCar AxCar1 
      Height          =   855
      Left            =   360
      TabIndex        =   8
      Top             =   1800
      Width           =   975
      _cx             =   1720
      _cy             =   1508
      Animate         =   0
      Speed           =   50
      PetName         =   "Billy"
      MaxSpeed        =   0
   End
   Begin VB.TextBox txtMaxSpeed 
      Height          =   375
      Left            =   1560
      TabIndex        =   3
      Top             =   960
      Width           =   2175
   End
   Begin VB.TextBox txtPetName 
      Height          =   375
      Left            =   1560
      TabIndex        =   2
      Top             =   240
      Width           =   2175
   End
   Begin VB.CommandButton btnSpeedUp 
      Caption         =   "Speed Up"
      Height          =   375
      Left            =   3960
      TabIndex        =   1
      Top             =   960
      Width           =   1215
   End
   Begin VB.CommandButton btnMakeCar 
      Caption         =   "Create Car"
      Height          =   375
      Left            =   3960
      TabIndex        =   0
      Top             =   240
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "Max Speed"
      Height          =   375
      Left            =   120
      TabIndex        =   7
      Top             =   960
      Width           =   1215
   End
   Begin VB.Label Label2 
      Caption         =   "Pet Name"
      Height          =   375
      Left            =   120
      TabIndex        =   6
      Top             =   240
      Width           =   1215
   End
   Begin VB.Label lblEventMsg 
      Caption         =   "Message From Car:"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   18
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000FF&
      Height          =   1815
      Left            =   360
      TabIndex        =   5
      Top             =   2880
      Width           =   4695
   End
   Begin VB.Label lblCurrSpeed 
      Caption         =   "Current Speed:"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   13.5
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   495
      Left            =   1560
      TabIndex        =   4
      Top             =   1680
      Width           =   3255
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

' Event sinks.
Private Sub AxCar1_AboutToBlow()
    lblEventMsg.Caption = "Message From Car:" & vbLf & "Your car is about to blow up, careful!"
End Sub

Private Sub AxCar1_BlewUp()
  lblEventMsg.Caption = "Message From Car:" & vbLf & "Your car has EXPLODED!"
  btnSpeedUp.Enabled = False
End Sub

' Create the car and start animation.
Private Sub btnMakeCar_Click()
    AxCar1.CreateCar txtPetName, txtMaxSpeed
    AxCar1.Animate = Yes
    btnMakeCar.Enabled = False
End Sub

' Speed up.  This will adjust the images you see
' based on the current max frame.
Private Sub btnSpeedUp_Click()
    AxCar1.SpeedUp
    lblCurrSpeed.Caption = "Current Speed: " & AxCar1.Speed
End Sub

' Show control defaults in edit boxes when starting
Private Sub Form_Load()
    txtPetName = AxCar1.petName
    txtMaxSpeed = AxCar1.MaxSpeed
End Sub
