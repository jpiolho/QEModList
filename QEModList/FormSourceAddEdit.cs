using QEModList.Core.Models.Sources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QEModList
{
    public partial class FormSourceAddEdit : Form
    {
        private class SourceType
        {
            public string Name { get; set; }
            public Type Type { get; set; }

            public override string ToString() => Name;

            public SourceType(string name, Type type)
            {
                Name = name;
                Type = type;
            }
        }


        private SourceType[] SourceTypes = new SourceType[]
        {
            new("Addon list", typeof(SourceAddonList)),
            new("Folder", typeof(SourceFolder)),
            new("Github", typeof(SourceGithub))
        };

        private Source? _editingSource;
        private Action<string> _setValueFunc;
        private Func<string> _getValueFunc;

        public FormSourceAddEdit(Source? editingSource)
        {
            InitializeComponent();

            _editingSource = editingSource;
        }

        private void FormSourceAddEdit_Load(object sender, EventArgs e)
        {
            comboBoxSourceType.Items.AddRange(SourceTypes);

            if (_editingSource is not null)
            {
                comboBoxSourceType.SelectedItem = SourceTypes.FirstOrDefault(st => st.Type == _editingSource.GetType());
                comboBoxSourceType.Enabled = false;
                UpdateValuePanel();

                _setValueFunc(_editingSource.SourceValue);

                this.Text = this.Text.Replace("{Op}", "Edit");
                buttonOK.Text = "Save";
            }
            else
            {
                this.Text = this.Text.Replace("{Op}", "Add");
                buttonOK.Text = "Add";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_editingSource is not null)
            {
                try
                {
                    _editingSource.SourceValue = _getValueFunc();
                }
                catch
                {
                    MessageBox.Show("The value you inserted isn't right. Please double check and correct it", "Failed to save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            var sourceType = (SourceType)comboBoxSourceType.SelectedItem;
            var source = (Source)Activator.CreateInstance(sourceType.Type);

            try
            {
                source.SourceValue = _getValueFunc();
            }
            catch
            {
                MessageBox.Show("The value you inserted isn't right. Please double check and correct it", "Failed to save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Program.Server.AddonsRepository!.Sources.Add(source);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBoxSourceType_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateValuePanel();
        }

        private void UpdateValuePanel()
        {
            var sourceType = comboBoxSourceType.SelectedItem as SourceType;

            if (panelValue.Controls.Count > 0)
            {
                var c = panelValue.Controls[0];
                c.Visible = false;
                c.Parent = this;
            }

            Panel panel;

            // Meh, this is pretty bad. Wish for blazor
            if (sourceType == null)
            {
                panel = null;
                _setValueFunc = null;
                _getValueFunc = null;
            }
            else if (sourceType.Type == typeof(SourceFolder))
            {
                panel = panelValueFolderPath;
                _setValueFunc = text => textBoxFolderPath.Text = text;
                _getValueFunc = () => textBoxFolderPath.Text;
            }
            else
            {
                panel = panelValueTextbox;
                _setValueFunc = text => textBoxValue.Text = text;
                _getValueFunc = () => textBoxValue.Text;
            }

            if (panel != null)
            {
                panel.Visible = true;
                panel.Parent = panelValue;
                panel.Dock = DockStyle.Top;
            }
        }

        private void buttonFolderBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                textBoxFolderPath.Text = folderBrowserDialog.SelectedPath;

        }

        private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonOK_Click(textBoxValue, EventArgs.Empty);
        }
    }
}
