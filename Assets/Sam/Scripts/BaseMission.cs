using UnityEngine;

public class BaseMission : MonoBehaviour
{
    public string missionName;
    private bool isCompleted = false;

    public void CompleteMission()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log($"Mission '{missionName}' completed!");

            // Notify MissionManager
            var missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null)
            {
                missionManager.MissionCompleted();
            }
        }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }
}
