using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Clair.Properties;
using Hardcodet.Wpf.TaskbarNotification;

namespace Clair
{
    public partial class SettingsWindow : Window
    {


        public SettingsWindow()
        {
            InitializeComponent();

            // Sets checkbox to match user settings.

            checkUpdatesCheckbox.IsChecked = Settings.Default.isCheckingUpdates;

            autoShuffleCheckbox.IsChecked = Settings.Default.isAutoShuffleEnabled;

            enableExtensionsCheckbox.IsChecked = Settings.Default.isUnsupportedExtensionsEnabled;

        }

        private void homeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if(homeGrid.Visibility != Visibility.Visible)
            {
                homeGrid.Visibility = Visibility.Visible;
                aboutGrid.Visibility = Visibility.Hidden;
                downloaderGrid.Visibility = Visibility.Hidden;
            }
        }

        private void aboutButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (aboutGrid.Visibility != Visibility.Visible)
            {
                aboutGrid.Visibility = Visibility.Visible;
                homeGrid.Visibility = Visibility.Hidden;
                downloaderGrid.Visibility = Visibility.Hidden;
            }
        }

        private void downloaderButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (downloaderGrid.Visibility != Visibility.Visible)
            {
                downloaderGrid.Visibility = Visibility.Visible;
                aboutGrid.Visibility = Visibility.Hidden;
                homeGrid.Visibility = Visibility.Hidden;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {

            // Opens Webpage 

            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void autoShuffleCheckbox_Checked(object sender, RoutedEventArgs e)
        {


            if(Settings.Default.isAutoShuffleEnabled == false)
            {
                Settings.Default.isAutoShuffleEnabled = true;
                Settings.Default.Save();

            }
        }

        private void autoShuffleCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

            if (Settings.Default.isAutoShuffleEnabled == true)
            {
                Settings.Default.isAutoShuffleEnabled = false;
                Settings.Default.Save();

            }
        }

        private void enableExtensionsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isUnsupportedExtensionsEnabled == false)
            {
                Settings.Default.isUnsupportedExtensionsEnabled = true;
                Settings.Default.Save();

            }
        }

        private void enableExtensionsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isUnsupportedExtensionsEnabled == true)
            {
                Settings.Default.isUnsupportedExtensionsEnabled = false;
                Settings.Default.Save();

            }
        }

        private void checkUpdatesCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isCheckingUpdates == false)
            {
                Settings.Default.isCheckingUpdates = true;
                Settings.Default.Save();

            }
        }

        private void checkUpdatesCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.isCheckingUpdates == true)
            {
                Settings.Default.isCheckingUpdates = false;
                Settings.Default.Save();

            }
        }

        private void downloaderTitleField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string targetDir = AppDomain.CurrentDomain.BaseDirectory + "\\resources";
                string musicDir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                try
                {
                    Process ffmpegDetection = new Process();
                    ffmpegDetection.StartInfo.FileName = "ffmpeg.exe";
                    ffmpegDetection.Start();
                    ffmpegDetection.WaitForExit();
                }
                catch (Win32Exception)
                {
                    //  ffmpeg is not in PATH Variable
                    Environment.SetEnvironmentVariable("PATH", targetDir + "\\binaries");
                }

                if (isUrlValid(downloaderTitleField.Text))
                {
                    Process spotdlProcess = new Process();
                    spotdlProcess.StartInfo.FileName = targetDir + "\\spotdl\\spotdl.exe";
                    spotdlProcess.StartInfo.Arguments = "-p "
                        + downloaderTitleField.Text
                        +" --write-to " 
                        + musicDir 
                        +"\\0playlist.txt";
                    spotdlProcess.Start();
                    spotdlProcess.WaitForExit();
                    if (!File.Exists(musicDir + "\\0playlist.txt"))
                        return;
                    Directory.CreateDirectory(musicDir + "\\downloaded-playlist");
                    spotdlProcess.StartInfo.Arguments = "-l "
                        + musicDir + "\\0playlist.txt"
                        + " -f "
                        + musicDir
                        + "\\downloaded-playlist\\";
                    spotdlProcess.Start();
                    spotdlProcess.WaitForExit();
                    File.Delete(musicDir + "\\0playlist.txt");
                }
                else
                {
                    Process spotdlProcess = new Process();
                    spotdlProcess.StartInfo.FileName = targetDir + "\\spotdl\\spotdl.exe";
                    spotdlProcess.StartInfo.Arguments = "-q best -m -f "
                        + musicDir
                        + " -s \"" 
                        + downloaderTitleField.Text 
                        + "\"";
                    spotdlProcess.Start();
                    spotdlProcess.WaitForExit();
                }
                
                using (TaskbarIcon taskbar = new TaskbarIcon())
                {
                    taskbar.ShowBalloonTip(
                        "Download Finished!.",
                        "Localization: " + musicDir,
                        BalloonIcon.None
                    );
                    taskbar.Visibility = Visibility.Visible;
                }
            }
        }
        private bool isUrlValid(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }
    }
}
