using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionStatusText; // Displays mission statuses
    [SerializeField] private TextMeshProUGUI missionPromptText; // Displays mission prompts
    [SerializeField] private TextMeshProUGUI winInstructionsText; // Displays win-related instructions

    private Dictionary<BaseMission, string> missionStatuses = new Dictionary<BaseMission, string>();

    public void InitializeMissionList(List<BaseMission> missions)
    {
        missionStatuses.Clear();

        foreach (var mission in missions)
        {
            missionStatuses[mission] = "Not Done";
        }

        UpdateMissionListUI();

        // Set initial win instructions
        if (winInstructionsText != null)
        {
            winInstructionsText.text = "Complete all missions and go back to the start to win!";
        }
    }

    public void UpdateMissionStatus(BaseMission completedMission)
    {
        if (missionStatuses.ContainsKey(completedMission))
        {
            missionStatuses[completedMission] = "Done";
            UpdateMissionListUI();
        }
    }

    private void UpdateMissionListUI()
    {
        if (missionStatusText == null) return;

        string missionList = "";
        foreach (var mission in missionStatuses)
        {
            missionList += $"{mission.Key.MissionName}: {mission.Value}\n";
        }
        missionStatusText.text = missionList; // Update the mission status UI
    }

    public void UpdateMissionPrompt(string prompt)
    {
        if (missionPromptText != null)
        {
            missionPromptText.text = prompt; // Update the mission prompt UI
        }
    }

    public void ClearMissionPrompt()
    {
        if (missionPromptText != null)
        {
            missionPromptText.text = ""; // Clear mission prompt UI
        }
    }

    public void UpdateWinInstructions(string message)
    {
        if (winInstructionsText != null)
        {
            winInstructionsText.text = message; // Update win instructions UI
        }
    }
}
