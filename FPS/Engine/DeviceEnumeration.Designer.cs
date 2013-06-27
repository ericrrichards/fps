namespace Engine {
    partial class DeviceEnumeration {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAdapterName = new System.Windows.Forms.TextBox();
            this.txtDriverVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbWindowed = new System.Windows.Forms.RadioButton();
            this.rbFullscreen = new System.Windows.Forms.RadioButton();
            this.chkVSync = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbColorFormat = new System.Windows.Forms.ComboBox();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbRefresh = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDriverVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAdapterName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Adapter Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display Adapter:";
            // 
            // txtAdapterName
            // 
            this.txtAdapterName.Location = new System.Drawing.Point(96, 19);
            this.txtAdapterName.Name = "txtAdapterName";
            this.txtAdapterName.Size = new System.Drawing.Size(254, 20);
            this.txtAdapterName.TabIndex = 1;
            // 
            // txtDriverVersion
            // 
            this.txtDriverVersion.Location = new System.Drawing.Point(96, 45);
            this.txtDriverVersion.Name = "txtDriverVersion";
            this.txtDriverVersion.Size = new System.Drawing.Size(254, 20);
            this.txtDriverVersion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Driver Version:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbRefresh);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbResolution);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbColorFormat);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkVSync);
            this.groupBox2.Controls.Add(this.rbFullscreen);
            this.groupBox2.Controls.Add(this.rbWindowed);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 151);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display Settings";
            // 
            // rbWindowed
            // 
            this.rbWindowed.AutoSize = true;
            this.rbWindowed.Location = new System.Drawing.Point(9, 19);
            this.rbWindowed.Name = "rbWindowed";
            this.rbWindowed.Size = new System.Drawing.Size(76, 17);
            this.rbWindowed.TabIndex = 0;
            this.rbWindowed.TabStop = true;
            this.rbWindowed.Text = "Windowed";
            this.rbWindowed.UseVisualStyleBackColor = true;
            this.rbWindowed.CheckedChanged += new System.EventHandler(this.rbWindowed_CheckedChanged);
            // 
            // rbFullscreen
            // 
            this.rbFullscreen.AutoSize = true;
            this.rbFullscreen.Location = new System.Drawing.Point(9, 42);
            this.rbFullscreen.Name = "rbFullscreen";
            this.rbFullscreen.Size = new System.Drawing.Size(73, 17);
            this.rbFullscreen.TabIndex = 1;
            this.rbFullscreen.TabStop = true;
            this.rbFullscreen.Text = "Fullscreen";
            this.rbFullscreen.UseVisualStyleBackColor = true;
            this.rbFullscreen.CheckedChanged += new System.EventHandler(this.rbWindowed_CheckedChanged);
            // 
            // chkVSync
            // 
            this.chkVSync.AutoSize = true;
            this.chkVSync.Enabled = false;
            this.chkVSync.Location = new System.Drawing.Point(96, 42);
            this.chkVSync.Name = "chkVSync";
            this.chkVSync.Size = new System.Drawing.Size(60, 17);
            this.chkVSync.TabIndex = 2;
            this.chkVSync.Text = "V-Sync";
            this.chkVSync.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Color Depth:";
            // 
            // cbColorFormat
            // 
            this.cbColorFormat.DisplayMember = "value";
            this.cbColorFormat.Enabled = false;
            this.cbColorFormat.FormattingEnabled = true;
            this.cbColorFormat.Location = new System.Drawing.Point(96, 65);
            this.cbColorFormat.Name = "cbColorFormat";
            this.cbColorFormat.Size = new System.Drawing.Size(254, 21);
            this.cbColorFormat.TabIndex = 4;
            this.cbColorFormat.ValueMember = "Value";
            this.cbColorFormat.SelectedIndexChanged += new System.EventHandler(this.cbColorFormat_SelectedIndexChanged);
            // 
            // cbResolution
            // 
            this.cbResolution.DisplayMember = "value";
            this.cbResolution.Enabled = false;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Location = new System.Drawing.Point(96, 92);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(254, 21);
            this.cbResolution.TabIndex = 6;
            this.cbResolution.ValueMember = "Value";
            this.cbResolution.SelectedIndexChanged += new System.EventHandler(this.cbResolution_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Resolution:";
            // 
            // cbRefresh
            // 
            this.cbRefresh.DisplayMember = "Value";
            this.cbRefresh.Enabled = false;
            this.cbRefresh.FormattingEnabled = true;
            this.cbRefresh.Location = new System.Drawing.Point(96, 119);
            this.cbRefresh.Name = "cbRefresh";
            this.cbRefresh.Size = new System.Drawing.Size(254, 21);
            this.cbRefresh.TabIndex = 8;
            this.cbRefresh.ValueMember = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Refresh Rate:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(206, 252);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(287, 252);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DeviceEnumeration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 288);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceEnumeration";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Graphics Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDriverVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdapterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbRefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbColorFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkVSync;
        private System.Windows.Forms.RadioButton rbFullscreen;
        private System.Windows.Forms.RadioButton rbWindowed;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}