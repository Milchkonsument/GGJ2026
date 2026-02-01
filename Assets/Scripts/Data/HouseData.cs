using UnityEngine;

[CreateAssetMenu(fileName = "NewHouse", menuName = "Scriptable Objects/House Data")]
public class HouseData : ScriptableObject
{
    public string HouseName;
    public EnemyData[] Enemies;
    public Difficulty HouseDifficulty;
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}
