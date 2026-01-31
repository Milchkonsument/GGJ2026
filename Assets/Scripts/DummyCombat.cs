using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DummyCombat : MonoBehaviour
{
    public List<CharacterData> DummyCharacters;
    public List<EnemyData> DummyEnemies;

    private List<CharacterController> CharacterControllers = new();
    private List<EnemyController> EnemyControllers = new();

    void Awake()
    {
        CharacterControllers = DummyCharacters.ConvertAll(data =>
        {
            var go = new GameObject(data.Name);
            var controller = go.AddComponent<CharacterController>();
            controller.Data = data;
            controller.CurrentMotivation = controller.GetCurrentMaxMotivation();
            return controller;
        });

        EnemyControllers = DummyEnemies.ConvertAll(data =>
        {
            var go = new GameObject(data.Name);
            var controller = go.AddComponent<EnemyController>();
            controller.Data = data;
            controller.CurrentMotivation = controller.GetCurrentMaxMotivation();
            return controller;
        });

        CombatController.Instance.OnCombatStart.AddListener(() =>
        {
            Debug.Log("Combat Started!");
        });
            CombatController.Instance.OnCombatEnd.AddListener(() =>
            {
                Debug.Log($"Combat Ended! Victory: {PlayerController.Instance.PartyMembers.Any(c => c.IsAlive())}");
                Debug.Log("Surviving Party Members:");
                foreach (var member in PlayerController.Instance.PartyMembers.Where(c => c.IsAlive()))
                {
                    Debug.Log($"- {member.Data.Name} with {member.CurrentMotivation}/{member.GetCurrentMaxMotivation()} Motivation");
                }
                Debug.Log("Surviving Enemies:");
                foreach (var enemy in EnemyControllers.Where(e => e.IsAlive()))
                {
                    Debug.Log($"- {enemy.Data.Name} with {enemy.CurrentMotivation}/{enemy.GetCurrentMaxMotivation()} Motivation");
                }
            });
            CombatController.Instance.OnCharacterAttack.AddListener((e) =>
            {
                Debug.Log($"{e.Attacker.Data.Name} attacked {e.Target.Data.Name} for {e.Outcome.Damage} damage. Events: {string.Join(", ", e.Outcome.Events)}");
            });
            CombatController.Instance.OnEnemyAttack.AddListener((e) =>
            {
                Debug.Log($"{e.Attacker.Data.Name} attacked {e.Target.Data.Name} for {e.Outcome.Damage} damage. Events: {string.Join(", ", e.Outcome.Events)}");
            });

        PlayerController.Instance.PartyMembers = CharacterControllers;
        CombatController.Instance.FightAgainst(EnemyControllers);
    }
}