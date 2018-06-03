using NReco.VideoConverter;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace MediaTracker
{
    class EpisodeControl : Panel
    {
        public EpisodeControl(string file)
        {
            string[] splitPath = file.Split('\\');
            string show = splitPath[2];
            string season = splitPath[3];
            string episodeName = splitPath[4];
            bool watched = false;

            BackColor = Color.DarkGray;
            BackgroundImageLayout = ImageLayout.Stretch;

            if (ConnectionHelper.ShowExists(show, season, episodeName))
            {
                watched = ConnectionHelper.ShowWatched(show, season, episodeName);
                try
                {
                    BackgroundImage = Image.FromFile(ConnectionHelper.GetPic(show, season, episodeName));
                }
                catch { }
            }
            else
            {
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "resources", "Thumbnails"))){
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "resources", "Thumbnails"));
                }
                string path = Path.Combine(Directory.GetCurrentDirectory(), "resources", "Thumbnails", episodeName + ".jpg");
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

                ConnectionHelper.AddShow(show, season, episodeName, path);
            }

            Size = new Size(200, 200);

            PictureBox l1 = new PictureBox();
            l1.Left = 4;
            l1.Top = 4;
            l1.Size = new Size(25, 25);
            l1.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", (watched) ? "Watched.png" : "Not Watched.png"));
            l1.BackgroundImageLayout = ImageLayout.Stretch;
            l1.BackColor = Color.Transparent;
            l1.Click += (o, i) =>
            {
                watched = !ConnectionHelper.ShowWatched(show, season, episodeName);
                ConnectionHelper.SetWatched(show, season, episodeName, watched);
                l1.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", (watched) ? "Watched.png" : "Not Watched.png"));
                Refresh();
            };
            Controls.Add(l1);

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
                ConnectionHelper.SetWatched(show, season, episodeName, true);
                l1.BackgroundImage = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "resources", (ConnectionHelper.ShowWatched(show, season, episodeName) ? "Watched.png" : "Not Watched.png")));
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
    }
}
