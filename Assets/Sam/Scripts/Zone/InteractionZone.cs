/// <summary>
/// Handles player interactions within a specific zone, triggering mission-related prompts and actions.
/// </summary>
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private BaseMission attachedMission;
    private MissionUIManager uiManager;
    private bool playerInRange = false;

    private void Start()
    {
        attachedMission = GetComponentInParent<BaseMission>();
        uiManager = FindObjectOfType<MissionUIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (attachedMission != null && !attachedMission.IsCompleted())
            {
                uiManager.UpdateMissionPrompt($"Press 'E' to {attachedMission.MissionName}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            uiManager.ClearMissionPrompt();
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && attachedMission != null)
        {
            attachedMission.CompleteMission();
            uiManager.ClearMissionPrompt();
        }
    }
}
