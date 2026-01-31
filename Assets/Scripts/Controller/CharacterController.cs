using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public int CurrentMotivation;
    public CharacterData Data;
    public MaskController Mask;
    
    public int CurrentCombatStacks;

    public bool IsAlive() => CurrentMotivation > 0;
    public bool IsDead() => !IsAlive();

    public void ResetCombatStacks()
    {
        CurrentCombatStacks = 0;
    }

    public int GetCurrentMaxMotivation()
    {
        var motivation = Data.BaseStats.BaseMotivation;
        var uniqueBuff = GetUniqueMaskBuff();
        var factionBuff = GetFactionBuff();
        var traitBuff = GetTraitBuff();

        if (uniqueBuff != null && uniqueBuff.MotivationMultiplierPercentage > 0)
        {
            var value = (motivation * uniqueBuff.MotivationMultiplierPercentage) / 100;
            motivation += value;
        }

        if (factionBuff != null && factionBuff.MotivationMultiplierPercentage > 0)
        {
            var value = (motivation * factionBuff.MotivationMultiplierPercentage) / 100;
            motivation += value;
        }

        if (traitBuff != null && traitBuff.MotivationMultiplierPercentage > 0)
        {
            var value = (motivation * traitBuff.MotivationMultiplierPercentage) / 100;
            motivation += value;
        }

        return motivation;
    }

    public int GetCurrentCritChancePercentage()
    {
        var critChance = Data.BaseStats.BaseCritRatePercentage;
        var uniqueBuff = GetUniqueMaskBuff();
        var factionBuff = GetFactionBuff();
        var traitBuff = GetTraitBuff();

        if (uniqueBuff != null && uniqueBuff.CritRateBonusPercentage > 0)
        {
            critChance += uniqueBuff.CritRateBonusPercentage;
        }

        if (factionBuff != null && factionBuff.CritRateBonusPercentage > 0)
        {
            critChance += factionBuff.CritRateBonusPercentage;
        }

        if (traitBuff != null && traitBuff.CritRateBonusPercentage > 0)
        {
            critChance += traitBuff.CritRateBonusPercentage;
        }

        return critChance;
    }

    public int GetCurrentAttackPower()
    {
        var power = Data.BaseStats.BaseAttack;
        var uniqueBuff = GetUniqueMaskBuff();
        var factionBuff = GetFactionBuff();
        var traitBuff = GetTraitBuff();

        if (uniqueBuff != null && uniqueBuff.AttackPowerMultiplierPercentage > 0)
        {
            var value = (power * uniqueBuff.AttackPowerMultiplierPercentage) / 100;
            power += value;
        }

        if (factionBuff != null && factionBuff.AttackPowerMultiplierPercentage > 0)
        {
            var value = (power * factionBuff.AttackPowerMultiplierPercentage) / 100;
            power += value;
        }

        if (factionBuff != null && factionBuff.BonusDamagePerUnitInFaction > 0)
        {
            var value = factionBuff.BonusDamagePerUnitInFaction * PlayerController.Instance.GetUnitCountForFaction(Mask.Data.Faction);
            power += value;
        }

        if (traitBuff != null && traitBuff.AttackPowerMultiplierPercentage > 0)
        {
            var value = (power * traitBuff.AttackPowerMultiplierPercentage) / 100;
            power += value;
        }

        return power;
    }

    public int GetCurrentDodgeRatePercentage()
    {
        var rate = Data.BaseStats.BaseDodgeRatePercentage;
        var uniqueBuff = GetUniqueMaskBuff();
        var factionBuff = GetFactionBuff();
        var traitBuff = GetTraitBuff();

        if (uniqueBuff != null && uniqueBuff.DodgeRateBonusPercentage > 0)
        {
            rate += uniqueBuff.DodgeRateBonusPercentage;
        }

        if (factionBuff != null && factionBuff.DodgeRateBonusPercentage > 0)
        {
            rate += factionBuff.DodgeRateBonusPercentage;
        }

        if (traitBuff != null && traitBuff.DodgeRateBonusPercentage > 0)
        {
            rate += traitBuff.DodgeRateBonusPercentage;
        }

        return rate;
    }

    public bool CanBeFed() => CurrentMotivation < GetCurrentMaxMotivation();

    public void TakeDamage(int damage)
    {
        CurrentMotivation -= damage;

        if (CurrentMotivation < 0)
        {
            CurrentMotivation = 0;
        }
    }

    public AttackOutcome Attack(EnemyController enemy)
    {
        var events = new List<AttackEvents>();
        var damage = GetCurrentAttackPower();
        var critChance = GetCurrentCritChancePercentage();
        var hexChance = Mask != null && Mask.Data.UniqueBuff != null ? Mask.Data.UniqueBuff.HexChanceOnHitPercentage : 0;
        var doubleHitChance = Mask != null && Mask.Data.UniqueBuff != null ? Mask.Data.UniqueBuff.ExtraHitChancePercentage : 0;
        var lifestealPercentage = Mask != null && Mask.Data.UniqueBuff != null ? Mask.Data.UniqueBuff.LifestealPercentage : 0;
        var isStacking = Mask != null && Mask.Data.UniqueBuff != null && Mask.Data.UniqueBuff.ApplyStacksOnHit;
        var maxStacks = Mask != null && Mask.Data.UniqueBuff != null ? Mask.Data.UniqueBuff.MaxStacks : 0;
        var extraDamagePerStack = Mask != null && Mask.Data.UniqueBuff != null ? Mask.Data.UniqueBuff.ExtraDamagePerStack : 0;
        var enemyDodgeRate = enemy.GetCurrentDodgeRatePercentage();

        var isDodge = Random.Range(0, 100) < enemyDodgeRate;

        if (isStacking && CurrentCombatStacks < maxStacks)
        {
            CurrentCombatStacks += 1;
        }

        if (isDodge) return new AttackOutcome
        {
            Events = new() { AttackEvents.Miss },
            Damage = 0,
            IsKillingBlow = false
        };

        events.Add(AttackEvents.Hit);

        var isCritical = Random.Range(0, 100) < critChance;
        var isDoubleHit = Random.Range(0, 100) < doubleHitChance;
        var isHexApplied = Random.Range(0, 100) < hexChance;

        if (isHexApplied)
        {
            return new AttackOutcome
            {
                Events = new() { AttackEvents.Hex },
                Damage = 0,
                IsKillingBlow = false,
            };
        }

        if (isCritical)
        {
            damage = Mathf.RoundToInt(damage * 1.5f);
            events.Add(AttackEvents.Critical);
        }

        if (isStacking)
        {
            damage += CurrentCombatStacks * extraDamagePerStack;
            events.Add(AttackEvents.StackDamage);
        }

        if (isDoubleHit)
        {
            damage *= 2;
            events.Add(AttackEvents.DoubleHit);
        }

        enemy.TakeDamage(damage);

        if (lifestealPercentage > 0)
        {
            var lifestealAmount = (damage * lifestealPercentage) / 100;
            CurrentMotivation += lifestealAmount;

            if (CurrentMotivation > GetCurrentMaxMotivation())
            {
                CurrentMotivation = GetCurrentMaxMotivation();
            }
        }

        return new AttackOutcome
        {
            Events = events.Count == 0 ? new() { AttackEvents.Hit } : events,
            Damage = damage,
            IsKillingBlow = enemy.IsDead(),
        };
    }

    private BuffData GetUniqueMaskBuff()
    {
        if (Mask == null || Mask.Data.UniqueBuff == null)
            return null;

        return Mask.Data.UniqueBuff;
    }

    private BuffData GetFactionBuff()
    {
        return PlayerController.Instance.GetPossibleBuffFromFactionsFor(this);
    }

    private BuffData GetTraitBuff()
    {
        return PlayerController.Instance.GetPossibleBuffFromTraitsFor(this);
    }
}

public class AttackOutcome
{
    public List<AttackEvents> Events = new();
    public int Damage;
    public bool IsKillingBlow;
}

public enum AttackEvents
{
    Miss,
    Hit,
    Hex,
    DoubleHit,
    StackDamage,
    Critical,
    Lifesteal,
}
