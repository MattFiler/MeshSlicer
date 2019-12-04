namespace MaterialTool
{
    partial class materialList
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
            this.editSelected = new System.Windows.Forms.Button();
            this.deleteSelected = new System.Windows.Forms.Button();
            this.addNew = new System.Windows.Forms.Button();
            this.materialListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // editSelected
            // 
            this.editSelected.Location = new System.Drawing.Point(428, 249);
            this.editSelected.Name = "editSelected";
            this.editSelected.Size = new System.Drawing.Size(147, 36);
            this.editSelected.TabIndex = 7;
            this.editSelected.Text = "EDIT SELECTED";
            this.editSelected.UseVisualStyleBackColor = true;
            this.editSelected.Click += new System.EventHandler(this.editSelected_Click);
            // 
            // deleteSelected
            // 
            this.deleteSelected.Location = new System.Drawing.Point(428, 291);
            this.deleteSelected.Name = "deleteSelected";
            this.deleteSelected.Size = new System.Drawing.Size(147, 36);
            this.deleteSelected.TabIndex = 6;
            this.deleteSelected.Text = "DELETE SELECTED";
            this.deleteSelected.UseVisualStyleBackColor = true;
            this.deleteSelected.Click += new System.EventHandler(this.deleteSelected_Click);
            // 
            // addNew
            // 
            this.addNew.Location = new System.Drawing.Point(428, 207);
            this.addNew.Name = "addNew";
            this.addNew.Size = new System.Drawing.Size(147, 36);
            this.addNew.TabIndex = 5;
            this.addNew.Text = "ADD NEW";
            this.addNew.UseVisualStyleBackColor = true;
            this.addNew.Click += new System.EventHandler(this.addNew_Click);
            // 
            // materialListBox
            // 
            this.materialListBox.FormattingEnabled = true;
            this.materialListBox.Location = new System.Drawing.Point(12, 11);
            this.materialListBox.Name = "materialListBox";
            this.materialListBox.Size = new System.Drawing.Size(410, 316);
            this.materialListBox.TabIndex = 4;
            // 
            // materialList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 338);
            this.Controls.Add(this.editSelected);
            this.Controls.Add(this.deleteSelected);
            this.Controls.Add(this.addNew);
            this.Controls.Add(this.materialListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "materialList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material List";
            this.Load += new System.EventHandler(this.materialList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button editSelected;
        private System.Windows.Forms.Button deleteSelected;
        private System.Windows.Forms.Button addNew;
        private System.Windows.Forms.ListBox materialListBox;
    }
}

