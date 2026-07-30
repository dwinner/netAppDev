VERSION 5.00
Begin VB.Form frmMain 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Creating Cars"
   ClientHeight    =   3870
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   3240
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3870
   ScaleWidth      =   3240
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnHotRod 
      Caption         =   "Make a HotRod"
      Height          =   615
      Left            =   240
      TabIndex        =   3
      Top             =   2040
      Width           =   2775
   End
   Begin VB.CommandButton btnJeep 
      Caption         =   "Make a Jeep"
      Height          =   615
      Left            =   240
      TabIndex        =   2
      Top             =   1080
      Width           =   2775
   End
   Begin VB.CommandButton btnDragRace 
      Caption         =   "Drag Race"
      Height          =   615
      Left            =   240
      TabIndex        =   1
      Top             =   3000
      Width           =   2775
   End
   Begin VB.CommandButton btnMiniVan 
      Caption         =   "Make a Minivan"
      Height          =   615
      Left            =   240
      TabIndex        =   0
      Top             =   120
      Width           =   2775
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub btnDragRace_Click()
    Dim ardrag(2) As IDragRace
    Set ardrag(0) = New CoHotRod
    Set ardrag(1) = New CoJeep
    Set ardrag(2) = New CoMiniVan
    
    Dim i As Integer
    For i = 0 To 2
        ardrag(i).DragRace
    Next i
End Sub

Private Sub btnHotRod_Click()
    Dim o As New CoHotRod
    o.SetMaxSpeed 100
    o.SetPetName "BeMer"
    Dim i As IStats
    Set i = o
    i.DisplayStats
End Sub

Private Sub btnJeep_Click()
    Dim o As New CoJeep
    o.Create "Martha", "777 Peace Ave", "Manu", 100
    Dim i As IStats
    Set i = o
    i.DisplayStats
End Sub

Private Sub btnMiniVan_Click()
    Dim o As New CoMiniVan
    Dim i As IEngine
    Dim i2 As IStats
    
    o.CreateMiniVan "Norman", 20, "Al", "123 Boring lane", 8
    
    Set i2 = o
    i2.DisplayStats
    MsgBox o.numberOfDoors
    Set i = i2
    Do While i.GetCurSpeed <= i.GetMaxSpeed
         i.SpeedUp
    Loop
    
    MsgBox i2.GetPetName & " has blown up!", , "Lead foot"
    
End Sub


