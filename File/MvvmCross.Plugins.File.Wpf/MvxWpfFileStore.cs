// MvxWpfFileStore.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using MvvmCross.Platform.Platform;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MvvmCross.Plugins.File.Wpf
{
    public class MvxWpfFileStore : MvxIoFileStoreBase
    {
        private readonly string _rootFolder;

        public MvxWpfFileStore(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        public override ulong GetDiskFreeSpace(string DirectoryName)
        {
            ulong result = 0;

            ulong lpFreeBytesAvailable;
            ulong lpTotalNumberOfBytes;
            ulong lpTotalNumberOfFreeBytes;

            if (GetDiskFreeSpaceEx(DirectoryName, out lpFreeBytesAvailable, out lpTotalNumberOfBytes, out lpTotalNumberOfFreeBytes))
            {
                result = lpFreeBytesAvailable;
            }
            else
            {
                MvxTrace.Trace($"Error during : System Error Code  : {Marshal.GetLastWin32Error()}");
            }

            return result;
        }

        [DllImport("api-ms-win-core-file-l1-2-0.dll", CharSet = CharSet.Unicode, EntryPoint = "GetDiskFreeSpaceExW", SetLastError = true)]
        private static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
                                      out ulong lpFreeBytesAvailable,
                                      out ulong lpTotalNumberOfBytes,
                                      out ulong lpTotalNumberOfFreeBytes);
        protected override string FullPath(string path)
        {
            return Path.Combine(_rootFolder, path);
        }
    }
}

// TODO - credits needed!