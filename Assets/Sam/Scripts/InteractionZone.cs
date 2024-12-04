using UnityEngine;
using TMPro;

public class InteractionZone : MonoBehaviour
{
    public TextMeshProUGUI interactionPrompt; // Text for interaction prompt
    private BaseMission attachedMission;      // The mission attached to this zone
    private bool playerInRange = false;       // Tracks if the player is in range
    private MissionManager missionManager;    // Reference to the Mission Manager

    private void Start()
    {
        attachedMission = GetComponent<BaseMission>();
        missionManager = FindObjectOfType<MissionManager>();

        if (interactionPrompt != null)
        {
            interactionPrompt.text = ""; // Clear prompt at start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactionPrompt != null)
            {
                interactionPrompt.text = $"Press 'E' to {attachedMission.missionName}";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactionPrompt != null)
            {
                interactionPrompt.text = ""; // Clear prompt when player leaves
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithMission();
        }
    }

    private void InteractWithMission()
    {
        if (attachedMission != null && !attachedMission.IsCompleted())
        {
            Debug.Log($"Interacting with mission: {attachedMission.missionName}");
            attachedMission.CompleteMission();

            if (missionManager != null)
            {
                missionManager.UpdateAllMissionStatuses();
            }

            // Clear the interaction prompt on completion
            if (interactionPrompt != null)
            {
                interactionPrompt.text = "";
            }
        }
    }
}
