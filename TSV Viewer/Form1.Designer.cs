﻿namespace MediaTracker
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
            this.SeriesBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SeasonBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FileLocation = new System.Windows.Forms.TextBox();
            this.ScanBtn = new System.Windows.Forms.Button();
            this.EpisodeFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // SeriesBox
            // 
            this.SeriesBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeriesBox.FormattingEnabled = true;
            this.SeriesBox.Location = new System.Drawing.Point(12, 32);
            this.SeriesBox.Name = "SeriesBox";
            this.SeriesBox.Size = new System.Drawing.Size(314, 28);
            this.SeriesBox.TabIndex = 0;
            this.SeriesBox.SelectedIndexChanged += new System.EventHandler(this.SeriesBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Show / Movie Series";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(655, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Season / Movie";
            // 
            // SeasonBox
            // 
            this.SeasonBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SeasonBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SeasonBox.FormattingEnabled = true;
            this.SeasonBox.Location = new System.Drawing.Point(332, 32);
            this.SeasonBox.Name = "SeasonBox";
            this.SeasonBox.Size = new System.Drawing.Size(440, 28);
            this.SeasonBox.TabIndex = 2;
            this.SeasonBox.SelectedIndexChanged += new System.EventHandler(this.SeasonBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 726);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Main Directory";
            // 
            // FileLocation
            // 
            this.FileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileLocation.Location = new System.Drawing.Point(135, 723);
            this.FileLocation.Name = "FileLocation";
            this.FileLocation.Size = new System.Drawing.Size(556, 26);
            this.FileLocation.TabIndex = 6;
            this.FileLocation.Text = "E:\\Shows";
            // 
            // ScanBtn
            // 
            this.ScanBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanBtn.Location = new System.Drawing.Point(697, 720);
            this.ScanBtn.Name = "ScanBtn";
            this.ScanBtn.Size = new System.Drawing.Size(75, 33);
            this.ScanBtn.TabIndex = 7;
            this.ScanBtn.Text = "Scan";
            this.ScanBtn.UseVisualStyleBackColor = true;
            this.ScanBtn.Click += new System.EventHandler(this.ScanBtn_Click);
            // 
            // EpisodeFlow
            // 
            this.EpisodeFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EpisodeFlow.AutoScroll = true;
            this.EpisodeFlow.Location = new System.Drawing.Point(12, 67);
            this.EpisodeFlow.Name = "EpisodeFlow";
            this.EpisodeFlow.Size = new System.Drawing.Size(760, 647);
            this.EpisodeFlow.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.EpisodeFlow);
            this.Controls.Add(this.ScanBtn);
            this.Controls.Add(this.FileLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SeasonBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SeriesBox);
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "Form1";
            this.Text = "Watched Media Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SeriesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SeasonBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FileLocation;
        private System.Windows.Forms.Button ScanBtn;
        private System.Windows.Forms.FlowLayoutPanel EpisodeFlow;
    }
}

