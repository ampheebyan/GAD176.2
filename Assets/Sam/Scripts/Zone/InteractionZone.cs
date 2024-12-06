using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private BaseMission attachedMission;
    private bool playerInRange = false;
    private MissionUIManager uiManager;

    private void Start()
    {
        attachedMission = GetComponentInParent<BaseMission>();
        uiManager = FindObjectOfType<MissionUIManager>();

        if (attachedMission == null)
        {
            Debug.LogError($"No BaseMission attached to {gameObject.name} or its parent.");
        }

        if (uiManager == null)
        {
            Debug.LogError("MissionUIManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (attachedMission != null && !attachedMission.IsCompleted())
            {
                uiManager?.UpdateMissionPrompt($"Press 'E' to {attachedMission.MissionName}");
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
            Debug.Log($"Interacting with mission: {attachedMission.MissionName}");
            attachedMission.CompleteMission();
            uiManager?.ClearMissionPrompt();
        }
    }
}
