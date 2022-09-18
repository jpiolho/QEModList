using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QEModList
{
    public partial class FormOptions : Form
    {
        public Options DialogResultOptions { get; private set; }

        public FormOptions(Options options)
        {
            InitializeComponent();

            LoadOptions(options);
        }

        private void LoadOptions(Options options)
        {
            textBoxQuakeArguments.Text = options.QuakeLaunchArguments;
            
            if(options.QuakeLaunchPath == "STEAM")
            {
                radioButtonQuakePathSteam.Checked = true;
            }
            else
            {
                radioButtonQuakePath.Checked = true;
                textBoxQuakePath.Text = options.QuakeLaunchPath;
            }

            checkBoxAlwaysRefreshLocalAddons.Checked = options.RefreshLocalAddons;

            UpdateUI();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            DialogResultOptions = new Options()
            {
                Version = 1,
                QuakeLaunchPath = radioButtonQuakePathSteam.Checked ? "STEAM" : textBoxQuakePath.Text,
                QuakeLaunchArguments = textBoxQuakeArguments.Text,
                RefreshLocalAddons = checkBoxAlwaysRefreshLocalAddons.Checked
            };

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButtonQuakePath_CheckedChanged(object sender, EventArgs e) => UpdateUI();
        private void radioButtonQuakePathSteam_CheckedChanged(object sender, EventArgs e) => UpdateUI();


        private void UpdateUI()
        {
            textBoxQuakePath.ReadOnly = !radioButtonQuakePath.Checked;
        }
    }
}
