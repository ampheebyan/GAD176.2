using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public MissionUIManager uiButtonManager;
    public string interactMessage = "Press 'E' to interact";
    private bool isPlayerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sam_PlayeMovement>() != null)
        {
            isPlayerInRange = true;
            uiButtonManager.EnableButton(true);
            uiButtonManager.UpdateButtonText(interactMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Sam_PlayeMovement>() != null)
        {
            isPlayerInRange = false;
            uiButtonManager.EnableButton(false);
        }
    }

    public bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }
}
