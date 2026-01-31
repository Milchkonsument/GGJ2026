using UnityEngine;

[CreateAssetMenu(fileName = "NewStat", menuName = "Scriptable Objects/Stat Data")]
public class StatData : ScriptableObject
{
    [Range(20, 100)]
    public int BaseMotivation;
    [Range(1, 20)]
    public int BaseAttack;
    [Range(0f, 1f)]
    public float BaseCritRate;
    [Range(0f, 1f)]
    public float BaseDodgeRate;
}
