/// <summary>
/// A ScriptableObject that holds mission-specific data, such as the mission's name, description, and repeatability.
/// </summary>
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionData", menuName = "Mission Data")]
public class MissionData : ScriptableObject
{
    public string missionName; // The name of the mission
    public string description; // A brief description of the mission
    public bool isRepeatable; // Determines if the mission can be repeated
}
