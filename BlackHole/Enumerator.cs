using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlackHole
{
    class Enumerator
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string workingDir;
        private static string currentAction;
        //Get a list of files in the root directory
        public static List<FileFolderInfo> getRootFiles(string dir)
        {
            currentAction = "Scanning for root files in " + dir;
#if DEBUG
            log.Debug("Scanning for root files in " + dir);
#endif
            Int32 fileCount = 0;            
            List<FileFolderInfo> f = new List<FileFolderInfo>();
            IEnumerable<string> subFiles = SafeWalk.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly);
            foreach (string file in subFiles)
            {
                FileInfo fi = new FileInfo(file);
                string extension = Path.GetExtension(file);
                fileCount++;
                f.Add(new FileFolderInfo { id = fileCount, name = fi.Name, size = fi.Length, type = "file", ext = extension, accessible = 1, subDirErrors = 0 });
            }
            return f;
        }
        
        //Get a list of root directories and their sizes
        public static List<FileFolderInfo> getDirectories(string dir)
        {
            currentAction = "Scanning for directories in " + dir;
#if DEBUG
            log.Debug("Scanning for { in " + dir);
#endif
            //setup the return value
            List<FileFolderInfo> f = new List<FileFolderInfo>();
            //Get a list of root level directories
            string[] dirs = Directory.GetDirectories(dir);
            foreach(string d in dirs)
            {
                currentAction = "Calculating directory size: " + dir;
                Int32 fileCount = 0;
                Int64 size = 0;
                IEnumerable<string> subFiles = SafeWalk.EnumerateFiles(dir, "*", SearchOption.AllDirectories);
                foreach (string file in subFiles)
                {
                    try {
                        FileInfo fi = new FileInfo(file);
                        size += fi.Length;
                        fileCount++;
                    } catch (Exception ex)
                    {
                        log.Error("Skipping file " + file, ex);
                    }
                }
                f.Add(new FileFolderInfo { id = fileCount, name = d, size = size, type = "dir", ext = "", accessible = 1, subDirErrors = 0 });
            }
            
            return f;
        }
        /*
        public static List<FileFolderInfo> enumerateDir(string dir)
        {
            workingDir = dir;
            List<FileFolderInfo> f = new List<FileFolderInfo>();
            try {
                string[] dirs = Directory.GetDirectories(dir);
                string[] files = Directory.GetFiles(dir);
                List<IFileFolderInfo> dirList = new List<IFileFolderInfo>();
                List<IFileFolderInfo> fileList = new List<IFileFolderInfo>();
                int id = 0;
                foreach (string d in dirs)
                {
                    try {
                        id++;
                        Dictionary<long, int> subDirInfo = getDirectorySize(d);
                        var subDirInfoItem = subDirInfo.First();
                        long dirSize = subDirInfoItem.Key;
                        int fileErrors = subDirInfoItem.Value;
                        f.Add(new FileFolderInfo { id = id, name = d, size = dirSize, type = "dir", ext = "dir", accessible = 1, subDirErrors = fileErrors });
                    } catch (System.UnauthorizedAccessException ex)
                    {
                        log.Info("We were denied access to a file in " + d + ".  Skipping this file and logging more details to the error-log.  Reference ID AD-11");
                        log.Error("Access denied [AD-11], see inner exception", ex);
                    }

                }
                foreach (string file in files)
                {
                    id++;
                    string extension = "";
                    int accessible = 1;
                    long fileSize;
                    try {
                        extension = Path.GetExtension(file);
                        FileInfo fInfo = new FileInfo(file);
                        fileSize = Convert.ToInt64(fInfo.Length);
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        accessible = 0;
                        fileSize = 0;
                        log.Info("Access denied on file " + file + ".  Cannot calculate filesize");
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error capturing file size for " + file, ex);
                        fileSize = 0;
                    }
                    f.Add(new FileFolderInfo { id = id, name = file, size = fileSize, type = "file", ext = extension, accessible = accessible });
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                log.Info("Access denied. See error-log for details including the filename.  Reference ID AD-12F");
                log.Error("Access denied [AD-12F], see inner exception", ex);
            } catch (Exception ex) {
                log.Info("General error occurred", ex);
                log.Error("General error occurred", ex);
            }
            return f;
        }       

        public static List<FileFolderInfo> enumerateDirNew(string dir)
        {
            workingDir = dir;
            Int64 dirSize = 0;
            Int32 fileCount = 0;
            int id = 0;
            List<FileFolderInfo> f = new List<FileFolderInfo>();
            string[] files = Directory.GetFiles(dir);
            try
            {
                List<IFileFolderInfo> dirList = new List<IFileFolderInfo>();
                List<IFileFolderInfo> fileList = new List<IFileFolderInfo>();
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                IEnumerable<string> subFiles = SafeWalk.EnumerateFiles(dir, "*", SearchOption.AllDirectories);
                foreach (string file in subFiles)
                {
                    FileInfo fi = new FileInfo(file);
                    dirSize += fi.Length;
                    fileCount++;
                    
                }
                f.Add(new FileFolderInfo { id = id, name = dir, size = dirSize, type = "dir", ext = "dir", accessible = 1, subDirErrors = 0 });              
                
                foreach (string file in files)
                {
                    id++;
                    string extension = "";
                    int accessible = 1;
                    long fileSize;
                    try
                    {
                        extension = Path.GetExtension(file);
                        FileInfo fInfo = new FileInfo(file);
                        fileSize = Convert.ToInt64(fInfo.Length);
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        accessible = 0;
                        fileSize = 0;
                        log.Info("Access denied on file " + file + ".  Cannot calculate filesize");
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error capturing file size for " + file, ex);
                        fileSize = 0;
                    }
                    f.Add(new FileFolderInfo { id = id, name = file, size = fileSize, type = "file", ext = extension, accessible = accessible });
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                log.Info("Access denied. See error-log for details including the filename.  Reference ID AD-12F");
                log.Error("Access denied [AD-12F], see inner exception", ex);
            }
            catch (Exception ex)
            {
                log.Info("General error occurred", ex);
                log.Error("General error occurred", ex);
            }
            return f;
        }      
        */
        //Need to provide a way to loop through and ignore access denied errors
        public static Dictionary<long,int> getDirectorySize(string dir)
        {          
            long dirSize = 0;
            int failed = 0;
            Dictionary<long, int> ret = new Dictionary<long, int>();
            try
            {
                string[] a = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
                foreach (string f in a)
                {
                    try
                    {
                        if (f.Length < 250)
                        {
                            FileInfo fInfo = new FileInfo(f);
                            dirSize += fInfo.Length;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Info("Access denied on " + f + ".  Cannot calculate filesize");
                        failed++;
                    }
                }
                ret.Add(dirSize, failed);
            }
            catch (UnauthorizedAccessException ex)
            {
                log.Info("We were denied access to a file in " + dir + ".  Skipping this file and logging more details to the error-log.  Reference ID AD-13");
                log.Error("Access denied [AD-13], see inner exception", ex);
                //Calculate old fashioned way
                dirSize = getDirectorySizeLegacy(dir);
                ret.Add(dirSize, 1);
            } catch (Exception e)
            {
                log.Info("We ran into an issue on a file in " + dir + ".  Skipping this file and logging more details to the error-log.  Reference ID AD-14");
                log.Error("Unknown error [AD-14], see inner exception", e);
            }       
            return ret;
        }       

        //Need to turn this into a task

        private static long getDirectorySizeLegacy(string dir)
        {
            long dirSize = 0;
            //Get a recursive list of files
            List<string> files = recursiveDirSearchLegacy(dir);
            foreach(string f in files)
            {
                try {
                    FileInfo fInfo = new FileInfo(f);
                    dirSize += fInfo.Length;
                }
                catch (Exception e)
                {
                    log.Info("We ran into an issue on a file in " + dir + ".  Skipping this file and logging more details to the error-log.  Reference ID UE-17");
                    log.Error("Unknown error [UE-17], see inner exception", e);
                }
            }
            return dirSize;
        }
        private static List<string> recursiveDirSearchLegacy(string dir)
        {
            List<string> files = new List<string>();

            try
            {
                foreach (string f in Directory.GetFiles(dir))
                {
                    files.Add(f);
                }

                foreach (string d in Directory.GetDirectories(dir))
                {
                    files.AddRange(recursiveDirSearchLegacy(d));
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

            return files;
        }
    }
    public static class SafeWalk
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOpt)
        {
            try
            {
                var dirFiles = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories)
                {
                    dirFiles = Directory.EnumerateDirectories(path)
                                        .SelectMany(x => EnumerateFiles(x, searchPattern, searchOpt));
                }
                return dirFiles.Concat(Directory.EnumerateFiles(path, searchPattern));
            }
            catch (UnauthorizedAccessException uex)
            {
                log.Error("Unauthorized error", uex);
                return Enumerable.Empty<string>();
            }
            catch (Exception ex)
            {
                log.Error("Unkown error in safeWalk", ex);
                return Enumerable.Empty<string>();
            }

        }
    }
}
