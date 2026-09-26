using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WinterRose.WinterForgeSerializing;
using WinterRose.WinterForgeSerializing.Workers;

namespace WinterRose.Preferences.Storage.WinterForgeStorage;

internal class OSPlatformValueProvider : CustomValueProvider<OSPlatform>
{
    public override OSPlatform CreateObject(object value, WinterForgeVM executor)
    {
        if (value is string str)
        {
            return str.ToLowerInvariant() switch
            {
                "windows" => OSPlatform.Windows,
                "linux" => OSPlatform.Linux,
                "osx" => OSPlatform.OSX,
                _ => throw new InvalidOperationException($"Unknown OSPlatform string: {str}")
            };
        }
        throw new InvalidOperationException($"Cannot create OSPlatform from value of type {value.GetType()}");
    }

    public override object CreateString(OSPlatform obj, ObjectSerializer serializer) => obj.ToString().ToLowerInvariant();
}
