namespace QEModList
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.textBoxQuakePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonQuakePathSteam = new System.Windows.Forms.RadioButton();
            this.radioButtonQuakePath = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelQuakeArguments = new System.Windows.Forms.Label();
            this.textBoxQuakeArguments = new System.Windows.Forms.TextBox();
            this.checkBoxAlwaysRefreshLocalAddons = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxQuakePath
            // 
            this.textBoxQuakePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuakePath.Location = new System.Drawing.Point(248, 7);
            this.textBoxQuakePath.Name = "textBoxQuakePath";
            this.textBoxQuakePath.Size = new System.Drawing.Size(466, 27);
            this.textBoxQuakePath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quake path:";
            // 
            // radioButtonQuakePathSteam
            // 
            this.radioButtonQuakePathSteam.AutoSize = true;
            this.radioButtonQuakePathSteam.Location = new System.Drawing.Point(3, 1);
            this.radioButtonQuakePathSteam.Name = "radioButtonQuakePathSteam";
            this.radioButtonQuakePathSteam.Size = new System.Drawing.Size(72, 24);
            this.radioButtonQuakePathSteam.TabIndex = 1;
            this.radioButtonQuakePathSteam.TabStop = true;
            this.radioButtonQuakePathSteam.Text = "Steam";
            this.radioButtonQuakePathSteam.UseVisualStyleBackColor = true;
            this.radioButtonQuakePathSteam.CheckedChanged += new System.EventHandler(this.radioButtonQuakePathSteam_CheckedChanged);
            // 
            // radioButtonQuakePath
            // 
            this.radioButtonQuakePath.AutoSize = true;
            this.radioButtonQuakePath.Location = new System.Drawing.Point(78, 1);
            this.radioButtonQuakePath.Name = "radioButtonQuakePath";
            this.radioButtonQuakePath.Size = new System.Drawing.Size(58, 24);
            this.radioButtonQuakePath.TabIndex = 0;
            this.radioButtonQuakePath.TabStop = true;
            this.radioButtonQuakePath.Text = "Path";
            this.radioButtonQuakePath.UseVisualStyleBackColor = true;
            this.radioButtonQuakePath.CheckedChanged += new System.EventHandler(this.radioButtonQuakePath_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(620, 125);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 29);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(520, 125);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonQuakePathSteam);
            this.panel1.Controls.Add(this.radioButtonQuakePath);
            this.panel1.Location = new System.Drawing.Point(106, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 27);
            this.panel1.TabIndex = 6;
            // 
            // labelQuakeArguments
            // 
            this.labelQuakeArguments.AutoSize = true;
            this.labelQuakeArguments.Location = new System.Drawing.Point(12, 43);
            this.labelQuakeArguments.Name = "labelQuakeArguments";
            this.labelQuakeArguments.Size = new System.Drawing.Size(128, 20);
            this.labelQuakeArguments.TabIndex = 7;
            this.labelQuakeArguments.Text = "Quake arguments:";
            // 
            // textBoxQuakeArguments
            // 
            this.textBoxQuakeArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuakeArguments.Location = new System.Drawing.Point(146, 40);
            this.textBoxQuakeArguments.Name = "textBoxQuakeArguments";
            this.textBoxQuakeArguments.Size = new System.Drawing.Size(568, 27);
            this.textBoxQuakeArguments.TabIndex = 8;
            // 
            // checkBoxAlwaysRefreshLocalAddons
            // 
            this.checkBoxAlwaysRefreshLocalAddons.AutoSize = true;
            this.checkBoxAlwaysRefreshLocalAddons.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxAlwaysRefreshLocalAddons.Location = new System.Drawing.Point(12, 73);
            this.checkBoxAlwaysRefreshLocalAddons.Name = "checkBoxAlwaysRefreshLocalAddons";
            this.checkBoxAlwaysRefreshLocalAddons.Size = new System.Drawing.Size(218, 24);
            this.checkBoxAlwaysRefreshLocalAddons.TabIndex = 10;
            this.checkBoxAlwaysRefreshLocalAddons.Text = "Always refresh local addons:";
            this.checkBoxAlwaysRefreshLocalAddons.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 166);
            this.Controls.Add(this.checkBoxAlwaysRefreshLocalAddons);
            this.Controls.Add(this.textBoxQuakeArguments);
            this.Controls.Add(this.labelQuakeArguments);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxQuakePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOptions";
            this.Text = "QEModList - Options";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxQuakePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonQuakePathSteam;
        private System.Windows.Forms.RadioButton radioButtonQuakePath;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelQuakeArguments;
        private System.Windows.Forms.TextBox textBoxQuakeArguments;
        private System.Windows.Forms.CheckBox checkBoxAlwaysRefreshLocalAddons;
    }
}