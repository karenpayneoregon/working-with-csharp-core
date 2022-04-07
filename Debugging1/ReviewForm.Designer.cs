namespace DebuggingFiles
{
    partial class ReviewForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.districtColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beatColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gridColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicicColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latitudeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longitudeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdUpdate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 389);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 61);
            this.panel1.TabIndex = 0;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdUpdate.Location = new System.Drawing.Point(12, 16);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(75, 23);
            this.cmdUpdate.TabIndex = 3;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.dateColumn,
            this.addressColumn,
            this.districtColumn,
            this.beatColumn,
            this.gridColumn,
            this.descriptionColumn,
            this.nicicColumn,
            this.latitudeColumn,
            this.longitudeColumn});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 389);
            this.dataGridView1.TabIndex = 2;
            // 
            // idColumn
            // 
            this.idColumn.DataPropertyName = "id";
            this.idColumn.HeaderText = "Row index";
            this.idColumn.Name = "idColumn";
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "Date";
            this.dateColumn.HeaderText = "Date";
            this.dateColumn.Name = "dateColumn";
            // 
            // addressColumn
            // 
            this.addressColumn.DataPropertyName = "Address";
            this.addressColumn.HeaderText = "Address";
            this.addressColumn.Name = "addressColumn";
            // 
            // districtColumn
            // 
            this.districtColumn.DataPropertyName = "District";
            this.districtColumn.HeaderText = "District";
            this.districtColumn.Name = "districtColumn";
            // 
            // beatColumn
            // 
            this.beatColumn.DataPropertyName = "beat";
            this.beatColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.beatColumn.HeaderText = "Beat";
            this.beatColumn.Name = "beatColumn";
            this.beatColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.beatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gridColumn
            // 
            this.gridColumn.DataPropertyName = "Grid";
            this.gridColumn.HeaderText = "Grid";
            this.gridColumn.Name = "gridColumn";
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.DataPropertyName = "Description";
            this.descriptionColumn.HeaderText = "Description";
            this.descriptionColumn.Name = "descriptionColumn";
            // 
            // nicicColumn
            // 
            this.nicicColumn.DataPropertyName = "NcicCode";
            this.nicicColumn.HeaderText = "NICIC";
            this.nicicColumn.Name = "nicicColumn";
            // 
            // latitudeColumn
            // 
            this.latitudeColumn.DataPropertyName = "Latitude";
            this.latitudeColumn.HeaderText = "Latitude";
            this.latitudeColumn.Name = "latitudeColumn";
            // 
            // longitudeColumn
            // 
            this.longitudeColumn.DataPropertyName = "Longitude";
            this.longitudeColumn.HeaderText = "Longitude";
            this.longitudeColumn.Name = "longitudeColumn";
            this.longitudeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.longitudeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "ReviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReviewForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn districtColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn beatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicicColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latitudeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitudeColumn;
        private System.Windows.Forms.Button cmdUpdate;
    }
}