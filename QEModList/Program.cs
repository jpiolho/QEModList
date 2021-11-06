using QEModList.Core;
using QEModList.Properties;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QEModList
{
    class Program
    {
        private static QEModListServer _server;
        private static Task _serverTask;
        private static NotifyIcon _notifyIcon;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetupNotifyIcon();
            SetupListServer();

            try
            {
                Application.Run();
            }
            finally
            {
                await TerminateListServerAsync();
                _notifyIcon.Visible = false;
            }
        }

        private static async Task TerminateListServerAsync()
        {
            await _server.StopAsync(new CancellationTokenSource(TimeSpan.FromMinutes(1)).Token);
        }

        private static void SetupListServer()
        {
            _server = new QEModListServer();
            _serverTask = _server.RunAsync();
        }

        private static void SetupNotifyIcon()
        {
            var contextMenu = new ContextMenuStrip();
            //contextMenu.Items.Add("Refresh mods...").Click += ContextMenu_Refresh_OnClick;
            //contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Exit").Click += ContextMenu_Exit_OnClick;

            var notifyIcon = _notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.Icon;
            notifyIcon.Text = "QEModList";
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private static async void ContextMenu_Refresh_OnClick(object sender, EventArgs e)
        {
            
        }

        private static void ContextMenu_Exit_OnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
