using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Scriptable Objects/Event Data")]
public class EventData : ScriptableObject
{
    public string Name;
    public enum EventType { Positive, Negative, Neutral }
    public EventType Type;
    public string Message;
    public int motivationChange;
    public int candyChange;
    public bool maskFound;
    public int chanceWeight;
    public AudioClip eventSound;
}
