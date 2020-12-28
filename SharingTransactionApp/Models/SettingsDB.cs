using System;

namespace SharingTransactionApp
{
    public class SettingsDB:ISettingsDB
    {
        public string StorageCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISettingsDB
    {
        public string StorageCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
