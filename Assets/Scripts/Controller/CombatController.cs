using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CombatController : Singleton<CombatController>
{
    [SerializeField, Range(.0f, 1.0f)] private float CombatTickRate = .5f;
    [SerializeField, Range (20, 100)] private int MaxTicksPerCombat = 50;

    public readonly UnityEvent<CharacterAttackEvent> OnCharacterAttack = new();
    public readonly UnityEvent<EnemyAttackEvent> OnEnemyAttack = new();
    public readonly UnityEvent OnCombatStart = new();
    public readonly UnityEvent<FightResult> OnCombatEnd = new();

    public void FightAgainst(List<EnemyController> enemies)
    {
        StartCoroutine(FightAgainstCoroutine(enemies));
    }

    private IEnumerator FightAgainstCoroutine(List<EnemyController> enemies)
    {
        OnCombatStart.Invoke();

        var tickInterval = new WaitForSeconds(CombatTickRate);
        var playerParty = PlayerController.Instance.PartyMembers;
        var lootedCandies = new List<CandyData>();
        
        while(playerParty.Any(m => m.IsAlive() ) && enemies.Any(e => e.IsAlive()))
        {
            yield return tickInterval;
            lootedCandies.AddRange(PerformCombatTick(enemies));
        }

        PlayerController.Instance.PartyMembers.ForEach(m => m.ResetCombatStacks());
        var r = UnityEngine.Random.Range(0, 100);
        var randomMask = (MaskController)null;

        if (r < 25)
        {
            randomMask = ResourceLoader.Masks.CreateRandomMask();
        }

        OnCombatEnd.Invoke(new FightResult()
        {
            IsWin = playerParty.Any(m => m.IsAlive()),
            LootedCandies = lootedCandies,
            Mask = randomMask
        });
}

    List<CandyData> PerformCombatTick(List<EnemyController> enemies)
    {
        List<CandyData> lootedCandies = new();

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

            if (!target.IsAlive())
            {
                var drops = target.Data.Drops;
                lootedCandies.AddRange(drops);
            }
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

        return lootedCandies;
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

public class FightResult
{
    public List<CandyData> LootedCandies = new();
    public MaskController Mask;
    public bool IsWin;
}
