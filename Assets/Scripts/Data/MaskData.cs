using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMask", menuName = "Scriptable Objects/Mask Data")]
public class MaskData : ScriptableObject
{
    public Sprite Sprite;
    public FactionData Faction;
    public List<TraitData> Traits; 
}
