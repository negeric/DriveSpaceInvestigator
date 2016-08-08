namespace BlackHole
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dgEnumerator = new System.Windows.Forms.DataGridView();
            this.lblActionTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.txtIconStartSpacer = new System.Windows.Forms.TextBox();
            this.txtStartSettingsSpacer = new System.Windows.Forms.TextBox();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.pnlApp = new System.Windows.Forms.Panel();
            this.pnlCurrentDisk = new System.Windows.Forms.Panel();
            this.chartDriveUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalDriveSpace = new System.Windows.Forms.Label();
            this.lblUsedDriveSpace = new System.Windows.Forms.Label();
            this.lblFreeDriveSpace = new System.Windows.Forms.Label();
            this.groupTools = new System.Windows.Forms.GroupBox();
            this.groupDriveInfo = new System.Windows.Forms.GroupBox();
            this.groupData = new System.Windows.Forms.GroupBox();
            this.lnkBack = new System.Windows.Forms.LinkLabel();
            this.pictureAppStart = new System.Windows.Forms.PictureBox();
            this.pictureAppSettings = new System.Windows.Forms.PictureBox();
            this.pictureAppLogo = new System.Windows.Forms.PictureBox();
            this.groupStatus = new System.Windows.Forms.GroupBox();
            this.lblRunTime = new System.Windows.Forms.Label();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnumerator)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.pnlApp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDriveUsage)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupDriveInfo.SuspendLayout();
            this.groupData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppLogo)).BeginInit();
            this.groupStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgEnumerator
            // 
            this.dgEnumerator.AllowUserToAddRows = false;
            this.dgEnumerator.AllowUserToDeleteRows = false;
            this.dgEnumerator.AllowUserToResizeRows = false;
            this.dgEnumerator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgEnumerator.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgEnumerator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEnumerator.Location = new System.Drawing.Point(6, 18);
            this.dgEnumerator.MultiSelect = false;
            this.dgEnumerator.Name = "dgEnumerator";
            this.dgEnumerator.ReadOnly = true;
            this.dgEnumerator.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEnumerator.Size = new System.Drawing.Size(744, 414);
            this.dgEnumerator.TabIndex = 0;
            this.dgEnumerator.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnumerator_CellClick);
            this.dgEnumerator.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnumerator_CellDoubleClick);
            this.dgEnumerator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // lblActionTitle
            // 
            this.lblActionTitle.AutoSize = true;
            this.lblActionTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionTitle.Location = new System.Drawing.Point(6, 108);
            this.lblActionTitle.Name = "lblActionTitle";
            this.lblActionTitle.Size = new System.Drawing.Size(77, 27);
            this.lblActionTitle.TabIndex = 1;
            this.lblActionTitle.Text = "label1";
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.BackColor = System.Drawing.SystemColors.ControlText;
            this.panelHeader.Controls.Add(this.txtIconStartSpacer);
            this.panelHeader.Controls.Add(this.pictureAppStart);
            this.panelHeader.Controls.Add(this.pnlCurrentDisk);
            this.panelHeader.Controls.Add(this.txtStartSettingsSpacer);
            this.panelHeader.Controls.Add(this.pictureAppSettings);
            this.panelHeader.Controls.Add(this.lblAppTitle);
            this.panelHeader.Controls.Add(this.pictureAppLogo);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(986, 100);
            this.panelHeader.TabIndex = 2;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // txtIconStartSpacer
            // 
            this.txtIconStartSpacer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIconStartSpacer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIconStartSpacer.Location = new System.Drawing.Point(780, 0);
            this.txtIconStartSpacer.Multiline = true;
            this.txtIconStartSpacer.Name = "txtIconStartSpacer";
            this.txtIconStartSpacer.Size = new System.Drawing.Size(1, 100);
            this.txtIconStartSpacer.TabIndex = 5;
            // 
            // txtStartSettingsSpacer
            // 
            this.txtStartSettingsSpacer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartSettingsSpacer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartSettingsSpacer.Location = new System.Drawing.Point(882, 0);
            this.txtStartSettingsSpacer.Multiline = true;
            this.txtStartSettingsSpacer.Name = "txtStartSettingsSpacer";
            this.txtStartSettingsSpacer.Size = new System.Drawing.Size(1, 100);
            this.txtStartSettingsSpacer.TabIndex = 3;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Agency FB", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblAppTitle.Location = new System.Drawing.Point(115, 29);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(277, 42);
            this.lblAppTitle.TabIndex = 1;
            this.lblAppTitle.Text = "Drive Space Investigator";
            this.lblAppTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // pnlApp
            // 
            this.pnlApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlApp.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlApp.Controls.Add(this.groupStatus);
            this.pnlApp.Controls.Add(this.groupData);
            this.pnlApp.Controls.Add(this.groupDriveInfo);
            this.pnlApp.Controls.Add(this.groupTools);
            this.pnlApp.Location = new System.Drawing.Point(0, 142);
            this.pnlApp.Name = "pnlApp";
            this.pnlApp.Size = new System.Drawing.Size(986, 445);
            this.pnlApp.TabIndex = 3;
            this.pnlApp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // pnlCurrentDisk
            // 
            this.pnlCurrentDisk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCurrentDisk.Location = new System.Drawing.Point(300, 106);
            this.pnlCurrentDisk.Name = "pnlCurrentDisk";
            this.pnlCurrentDisk.Size = new System.Drawing.Size(217, 223);
            this.pnlCurrentDisk.TabIndex = 1;
            // 
            // chartDriveUsage
            // 
            this.chartDriveUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartDriveUsage.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.chartDriveUsage.Legends.Add(legend1);
            this.chartDriveUsage.Location = new System.Drawing.Point(7, 19);
            this.chartDriveUsage.Name = "chartDriveUsage";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDriveUsage.Series.Add(series1);
            this.chartDriveUsage.Size = new System.Drawing.Size(204, 162);
            this.chartDriveUsage.TabIndex = 3;
            this.chartDriveUsage.Text = "chart1";
            this.chartDriveUsage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(118, 17);
            this.statusLbl.Text = "toolStripStatusLabel1";
            // 
            // lblTotalDriveSpace
            // 
            this.lblTotalDriveSpace.AutoSize = true;
            this.lblTotalDriveSpace.Location = new System.Drawing.Point(7, 170);
            this.lblTotalDriveSpace.Name = "lblTotalDriveSpace";
            this.lblTotalDriveSpace.Size = new System.Drawing.Size(0, 13);
            this.lblTotalDriveSpace.TabIndex = 4;
            // 
            // lblUsedDriveSpace
            // 
            this.lblUsedDriveSpace.AutoSize = true;
            this.lblUsedDriveSpace.Location = new System.Drawing.Point(7, 188);
            this.lblUsedDriveSpace.Name = "lblUsedDriveSpace";
            this.lblUsedDriveSpace.Size = new System.Drawing.Size(0, 13);
            this.lblUsedDriveSpace.TabIndex = 5;
            // 
            // lblFreeDriveSpace
            // 
            this.lblFreeDriveSpace.AutoSize = true;
            this.lblFreeDriveSpace.Location = new System.Drawing.Point(7, 207);
            this.lblFreeDriveSpace.Name = "lblFreeDriveSpace";
            this.lblFreeDriveSpace.Size = new System.Drawing.Size(130, 13);
            this.lblFreeDriveSpace.TabIndex = 6;
            this.lblFreeDriveSpace.Text = "Select a Drive to continue";
            // 
            // groupTools
            // 
            this.groupTools.Location = new System.Drawing.Point(3, 232);
            this.groupTools.Name = "groupTools";
            this.groupTools.Size = new System.Drawing.Size(217, 123);
            this.groupTools.TabIndex = 2;
            this.groupTools.TabStop = false;
            this.groupTools.Text = "Tools";
            // 
            // groupDriveInfo
            // 
            this.groupDriveInfo.Controls.Add(this.lblFreeDriveSpace);
            this.groupDriveInfo.Controls.Add(this.lblTotalDriveSpace);
            this.groupDriveInfo.Controls.Add(this.lblUsedDriveSpace);
            this.groupDriveInfo.Controls.Add(this.chartDriveUsage);
            this.groupDriveInfo.Location = new System.Drawing.Point(3, 3);
            this.groupDriveInfo.Name = "groupDriveInfo";
            this.groupDriveInfo.Size = new System.Drawing.Size(217, 223);
            this.groupDriveInfo.TabIndex = 3;
            this.groupDriveInfo.TabStop = false;
            this.groupDriveInfo.Text = "Drive Info";
            // 
            // groupData
            // 
            this.groupData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupData.Controls.Add(this.dgEnumerator);
            this.groupData.Location = new System.Drawing.Point(227, 4);
            this.groupData.Name = "groupData";
            this.groupData.Size = new System.Drawing.Size(756, 438);
            this.groupData.TabIndex = 4;
            this.groupData.TabStop = false;
            this.groupData.Text = "Select a Drive";
            // 
            // lnkBack
            // 
            this.lnkBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkBack.AutoSize = true;
            this.lnkBack.Location = new System.Drawing.Point(888, 115);
            this.lnkBack.Name = "lnkBack";
            this.lnkBack.Size = new System.Drawing.Size(85, 13);
            this.lnkBack.TabIndex = 5;
            this.lnkBack.TabStop = true;
            this.lnkBack.Text = "Up one directory";
            this.lnkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBack_LinkClicked);
            // 
            // pictureAppStart
            // 
            this.pictureAppStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureAppStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAppStart.Image = global::BlackHole.Properties.Resources.search_icon;
            this.pictureAppStart.Location = new System.Drawing.Point(782, 0);
            this.pictureAppStart.Name = "pictureAppStart";
            this.pictureAppStart.Size = new System.Drawing.Size(100, 100);
            this.pictureAppStart.TabIndex = 4;
            this.pictureAppStart.TabStop = false;
            // 
            // pictureAppSettings
            // 
            this.pictureAppSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureAppSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureAppSettings.Image = global::BlackHole.Properties.Resources.settings_icon;
            this.pictureAppSettings.Location = new System.Drawing.Point(883, 0);
            this.pictureAppSettings.Name = "pictureAppSettings";
            this.pictureAppSettings.Size = new System.Drawing.Size(100, 100);
            this.pictureAppSettings.TabIndex = 2;
            this.pictureAppSettings.TabStop = false;
            // 
            // pictureAppLogo
            // 
            this.pictureAppLogo.Image = global::BlackHole.Properties.Resources.atom_icon;
            this.pictureAppLogo.Location = new System.Drawing.Point(11, 4);
            this.pictureAppLogo.Name = "pictureAppLogo";
            this.pictureAppLogo.Size = new System.Drawing.Size(98, 92);
            this.pictureAppLogo.TabIndex = 0;
            this.pictureAppLogo.TabStop = false;
            this.pictureAppLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // groupStatus
            // 
            this.groupStatus.Controls.Add(this.lblItem);
            this.groupStatus.Controls.Add(this.lblCurrentStatus);
            this.groupStatus.Controls.Add(this.lblRunTime);
            this.groupStatus.Location = new System.Drawing.Point(3, 361);
            this.groupStatus.Name = "groupStatus";
            this.groupStatus.Size = new System.Drawing.Size(217, 75);
            this.groupStatus.TabIndex = 5;
            this.groupStatus.TabStop = false;
            this.groupStatus.Text = "Status";
            // 
            // lblRunTime
            // 
            this.lblRunTime.AutoSize = true;
            this.lblRunTime.Location = new System.Drawing.Point(7, 37);
            this.lblRunTime.Name = "lblRunTime";
            this.lblRunTime.Size = new System.Drawing.Size(65, 13);
            this.lblRunTime.TabIndex = 0;
            this.lblRunTime.Text = "Run Time: 0";
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.Location = new System.Drawing.Point(7, 20);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(60, 13);
            this.lblCurrentStatus.TabIndex = 1;
            this.lblCurrentStatus.Text = "Status: Idle";
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Location = new System.Drawing.Point(7, 54);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(30, 13);
            this.lblItem.TabIndex = 2;
            this.lblItem.Text = "Item:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.lnkBack);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlApp);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblActionTitle);
            this.Name = "Form1";
            this.Text = "Disk Space Utilization";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgEnumerator)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.pnlApp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDriveUsage)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupDriveInfo.ResumeLayout(false);
            this.groupDriveInfo.PerformLayout();
            this.groupData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppLogo)).EndInit();
            this.groupStatus.ResumeLayout(false);
            this.groupStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgEnumerator;
        private System.Windows.Forms.Label lblActionTitle;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureAppLogo;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.PictureBox pictureAppStart;
        private System.Windows.Forms.TextBox txtStartSettingsSpacer;
        private System.Windows.Forms.PictureBox pictureAppSettings;
        private System.Windows.Forms.TextBox txtIconStartSpacer;
        private System.Windows.Forms.Panel pnlApp;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.Panel pnlCurrentDisk;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDriveUsage;
        private System.Windows.Forms.Label lblFreeDriveSpace;
        private System.Windows.Forms.Label lblUsedDriveSpace;
        private System.Windows.Forms.Label lblTotalDriveSpace;
        private System.Windows.Forms.GroupBox groupDriveInfo;
        private System.Windows.Forms.GroupBox groupTools;
        private System.Windows.Forms.GroupBox groupData;
        private System.Windows.Forms.LinkLabel lnkBack;
        private System.Windows.Forms.GroupBox groupStatus;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblRunTime;
    }
}

