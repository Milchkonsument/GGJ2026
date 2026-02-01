using UnityEngine;

[CreateAssetMenu(fileName = "NewHouse", menuName = "Scriptable Objects/House Data")]
class HouseData : ScriptableObject
{
    public string HouseName;
    public EnemyData[] Enemies;
}
