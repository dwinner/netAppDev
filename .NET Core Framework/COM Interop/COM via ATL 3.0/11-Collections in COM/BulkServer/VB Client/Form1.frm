VERSION 5.00
Begin VB.Form Form1 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "Bulk Server Tester"
   ClientHeight    =   3330
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   5100
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3330
   ScaleWidth      =   5100
   ShowInTaskbar   =   0   'False
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton btnGetSub 
      Caption         =   "Get Sub Obj"
      Height          =   495
      Left            =   120
      TabIndex        =   4
      Top             =   1080
      Width           =   2055
   End
   Begin VB.CommandButton btnStruct 
      Caption         =   "Struct me!"
      Height          =   495
      Left            =   2640
      TabIndex        =   3
      Top             =   240
      Width           =   2175
   End
   Begin VB.ListBox List1 
      Height          =   1035
      Left            =   2760
      TabIndex        =   2
      Top             =   2040
      Width           =   1935
   End
   Begin VB.CommandButton btnGetASafeArray 
      Caption         =   "Get some SAFE strings"
      Height          =   615
      Left            =   120
      TabIndex        =   1
      Top             =   2400
      Width           =   2175
   End
   Begin VB.CommandButton btnStrings 
      Caption         =   "Send in strings"
      Height          =   495
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   2175
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Dim o As New CoBulkObject

' Get the sub object.
'
Private Sub btnGetSub_Click()
    Dim i As CoSub
    Set i = o.GetSubObject
    MsgBox i.DatumOne
End Sub

' Create some strings and send in for processing.
'
Private Sub btnStrings_Click()
    ' Initial string array.
    Dim strs(3) As String
    strs(0) = "Hello"
    strs(1) = "there"
    strs(2) = "from"
    strs(3) = "VB..."
    o.UseTheseStrings 4, strs(0)
    
    ' See how they were changed
    Dim i As Integer
    For i = 0 To 3
            MsgBox strs(i), , "New String from coclass"
    Next i
End Sub

' Get some strings from the coclass.
'
Private Sub btnGetASafeArray_Click()
        Dim strs As Variant
        strs = o.GiveMeASafeArrayOfString
        
        Dim upper As Long
        Dim i As Long
        upper = UBound(strs)
        For i = 0 To upper
                List1.AddItem strs(i)
        Next i
End Sub

Private Sub btnStruct_Click()

        ' Make a pet.
        Dim pet As MyPet
        pet.Name = "Fred"
        pet.Age = 20
        pet.Type = petDog
    
        ' Send in for processing.
        o.WorkWithAPet pet
    
        ' See what happened.
        MsgBox pet.Age, , "New Age"
        MsgBox pet.Name, , "New Name"

    ' Map enum to string.
        Dim t(2) As String
        t(0) = "Dog"
        t(1) = "Cat"
        t(2) = "Tick"

        MsgBox t(pet.Type), , "New Type"
End Sub

