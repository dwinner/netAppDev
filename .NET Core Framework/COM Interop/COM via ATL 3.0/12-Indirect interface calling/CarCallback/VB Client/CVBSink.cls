VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "CVBSink"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

' CVBSink
'
' This client implements the callback interface
Implements IEngineEvents

Private Sub IEngineEvents_AboutToExplode()
    MsgBox "Prepair to meet they doom..."
End Sub

Private Sub IEngineEvents_Exploded()
        MsgBox "Your car has exploded!"
End Sub
