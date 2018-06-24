using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MediaTracker
{
    class DirectoryControl : Panel
    {
        public string path = "";
        string name = "";

        public DirectoryControl(string folder, Form1 instance, bool regressive = false)
        {
            if (folder != string.Empty)
            {
                path = folder;
                name = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();
                DoubleClick += instance.DirectoryDoubleClick; //Go Directory diving on double click
            }
            else
            {
                name = "..";
            }

            Size = new Size(250, 200);
            BackgroundImage = (regressive) ? Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\BackFolder.png") : Image.FromFile(Directory.GetCurrentDirectory() + "\\resources\\Folder.png");
            BackgroundImageLayout = ImageLayout.Stretch;

            Label nameLabel = new Label
            {
                Text = name,
                Width = 250,
                Height = 160,
                Top = 40,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Black,
                Font = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold),
            };
            nameLabel.DoubleClick += (s, e) => { OnDoubleClick(null); }; //Call parent's DoubleClick method if the double click is registered on the label.
            Controls.Add(nameLabel);
        }
    }
}
