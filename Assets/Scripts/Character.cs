using UnityEngine;

[CreateAssetMenu(fileName = "NewKid", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string CharacterName;
    public Sprite Sprite;
    public AudioClip voiceClip;
    public Sprite MaskSprite; // eigener Type
    public int MaxMotivation;
    public int AttackPower;
    public float MotivationGainPerCandy;
    public float MotivationLossPerHit;
    public int BaseCritRate;
    public int LootChance;
    public string Class; //eigener Type
}
