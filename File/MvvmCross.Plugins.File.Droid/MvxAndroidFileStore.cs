// MvxAndroidFileStore.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

#region using

using System.IO;
using Android.Content;
using MvvmCross.Platform.Droid;
using MvvmCross.Platform;
using Android.OS;

#endregion

namespace MvvmCross.Plugins.File.Droid
{
    public class MvxAndroidFileStore
        : MvxIoFileStoreBase
    {

        public override ulong GetDiskFreeSpace(string DirectoryName)
        {
            var path = Environment.DataDirectory;
            StatFs stat = new StatFs(path.Path);
            int blockSize = stat.BlockSize;
            int availableBlocks = stat.AvailableBlocks;
            
            return (ulong) (availableBlocks * blockSize);
        }

        //File path = Environment.getDataDirectory();
        //StatFs stat = new StatFs(path.getPath());
        //long blockSize = stat.getBlockSize();
        //long availableBlocks = stat.getAvailableBlocks();
        //return formatSize(availableBlocks* blockSize);

        private Context _context;

        private Context Context => _context ?? (_context = Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext);

        protected override string FullPath(string path)
        {
            return Path.Combine(Context.FilesDir.Path, path);
        }
    }
}