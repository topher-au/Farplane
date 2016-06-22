//css_reference "..\bin\Debug\farplane.exe";
using System;
using System.Linq;
using System.Windows;

using Farplane;
using Farplane.Common;
using Farplane.FarplaneMod;
using Farplane.FFX;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using Farplane.Memory;

public class FFXBreakDamageLimit : IFarplaneMod
{
    private bool _modActive = false;
    private static int _offsetDamageLimit = 0x38ED3D;
    public void Configure(object parentWindow)
    {

    }
    public string ConfigButton { get { return null; } }
    public bool AutoActivate { get { return true; } }
    public string Name
    {
        get { return "Break Damage Limit"; }
    }

    public string Author
    {
        get { return "Topher"; }
    }

    public string Description
    {
        get { return "Remove the damage cap allowing damage to exceed 99999."; }
    }

    public GameType GameType
    {
        get { return GameType.FFX; }
    }

    public ModState GetState()
    {
        if (_modActive) return ModState.Activated;
        return ModState.Deactivated;
    }

    public void Activate()
    {
        if (_modActive) return;
        ModLogger.WriteLine("Activating Break Damage Limit");
        
        var assembly = new []
        {
            "nop",
            "nop",
            "nop",
            "nop",
            "mov ebx,0x7FFFFFFF"
        };

        GameMemory.Assembly.Inject(_offsetDamageLimit, assembly);

        _modActive = true;
    }

    public void Deactivate()
    {
        if (!_modActive) return;
        ModLogger.WriteLine("Deactivating Break Damage Limit");
        ModLogger.WriteLine("Deactivation NYI");
        _modActive = false;
    }

    public static void ValidateBytes(byte[] bytes)
    {
        // Verify bytes against original bytes here
    }

    public void Update()
    {
        return;
    }
}