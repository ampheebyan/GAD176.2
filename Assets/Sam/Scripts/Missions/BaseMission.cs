using UnityEngine;

public class BaseMission : MonoBehaviour
{
    [SerializeField] private MissionData missionData;
    private bool isCompleted = false;

    public string MissionName => missionData != null ? missionData.missionName : "Unnamed Mission";

    public void CompleteMission()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log($"Completing mission: {MissionName}");
            
            var missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null)
            {
                missionManager.MissionCompleted(this);
            }
        }
    }

    public bool IsCompleted() => isCompleted;
}
