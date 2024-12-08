/// <summary>
/// Updates mission-related UI elements, such as mission status, prompts, and win instructions.
/// </summary>
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionStatusText; // Displays mission statuses
    [SerializeField] private TextMeshProUGUI missionPromptText; // Displays mission prompts
    [SerializeField] private TextMeshProUGUI winInstructionsText; // Displays win-related instructions

    private Dictionary<BaseMission, string> missionStatuses = new Dictionary<BaseMission, string>();

    /// <summary>
    /// Initializes the mission list UI.
    /// </summary>
    public void InitializeMissionList(List<BaseMission> missions)
    {
        missionStatuses.Clear();

        foreach (var mission in missions)
        {
            missionStatuses[mission] = "Not Done";
        }

        UpdateMissionListUI();

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
        missionStatusText.text = missionList;
    }

    public void UpdateMissionPrompt(string prompt)
    {
        if (missionPromptText != null)
        {
            missionPromptText.text = prompt;
        }
    }

    public void ClearMissionPrompt()
    {
        if (missionPromptText != null)
        {
            missionPromptText.text = "";
        }
    }

    public void UpdateWinInstructions(string message)
    {
        if (winInstructionsText != null)
        {
            winInstructionsText.text = message;
        }
    }
}
