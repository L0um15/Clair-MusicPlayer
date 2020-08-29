using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Musicplayer
{
    public partial class Musicplayer : Form
    {

        TagLib.File file;
        String[] fileNames, filePaths;
        OpenFileDialog ofd = new OpenFileDialog();
        Boolean isLeftPanelVisible = false;
        public Musicplayer()
        {
            InitializeComponent();
            albumArtPicturebox.Image = noAlbumPictureBox.Image;
            albumArtNextPicturebox.Image = noAlbumPictureBox.Image;
            albumArtFuturePicturebox.Image = noAlbumPictureBox.Image;
            songTitleLabel.Text = "No Music file selected";
        }
        private void playButton_Click(object sender, EventArgs e)
        {
            mPlayer.Ctlcontrols.play();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            mPlayer.Ctlcontrols.stop();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            mPlayer.Ctlcontrols.pause();
        }


        private void songList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mPlayer.URL = filePaths[songList.SelectedIndex];

            getCurrentMetaFromMusic(filePaths[songList.SelectedIndex]);

            if (songList.SelectedIndex + 1 < songList.Items.Count) {
                getNextMetaFromMusic(filePaths[songList.SelectedIndex + 1]);
            }

            if (songList.SelectedIndex + 2 < songList.Items.Count) {
                getFutureMetaFromMusic(filePaths[songList.SelectedIndex + 2]);
            }

            

            
            

        }

        private void expandListButton_Click(object sender, EventArgs e)
        {
            if (isLeftPanelVisible)
            {
                leftpanel.Hide();
                isLeftPanelVisible = false;
            }
            else {
                leftpanel.Show();
                isLeftPanelVisible = true;
            }
        }

        

        private void folderButton_Click(object sender, EventArgs e)
        {
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK) {
                fileNames = ofd.SafeFileNames;
                filePaths = ofd.FileNames;

                if (songList.Items.Count > 0) {
                    songList.Items.Clear();
                }
                

                for (int i = 0; i < fileNames.Length; i++)
                {
                    songList.Items.Add(fileNames[i]);
                }

                songList.SetSelected(0, true);

                mPlayer.Ctlcontrols.play();
            }
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex > 0)
            {
                songList.SetSelected(songList.SelectedIndex - 1, true);
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex < songList.Items.Count - 1) {
                songList.SetSelected(songList.SelectedIndex + 1, true);
            }
            
        }

        private void getCurrentMetaFromMusic(String path) {


            file = TagLib.File.Create(path);

            artistLabel.Text = file.Tag.Artists[0];
            songTitleLabel.Text = file.Tag.Title;

            var mstream = new MemoryStream();
            var picture = file.Tag.Pictures.FirstOrDefault();
            if (picture != null)
            {
                byte[] data = picture.Data.Data;
                mstream.Write(data, 0, Convert.ToInt32(data.Length));
                var bm = new Bitmap(mstream, false);
                mstream.Dispose();
                albumArtPicturebox.Image = bm;
            }
            else
            {
                // Ouch it appears that music file has no album art PeepoSad.
                albumArtPicturebox.Image = noAlbumPictureBox.Image;

            }

        }

        private void getNextMetaFromMusic(String path)
        {


            file = TagLib.File.Create(path);

            var mstream = new MemoryStream();
            var picture = file.Tag.Pictures.FirstOrDefault();
            if (picture != null)
            {
                byte[] data = picture.Data.Data;
                mstream.Write(data, 0, Convert.ToInt32(data.Length));
                var bm = new Bitmap(mstream, false);
                mstream.Dispose();
                albumArtNextPicturebox.Image = bm;
            }
            else
            {
                // Ouch it appears that music file has no album art PeepoSad.
                albumArtNextPicturebox.Image = noAlbumPictureBox.Image;

            }

        }

        private void getFutureMetaFromMusic(String path)
        {


            file = TagLib.File.Create(path);

            var mstream = new MemoryStream();
            var picture = file.Tag.Pictures.FirstOrDefault();
            if (picture != null)
            {
                byte[] data = picture.Data.Data;
                mstream.Write(data, 0, Convert.ToInt32(data.Length));
                var bm = new Bitmap(mstream, false);
                mstream.Dispose();
                albumArtFuturePicturebox.Image = bm;
            }
            else
            {
                // Ouch it appears that music file has no album art PeepoSad.
                albumArtFuturePicturebox.Image = noAlbumPictureBox.Image;

            }

        }


        private void Musicplayer_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) {
                notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                notifyIcon.BalloonTipText = "I'be waiting here.";
                notifyIcon.ShowBalloonTip(1000);
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void volumeslider_ValueChanged(object sender, EventArgs e)
        {
            mPlayer.settings.volume = (int) volumeslider.Value;
        }

        private void durationsliderProgression_Tick(object sender, EventArgs e)
        {
            durationslider.Value = (int)mPlayer.Ctlcontrols.currentPosition;
        }

        private void durationslider_MouseClick(object sender, MouseEventArgs e)
        {
            mPlayer.Ctlcontrols.currentPosition = (int) durationslider.Value;
        }

        private void mPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {

            if (e.newState == 3)
            {
                playButton.Hide();
                pauseButton.Show();

                double duration = mPlayer.currentMedia.duration;

                durationslider.Maximum = (int)duration;
            }
            else {
                pauseButton.Hide();
                playButton.Show();
            }
            
            if (e.newState == 8) {

                if (songList.SelectedIndex != songList.Items.Count - 1)
                { 
                    BeginInvoke(new Action(() => {
                        songList.SetSelected(songList.SelectedIndex + 1, true);
                    }));
                }

            }
        }



    }
}
