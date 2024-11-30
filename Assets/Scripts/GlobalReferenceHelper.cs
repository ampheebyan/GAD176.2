using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phoebe Faith (1033478)

/// <summary>
/// Handles moving things to be referenced globally into DDOL.
/// </summary>

public class GlobalReferenceHelper : MonoBehaviour
{
    private void OnEnable()
    {
        if (GlobalReference.ddolMutexes.Contains(gameObject.name)) {
            throw new Exception($"Mutex \"{gameObject.name}\" already exists in ddolMutexes.");
        }

        DontDestroyOnLoad(this.gameObject);
        Debug.Log($"Moved ${gameObject.name} to DontDestroyOnLoad.");
        GlobalReference.ddolMutexes.Add(gameObject.name);
        Debug.Log($"Added \"${gameObject.name}\" to ddolMutexes.");
        Destroy(this);
    }
}
