using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Scriptable Objects/Buff Data")]
public class BuffData : ScriptableObject
{
    [Header("General")]
    public string Name;

    [Header("FX")]
    public ParticleSystem ParticleEffect;

    [Header("Basic Stat Multipliers")]
    public int AttackPowerMultiplierPercentage;
    public int MotivationMultiplierPercentage;
    public int CritRateBonusPercentage;

    [Header("Combat Effects")]
    public int MotivationLossReductionPercentage;
    public int HexChanceOnHitPercentage;
    public int CurseDamagePerTurn;
    public int ExtraHitChancePercentage;
    public int StartOfCombatPacifierPercentage;
    public int EndOfCombatHealing;
    public int EndOfCombatCandyGain;
    public int MotivationGainPerRound;
    public int ExtraDamageFromExtraCandy;
    public int MaximumExtraCandyUsable;
    public bool ApplyStacksOnHit;
    public int MaxStacks;
    public int ExtraDamagePerStack;
    public int BonusCandyPerDefeatedEnemy;
    public int BonusDamagePerUnitInFaction;
    public int LifestealPercentage;
    public int DodgeRateBonusPercentage;
    public int PlanningPhasePartyFinderPercentage;
    public int BloodCandyGenerationOnKillingBlow;
}
