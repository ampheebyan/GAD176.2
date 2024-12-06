using UnityEngine;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the win zone.");

            // Find the MissionManager in the scene
            var missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null && missionManager.AreAllMissionsCompleted())
            {
                Debug.Log("You have completed all missions. Congratulations!");

                // Notify the player they have won
                var uiManager = FindObjectOfType<MissionUIManager>();
                if (uiManager != null)
                {
                    uiManager.UpdateWinZoneMessage("You Win! Thanks for playing!");
                }

                // Exit the game
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
                Application.Quit(); // Quit the application
#endif
            }
            else
            {
                Debug.Log("You cannot finish yet! Complete all missions first.");

                // Update the win zone message to notify the player
                var uiManager = FindObjectOfType<MissionUIManager>();
                if (uiManager != null)
                {
                    uiManager.UpdateWinZoneMessage("Complete all missions before accessing the win zone!");
                }
            }
        }
    }
}
