using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HouseUIManager : Singleton<HouseUIManager>
{
    [Header("UI Panels")]
    [SerializeField] private GameObject houseInfoPanel;
    [SerializeField] private GameObject planningPanel;
    [SerializeField] private GameObject raidPanel;
    [SerializeField] private GameObject endPanel;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text houseNameText;
    [SerializeField] private Image houseSprite;
    [SerializeField] private Transform enemiesContainer;

    [Header("House Sprites")]
    [SerializeField] private List<Sprite> houseSprites;

    public static event Action OnPlanningStarted;

    private void Awake()
    {
        //subcribe to event
    }

    private void OnEnable()
    {
        CombatController.Instance.OnCombatEnd.AddListener(_ => 
        {
            planningPanel.SetActive(false);
            raidPanel.SetActive(false);
            endPanel.SetActive(true);
        });
    }

    private void OnDisable()
    {
        CombatController.Instance.OnCombatEnd.RemoveAllListeners();
    }

    private void Start()
    {
        TMP_Text[] enemyTexts = enemiesContainer.GetComponentsInChildren<TMP_Text>();
        UpdateHouseUI();
    }   

    public void UpdateHouseUI()
    {
        HouseData houseData = GameController.Instance.GetCurrentHouse();
        houseNameText.text = houseData.HouseName;
        houseSprite.sprite = houseSprites[UnityEngine.Random.Range(0, houseSprites.Count)];

        for (int i = 0; i < enemiesContainer.childCount; i++)
        {
            TMP_Text enemyText = enemiesContainer.GetChild(i).GetComponent<TMP_Text>();
            if (i < houseData.Enemies.Length)
            {
                enemyText.text = houseData.Enemies[i].Data.Name;
                enemyText.gameObject.SetActive(true);
            }
            else
            {
                enemyText.gameObject.SetActive(false);
            }
        }

    }

    public void StartPlanning()
    {
        Debug.Log("Planning Started");
        OnPlanningStarted?.Invoke();
        houseInfoPanel.SetActive(false);
        planningPanel.SetActive(true);
    }

    public void StartRaid()
    {
        planningPanel.SetActive(false);
        CombatController.Instance.FightAgainst(GameController.Instance.GetCurrentHouse().Enemies.Select(e => Instantiate(e)).ToList());
        raidPanel.SetActive(true);
    }
}
