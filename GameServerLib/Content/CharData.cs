using System;
using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.Logging;
using log4net;
using Newtonsoft.Json;

namespace LeagueSandbox.GameServer.Content
{
    public class PassiveData : IPassiveData
    {
        public string PassiveNameStr { get; set; } = "";
        public string PassiveLuaName { get; set; } = "";
        public int[] PassiveLevels { get; set; } = { -1, -1, -1, -1, -1, -1 };
    }

    public class CharData : ICharData
    {
        private readonly Game _game;
        private readonly ILog _logger;

        public CharData(Game game)
        {
            _game = game;
            _logger = LoggerProvider.GetLogger();
        }

        public float BaseHp { get; private set; } = 100.0f;
        public float BaseMp { get; private set; } = 100.0f;
        public float HpPerLevel { get; private set; } = 0.0f;
        public float MpPerLevel { get; private set; } = 0.0f;
        public float HpRegenPerLevel { get; private set; } = 0.0f;
        public float MpRegenPerLevel { get; private set; } = 0.0f;
        public float BaseStaticHpRegen { get; private set; } = 1.0f;
        public float BaseFactorHpRegen { get; private set; } = 0.0f;
        public float BaseStaticMpRegen { get; private set; } = 1.0f;
        public float BaseFactorMpRegen { get; private set; } = 0.0f;
        public float BaseDamage { get; private set; } = 10.0f;
        public float DamagePerLevel { get; private set; } = 0.0f;
        public float Armor { get; private set; } = 1.0f;
        public float ArmorPerLevel { get; private set; } = 1.0f;
        public float SpellBlock { get; private set; } = 0.0f;
        public float SpellBlockPerLevel { get; private set; } = 0.0f;
        public float BaseDodge { get; private set; } = 0.0f;
        public float DodgePerLevel { get; private set; } = 0.0f;
        public float BaseMissChance { get; private set; } = 0.0f;
        public float BaseCritChance { get; private set; } = 0.0f;
        public float CritPerLevel { get; private set; } = 0.0f;
        public float CritDamageBonus { get; private set; } = 2.0f;
        public int MoveSpeed { get; private set; } = 100;
        public float AttackRange { get; private set; } = 100.0f;
        public float AttackAutoInterruptPercent { get; private set; } = 0.2f;
        public float AcquisitionRange { get; private set; } = 750.0f;
        public float AttackSpeedPerLevel { get; private set; } = 0.0f;
        public float TowerTargetingPriority { get; private set; } = 0.0f;
        public float DeathTime { get; private set; } = -1.0f;
        public float ExpGivenOnDeath { get; private set; } = 48.0f;
        public float GoldGivenOnDeath { get; private set; } = 25.0f;
        public float ExperienceRadius { get; private set; } = 0.0f;
        public float GoldRadius { get; private set; } = 0.0f;
        public float DeathEventListeningRadius { get; private set; } = 1000.0f;
        public bool LocalGoldSplitWithLastHitter { get; private set; } = false;
        public float LocalGoldGivenOnDeath { get; private set; } = 0.0f;
        public float LocalExpGivenonDeath { get; private set; } = 0.0f;
        public float GlobalGoldGivenOnDeath { get; private set; } = 0.0f;
        public float GlobalExpGivenOnDeath { get; private set; } = 0.0f;
        public float Significance { get; private set; } = 0.0f;
        public float AbilityPower { get; private set; } = 0.0f;
        public float AbilityPowerIncPerLevel { get; private set; } = 0.0f;
        public float PerceptionBubbleRadius { get; private set; } = 1350.0f;
        public float OverrideCollisionHeight { get; private set; } = -1.0f;
        public float OverrideCollisionWidth { get; private set; } = -1.0f;
        public float PathfindingCollisionRadius { get; private set; } = -1.0f;
        public float GameplayCollisionRadius { get; private set; } = 65.0f;
        public bool UseOverrideBoundingBox { get; private set; }
        public Vector3 OverrideboundingBox { get; private set; }
        public float BoundingCylinderRadius { get; private set; }
        public float BoundingCylinderHeight { get; private set; }
        public float BoundingSphereRadius { get; private set; }
        public float OccludedUnitySelectableDistance { get; private set; } = 0.0f;
        public string[] SpellNames { get; private set; } = {"", "", "", ""};
        public string[] ExtraSpells { get; private set; } = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""};
        public string[] AttackNames { get; private set; } = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""};
        public string[] PassiveNames { get; private set; } = {"", "", "", "", "", ""};
        public string[] PassiveLuaNames { get; private set; } = { "", "", "", "", "", "" };
        public string CriticalAttackStr { get; private set; }
        public int[] SpellMaxLevelsOverride { get; private set; } = { 5, 5, 5, 3 };

        public int[][] SpellsUpLevelsOverride { get; private set; } =
        {
            new[] {1, 3, 5, 7, 9, 99},
            new[] {1, 3, 5, 7, 9, 99},
            new[] {1, 3, 5, 7, 9, 99},
            new[] {6, 11, 16, 99, 99, 99}
        };

        public float[] AttackDelayCastOffsetPercentAttackSpeedRatio { get; private set; } =
        {
            1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f
        };

        public float[] AttackDelayCastOffsetPercent { get; private set; } =
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public float[] AttackDelayOffsetPercent { get; private set; } =
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f
        };

        public float[] AttackProbability { get; private set; } =
        {
            2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f
        };

        public float[] PassiveRanges { get; private set; }

        //public float AttackDelayOffsetPercent { get; private set; }
        //public float AttackDelayCastOffsetPercent { get; private set; }
        public bool IsMelee { get; private set; } //Yes or no
        
        public PrimaryAbilityResourceType ParType { get; private set; } = PrimaryAbilityResourceType.MANA;

        public IPassiveData[] Passives { get; private set; } =
        {
            new PassiveData(),
            new PassiveData(),
            new PassiveData(),
            new PassiveData(),
            new PassiveData(),
            new PassiveData()
        };

        public void Load(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            var file = new ContentFile();
            try
            {
                file = (ContentFile)_game.Config.ContentManager.GetContentFileFromJson("Stats", name);
            }
            catch (ContentNotFoundException exception)
            {
                _logger.Warn(exception.Message);
                return;
            }

            BaseHp = file.GetFloat("Data", "BaseHP", BaseHp);
            BaseMp = file.GetFloat("Data", "BaseMP", BaseMp);

            HpPerLevel = file.GetFloat("Data", "HPPerLevel", HpPerLevel);
            MpPerLevel = file.GetFloat("Data", "MPPerLevel", MpPerLevel);

            HpRegenPerLevel = file.GetFloat("Data", "HPRegenPerLevel", HpRegenPerLevel);
            MpRegenPerLevel = file.GetFloat("Data", "MPRegenPerLevel", MpRegenPerLevel);

            BaseStaticHpRegen = file.GetFloat("Data", "BaseStaticHPRegen", BaseStaticHpRegen);
            BaseFactorHpRegen = file.GetFloat("Data", "BaseFactorHPRegen", BaseFactorHpRegen);
            BaseStaticMpRegen = file.GetFloat("Data", "BaseStaticMPRegen", BaseStaticMpRegen);
            BaseFactorMpRegen = file.GetFloat("Data", "BaseFactorMPRegen", BaseFactorMpRegen);

            BaseDamage = file.GetFloat("Data", "BaseDamage", BaseDamage);
            DamagePerLevel = file.GetFloat("Data", "DamagePerLevel", DamagePerLevel);

            Armor = file.GetFloat("Data", "Armor", Armor);
            ArmorPerLevel = file.GetFloat("Data", "ArmorPerLevel", ArmorPerLevel);

            SpellBlock = file.GetFloat("Data", "SpellBlock", SpellBlock);
            SpellBlockPerLevel = file.GetFloat("Data", "SpellBlockPerLevel", SpellBlockPerLevel);

            BaseDodge = file.GetFloat("Data", "BaseDodge", BaseDodge);
            DodgePerLevel = file.GetFloat("Data", "LevelDodge", DodgePerLevel);

            BaseCritChance = file.GetFloat("Data", "BaseCritChance", BaseCritChance);
            CritPerLevel = file.GetFloat("Data", "CritPerLevel", CritPerLevel);
            CritDamageBonus = file.GetFloat("Data", "CritDamageBonus", CritDamageBonus);

            AttackRange = file.GetFloat("Data", "AttackRange", AttackRange);
            AcquisitionRange = file.GetFloat("Data", "AcquisitionRange", AcquisitionRange);

            MoveSpeed = file.GetInt("Data", "MoveSpeed", MoveSpeed);

            AttackSpeedPerLevel = file.GetFloat("Data", "AttackSpeedPerLevel", AttackSpeedPerLevel);
            AttackDelayOffsetPercent[0] = file.GetFloat("Data", "AttackDelayOffsetPercent", AttackDelayOffsetPercent[0]);
            AttackDelayCastOffsetPercent[0] = file.GetFloat("Data", "AttackDelayCastOffsetPercent", AttackDelayCastOffsetPercent[0]);
            IsMelee = file.GetString("Data", "IsMelee", IsMelee ? "true" : "false").Equals("true");

            PathfindingCollisionRadius = file.GetFloat("Data", "PathfindingCollisionRadius", PathfindingCollisionRadius);
            GameplayCollisionRadius = file.GetFloat("Data", "GameplayCollisionRadius", GameplayCollisionRadius);

            Enum.TryParse<PrimaryAbilityResourceType>(file.GetString("Data", "PARType", ParType.ToString()), out var tempPar);

            ParType = tempPar;

            for (var i = 0; i < 4; i++)
            {
                SpellNames[i] = file.GetString("Data", $"Spell{i + 1}", SpellNames[i]);
            }

            for (var i = 0; i < 4; i++)
            {
                SpellsUpLevelsOverride[i] = file.GetIntArray("Data", $"SpellsUpLevels{i + 1}", SpellsUpLevelsOverride[i]);
            }

            SpellMaxLevelsOverride = file.GetIntArray("Data", "MaxLevels", SpellMaxLevelsOverride);

            for (var i = 0; i < 8; i++)
            {
                ExtraSpells[i] = file.GetString("Data", $"ExtraSpell{i + 1}", ExtraSpells[i]);
            }

            for (var i = 0; i < 6; i++)
            {
                Passives[i].PassiveNameStr = file.GetString("Data", $"Passive{i + 1}Name", Passives[i].PassiveNameStr);
                Passives[i].PassiveLuaName =
                    file.GetString("Data", $"Passive{i + 1}LuaName", Passives[i].PassiveLuaName);
                Passives[i].PassiveLevels = file.GetMultiInt("Data", $"Passive{i + 1}Level", 6, -1);
            }
        }
    }
}
