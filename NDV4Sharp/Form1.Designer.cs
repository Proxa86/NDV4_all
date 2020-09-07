namespace NDV4Sharp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenFolderSrc = new System.Windows.Forms.Button();
            this.buttonOpenFolderBin = new System.Windows.Forms.Button();
            this.labelInformation = new System.Windows.Forms.Label();
            this.buttonExcel = new System.Windows.Forms.Button();
            this.listBoxReport = new System.Windows.Forms.ListBox();
            this.bInsertMarker = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenFolderSrc
            // 
            this.buttonOpenFolderSrc.Location = new System.Drawing.Point(8, 60);
            this.buttonOpenFolderSrc.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenFolderSrc.Name = "buttonOpenFolderSrc";
            this.buttonOpenFolderSrc.Size = new System.Drawing.Size(116, 28);
            this.buttonOpenFolderSrc.TabIndex = 0;
            this.buttonOpenFolderSrc.Text = "Choose source";
            this.buttonOpenFolderSrc.UseVisualStyleBackColor = true;
            // 
            // buttonOpenFolderBin
            // 
            this.buttonOpenFolderBin.Location = new System.Drawing.Point(8, 23);
            this.buttonOpenFolderBin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenFolderBin.Name = "buttonOpenFolderBin";
            this.buttonOpenFolderBin.Size = new System.Drawing.Size(116, 28);
            this.buttonOpenFolderBin.TabIndex = 1;
            this.buttonOpenFolderBin.Text = "Start analisys";
            this.buttonOpenFolderBin.UseVisualStyleBackColor = true;
            this.buttonOpenFolderBin.Click += new System.EventHandler(this.buttonOpenFolderBin_Click);
            // 
            // labelInformation
            // 
            this.labelInformation.AutoSize = true;
            this.labelInformation.Location = new System.Drawing.Point(17, 250);
            this.labelInformation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(0, 17);
            this.labelInformation.TabIndex = 2;
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(8, 68);
            this.buttonExcel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonExcel.Size = new System.Drawing.Size(133, 28);
            this.buttonExcel.TabIndex = 3;
            this.buttonExcel.Text = "OpenReportExcel";
            this.buttonExcel.UseVisualStyleBackColor = true;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // listBoxReport
            // 
            this.listBoxReport.FormattingEnabled = true;
            this.listBoxReport.ItemHeight = 16;
            this.listBoxReport.Location = new System.Drawing.Point(8, 23);
            this.listBoxReport.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxReport.Name = "listBoxReport";
            this.listBoxReport.Size = new System.Drawing.Size(384, 36);
            this.listBoxReport.TabIndex = 4;
            // 
            // bInsertMarker
            // 
            this.bInsertMarker.Location = new System.Drawing.Point(8, 23);
            this.bInsertMarker.Margin = new System.Windows.Forms.Padding(4);
            this.bInsertMarker.Name = "bInsertMarker";
            this.bInsertMarker.Size = new System.Drawing.Size(100, 28);
            this.bInsertMarker.TabIndex = 5;
            this.bInsertMarker.Text = "Insert marker";
            this.bInsertMarker.UseVisualStyleBackColor = true;
            this.bInsertMarker.Click += new System.EventHandler(this.buttonInsertMarker_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createStripMenuItem,
            this.openStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(439, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createStripMenuItem
            // 
            this.createStripMenuItem.Name = "createStripMenuItem";
            this.createStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.createStripMenuItem.Text = "Create Project";
            // 
            // openStripMenuItem
            // 
            this.openStripMenuItem.Name = "openStripMenuItem";
            this.openStripMenuItem.Size = new System.Drawing.Size(109, 24);
            this.openStripMenuItem.Text = "Open Project";
            this.openStripMenuItem.Click += new System.EventHandler(this.openStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonOpenFolderSrc);
            this.groupBox1.Controls.Add(this.buttonOpenFolderBin);
            this.groupBox1.Location = new System.Drawing.Point(291, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(139, 111);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Check marker";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxReport);
            this.groupBox2.Controls.Add(this.buttonExcel);
            this.groupBox2.Location = new System.Drawing.Point(21, 166);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(408, 113);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open report";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bInsertMarker);
            this.groupBox3.Location = new System.Drawing.Point(16, 36);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(267, 123);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Insert marker";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 290);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFolderSrc;
        private System.Windows.Forms.Button buttonOpenFolderBin;
        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.Button buttonExcel;
        private System.Windows.Forms.ListBox listBoxReport;
        private System.Windows.Forms.Button bInsertMarker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

