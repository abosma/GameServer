using System.Numerics;
using GameServerCore.Enums;

namespace LeagueSandbox.GameServer.Content
{
    public interface ICharData
    {
        float BaseHp { get; }
        float BaseMp { get; }
        float HpPerLevel { get; }
        float MpPerLevel { get; }
        float HpRegenPerLevel { get; }
        float MpRegenPerLevel { get; }
        float BaseStaticHpRegen { get; }
        float BaseFactorHpRegen { get; }
        float BaseStaticMpRegen { get; }
        float BaseFactorMpRegen { get; }
        float BaseDamage { get; }
        float DamagePerLevel { get; }
        float Armor { get; }
        float ArmorPerLevel { get; }
        float SpellBlock { get; }
        float SpellBlockPerLevel { get; }
        float BaseDodge { get; }
        float DodgePerLevel { get; }
        float BaseMissChance { get; }
        float BaseCritChance { get; }
        float CritPerLevel { get; }
        float CritDamageBonus { get; }
        int MoveSpeed { get; }
        float AttackRange { get; }
        float AttackAutoInterruptPercent { get; }
        float AcquisitionRange { get; }
        float AttackSpeedPerLevel { get; }
        float TowerTargetingPriority { get; }
        float DeathTime { get; }
        float ExpGivenOnDeath { get; }
        float GoldGivenOnDeath { get; }
        float ExperienceRadius { get; }
        float GoldRadius { get; }
        float DeathEventListeningRadius { get; }
        bool LocalGoldSplitWithLastHitter { get; }
        float LocalGoldGivenOnDeath { get; }
        float LocalExpGivenonDeath { get; }
        float GlobalGoldGivenOnDeath { get; }
        float GlobalExpGivenOnDeath { get; }
        float Significance { get; }
        float AbilityPower { get; }
        float AbilityPowerIncPerLevel { get; }
        float PerceptionBubbleRadius { get; }
        float OverrideCollisionHeight { get; }
        float OverrideCollisionWidth { get; }
        float PathfindingCollisionRadius { get; }
        float GameplayCollisionRadius { get; }
        bool UseOverrideBoundingBox { get; }
        Vector3 OverrideboundingBox { get; }
        float BoundingCylinderRadius { get; }
        float BoundingCylinderHeight { get; }
        float BoundingSphereRadius { get; }
        float OccludedUnitySelectableDistance { get; }
        string[] SpellNames { get; }
        string[] ExtraSpells { get; }
        string[] AttackNames { get; }
        string[] PassiveNames { get; }
        string[] PassiveLuaNames { get; }
        string CriticalAttackStr { get; }
        int[] SpellMaxLevelsOverride { get; }
        int[][] SpellsUpLevelsOverride { get; }
        float[] AttackDelayCastOffsetPercentAttackSpeedRatio { get; }
        float[] AttackDelayCastOffsetPercent { get; }
        float[] AttackDelayOffsetPercent { get; }
        float[] AttackProbability { get; }
        float[] PassiveRanges { get; }
        bool IsMelee { get; } //Yes or no
        PrimaryAbilityResourceType ParType { get; }
        IPassiveData[] Passives { get; }
        void Load(string name);
    }
}