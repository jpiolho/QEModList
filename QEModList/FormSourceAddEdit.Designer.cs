namespace QEModList
{
    partial class FormSourceAddEdit
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
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxSourceType = new System.Windows.Forms.ComboBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelValueTextbox = new System.Windows.Forms.Panel();
            this.panelValue = new System.Windows.Forms.Panel();
            this.panelValueFolderPath = new System.Windows.Forms.Panel();
            this.buttonFolderBrowse = new System.Windows.Forms.Button();
            this.textBoxFolderPath = new System.Windows.Forms.TextBox();
            this.labelFolderPath = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panelValueTextbox.SuspendLayout();
            this.panelValueFolderPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(12, 15);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(43, 20);
            this.labelType.TabIndex = 0;
            this.labelType.Text = "Type:";
            // 
            // comboBoxSourceType
            // 
            this.comboBoxSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSourceType.FormattingEnabled = true;
            this.comboBoxSourceType.Location = new System.Drawing.Point(90, 12);
            this.comboBoxSourceType.Name = "comboBoxSourceType";
            this.comboBoxSourceType.Size = new System.Drawing.Size(662, 28);
            this.comboBoxSourceType.TabIndex = 1;
            this.comboBoxSourceType.SelectedValueChanged += new System.EventHandler(this.comboBoxSourceType_SelectedValueChanged);
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(3, 3);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(48, 20);
            this.labelValue.TabIndex = 2;
            this.labelValue.Text = "Value:";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(78, 0);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(662, 27);
            this.textBoxValue.TabIndex = 3;
            this.textBoxValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(658, 81);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 29);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "{Op}";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(558, 82);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelValueTextbox
            // 
            this.panelValueTextbox.Controls.Add(this.textBoxValue);
            this.panelValueTextbox.Controls.Add(this.labelValue);
            this.panelValueTextbox.Location = new System.Drawing.Point(12, 194);
            this.panelValueTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.panelValueTextbox.Name = "panelValueTextbox";
            this.panelValueTextbox.Size = new System.Drawing.Size(740, 30);
            this.panelValueTextbox.TabIndex = 6;
            // 
            // panelValue
            // 
            this.panelValue.Location = new System.Drawing.Point(12, 46);
            this.panelValue.Name = "panelValue";
            this.panelValue.Size = new System.Drawing.Size(740, 33);
            this.panelValue.TabIndex = 7;
            // 
            // panelValueFolderPath
            // 
            this.panelValueFolderPath.Controls.Add(this.buttonFolderBrowse);
            this.panelValueFolderPath.Controls.Add(this.textBoxFolderPath);
            this.panelValueFolderPath.Controls.Add(this.labelFolderPath);
            this.panelValueFolderPath.Location = new System.Drawing.Point(12, 227);
            this.panelValueFolderPath.Margin = new System.Windows.Forms.Padding(0);
            this.panelValueFolderPath.Name = "panelValueFolderPath";
            this.panelValueFolderPath.Size = new System.Drawing.Size(740, 30);
            this.panelValueFolderPath.TabIndex = 7;
            // 
            // buttonFolderBrowse
            // 
            this.buttonFolderBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolderBrowse.Location = new System.Drawing.Point(646, 0);
            this.buttonFolderBrowse.Name = "buttonFolderBrowse";
            this.buttonFolderBrowse.Size = new System.Drawing.Size(94, 27);
            this.buttonFolderBrowse.TabIndex = 4;
            this.buttonFolderBrowse.Text = "...";
            this.buttonFolderBrowse.UseVisualStyleBackColor = true;
            this.buttonFolderBrowse.Click += new System.EventHandler(this.buttonFolderBrowse_Click);
            // 
            // textBoxFolderPath
            // 
            this.textBoxFolderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderPath.Location = new System.Drawing.Point(78, 0);
            this.textBoxFolderPath.Name = "textBoxFolderPath";
            this.textBoxFolderPath.Size = new System.Drawing.Size(562, 27);
            this.textBoxFolderPath.TabIndex = 3;
            this.textBoxFolderPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // labelFolderPath
            // 
            this.labelFolderPath.AutoSize = true;
            this.labelFolderPath.Location = new System.Drawing.Point(3, 3);
            this.labelFolderPath.Name = "labelFolderPath";
            this.labelFolderPath.Size = new System.Drawing.Size(40, 20);
            this.labelFolderPath.TabIndex = 2;
            this.labelFolderPath.Text = "Path:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FormSourceAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 123);
            this.Controls.Add(this.panelValueTextbox);
            this.Controls.Add(this.panelValueFolderPath);
            this.Controls.Add(this.panelValue);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxSourceType);
            this.Controls.Add(this.labelType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSourceAddEdit";
            this.Text = "{Op} Source";
            this.Load += new System.EventHandler(this.FormSourceAddEdit_Load);
            this.panelValueTextbox.ResumeLayout(false);
            this.panelValueTextbox.PerformLayout();
            this.panelValueFolderPath.ResumeLayout(false);
            this.panelValueFolderPath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxSourceType;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelValueTextbox;
        private System.Windows.Forms.Panel panelValue;
        private System.Windows.Forms.Panel panelValueFolderPath;
        private System.Windows.Forms.Button buttonFolderBrowse;
        private System.Windows.Forms.TextBox textBoxFolderPath;
        private System.Windows.Forms.Label labelFolderPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}