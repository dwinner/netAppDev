using System;

namespace IpcRemoting.Lib;

[Serializable]
public class StatusEventArgs(string message) : EventArgs
{
   public string Message { get; set; } = message;
}