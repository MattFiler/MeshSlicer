namespace MaterialTool
{
    partial class materialEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MaterialDensity = new System.Windows.Forms.TrackBar();
            this.densityTracker = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MaterialStrength = new System.Windows.Forms.TrackBar();
            this.strengthTracker = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MaterialName = new System.Windows.Forms.TextBox();
            this.MaterialIsBreakable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveMaterial = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialStrength)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.MaterialName);
            this.groupBox1.Controls.Add(this.MaterialIsBreakable);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 239);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Material";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MaterialDensity);
            this.groupBox2.Controls.Add(this.densityTracker);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.MaterialStrength);
            this.groupBox2.Controls.Add(this.strengthTracker);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 150);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Breakable Configuration";
            // 
            // MaterialDensity
            // 
            this.MaterialDensity.Location = new System.Drawing.Point(9, 100);
            this.MaterialDensity.Maximum = 100;
            this.MaterialDensity.Name = "MaterialDensity";
            this.MaterialDensity.Size = new System.Drawing.Size(343, 45);
            this.MaterialDensity.TabIndex = 22;
            this.MaterialDensity.Scroll += new System.EventHandler(this.MaterialDensity_Scroll);
            // 
            // densityTracker
            // 
            this.densityTracker.AutoSize = true;
            this.densityTracker.Location = new System.Drawing.Point(252, 84);
            this.densityTracker.MinimumSize = new System.Drawing.Size(100, 0);
            this.densityTracker.Name = "densityTracker";
            this.densityTracker.Size = new System.Drawing.Size(100, 13);
            this.densityTracker.TabIndex = 23;
            this.densityTracker.Text = "0";
            this.densityTracker.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Material Density";
            // 
            // MaterialStrength
            // 
            this.MaterialStrength.Location = new System.Drawing.Point(9, 36);
            this.MaterialStrength.Maximum = 100;
            this.MaterialStrength.Name = "MaterialStrength";
            this.MaterialStrength.Size = new System.Drawing.Size(343, 45);
            this.MaterialStrength.TabIndex = 19;
            this.MaterialStrength.Scroll += new System.EventHandler(this.MaterialStrength_Scroll);
            // 
            // strengthTracker
            // 
            this.strengthTracker.AutoSize = true;
            this.strengthTracker.Location = new System.Drawing.Point(252, 20);
            this.strengthTracker.MinimumSize = new System.Drawing.Size(100, 0);
            this.strengthTracker.Name = "strengthTracker";
            this.strengthTracker.Size = new System.Drawing.Size(100, 13);
            this.strengthTracker.TabIndex = 20;
            this.strengthTracker.Text = "0";
            this.strengthTracker.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Material Strength";
            // 
            // MaterialName
            // 
            this.MaterialName.Location = new System.Drawing.Point(6, 35);
            this.MaterialName.Name = "MaterialName";
            this.MaterialName.Size = new System.Drawing.Size(358, 20);
            this.MaterialName.TabIndex = 1;
            // 
            // MaterialIsBreakable
            // 
            this.MaterialIsBreakable.AutoSize = true;
            this.MaterialIsBreakable.Checked = true;
            this.MaterialIsBreakable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MaterialIsBreakable.Location = new System.Drawing.Point(6, 61);
            this.MaterialIsBreakable.Name = "MaterialIsBreakable";
            this.MaterialIsBreakable.Size = new System.Drawing.Size(74, 17);
            this.MaterialIsBreakable.TabIndex = 13;
            this.MaterialIsBreakable.Text = "Breakable";
            this.MaterialIsBreakable.UseVisualStyleBackColor = true;
            this.MaterialIsBreakable.CheckedChanged += new System.EventHandler(this.MaterialIsBreakable_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Material Name";
            // 
            // saveMaterial
            // 
            this.saveMaterial.Location = new System.Drawing.Point(261, 255);
            this.saveMaterial.Name = "saveMaterial";
            this.saveMaterial.Size = new System.Drawing.Size(120, 32);
            this.saveMaterial.TabIndex = 6;
            this.saveMaterial.Text = "SAVE";
            this.saveMaterial.UseVisualStyleBackColor = true;
            this.saveMaterial.Click += new System.EventHandler(this.saveMaterial_Click);
            // 
            // materialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 296);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveMaterial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "materialEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialStrength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox MaterialIsBreakable;
        private System.Windows.Forms.TextBox MaterialName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveMaterial;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar MaterialDensity;
        private System.Windows.Forms.Label densityTracker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar MaterialStrength;
        private System.Windows.Forms.Label strengthTracker;
        private System.Windows.Forms.Label label4;
    }
}