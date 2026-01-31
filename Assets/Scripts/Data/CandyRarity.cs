using UnityEngine;

[CreateAssetMenu(fileName = "NewCandyRarity", menuName = "Scriptable Objects/Candy Rarity")]
public class CandyRarity : ScriptableObject
{
    public string RarityName;
    public Color RarityColor;
    public ParticleSystem ParticleEffect;
}
