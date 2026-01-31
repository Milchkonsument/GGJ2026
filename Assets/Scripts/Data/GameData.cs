using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData", menuName = "Scriptable Objects/Game Data")]
class GameData : ScriptableObject
{
    public StageData[] Stages;
}
