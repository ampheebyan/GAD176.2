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
                var uiManager = FindObjectOfType<MissionUIManager>();
                if (uiManager != null)
                {
                    uiManager.UpdateWinInstructions("Congratulations! You Win! Thanks for playing!");
                }

                StartCoroutine(EndGameAfterDelay(3f));
            }
            else
            {
                var uiManager = FindObjectOfType<MissionUIManager>();
                if (uiManager != null)
                {
                    uiManager.UpdateWinInstructions("Complete all tasks before accessing the win zone!");
                }
            }
        }
    }

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
