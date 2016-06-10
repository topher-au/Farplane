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

public class FFXBreakDamageLimit : IFarplaneMod
{
    private bool _modActive = false;
    private static int _offsetDamageLimit = 0x38ED3D;

    byte[] _ModDamageLimit = new byte[]
    {
        0x90, 0x90, 0x90, 0x90,         // db 90 90 90 90
        0xBB, 0xFF, 0xFF, 0xFF, 0x7F    // mov ebx, 0x7FFFFFFF
    };

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

    public bool Activated
    {
        get { return _modActive; }
    }
	
    public void Activate()
    {
        if (_modActive) return;
        ModLogger.WriteLine("Activating Break Damage Limit");

        // No check (yet)
        Memory.WriteBytes(_offsetDamageLimit, _ModDamageLimit);

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