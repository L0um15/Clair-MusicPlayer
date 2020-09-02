using Hardcodet.Wpf.TaskbarNotification;
using Hardcodet.Wpf.TaskbarNotification.Interop;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Clair
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String[] fileNames, filePaths;
        private bool isMediaPlaying = false;
        private MediaPlayer mPlayer = new MediaPlayer();
        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        TaskbarIcon taskbarIcon = new TaskbarIcon();

        public MainWindow()
        {
            InitializeComponent();
            mPlayer.MediaEnded += mPlayer_MediaEnded;
            mPlayer.MediaOpened += mPlayer_MediaOpened;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            this.StateChanged += Window_StateChanged;
            taskbarIcon.TrayLeftMouseDown += taskbar_TrayLeftMouseDown;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            volumeSlider.IsMoveToPointEnabled = true;
            
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
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
            if (this.WindowState == WindowState.Minimized) {
                this.WindowState = WindowState.Normal;
                this.Activate();
                this.ShowInTaskbar = true;
                taskbarIcon.Visibility = Visibility.Hidden;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs args) {
            //TODO: Put TrackTimer here.

            if (isMediaPlaying)
            {
                durationSlider.Value = (int) mPlayer.Position.TotalSeconds;
            }

        }

        private void mPlayer_MediaOpened(object sender, EventArgs args) {
            double duration = mPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            durationSlider.Maximum = (int)duration;
            durationSlider.IsMoveToPointEnabled = true;
            dispatcherTimer.Start();
        }

        private void mPlayer_MediaEnded(object sender, EventArgs args) {

            if (songList.SelectedIndex < songList.Items.Count - 1) {
                songList.SelectedIndex += 1;
            }

        }



        private void getCurrentMetaFromMusic(String path) {

            TagLib.File file = TagLib.File.Create(path);

            songTitle.Content = file.Tag.Title;

            artistTitle.Content = file.Tag.Artists[0];


            var picture = file.Tag.Pictures.FirstOrDefault();

            if (picture != null)
            {
                MemoryStream memory = new MemoryStream(picture.Data.Data);

                memory.Seek(0, SeekOrigin.Begin);
                
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();


                albumArtImage.Source = bitmap;
            }
            else { 
                //Music file does not contain album art.

            }
        }

        private void getNextMetaFromMusic(String path)
        {

            TagLib.File file = TagLib.File.Create(path);

            var picture = file.Tag.Pictures.FirstOrDefault();

            if (picture != null)
            {
                MemoryStream memory = new MemoryStream(picture.Data.Data);

                memory.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();


                albumArtImage2.Source = bitmap;
            }
            else
            {
                //Music file does not contain album art.

            }
        }

        private void getFutureMetaFromMusic(String path)
        {

            TagLib.File file = TagLib.File.Create(path);

            var picture = file.Tag.Pictures.FirstOrDefault();

            if (picture != null)
            {
                MemoryStream memory = new MemoryStream(picture.Data.Data);

                memory.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = memory;
                bitmap.EndInit();


                albumArtImage3.Source = bitmap;
            }
            else
            {
                //Music file does not contain album art.

            }
        }

        private void playButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mPlayer.Play();
            if (!isMediaPlaying) {
                playButton.Visibility = Visibility.Hidden;
                pauseButton.Visibility = Visibility.Visible;
                isMediaPlaying = true;
            }
        }

        private void folderButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ofd.Multiselect = true;

            if (ofd.ShowDialog() == true)
            {

                if (songList.Items.Count > 0) {
                    songList.Items.Clear();
                }

                fileNames = ofd.SafeFileNames;
                filePaths = ofd.FileNames;

                for (int i = 0; i < fileNames.Length; i++) {
                    songList.Items.Add(fileNames[i]);
                }

                songList.SelectedIndex = 0;

            }
        }

        private void songList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mPlayer.Open(new Uri(filePaths[songList.SelectedIndex]));
                getCurrentMetaFromMusic(filePaths[songList.SelectedIndex]);

                if (songList.SelectedIndex + 1 < songList.Items.Count)
                {
                    getNextMetaFromMusic(filePaths[songList.SelectedIndex + 1]);
                }
                else {
                    albumArtImage2.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
                }
                    
                if (songList.SelectedIndex + 2 < songList.Items.Count)
                {
                    getFutureMetaFromMusic(filePaths[songList.SelectedIndex + 2]);
                }
                else {
                    albumArtImage3.Source = new BitmapImage(new Uri("assets/images/noalbum.png", UriKind.RelativeOrAbsolute));
                }
                    


            }
            catch (IndexOutOfRangeException) { 
                //Ignore this...
                //SetSelected Method from WinForms uses try/catch to ignore IndexOutOfRangeException.
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

        private void arrowButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.Visibility == Visibility.Hidden)
            {
                songList.Visibility = Visibility.Visible;
            }
            else {
                songList.Visibility = Visibility.Hidden;
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
            mPlayer.Volume = (double)volumeSlider.Value / 100;
        }

        private void durationSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mPlayer.Position = TimeSpan.FromSeconds((double)durationSlider.Value);
        }

        private void nextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (songList.SelectedIndex < songList.Items.Count - 1) {
                songList.SelectedIndex += 1;
            }
        }
    }
}
