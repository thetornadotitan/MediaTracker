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
using System.Data.SqlClient;

namespace MediaTracker
{
    public partial class Form1 : Form
    {
        private string fileLocation = "";
        private SqlConnection conn;
        private string query;
        private SqlCommand cmd;
        private SqlDataReader rdr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\db.mdf;Integrated Security=True");
            conn.Open();
            conn.Close();
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
                    EpisodeFlow.Controls.Add(GenerateEpisodeControl(f));
                }
            }
            EpisodeFlow.Refresh();
        }

        private Control GenerateEpisodeControl(string file)
        {
            string[] splitPath = file.Split('\\');
            string show = splitPath[2];
            string season = splitPath[3];
            string episodeName = splitPath[4];

            Color watchedColor = Color.Red;
            string watchedSymbol = "X";

            query = "SELECT * FROM [dbo].[media] WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@showName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@season", SqlDbType.NVarChar);
            cmd.Parameters.Add("@fileName", SqlDbType.NVarChar);
            cmd.Parameters["@showName"].Value = show;
            cmd.Parameters["@season"].Value = season;
            cmd.Parameters["@fileName"].Value = episodeName;

            conn.Open();
            rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                bool watched = rdr.GetBoolean(4);

                watchedColor = (watched == true) ? Color.Green : Color.Red;
                watchedSymbol = (watched == true) ? "O" : "X";
                conn.Close();
            }
            else
            {
                conn.Close();
                query = "INSERT INTO [dbo].[media] ([Show Name], [Season], [File Name], [Watched]) VALUES (@showName, @season, @fileName, @watched)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@showName", show);
                cmd.Parameters.AddWithValue("@season", season);
                cmd.Parameters.AddWithValue("@fileName", episodeName);
                cmd.Parameters.AddWithValue("@watched", false);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            

            GroupBox g = new GroupBox();
            g.Size = new Size(200, 200);

            Label l1 = new Label();
            l1.Left = 4;
            l1.Top = 4;
            l1.Size = new Size(20, 20);
            l1.ForeColor = watchedColor;
            l1.BackColor = Color.Black;
            l1.Font = new Font("Arial", 12);
            l1.TextAlign = ContentAlignment.MiddleCenter;
            l1.Text = watchedSymbol;
            l1.Click += (o, i) =>
            {
                query = "SELECT [Watched] FROM [dbo].[media] WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@showName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@season", SqlDbType.NVarChar);
                cmd.Parameters.Add("@fileName", SqlDbType.NVarChar);
                cmd.Parameters["@showName"].Value = show;
                cmd.Parameters["@season"].Value = season;
                cmd.Parameters["@fileName"].Value = episodeName;

                conn.Open();
                bool watched = false;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    watched = rdr.GetBoolean(0);
                }
                conn.Close();

                watched = !watched;
                watchedColor = (watched == true) ? Color.Green : Color.Red;
                watchedSymbol = (watched == true) ? "O" : "X";
                l1.ForeColor = watchedColor;
                l1.Text = watchedSymbol;

                query = "UPDATE [dbo].[media] SET [Watched] = @watched WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@showName", show);
                cmd.Parameters.AddWithValue("@season", season);
                cmd.Parameters.AddWithValue("@fileName", episodeName);
                cmd.Parameters.AddWithValue("@watched", watched);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                g.Refresh();
            };
            g.Controls.Add(l1);

            Button play = new Button();
            play.Text = "Play";
            play.FlatStyle = FlatStyle.Popup;
            play.Left = 25;
            play.Top = 2;
            play.Click += (o, i) =>
            {
                query = "UPDATE [dbo].[media] SET [Watched] = 1 WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@showName", show);
                cmd.Parameters.AddWithValue("@season", season);
                cmd.Parameters.AddWithValue("@fileName", episodeName);
                cmd.Parameters.AddWithValue("@watched", false);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                l1.Text = "O";
                l1.ForeColor = Color.Green;
                g.Refresh();
                System.Diagnostics.Process.Start(file);
            };
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
