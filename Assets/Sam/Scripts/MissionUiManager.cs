using UnityEngine;
using TMPro;

public class MissionUIManager : MonoBehaviour
{
    public TextMeshProUGUI missionStatusText; // Text for mission status
    public TextMeshProUGUI winZoneMessage;    // Text for win zone message

    public void UpdateMissionStatus(string status)
    {
        if (missionStatusText != null)
        {
            missionStatusText.text = status;
        }
        else
        {
            Debug.LogWarning("Mission status text not assigned.");
        }
    }

    public void UpdateWinZoneMessage(string message)
    {
        if (winZoneMessage != null)
        {
            winZoneMessage.text = message;
        }
        else
        {
            Debug.LogWarning("Win zone message text not assigned.");
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
