using UnityEngine;
using TMPro;

public class MissionText : MonoBehaviour
{
    public TextMeshProUGUI missionStatusText;
    private bool missionCompleted = false;
    private string missionStatus = "Mission In Progress...";

    void Start()
    {
        UpdateMissionStatus();
    }

    void Update()
    {
        // For testing purposes, you can use this to simulate mission completion
        if (Input.GetKeyDown(KeyCode.M))
        {
            CompleteMission();
        }
    }

    public void CompleteMission()
    {
        missionCompleted = true;
        UpdateMissionStatus();
    }

    public void SetMissionStatus(string status)
    {
        missionStatus = status;
        UpdateMissionStatus();
    }

    private void UpdateMissionStatus()
    {
        if (missionCompleted)
        {
            missionStatusText.text = "Mission Complete!";
        }
        else
        {
            missionStatusText.text = missionStatus;
        }
    }
}

