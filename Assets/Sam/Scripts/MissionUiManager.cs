using UnityEngine;
using TMPro;
using System.Collections;

public class MissionUIManager : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public GameObject button;

    public void EnableButton(bool enable)
    {
        if (button != null)
        {
            button.SetActive(enable);
        }
    }

    public void UpdateButtonText(string text)
    {
        if (buttonText != null)
        {
            buttonText.text = text;
        }
    }

    public IEnumerator DisableButtonAndTextWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableButton(false);
        UpdateButtonText("");
    }

    public void ShowNextMissionText()
    {
        UpdateButtonText("Go to next mission");
    }
}
