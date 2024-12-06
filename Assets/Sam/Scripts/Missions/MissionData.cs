using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Missions/BaseMission")]
public class MissionData : ScriptableObject
{
    public string missionName;
    public string description;
    public bool isRepeatable;
}
