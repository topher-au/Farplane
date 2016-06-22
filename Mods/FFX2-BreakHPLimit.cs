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

public class FFX2BreakHPLimit : IFarplaneMod
{
    private bool _modActive = false;
    private static int _offsetHPLimit = 0x20E73D;
    private static int _offsetMPLimit = 0x20E774;
    private static int _offsetHPCheck = 0x2365DE;
    private static int _offsetMPCheck = 0x23662D;
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
        get { return GameType.FFX2; }
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
        GameMemory.Assembly.Inject(_offsetHPCheck, new []
        {
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "mov eax,0x7fffffff",
        });
        GameMemory.Assembly.Inject(_offsetMPCheck, new[]
        {
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "mov eax,0x7fffffff",
        });
        GameMemory.Assembly.Inject(_offsetMPLimit, new[]
        {
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "mov eax,0x7fffffff",
        });
        GameMemory.Assembly.Inject(_offsetHPLimit, new[]
        {
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "nop",
            "mov eax,0x7fffffff",
        });

        _modActive = true;
    }

    public void Deactivate()
    {
        if (!_modActive) return;
        ModLogger.WriteLine("Deactivating Break HP/MP Limit");
        GameMemory.Assembly.Inject(_offsetHPCheck, new[]
        {
            "and eax,0x00015f90",
            "add eax,0x0000270f",
        }, true);
        GameMemory.Assembly.Inject(_offsetMPCheck, new[]
        {
            "and eax,0x00002328",
            "add eax,0x000003E7",
        }, true);
        GameMemory.Assembly.Inject(_offsetMPLimit, new[]
        {
            "mov eax,0x0000270F",
            "jne 0x20E780",
            "mov eax,0x000003E7",
        }, true);
        GameMemory.Assembly.Inject(_offsetHPLimit, new[]
        {
            "mov eax,0x0001869F",
            "jne 0x20E749",
            "mov eax,0x0000270F",
        }, true);
        _modActive = false;
    }

    public void Update()
    {
        return;
    }
}