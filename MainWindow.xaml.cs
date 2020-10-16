﻿using Clair.Properties;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        Musicplayer musicplayer;
        TaskbarIcon taskbarIcon = new TaskbarIcon();
        public DispatcherTimer durationTimer = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            musicplayer = new Musicplayer(this);
            // Bind Events

            taskbarIcon.TrayLeftMouseDown += taskbar_TrayLeftMouseDown;
            this.StateChanged += Window_StateChanged;
            this.KeyDown += Window_OnKeyPressed;
            durationTimer.Interval = new TimeSpan(0, 0, 1);
            durationTimer.Tick += DurationTimer_Tick;
            volumeSlider.IsMoveToPointEnabled = true;
            blurBackground.Source = new BitmapImage(new Uri("assets/images/pixel.png", UriKind.RelativeOrAbsolute));

            // User Settings

            volumeSlider.Value = (double) Settings.Default.volumelevel;

            if (Settings.Default.isCheckingUpdates)
                VersionHandler.checkForUpdates();

            if (Settings.Default.lastKnownDirectory != "nopath")
                musicplayer.loadLastKnownDirectory();
        }

        private void DurationTimer_Tick(object sender, EventArgs e)
        {
            if (musicplayer.isMediaPlaying)
                durationSlider.Value = (int) musicplayer.Position.TotalSeconds;
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

        private void playButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.Play();
        }

        private void folderButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.openFileDialog();
        }

        private void extractMetadata(String path)
        {
            TagLib.File file = TagLib.File.Create(path);
            if (file.Tag.Title != null)
            {
                songTitle.Content = file.Tag.Title;
                artistTitle.Content = file.Tag.Artists[0];
            }
            else
            {
                songTitle.Content = songList.Items[songList.SelectedIndex];
                artistTitle.Content = null;
            }

            if (file.Tag.Lyrics != null)
                lyricsField.Text = file.Tag.Lyrics;
            else
                lyricsField.Text = "No Lyrics Available.";
        }

        private bool hasAlbumArt(String path)
        {
            TagLib.File file = TagLib.File.Create(path);
            var albumPicture = file.Tag.Pictures.FirstOrDefault();
            return (albumPicture != null) ? true : false;
        }

        private ImageSource extractAlbumArt(String path)
        {
            TagLib.File file = TagLib.File.Create(path);
            var albumPicture = file.Tag.Pictures.FirstOrDefault();
            if (albumPicture != null)
            {
                MemoryStream memory = new MemoryStream(albumPicture.Data.Data);
                memory.Seek(0, SeekOrigin.Begin);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();
                return bitmap;
            }
            else
                return new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
        }

        private void songList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                String path = musicplayer.filePaths[songList.SelectedIndex];
                musicplayer.Prepare(path);
                extractMetadata(path);
                albumArtImage.Source = extractAlbumArt(path);

                if (hasAlbumArt(path))
                {
                    blurBackground.Source = extractAlbumArt(path);
                    menuBackground.Opacity = 0.7;
                    songList.Background.Opacity = 0.7;
                    songListFiltered.Background.Opacity = 0.7;
                }
                else
                {
                    blurBackground.Source = new BitmapImage(new Uri("assets/images/pixel.png", UriKind.RelativeOrAbsolute));
                    menuBackground.Opacity = 1;
                    songList.Background.Opacity = 1;
                    songListFiltered.Background.Opacity = 1;
                }

                if (songList.SelectedIndex + 1 < songList.Items.Count)
                    albumArtImage2.Source = extractAlbumArt(musicplayer.filePaths[songList.SelectedIndex + 1]);
                else
                    albumArtImage2.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));

                if (songList.SelectedIndex + 2 < songList.Items.Count)
                    albumArtImage3.Source = extractAlbumArt(musicplayer.filePaths[songList.SelectedIndex + 2]);
                else
                    albumArtImage3.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
            }
            catch (IndexOutOfRangeException) { }
            
        }

        private void previousButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.SelectedIndex > 0)
                songList.SelectedIndex -= 1;
        }

        private void stopButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.Stop();
        }

        private void listButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            musicplayer.Volume = volumeSlider.Value / 100;
            Settings.Default.volumelevel = (int)volumeSlider.Value;
            Settings.Default.Save();
            Settings.Default.Reload();
        }

        private void durationSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            musicplayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);
        }

        private void durationSlider_DragCompleted(object sender, EventArgs e) 
        {
            musicplayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);
            durationTimer.Start();
        }

        private void durationSlider_DragStarted(object sender, EventArgs e) 
        {
            durationTimer.Stop();
        }

        private void durationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan t = TimeSpan.FromSeconds(durationSlider.Value);
            string time = t.ToString(@"mm\:ss");
            currentPosition.Text = time;
        }

        private void shuffleButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(songList.Items.Count > 1)
            {
                musicplayer.Shuffle();
                albumArtImage2.Source = extractAlbumArt(musicplayer.filePaths[0]);
                albumArtImage3.Source = extractAlbumArt(musicplayer.filePaths[1]);
            }
        }

        private void volumeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            musicplayer.Mute();
        }

        private void settingsButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            settingsWindow.Activate();
        }

        private void nextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.SelectedIndex < songList.Items.Count - 1)
                songList.SelectedIndex += 1;
        }

        private void songListFiltered_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            songList.SelectedItem = songListFiltered.SelectedItem;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
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
            switch (e.Key)
            {
                case Key.Space:
                    if (musicplayer.isMediaPlaying)
                        musicplayer.Play();
                    else
                        musicplayer.Pause();
                    break;
                case Key.Escape:
                    if (musicplayer.isMediaPlaying)
                        musicplayer.Stop();
                    break;
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
