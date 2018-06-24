using System.Data;
using System.Data.SqlClient;

namespace MediaTracker
{
    public static class ConnectionHelper
    {
        private static SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\db.mdf;Integrated Security=True");
        private static string query;
        private static SqlCommand cmd;
        private static SqlDataReader rdr;

        private static class Columns
        {
            public static int ID = 0;
            public static int FilePath = 1;
            public static int Watched = 2;
        }

        public static bool ShowDoesExists(string file)
        {
            bool exists = false;

            query = "SELECT * FROM [dbo].[media] WHERE [FilePath] = @filePath";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@filePath", file);
            conn.Open();
            rdr = cmd.ExecuteReader();
            exists = rdr.HasRows;
            conn.Close();

            return exists;
        }

        public static bool ShowIsWatched(string file)
        {
            bool result = false;

            query = "SELECT * FROM [dbo].[media] WHERE [FilePath] = @filePath";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@filePath", file);
            conn.Open();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            result = rdr.GetBoolean(Columns.Watched);
            conn.Close();

            return result;
        }

        public static void SetWatched(string file, bool watched)
        {
            query = "UPDATE [dbo].[media] SET [Watched] = @watched WHERE [FilePath] = @filePath";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@filePath", file);
            cmd.Parameters.AddWithValue("@watched", watched);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void AddShow(string file)
        {
            query = "INSERT INTO [dbo].[media] ([FilePath], [Watched]) VALUES (@filePath, @watched)";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@filePath", file);
            cmd.Parameters.AddWithValue("@watched", false);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /*
        public static bool ShowExists(string show, string season, string name)
        {
            bool result = false;

            query = "SELECT * FROM [dbo].[media] WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@showName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@season", SqlDbType.NVarChar);
            cmd.Parameters.Add("@fileName", SqlDbType.NVarChar);
            cmd.Parameters["@showName"].Value = show;
            cmd.Parameters["@season"].Value = season;
            cmd.Parameters["@fileName"].Value = name;

            conn.Open();
            rdr = cmd.ExecuteReader();
            result = rdr.HasRows;
            conn.Close();

            return result;
        }

        public static bool ShowWatched(string show, string season, string name)
        {
            bool result = false;

            query = "SELECT * FROM [dbo].[media] WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@showName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@season", SqlDbType.NVarChar);
            cmd.Parameters.Add("@fileName", SqlDbType.NVarChar);
            cmd.Parameters["@showName"].Value = show;
            cmd.Parameters["@season"].Value = season;
            cmd.Parameters["@fileName"].Value = name;

            conn.Open();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            result = rdr.GetBoolean(Columns.Watched);
            conn.Close();

            return result;
        }

        public static string GetPic(string show, string season, string name)
        {
            string result = "";

            query = "SELECT * FROM [dbo].[media] WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@showName", SqlDbType.NVarChar);
            cmd.Parameters.Add("@season", SqlDbType.NVarChar);
            cmd.Parameters.Add("@fileName", SqlDbType.NVarChar);
            cmd.Parameters["@showName"].Value = show;
            cmd.Parameters["@season"].Value = season;
            cmd.Parameters["@fileName"].Value = name;

            conn.Open();
            rdr = cmd.ExecuteReader();
            rdr.Read();
            result = rdr.GetString(Columns.pic);
            conn.Close();

            return result;
        }

        public static void AddShow(string show, string season, string name)
        {
            query = "INSERT INTO [dbo].[media] ([Show Name], [Season], [File Name], [Watched], [Pic]) VALUES (@showName, @season, @fileName, @watched, @pic)";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@showName", show);
            cmd.Parameters.AddWithValue("@season", season);
            cmd.Parameters.AddWithValue("@fileName", name);
            cmd.Parameters.AddWithValue("@watched", false);
            cmd.Parameters.AddWithValue("@pic", "");
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void SetPicPath(string show, string season, string name, string pic)
        {
            query = "UPDATE [dbo].[media] SET [Pic] = @pic WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@showName", show);
            cmd.Parameters.AddWithValue("@season", season);
            cmd.Parameters.AddWithValue("@fileName", name);
            cmd.Parameters.AddWithValue("@pic", pic);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void SetWatched(string show, string season, string name, bool watched)
        {
            query = "UPDATE [dbo].[media] SET [Watched] = @watched WHERE [Show Name] = @showName AND [Season] = @season AND [File Name] = @fileName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@showName", show);
            cmd.Parameters.AddWithValue("@season", season);
            cmd.Parameters.AddWithValue("@fileName", name);
            cmd.Parameters.AddWithValue("@watched", watched);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void SetSeasonWatched(string show, string season, bool watched)
        {
            query = "UPDATE [dbo].[media] SET [Watched] = @watched WHERE [Show Name] = @showName AND [Season] = @season";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@showName", show);
            cmd.Parameters.AddWithValue("@season", season);
            cmd.Parameters.AddWithValue("@watched", watched);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void SetShowWatched(string show, bool watched)
        {
            query = "UPDATE [dbo].[media] SET [Watched] = @watched WHERE [Show Name] = @showName";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@showName", show);
            cmd.Parameters.AddWithValue("@watched", watched);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        */
    }
}
