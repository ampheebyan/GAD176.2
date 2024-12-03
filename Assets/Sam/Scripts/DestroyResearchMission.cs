using UnityEngine;
using System.Collections;
using TMPro;

public class DestroyResearchMission : BaseMission
{
    public float holdTime = 5.0f;
    private float holdTimer = 0.0f;
    private bool isHolding = false;
    public MissionUIManager uiButtonManager;
    public InteractionZone interactionZone;
    private MissionText missionTextScript;
    private bool missionCompleted = false; // Flag to track mission completion

    void Start()
    {
        missionName = "Destroy Research";
        missionDescription = "Destroy research by holding on an object for " + holdTime + " seconds.";
        missionTextScript = FindObjectOfType<MissionText>();
        if (missionTextScript != null)
        {
            missionTextScript.SetMissionStatus("Destroy Research: not done");
        }
        var missionManager = FindObjectOfType<MissionManager>();
        if (missionManager != null)
        {
            missionManager.AddMission(this);
        }
        uiButtonManager.EnableButton(false);
    }

    void Update()
    {
        if (!missionCompleted)
        {
            HandleHoldingLogic();
        }
    }

    private void HandleHoldingLogic()
    {
        if (interactionZone != null && interactionZone.IsPlayerInRange())
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTimer += Time.deltaTime;
                if (!isHolding)
                {
                    StartHolding();
                }

                if (holdTimer >= holdTime)
                {
                    missionCompleted = true;
                    CompleteMission();
                    StartCoroutine(DisableUIElementsWithDelay(1.5f));
                    uiButtonManager.ShowNextMissionText();
                    interactionZone.gameObject.SetActive(false); // Disable the interaction zone
                }
            }
            else
            {
                StopHolding();
                holdTimer = 0.0f;
            }
        }
        else
        {
            uiButtonManager.EnableButton(false);
        }
    }

    private void StartHolding()
    {
        isHolding = true;
        UpdateMissionUI("Hold 'E' to destroy research...");
    }

    private void StopHolding()
    {
        isHolding = false;
        UpdateMissionUI();
    }

    private void CompleteMission()
    {
        base.CompleteMission();
        missionCompleted = true; // Ensure this mission is flagged as completed
        if (missionTextScript != null)
        {
            missionTextScript.SetMissionStatus("Destroy Research: done");
        }
    }

    private IEnumerator DisableUIElementsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uiButtonManager.EnableButton(false);
    }

    void UpdateMissionUI(string message = "")
    {
        if (uiButtonManager != null)
        {
            if (isHolding)
            {
                uiButtonManager.UpdateButtonText(message);
            }
            else
            {
                uiButtonManager.UpdateButtonText("Press 'E' to interact");
            }
        }
    }
}
