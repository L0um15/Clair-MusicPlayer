namespace Musicplayer
{
    partial class Musicplayer
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Musicplayer));
            this.playButton = new System.Windows.Forms.PictureBox();
            this.pauseButton = new System.Windows.Forms.PictureBox();
            this.stopButton = new System.Windows.Forms.PictureBox();
            this.folderButton = new System.Windows.Forms.PictureBox();
            this.mPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.albumArtPicturebox = new System.Windows.Forms.PictureBox();
            this.Artist = new System.Windows.Forms.Label();
            this.artistLabel = new System.Windows.Forms.Label();
            this.songTitleLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.songList = new System.Windows.Forms.ListBox();
            this.expandListButton = new System.Windows.Forms.PictureBox();
            this.noAlbumPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.folderButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtPicturebox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expandListButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noAlbumPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.Location = new System.Drawing.Point(160, 436);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(64, 64);
            this.playButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playButton.TabIndex = 1;
            this.playButton.TabStop = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
            this.pauseButton.Location = new System.Drawing.Point(230, 445);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(48, 48);
            this.pauseButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pauseButton.TabIndex = 2;
            this.pauseButton.TabStop = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(106, 445);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(48, 48);
            this.stopButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stopButton.TabIndex = 3;
            this.stopButton.TabStop = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // folderButton
            // 
            this.folderButton.Image = ((System.Drawing.Image)(resources.GetObject("folderButton.Image")));
            this.folderButton.Location = new System.Drawing.Point(284, 445);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(48, 48);
            this.folderButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.folderButton.TabIndex = 4;
            this.folderButton.TabStop = false;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // mPlayer
            // 
            this.mPlayer.AccessibleName = "mPlayer";
            this.mPlayer.Enabled = true;
            this.mPlayer.Location = new System.Drawing.Point(10, 12);
            this.mPlayer.Name = "mPlayer";
            this.mPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mPlayer.OcxState")));
            this.mPlayer.Size = new System.Drawing.Size(31, 35);
            this.mPlayer.TabIndex = 5;
            this.mPlayer.Visible = false;
            this.mPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.mPlayer_PlayStateChange);
            // 
            // albumArtPicturebox
            // 
            this.albumArtPicturebox.Location = new System.Drawing.Point(47, 12);
            this.albumArtPicturebox.Name = "albumArtPicturebox";
            this.albumArtPicturebox.Size = new System.Drawing.Size(289, 287);
            this.albumArtPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.albumArtPicturebox.TabIndex = 6;
            this.albumArtPicturebox.TabStop = false;
            // 
            // Artist
            // 
            this.Artist.AutoSize = true;
            this.Artist.ForeColor = System.Drawing.SystemColors.Control;
            this.Artist.Location = new System.Drawing.Point(45, 314);
            this.Artist.Name = "Artist";
            this.Artist.Size = new System.Drawing.Size(0, 13);
            this.Artist.TabIndex = 7;
            // 
            // artistLabel
            // 
            this.artistLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.artistLabel.Location = new System.Drawing.Point(47, 314);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(285, 23);
            this.artistLabel.TabIndex = 8;
            this.artistLabel.Text = " ";
            // 
            // songTitleLabel
            // 
            this.songTitleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.songTitleLabel.Location = new System.Drawing.Point(47, 337);
            this.songTitleLabel.Name = "songTitleLabel";
            this.songTitleLabel.Size = new System.Drawing.Size(289, 23);
            this.songTitleLabel.TabIndex = 10;
            this.songTitleLabel.Text = " ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.panel1.Controls.Add(this.songList);
            this.panel1.Location = new System.Drawing.Point(386, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 516);
            this.panel1.TabIndex = 11;
            // 
            // songList
            // 
            this.songList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.songList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.songList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songList.ForeColor = System.Drawing.SystemColors.Window;
            this.songList.FormattingEnabled = true;
            this.songList.ItemHeight = 18;
            this.songList.Location = new System.Drawing.Point(12, 11);
            this.songList.Name = "songList";
            this.songList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.songList.Size = new System.Drawing.Size(324, 486);
            this.songList.TabIndex = 0;
            this.songList.SelectedIndexChanged += new System.EventHandler(this.songList_SelectedIndexChanged);
            // 
            // expandListButton
            // 
            this.expandListButton.Image = ((System.Drawing.Image)(resources.GetObject("expandListButton.Image")));
            this.expandListButton.Location = new System.Drawing.Point(346, 12);
            this.expandListButton.Name = "expandListButton";
            this.expandListButton.Size = new System.Drawing.Size(32, 32);
            this.expandListButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.expandListButton.TabIndex = 12;
            this.expandListButton.TabStop = false;
            this.expandListButton.Click += new System.EventHandler(this.expandListButton_Click);
            // 
            // noAlbumPictureBox
            // 
            this.noAlbumPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("noAlbumPictureBox.Image")));
            this.noAlbumPictureBox.Location = new System.Drawing.Point(10, 64);
            this.noAlbumPictureBox.Name = "noAlbumPictureBox";
            this.noAlbumPictureBox.Size = new System.Drawing.Size(31, 34);
            this.noAlbumPictureBox.TabIndex = 13;
            this.noAlbumPictureBox.TabStop = false;
            this.noAlbumPictureBox.Visible = false;
            // 
            // Musicplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InfoText;
            this.ClientSize = new System.Drawing.Size(734, 515);
            this.Controls.Add(this.noAlbumPictureBox);
            this.Controls.Add(this.expandListButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.songTitleLabel);
            this.Controls.Add(this.artistLabel);
            this.Controls.Add(this.Artist);
            this.Controls.Add(this.albumArtPicturebox);
            this.Controls.Add(this.mPlayer);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Musicplayer";
            this.Text = "MusicPlayer";
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.folderButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtPicturebox)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.expandListButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noAlbumPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox playButton;
        private System.Windows.Forms.PictureBox pauseButton;
        private System.Windows.Forms.PictureBox stopButton;
        private System.Windows.Forms.PictureBox folderButton;
        private AxWMPLib.AxWindowsMediaPlayer mPlayer;
        private System.Windows.Forms.PictureBox albumArtPicturebox;
        private System.Windows.Forms.Label Artist;
        private System.Windows.Forms.Label artistLabel;
        private System.Windows.Forms.Label songTitleLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox songList;
        private System.Windows.Forms.PictureBox expandListButton;
        private System.Windows.Forms.PictureBox noAlbumPictureBox;
    }
}

