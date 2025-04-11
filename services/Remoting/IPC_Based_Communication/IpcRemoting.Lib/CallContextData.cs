using System;
using System.Runtime.Remoting.Messaging;

namespace IpcRemoting.Lib;

[Serializable]
public class CallContextData : ILogicalThreadAffinative
{
   public string Data { get; set; }
}