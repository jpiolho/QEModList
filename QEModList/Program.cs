using QEModList.Core;
using QEModList.Properties;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QEModList
{
    class Program
    {
        public static QEModListServer Server { get; private set; }
        private static Task _serverTask;
        private static NotifyIcon _notifyIcon;

        [DllImport("shcore.dll")]
        static extern int SetProcessDpiAwareness(int value);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);


            SetupNotifyIcon();
            SetupListServer();

            try
            {
                Application.Run();
            }
            finally
            {
                TerminateListServerAsync().GetAwaiter().GetResult();
                _notifyIcon.Visible = false;
            }
        }

        private static async Task TerminateListServerAsync()
        {
            await Server.StopAsync(new CancellationTokenSource(TimeSpan.FromMinutes(1)).Token);
        }

        private static void SetupListServer()
        {
            Server = new QEModListServer();
            _serverTask = Server.RunAsync();
            _serverTask.ContinueWith((t) => MessageBox.Show($"Unhandled exception in QEModList: {t.Exception}","QEModList error",MessageBoxButtons.OK,MessageBoxIcon.Error), TaskContinuationOptions.OnlyOnFaulted);
        }

        private static void SetupNotifyIcon()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Sources...").Click += ContextMenu_Sources_OnClick;
            contextMenu.Items.Add("Refresh mods...").Click += ContextMenu_Refresh_OnClick;
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Exit").Click += ContextMenu_Exit_OnClick;

            var notifyIcon = _notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.Icon;
            notifyIcon.Text = "QEModList";
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private static async void ContextMenu_Refresh_OnClick(object sender, EventArgs e)
        {
            new FormRefreshMods().ShowDialog();
        }

        private static async void ContextMenu_Sources_OnClick(object sender, EventArgs e)
        {
            var form = new FormSources();
            form.ShowDialog();
        }

        private static void ContextMenu_Exit_OnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
