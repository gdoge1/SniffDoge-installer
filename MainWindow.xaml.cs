using System;
using System.IO;
using System.Windows;
using System.Diagnostics;
using IWshRuntimeLibrary;

namespace SniffDoge_installer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void install_btn_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "SniffDoge"));
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SniffDoge"));
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SniffDoge\\Decompression"));
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SniffDoge\\Rules"));
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SniffDoge\\Rules\\Custom"));

            string cd = Directory.GetCurrentDirectory();
            foreach (string f in Directory.GetFiles(Path.Combine(cd, "Install Files")))
            {
                try
                {
                    System.IO.File.Copy(f, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "SniffDoge\\" + Path.GetFileName(f)));
                } catch { }
            }
            foreach (string f in Directory.GetFiles(Path.Combine(cd, "Additional Files")))
            {
                try
                {
                    System.IO.File.Copy(f, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SniffDoge\\Rules\\" + Path.GetFileName(f)));
                } catch { }
            }
            if (desktop_btn.IsChecked == true)
            {
                appShortcutToDesktop("SniffDoge", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "SniffDoge\\SniffDoge_gui.exe"));
            }
            Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "SniffDoge\\SniffDoge_gui.exe"));
            Close();
        }

        private void appShortcutToDesktop(string linkName, string sniffdogeexe)
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
            {
                string app = sniffdogeexe;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
            }
        }
    }
}
