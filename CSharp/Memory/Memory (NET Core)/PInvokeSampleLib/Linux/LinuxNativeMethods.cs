using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static PInvokeSample.LinuxNativeMethods.LinkErrors;

namespace PInvokeSample
{
   [SupportedOSPlatform("Linux")]
   internal static class LinuxNativeMethods
   {
      private static readonly Dictionary<LinkErrors, string> ErrorMessages = new()
      {
         {
            Eperm,
            "On GNU/Linux and GNU/Hurd systems and some others, you cannot make links to directories.Many systems allow only privileged users to do so."
         },
         { Enoent, "The file named by oldname doesn’t exist. You can’t make a link to a file that doesn’t exist." },
         { Eio, "A hardware error occurred while trying to read or write to the filesystem." },
         { Eacces, "You are not allowed to write to the directory in which the new link is to be written." },
         {
            Eexist,
            "There is already a file named newname. If you want to replace this link with a new link, you must remove the old link explicitly first."
         },
         { Exdev, "The directory specified in newname is on a different file system than the existing file." },
         { Enospc, "The directory or file system that would contain the new link is full and cannot be extended." },
         {
            Erofs, "The directory containing the new link can’t be modified because it’s on a read - only file system."
         },
         {
            Emlink,
            "There are already too many links to the file named by oldname. (The maximum number of links to a file is LINK_MAX; see Section 32.6 [Limits on File System Capacity], page 904.)"
         }
      };


      [DllImport("libc", EntryPoint = "link", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
      private static extern int Link(string oldpath, string newpath);

      internal static void CreateHardLink(string oldFileName,
         string newFileName)
      {
         var result = Link(oldFileName, newFileName);
         if (result != 0)
         {
            var errorCode = Marshal.GetLastWin32Error();
            if (!ErrorMessages.TryGetValue((LinkErrors)errorCode, out var errorText))
            {
               errorText = "No error message defined";
            }

            throw new IOException(errorText, errorCode);
         }
      }

      internal enum LinkErrors
      {
         Eperm = 1,
         Enoent = 2,
         Eio = 5,
         Eacces = 13,
         Eexist = 17,
         Exdev = 18,
         Enospc = 28,
         Erofs = 30,
         Emlink = 31
      }
   }
}