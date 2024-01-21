namespace winforms_custom_web_server
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
            components = new System.ComponentModel.Container();
            PythonBtn = new Button();
            ShowBrowserBtn = new Button();
            Memo1 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // PythonBtn
            // 
            PythonBtn.Dock = DockStyle.Top;
            PythonBtn.Location = new Point(10, 10);
            PythonBtn.Name = "PythonBtn";
            PythonBtn.Size = new Size(264, 23);
            PythonBtn.TabIndex = 0;
            PythonBtn.Text = "1. Run python script";
            PythonBtn.UseVisualStyleBackColor = true;
            PythonBtn.Click += PythonBtn_Click;
            // 
            // ShowBrowserBtn
            // 
            ShowBrowserBtn.Enabled = false;
            ShowBrowserBtn.Location = new Point(10, 39);
            ShowBrowserBtn.Name = "ShowBrowserBtn";
            ShowBrowserBtn.Size = new Size(264, 23);
            ShowBrowserBtn.TabIndex = 1;
            ShowBrowserBtn.Text = "2. Show browser";
            ShowBrowserBtn.UseVisualStyleBackColor = true;
            ShowBrowserBtn.Click += ShowBrowserBtn_Click;
            // 
            // Memo1
            // 
            Memo1.Dock = DockStyle.Bottom;
            Memo1.Location = new Point(10, 70);
            Memo1.Multiline = true;
            Memo1.Name = "Memo1";
            Memo1.ReadOnly = true;
            Memo1.ScrollBars = ScrollBars.Vertical;
            Memo1.Size = new Size(264, 148);
            Memo1.TabIndex = 2;
            // 
            // timer1
            // 
            timer1.Interval = 250;
            timer1.Tick += timer1_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 228);
            Controls.Add(Memo1);
            Controls.Add(ShowBrowserBtn);
            Controls.Add(PythonBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "custom_web_server";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button PythonBtn;
        private Button ShowBrowserBtn;
        private TextBox Memo1;
        private System.Windows.Forms.Timer timer1;
    }
}
