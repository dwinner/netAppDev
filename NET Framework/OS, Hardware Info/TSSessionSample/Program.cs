using System;
using System.Runtime.InteropServices;

namespace TSSessionSample
{
   internal static class Program
   {
      #region Constants

      public const int WtsCurrentSession = -1;

      #endregion

      private static void Main()
      {
         var pServer = IntPtr.Zero;
         var sUserName = string.Empty;
         var sDomain = string.Empty;
         var sClientApplicationDirectory = string.Empty;
         var sIpAddress = string.Empty;

         var oClientAddres = new WtsClientAddress();
         var oClientDisplay = new WtsClientDisplay();

         var pSessionInfo = IntPtr.Zero;

         var iCount = 0;
         var iReturnValue = WTSEnumerateSessions
            (pServer, 0, 1, ref pSessionInfo, ref iCount);
         var iDataSize = Marshal.SizeOf(typeof(WtsSessionInfo));

         var iCurrent = (int) pSessionInfo;

         if (iReturnValue != 0)
         {
            //Go to all sessions
            for (var i = 0; i < iCount; i++)
            {
               var oSessionInfo =
                  (WtsSessionInfo) Marshal.PtrToStructure((IntPtr) iCurrent,
                     typeof(WtsSessionInfo));
               iCurrent += iDataSize;

               uint iReturned = 0;

               //Get the IP address of the Terminal Services User
               IntPtr pAddress/* = IntPtr.Zero*/;
               if (WTSQuerySessionInformation(pServer,
                  oSessionInfo.iSessionID, WtsInfoClass.WtsClientAddress,
                  out pAddress, out iReturned))
               {
                  oClientAddres = (WtsClientAddress) Marshal.PtrToStructure(pAddress, oClientAddres.GetType());
                  sIpAddress = string.Format("{0}.{1}.{2}.{3}",
                     oClientAddres.bAddress[2],
                     oClientAddres.bAddress[3],
                     oClientAddres.bAddress[4],
                     oClientAddres.bAddress[5]);
               }

               //Get the User Name of the Terminal Services User
               if (WTSQuerySessionInformation(pServer,
                  oSessionInfo.iSessionID, WtsInfoClass.WtsUserName,
                  out pAddress, out iReturned))
                  sUserName = Marshal.PtrToStringAnsi(pAddress);
               //Get the Domain Name of the Terminal Services User
               if (WTSQuerySessionInformation(pServer,
                  oSessionInfo.iSessionID, WtsInfoClass.WtsDomainName,
                  out pAddress, out iReturned))
                  sDomain = Marshal.PtrToStringAnsi(pAddress);
               //Get the Display Information  of the Terminal Services User
               if (WTSQuerySessionInformation(pServer,
                  oSessionInfo.iSessionID, WtsInfoClass.WtsClientDisplay,
                  out pAddress, out iReturned))
                  oClientDisplay = (WtsClientDisplay) Marshal.PtrToStructure
                     (pAddress, oClientDisplay.GetType());
               //Get the Application Directory of the Terminal Services User
               if (WTSQuerySessionInformation(pServer, oSessionInfo.iSessionID,
                  WtsInfoClass.WtsClientDirectory, out pAddress, out iReturned))
                  sClientApplicationDirectory = Marshal.PtrToStringAnsi(pAddress);

               Console.WriteLine("Session ID : {0}", oSessionInfo.iSessionID);
               Console.WriteLine("Session State : {0}", oSessionInfo.oState);
               Console.WriteLine("Workstation Name : {0}", oSessionInfo.sWinsWorkstationName);
               Console.WriteLine("IP Address : {0}", sIpAddress);
               Console.WriteLine("User Name : {0}\\{1}", sDomain, sUserName);
               Console.WriteLine("Client Display Resolution: " +
                                 oClientDisplay.iHorizontalResolution + " x " +
                                 oClientDisplay.iVerticalResolution);
               Console.WriteLine("Client Display Colour Depth: " +
                                 oClientDisplay.iColorDepth);
               Console.WriteLine("Client Application Directory: " +
                                 sClientApplicationDirectory);

               Console.WriteLine("-----------------------");
            }

            WTSFreeMemory(pSessionInfo);
         }
      }

      #region Dll Imports

      [DllImport("wtsapi32.dll")]
      private static extern int WTSEnumerateSessions(
         IntPtr pServer,
         [MarshalAs(UnmanagedType.U4)] int iReserved,
         [MarshalAs(UnmanagedType.U4)] int iVersion,
         ref IntPtr pSessionInfo,
         [MarshalAs(UnmanagedType.U4)] ref int iCount);

      [DllImport("Wtsapi32.dll")]
      public static extern bool WTSQuerySessionInformation(
         IntPtr pServer,
         int iSessionId,
         WtsInfoClass oInfoClass,
         out IntPtr pBuffer,
         out uint iBytesReturned);

      [DllImport("wtsapi32.dll")]
      private static extern void WTSFreeMemory(
         IntPtr pMemory);

      #endregion

      #region Structures

      //Structure for Terminal Service Client IP Address
      [StructLayout(LayoutKind.Sequential)]
      private struct WtsClientAddress
      {
         public readonly int iAddressFamily;

         [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
         public readonly byte[] bAddress;
      }

      //Structure for Terminal Service Session Info
      [StructLayout(LayoutKind.Sequential)]
      private struct WtsSessionInfo
      {
         public readonly int iSessionID;
         [MarshalAs(UnmanagedType.LPStr)] public readonly string sWinsWorkstationName;
         public readonly WtsConnectstateClass oState;
      }

      //Structure for Terminal Service Session Client Display
      [StructLayout(LayoutKind.Sequential)]
      private struct WtsClientDisplay
      {
         public readonly int iHorizontalResolution;

         public readonly int iVerticalResolution;

         //1 = The display uses 4 bits per pixel for a maximum of 16 colors.
         //2 = The display uses 8 bits per pixel for a maximum of 256 colors.
         //4 = The display uses 16 bits per pixel for a maximum of 2^16 colors.
         //8 = The display uses 3-byte RGB values for a maximum of 2^24 colors.
         //16 = The display uses 15 bits per pixel for a maximum of 2^15 colors.
         public readonly int iColorDepth;
      }

      #endregion

      #region Enumurations

      public enum WtsConnectstateClass
      {
         WtsActive,
         WtsConnected,
         WtsConnectQuery,
         WtsShadow,
         WtsDisconnected,
         WtsIdle,
         WtsListen,
         WtsReset,
         WtsDown,
         WtsInit
      }

      public enum WtsInfoClass
      {
         WtsInitialProgram,
         WtsApplicationName,
         WtsWorkingDirectory,
         WtsoemId,
         WtsSessionId,
         WtsUserName,
         WtsWinStationName,
         WtsDomainName,
         WtsConnectState,
         WtsClientBuildNumber,
         WtsClientName,
         WtsClientDirectory,
         WtsClientProductId,
         WtsClientHardwareId,
         WtsClientAddress,
         WtsClientDisplay,
         WtsClientProtocolType,
         WtsIdleTime,
         WtsLogonTime,
         WtsIncomingBytes,
         WtsOutgoingBytes,
         WtsIncomingFrames,
         WtsOutgoingFrames,
         WtsClientInfo,
         WtsSessionInfo,
         WtsConfigInfo,
         WtsValidationInfo,
         WtsSessionAddressV4,
         WtsIsRemoteSession
      }

      #endregion
   }
}