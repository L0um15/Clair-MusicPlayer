using Clair.Properties;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Clair
{
    public partial class MainWindow : Window
    {

        // Global Variables
        String version = "pre-0.7.0";
        Musicplayer musicplayer;
        TaskbarIcon taskbarIcon = new TaskbarIcon();

        public MainWindow()
        {
            InitializeComponent();
            musicplayer = new Musicplayer(this);
            // Bind Events

            taskbarIcon.TrayLeftMouseDown += taskbar_TrayLeftMouseDown;
            this.StateChanged += Window_StateChanged;
            this.KeyDown += Window_OnKeyPressed;
            volumeSlider.IsMoveToPointEnabled = true;

            blurBackground.Source = new BitmapImage(new Uri("assets/images/pixel.png", UriKind.RelativeOrAbsolute));

            // User Settings

            volumeSlider.Value = (double) Settings.Default.volumelevel;

            if (Settings.Default.isCheckingUpdates)
                checkVersion();

            if (Settings.Default.lastKnownDirectory != "nopath")
                musicplayer.loadLastKnownDirectory();
                /*try {
                    if (!Settings.Default.isUnsupportedExtensionsEnabled)
                        filePaths = Directory.GetFiles(Settings.Default.lastKnownDirectory, "*.mp3");
                    else
                        filePaths = Directory.GetFiles(Settings.Default.lastKnownDirectory, "*.*");
                } 
                catch (DirectoryNotFoundException)
                {
                    Settings.Default.lastKnownDirectory = "nopath";
                    Settings.Default.Save();
                    return;
                }

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

            }*/

        }
        private void checkVersion()
        {
            try
            {
                string ver = new WebClient().DownloadString("https://raw.githubusercontent.com/L0um15/Clair-MusicPlayer/master/version.txt");
                if (ver != version)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("New version is available to download.\n" +
                        "Should i open github page?", "Information", MessageBoxButton.YesNo);
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


        /*private void getArtistTagFromFile(String path) {

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
            
        }*/


        /*private void getAlbumArtFromFile(int index,String path) {

            // Get Album cover if available

            TagLib.File file = TagLib.File.Create(path);
            var picture = file.Tag.Pictures.FirstOrDefault();
            
            if (index == 0) {
                if (file.Tag.Lyrics != null)
                    lyricsField.Text = file.Tag.Lyrics;
                else
                    lyricsField.Text = "Lyrics are unavailable.";
            }
            if (picture != null)
            {
                MemoryStream memory = new MemoryStream(picture.Data.Data);
                memory.Seek(0, SeekOrigin.Begin);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();
                if (index == 0) {
                    albumArtImage.Source = bitmap;
                    blurBackground.Source = bitmap;
                    menuBackground.Opacity = 0.7;
                    songList.Background.Opacity = 0.7;
                    songListFiltered.Background.Opacity = 0.7;
                }
                else if (index == 1)
                    albumArtImage2.Source = bitmap;
                else if (index == 2)
                    albumArtImage3.Source = bitmap;

            }
            else {
                if (index == 0) {
                    albumArtImage.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
                    blurBackground.Source = new BitmapImage(new Uri("assets/images/pixel.png", UriKind.RelativeOrAbsolute));
                    menuBackground.Opacity = 1;
                    
                }
                else if (index == 1)
                    albumArtImage2.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
                else if (index == 2)
                    albumArtImage3.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
            }
        }*/

        private void playButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.Play();
        }

        private void folderButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.openFileDialog();
        }

        private void songList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            /*
             * Whole Musicplayer works with this event
             * Here every track gets processed and played
             * Every time when selection is changed, new track will be loaded ...
             * ... with equal index as position in filePaths array
             */
            musicplayer.Prepare(musicplayer.filePaths[songList.SelectedIndex]);
            /*try
            {
                // Gets FILE PATH from filePaths and open it
                mPlayer.Open(new Uri(filePaths[songList.SelectedIndex]));

                // Gets Metadata from file
                getArtistTagFromFile(filePaths[songList.SelectedIndex]);
                getAlbumArtFromFile(0,filePaths[songList.SelectedIndex]);

                // Sets Album Cover for second ImageBox
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
                *//*
                 * WinForms have SetSelected() method which uses try/catch for IndexOutOfRangeException
                 * After inspection of Listbox class i found out that catch field was blank
                 * So this field is ignored by me.
                 *//*
            }
            mPlayer.Play();
            if (!isMediaPlaying)
            {
                playButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Visible;
                isMediaPlaying = true;
            }*/
        }

        private void previousButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.SelectedIndex > 0)
                songList.SelectedIndex -= 1;
        }

        /*private void randomizeSongSelection<T>( T[] songPaths)
        {

            // Custom Randomize method

            Random rand = new Random();
            for (int i = 0; i < songPaths.Length; i++) {
                int j = rand.Next(i, songPaths.Length);
                T tempPaths = songPaths[i];
                songPaths[i] = songPaths[j];
                songPaths[j] = tempPaths;
            }
            

        }*/

        private void stopButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.Stop();
        }

        private void listButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            // Hide / Reveal songList and focus searchBox for input

            if (songList.Visibility == Visibility.Hidden)
            {
                if(songListFiltered.Visibility == Visibility.Visible)
                {
                    songList.Visibility = Visibility.Hidden;
                    songListFiltered.Visibility = Visibility.Hidden;
                    searchBox.Visibility = Visibility.Hidden;
                    listButton.Opacity = 1;
                    return;
                }
                if (lyricsWrapper.Visibility == Visibility.Visible) {
                    lyricsWrapper.Visibility = Visibility.Hidden;
                    lyricsButton.Opacity = 1;
                }
                    

                songList.Visibility = Visibility.Visible;
                songListFiltered.Visibility = Visibility.Hidden;
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
            musicplayer.Pause();
        }


        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*
             * Sets and saves value from volumeSlider to be used after closing application
             */

            musicplayer.setVolume(volumeSlider.Value / 100);
            Settings.Default.volumelevel = (int)volumeSlider.Value;
            Settings.Default.Save();
            Settings.Default.Reload();
        }

        private void durationSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Sets player position when Left Mouse button is released

            // mPlayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);
            musicplayer.setPosition(TimeSpan.FromSeconds((double)durationSlider.Value));
        }

        private void durationSlider_DragCompleted(object sender, EventArgs e) {

            // Sets player position when drag is completed

            /*mPlayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);*/
            musicplayer.setPosition(TimeSpan.FromSeconds((double)durationSlider.Value));
            musicplayer.StartDurationTimer();
        }

        private void durationSlider_DragStarted(object sender, EventArgs e) {

            // Stop durationTimer to prevent changing value when dragging

            musicplayer.StopDurationTimer();
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

            musicplayer.Shuffle();
        }

        private void volumeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            musicplayer.Mute();
                
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
                    songList.Visibility = Visibility.Hidden;
                }

                songListFiltered.Items.Clear();
                foreach(string item in songList.Items)
                {
                    if (item.IndexOf(searchBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        songListFiltered.Items.Add(item);
                    }
                }

            }
            else if(searchBox.Text == "")
            {
                if (songListFiltered.Visibility == Visibility.Visible)
                {
                    songListFiltered.Visibility = Visibility.Hidden;
                    songList.Visibility = Visibility.Visible;
                }
            }
        }

        private void Window_OnKeyPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {

            /*
             * When searchBox is not focused, this event is being called ...
             * ... every time when key is pressed.
             */

            switch (e.Key)
            {
                /*case Key.Space:
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
                case Key.Escape:
                    if (isMediaPlaying)
                    {
                        mPlayer.Stop();
                        playButton.Visibility = Visibility.Visible;
                        pauseButton.Visibility = Visibility.Hidden;
                        isMediaPlaying = false;
                    }
                    break;*/
                case Key.F1:
                    Process.Start("https://github.com/L0um15/Clair-MusicPlayer/issues/");
                    break;
                case Key.Right:
                    if (songList.SelectedIndex < songList.Items.Count - 1)
                        songList.SelectedIndex += 1;
                    break;
                case Key.Left:
                    if (songList.SelectedIndex > 0)
                        songList.SelectedIndex -= 1;
                    break;
                case Key.Up:
                    volumeSlider.Value += 5;
                    break;
                case Key.Down:
                    volumeSlider.Value -= 5;
                    break;

            }
        }

        private void lyricsButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (lyricsWrapper.Visibility != Visibility.Visible) {
                if (songList.Visibility == Visibility.Visible || songListFiltered.Visibility == Visibility.Visible)
                {
                    songList.Visibility = Visibility.Hidden;
                    songListFiltered.Visibility = Visibility.Hidden;
                    searchBox.Visibility = Visibility.Hidden;
                    listButton.Opacity = 1;
                }
                lyricsWrapper.Visibility = Visibility.Visible;
                lyricsButton.Opacity = 0.5;
            }
            else
            {
                lyricsWrapper.Visibility = Visibility.Hidden;
                lyricsButton.Opacity = 1;
            }

            
        }
    }
}
