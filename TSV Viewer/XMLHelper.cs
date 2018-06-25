using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MediaTracker
{
    public static class XMLHelper
    {

        public static bool ShowDoesExists(string file)
        {
            XDocument doc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Data.xml");
            XElement e = doc.Root.Elements().Where(x => { return x.Attribute("file").Value == file; }).LastOrDefault();
            
            return e != null;
        }

        public static bool ShowIsWatched(string file)
        {
            XDocument doc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Data.xml");
            string result = doc.Root.Elements().Where(x => { return x.Attribute("file").Value == file; }).LastOrDefault().Attribute("watched").Value;
            
            return bool.Parse(result);
        }

        public static void SetWatched(string file, bool watched)
        {
            XDocument doc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Data.xml");
            doc.Root.Elements().Where(x => { return x.Attribute("file").Value == file; }).LastOrDefault().Attribute("watched").Value = watched.ToString();
            doc.Save(Directory.GetCurrentDirectory() + "\\Data.xml");
        }

        public static void AddShow(string file)
        {
            XDocument doc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Data.xml");
            doc.Root.Add(
                new XElement("Media", 
                    new XAttribute("file", file),
                    new XAttribute("watched", "False")
                )
            );
            doc.Save(Directory.GetCurrentDirectory() + "\\Data.xml");
        }
    }
}
