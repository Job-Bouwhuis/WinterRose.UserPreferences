using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WinterRose.Preferences;

public enum MissingPreferenceMode
{
    ThrowException,
    AddNew
}

/// <summary>
/// Represents all the user preferences in the application. 
/// This class is responsible for managing the collection of preference options, 
/// allowing registration of new options, and providing methods to retrieve and modify preference values.
/// </summary>
public class UserPreferences
{
    public MissingPreferenceMode MissingPreferenceMode { get; set; } = MissingPreferenceMode.ThrowException;

    [WFInclude]
    private List<IPreferenceOption> options = new();

    /// <summary>
    /// Gets a read-only list of all registered preference options.
    /// </summary>
    public IReadOnlyList<IPreferenceOption> Options => options;

    /// <summary>
    /// Registers a new preference option to the collection of user preferences.
    /// </summary>
    /// <param name="option">The preference option to register.</param>
    public void Register(IPreferenceOption option)
    {
        options.Add(option);
    }

    /// <summary>
    /// Retrieves a distinct and ordered list of all categories from the registered preference options.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetCategories()
    {
        return options
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x);
    }
    
    /// <summary>
    /// Retrieves a list of all preference options in the specified category.
    /// </summary>
    /// <param name="category">The category of the preference options to retrieve.</param>
    /// <returns></returns>
    public IEnumerable<IPreferenceOption> GetOptions(string category)
    {
        return options
            .Where(x => x.Category == category)
            .Where(x => x.IsVisibleOnCurrentOs);
    }

    /// <summary>
    /// Retrieves the value of a preference option by its name and casts it to the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Get<T>(string name)
    {
        var option = options.FirstOrDefault(x => x.Name == name);

        if (option is null)
        {
            if (MissingPreferenceMode == MissingPreferenceMode.ThrowException)
                throw new KeyNotFoundException(
                    $"Preference option '{name}' was not found.");

            throw new InvalidOperationException(
                $"Preference option '{name}' cannot be created by Get<T>. " +
                "Use Set<T> to add a missing option.");
        }

        return (T)option.Value!;
    }

    /// <summary>
    /// Attempts to retrieve the value of a preference option by its name and casts it to the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool TryGet<T>(string name, out T? value)
    {
        var option = options.FirstOrDefault(x => x.Name == name);

        if (option is null)
        {
            value = default;
            return false;
        }

        value = (T?)option.Value;
        return true;
    }

    /// <summary>
    /// Sets the value of a preference option by its name. The value is cast to the specified type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void Set<T>(string name, T value)
    {
        var option = options.FirstOrDefault(x => x.Name == name);

        if (option is null)
        {
            if (MissingPreferenceMode == MissingPreferenceMode.ThrowException)
                throw new KeyNotFoundException(
                    $"Preference option '{name}' was not found.");

            Register(new PreferenceOption<T>(
                name,
                string.Empty,
                string.Empty,
                value));

            return;
        }

        option.Value = value!;
    }
}