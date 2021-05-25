
namespace ApplicationDev
{
    partial class FM_Main
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.M_SYSTEM = new System.Windows.Forms.ToolStripMenuItem();
            this.MDI_TEST = new System.Windows.Forms.ToolStripMenuItem();
            this.MDI_TEST2 = new System.Windows.Forms.ToolStripMenuItem();
            this.FM_ITEM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.stbSearch = new System.Windows.Forms.ToolStripButton();
            this.stbInsert = new System.Windows.Forms.ToolStripButton();
            this.stbDelete = new System.Windows.Forms.ToolStripButton();
            this.stbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stbClose = new System.Windows.Forms.ToolStripButton();
            this.stbExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssNowDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.myTabControl1 = new ApplicationDev.MyTabControl();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.M_SYSTEM,
            this.toolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // M_SYSTEM
            // 
            this.M_SYSTEM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MDI_TEST,
            this.MDI_TEST2,
            this.FM_ITEM});
            this.M_SYSTEM.Name = "M_SYSTEM";
            this.M_SYSTEM.Size = new System.Drawing.Size(103, 24);
            this.M_SYSTEM.Text = "시스템 관리";
            // 
            // MDI_TEST
            // 
            this.MDI_TEST.Name = "MDI_TEST";
            this.MDI_TEST.Size = new System.Drawing.Size(167, 26);
            this.MDI_TEST.Text = "MDI_TEST";
            // 
            // MDI_TEST2
            // 
            this.MDI_TEST2.Name = "MDI_TEST2";
            this.MDI_TEST2.Size = new System.Drawing.Size(167, 26);
            this.MDI_TEST2.Text = "MDI_TEST2";
            // 
            // FM_ITEM
            // 
            this.FM_ITEM.Name = "FM_ITEM";
            this.FM_ITEM.Size = new System.Drawing.Size(167, 26);
            this.FM_ITEM.Text = "품목 관리";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 24);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbSearch,
            this.stbInsert,
            this.stbDelete,
            this.stbSave,
            this.toolStripSeparator1,
            this.stbClose,
            this.stbExit});
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 83);
            this.toolStrip.TabIndex = 1;
            // 
            // stbSearch
            // 
            this.stbSearch.Image = global::ApplicationDev.Properties.Resources.BtnSearch;
            this.stbSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbSearch.Name = "stbSearch";
            this.stbSearch.Size = new System.Drawing.Size(54, 80);
            this.stbSearch.Text = "조회";
            this.stbSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stbSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // stbInsert
            // 
            this.stbInsert.Image = global::ApplicationDev.Properties.Resources.BtnAdd;
            this.stbInsert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stbInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbInsert.Name = "stbInsert";
            this.stbInsert.Size = new System.Drawing.Size(54, 80);
            this.stbInsert.Text = "추가";
            this.stbInsert.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stbInsert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // stbDelete
            // 
            this.stbDelete.Image = global::ApplicationDev.Properties.Resources.BtnDelete;
            this.stbDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbDelete.Name = "stbDelete";
            this.stbDelete.Size = new System.Drawing.Size(54, 80);
            this.stbDelete.Text = "삭제";
            this.stbDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // stbSave
            // 
            this.stbSave.Image = global::ApplicationDev.Properties.Resources.BtnSaveB;
            this.stbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbSave.Name = "stbSave";
            this.stbSave.Size = new System.Drawing.Size(54, 80);
            this.stbSave.Text = "저장";
            this.stbSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 83);
            // 
            // stbClose
            // 
            this.stbClose.Image = global::ApplicationDev.Properties.Resources.BtnClose;
            this.stbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbClose.Name = "stbClose";
            this.stbClose.Size = new System.Drawing.Size(54, 80);
            this.stbClose.Text = "닫기";
            this.stbClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // stbExit
            // 
            this.stbExit.Image = global::ApplicationDev.Properties.Resources.BtcExit;
            this.stbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbExit.Name = "stbExit";
            this.stbExit.Size = new System.Drawing.Size(54, 80);
            this.stbExit.Text = "종료";
            this.stbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.stbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssSpace,
            this.tssUserName,
            this.tssNowDate});
            this.statusStrip.Location = new System.Drawing.Point(0, 424);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tssSpace
            // 
            this.tssSpace.AutoSize = false;
            this.tssSpace.Name = "tssSpace";
            this.tssSpace.Size = new System.Drawing.Size(185, 20);
            this.tssSpace.Spring = true;
            // 
            // tssUserName
            // 
            this.tssUserName.AutoSize = false;
            this.tssUserName.Name = "tssUserName";
            this.tssUserName.Size = new System.Drawing.Size(300, 20);
            this.tssUserName.Text = "toolStripStatusLabel2";
            // 
            // tssNowDate
            // 
            this.tssNowDate.AutoSize = false;
            this.tssNowDate.Name = "tssNowDate";
            this.tssNowDate.Size = new System.Drawing.Size(300, 20);
            this.tssNowDate.Text = "toolStripStatusLabel3";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // myTabControl1
            // 
            this.myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabControl1.Location = new System.Drawing.Point(0, 111);
            this.myTabControl1.Name = "myTabControl1";
            this.myTabControl1.SelectedIndex = 0;
            this.myTabControl1.Size = new System.Drawing.Size(800, 313);
            this.myTabControl1.TabIndex = 4;
            // 
            // FM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.myTabControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FM_Main";
            this.Text = "Application DEV V 1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem M_SYSTEM;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem MDI_TEST;
        private System.Windows.Forms.ToolStripButton stbSearch;
        private System.Windows.Forms.ToolStripButton stbInsert;
        private System.Windows.Forms.ToolStripButton stbDelete;
        private System.Windows.Forms.ToolStripButton stbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton stbClose;
        private System.Windows.Forms.ToolStripButton stbExit;
        private System.Windows.Forms.ToolStripStatusLabel tssSpace;
        private System.Windows.Forms.ToolStripStatusLabel tssUserName;
        private System.Windows.Forms.ToolStripStatusLabel tssNowDate;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem MDI_TEST2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private MyTabControl myTabControl1;
        private System.Windows.Forms.ToolStripMenuItem FM_ITEM;
    }
}