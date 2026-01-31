using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLocation", menuName = "Scriptable Objects/Location Data")]
public class LocationData : ScriptableObject
{
    public string Name;
    public SubLocationData[] subLocations;
}
