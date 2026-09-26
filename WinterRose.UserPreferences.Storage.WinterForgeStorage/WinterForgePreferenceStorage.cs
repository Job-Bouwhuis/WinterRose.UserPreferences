using WinterRose.Preferences.Storage;
using WinterRose.WinterForgeSerializing;

namespace WinterRose.Preferences.WinterForgeStorage;

public class WinterForgePreferenceStorage : IPreferencesStorage
{
    public UserPreferences Load(Stream source, CancellationToken cancellationToken = default)
    {
        object? res = WinterForge.DeserializeFromHumanReadableStream<UserPreferences>(source);
        if(res is not UserPreferences userprefs)
            throw new InvalidOperationException("Deserialized object is not of type UserPreferences.");

        return userprefs;
    }
    public void Save(Stream target, UserPreferences preferences, CancellationToken cancellationToken = default)
    {
        WinterForge.SerializeToStream(preferences, target, TargetFormat.FormattedHumanReadable);
    }
}
