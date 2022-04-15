
namespace TryCatchExamples
{
    partial class Form1
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
            this.ConnectionTimeOutButton = new System.Windows.Forms.Button();
            this.FailConnectionCheckBox = new System.Windows.Forms.CheckBox();
            this.TryFinallyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectionTimeOutButton
            // 
            this.ConnectionTimeOutButton.Location = new System.Drawing.Point(12, 21);
            this.ConnectionTimeOutButton.Name = "ConnectionTimeOutButton";
            this.ConnectionTimeOutButton.Size = new System.Drawing.Size(143, 23);
            this.ConnectionTimeOutButton.TabIndex = 0;
            this.ConnectionTimeOutButton.Text = "Connection time out";
            this.ConnectionTimeOutButton.UseVisualStyleBackColor = true;
            this.ConnectionTimeOutButton.Click += new System.EventHandler(this.ConnectionTimeOutButton_Click);
            // 
            // FailConnectionCheckBox
            // 
            this.FailConnectionCheckBox.AutoSize = true;
            this.FailConnectionCheckBox.Location = new System.Drawing.Point(161, 25);
            this.FailConnectionCheckBox.Name = "FailConnectionCheckBox";
            this.FailConnectionCheckBox.Size = new System.Drawing.Size(94, 19);
            this.FailConnectionCheckBox.TabIndex = 1;
            this.FailConnectionCheckBox.Text = "Cause failure";
            this.FailConnectionCheckBox.UseVisualStyleBackColor = true;
            // 
            // TryFinallyButton
            // 
            this.TryFinallyButton.Location = new System.Drawing.Point(12, 50);
            this.TryFinallyButton.Name = "TryFinallyButton";
            this.TryFinallyButton.Size = new System.Drawing.Size(143, 23);
            this.TryFinallyButton.TabIndex = 2;
            this.TryFinallyButton.Text = "Try finally";
            this.TryFinallyButton.UseVisualStyleBackColor = true;
            this.TryFinallyButton.Click += new System.EventHandler(this.TryFinallyButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 219);
            this.Controls.Add(this.TryFinallyButton);
            this.Controls.Add(this.FailConnectionCheckBox);
            this.Controls.Add(this.ConnectionTimeOutButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectionTimeOutButton;
        private System.Windows.Forms.CheckBox FailConnectionCheckBox;
        private System.Windows.Forms.Button TryFinallyButton;
    }
}

