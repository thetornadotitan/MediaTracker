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
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            FileLocation.Text = fbd.SelectedPath;
            ScanBtn_Click(null, null);
        }

        private void ScanBtn_Click(object sender, EventArgs e)
        {
            SeasonBox.Items.Clear();
            SeasonBox.ResetText();
            SeriesBox.Items.Clear();
            SeriesBox.ResetText();
            EpisodeFlow.Controls.Clear();
            fileLocation = FileLocation.Text;
            if (Directory.Exists(fileLocation))
            {
                string[] directories = Directory.GetDirectories(fileLocation);
                foreach(string d in directories)
                {
                    SeriesBox.Items.Add(d);
                }
            }
        }

        private void SeriesBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeasonBox.Items.Clear();
            SeasonBox.ResetText();
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
            Console.WriteLine(fileLocation);
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
    }
}
