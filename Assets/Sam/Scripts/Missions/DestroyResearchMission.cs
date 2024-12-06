/// <summary>
/// A mission where the player destroys research objects to complete the task.
/// </summary>
using UnityEngine;

public class DestroyResearchMission : BaseMission
{
    private void Start()
    {
        Debug.Log($"Mission '{MissionName}' started.");
    }
}
