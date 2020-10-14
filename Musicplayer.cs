using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Clair.Properties;
using System.IO;
using System.Windows.Threading;

namespace Clair
{
    class Musicplayer
    {
        
        MainWindow masterClass;
        bool isMediaPlaying;
        Double tempVolume;
        public String[] filePaths;
        OpenFileDialog ofd = new OpenFileDialog();
        DispatcherTimer durationTimer = new DispatcherTimer();
        MediaPlayer mPlayer = new MediaPlayer();

        public Musicplayer(MainWindow mainWindow)
        {
            // Initialize.
            masterClass = mainWindow;
            mPlayer.MediaEnded += MPlayer_MediaEnded;
            mPlayer.MediaOpened += MPlayer_MediaOpened;
            durationTimer.Interval = new TimeSpan(0, 0, 1);
            durationTimer.Tick += DurationTimer_Tick;
        }

        

        public void Play()
        {
            mPlayer.Play();
            if (!isMediaPlaying)
            {
                masterClass.playButton.Visibility = Visibility.Hidden;
                masterClass.pauseButton.Visibility = Visibility.Visible;
                isMediaPlaying = true;
            }
        }
        public void Pause()
        {
            mPlayer.Pause();
            if (isMediaPlaying)
            {
                masterClass.playButton.Visibility = Visibility.Visible;
                masterClass.pauseButton.Visibility = Visibility.Hidden;
                isMediaPlaying = false;
            }
        }
        public void Stop()
        {
            mPlayer.Stop();
            if (isMediaPlaying)
            {
                masterClass.playButton.Visibility = Visibility.Visible;
                masterClass.pauseButton.Visibility = Visibility.Hidden;
                isMediaPlaying = false;
            }
        }
        public void Prepare(String path)
        {
            // Fetch list and prepare track.
            try {
                mPlayer.Open(new Uri(path));
            }
            catch (IndexOutOfRangeException) { }
            Play();
        }
        public void Shuffle()
        {
            if(masterClass.songList.Items.Count > 0)
            {
                randomizeTrackSelection(filePaths);
                masterClass.songList.Items.Clear();
                for (int i = 0; i < filePaths.Length; i++)
                {
                    masterClass.songList.Items.Add(Path.GetFileNameWithoutExtension(filePaths[i]));
                }
            }
        }
        public void Mute()
        {
            if (mPlayer.Volume != 0.0D)
            {
                tempVolume = mPlayer.Volume;
                masterClass.volumeSlider.Value = 0.0D;
                mPlayer.Volume = 0.0D;
                masterClass.volumeImage.Opacity = 0.5;
            }
            else
            {
                mPlayer.Volume = tempVolume;
                masterClass.volumeSlider.Value = mPlayer.Volume * 100;
                masterClass.volumeImage.Opacity = 1;
            }
        }
        public void setVolume(double value)
        {
            mPlayer.Volume = value;
        }
        public void setPosition(TimeSpan value)
        {
            mPlayer.Position = value;
        }
        public void openFileDialog()
        {
            ofd.Multiselect = true;
            if (ofd.ShowDialog() != true) return;
            
            if (!Settings.Default.isUnsupportedExtensionsEnabled)
                ofd.Filter = "Music files (*.mp3) | *.mp3";
            else
                ofd.Filter = "All files (*.*) | *.*";

            if (masterClass.songList.Items.Count > 0) {
                masterClass.songList.Items.Clear();
                masterClass.songListFiltered.Items.Clear();
            }

            filePaths = ofd.FileNames;

            if (Settings.Default.isAutoShuffleEnabled)
                randomizeTrackSelection(filePaths);

            for (int i = 0; i < filePaths.Length; i++)
            {
                masterClass.songList.Items.Add(Path.GetFileNameWithoutExtension(filePaths[i]));
            }

            Settings.Default.lastKnownDirectory = Path.GetDirectoryName(filePaths[0]);
            Settings.Default.Save();

            masterClass.songList.SelectedIndex = 0;

        }

        public void StartDurationTimer()
        {
            if (!durationTimer.IsEnabled)
                durationTimer.Start();
        }
        public void StopDurationTimer()
        {
            if (durationTimer.IsEnabled)
                durationTimer.Stop();
        }

        public void loadLastKnownDirectory()
        {
            try
            {
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
                randomizeTrackSelection(filePaths);

            for (int i = 0; i < filePaths.Length; i++)
            {
                masterClass.songList.Items.Add(Path.GetFileNameWithoutExtension(filePaths[i]));
            }
            
            masterClass.songList.SelectedIndex = 0;
            
            Pause();
        }

        private void randomizeTrackSelection<T>(T[] songPaths)
        {
            // Custom Randomize method
            Random rand = new Random();
            for (int i = 0; i < songPaths.Length; i++)
            {
                int j = rand.Next(i, songPaths.Length);
                T tempPaths = songPaths[i];
                songPaths[i] = songPaths[j];
                songPaths[j] = tempPaths;
            }
        }

        private void MPlayer_MediaOpened(object sender, EventArgs e)
        {
            double duration = mPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            masterClass.durationSlider.Maximum = (int)duration;
            masterClass.durationSlider.IsMoveToPointEnabled = true;
            TimeSpan t = mPlayer.NaturalDuration.TimeSpan;
            string time = t.ToString(@"mm\:ss");
            masterClass.totalTime.Text = time;
            StartDurationTimer();
        }

        private void MPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (masterClass.songList.SelectedIndex < masterClass.songList.Items.Count - 1)
                masterClass.songList.SelectedIndex += 1;
            else
                Stop();
        }
        private void DurationTimer_Tick(object sender, EventArgs e)
        {
            if (isMediaPlaying)
                masterClass.durationSlider.Value = (int)mPlayer.Position.TotalSeconds;
        }

    }
}
