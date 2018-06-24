using NReco.VideoConverter;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MediaTracker
{
    class EpisodeControl : Panel
    {
        public string path = "";
        string name = "";
        string filePathName = "";
        Color myTransperancyColor = Color.FromArgb(200, 0, 0, 0);
        Color myTransperancyColor2 = Color.FromArgb(135, 0, 0, 0);
        PictureBox watchedStatusIcon;

        public void RefreshEpisodeStatus(string file, bool watched)
        {
            watchedStatusIcon.BackgroundImage = (watched) ? Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Watched.png") : Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Not Watched.png");
        }

        public void RefreshEpisodeStatus(string file)
        {
            bool watched = ConnectionHelper.ShowIsWatched(file);
            watchedStatusIcon.BackgroundImage = (watched) ? Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Watched.png") : Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Not Watched.png");
        }

        public EpisodeControl(string file)
        {
            path = file;
            filePathName = file.Replace('\\', '_');
            filePathName = filePathName.Replace(':', '_');
            filePathName = filePathName.Remove(0, 3);
            name = file.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();
            Size = new Size(250, 200);

            if(!File.Exists(Directory.GetCurrentDirectory() + "\\resources\\Thumbnails\\" + filePathName + ".jpeg"))
            {
                try
                {
                    Console.WriteLine(filePathName);
                    new FFMpegConverter().GetVideoThumbnail(file, Directory.GetCurrentDirectory() + "\\resources\\Thumbnails\\" + filePathName + ".jpeg", 60);
                }catch
                {

                }
            }

            try
            {
                BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Thumbnails\\" + filePathName + ".jpeg");
            }
            catch
            {
                BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\DefaultFile.png");
            }

            BackgroundImageLayout = ImageLayout.Stretch;

            if (!ConnectionHelper.ShowDoesExists(file))
                ConnectionHelper.AddShow(file);

            watchedStatusIcon = new PictureBox
            {
                BackgroundImage = (ConnectionHelper.ShowIsWatched(file)) ? Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Watched.png") : Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Not Watched.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                Size = new Size(30, 20),
                Top = 15,
                Left = 30,
            };
            watchedStatusIcon.Click += (o, s) => {
                bool watched = ConnectionHelper.ShowIsWatched(file);
                watched = !watched;
                ConnectionHelper.SetWatched(file, watched);
                watchedStatusIcon.BackgroundImage = (watched) ? Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Watched.png") : Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Not Watched.png");
            };
            Controls.Add(watchedStatusIcon);

            PictureBox PlayButton = new PictureBox
            {
                BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Play.png"),
                BackgroundImageLayout = ImageLayout.Stretch,
                BackColor = Color.Transparent,
                Size = new Size(30, 20),
                Top = 14,
                Left = 70,
            };
            PlayButton.Click += (o, s) => {
                OnDoubleClick(null);
            };
            Controls.Add(PlayButton);

            Label FileName = new Label
            {
                Size = new Size(250, 200),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = name,
                Font = new Font(FontFamily.GenericMonospace, 14),
                ForeColor = Color.White,
                BackColor = myTransperancyColor,
                Visible = false,
            };
            Controls.Add(FileName);

            MouseEnter += (o, s) => { FileName.Visible = true; PlayButton.BackColor = myTransperancyColor2; watchedStatusIcon.BackColor = myTransperancyColor2; };

            DoubleClick += (o, s) => {
                watchedStatusIcon.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Watched.png");
                ConnectionHelper.SetWatched(file, true);
                System.Diagnostics.Process.Start(file);
            };

            FileName.MouseLeave += (o, s) => { FileName.Visible = false; PlayButton.BackColor = Color.Transparent; watchedStatusIcon.BackColor = Color.Transparent; };
            FileName.DoubleClick += (o, s) => { OnDoubleClick(null); };
        }
    }
}
