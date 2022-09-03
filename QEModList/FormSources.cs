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
    public partial class FormSources : Form
    {
        private List<Source> _sources;
        private bool _sourcesChanged;
        public FormSources()
        {
            InitializeComponent();
        }

        private void FormSources_Load(object sender, EventArgs e)
        {
            if(!Program.Server.IsRunning)
            {
                MessageBox.Show("The server is not running","Cannot open",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }


            RefreshList();
        }


        private void RefreshList()
        {
            _sources = Program.Server.AddonsRepository.Sources;

            listView.Items.Clear();

            foreach (var source in _sources)
            {
                var item = new ListViewItem();
                item.Text = source.SourceValue;
                item.SubItems.Add(SourceTypeToString(source));
                item.Tag = source;
                listView.Items.Add(item);
            }
        }

        private string SourceTypeToString(Source source)
        {
            switch(source)
            {
                default: return "Unknown";
                case SourceAddonList: return "Addon List";
                case SourceFolder: return "Folder";
                case SourceGithub: return "Github";
            }
        }

        private void buttonAddSource_Click(object sender, EventArgs e)
        {
            var form = new FormSourceAddEdit(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshList();
                _sourcesChanged = true;
            }
        }

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = listView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this source?", "Delete source", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _sources.Remove((Source)listView.FocusedItem.Tag);
            RefreshList();
            _sourcesChanged = true;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormSourceAddEdit((Source)listView.FocusedItem.Tag);

            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshList();
                _sourcesChanged = true;
            }
        }

        private void FormSources_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_sourcesChanged)
            {
                _ = Program.Server.SourcesListLoader.SaveAsync(_sources, System.Threading.CancellationToken.None);
                new FormRefreshMods().ShowDialog();
            }
        }
    }
}
