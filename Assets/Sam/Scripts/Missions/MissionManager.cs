using UnityEngine;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public List<BaseMission> missions = new List<BaseMission>();
    public GameObject winZone;

    private MissionUIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<MissionUIManager>();

        // Initialize the UI with all missions and their statuses
        if (uiManager != null)
        {
            uiManager.InitializeMissionList(missions);
        }

        if (winZone != null)
        {
            winZone.SetActive(false);
        }
    }

    public void MissionCompleted(BaseMission completedMission)
    {
        Debug.Log($"Mission '{completedMission.MissionName}' completed!");

        // Notify the UI manager to update the mission status
        if (uiManager != null)
        {
            uiManager.UpdateMissionStatus(completedMission);
        }

        // Check if all missions are completed
        if (AreAllMissionsCompleted())
        {
            ActivateWinZone();
        }
    }

    private void ActivateWinZone()
    {
        if (winZone != null)
        {
            winZone.SetActive(true);
        }
        uiManager?.UpdateWinZoneMessage("Go to the win zone to complete the game!");
        Debug.Log("Win zone activated!");
    }

    public bool AreAllMissionsCompleted()
    {
        foreach (var mission in missions)
        {
            if (!mission.IsCompleted())
            {
                return false;
            }
        }
        return true;
    }
}
