using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phoebe Faith (1033478)

/// <summary>
/// Base of all detection classes.
/// </summary>

public class DetectionBase : MonoBehaviour
{
    public delegate void DetectedEventHandler(object sender, BasePlayer detected);
    public event DetectedEventHandler OnDetected;
    public float detectionRadius;
    public float detectionLockTime = 5f;
    
    private bool _detectionLock;
    private float _detectionLockTimer = 0;


    private void Update()
    {
        if (_detectionLock)
        {
            if (_detectionLockTimer >= detectionLockTime)
            {
                Debug.Log($"DetectionBase: unlocked detection lock at {_detectionLockTimer} (full game time: {Time.time}).");
                _detectionLock = false;
                _detectionLockTimer = 0f;
                return;
            }
            _detectionLockTimer += Time.deltaTime;
        }
    }

    protected void OnDetection(BasePlayer detected)
    {
        _detectionLockTimer = 0f;
        if (_detectionLock) return;
        Debug.Log($"DetectionBase: detected {detected.name}");
        // Can't call events from derived classes, so call this.
        OnDetected?.Invoke(this, detected);
        _detectionLock = true;
    }
}
