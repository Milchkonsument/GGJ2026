using UnityEngine;

[CreateAssetMenu(fileName = "NewHouse", menuName = "Scriptable Objects/House Data")]
public class HouseData : ScriptableObject
{
    public string HouseName;
    public EnemyController[] Enemies;
}
