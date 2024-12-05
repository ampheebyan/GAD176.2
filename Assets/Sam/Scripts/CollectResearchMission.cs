using UnityEngine;

public class CollectResearchMission : BaseMission
{
    private void Start()
    {
        missionName = "Collect Research";
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
