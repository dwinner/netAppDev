VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "VB Tear Off Tester"
   ClientHeight    =   5265
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5265
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnOldWay 
      Caption         =   "Create with ICreateCar"
      Height          =   615
      Left            =   2160
      TabIndex        =   10
      Top             =   3600
      Width           =   2295
   End
   Begin VB.CommandButton btnStats 
      Caption         =   "Get Stats"
      Height          =   495
      Left            =   120
      TabIndex        =   9
      Top             =   3960
      Width           =   1695
   End
   Begin VB.TextBox txtMaxSpeed 
      Height          =   375
      Left            =   1800
      TabIndex        =   8
      Text            =   "0"
      Top             =   2400
      Width           =   2655
   End
   Begin VB.TextBox txtPetName 
      Height          =   375
      Left            =   1800
      TabIndex        =   7
      Text            =   "Hector"
      Top             =   1680
      Width           =   2655
   End
   Begin VB.TextBox txtOwnerAddress 
      Height          =   375
      Left            =   1800
      TabIndex        =   6
      Text            =   "123 Happy Lane"
      Top             =   960
      Width           =   2655
   End
   Begin VB.TextBox txtOwnerName 
      Height          =   375
      Left            =   1800
      TabIndex        =   5
      Text            =   "Fred"
      Top             =   240
      Width           =   2655
   End
   Begin VB.CommandButton btnCreate 
      Caption         =   "Create Car"
      Height          =   495
      Left            =   120
      TabIndex        =   0
      Top             =   3240
      Width           =   1695
   End
   Begin VB.Label Label4 
      Caption         =   "Car's Max Speed"
      Height          =   495
      Left            =   120
      TabIndex        =   4
      Top             =   2400
      Width           =   1455
   End
   Begin VB.Label Label3 
      Caption         =   "Car's Pet Name"
      Height          =   495
      Left            =   120
      TabIndex        =   3
      Top             =   1680
      Width           =   1455
   End
   Begin VB.Label Label2 
      Caption         =   "Owner Address"
      Height          =   495
      Left            =   120
      TabIndex        =   2
      Top             =   960
      Width           =   1455
   End
   Begin VB.Label Label1 
      Caption         =   "Owner Name"
      Height          =   495
      Left            =   120
      TabIndex        =   1
      Top             =   240
      Width           =   1455
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
' The new CoCar
Private mCar As ATLCoCar
Private mitfStats As ATLTEAROFFLib.IStats
Private mitfOwner As IOwnerInfo

Private Sub btnCreate_Click()
    Set mCar = New ATLCoCar
    mCar.Create txtOwnerName, txtOwnerAddress, txtPetName, txtMaxSpeed
End Sub

Private Sub btnOldWay_Click()
    Dim oldCar As New ATLCoCar
    Dim itfCCar As ICreateCar
    Set itfCCar = oldCar
    itfCCar.SetMaxSpeed 40
    itfCCar.SetPetName "JoJo"
    
    ' Set another reference to the tear-off.
    Dim itfCCar2 As ICreateCar
    Set itfCCar2 = oldCar
    itfCCar2.SetPetName "JoJo Jr."
    Set itfCCar2 = Nothing
    Set itfCCar = Nothing
    Set oldCar = Nothing
    
End Sub

Private Sub btnStats_Click()
    Set mitfStats = mCar
    mitfStats.DisplayStats
    Set mitfOwner = mCar
    MsgBox mitfOwner.Name, , "Owner Name"
    MsgBox mitfOwner.Address, , "Owner Address"
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Set mCar = Nothing
    Set mitfStats = Nothing
    Set mitfOwner = Nothing
End Sub
