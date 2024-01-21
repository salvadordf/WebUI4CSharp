namespace winforms_public_network_access
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ShowBrowserBtn = new Button();
            OpenDefBrowserBtn = new Button();
            SuspendLayout();
            // 
            // ShowBrowserBtn
            // 
            ShowBrowserBtn.Dock = DockStyle.Top;
            ShowBrowserBtn.Location = new Point(10, 10);
            ShowBrowserBtn.Name = "ShowBrowserBtn";
            ShowBrowserBtn.Size = new Size(244, 23);
            ShowBrowserBtn.TabIndex = 0;
            ShowBrowserBtn.Text = "1. Show browser";
            ShowBrowserBtn.UseVisualStyleBackColor = true;
            ShowBrowserBtn.Click += ShowBrowserBtn_Click;
            // 
            // OpenDefBrowserBtn
            // 
            OpenDefBrowserBtn.Dock = DockStyle.Bottom;
            OpenDefBrowserBtn.Enabled = false;
            OpenDefBrowserBtn.Location = new Point(10, 40);
            OpenDefBrowserBtn.Name = "OpenDefBrowserBtn";
            OpenDefBrowserBtn.Size = new Size(244, 23);
            OpenDefBrowserBtn.TabIndex = 1;
            OpenDefBrowserBtn.Text = "2. Open default web browser";
            OpenDefBrowserBtn.UseVisualStyleBackColor = true;
            OpenDefBrowserBtn.Click += this.OpenDefBrowserBtn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 73);
            Controls.Add(OpenDefBrowserBtn);
            Controls.Add(ShowBrowserBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "public_network_access";
            ResumeLayout(false);
        }

        #endregion

        private Button ShowBrowserBtn;
        private Button OpenDefBrowserBtn;
    }
}
