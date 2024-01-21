namespace winforms_serve_a_folder
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
            ShowBrowserBtn = new Button();
            Memo1 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // ShowBrowserBtn
            // 
            ShowBrowserBtn.Dock = DockStyle.Top;
            ShowBrowserBtn.Location = new Point(10, 10);
            ShowBrowserBtn.Name = "ShowBrowserBtn";
            ShowBrowserBtn.Size = new Size(288, 23);
            ShowBrowserBtn.TabIndex = 0;
            ShowBrowserBtn.Text = "Show browser";
            ShowBrowserBtn.UseVisualStyleBackColor = true;
            ShowBrowserBtn.Click += ShowBrowserBtn_Click;
            // 
            // Memo1
            // 
            Memo1.Dock = DockStyle.Bottom;
            Memo1.Location = new Point(10, 41);
            Memo1.Multiline = true;
            Memo1.Name = "Memo1";
            Memo1.ScrollBars = ScrollBars.Vertical;
            Memo1.Size = new Size(288, 204);
            Memo1.TabIndex = 1;
            // 
            // timer1
            // 
            timer1.Interval = 250;
            timer1.Tick += this.timer1_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 255);
            Controls.Add(Memo1);
            Controls.Add(ShowBrowserBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "serve_a_folder";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ShowBrowserBtn;
        private TextBox Memo1;
        private System.Windows.Forms.Timer timer1;
    }
}
