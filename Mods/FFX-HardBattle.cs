//css_reference "D:\Dev\Farplane\Farplane\bin\Debug\Farplane.exe";
using System;
using System.Linq;
using System.Threading;
using System.Windows;

using Farplane;
using Farplane.Common;
using Farplane.FarplaneMod;
using Farplane.FFX;
using Farplane.FFX.Data;
using Farplane.FFX.Values;
using Farplane.Memory;

public class FFXHardBattleMod : IFarplaneMod
{
    private bool _modActive = false;
    private static int _offsetEnemyPointer = Offsets.GetOffset(OffsetType.BattleEnemyPointer);
    private static int _sizeBattleEntity = StructHelper.GetSize<BattleEntityData>();
    private static bool changedThisBattle;
    private static bool[] changedCreature;
    public int m_HpMpMultiplier = 10;
    public int m_StatMultiplier = 3;
    public void Configure(object parentWindow)
    {

    }
    public string ConfigButton { get { return null; } }
    public bool AutoActivate { get { return true; } }
    public string Name
    {
        get { return "Harder Battles"; }
    }

    public string Author
    {
        get { return "Topher"; }
    }

    public string Description
    {
        get { return "Multiplies enemy stats at the start of every battle."; }
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
        ModLogger.WriteLine("Harder battle mod activated");
        _modActive = true;
    }

    public void Deactivate()
    {
        if (!_modActive) return;
        ModLogger.WriteLine("Harder battle mod deactivated");
        _modActive = false;
    }

    public void Update()
    {
        if (!_modActive) return;

        // Read battle pointer from memory
        var battlePointer = GameMemory.Read<IntPtr>(_offsetEnemyPointer);

        // Check if we need to reset battle
        if (battlePointer == IntPtr.Zero && changedCreature != null)
        {
            changedThisBattle = false;
            changedCreature = null;
        }

        if (battlePointer != IntPtr.Zero && !changedThisBattle)
        {
            ModLogger.WriteLine("Battle detected, modifying enemies");
            changedCreature = new bool[8];

            // Loop until all creatures are modified or limit is hit
            var loopLimit = 2000; // maximum times to attempt modification before giving up

            while (changedCreature.Contains(false) && loopLimit > 0)
            {
                loopLimit--;
                for (int i = 0; i < 8; i++)
                {
                    if (changedCreature[i]) continue; // Enemy already modified

                    var entityData = Battle.GetEnemyEntity(i);
                    var entityName = StringConverter.ToString(entityData.text_name);
                    int entityOffset = (int)battlePointer + _sizeBattleEntity*i;

                    if (entityData.guid == 0) // No enemy in this slot
                        continue;

                    ModLogger.WriteLine("Modifying creature: {0}", entityName);

                    var newHP = entityData.hp_max*m_HpMpMultiplier;
                    newHP = newHP > int.MaxValue ? int.MaxValue : newHP;

                    var newMP = entityData.mp_max* m_HpMpMultiplier;
                    newMP = newMP > int.MaxValue ? int.MaxValue : newMP;

                    var newStrength = entityData.strength* m_StatMultiplier;
                    newStrength = newStrength > byte.MaxValue ? byte.MaxValue : newStrength;
                    var newDefense = entityData.defense*m_StatMultiplier;
                    newDefense = newDefense > byte.MaxValue ? byte.MaxValue : newDefense;
                    var newMagic = entityData.magic * m_StatMultiplier;
                    newMagic = newMagic > byte.MaxValue ? byte.MaxValue : newMagic;
                    var newMagicDef = entityData.magic_defense * m_StatMultiplier;
                    newMagicDef = newMagicDef > byte.MaxValue ? byte.MaxValue : newMagicDef;
                    var newAgility = entityData.agility * m_StatMultiplier;
                    newAgility = newAgility > byte.MaxValue ? byte.MaxValue : newAgility;
                    var newLuck = entityData.luck * m_StatMultiplier;
                    newLuck = newLuck > byte.MaxValue ? byte.MaxValue : newLuck;
                    var newAccuracy = entityData.accuracy * m_StatMultiplier;
                    newAccuracy = newAccuracy > byte.MaxValue ? byte.MaxValue : newAccuracy;
                    var newEvasion = entityData.evasion * m_StatMultiplier;
                    newEvasion = newEvasion > byte.MaxValue ? byte.MaxValue : newEvasion;

                    // update entity values
                    GameMemory.Write<int>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("hp_current"),
                        newHP, false);

                    GameMemory.Write<int>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("hp_max"),
                        newHP, false);

                    GameMemory.Write<int>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("hp_max2"),
                        newHP, false);

                    GameMemory.Write<int>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("mp_current"),
                        newMP, false);

                    GameMemory.Write<int>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("mp_max"),
                        newMP, false);
                
                    GameMemory.Write<int>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("mp_max2"),
                        newMP, false);

                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("strength"),
                        (byte)newStrength, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("defense"),
                        (byte)newDefense, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("magic"),
                        (byte)newMagic, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("magic_defense"),
                        (byte)newMagicDef, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("agility"),
                        (byte)newAgility, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("luck"),
                        (byte)newLuck, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("accuracy"),
                        (byte)newAccuracy, false);
                    GameMemory.Write<byte>(entityOffset + StructHelper.GetFieldOffset<BattleEntityData>("evasion"),
                        (byte)newEvasion, false);


                    changedCreature[i] = true;
                }
            }
            changedThisBattle = true;
        }
        
    }
}