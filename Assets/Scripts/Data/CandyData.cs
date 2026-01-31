using UnityEngine;

[CreateAssetMenu(fileName = "NewCandy", menuName = "Scriptable Objects/Candy Data")]
public class CandyData : ScriptableObject {
    public Sprite Sprite;
    [Range(1, 10)]
    public int MotivationGain;
    public CandyRarity Rarity;
}
