using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private BaseMission attachedMission;
    private bool playerInRange = false;
    private MissionUIManager uiManager;

    private void Start()
    {
        attachedMission = GetComponent<BaseMission>();
        uiManager = FindObjectOfType<MissionUIManager>();

        if (attachedMission == null)
        {
            Debug.LogWarning($"No BaseMission attached to {gameObject.name}");
        }

        if (uiManager == null)
        {
            Debug.LogWarning("MissionUIManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (attachedMission != null && !attachedMission.IsCompleted())
            {
                uiManager?.UpdateWinZoneMessage($"Press 'E' to {attachedMission.missionName}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            uiManager?.ClearMissionPrompt();
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
            uiManager?.ClearMissionPrompt();
        }
    }
}
