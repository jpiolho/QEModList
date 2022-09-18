using Gameloop.Vdf;
using Microsoft.Win32;
using QEModList.Core;
using QEModList.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QEModList
{
    class Program
    {
        private static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static QEModListServer Server { get; private set; }
        private static Task _serverTask;
        private static NotifyIcon _notifyIcon;
        private static Options _options = new Options();

        [DllImport("shcore.dll")]
        static extern int SetProcessDpiAwareness(int value);


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            await LoadOptionsAsync();

            if (_options.RefreshLocalAddons)
                RefreshLocalAddons();

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


        private static async Task LoadOptionsAsync()
        {
            var path = Path.Combine(QEModListPaths.GetProgramAppDataFolder(), "options.json");

            if (!File.Exists(path))
                return;

            _options = JsonSerializer.Deserialize<Options>(await File.ReadAllTextAsync(path),JsonSerializerOptions);
        }

        private static async Task SaveOptionsAsync()
        {
            var path = Path.Combine(QEModListPaths.GetProgramAppDataFolder(), "options.json");

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(_options, JsonSerializerOptions));
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
            contextMenu.Items.Add("Sources...").Click += (_, _) => new FormSources().ShowDialog();
            contextMenu.Items.Add("Refresh addons sources...").Click += (_,_) => new FormRefreshMods().ShowDialog();
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Options...").Click += ContextMenu_Options_OnClick;
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Launch quake").Click += ContextMenu_LaunchQuake_OnClick;
            contextMenu.Items.Add("Refresh local addons").Click += (_,_) => RefreshLocalAddons();
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("Exit").Click += (_,_) => Application.Exit();

            var notifyIcon = _notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.Icon;
            notifyIcon.Text = "QEModList";
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private static void RefreshLocalAddons()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Nightdive Studios", "Quake");

            if (!Directory.Exists(path))
            {
                MessageBox.Show("Quake saved games folder was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            path = Path.Combine(path, "addons.json");

            if (File.Exists(path))
                File.Delete(path);
        }

        private static async void ContextMenu_LaunchQuake_OnClick(object sender, EventArgs e)
        {
            string path = _options.QuakeLaunchPath;

            if (_options.QuakeLaunchPath == "STEAM")
            {
                var steamQuakePath = GetQuakeSteamInstallPath();

                if (steamQuakePath is null)
                {
                    MessageBox.Show("Could not find quake steam install path", "Failed to start quake", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                path = Path.Combine(steamQuakePath, "Quake_x64_steam.exe");
                var steamappIdPath = Path.Combine(Path.GetDirectoryName(path), "steam_appid.txt");

                if (!File.Exists(steamappIdPath))
                    await File.WriteAllTextAsync(steamappIdPath, "2310");
            }

            Process.Start(path, $"{_options.QuakeLaunchArguments} +ui_addonsBaseURL \"http://localhost/\"");

            if (_options.RefreshLocalAddons)
                RefreshLocalAddons();
        }


        private static async void ContextMenu_Options_OnClick(object sender, EventArgs e)
        {
            var form = new FormOptions(_options);

            if (form.ShowDialog() != DialogResult.OK)
                return;

            _options = form.DialogResultOptions;
            await SaveOptionsAsync();
        }

        private static string GetQuakeSteamInstallPath()
        {
            var steamInstallPath = Registry.GetValue(Environment.Is64BitOperatingSystem ? @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam" : @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null);
            if (steamInstallPath == null)
                return null;

            var libraryFoldersPath = Path.Combine((string)steamInstallPath, "steamapps", "libraryfolders.vdf");
            if (!File.Exists(libraryFoldersPath))
                return null;

            // Parse VDF file
            var vdf = VdfConvert.Deserialize(File.ReadAllText(libraryFoldersPath));

            // Try to look for the app 2310 in each library
            int dirId = 0;
            var libraryFolders = vdf.Value;
            dynamic dir;
            while ((dir = libraryFolders[(dirId++).ToString()]) != null)
            {
                if (dir["apps"]["2310"] != null)
                {
                    return Path.Combine(dir["path"].ToString(), "steamapps", "common", "Quake", "rerelease");
                }
            }

            return null;
        }
    }
}
