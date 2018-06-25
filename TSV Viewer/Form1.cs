using System;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;

namespace MediaTracker
{
    public partial class Form1 : Form
    {
        string workingDirectory;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\resources\\Thumbnails"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\resources\\Thumbnails");

            DisplayRootDirectory(null, null);
            workingDirectory = "";
        }

        private void DisplayRootDirectory(object sender, EventArgs e)
        {
            EpisodeFlow.Controls.Clear();
            foreach (string s in Directory.GetLogicalDrives())
            {
                EpisodeFlow.Controls.Add(new DirectoryControl(s, this));
            }
        }

        public void DirectoryDoubleClick(object sender, EventArgs e)
        {
            DirectoryControl c = sender as DirectoryControl;
            UpdateFlowControl(c.path);
            workingDirectory = c.path;
        }

        private void UpdateFlowControl(string path)
        {
            EpisodeFlow.Controls.Clear();
            CreateRegressControls(path);

            string[] directories;

            try
            {
                directories = Directory.GetDirectories(path);
            }
            catch
            {
                DisplayRootDirectory(null, null);
                return;
            }

            foreach(string d in directories)
            {
                if (Directory.Exists(d))
                {
                    EpisodeFlow.Controls.Add(new DirectoryControl(d, this));
                }
            }

            string[] files = Directory.GetFiles(path);

            foreach(string f in files)
            {
                if (ShowHelper.IsShow(f) && File.Exists(f))
                {
                    EpisodeFlow.Controls.Add(new EpisodeControl(f));
                }
            }

        }

        private void CreateRegressControls(string path)
        {
            string parent = "";

            if (Directory.GetParent(path) != null)
                parent = Directory.GetParent(path).ToString();

            if (parent != String.Empty)
            {
                //Create .. directory
                EpisodeFlow.Controls.Add(new DirectoryControl(parent, this, true));
            }
            else
            {
                DirectoryControl c = new DirectoryControl("", this, true);
                c.DoubleClick += DisplayRootDirectory;
                EpisodeFlow.Controls.Add(c);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(workingDirectory);

            foreach (string f in files)
            {
                if (ShowHelper.IsShow(f) && File.Exists(f))
                {
                    string filePathName = f.Replace('\\', '_');
                    filePathName = filePathName.Replace(':', '_');
                    filePathName = filePathName.Remove(0, 3);
                    XMLHelper.SetWatched(filePathName, true);
                    foreach (Panel ep in EpisodeFlow.Controls)
                    {
                        try
                        {
                            EpisodeControl epc = ep as EpisodeControl;
                            epc.RefreshEpisodeStatus(filePathName, true);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            EpisodeFlow.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(workingDirectory);

            foreach (string f in files)
            {
                if (ShowHelper.IsShow(f) && File.Exists(f))
                {
                    string filePathName = f.Replace('\\', '_');
                    filePathName = filePathName.Replace(':', '_');
                    filePathName = filePathName.Remove(0, 3);
                    XMLHelper.SetWatched(filePathName, false);
                    foreach (Panel ep in EpisodeFlow.Controls)
                    {
                        try
                        {
                            EpisodeControl epc = ep as EpisodeControl;
                            epc.RefreshEpisodeStatus(filePathName, false);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }
            }
            EpisodeFlow.Refresh();
        }
    }
}
