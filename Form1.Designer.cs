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
            this.components = new System.ComponentModel.Container();
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
            this.songList = new System.Windows.Forms.ListBox();
            this.expandListButton = new System.Windows.Forms.PictureBox();
            this.noAlbumPictureBox = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.durationslider = new ColorSlider.ColorSlider();
            this.volumeslider = new ColorSlider.ColorSlider();
            this.previousButton = new System.Windows.Forms.PictureBox();
            this.nextButton = new System.Windows.Forms.PictureBox();
            this.leftpanel = new System.Windows.Forms.Panel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.albumArtNextPicturebox = new System.Windows.Forms.PictureBox();
            this.albumArtFuturePicturebox = new System.Windows.Forms.PictureBox();
            this.durationsliderProgression = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.folderButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.expandListButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noAlbumPictureBox)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextButton)).BeginInit();
            this.leftpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtNextPicturebox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtFuturePicturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.Location = new System.Drawing.Point(612, 3);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(40, 40);
            this.playButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playButton.TabIndex = 1;
            this.playButton.TabStop = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
            this.pauseButton.Location = new System.Drawing.Point(612, 3);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(40, 40);
            this.pauseButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pauseButton.TabIndex = 2;
            this.pauseButton.TabStop = false;
            this.pauseButton.Visible = false;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.Location = new System.Drawing.Point(535, 8);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(32, 32);
            this.stopButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stopButton.TabIndex = 3;
            this.stopButton.TabStop = false;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // folderButton
            // 
            this.folderButton.Image = ((System.Drawing.Image)(resources.GetObject("folderButton.Image")));
            this.folderButton.Location = new System.Drawing.Point(696, 8);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(32, 32);
            this.folderButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.folderButton.TabIndex = 4;
            this.folderButton.TabStop = false;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // mPlayer
            // 
            this.mPlayer.AccessibleName = "mPlayer";
            this.mPlayer.Enabled = true;
            this.mPlayer.Location = new System.Drawing.Point(1221, 12);
            this.mPlayer.Name = "mPlayer";
            this.mPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mPlayer.OcxState")));
            this.mPlayer.Size = new System.Drawing.Size(31, 35);
            this.mPlayer.TabIndex = 5;
            this.mPlayer.Visible = false;
            this.mPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.mPlayer_PlayStateChange);
            // 
            // albumArtPicturebox
            // 
            this.albumArtPicturebox.Location = new System.Drawing.Point(485, 136);
            this.albumArtPicturebox.Name = "albumArtPicturebox";
            this.albumArtPicturebox.Size = new System.Drawing.Size(289, 289);
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
            this.artistLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.artistLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.artistLabel.Location = new System.Drawing.Point(54, 44);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(285, 23);
            this.artistLabel.TabIndex = 8;
            this.artistLabel.Text = " ";
            // 
            // songTitleLabel
            // 
            this.songTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songTitleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.songTitleLabel.Location = new System.Drawing.Point(54, 17);
            this.songTitleLabel.Name = "songTitleLabel";
            this.songTitleLabel.Size = new System.Drawing.Size(285, 23);
            this.songTitleLabel.TabIndex = 10;
            this.songTitleLabel.Text = " ";
            // 
            // songList
            // 
            this.songList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.songList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.songList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songList.ForeColor = System.Drawing.SystemColors.Window;
            this.songList.FormattingEnabled = true;
            this.songList.ItemHeight = 18;
            this.songList.Location = new System.Drawing.Point(12, 12);
            this.songList.Name = "songList";
            this.songList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.songList.Size = new System.Drawing.Size(282, 576);
            this.songList.TabIndex = 0;
            this.songList.SelectedIndexChanged += new System.EventHandler(this.songList_SelectedIndexChanged);
            // 
            // expandListButton
            // 
            this.expandListButton.Image = ((System.Drawing.Image)(resources.GetObject("expandListButton.Image")));
            this.expandListButton.Location = new System.Drawing.Point(0, 19);
            this.expandListButton.Name = "expandListButton";
            this.expandListButton.Size = new System.Drawing.Size(48, 48);
            this.expandListButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.expandListButton.TabIndex = 12;
            this.expandListButton.TabStop = false;
            this.expandListButton.Click += new System.EventHandler(this.expandListButton_Click);
            // 
            // noAlbumPictureBox
            // 
            this.noAlbumPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("noAlbumPictureBox.Image")));
            this.noAlbumPictureBox.Location = new System.Drawing.Point(1184, 12);
            this.noAlbumPictureBox.Name = "noAlbumPictureBox";
            this.noAlbumPictureBox.Size = new System.Drawing.Size(31, 34);
            this.noAlbumPictureBox.TabIndex = 13;
            this.noAlbumPictureBox.TabStop = false;
            this.noAlbumPictureBox.Visible = false;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.menu.Controls.Add(this.pictureBox1);
            this.menu.Controls.Add(this.durationslider);
            this.menu.Controls.Add(this.volumeslider);
            this.menu.Controls.Add(this.previousButton);
            this.menu.Controls.Add(this.nextButton);
            this.menu.Controls.Add(this.folderButton);
            this.menu.Controls.Add(this.expandListButton);
            this.menu.Controls.Add(this.pauseButton);
            this.menu.Controls.Add(this.artistLabel);
            this.menu.Controls.Add(this.songTitleLabel);
            this.menu.Controls.Add(this.playButton);
            this.menu.Controls.Add(this.stopButton);
            this.menu.Location = new System.Drawing.Point(0, 597);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1265, 86);
            this.menu.TabIndex = 14;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1049, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // durationslider
            // 
            this.durationslider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.durationslider.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.durationslider.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.durationslider.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.durationslider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.durationslider.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.durationslider.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.durationslider.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.durationslider.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.durationslider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.durationslider.LargeChange = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.durationslider.Location = new System.Drawing.Point(345, 44);
            this.durationslider.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.durationslider.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.durationslider.Name = "durationslider";
            this.durationslider.Padding = 5;
            this.durationslider.ScaleDivisions = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.durationslider.ScaleSubDivisions = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.durationslider.ShowDivisionsText = true;
            this.durationslider.ShowSmallScale = false;
            this.durationslider.Size = new System.Drawing.Size(567, 40);
            this.durationslider.SmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.durationslider.TabIndex = 19;
            this.durationslider.Text = "colorSlider1";
            this.durationslider.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.durationslider.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.durationslider.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.durationslider.ThumbRoundRectSize = new System.Drawing.Size(16, 16);
            this.durationslider.ThumbSize = new System.Drawing.Size(16, 16);
            this.durationslider.TickAdd = 0F;
            this.durationslider.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.durationslider.TickDivide = 0F;
            this.durationslider.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.durationslider.MouseClick += new System.Windows.Forms.MouseEventHandler(this.durationslider_MouseClick);
            // 
            // volumeslider
            // 
            this.volumeslider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.volumeslider.BarInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.volumeslider.BarPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.volumeslider.BarPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.volumeslider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.volumeslider.ElapsedInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.volumeslider.ElapsedPenColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.volumeslider.ElapsedPenColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.volumeslider.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.volumeslider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.volumeslider.LargeChange = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.volumeslider.Location = new System.Drawing.Point(1076, 17);
            this.volumeslider.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.volumeslider.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.volumeslider.Name = "volumeslider";
            this.volumeslider.Padding = 5;
            this.volumeslider.ScaleDivisions = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.volumeslider.ScaleSubDivisions = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.volumeslider.ShowDivisionsText = true;
            this.volumeslider.ShowSmallScale = false;
            this.volumeslider.Size = new System.Drawing.Size(150, 53);
            this.volumeslider.SmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.volumeslider.TabIndex = 18;
            this.volumeslider.Text = "colorSlider1";
            this.volumeslider.ThumbInnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.volumeslider.ThumbOuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.volumeslider.ThumbPenColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(227)))), ((int)(((byte)(109)))));
            this.volumeslider.ThumbRoundRectSize = new System.Drawing.Size(10, 10);
            this.volumeslider.ThumbSize = new System.Drawing.Size(12, 12);
            this.volumeslider.TickAdd = 0F;
            this.volumeslider.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.volumeslider.TickDivide = 0F;
            this.volumeslider.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.volumeslider.ValueChanged += new System.EventHandler(this.volumeslider_ValueChanged);
            // 
            // previousButton
            // 
            this.previousButton.Image = ((System.Drawing.Image)(resources.GetObject("previousButton.Image")));
            this.previousButton.Location = new System.Drawing.Point(574, 8);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(32, 32);
            this.previousButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previousButton.TabIndex = 16;
            this.previousButton.TabStop = false;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Image = ((System.Drawing.Image)(resources.GetObject("nextButton.Image")));
            this.nextButton.Location = new System.Drawing.Point(658, 8);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(32, 32);
            this.nextButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.nextButton.TabIndex = 13;
            this.nextButton.TabStop = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // leftpanel
            // 
            this.leftpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(8)))), ((int)(((byte)(8)))));
            this.leftpanel.Controls.Add(this.songList);
            this.leftpanel.Location = new System.Drawing.Point(0, 0);
            this.leftpanel.Name = "leftpanel";
            this.leftpanel.Size = new System.Drawing.Size(307, 597);
            this.leftpanel.TabIndex = 15;
            this.leftpanel.Visible = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "Musicplayer";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // albumArtNextPicturebox
            // 
            this.albumArtNextPicturebox.Location = new System.Drawing.Point(829, 177);
            this.albumArtNextPicturebox.Name = "albumArtNextPicturebox";
            this.albumArtNextPicturebox.Size = new System.Drawing.Size(200, 200);
            this.albumArtNextPicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.albumArtNextPicturebox.TabIndex = 16;
            this.albumArtNextPicturebox.TabStop = false;
            // 
            // albumArtFuturePicturebox
            // 
            this.albumArtFuturePicturebox.Location = new System.Drawing.Point(1076, 200);
            this.albumArtFuturePicturebox.Name = "albumArtFuturePicturebox";
            this.albumArtFuturePicturebox.Size = new System.Drawing.Size(150, 150);
            this.albumArtFuturePicturebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.albumArtFuturePicturebox.TabIndex = 17;
            this.albumArtFuturePicturebox.TabStop = false;
            // 
            // durationsliderProgression
            // 
            this.durationsliderProgression.Enabled = true;
            this.durationsliderProgression.Interval = 1000;
            this.durationsliderProgression.Tick += new System.EventHandler(this.durationsliderProgression_Tick);
            // 
            // Musicplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InfoText;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.albumArtFuturePicturebox);
            this.Controls.Add(this.albumArtNextPicturebox);
            this.Controls.Add(this.leftpanel);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.noAlbumPictureBox);
            this.Controls.Add(this.Artist);
            this.Controls.Add(this.albumArtPicturebox);
            this.Controls.Add(this.mPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Musicplayer";
            this.Text = "MusicPlayer";
            this.SizeChanged += new System.EventHandler(this.Musicplayer_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.playButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stopButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.folderButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.expandListButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noAlbumPictureBox)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previousButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nextButton)).EndInit();
            this.leftpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.albumArtNextPicturebox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.albumArtFuturePicturebox)).EndInit();
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
        private System.Windows.Forms.ListBox songList;
        private System.Windows.Forms.PictureBox expandListButton;
        private System.Windows.Forms.PictureBox noAlbumPictureBox;
        private System.Windows.Forms.Panel menu;
        private System.Windows.Forms.Panel leftpanel;
        private System.Windows.Forms.PictureBox previousButton;
        private System.Windows.Forms.PictureBox nextButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.PictureBox albumArtNextPicturebox;
        private System.Windows.Forms.PictureBox albumArtFuturePicturebox;
        private ColorSlider.ColorSlider volumeslider;
        private ColorSlider.ColorSlider durationslider;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer durationsliderProgression;
    }
}

