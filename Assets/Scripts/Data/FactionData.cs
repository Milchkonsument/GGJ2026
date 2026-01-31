using UnityEngine;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Scriptable Objects/Faction Data")]
public class FactionData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public BuffData SecondUnitBuff;
    public BuffData FourthUnitBuff;
    public BuffData SixthUnitBuff;
}
