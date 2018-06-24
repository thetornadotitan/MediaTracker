using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker
{
    public static class ShowHelper
    {
        private static List<string> videoFiles = new List<string>()
        {
            ".webm", ".mkv", ".flv", ".vob", ".ogv", ".ogg", ".drc",
            ".gif", ".avi", ".mov", ".qt", ".wmv", ".mng", ".yuv",
            ".rm", ".rmvb", ".asf", ".mp4", ".m4p", ".m4v", ".mpg",
            ".mp2", ".mpeg", ".mpe", ".mpv", ".m2v", ".m4v", ".svi",
            ".3gp", ".3g2", ".mxf", ".roq", ".nsv", ".f4v", ".f4p",
            ".f4a", ".f4b"
        };

        public static bool IsShow(string file)
        {
            if (File.Exists(file))
            {
                return videoFiles.Contains(Path.GetExtension(file));
            }

            return false;
        }
    }
}
