using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionStatusText; // Text for mission statuses
    [SerializeField] private TextMeshProUGUI winZoneMessage;    // Text for win zone message

    private Dictionary<BaseMission, string> missionStatuses = new Dictionary<BaseMission, string>();

    public void InitializeMissionList(List<BaseMission> missions)
    {
        missionStatuses.Clear();

        foreach (var mission in missions)
        {
            missionStatuses[mission] = "Not Done";
        }

        UpdateMissionListUI();
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

    public void UpdateWinZoneMessage(string message)
    {
        if (winZoneMessage != null)
        {
            winZoneMessage.text = message;
        }
    }

    public void ClearMissionPrompt()
    {
        if (winZoneMessage != null)
        {
            winZoneMessage.text = "";
        }
    }
}
