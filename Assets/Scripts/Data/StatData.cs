using UnityEngine;

[CreateAssetMenu(fileName = "NewStat", menuName = "Scriptable Objects/Stat Data")]
public class StatData : ScriptableObject
{
    [Range(20, 100)]
    public int BaseMotivation;
    [Range(1, 20)]
    public int BaseAttack;
    [Range(0, 100)]
    public int BaseCritRatePercentage;
    [Range(0, 100)]
    public int BaseDodgeRatePercentage;
}
