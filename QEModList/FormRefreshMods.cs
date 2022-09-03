using QEModList.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QEModList
{
    public partial class FormRefreshMods : Form
    {

        public ProgressBar ProgressBar => this.progressBar;

        private CancellationTokenSource _refreshCts;


        public FormRefreshMods()
        {
            InitializeComponent();
        }


        private async void FormRefreshMods_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.Icon;

            _refreshCts = new CancellationTokenSource();

            try
            {
                await Program.Server.AddonsRepository!.RefreshAsync((done, total) =>
                {
                    this.BeginInvoke((MethodInvoker)delegate { UpdateProgressbar(done, total); });
                }, _refreshCts.Token);
            }
            catch (OperationCanceledException) { }

            this.Close();
        }

        private void UpdateProgressbar(int done, int total)
        {
            this.progressBar.Maximum = total;
            this.progressBar.Value = done;
        }

        private void FormRefreshMods_FormClosing(object sender, FormClosingEventArgs e)
        {
            _refreshCts.Cancel();
        }
    }
}
