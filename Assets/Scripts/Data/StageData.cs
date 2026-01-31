using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "Scriptable Objects/Stage Data")]
class StageData : ScriptableObject
{
    public string StageName;
    public Sprite BackgroundImage;
    public HouseData[] Houses;
    public HouseData StageBoss;
}
