using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data;
    public int CurrentMotivation = 0;

    public bool IsAlive() => CurrentMotivation > 0;
    public bool IsDead() => !IsAlive();

    public int GetCurrentMaxMotivation()
    {
        return Data.BaseStats.BaseMotivation;
    }

    public int GetCurrentAttackPower()
    {
        return Data.BaseStats.BaseAttack;
    }

    public int GetCurrentCritChancePercentage()
    {
        return Data.BaseStats.BaseCritRatePercentage;
    }

    public int GetCurrentDodgeRatePercentage()
    {
        return Data.BaseStats.BaseDodgeRatePercentage;
    }

    public void TakeDamage(int damage)
    {
        CurrentMotivation -= damage;
        
        if (CurrentMotivation < 0)
        {
            CurrentMotivation = 0;
        }
    }

    public AttackOutcome Attack(CharacterController target)
    {
        var characterDodgeRate = target.GetCurrentDodgeRatePercentage();
        var critChance = GetCurrentCritChancePercentage();
        var damage = GetCurrentAttackPower();

        var events = new List<AttackEvents>();
        
        var isDodge = Random.Range(0, 100) < characterDodgeRate;
        if (isDodge)
        {
            return new AttackOutcome
            {
                Events = new() { AttackEvents.Miss },
                Damage = 0,
                IsKillingBlow = false
            };
        }

        events.Add(AttackEvents.Hit);

        var isCritical = Random.Range(0, 100) < critChance;
        if (isCritical)
        {
            damage = Mathf.RoundToInt(damage * 1.5f);
            events.Add(AttackEvents.Critical);
        }

        target.TakeDamage(damage);

        return new AttackOutcome
        {
            Events = events.Count == 0 ? new() { AttackEvents.Hit } : events,
            Damage = damage,
            IsKillingBlow = target.IsDead(),
        };
    }
}