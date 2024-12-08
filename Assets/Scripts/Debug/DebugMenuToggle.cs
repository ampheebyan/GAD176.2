using System;
using UnityEngine;

// Phoebe Faith (1033478)

/// <summary>
/// Single purpose script to toggle the debug menu
/// </summary>
///

namespace PDebug
{
    public class DebugMenuToggle : MonoBehaviour
    {
        [SerializeField] private GameObject debugMenu;
        [SerializeField] private GameObject loggingWarning;

        public void EnableDebugLogging()
        {
            // can't say i didn't warn you.
            GlobalReference.isDebugLog = true;
            loggingWarning.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                // Toggle debug
                debugMenu.SetActive(!debugMenu.activeSelf);
                GlobalReference.isDebug = debugMenu.activeSelf;
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
            {
                if (GlobalReference.isDebugLog == false)
                {
                    if (!GlobalReference.isDebug) return; // Don't do anything if regular debugging isn't on.
                    loggingWarning.SetActive(true); // Show warning UI
                }
                else
                {
                    GlobalReference.isDebugLog = false; // Turn it off if it's on
                }
            }
        }
    }
}
