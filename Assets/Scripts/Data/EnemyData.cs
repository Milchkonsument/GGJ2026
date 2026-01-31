using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Scriptable Objects/Enemy Data")]
class EnemyData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public StatData BaseStats;
    public DropData[] DropTable;
    public AudioClip DamageClip;
    public AudioClip DeathClip;
    public AudioClip AttackClip;
    public AudioClip CritClip;
}
