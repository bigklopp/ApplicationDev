
namespace ApplicationDev
{
    partial class FM_PassWord
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.TxtPrevPW = new System.Windows.Forms.TextBox();
            this.TxtChgPW = new System.Windows.Forms.TextBox();
            this.btnChgPW = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "사용자 ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "이전 PW";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "변경 PW";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(182, 66);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(267, 27);
            this.txtID.TabIndex = 3;
            // 
            // TxtPrevPW
            // 
            this.TxtPrevPW.Location = new System.Drawing.Point(182, 134);
            this.TxtPrevPW.Name = "TxtPrevPW";
            this.TxtPrevPW.Size = new System.Drawing.Size(266, 27);
            this.TxtPrevPW.TabIndex = 4;
            // 
            // TxtChgPW
            // 
            this.TxtChgPW.Location = new System.Drawing.Point(182, 198);
            this.TxtChgPW.Name = "TxtChgPW";
            this.TxtChgPW.Size = new System.Drawing.Size(267, 27);
            this.TxtChgPW.TabIndex = 5;
            // 
            // btnChgPW
            // 
            this.btnChgPW.Location = new System.Drawing.Point(208, 273);
            this.btnChgPW.Name = "btnChgPW";
            this.btnChgPW.Size = new System.Drawing.Size(101, 68);
            this.btnChgPW.TabIndex = 6;
            this.btnChgPW.Text = "변경 등록";
            this.btnChgPW.UseVisualStyleBackColor = true;
            this.btnChgPW.Click += new System.EventHandler(this.btnChgPW_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(343, 273);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 68);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FM_PassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 373);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnChgPW);
            this.Controls.Add(this.TxtChgPW);
            this.Controls.Add(this.TxtPrevPW);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FM_PassWord";
            this.Text = "비밀번호 변경";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox TxtPrevPW;
        private System.Windows.Forms.TextBox TxtChgPW;
        private System.Windows.Forms.Button btnChgPW;
        private System.Windows.Forms.Button btnClose;
    }
}