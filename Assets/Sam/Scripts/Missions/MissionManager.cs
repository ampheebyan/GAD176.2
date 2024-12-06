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

        if (uiManager != null)
        {
            uiManager.UpdateMissionStatus(completedMission);
        }

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

        if (uiManager != null)
        {
            uiManager.UpdateWinInstructions("All tasks complete! Go back to the start to win!");
        }

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
