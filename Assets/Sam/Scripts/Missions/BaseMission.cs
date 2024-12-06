/// <summary>
/// Foundation for mission scripts
///  Tracks completion and integrates with the MissionManager.
/// </summary>
using UnityEngine;

public class BaseMission : MonoBehaviour
{
    [SerializeField] private MissionData missionData;
    private bool isCompleted = false;

    /// <summary>
    /// Name of the mission as defined in the associated MissionData ScriptableObject.
    /// </summary>
    public string MissionName => missionData != null ? missionData.missionName : "Unnamed Mission";

    /// <summary>
    /// Marks the mission as completed and notifies the MissionManager.
    /// </summary>
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

    /// <summary>
    /// Checks whether the mission is completed.
    /// </summary>
    public bool IsCompleted() => isCompleted;
}
