using UnityEngine;

[CreateAssetMenu(fileName = "NewDrop", menuName = "Scriptable Objects/Drop Data")]
public class DropData : ScriptableObject
{
    public CandyData Candy;
    public float DropChance;
}
