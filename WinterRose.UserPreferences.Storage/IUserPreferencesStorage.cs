namespace WinterRose.Preferences.Storage;

public interface IPreferencesStorage
{
    UserPreferences Load(Stream source, CancellationToken cancellationToken = default);
    void Save(Stream target, UserPreferences preferences, CancellationToken cancellationToken = default);
}
