using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farplane.Common;
using Farplane.FFX.Values;
using Farplane.Memory;

namespace Farplane.FFX.Data
{
    public static class Battle
    {
        private static readonly int _offsetEnemyPointer = OffsetScanner.GetOffset(GameOffset.FFX_BattlePointerEnemy);
        private static readonly int _offsetPartyPointer = OffsetScanner.GetOffset(GameOffset.FFX_BattlePointerParty);

        public const int BlockLengthEntity = 0xF90;

        public static bool GetBattleState()
        {
            var battlePointer = LegacyMemoryReader.ReadUInt32(_offsetPartyPointer);
            return battlePointer != 0;
        }

        public static BattleEntityData GetPartyEntity(int entityIndex)
        {
            BattleEntityData entityData;
            BattleEntity.ReadEntity(EntityType.Party, entityIndex, out entityData);
            return entityData;
        }

        public static BattleEntityData GetEnemyEntity(int entityIndex)
        {
            BattleEntityData entityData;
            BattleEntity.ReadEntity(EntityType.Enemy, entityIndex, out entityData);
            return entityData;
        }

    }
}
