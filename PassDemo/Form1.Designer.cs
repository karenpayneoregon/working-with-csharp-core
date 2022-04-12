
namespace PassDemo
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
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.ToggleCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(50, 29);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.PasswordTextBox.TabIndex = 0;
            // 
            // ToggleCheckBox
            // 
            this.ToggleCheckBox.AutoSize = true;
            this.ToggleCheckBox.Location = new System.Drawing.Point(165, 32);
            this.ToggleCheckBox.Name = "ToggleCheckBox";
            this.ToggleCheckBox.Size = new System.Drawing.Size(80, 17);
            this.ToggleCheckBox.TabIndex = 1;
            this.ToggleCheckBox.Text = "checkBox1";
            this.ToggleCheckBox.UseVisualStyleBackColor = true;
            this.ToggleCheckBox.CheckedChanged += new System.EventHandler(this.ToggleCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 230);
            this.Controls.Add(this.ToggleCheckBox);
            this.Controls.Add(this.PasswordTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.CheckBox ToggleCheckBox;
    }
}

