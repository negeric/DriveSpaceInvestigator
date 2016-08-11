using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BlackHole
{
    class Shared
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static DriveInfo[] getDrives()
        {
            DriveInfo[] drives;
            try
            {
                drives = DriveInfo.GetDrives();
#if DEBUG
                log.Debug("Enumerated " + drives.Count() + " disk drives");
#endif 
            } catch (Exception ex)
            {
                log.Error("Error enumerating drives", ex);
                drives = null;
            }
            return drives;
        }
        //Byte Conversion to human readable
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static Dictionary<double, string> sizeSuffix(Int64 value)
        {
            Dictionary<double, string> dict = new Dictionary<double, string>();
            if (value < 0)
            {
                dict.Add(0,"Error");
                return dict;
            }
            if (value == 0)
            {
                dict.Add(0,"bytes");
            }

            int mag = (int)Math.Log(value, 1024);
            double adjustedSize = (double)value / (1L << (mag * 10));
            dict.Add(adjustedSize, SizeSuffixes[mag]);
            //return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
            return dict;
        }
        public static double Ceil(double value, int dec)
        {
            double adjust = Math.Pow(10, dec);
            return Math.Ceiling(value * adjust) / adjust;
        }
        //Map an image resource file to an extension
        public static System.Drawing.Image getIconForFileType(string extension)
        {
            System.Drawing.Image img;
            switch (extension.ToLower())
            {
                case ".accdb":
                    img = Properties.Resources.ACCDB;
                    break;
                case ".ai":
                    img = Properties.Resources.AI;
                    break;
                case ".avi":
                    img = Properties.Resources.AVI;
                    break;
                case ".bmp":
                    img = Properties.Resources.BMP;
                    break;
                case ".css":
                    img = Properties.Resources.CSS;
                    break;
                case "dir":
                    img = Properties.Resources.closed_folder;
                    break;
                case ".doc":
                    img = Properties.Resources.DOC;
                    break;
                case ".docx":
                    img = Properties.Resources.DOCX;
                    break;
                case ".eps":
                    img = Properties.Resources.EPS;
                    break;
                case ".fla":
                    img = Properties.Resources.FLA;
                    break;
                case ".gif":
                    img = Properties.Resources.GIF;
                    break;
                case ".htm":
                    img = Properties.Resources.HTML;
                    break;
                case ".html":
                    img = Properties.Resources.HTML;
                    break;
                case ".indd":
                    img = Properties.Resources.INDD;
                    break;
                case ".ini":
                    img = Properties.Resources.INI;
                    break;
                case ".jpeg":
                    img = Properties.Resources.JPEG;
                    break;
                case ".jpg":
                    img = Properties.Resources.JPEG;
                    break;
                case ".jsf":
                    img = Properties.Resources.JSF;
                    break;
                case ".midi":
                    img = Properties.Resources.MIDI;
                    break;
                case ".mov":
                    img = Properties.Resources.MOV;
                    break;
                case ".mp3":
                    img = Properties.Resources.MP3;
                    break;
                case ".mpeg":
                    img = Properties.Resources.MPEG;
                    break;
                case ".mpg":
                    img = Properties.Resources.MPEG;
                    break;
                case ".pdf":
                    img = Properties.Resources.PDF;
                    break;
                case ".png":
                    img = Properties.Resources.PNG;
                    break;                
                case ".ppt":
                    img = Properties.Resources.PPT;
                    break;
                case ".pptx":
                    img = Properties.Resources.PPTX;
                    break;
                case ".proj":
                    img = Properties.Resources.PROJ;
                    break;
                case ".psd":
                    img = Properties.Resources.PSD;
                    break;
                case ".pst":
                    img = Properties.Resources.PST;
                    break;
                case ".rar":
                    img = Properties.Resources.RAR;
                    break;
                case ".readme":
                    img = Properties.Resources.README;
                    break;
                case ".set":
                    img = Properties.Resources.SET;
                    break;
                case ".tif":
                    img = Properties.Resources.TIFF;
                    break;
                case ".tiff":
                    img = Properties.Resources.FLA;
                    break;
                case ".txt":
                    img = Properties.Resources.TXT;
                    break;
                case ".url":
                    img = Properties.Resources.URL;
                    break;
                case ".wav":
                    img = Properties.Resources.WAV;
                    break;
                case ".wma":
                    img = Properties.Resources.WMA;
                    break;
                case ".wmv":
                    img = Properties.Resources.WMV;
                    break;
                case ".xls":
                    img = Properties.Resources.XLS;
                    break;
                case ".xlsx":
                    img = Properties.Resources.XLSX;
                    break;
                case ".zip":
                    img = Properties.Resources.ZIP;
                    break;
                default:
                    img = Properties.Resources.TXT;
                    break;
            }
            return img;
        }
        public static string secondsToMinutes(long s)
        {
            TimeSpan t = TimeSpan.FromSeconds(s);
            string formatted = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
            return formatted;
        }
    }

    //Custom classes
    public interface IFileFolderInfo
    {
        int id { get; }
    }
    public class FileFolderInfo : IFileFolderInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public long size { get; set; }
        public string ext { get; set; }
        public int accessible { get; set; }
        public int subDirErrors { get; set; }        
    }
    //INotfiyPropertyChanged for status and current working directory
    public class StatusUpdate : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));

        }
    }
}
