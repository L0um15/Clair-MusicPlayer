using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using Clair.Properties;
using System.IO;
using System.Linq;

namespace Clair
{
    class Musicplayer
    {
        
        MainWindow masterClass;
        Double tempVolume;
        OpenFileDialog ofd = new OpenFileDialog();
        MediaPlayer mPlayer = new MediaPlayer();

        public bool isMediaPlaying { get; set; }
        public String[] filePaths { get; set; }
        public double Volume { get { return mPlayer.Volume; } set { mPlayer.Volume = value; } }
        public TimeSpan Position { get { return mPlayer.Position;} set { mPlayer.Position = value; } }



        public Musicplayer(MainWindow mainWindow)
        {
            // Initialize.
            masterClass = mainWindow;
            mPlayer.MediaEnded += MPlayer_MediaEnded;
            mPlayer.MediaOpened += MPlayer_MediaOpened;
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
            mPlayer.Open(new Uri(path));
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
        public void openFileDialog()
        {
            ofd.Multiselect = true;

            if (!Settings.Default.isUnsupportedExtensionsEnabled)
                ofd.Filter = "Music files (*.mp3) | *.mp3";
            else
                ofd.Filter = "All Media files (*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;" +
                    "*.mpa;*.mpe;*.m3u;*.mid;*.midi;*.rmi;*.aif;*.aifc;*.aiff;" +
                    "*.wav;*.mov;*.m4a;*.mp4;*.m4v;*.mp4v;*.3g2;*.3gp2;*.3gp;*.3gpp;*.aac) |" +
                    " *.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mid;*.midi;*.rmi;*.aif;*.aifc;*.aiff;" +
                    "*.wav;*.mov;*.m4a;*.mp4;*.m4v;*.mp4v;*.3g2;*.3gp2;*.3gp;*.3gpp;*.aac";

            if (ofd.ShowDialog() != true) return;

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

        public void loadLastKnownDirectory()
        {
            try
            {
                if (!Settings.Default.isUnsupportedExtensionsEnabled)
                    filePaths = Directory.GetFiles(Settings.Default.lastKnownDirectory, "*.mp3");
                else
                {
                    var allowedExtensions = ".mpg,.mpeg,.m1v,.mp2,.mp3," +
                    ".mpa,.mpe,.m3u,.mid,.midi,.rmi,.aif,.aifc,.aiff," +
                    ".wav,.mov,.m4a,.mp4,.m4v,.mp4v,.3g2,.3gp2,.3gp,.3gpp,.aac";
                    filePaths = Directory.GetFiles(Settings.Default.lastKnownDirectory, "*.*")
                        .Where(s => allowedExtensions.IndexOf(Path.GetExtension(s)) > -1).ToArray();
                }
                    //Do something here
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
            if (mPlayer.NaturalDuration.HasTimeSpan)
            {
                double duration = mPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                masterClass.durationSlider.Maximum = (int)duration;
                masterClass.durationSlider.IsMoveToPointEnabled = true;
                TimeSpan t = mPlayer.NaturalDuration.TimeSpan;
                string time = t.ToString(@"mm\:ss");
                masterClass.totalTime.Text = time;
                masterClass.durationTimer.Start();
            } 
        }

        private void MPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (masterClass.songList.SelectedIndex < masterClass.songList.Items.Count - 1)
                masterClass.songList.SelectedIndex += 1;
            else
                Stop();
        }
    }
}
