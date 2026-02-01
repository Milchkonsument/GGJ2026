using System.Collections.Generic;
using UnityEngine;

public class CombatOverlayUI : MonoBehaviour
{
    [SerializeField] private RectTransform CharacterPanelContainer;
    [SerializeField] private RectTransform EnemyPanelContainer;
    
    private readonly List<CombatPanel> characterPanels = new();
    private readonly List<CombatPanel> enemyPanels = new();

    void OnEnable()
    {
        InstantiateCombatPanels();

        CombatController.Instance.OnCharacterAttack.AddListener(OnCharacterAttack);
        CombatController.Instance.OnEnemyAttack.AddListener(OnEnemyAttack);
    }

    void OnDisable()
    {
        ClearCombatPanels();

        CombatController.Instance.OnCharacterAttack.RemoveListener(OnCharacterAttack);
        CombatController.Instance.OnEnemyAttack.RemoveListener(OnEnemyAttack);
    }

    void OnCharacterAttack(CharacterAttackEvent e)
    {
        var panel = characterPanels.Find(p => p.Character == e.Attacker);
        panel.PlayHitAnimation();
        var targetPanel = enemyPanels.Find(p => p.Enemy == e.Target);

        if(targetPanel != null)
        {
            targetPanel.PlayDamageAnimation(e.Outcome);
        }
    }

    void OnEnemyAttack(EnemyAttackEvent e)
    {
        var panel = enemyPanels.Find(p => p.Enemy == e.Attacker);
        panel.PlayHitAnimation();

        var targetPanel = characterPanels.Find(p => p.Character == e.Target);

        if (targetPanel != null)
        {
            targetPanel.PlayDamageAnimation(e.Outcome);
        }
    }


    void InstantiateCombatPanels()
    {
        foreach (var character in PlayerController.Instance.PartyMembers)
        {
            InstantiateCombatPanelForCharacter(character);
        }

        foreach (var enemy in CombatController.Instance.CurrentEnemies)
        {
            InstantiateCombatPanelForEnemy(enemy);
        }
    }

    void ClearCombatPanels()
    {
        foreach (var child in characterPanels)
        {
            Destroy(child.gameObject);
        }

        foreach (var child in enemyPanels)
        {
            Destroy(child.gameObject);
        }       
    }

    void InstantiateCombatPanelForCharacter(CharacterController character)
    {
        var panel = ResourceLoader.UI.CreateCombatPanel();
        panel.Character = character;
        panel.transform.SetParent(CharacterPanelContainer, false);
        characterPanels.Add(panel);
        panel.LoadUI();
    }

    void InstantiateCombatPanelForEnemy(EnemyController enemy)
    {
        var panel = ResourceLoader.UI.CreateCombatPanel();
        panel.Enemy = enemy;
        panel.transform.SetParent(EnemyPanelContainer, false);
        enemyPanels.Add(panel);
        panel.LoadUI();
    }
}
