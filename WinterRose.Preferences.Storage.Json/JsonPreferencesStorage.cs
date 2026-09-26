using System.Text.Json;

namespace WinterRose.Preferences.Storage.Json;


public class JsonPreferencesStorage : IPreferencesStorage
{
    private static readonly JsonSerializerOptions JSON_OPTIONS = new()
    {
        WriteIndented = true
    };

    public UserPreferences Load(
        Stream source,
        CancellationToken cancellationToken = default)
    {
        return JsonSerializer.Deserialize<UserPreferences>(
            source,
            JSON_OPTIONS) ?? throw new JsonException("Failed to deserialize user preferences.");
    }

    public void Save(
        Stream target,
        UserPreferences preferences,
        CancellationToken cancellationToken = default)
    {
        JsonSerializer.Serialize(
            target,
            preferences,
            JSON_OPTIONS);
    }
}
