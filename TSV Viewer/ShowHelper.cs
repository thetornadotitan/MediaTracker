using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTracker
{
    public static class ShowHelper
    {
        public static string[] GetShowInfo(string file)
        {
            string[] splitPath = file.Split('\\');
            string[] result = { splitPath[2], splitPath[3], splitPath[4] };
            return result;
        }
    }
}
