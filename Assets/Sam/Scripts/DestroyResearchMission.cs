using UnityEngine;

public class DestroyResearchMission : BaseMission
{
    private void Start()
    {
        missionName = "Destroy Research";
        Debug.Log($"Mission '{missionName}' started.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CompleteMission();
        }
    }
}
