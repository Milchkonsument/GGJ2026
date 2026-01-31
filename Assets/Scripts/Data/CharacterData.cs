using UnityEngine;

[CreateAssetMenu(fileName = "NewKid", menuName = "Scriptable Objects/Character Data")]
public class CharacterData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public AudioClip DamageClip;
    public AudioClip DeathClip;
    public AudioClip AttackClip;
    public AudioClip CritClip;
    public StatData BaseStats;
}
