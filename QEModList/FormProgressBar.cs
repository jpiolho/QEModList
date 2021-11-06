using QEModList.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QEModList
{
    public partial class FormRefreshMods : Form
    {
        public ProgressBar ProgressBar => this.progressBar;

        public FormRefreshMods(string title)
        {
            InitializeComponent();

            this.labelTitle.Text = title;
        }

        private void FormRefreshMods_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.Icon;
        }
    }
}
