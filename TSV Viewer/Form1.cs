using System;
using System.Windows.Forms;
using System.IO;

namespace MediaTracker
{
    public partial class Form1 : Form
    {
        private string fileLocation = "";

        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void ScanBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            FileLocation.Text = fbd.SelectedPath;
            SeasonBox.Items.Clear();
            SeasonBox.ResetText();
            SeriesBox.Items.Clear();
            SeriesBox.ResetText();
            ToggleWatchedSeason.Enabled = false;
            ToggleWatchedShow.Enabled = false;
            ToggleWatchedLabel.Enabled = false;
            EpisodeFlow.Controls.Clear();
            fileLocation = FileLocation.Text;
            if (Directory.Exists(fileLocation))
            {
                string[] shows = Directory.GetDirectories(fileLocation);
                foreach(string show in shows)
                {
                    string[] seasons = Directory.GetDirectories(show);
                    foreach (string season in seasons)
                    {
                        string[] files = Directory.GetFiles(season);
                        foreach (string file in files)
                        {
                            string[] showInfo = ShowHelper.GetShowInfo(file);
                            ConnectionHelper.AddShow(showInfo[0], showInfo[1], showInfo[2]);
                        }
                    }
                    SeriesBox.Items.Add(show);
                }
            }
        }

        private void SeriesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeasonBox.Items.Clear();
            SeasonBox.ResetText();
            ToggleWatchedSeason.Enabled = false;
            ToggleWatchedShow.Enabled = false;
            ToggleWatchedLabel.Enabled = false;
            EpisodeFlow.Controls.Clear();
            fileLocation = SeriesBox.SelectedItem.ToString();
            if (Directory.Exists(fileLocation))
            {
                string[] directories = Directory.GetDirectories(fileLocation);
                foreach (string d in directories)
                {
                    SeasonBox.Items.Add(d); 
                }
            }
        }

        private void SeasonBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EpisodeFlow.Controls.Clear();
            fileLocation = SeasonBox.SelectedItem.ToString();
            ToggleWatchedSeason.Enabled = true;
            ToggleWatchedShow.Enabled = true;
            ToggleWatchedLabel.Enabled = true;
            if (Directory.Exists(fileLocation))
            {
                string[] files = Directory.GetFiles(fileLocation);
                foreach (string f in files)
                {
                    EpisodeFlow.Controls.Add(new EpisodeControl(f));
                }
            }
            EpisodeFlow.Refresh();
        }

        private void ToggleWatchedSeason_Click(object sender, EventArgs e)
        {
            fileLocation = SeasonBox.SelectedItem.ToString();
            if (Directory.Exists(fileLocation))
            {
                string[] files = Directory.GetFiles(fileLocation);
                if (files.Length >= 1)
                {
                    string file = files[0];
                    string[] showInfo = ShowHelper.GetShowInfo(file);
                    bool watched = !ConnectionHelper.ShowWatched(showInfo[0], showInfo[1], showInfo[2]);
                    ConnectionHelper.SetSeasonWatched(showInfo[0], showInfo[1], watched);
                    foreach(EpisodeControl ec in EpisodeFlow.Controls)
                    {
                        ec.SetWatchedIcon(watched);
                    }
                    EpisodeFlow.Refresh();
                }
            }
        }

        private void ToggleWatchedShow_Click(object sender, EventArgs e)
        {
            fileLocation = SeasonBox.SelectedItem.ToString();
            if (Directory.Exists(fileLocation))
            {
                string[] files = Directory.GetFiles(fileLocation);
                if (files.Length >= 1)
                {
                    string file = files[0];
                    string[] showInfo = ShowHelper.GetShowInfo(file);
                    bool watched = !ConnectionHelper.ShowWatched(showInfo[0], showInfo[1], showInfo[2]);
                    ConnectionHelper.SetShowWatched(showInfo[0], watched);
                    foreach (EpisodeControl ec in EpisodeFlow.Controls)
                    {
                        ec.SetWatchedIcon(watched);
                    }
                    EpisodeFlow.Refresh();
                }
            }
        }
    }
}
