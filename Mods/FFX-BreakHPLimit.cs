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

public class FFXBreakHPLimit : IFarplaneMod
{
    private bool _modActive = false;
    private static int _offsetHPLimit = 0x386897;
    private static int _offsetMPLimit = 0x3868BF;
    private static int _offsetHPCheck = 0x38D389;
    private static int _offsetMPCheck = 0x38E5FE;

    byte[] _ModHPLimit = new byte[]
    {
        0xB8, 0xFF, 0xFF, 0xFF, 0x7F    // mov eax, 0x7FFFFFFF
    };

    byte[] _ModHPCheck = new byte[]
    {
        0x25, 0xFF, 0xFF, 0xFF, 0x7F,   // and eax,7FFFFFFF
        0x90,                           // nop
        0x90,                           // nop
        0x90,                           // nop
        0x90,                           // nop
        0x90,                           // nop
    };   

    byte[] _ModMPLimit = new byte[]
    {
        0xB8, 0xFF, 0xFF, 0xFF, 0x7F    // mov eax, 0x7FFFFFFF
    };

    byte[] _ModMPCheck = new byte[]
    {
        0x68, 0xFF, 0xFF, 0xFF, 0x7F    // push 0x7FFFFFFF
    };   

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

    public bool Activated
    {
        get { return _modActive; }
    }
	
    public void Activate()
    {
        if (_modActive) return;
        ModLogger.WriteLine("Activating Break HP/MP Limit");

        // No check (yet)
        Memory.WriteBytes(_offsetHPLimit, _ModHPLimit);
        Memory.WriteBytes(_offsetMPLimit, _ModMPLimit);
        Memory.WriteBytes(_offsetHPCheck, _ModHPCheck);
        Memory.WriteBytes(_offsetMPCheck, _ModMPCheck);

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