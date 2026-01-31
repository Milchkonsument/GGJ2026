using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class CombatController : Singleton<CombatController>
{
    [SerializeField, Range(.0f, 1.0f)] private float CombatTickRate = .5f;

    public readonly UnityEvent<CharacterAttackEvent> OnCharacterAttack = new();
    public readonly UnityEvent<EnemyAttackEvent> OnEnemyAttack = new();
    public readonly UnityEvent OnCombatStart = new();
    public readonly UnityEvent OnCombatEnd = new();

    public void FightAgainst(List<EnemyController> enemies)
    {
        StartCoroutine(FightAgainstCoroutine(enemies));
    }

    private IEnumerator FightAgainstCoroutine(List<EnemyController> enemies)
    {
        OnCombatStart.Invoke();

        var tickInterval = new WaitForSeconds(CombatTickRate);
        var playerParty = PlayerController.Instance.PartyMembers;
        
        while(playerParty.Any(m => m.IsAlive() ) && enemies.Any(e => e.IsAlive()))
        {
            yield return tickInterval;
            PerformCombatTick(enemies);
        }

        PlayerController.Instance.PartyMembers.ForEach(m => m.ResetCombatStacks());

        OnCombatEnd.Invoke();
}

    void PerformCombatTick(List<EnemyController> enemies)
    {
        foreach (var member in PlayerController.Instance.PartyMembers.Where(m => m.IsAlive()))
        {
            var target = enemies.Where(e => e.IsAlive()).GetRandomElement();
            var outcome = member.Attack(target);
            OnCharacterAttack.Invoke(new CharacterAttackEvent
            {
                Attacker = member,
                Target = target,
                Outcome = outcome
            });
        }

        foreach (var enemy in enemies.Where(e => e.IsAlive()))
        {
            var target = PlayerController.Instance.PartyMembers.Where(m => m.IsAlive()).GetRandomElement();
           var outcome = enemy.Attack(target);
            OnEnemyAttack.Invoke(new EnemyAttackEvent
            {
                Attacker = enemy,
                Target = target,
                Outcome = outcome
            });
        }
    }
}

public class CharacterAttackEvent
{
    public CharacterController Attacker;
    public EnemyController Target;
    public AttackOutcome Outcome;
}

public class EnemyAttackEvent
{
    public EnemyController Attacker;
    public CharacterController Target;
    public AttackOutcome Outcome;
}