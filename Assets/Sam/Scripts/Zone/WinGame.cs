/// <summary>
/// Detects when the player enters the win zone and handles game-ending logic.
/// </summary>
using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var missionManager = FindObjectOfType<MissionManager>();
            if (missionManager != null && missionManager.AreAllMissionsCompleted())
            {
                Debug.Log("All missions complete! Ending game...");
                StartCoroutine(EndGameAfterDelay(3f));
            }
        }
    }

    /// <summary>
    /// Ends the game after a delay.
    /// </summary>
    private IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
