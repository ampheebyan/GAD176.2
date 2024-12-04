using UnityEngine;

public class DestroyResearchMission : BaseMission
{
    private void Start()
    {
        missionName = "Destroy Research";
        Debug.Log($"Mission '{missionName}' started.");
    }

    public override void CompleteMission()
    {
        base.CompleteMission();
    }
}
