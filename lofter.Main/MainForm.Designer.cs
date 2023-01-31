namespace lofter.Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button_Start = new System.Windows.Forms.Button();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.textBox_TargetUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(12, 44);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(79, 29);
            this.button_Start.TabIndex = 0;
            this.button_Start.Text = "开始";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // textBox_Status
            // 
            this.textBox_Status.AcceptsReturn = true;
            this.textBox_Status.Location = new System.Drawing.Point(12, 79);
            this.textBox_Status.Multiline = true;
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Status.Size = new System.Drawing.Size(372, 184);
            this.textBox_Status.TabIndex = 1;
            // 
            // textBox_TargetUrl
            // 
            this.textBox_TargetUrl.Location = new System.Drawing.Point(12, 15);
            this.textBox_TargetUrl.Name = "textBox_TargetUrl";
            this.textBox_TargetUrl.Size = new System.Drawing.Size(362, 23);
            this.textBox_TargetUrl.TabIndex = 2;
            this.textBox_TargetUrl.Text = "https://siyechonger.lofter.com/";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 277);
            this.Controls.Add(this.textBox_TargetUrl);
            this.Controls.Add(this.textBox_Status);
            this.Controls.Add(this.button_Start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LofterEasyCrawler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.TextBox textBox_Status;
        private System.Windows.Forms.TextBox textBox_TargetUrl;
    }
}
