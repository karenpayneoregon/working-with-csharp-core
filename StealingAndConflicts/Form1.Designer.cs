
namespace StealingAndConflicts
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
            this.StealButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NotStealingButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StealButton
            // 
            this.StealButton.Location = new System.Drawing.Point(18, 22);
            this.StealButton.Name = "StealButton";
            this.StealButton.Size = new System.Drawing.Size(98, 23);
            this.StealButton.TabIndex = 0;
            this.StealButton.Text = "Steal";
            this.StealButton.UseVisualStyleBackColor = true;
            this.StealButton.Click += new System.EventHandler(this.StealButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(135, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Results goes here";
            this.textBox1.Size = new System.Drawing.Size(214, 81);
            this.textBox1.TabIndex = 1;
            // 
            // NotStealingButton
            // 
            this.NotStealingButton.Location = new System.Drawing.Point(18, 115);
            this.NotStealingButton.Name = "NotStealingButton";
            this.NotStealingButton.Size = new System.Drawing.Size(98, 23);
            this.NotStealingButton.TabIndex = 2;
            this.NotStealingButton.Text = "Not stealing";
            this.NotStealingButton.UseVisualStyleBackColor = true;
            this.NotStealingButton.Click += new System.EventHandler(this.NotStealingButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(135, 115);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "Results goes here";
            this.textBox2.Size = new System.Drawing.Size(214, 81);
            this.textBox2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 228);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.NotStealingButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.StealButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chunk variations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StealButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button NotStealingButton;
        private System.Windows.Forms.TextBox textBox2;
    }
}

