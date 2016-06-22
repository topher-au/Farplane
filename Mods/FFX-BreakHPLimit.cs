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

public class FFXBreakHPLimit : IFarplaneMod
{
    private bool _modActive = false;
    private static int _offsetHPLimit = 0x386897;
    private static int _offsetMPLimit = 0x3868BF;
    private static int _offsetHPCheck = 0x38D389;
    private static int _offsetMPCheck = 0x38E5FE;
    public void Configure(object parentWindow)
    {

    }
    public string ConfigButton { get { return null; } }
    public bool AutoActivate { get { return true; } }
    public string Name
    {
        get { return "Break HP Limit"; }
    }

    public string Author
    {
        get { return "Topher"; }
    }

    public string Description
    {
        get { return "Remove the HP/MP cap allowing HP and MP to exceed 99999."; }
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
        ModLogger.WriteLine("Activating Break HP/MP Limit");

        // No check (yet)
        GameMemory.Assembly.Inject(_offsetHPCheck, "and eax,7FFFFFFF");
        GameMemory.Assembly.Inject(_offsetMPLimit, "mov eax,7FFFFFFF");
        GameMemory.Assembly.Inject(_offsetMPCheck, "push 7fffffff");
        GameMemory.Assembly.Inject(_offsetHPCheck, new[]
        {
            "and eax, 7FFFFFFF",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
        });

        _modActive = true;
    }

    public void Deactivate()
    {
        if (!_modActive) return;
        ModLogger.WriteLine("Deactivating Break HP/MP Limit");
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