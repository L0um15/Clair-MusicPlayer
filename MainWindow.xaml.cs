using Clair.Properties;
using Hardcodet.Wpf.TaskbarNotification;
using Hardcodet.Wpf.TaskbarNotification.Interop;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Clair
{
    public partial class MainWindow : Window
    {

        // Global Variables
        String version = "pre-5.0";
        Double tempVolume;
        String[] filePaths;
        bool isMediaPlaying = false;
        MediaPlayer mPlayer = new MediaPlayer();
        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
        DispatcherTimer durationTimer = new DispatcherTimer();
        TaskbarIcon taskbarIcon = new TaskbarIcon();


        public MainWindow()
        {
            InitializeComponent();

            // Bind Events
            mPlayer.MediaEnded += mPlayer_MediaEnded;
            mPlayer.MediaOpened += mPlayer_MediaOpened;
            durationTimer.Tick += dispatcherTimer_Tick;
            taskbarIcon.TrayLeftMouseDown += taskbar_TrayLeftMouseDown;
            this.StateChanged += Window_StateChanged;
            this.KeyDown += Window_OnKeyPressed;

            durationTimer.Interval = new TimeSpan(0, 0, 1);
            volumeSlider.IsMoveToPointEnabled = true;

            // User Settings

            volumeSlider.Value = (double) Settings.Default.volumelevel;

            if (Settings.Default.isCheckingUpdates)
                checkVersion();

            if (Settings.Default.lastKnownDirectory != "nopath")
            {

                filePaths = Directory.GetFiles(Settings.Default.lastKnownDirectory, "*.mp3");

                if (Settings.Default.isAutoShuffleEnabled)
                    randomizeSongSelection(filePaths);

                for (int i = 0; i < filePaths.Length; i++)
                {
                    songList.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
                }

                songList.SelectedIndex = 0;
                playButton.Visibility = Visibility.Visible;
                pauseButton.Visibility = Visibility.Hidden;
                isMediaPlaying = false;
                mPlayer.Pause();

            }

        }

        private void checkVersion()
        {
            try
            {
                string ver = new WebClient().DownloadString("https://raw.githubusercontent.com/L0um15/Clair-MusicPlayer/feature_UpdateNotifier/version.txt");
                if (ver != version)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Shall i open Github Page?", "New Version Available!", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("https://github.com/L0um15/Clair-MusicPlayer"));
                        this.Close();
                    }

                }
            }
            catch (WebException e)
            {
                switch (e.Status)
                {
                    case WebExceptionStatus.Timeout:
                        System.Windows.MessageBox.Show("Connection Timeout.", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.ProtocolError:
                        System.Windows.MessageBox.Show("ProtocolError", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.RequestCanceled:
                        System.Windows.MessageBox.Show("Request Canceled", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.UnknownError:
                        System.Windows.MessageBox.Show("Unknown Error", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.ConnectFailure:
                        System.Windows.MessageBox.Show("Connection Failure", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.ServerProtocolViolation:
                        System.Windows.MessageBox.Show("Server Protocol Violation", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.ReceiveFailure:
                        System.Windows.MessageBox.Show("Receive Failure", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        System.Windows.MessageBox.Show("Could not resolve hostname", "Unable to fetch updates", MessageBoxButton.OK);
                        break;
                }
            }
            
                

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

            // Minimizes to taskbar

            if (this.WindowState == WindowState.Minimized) {
                taskbarIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
                taskbarIcon.ShowBalloonTip(
                        "I'be Waiting Here.",
                        "Your workspace is much cleaner now!",
                        BalloonIcon.None
                    );
                taskbarIcon.Visibility = Visibility.Visible;
                this.ShowInTaskbar = false;
            }
        }

        private void taskbar_TrayLeftMouseDown(object sender, EventArgs args) {

            // Reveals from taskbar

            if (this.WindowState == WindowState.Minimized) {
                this.WindowState = WindowState.Normal;
                this.Activate();
                this.ShowInTaskbar = true;
                taskbarIcon.Visibility = Visibility.Hidden;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs args) {

            // Changes Value of durationSlider

            if (isMediaPlaying)
            {
                durationSlider.Value = (int) mPlayer.Position.TotalSeconds;
            }
        }

        private void mPlayer_MediaOpened(object sender, EventArgs args) {

            // Calls when mPlayer loads file

            double duration = mPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            durationSlider.Maximum = (int)duration;
            durationSlider.IsMoveToPointEnabled = true;
            TimeSpan t = mPlayer.NaturalDuration.TimeSpan;
            string time = t.ToString(@"mm\:ss");
            totalTime.Text = time;
            durationTimer.Start();
        }

        private void mPlayer_MediaEnded(object sender, EventArgs args) {

            // Moves to next track if available

            if (songList.SelectedIndex < songList.Items.Count - 1)
            {
                songList.SelectedIndex += 1;
            }
            else
            {

                // Hides pause button when there is no more tracks in queue

                playButton.Visibility = Visibility.Visible;
                pauseButton.Visibility = Visibility.Hidden;
                isMediaPlaying = false;
            }
                
        }

        private void getArtistTagFromFile(String path) {

            // Get Artist and Title if available ...

            TagLib.File file = TagLib.File.Create(path);

            if (file.Tag.Title != null)
            {
                songTitle.Content = file.Tag.Title;
                artistTitle.Content = file.Tag.Artists[0];
            }
            else {

                // ... or set filename as Title

                songTitle.Content = songList.Items[songList.SelectedIndex];
                artistTitle.Content = null;
            }
            
        }


        private void getAlbumArtFromFile(int index,String path) {

            // Get Album cover if available

            TagLib.File file = TagLib.File.Create(path);

            var picture = file.Tag.Pictures.FirstOrDefault();

            /*
             * Other files than mp3 will return blank album cover
             * TODO: Fix black album cover
             */

            if (picture != null)
            {
                MemoryStream memory = new MemoryStream(picture.Data.Data);
                memory.Seek(0, SeekOrigin.Begin);
                
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();
                if (index == 0)
                    albumArtImage.Source = bitmap;
                else if (index == 1)
                    albumArtImage2.Source = bitmap;
                else if (index == 2)
                    albumArtImage3.Source = bitmap;
            }
            else {

                if (index == 0)
                    albumArtImage.Source = new BitmapImage(new Uri("assets/images/noalbum.jpg", UriKind.RelativeOrAbsolute));
                else if (index == 1)
                    albumArtImage2.Source = new BitmapImage(new Uri("assets/images/noalbum.jpg", UriKind.RelativeOrAbsolute));
                else if (index == 2)
                    albumArtImage3.Source = new BitmapImage(new Uri("assets/images/noalbum.jpg", UriKind.RelativeOrAbsolute));
            }
        }

        private void playButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isMediaPlaying) {
                mPlayer.Play();
                playButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Visible;
                isMediaPlaying = true;
            }
        }

        private void folderButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Enables multiple selection
            ofd.Multiselect = true;


            // Shows only supported files when disabled
            if (!Settings.Default.isUnsupportedExtensionsEnabled)
                ofd.Filter = "Music files (*.mp3) | *.mp3";
            else
                ofd.Filter = "All files (*.*) | *.*";

            if (ofd.ShowDialog() == true)
            {
                // Clears songList before adding to prevent duplication
                if (songList.Items.Count > 0) {
                    songList.Items.Clear();
                    songListFiltered.Items.Clear();
                }
                // Adds file paths to array
                filePaths = ofd.FileNames;
                

                // Randomizes if enabled
                if(Settings.Default.isAutoShuffleEnabled == true)
                    randomizeSongSelection(filePaths);

                // Converts file paths to normal ones
                for (int i = 0; i < filePaths.Length; i++) {
                    songList.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));
                }

                // Saves location for auto open feature
                Settings.Default.lastKnownDirectory = System.IO.Path.GetDirectoryName(filePaths[0]);
                Settings.Default.Save();

                // Calls SelectionChanged Event
                songList.SelectedIndex = 0;

            }
        }

        private void songList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            /*
             * Whole Musicplayer works with this event
             * Here every track gets processed and played
             * Every time when selection is changed, new track will be loaded ...
             * ... with equal index as position in filePaths array
             */

            try
            {
                // Gets FILE PATH from filePaths and open it
                mPlayer.Open(new Uri(filePaths[songList.SelectedIndex]));

                // Gets Metadata from file
                getArtistTagFromFile(filePaths[songList.SelectedIndex]);
                getAlbumArtFromFile(0,filePaths[songList.SelectedIndex]);

                // Sets Album Cover for secondary ImageBox
                if (songList.SelectedIndex + 1 < songList.Items.Count)
                    getAlbumArtFromFile(1, filePaths[songList.SelectedIndex + 1]);
                else
                    albumArtImage2.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
                // Sets Album Cover for third ImageBox
                if (songList.SelectedIndex + 2 < songList.Items.Count)
                    getAlbumArtFromFile(2, filePaths[songList.SelectedIndex + 2]);
                else
                    albumArtImage3.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
            }
            catch (IndexOutOfRangeException) { 
                /*
                 * WinForms have SetSelected() method which uses try/catch for IndexOutOfRangeException
                 * After inspection of Listbox class i found out that catch field was blank
                 * So this field is ignored by me.
                 */
            }
            mPlayer.Play();
            if (!isMediaPlaying)
            {
                playButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Visible;
                isMediaPlaying = true;
            }
        }

        private void previousButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.SelectedIndex > 0)
            {
                songList.SelectedIndex -= 1;
            }
        }

        private void randomizeSongSelection<T>( T[] songPaths)
        {

            // Custom Randomize method

            Random rand = new Random();
            for (int i = 0; i < songPaths.Length; i++) {

                int j = rand.Next(i, songPaths.Length);
                T tempPaths = songPaths[i];
                songPaths[i] = songPaths[j];
                songPaths[j] = tempPaths;
            }
            

        }

        private void stopButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isMediaPlaying)
            {
                playButton.Visibility = Visibility.Visible;
                pauseButton.Visibility = Visibility.Hidden;
                mPlayer.Stop();
                isMediaPlaying = false;
            }
        }

        private void listButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Hide / Reveal songList and focus searchBox for input

            if (songList.Visibility == Visibility.Hidden)
            {
                songList.Visibility = Visibility.Visible;
                searchBox.Visibility = Visibility.Visible;
                searchBox.Focus();
                listButton.Opacity = 0.5;
            }
            else {
                songList.Visibility = Visibility.Hidden;
                songListFiltered.Visibility = Visibility.Hidden;
                searchBox.Visibility = Visibility.Hidden;
                listButton.Opacity = 1;
            }
        }

        private void pauseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isMediaPlaying) {
                playButton.Visibility = Visibility.Visible;
                pauseButton.Visibility = Visibility.Hidden;
                mPlayer.Pause();
                isMediaPlaying = false;
            }
        }


        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            /*
             * Sets and saves value from volumeSlider to be used after closing application
             */

            mPlayer.Volume = (double)volumeSlider.Value / 100;
            Settings.Default.volumelevel = (int)volumeSlider.Value;
            Settings.Default.Save();
            Settings.Default.Reload();
        }

        private void durationSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            // Sets player position when Left Mouse button is released

            mPlayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);
        }

        private void durationSlider_DragCompleted(object sender, EventArgs e) {

            // Sets player position when drag is completed

            mPlayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);
            if(!durationTimer.IsEnabled)
                durationTimer.Start();
        }
        
        private void durationSlider_DragStarted(object sender, EventArgs e) {

            // Stop durationTimer to prevent changing value when dragging

            if (durationTimer.IsEnabled) {
                durationTimer.Stop();
            } 
        }

        private void durationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            /*
             * Set currentPosition.text every time when value is changed
             * Cant use this event instead of LeftMouseButtonUp event ...
             * ... because of the durationTimer
             */

            TimeSpan t = TimeSpan.FromSeconds(durationSlider.Value);
            string time = t.ToString(@"mm\:ss");
            currentPosition.Text = time;
        }

        private void shuffleButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Randomize array, clear list and put randomized array back to list

            if (songList.Items.Count > 0)
            {
                randomizeSongSelection(filePaths);
                songList.Items.Clear();

                for (int i = 0; i < filePaths.Length; i++)
                {

                    songList.Items.Add(System.IO.Path.GetFileNameWithoutExtension(filePaths[i]));

                }

                getAlbumArtFromFile(1, filePaths[0]);
                getAlbumArtFromFile(2, filePaths[1]);

            }
        }

        private void volumeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (mPlayer.Volume != 0.0D) {
                tempVolume = mPlayer.Volume;
                volumeSlider.Value = 0.0D;
                mPlayer.Volume = 0.0D;
                volumeImage.Opacity = 0.5;
            }
            else
            {
                mPlayer.Volume = tempVolume;
                volumeSlider.Value = mPlayer.Volume * 100;
                volumeImage.Opacity = 1;
            }
                
        }

        private void settingsButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Open SettingsWindow form

            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            settingsWindow.Activate();
        }

        private void nextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.SelectedIndex < songList.Items.Count - 1) {
                songList.SelectedIndex += 1;
            }
        }

        private void songListFiltered_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
             * This time instead of SelectionIndex i used SelectedItem
             * This makes sure that track from songListFiltered is equal to the songList one
             */
            songList.SelectedItem = songListFiltered.SelectedItem;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
             * Hide songList and Reveal songListFiltered
             * Every Time when key is pressed this event is called
             * Adds matches to songListFiltered
             * Erase searchBox for songList reveal
             */
            if(string.IsNullOrEmpty(searchBox.Text) == false)
            {

                if (songListFiltered.Visibility == Visibility.Hidden) {
                    songListFiltered.Visibility = Visibility.Visible;
                }

                songListFiltered.Items.Clear();
                foreach(string item in songList.Items)
                {
                    if (item.IndexOf(searchBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        songListFiltered.Items.Add(item);
                    }
                }

            }else if(searchBox.Text == "")
            {
                if (songListFiltered.Visibility == Visibility.Visible)
                {
                    songListFiltered.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Window_OnKeyPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {

            /*
             * When searchBox is not focused then this event is being called ...
             * ... every time when key is pressed.
             */

            switch (e.Key)
            {
                case Key.Space:
                    if (!isMediaPlaying)
                    {
                        mPlayer.Play();
                        playButton.Visibility = Visibility.Hidden;
                        pauseButton.Visibility = Visibility.Visible;
                        isMediaPlaying = true;
                    }
                    else
                    {
                        mPlayer.Pause();
                        playButton.Visibility = Visibility.Visible;
                        pauseButton.Visibility = Visibility.Hidden;
                        isMediaPlaying = false;
                    }
                    break;
            }
        }

    }
}
