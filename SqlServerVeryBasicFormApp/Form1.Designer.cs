
namespace SqlServerVeryBasicFormApp
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
            this.categCB = new System.Windows.Forms.ComboBox();
            this.GetCurrentCategoryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // categCB
            // 
            this.categCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categCB.FormattingEnabled = true;
            this.categCB.Location = new System.Drawing.Point(12, 31);
            this.categCB.Name = "categCB";
            this.categCB.Size = new System.Drawing.Size(179, 23);
            this.categCB.TabIndex = 0;
            // 
            // GetCurrentCategoryButton
            // 
            this.GetCurrentCategoryButton.Location = new System.Drawing.Point(222, 30);
            this.GetCurrentCategoryButton.Name = "GetCurrentCategoryButton";
            this.GetCurrentCategoryButton.Size = new System.Drawing.Size(75, 23);
            this.GetCurrentCategoryButton.TabIndex = 1;
            this.GetCurrentCategoryButton.Text = "Current";
            this.GetCurrentCategoryButton.UseVisualStyleBackColor = true;
            this.GetCurrentCategoryButton.Click += new System.EventHandler(this.GetCurrentCategoryButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 200);
            this.Controls.Add(this.GetCurrentCategoryButton);
            this.Controls.Add(this.categCB);
            this.Name = "Form1";
            this.Text = "Code sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox categCB;
        private System.Windows.Forms.Button GetCurrentCategoryButton;
    }
}

