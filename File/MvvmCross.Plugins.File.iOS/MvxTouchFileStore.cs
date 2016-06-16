// MvxIosFileStore.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using Foundation;
using System;
using System.IO;

namespace MvvmCross.Plugins.File.iOS
{
    public class MvxIosFileStore : MvxIoFileStoreBase
    {
        public const string ResScheme = "res:";

        public override ulong GetDiskFreeSpace(string DirectoryName)
        {
            NSError error;
            NSFileManager mgr = new NSFileManager();
            string[] filesArray = mgr.GetDirectoryContentRecursive(DirectoryName, out error);
            ulong fileSize = 0;

            foreach (var item in filesArray)
            {
                var file = mgr.GetFileSystemAttributes(item);
                fileSize += file.Size;
            }

            return fileSize;
        }

        protected override string FullPath(string path)
        {
            if (path.StartsWith(ResScheme))
                return path.Substring(ResScheme.Length);

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), path);
        }
    }
}

// TODO - credits needed!