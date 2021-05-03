namespace QuanLyVCS
{
    partial class HeThong
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.quảnLýTeamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýGameThủToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýHLVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýTeamToolStripMenuItem,
            this.quảnLýGameThủToolStripMenuItem,
            this.quảnLýHLVToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(560, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // quảnLýTeamToolStripMenuItem
            // 
            this.quảnLýTeamToolStripMenuItem.Name = "quảnLýTeamToolStripMenuItem";
            this.quảnLýTeamToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.quảnLýTeamToolStripMenuItem.Text = "Quản lý team";
            this.quảnLýTeamToolStripMenuItem.Click += new System.EventHandler(this.quảnLýTeamToolStripMenuItem_Click);
            // 
            // quảnLýGameThủToolStripMenuItem
            // 
            this.quảnLýGameThủToolStripMenuItem.Name = "quảnLýGameThủToolStripMenuItem";
            this.quảnLýGameThủToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.quảnLýGameThủToolStripMenuItem.Text = "Quản lý game thủ";
            this.quảnLýGameThủToolStripMenuItem.Click += new System.EventHandler(this.quảnLýGameThủToolStripMenuItem_Click);
            // 
            // quảnLýHLVToolStripMenuItem
            // 
            this.quảnLýHLVToolStripMenuItem.Name = "quảnLýHLVToolStripMenuItem";
            this.quảnLýHLVToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.quảnLýHLVToolStripMenuItem.Text = "Hợp đồng game thủ";
            this.quảnLýHLVToolStripMenuItem.Click += new System.EventHandler(this.quảnLýHLVToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QuanLyVCS.Properties.Resources.tải_xuống__1_;
            this.pictureBox1.Location = new System.Drawing.Point(5, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(549, 283);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // HeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 311);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HeThong";
            this.Text = "HeThong";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quảnLýTeamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýGameThủToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýHLVToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}