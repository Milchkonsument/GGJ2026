using UnityEngine;

[CreateAssetMenu(fileName = "NewTrait", menuName = "Scriptable Objects/Trait Data")]
public class TraitData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public BuffData BuffTwoUnits;
    public BuffData BuffThreeUnits;
}
