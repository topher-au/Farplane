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

public class SeymourMod : IFarplaneMod
{
    private bool _modActive = false;

    private int _offsetModBytes1 = 0x4A8F47;
    private int _offsetModBytes2 = 0x4A8F9A;
    private int _offsetPartyList = Offsets.GetOffset(OffsetType.PartyList);

    private static int offsetSeymourData = Offsets.GetOffset(OffsetType.PartyStatsBase) + 7 * StructHelper.GetSize<PartyMember>();
    private static int offsetInParty = StructHelper.GetFieldOffset<PartyMember>("InParty");
    private static int offsetSeymourInParty = offsetSeymourData + offsetInParty;
    private static int updateTicks = 0;

    // An array containing bytecode to be written to the game

    private static byte[] modBytes = new byte[]
    {
              // This code does nothing
        0x90, //  nop
        0x90, //  nop
        0x90, //  nop
        0x90, //  nop
    };

    // An array containing the original bytecode
    
    private static byte[] originalBytes1 = new byte[]
    {
                    // This code checks if Seymour is in the party and stops
                    // him from showing up in the menus
        0x3C, 0x07, //  cmp al,07
        0x74, 0x24  //  je 015A8F6F
    };

    private static byte[] originalBytes2 = new byte[]
    {
                    // This code checks if Seymour is in the party and stops
                    // him from showing up in the menus
        0x3C, 0x07, //  cmp al,07
        0x74, 0x1e  //  je 015A8F6F
    };

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

    public bool Activated
    {
        get { return _modActive; }
    }
	
    public void Activate()
    {
        if (_modActive) return;

        _modActive = true;

        // Add Seymour to the party and set his state to active
        Party.AddCharacter(Character.Seymour);
        Memory.WriteByte(offsetSeymourInParty, 17);

        // Read the existing code and check if it is what we are expecting
        var bytes1 = Memory.ReadBytes(_offsetModBytes1, 4);
        var bytes2 = Memory.ReadBytes(_offsetModBytes2, 4);

		
        if (!originalBytes1.SequenceEqual(bytes1) || !originalBytes2.SequenceEqual(bytes2))
        {
            ModLogger.WriteLine("Unexpected assembly code, aborting code write.");
            return;
        }
        
        // Write modified assembly bytes
        Memory.WriteBytes(_offsetModBytes1, modBytes);
        Memory.WriteBytes(_offsetModBytes2, modBytes);
    }

    public void Deactivate()
    {
        if (!_modActive) return;

        _modActive = false;

        // Remove Seymour from party and set his state to inactive
        Party.RemoveCharacter(Character.Seymour);
        Memory.WriteByte(offsetSeymourInParty, 16);

        // Read current assembly code from memory
        var bytes1 = Memory.ReadBytes(_offsetModBytes1, 4);
        var bytes2 = Memory.ReadBytes(_offsetModBytes2, 4);

        // Check if code has been modified outside mod
        if (!bytes1.SequenceEqual(modBytes) || !bytes2.SequenceEqual(modBytes))
        {
            ModLogger.WriteLine("Unexpected assembly code, aborting code write");
            _modActive = false;
            return;
        }
        
        // Write original assembly code
        Memory.WriteBytes(_offsetModBytes1, originalBytes1);
        Memory.WriteBytes(_offsetModBytes2, originalBytes2);
    }

    public void Update()
    {
        // No update logic for this mod
    }
}