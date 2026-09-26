using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WinterRose.Preferences;

public interface IPreferenceOption
{
    string Name { get; }
    string Description { get; }
    string Category { get; }

    Type ValueType { get; }
    object? Value { get; set; }

    object? DefaultValue { get; }

    bool RequiresRestart { get; }

    ControlHint? Hint { get; }

    IReadOnlyList<OSPlatform>? AllowedOs { get; }

    bool IsVisibleOnCurrentOs { get; }

    void ResetToDefault();
}