using NReco.VideoConverter;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MediaTracker
{
    class EpisodeControl : Panel
    {
        private PictureBox watchedPic;
        string[] showInfo;

        public EpisodeControl(string file)
        {
            showInfo = ShowHelper.GetShowInfo(file);
            bool watched = false;

            BackColor = Color.DarkGray;
            BackgroundImageLayout = ImageLayout.Stretch;

            if (ConnectionHelper.ShowExists(showInfo[0], showInfo[1], showInfo[2]))
            {
                watched = ConnectionHelper.ShowWatched(showInfo[0], showInfo[1], showInfo[2]);
                string img = ConnectionHelper.GetPic(showInfo[0], showInfo[1], showInfo[2]);

                if(img == string.Empty)
                {
                    BackgroundImage = Image.FromFile(CreateThumbnail(file));
                }
                else
                {
                    BackgroundImage = Image.FromFile(img);
                }
            }

            Size = new Size(200, 200);

            watchedPic = new PictureBox();
            watchedPic.Left = 4;
            watchedPic.Top = 4;
            watchedPic.Size = new Size(25, 25);
            watchedPic.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", (watched) ? "Watched.png" : "Not Watched.png"));
            watchedPic.BackgroundImageLayout = ImageLayout.Stretch;
            watchedPic.BackColor = Color.Transparent;
            watchedPic.Click += (o, i) =>
            {
                watched = !ConnectionHelper.ShowWatched(showInfo[0], showInfo[1], showInfo[2]);
                ConnectionHelper.SetWatched(showInfo[0], showInfo[1], showInfo[2], watched);
                SetWatchedIcon(watched);
                Refresh();
            };
            Controls.Add(watchedPic);

            PictureBox play = new PictureBox();
            play.Text = "Play";
            play.Size = new Size(35, 25);
            play.Left = 35;
            play.Top = 4;
            play.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", "Play.png"));
            play.BackgroundImageLayout = ImageLayout.Stretch;
            play.BackColor = Color.Transparent;
            play.Click += (o, i) =>
            {
                ConnectionHelper.SetWatched(showInfo[0], showInfo[1], showInfo[2], true);
                watchedPic.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", (ConnectionHelper.ShowWatched(showInfo[0], showInfo[1], showInfo[2]) ? "Watched.png" : "Not Watched.png")));
                Refresh();
                System.Diagnostics.Process.Start(file);
            };
            Controls.Add(play);

            Label l4 = new Label();
            l4.ForeColor = Color.White;
            l4.BackColor = Color.FromArgb(200, 0, 0, 0);
            l4.Size = new Size(200, 200);
            l4.Font = new Font("Arial", 12);
            string[] s = file.Split('\\');
            l4.Text = s[s.Length - 1];
            l4.TextAlign = ContentAlignment.MiddleCenter;
            l4.Visible = false;
            l4.MouseLeave += (o, i) => { l4.Visible = false; };
            Controls.Add(l4);
            
            MouseEnter += (o, i) => { l4.Visible = true; };
        }

        private string CreateThumbnail(string file)
        {
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "resources", "Thumbnails")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "resources", "Thumbnails"));
            }
            string path = Path.Combine(Directory.GetCurrentDirectory(), "resources", "Thumbnails", showInfo[2] + ".jpg");
            ConnectionHelper.SetPicPath(showInfo[0], showInfo[1], showInfo[2], path);
            FFMpegConverter ffMpeg = new FFMpegConverter();
            Stream f = new MemoryStream();

            try
            {
                ffMpeg.GetVideoThumbnail(file, f, 60);
                Image i = Image.FromStream(f);
                BackgroundImage = i;
                i.Save(path, ImageFormat.Jpeg);
            }
            catch { }

            return path;
        }

        public void SetWatchedIcon(bool watched)
        {
            watchedPic.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", (watched) ? "Watched.png" : "Not Watched.png"));
        }
    }
}
