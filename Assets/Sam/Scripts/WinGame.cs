using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered win zone.");

            var missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null && missionManager.CompletedAllMissions())
            {
                Debug.Log("You have completed all missions. Congratulations!");

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
            else
            {
                Debug.Log("You cannot finish yet! Complete all missions first.");
            }
        }
    }
}
