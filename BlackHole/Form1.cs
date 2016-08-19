using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BlackHole
{
    public partial class Form1 : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool allowSingleClickActions;
        private int currentChartIndex = -1;
        private string previousDirectory;
        private string currentWorkingDirectory;
        private string bwCurrentAction;
        private long runTimeInSeconds;
        //System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        Timer timer = new Timer();
        BackgroundWorker bw;
        public Form1()
        {
            InitializeComponent();
            lnkStopScan.Visible = false;
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProcessChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_Completed);
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(t_Tick);
            //timer.Enabled = false;            
            
        }
        void t_Tick(object sender, EventArgs e)
        {
            runTimeInSeconds++;
            delTimerUpdate();
        }

        /*
            Background worker data
        */
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            timer.Enabled = true;
            timer.Start();
            if (bw.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            delLnkStopScanChange(true, true, "Stop Scanning");
            //Setup a loop through files and directories then call the directory size method
            //Root Files
#if DEBUG
            log.Debug("Scanning for root files in " + currentWorkingDirectory);
#endif
            try {
                Int32 fileCount = 0;
                List<FileFolderInfo> f = new List<FileFolderInfo>();
                IEnumerable<string> subFiles = SafeWalk.EnumerateFiles(currentWorkingDirectory, "*", SearchOption.TopDirectoryOnly);
                foreach (string file in subFiles)
                {
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        bwCurrentAction = "Scanning: " + fi.FullName;
                        bw.ReportProgress(0);
                        string extension = Path.GetExtension(file);
                        fileCount++;
                        try {
                            f.Add(new FileFolderInfo { id = fileCount, name = fi.Name, size = fi.Length, type = "file", ext = extension, accessible = 1, subDirErrors = 0 });

                            bwAddFiles(fi);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error looking up information for file " + file, ex);
                        }
                    }
                    catch (UnauthorizedAccessException uex)
                    {
                        //fileCount++;
                        log.Info("Access denied on file " + file + ".  See error log and referece [AD-154]");
                        log.Error("Error AD-154", uex);
                    }
                    catch (Exception ex) {
                        log.Info("Unknown error occurred on file " + file + ".  See error log and reference [UE-154]");
                        log.Error("Error UE-154", ex);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Info("Unknown error occurect.  See error log and reference [UE-155]");
                log.Error("Error UE-155", ex);
            }
            //Directories and size
            //Root Files
#if DEBUG
            log.Debug("Scanning directories in " + currentWorkingDirectory);
#endif
            string[] dirs = Directory.GetDirectories(currentWorkingDirectory);
            foreach (string d in dirs)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                bwCurrentAction = "Scanning directory: " + d;
                bw.ReportProgress(0);
                Dictionary<long, int> dirSize = Enumerator.getDirectorySize(d);
                bwAddDir(d, dirSize);
                //id = fileCount, name = d, size = size, type = "dir", ext = "", accessible = 1, subDirErrors = 0 });
            }

        }
        void bw_ProcessChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLbl.Text = bwCurrentAction;
            dgEnumeratorManualSort("manual", 2, "Size", "ActualSize", ListSortDirection.Descending);
        }
        void bw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
            string formattedTimer = Shared.secondsToMinutes(runTimeInSeconds);
            if (e.Cancelled)
            {
                statusLbl.Text = "Drive scan cancelled by user after " + formattedTimer;
                log.Info("Drive enumartation cancelled by user " + formattedTimer);
            }
            else if (e.Error != null)
            {
                statusLbl.Text = "Error while performing background operation after " + formattedTimer;
                log.Error("Error performing background operation after " + formattedTimer, e.Error);
            }
            else
            {
                statusLbl.Text = "Drive enumerated";
#if DEBUG
                log.Debug("Enumeration task completed after " + formattedTimer);
#endif
            }
            txtManualScan.Enabled = true;
            btnManulScan.Enabled = true;
            groupData.Enabled = true;
            groupTools.Enabled = true;
            groupData.Cursor = Cursors.Default;
            groupTools.Cursor = Cursors.Default;
            lnkStopScan.Visible = false;
        }
        //Update UI from BackgroundWorker
        private void bwAddFiles(FileInfo f)
        {
            if (this.dgEnumerator.InvokeRequired)
                this.dgEnumerator.Invoke((MethodInvoker)delegate { this.AddFileToGrid(f); });
            else
                this.AddFileToGrid(f);
        }
        private void AddFileToGrid(FileInfo f)
        {
            int rowIndex;
            Image icon;

            try
            {
                icon = Shared.getIconForFileType(f.Extension);
                //Color fontColor = (f.accessible == 1) ? Color.Black : Color.Red;
                Color fontColor = Color.Black;
                Dictionary<double, string> fileSize = Shared.sizeSuffix(f.Length);
                var fileSizeItem = fileSize.First();
                double roundFileSize = Shared.Ceil(fileSizeItem.Key, 2);
                rowIndex = dgEnumerator.Rows.Add(icon, f.Name, roundFileSize + " " + fileSizeItem.Value, f.Length, f.Extension);
                dgEnumerator.Rows[rowIndex].DefaultCellStyle.ForeColor = fontColor;
                string tag = f.Name + "|0|B|1|B|0|B";
                dgEnumerator.Rows[rowIndex].Tag = tag;
            }
            catch (System.UnauthorizedAccessException)
            {
                icon = Shared.getIconForFileType(".err");
                rowIndex = dgEnumerator.Rows.Add(icon, f.Name, "0 bytes");
                dgEnumerator.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                string tag = f.Name + "|0|B|1|B|0|B";
                dgEnumerator.Rows[rowIndex].Tag = tag;
            }
            catch (Exception ex)
            {
                log.Error("Error A-12", ex);
            }


        }
        //Update UI from BackgroundWorker
        private void bwAddDir(string name, Dictionary<long, int> size)
        {
            try {
                if (this.dgEnumerator.InvokeRequired)
                    this.dgEnumerator.Invoke((MethodInvoker)delegate { this.AddDirToGrid(name, size); });
                else
                    this.AddDirToGrid(name, size);
            }
            catch (Exception e)
            {
                log.Error("Error adding " + name + " to datagrid.  Trying to force this.  Look for error DG-1 to see if this succeeded.", e);
                try
                {
                    this.AddDirToGrid(name, size);
                    log.Error("Error [DG-1].  We were able to force " + name + " into the datagrid");
                }
                catch (Exception ex)
                {
                    log.Error("Error [DG-1] adding " + name + " to datagrid", ex); 
                }
            }
        }
        private void AddDirToGrid(string name, Dictionary<long, int> dirSize)
        {
            var dirSizeItem = dirSize.First();
            long size = dirSizeItem.Key;
            int rowIndex;
            Image icon;
            try
            {
                icon = Shared.getIconForFileType("dir");
                //Color fontColor = (f.accessible == 1) ? Color.Black : Color.Red;
                Color fontColor = Color.Black;
                Dictionary<double, string> fileSize = Shared.sizeSuffix(size);
                var fileSizeItem = fileSize.First();
                double roundFileSize = Shared.Ceil(fileSizeItem.Key, 2);
                rowIndex = dgEnumerator.Rows.Add(icon, name, roundFileSize + " " + fileSizeItem.Value, size, ".0dir");
                dgEnumerator.Rows[rowIndex].DefaultCellStyle.ForeColor = fontColor;
                string tag = name + "|0|B|1|B|0|B";
                dgEnumerator.Rows[rowIndex].Tag = tag;
            }
            catch (System.UnauthorizedAccessException)
            {
                icon = Shared.getIconForFileType(".err");
                rowIndex = dgEnumerator.Rows.Add(icon, name, "0 bytes");
                dgEnumerator.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                string tag = name + "|0|B|1|B|0|B";
                dgEnumerator.Rows[rowIndex].Tag = tag;
            }
            catch (Exception ex)
            {
                log.Error("Error A-12", ex);
            }


        }

        /*
            End Background worker data
        */

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Icon = Properties.Resources.atom_icon;
            log.Debug("App Starting");
            lblActionTitle.Text = "Loading...";
            listDrivesAsync();
        }
        private void setupMainGrid(string type)
        {
            dgEnumerator.Rows.Clear();
            dgEnumerator.Columns.Clear();
            switch (type)
            {
                case "drives":
                    dgEnumerator.Columns.Add("Drive", "Drive");
                    dgEnumerator.Columns.Add("FileSystem", "File System");
                    dgEnumerator.Columns.Add("Utilization", "Utilization");
                    allowSingleClickActions = true;
                    break;
                case "enumerate":
                    DataGridViewImageColumn img = new DataGridViewImageColumn();
                    img.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    img.Image = Properties.Resources.TXT;
                    img.Name = "FileType";
                    dgEnumerator.Columns.Add(img);
                    dgEnumerator.Columns.Add("Name", "Name");
                    dgEnumerator.Columns.Add("Size", "Size");
                    dgEnumerator.Columns.Add("ActualSize", "ActualSize");
                    dgEnumerator.Columns.Add("FileExt", "FileExt");
                    dgEnumerator.Columns[3].Visible = false;
                    dgEnumerator.Columns[4].Visible = false;
                    //Set the Size and FileType columns to Programmatic sort.
                    //Size colmun [2] will use the ActualSize column for sorting
                    //FileType column [0] will use the FileExt columnt for sorting
                    dgEnumerator.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
                    dgEnumerator.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
                    allowSingleClickActions = false;
                    break;
            }
            lnkBack.Visible = false;
            dgEnumerator.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private async Task<int> listDrives()
        {
            //Clear and setup the grid
            setupMainGrid("drives");
            DriveInfo[] drives = Shared.getDrives();
            if (drives != null)
            {
                //No errors enumerating drives, add to grid
                foreach (DriveInfo d in drives)
                {
                    //Get used space
                    long usedSpaceLong = Convert.ToInt64(d.TotalSize) - Convert.ToInt64(d.AvailableFreeSpace);
                    //Get a dictionary of the size and suffix
                    //Returns Key=>123 Val=>GB
                    Dictionary<double, string> availableSpace = Shared.sizeSuffix(Convert.ToInt64(d.AvailableFreeSpace));
                    Dictionary<double, string> totalSize = Shared.sizeSuffix(Convert.ToInt64(d.TotalSize));
                    Dictionary<double, string> usedSpace = Shared.sizeSuffix(usedSpaceLong);
                    //Get the first item in dictionary, this should be the only item
                    var availableSpaceItem = availableSpace.First();
                    var totalSizeItem = totalSize.First();
                    var usedSpaceItem = usedSpace.First();
                    //Set variables with the key and val
                    double availableSpaceDec = Shared.Ceil(availableSpaceItem.Key, 2);
                    string availableSpaceSuffix = availableSpaceItem.Value;
                    double totalSizeDec = Shared.Ceil(totalSizeItem.Key, 2);
                    string totalSizeSuffix = totalSizeItem.Value;
                    double usedSpaceDec = Shared.Ceil(usedSpaceItem.Key, 2);
                    string usedSpaceSuffix = usedSpaceItem.Value;
                    string tag = d.Name + "|" + availableSpaceDec.ToString() + "|" + availableSpaceSuffix + "|" + totalSizeDec.ToString() + "|" + totalSizeSuffix + "|" + usedSpaceDec + "|" + usedSpaceSuffix;

                    int rowIndex;
                    rowIndex = dgEnumerator.Rows.Add(d.Name + " " + d.VolumeLabel, d.DriveFormat, availableSpaceDec.ToString() + availableSpaceSuffix + " of " + totalSizeDec.ToString() + totalSizeSuffix);
                    dgEnumerator.Rows[rowIndex].Tag = tag;
                }
                dgEnumerator.ClearSelection();
                return 1;
            } else
            {
                return 0;
            }
        }

        private void setupPieChart(int index)
        {
            string drive = dgEnumerator.Rows[index].Cells[0].Value.ToString();
            string[] tag = dgEnumerator.Rows[index].Tag.ToString().Split(new[] { '|' });
            double freeSpaceDec = Convert.ToDouble(tag[1]);
            double totalSpaceDec = Convert.ToDouble(tag[3]);
            double usedSpaceDec = Convert.ToDouble(tag[5]);
            string freeSpaceSuffix = tag[2];
            string totalSpaceSuffix = tag[4];
            string usedSpaceSuffix = tag[6];
            updatePieChart(drive, totalSpaceDec, totalSpaceSuffix, freeSpaceDec, freeSpaceSuffix, usedSpaceDec, usedSpaceSuffix);
        }

        private void updatePieChart(string drive, double totalSpaceDec, string totalSpaceSuffix, double freeSpaceDec, string freeSpaceSuffix, double usedSpaceDec, string usedSpaceSuffix)
        {
            chartDriveUsage.Series.Clear();
            chartDriveUsage.Titles.Add(drive + " Usage");
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Free Space",
                IsVisibleInLegend = true,
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
            };
            double freePercentage = Shared.Ceil((double)freeSpaceDec / totalSpaceDec * 100, 0);
            double usedPercentage = 100 - freePercentage;
            chartDriveUsage.Series.Add(series1);
            series1.Points.Add(freeSpaceDec);
            series1.Points.Add(usedSpaceDec);
            chartDriveUsage.Legends[0].Enabled = true;
            chartDriveUsage.Series[0].Points[0].LegendText = "Free - " + freeSpaceDec.ToString() + " " + freeSpaceSuffix;
            chartDriveUsage.Series[0].Points[0].Label = freePercentage.ToString() + "%";
            chartDriveUsage.Series[0].Points[1].LegendText = "Used - " + usedSpaceDec.ToString() + " " + usedSpaceSuffix;
            chartDriveUsage.Series[0].Points[1].Label = usedPercentage.ToString() + "%";
            //How do I get the actual suffix of used disk space
            lblTotalDriveSpace.Text = "Drive Size: " + totalSpaceDec.ToString() + totalSpaceSuffix;
            lblUsedDriveSpace.Text = "In use: " + usedSpaceDec.ToString() + " " + usedSpaceSuffix + " (" + usedPercentage + "%)";
            lblFreeDriveSpace.Text = "Available: " + freeSpaceDec.ToString() + " " + freeSpaceSuffix + " (" + freePercentage + "%)";
        }

        //Async list drives
        public async Task listDrivesAsync()
        {
            updateStatusBar("Loading Disk Drives...");
            Task<int> driveTask = listDrives();
            int result = await driveTask;
            if (result != 1)
            {
                log.Error("Error loading drives");
                lblActionTitle.Text = "Error Loading Drives";
                updateStatusBar("Error laoding Drives, try running the app as administator");
            } else
            {
                lblActionTitle.Text = "Double click a Drive to continue";
                updateStatusBar("Drives loaded successfully");
            }
        }

        public void updateStatusBar(string text)
        {
            statusLbl.Text = text;
        }

        private void dgEnumerator_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1)
            {
                string[] tag = dgEnumerator.Rows[index].Tag.ToString().Split(new[] { '|' });
                string path = tag[0];
                currentWorkingDirectory = path;
                txtManualScan.Text = path;
                taskWalkDirectory(path);
                //bw.RunWorkerAsync();
                //walkDirectory(path);                
            }
        }


        private void dgEnumerator_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1 && allowSingleClickActions)
            {
                if (currentChartIndex != index)
                {
                    currentChartIndex = index;
                    setupPieChart(index);
                }
            }
        }
        private void taskWalkDirectory(string path)
        {
            setupMainGrid("enumerate");
            try
            {
                DirectoryInfo parent = Directory.GetParent(path);
                if (parent == null)
                {
                    previousDirectory = "main";
                    lnkBack.Text = "Back to drive list";
                }
                else
                {
                    previousDirectory = parent.FullName.ToString();
                    lnkBack.Text = "Up one directory";
                }

#if DEBUG
                log.Debug("Set parent directory to " + previousDirectory);
                statusLbl.Text = previousDirectory;
#endif
                lnkBack.Visible = true;
            }
            catch (Exception ex)
            {
#if DEBUG              
                log.Debug("Error setting parent directory", ex);
                statusLbl.Text = previousDirectory;
#endif
                previousDirectory = path;
                log.Info("Error setting parent directory on " + path);
                previousDirectory = null;
                lnkBack.Visible = false;
            }
            /*                
            int walkTask = await Task.FromResult<int>(walkDirectory(path));
            if (walkTask == 0)
                MessageBox.Show("Error enumerating directory structure.  Error has been logged to logs\\error-log.txt");
            */
            txtManualScan.Enabled = false;
            btnManulScan.Enabled = false;
            bw.RunWorkerAsync();
            groupTools.Enabled = true;
            groupData.Enabled = true;
            groupTools.Cursor = Cursors.Default;
            groupData.Cursor = Cursors.Default;
            //lblRunTime.Text = "Runtime: " + timer.ElapsedMilliseconds.ToString();
        }
        //Background worker enumerator

        private int walkDirectory(string dir)
        {
            try {
                groupData.Text = dir;
                groupTools.Enabled = false;
                groupData.Enabled = false;
                groupTools.Cursor = Cursors.WaitCursor;
                groupData.Cursor = Cursors.WaitCursor;
                //Turn off auto width and manually assign
                dgEnumerator.Columns[0].Width = 30;
                //List<FileFolderInfo> items = Enumerator.enumerateDirNew(dir);
                List<FileFolderInfo> dirs = Enumerator.getDirectories(dir);
                List<FileFolderInfo> files = Enumerator.getRootFiles(dir);
                //need to loop through this
                foreach (FileFolderInfo d in dirs)
                {
                    int rowIndex;
                    Image icon = icon = Properties.Resources.closed_folder;
                    Dictionary<double, string> fileSize = Shared.sizeSuffix(d.size);
                    var fileSizeItem = fileSize.First();
                    double roundFileSize = Shared.Ceil(fileSizeItem.Key, 2);
                    rowIndex = dgEnumerator.Rows.Add(icon, d.name, roundFileSize + " " + fileSizeItem.Value);
                    string tag = d.name + "|0|B|1|B|0|B";
                    dgEnumerator.Rows[rowIndex].Tag = tag;
                }
                foreach (FileFolderInfo f in files)
                {
                    int rowIndex;
                    Image icon;
                    try {

                        if (f.type == "dir")
                        {
                            icon = Properties.Resources.closed_folder;
                        } else
                        {
                            icon = Shared.getIconForFileType(f.ext);
                        }
                        Color fontColor = (f.accessible == 1) ? Color.Black : Color.Red;
                        Dictionary<double, string> fileSize = Shared.sizeSuffix(f.size);
                        var fileSizeItem = fileSize.First();
                        double roundFileSize = Shared.Ceil(fileSizeItem.Key, 2);
                        rowIndex = dgEnumerator.Rows.Add(icon, f.name, roundFileSize + " " + fileSizeItem.Value);
                        dgEnumerator.Rows[rowIndex].DefaultCellStyle.ForeColor = fontColor;
                        string tag = f.name + "|0|B|1|B|0|B";
                        dgEnumerator.Rows[rowIndex].Tag = tag;
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        icon = Shared.getIconForFileType(".err");
                        rowIndex = dgEnumerator.Rows.Add(icon, f.name, "0 bytes");
                        dgEnumerator.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                        string tag = f.name + "|0|B|1|B|0|B";
                        dgEnumerator.Rows[rowIndex].Tag = tag;
                    } catch (Exception ex)
                    {
                        log.Error("Error A-12", ex);
                    }
                }
                return 1;
            }

            catch (Exception ex)
            {
                log.Error("Error walking directory " + dir, ex);
                return 0;
            }

        }

        private void lnkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkBack();
        }

        private void linkBack()
        {
            if (!String.IsNullOrEmpty(previousDirectory) && previousDirectory != "main")
            {
                taskWalkDirectory(previousDirectory);
            }
            else if (previousDirectory == "main")
            {
                lblActionTitle.Text = "Loading...";
                listDrivesAsync();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Check for mouse back button
            if (e.Button == MouseButtons.XButton1)
            {
                if (lnkBack.Visible)
                    linkBack();
            }
        }
        private void delTimerUpdate()
        {
            if (this.lblRunTime.InvokeRequired)
                this.lblRunTime.Invoke((MethodInvoker)delegate { this.timerUpdate(); });
            else
                timerUpdate();
        }
        private void timerUpdate()
        {
            string formattedTimer = Shared.secondsToMinutes(runTimeInSeconds);
            lblRunTime.Text = "Run Time: " + formattedTimer;
        }

        private void delLnkStopScanChange(bool visible, bool enabled, string text)
        {
            if (this.lnkStopScan.InvokeRequired)
                this.lnkStopScan.Invoke((MethodInvoker)delegate { this.lnkStopScanChange(visible, enabled, text); });
            else
                this.lnkStopScanChange(visible, enabled, text);
        }
        private void lnkStopScanChange(bool visible, bool enabled, string text)
        {
            lnkStopScan.Visible = visible;
            lnkStopScan.Enabled = enabled;
            lnkStopScan.Text = text;
        }

        private void lnkStopScan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            bw.CancelAsync();
            lnkStopScan.Enabled = false;
            lnkStopScan.Text = "Stopping...";
        }       
        
        private void dgEnumeratorManualSort(string dir, int columnIndex, string columnName, string sortByColumnName, ListSortDirection defaultSort)
        {
            //Find out current sort order and reverse
            DataGridViewColumn oldColumn = dgEnumerator.SortedColumn;
            ListSortDirection sortDirection = defaultSort;          
            if (dir == "auto")
            {                
                if (dgEnumerator.SortOrder == SortOrder.None)
                {
                    sortDirection = defaultSort;
                }
                if (dgEnumerator.SortOrder == SortOrder.Descending)
                {
                    sortDirection = ListSortDirection.Ascending;
                }
                else
                {
                    sortDirection = ListSortDirection.Descending;
                }                
            } else if (dir == "manual")
            {
                sortDirection = defaultSort;
            }
            try {
                dgEnumerator.Sort(dgEnumerator.Columns[sortByColumnName], sortDirection);
                dgEnumerator.Columns[columnIndex].HeaderCell.SortGlyphDirection = (sortDirection == ListSortDirection.Ascending) ? SortOrder.Ascending : SortOrder.Descending;
            }
            catch (Exception ex)
            {
                log.Error("Error sorting column " + columnName + " at index " + columnIndex, ex);
            }
        } 

        private void dgEnumerator_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgEnumerator.Columns[e.ColumnIndex].Name.ToString();            
            //If sort column is the Size column, use the hidden AcutalSize column to perform the sort.
            //Cannot sort on Size column since it is a string with byte size appended
            if (columnName == "Size")
            {
                dgEnumeratorManualSort("auto", e.ColumnIndex, columnName, "ActualSize", ListSortDirection.Descending);                
            }
            else if (columnName == "FileType")
            {
                dgEnumeratorManualSort("auto", e.ColumnIndex, columnName, "FileExt", ListSortDirection.Ascending);                
            }
        }

        private void btnManulScan_Click(object sender, EventArgs e)
        {
            string manualDir = @txtManualScan.Text;
            if (Directory.Exists(manualDir))
            {
#if DEBUG
                log.Debug("Starting manual scan of " + manualDir);
#endif
                setupMainGrid("enumerate");
                currentWorkingDirectory = manualDir;
                taskWalkDirectory(manualDir);
            }
            else
            {
                statusLbl.Text = "Error: directory does not eixst";
                MessageBox.Show("Error: The specified directory does not exist");
                log.Info("Error on Manual Scan: The specified directory does not exist. [" + manualDir + "]");
            }
        }
    }
}
