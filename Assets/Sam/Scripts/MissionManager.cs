using UnityEngine;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public List<BaseMission> missions = new List<BaseMission>(); // List of all missions
    public GameObject winZone; // Win zone GameObject
    private int completedMissions = 0; // Counter for completed missions
    private int requiredMissionsToWin = 2; // Number of missions required to activate the win zone

    private MissionUIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<MissionUIManager>();

        if (uiManager != null)
        {
            uiManager.UpdateMissionStatus("Complete all missions.");
            uiManager.UpdateWinZoneMessage("Complete 2 missions to unlock the win zone!");
        }

        if (winZone != null)
        {
            winZone.SetActive(false); // Ensure the win zone is initially disabled
        }
    }

    public void MissionCompleted()
    {
        completedMissions++;
        Debug.Log($"Mission completed! Total completed missions: {completedMissions}/{requiredMissionsToWin}");
        
        // Update the UI
        UpdateMissionUI();

        // Check if all required missions are completed
        if (completedMissions >= requiredMissionsToWin)
        {
            ActivateWinZone();
        }
    }

    private void UpdateMissionUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateMissionStatus($"Missions completed: {completedMissions}/{requiredMissionsToWin}");
        }
        else
        {
            Debug.LogWarning("MissionUIManager not found. UI updates will not work.");
        }
    }

    private void ActivateWinZone()
    {
        if (winZone != null)
        {
            winZone.SetActive(true);
        }

        if (uiManager != null)
        {
            uiManager.UpdateWinZoneMessage("Go back to the start to win!");
        }

        Debug.Log("Win zone activated!");
    }

    public bool CompletedAllMissions()
    {
        return completedMissions >= requiredMissionsToWin;
    }
}
