namespace Contacts.Plugins.DataStore.SQLLite
{
    public class Constants
    {
        public const string DatabaseFileName = "ContactsSQLite.db3";

        public static string DatabasePath => 
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

    }
}
