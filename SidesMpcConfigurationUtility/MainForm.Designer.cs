
namespace SidesMpcConfigurationUtility
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.EditCurrentButton = new System.Windows.Forms.Button();
            this.SettingsDataGridView = new System.Windows.Forms.DataGridView();
            this.EnvironmentComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // EditCurrentButton
            // 
            this.EditCurrentButton.Image = global::SidesMpcConfigurationUtility.Properties.Resources.Edit_16x;
            this.EditCurrentButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.EditCurrentButton.Location = new System.Drawing.Point(12, 300);
            this.EditCurrentButton.Name = "EditCurrentButton";
            this.EditCurrentButton.Size = new System.Drawing.Size(125, 23);
            this.EditCurrentButton.TabIndex = 0;
            this.EditCurrentButton.Text = "Edit current";
            this.EditCurrentButton.UseVisualStyleBackColor = true;
            this.EditCurrentButton.Click += new System.EventHandler(this.EditCurrentButton_Click);
            // 
            // SettingsDataGridView
            // 
            this.SettingsDataGridView.AllowUserToAddRows = false;
            this.SettingsDataGridView.AllowUserToDeleteRows = false;
            this.SettingsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SettingsDataGridView.Location = new System.Drawing.Point(9, 8);
            this.SettingsDataGridView.Name = "SettingsDataGridView";
            this.SettingsDataGridView.ReadOnly = true;
            this.SettingsDataGridView.RowHeadersVisible = false;
            this.SettingsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SettingsDataGridView.Size = new System.Drawing.Size(625, 286);
            this.SettingsDataGridView.TabIndex = 1;
            // 
            // EnvironmentComboBox
            // 
            this.EnvironmentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnvironmentComboBox.FormattingEnabled = true;
            this.EnvironmentComboBox.Items.AddRange(new object[] {
            "Development",
            "Testing",
            "Production"});
            this.EnvironmentComboBox.Location = new System.Drawing.Point(488, 302);
            this.EnvironmentComboBox.Name = "EnvironmentComboBox";
            this.EnvironmentComboBox.Size = new System.Drawing.Size(146, 21);
            this.EnvironmentComboBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 335);
            this.Controls.Add(this.EnvironmentComboBox);
            this.Controls.Add(this.SettingsDataGridView);
            this.Controls.Add(this.EditCurrentButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIDE MPC Configuration - (Development)";
            ((System.ComponentModel.ISupportInitialize)(this.SettingsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EditCurrentButton;
        private System.Windows.Forms.DataGridView SettingsDataGridView;
        private System.Windows.Forms.ComboBox EnvironmentComboBox;
    }
}

