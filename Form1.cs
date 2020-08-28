using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Musicplayer
{
    public partial class Musicplayer : Form
    {

        int orginalWidth, croppedWidth;
        String[] fileNames, filePaths;
        OpenFileDialog ofd = new OpenFileDialog();


        public Musicplayer()
        {
            InitializeComponent();
            orginalWidth = this.Width;
            croppedWidth = 401;
            this.Width = croppedWidth;
            albumArtPicturebox.Image = noAlbumPictureBox.Image;
            
            artistLabel.Font = new Font("arial", 14);
            artistLabel.Text = "No Music file selected";

        }
        private void playButton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }


        private void songList_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = filePaths[songList.SelectedIndex];

            getMetaFromMusic(filePaths[songList.SelectedIndex]);

        }

        private void expandListButton_Click(object sender, EventArgs e)
        {
            if (this.Width != orginalWidth)
            {
                this.Width = orginalWidth;
            }
            else {
                this.Width = croppedWidth;
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

                axWindowsMediaPlayer1.URL = filePaths[0];

                for (int i = 0; i < fileNames.Length; i++)
                {
                    songList.Items.Add(fileNames[i]);
                }

                getMetaFromMusic(filePaths[0]);

                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }


        private void getMetaFromMusic(String path) {


            TagLib.File file = TagLib.File.Create(path);

            artistLabel.Font = new Font("arial", 14);
            artistLabel.Text = file.Tag.Artists[0];


            songTitleLabel.Font = new Font("arial", 14);
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

    }
}
