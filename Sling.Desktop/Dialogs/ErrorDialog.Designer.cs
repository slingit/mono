namespace Sling.Desktop.Dialogs
{
    partial class ErrorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialog));
            this.btnIgnore = new System.Windows.Forms.Button();
            this.chkPlatform = new System.Windows.Forms.CheckBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(12, 426);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(75, 23);
            this.btnIgnore.TabIndex = 0;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // chkPlatform
            // 
            this.chkPlatform.AutoSize = true;
            this.chkPlatform.Checked = true;
            this.chkPlatform.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlatform.Location = new System.Drawing.Point(93, 430);
            this.chkPlatform.Name = "chkPlatform";
            this.chkPlatform.Size = new System.Drawing.Size(91, 17);
            this.chkPlatform.TabIndex = 1;
            this.chkPlatform.Text = "Send platform";
            this.chkPlatform.UseVisualStyleBackColor = true;
            this.chkPlatform.CheckedChanged += new System.EventHandler(this.chkPlatform_CheckedChanged);
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(12, 73);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.Size = new System.Drawing.Size(560, 347);
            this.txtError.TabIndex = 2;
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.Location = new System.Drawing.Point(12, 9);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(560, 61);
            this.lblDisclaimer.TabIndex = 3;
            this.lblDisclaimer.Text = resources.GetString("lblDisclaimer.Text");
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(497, 426);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblDisclaimer);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.chkPlatform);
            this.Controls.Add(this.btnIgnore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.CheckBox chkPlatform;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Label lblDisclaimer;
        private System.Windows.Forms.Button btnSend;
    }
}