using WinterRose.Preferences;
using WinterRose.Preferences.Storage;
using WinterRose.Preferences.WinterForgeStorage;

internal class Program
{
    private static void Main(string[] args)
    {
        UserPreferences prefs = new();
        prefs.MissingPreferenceMode = MissingPreferenceMode.AddNew;
        prefs.Register(new PreferenceOption<int>("Option1", "opt1", "Category1", 42));
        prefs.Register(new PreferenceOption<int>("Option2", "opt2", "Category1", 42));
        prefs.Register(new PreferenceOption<int>("Option3", "opt3", "Category2", 42));
        prefs.Register(new PreferenceOption<int>("Option4", "opt4", "Category2", 42));

        prefs.SaveToFile<WinterForgePreferenceStorage>("prefs.wf");

        UserPreferences loadedPrefs = UserPreferences.LoadFromFile<WinterForgePreferenceStorage>("prefs.wf");
    }
}