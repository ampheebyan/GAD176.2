/// <summary>
/// A mission where the player collects research items to complete the task.
/// </summary>
using UnityEngine;

public class CollectResearchMission : BaseMission
{
    private void Start()
    {
        Debug.Log($"Mission '{MissionName}' started.");
    }
}
