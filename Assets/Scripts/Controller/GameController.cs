using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameData gameData;

    public readonly UnityEvent<HouseData> OnHouseChanged = new();
    public readonly UnityEvent<StageData> OnStageChanged = new();
    public readonly UnityEvent OnWin = new();
    public readonly UnityEvent<HouseData> OnRaidStarted = new();
    public readonly UnityEvent<List<CandyData>> OnRaidEnded = new();
    public readonly UnityEvent OnGameOver = new();

    private int _currentStageIndex = 0;
    private int _currentHouseIndex = 0;
    private bool _isBossRoom = false;

    public void AdvanceToNextHouse()
    {
        if (_isBossRoom)
        {
            _currentStageIndex++;
            _currentHouseIndex = 0;
            _isBossRoom = false;

            if (_currentStageIndex >= gameData.Stages.Length)
            {
                OnWin.Invoke();
                return;
            }

            OnStageChanged.Invoke(GetCurrentStage());
            OnHouseChanged.Invoke(GetCurrentHouse());
        }

        if(_currentHouseIndex + 1 >= GetCurrentStage().Houses.Length)
        {
            _isBossRoom = true;
        }
        else
        {
            _currentHouseIndex++;
        }

        OnHouseChanged.Invoke(GetCurrentHouse());
    }

    public void RaidHouse()
    {
        var house = GetCurrentHouse();
        OnRaidStarted.Invoke(house);
        CombatController.Instance.OnCombatEnd.AddListener(OnCombatEnd);
        CombatController.Instance.FightAgainst(house.Enemies.ToList());
        CombatController.Instance.OnCombatEnd.RemoveListener(OnCombatEnd);
    }
    
    private void OnCombatEnd(FightResult result)
    {
            if (!result.isWin)
            {
                OnGameOver.Invoke();
                return;
            }

            OnRaidEnded.Invoke(result.lootedCandies);
    }

    public StageData GetCurrentStage()
    {
        return gameData.Stages[_currentStageIndex];
    }

    public HouseData GetCurrentHouse()
    {
        Debug.Log("CurrentStageIndex: " + _currentStageIndex);
        Debug.Log("GameData: " + gameData);
        
        var stage = GetCurrentStage();
        if (_isBossRoom)
        {
            return stage.StageBoss;
        }
        return stage.Houses[_currentHouseIndex];
    }
}
