using UnityEngine;
using TMPro;

public class WinGame : MonoBehaviour
{
    [Header("Win Zone Settings")]
    public GameObject winZone; // The win zone GameObject

    [Header("UI Elements")]
    public TextMeshProUGUI statusMessage; // Text element to display status messages

    [Header("Mission Management")]
    public int requiredMissionsToWin = 2; // Number of missions required to activate the win zone
    private int completedMissions = 0; // Counter for completed missions

    private bool winZoneActivated = false; // Flag to ensure the win zone is only activated once

    private void Start()
    {
        // Disable the win zone initially
        if (winZone != null)
        {
            winZone.SetActive(false);
        }

        UpdateStatusMessage("Complete 2 missions to unlock the win zone!");
    }

    public void MissionCompleted()
    {
        completedMissions++; // Increment the completed missions counter
        Debug.Log($"Mission completed! Total completed: {completedMissions}/{requiredMissionsToWin}");

        if (completedMissions >= requiredMissionsToWin && !winZoneActivated)
        {
            ActivateWinZone();
        }
    }

    private void ActivateWinZone()
    {
        winZoneActivated = true;

        // Enable the win zone
        if (winZone != null)
        {
            winZone.SetActive(true);
        }

        // Update the status message
        UpdateStatusMessage("Go back to the start to win!");
    }

    private void UpdateStatusMessage(string message)
    {
        if (statusMessage != null)
        {
            statusMessage.text = message;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (completedMissions >= requiredMissionsToWin)
            {
                Debug.Log("You have completed all missions. Congratulations!");

                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            }
            else
            {
                Debug.Log("You cannot finish yet! Complete all missions first.");
            }
        }
    }
}
