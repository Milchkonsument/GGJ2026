using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Scriptable Objects/Trait Data")]
public class TraitData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public BuffData BuffTwoUnits;
    public BuffData BuffThreeUnits;

    public BuffData GetBuffDataForUnitCount(int unitCount)
    {
        if (unitCount >= 3)
            return BuffThreeUnits;
        if (unitCount == 2)
            return BuffTwoUnits;
        return null;
    }
}
