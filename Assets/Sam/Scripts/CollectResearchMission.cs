using UnityEngine;

public class CollectResearchMission : BaseMission
{
    private void Start()
    {
        missionName = "Collect Research";
        Debug.Log($"Mission '{missionName}' started.");
    }

    public override void CompleteMission()
    {
        base.CompleteMission();
    }
}
