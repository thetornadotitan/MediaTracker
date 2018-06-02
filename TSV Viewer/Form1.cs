using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Console.WriteLine(f);
                    EpisodeFlow.Controls.Add(GenerateEpisodeControl(f));
                    
                }
            }
            EpisodeFlow.Refresh();
        }

        private void ToggleVisibility(object sender, EventArgs e)
        {
            Console.WriteLine("Doing a thing");
        }

        private Control GenerateEpisodeControl(string file)
        {
            GroupBox g = new GroupBox();
            g.Size = new Size(200, 200);

            Label l1 = new Label();
            l1.Left = 4;
            l1.Top = 4;
            l1.Size = new Size(20, 20);
            l1.ForeColor = Color.Red;
            l1.BackColor = Color.Black;
            l1.Font = new Font("Arial", 12);
            l1.TextAlign = ContentAlignment.MiddleCenter;
            l1.Text = "O";
            g.Controls.Add(l1);

            Label l2 = new Label();
            l2.Left = 27;
            l2.Top = 4;
            l2.Size = new Size(26, 20);
            l2.ForeColor = Color.Red;
            l2.BackColor = Color.Black;
            l2.Font = new Font("Arial", 12);
            l2.TextAlign = ContentAlignment.MiddleCenter;
            l2.Text = "99";
            g.Controls.Add(l2);

            Label l3 = new Label();
            l3.Left = 56;
            l3.Top = 4;
            l3.Size = new Size(26, 20);
            l3.ForeColor = Color.Red;
            l3.BackColor = Color.Black;
            l3.Font = new Font("Arial", 12);
            l3.TextAlign = ContentAlignment.MiddleCenter;
            l3.Text = "99";
            g.Controls.Add(l3);

            Button play = new Button();
            play.Text = "Play";
            play.FlatStyle = FlatStyle.Popup;
            play.Left = 85;
            play.Top = 2;
            play.Click += (o, i) => { System.Diagnostics.Process.Start(file); };
            g.Controls.Add(play);

            Label l4 = new Label();
            l4.ForeColor = Color.White;
            l4.BackColor = Color.FromArgb(200,0,0,0);
            l4.Size = new Size(200, 200);
            l4.Font = new Font("Arial", 12);
            string[] s = file.Split('\\');
            l4.Text = s[s.Length-1];
            l4.TextAlign = ContentAlignment.MiddleCenter;
            l4.Visible = false;
            g.Controls.Add(l4);

            PictureBox p = new PictureBox();
            p.BackColor = Color.DarkGray;
            p.Size = new Size(200, 200);
            p.MouseEnter += (o, i) => { l4.Visible = true; };
            p.MouseLeave += (o, i) => { l4.Visible = false; };
            g.Controls.Add(p);

            return g;
        }
    }
}
