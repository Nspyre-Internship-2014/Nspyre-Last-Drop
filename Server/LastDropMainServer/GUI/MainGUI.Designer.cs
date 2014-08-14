namespace LastDropMainServer
{
    partial class MainGUI
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
            this.serviceLaunchButton = new System.Windows.Forms.Button();
            this.statusTextBox = new System.Windows.Forms.RichTextBox();
            this.editDatabaseButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serviceLaunchButton
            // 
            this.serviceLaunchButton.Location = new System.Drawing.Point(40, 478);
            this.serviceLaunchButton.Name = "serviceLaunchButton";
            this.serviceLaunchButton.Size = new System.Drawing.Size(86, 23);
            this.serviceLaunchButton.TabIndex = 1;
            this.serviceLaunchButton.Text = "Start Services";
            this.serviceLaunchButton.UseVisualStyleBackColor = true;
            this.serviceLaunchButton.Click += new System.EventHandler(this.serviceLaunchButton_Click);
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(12, 12);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(358, 460);
            this.statusTextBox.TabIndex = 2;
            this.statusTextBox.Text = "";
            // 
            // editDatabaseButton
            // 
            this.editDatabaseButton.Location = new System.Drawing.Point(126, 478);
            this.editDatabaseButton.Name = "editDatabaseButton";
            this.editDatabaseButton.Size = new System.Drawing.Size(80, 23);
            this.editDatabaseButton.TabIndex = 3;
            this.editDatabaseButton.Text = "Edit Plants";
            this.editDatabaseButton.UseVisualStyleBackColor = true;
            this.editDatabaseButton.Click += new System.EventHandler(this.editDatabaseButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 478);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Remove User Credentials";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 513);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.editDatabaseButton);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.serviceLaunchButton);
            this.Name = "MainGUI";
            this.Text = "Last Drop Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainGUI_FormClosing);
            this.Load += new System.EventHandler(this.MainGUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button serviceLaunchButton;
        public System.Windows.Forms.RichTextBox statusTextBox;
        private System.Windows.Forms.Button editDatabaseButton;
        private System.Windows.Forms.Button button1;

    }
}