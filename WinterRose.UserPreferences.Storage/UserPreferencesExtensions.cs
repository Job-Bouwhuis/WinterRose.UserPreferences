using System;
using System.Collections.Generic;
using System.Text;

namespace WinterRose.Preferences.Storage;

public static class UserPreferencesExtensions
{
    extension(UserPreferences preferences)
    {
        public void SaveToFile(string filePath, IPreferencesStorage storage)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            storage.Save(fileStream, preferences);
        }

        public static UserPreferences LoadFromFile(string filePath, IPreferencesStorage storage)
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Read, FileShare.Read);
            return storage.Load(fileStream);
        }

        public void SaveToFile<T>(string filePath) where T : IPreferencesStorage, new()
        {
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            new T().Save(fileStream, preferences);
        }

        public static UserPreferences LoadFromFile<T>(string filePath) where T : IPreferencesStorage, new()
        {
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return new T().Load(fileStream);
        }
    }
}

