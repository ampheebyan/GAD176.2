using UnityEngine;
using TMPro;

public class MissionUIManager : MonoBehaviour
{
    public TextMeshProUGUI missionPrompt;

    public void UpdateMissionPrompt(string text)
    {
        if (missionPrompt != null)
        {
            missionPrompt.text = text;
        }
    }

    public void ClearMissionPrompt()
    {
        if (missionPrompt != null)
        {
            missionPrompt.text = "";
        }
    }
}
