using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public List<BaseMission> missions = new List<BaseMission>(); // Predefined missions
    public TextMeshProUGUI missionStatusDisplay; // Text for mission statuses

    private void Start()
    {
        UpdateAllMissionStatuses();
    }

    public void UpdateAllMissionStatuses()
    {
        if (missionStatusDisplay == null) return;

        missionStatusDisplay.text = ""; // Clear the display

        foreach (var mission in missions)
        {
            string status = mission.IsCompleted() ? "Done" : "In Progress";
            missionStatusDisplay.text += $"{mission.missionName}: {status}\n";
        }
    }

    public void AddMission(BaseMission mission)
    {
        if (!missions.Contains(mission))
        {
            missions.Add(mission);
            UpdateAllMissionStatuses();
        }
    }
    public bool AreAllMissionsComplete()
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
