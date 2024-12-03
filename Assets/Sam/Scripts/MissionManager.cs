using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    public List<BaseMission> missions = new List<BaseMission>();
    public TextMeshProUGUI missionsStatusText;

    void Start()
    {
        UpdateAllMissionStatuses();
    }

    public void AddMission(BaseMission mission)
    {
        missions.Add(mission);
        mission.OnMissionUpdated += UpdateAllMissionStatuses; // Subscribe to mission update event
        UpdateAllMissionStatuses();
    }

    public void UpdateAllMissionStatuses()
    {
        missionsStatusText.text = "";
        foreach (var mission in missions)
        {
            string status = mission.IsCompleted() ? "done" : "not done";
            missionsStatusText.text += $"{mission.missionName}: {status}\n";
        }
    }
}
