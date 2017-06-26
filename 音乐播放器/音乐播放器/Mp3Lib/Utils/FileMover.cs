// from http://saftsack.fs.uni-bayreuth.de/~dun3/archives/howto-savely-move-a-file-using-c/145.html
// 
// Howto safely move a file using C#
// by Tobias Hertkorn on March 1st, 2008
//
// If possible this version uses the special File.Replace on NTFS 
// and graciously falls back on Delete+Move on any other file system. 
// Nothing else to comment about here. I am just amazed that this 
// kind of save moving is not supported by the framework itself. 
// Instead the framework's move routine throws an exception 
// if there is an file existing at the target location. Weird. ;)
//
// Just a heads up: Don't use this in performance critical parts...
// 
// Andy Pearmund - Jan 2009 - added backup support for non-NTFS file systems.

using System;
using System.IO;

namespace Com.Hertkorn.Helper.Filesystem
{
    /// <summary>
    /// Provides FileMove function to wrap System.IO.File.Replace
    /// </summary>
    public static class FileMover
    {
        /// <summary>
        /// Securely moves a file to a new location. Overwrites any
        /// preexisting file at new location (= replacing file).
        /// </summary>
        /// <remarks>
        /// If NTFS is available this is done via File.Replace.
        /// If NTFS is not available it will be moved via deleting
        /// any preexisting file and moving. Do NOT rely on the
        /// backupFile being there - or not - after the move process.
        /// That is not predetermined. This method is clearly
        /// optimized for the case that NTFS is available. Consider NOT
        /// using it on any other filesystem, if performance is an issue!
        /// </remarks>
        /// <param name="sourceLocation">The file to be moved.</param>
        /// <param name="targetLocation">The new resting place of the file.</param>
        /// <param name="backupLocation">A backup location that is used when replacing.</param>
        public static void FileMove( FileInfo sourceLocation,
                                     FileInfo targetLocation,
                                     FileInfo backupLocation )
        {
            if (targetLocation.Exists)
            {
                try
                {
                    File.Replace(sourceLocation.FullName,
                                 targetLocation.FullName,
                                 backupLocation.FullName,
                                 true);
                }
                catch (PlatformNotSupportedException)
                {
                    // Not operating on an NTFS volume
                     NonNtfsReplace(sourceLocation,
                                    targetLocation,
                                    backupLocation);
                }
            }
            else
            {
                File.Move(sourceLocation.FullName, targetLocation.FullName);
            }
        }

        private static void NonNtfsReplace(FileInfo sourceLocation, 
                                           FileInfo targetLocation, 
                                           FileInfo backupLocation)
        {
            // a unique name for a temp backup
            FileInfo backup2Location = null;

            // make sure there's space for the backup
            if (backupLocation.Exists)
            {
                // find unique name for backup2
                string backup2Name = Path.Combine(Path.GetDirectoryName(backupLocation.FullName), Path.GetRandomFileName());
                backup2Location = new FileInfo(backup2Name);

                // rename the old backup to backup2
                // if this fails we just exit; no permanent changes have been made yet.
                File.Move(backupLocation.FullName, backup2Location.FullName);
            }

            try
            {
                // rename the old target to backup
                // if this fails we need to rename the old backup2 back
                File.Move(targetLocation.FullName, backupLocation.FullName);
            }
            catch
            {
                if (backup2Location != null)
                {
                    // we've got a backup2 to unwind too
                    // we managed to rename it once before, so this will probably work again
                    File.Move(backup2Location.FullName, backupLocation.FullName);
                }
                throw;
            }

            try
            {
                // rename source to target
                // if this fails we need to rename the old target back
                File.Move(sourceLocation.FullName, targetLocation.FullName);
            }
            catch
            {
                // we managed to rename it once before, so this will probably work again
                File.Move(backupLocation.FullName, targetLocation.FullName);

                if (backup2Location != null)
                {
                    // we've got a backup2 to unwind too
                    File.Move(backup2Location.FullName, backupLocation.FullName);
                }
                throw;
            }

            // now finally get rid of the backup2
            try
            {
                backup2Location.Delete();
            }
            catch
            {
                // if this fails we can ignore it, as the main operation has succeeded.
            }
        }
    }
}
