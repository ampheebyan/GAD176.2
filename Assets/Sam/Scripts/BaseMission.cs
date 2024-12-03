using UnityEngine;

public class BaseMission : MonoBehaviour
{
    public string missionName;
    public string missionDescription;
    private bool isCompleted = false;

    public delegate void MissionUpdatedHandler();
    public event MissionUpdatedHandler OnMissionUpdated;

    public void CompleteMission()
    {
        isCompleted = true;
        OnMissionUpdated?.Invoke();
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }
}
