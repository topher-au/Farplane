//css_reference "..\bin\Debug\farplane.exe";
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Farplane;
using Farplane.Common;
using Farplane.FarplaneMod;
using Farplane.FFX;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using Farplane.Memory;
using MahApps.Metro.Controls;

public class SeymourMod : IFarplaneMod
{
    private bool _modActive = false;

    private static int _offsetModBytes1 = 0x4A8F47;
    private static int _offsetModBytes2 = 0x4A8F9A;
    private static int _offsetPartyList = Offsets.GetOffset(OffsetType.PartyList);

    private static int offsetSeymourData = Offsets.GetOffset(OffsetType.PartyStatsBase) + 7 * StructHelper.GetSize<PartyMember>();
    private static int offsetInParty = StructHelper.GetFieldOffset<PartyMember>("InParty");
    private static int offsetSeymourInParty = offsetSeymourData + offsetInParty;
    private static int updateTicks = 0;
    
    public void Configure(object parentWindow)
    {
        var win = new MetroWindow() {Title="Configuration", Width=300, Height=100, WindowStartupLocation = WindowStartupLocation.CenterScreen, Owner=(Window)parentWindow, BorderThickness = new Thickness(0), GlowBrush=Brushes.Black, ResizeMode = ResizeMode.NoResize};
        var check = new CheckBox() {Content="Add/remove Seymour automatically", Margin = new Thickness(5), IsChecked=ModSettings.ReadSetting<bool>("AutoAddRemove") };
        var butt = new Button() {Content="Save", Margin = new Thickness(5) };
        var stack = new StackPanel() {Children = { check, butt }, Margin= new Thickness(5) };
        butt.Click += (sender, args) =>
        {
            ModSettings.WriteSetting("AutoAddRemove", check.IsChecked);
            win.Close();
        };
        win.Content = stack;
        win.ShowDialog();
    }

    public string ConfigButton { get { return "Configure"; } }
    public bool AutoActivate { get { return true; } }
    private static byte[] _modBytes = GameMemory.Assembly.Generate(new[]
    {
        "nop",
        "nop",
        "nop",
        "nop",
    });

    private static byte[] _originalBytes1 = GameMemory.Assembly.Generate(new[]
    {
        "cmp al,07",
        "je 0x4A8F6F"
    }, _offsetModBytes1);

    private static byte[] _originalBytes2 = GameMemory.Assembly.Generate(new[]
    {
        "cmp al,07",
        "je 0x4A8FBC"
    }, _offsetModBytes2);

    public string Name
    {
        get { return "Seymour in Party"; }
    }

    public string Author
    {
        get { return "Topher"; }
    }

    public string Description
    {
        get { return "Adds Seymour to your party and enables him in the menu."; }
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

    public static bool ValidateBytes()
    {
        var codeBytes1 = GameMemory.Read<byte>(_offsetModBytes1, _originalBytes1.Length);
        var codeBytes2 = GameMemory.Read<byte>(_offsetModBytes2, _originalBytes2.Length);

        if ((codeBytes1.SequenceEqual(_originalBytes1) &&
            codeBytes2.SequenceEqual(_originalBytes2)) ||
            (codeBytes1.SequenceEqual(_modBytes) &&
            codeBytes2.SequenceEqual(_modBytes))) return true;
        ModLogger.WriteLine("Unexpected assembly code, aborting code write.");
        return false;
    }
	
    public void Activate()
    {
        if (_modActive) return;

        _modActive = true;

        if (ValidateBytes())
        {
            GameMemory.Write<byte>(_offsetModBytes1, _modBytes);
            GameMemory.Write<byte>(_offsetModBytes2, _modBytes);
        }

        if (ModSettings.ReadSetting<bool>("AutoAddRemove"))
        {
            Party.AddCharacter(Character.Seymour);
            GameMemory.Write<byte>(offsetSeymourInParty, 17);
        }
    }

    public void Deactivate()
    {
        if (!_modActive) return;

        _modActive = false;

        if (ValidateBytes())
        {
            GameMemory.Write<byte>(_offsetModBytes1, _originalBytes1);
            GameMemory.Write<byte>(_offsetModBytes2, _originalBytes2);
        }
        if (ModSettings.ReadSetting<bool>("AutoAddRemove"))
        {
            Party.RemoveCharacter(Character.Seymour);
            GameMemory.Write<byte>(offsetSeymourInParty, 16);
        }
    }

    public void Update()
    {
        // No update logic for this mod
    }
}