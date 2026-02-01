using UnityEngine;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Scriptable Objects/Faction Data")]
public class FactionData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public BuffData SecondUnitBuff;
    public BuffData FourthUnitBuff;
    public BuffData SixthUnitBuff;

    public BuffData GetBuffDataForUnitCount(int unitCount)
    {
        switch (unitCount)
        {
            case 2:
                return SecondUnitBuff;
            case 4:
                return FourthUnitBuff;
            case 6:
                return SixthUnitBuff;
            default:
                return null;
        }
    }
}
