using UnityEngine;

public class BaseMission : MonoBehaviour
{
    public string missionName;
    private bool isCompleted = false;

    public virtual void CompleteMission()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log($"Mission '{missionName}' completed!");

            // Notify WinGame
            FindObjectOfType<WinGame>()?.MissionCompleted();
        }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }
}
