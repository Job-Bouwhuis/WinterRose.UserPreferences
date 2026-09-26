using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace WinterRose.Preferences;

public class PreferenceOption<T> : IPreferenceOption
{
    [WFInclude]
    public string Name { get; private set; }
    [WFInclude]
    public string Description { get; private set; }
    [WFInclude]
    public string Category { get; private set; }

    [WFInclude]
    public T Value { get; set; }
    [WFInclude]
    public T DefaultValue { get; private set; }

    [WFInclude]
    public bool RequiresRestart { get; private set; }

    [WFInclude]
    public ControlHint? Hint { get; private set; }

    [WFInclude]
    public IReadOnlyList<OSPlatform>? AllowedOs { get; private set; }

    [WFInclude]
    public T? MinValue { get; private set; }
    [WFInclude]
    public T? MaxValue { get; private set; }

    public bool IsFlagsEnum => typeof(T).IsEnum && typeof(T).IsDefined(typeof(FlagsAttribute), false);

    public Type ValueType => typeof(T);

    object? IPreferenceOption.Value
    {
        get => Value;
        set => Value = (T)value!;
    }

    object? IPreferenceOption.DefaultValue => DefaultValue;

    public bool IsVisibleOnCurrentOs
    {
        get
        {
            if (AllowedOs == null || AllowedOs.Count == 0)
                return true;

            if (OperatingSystem.IsWindows() && AllowedOs.Contains(OSPlatform.Windows))
                return true;

            if (OperatingSystem.IsLinux() && AllowedOs.Contains(OSPlatform.Linux))
                return true;

            if (OperatingSystem.IsMacOS() && AllowedOs.Contains(OSPlatform.OSX))
                return true;

            return false;
        }
    }

    public PreferenceOption(
        string name,
        string description,
        string category,
        T defaultValue,
        bool requiresRestart = false,
        ControlHint? hint = null,
        IReadOnlyList<OSPlatform>? allowedOs = null,
        T? minValue = default,
        T? maxValue = default)
    {
        Name = name;
        Description = description;
        Category = category;

        Value = defaultValue;
        DefaultValue = defaultValue;

        RequiresRestart = requiresRestart;
        Hint = hint;
        AllowedOs = allowedOs;

        MinValue = minValue;
        MaxValue = maxValue;
    }

    private PreferenceOption() { } // for serialization

    public void ResetToDefault()
    {
        Value = DefaultValue;
    }
}