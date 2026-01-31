using UnityEngine;

[CreateAssetMenu(fileName = "NewBuff", menuName = "Scriptable Objects/Buff Data")]
public class BuffData : ScriptableObject
{
    [Header("General")]
    public string Name;

    [Header("FX")]
    public ParticleSystem ParticleEffect;

    [Header("Basic Stat Multipliers")]
    public float AttackPowerMultiplier;
    public float MotivationMultiplier;
    public float MotivationLossReduction;
    public float CritRateBonus;
    [Header("Combat Effects")]
    public float HexChanceOnHit;
    public int CurseDamagePerTurn;
    public float ExtraHitChance;
    public float StartOfCombatPacifierChance;
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
    public float LifestealPercentage;
}
