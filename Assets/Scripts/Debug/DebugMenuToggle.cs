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

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                debugMenu.SetActive(!debugMenu.activeSelf);
                GlobalReference.isDebug = debugMenu.activeSelf;
            }
        }
    }
}
