using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phoebe Faith

///<summary>
/// Middleman between all detection types, so can mix them together.
/// </summary>

namespace Detection
{
    public class DetectionMiddleman : MonoBehaviour
    {
        
        public delegate void DetectedEventHandler(object sender, DetectionBase.DetectionEventData data);
        public event DetectedEventHandler Detected;

        public Action DetectedClear;
        
        public DetectionBase[] detectionTypes;
        public bool currentDetection = false;
        public float detectionTimeout = 5f;
        private float _detectionTimeoutTimer = 0f;
        private void OnEnable()
        {
            foreach (DetectionBase detectionType in detectionTypes)
            {
                detectionType.OnDetected += PlayerDetectionHandler; // Hook into detection type.
            }
        }
        private void OnDisable()
        {
            foreach (DetectionBase detectionType in detectionTypes)
            {
                detectionType.OnDetected -= PlayerDetectionHandler; // Unhook from detection type.
            }
        }
        private void Update()
        {
            if (currentDetection) // If there is an active detection:
            {
                if (_detectionTimeoutTimer >= detectionTimeout) // Has the timer reached the timeout?
                {
                    if(GlobalReference.isDebugLog) Debug.Log($"DetectionMiddleman: timed out detection in {detectionTimeout} (full game time: {Time.time}).");
                    _detectionTimeoutTimer = 0f; // Set to 0
                    currentDetection = false; // Clear detection
                    DetectedClear?.Invoke(); // Event to clear detection
                    return;
                }
                _detectionTimeoutTimer += Time.deltaTime; // Increment timer.
            }
        }

        private void PlayerDetectionHandler(object sender, DetectionBase.DetectionEventData data)
        {
            if(GlobalReference.isDebugLog) Debug.Log($"DetectionMiddleman: detected: {(data.player ? "player" : "no player" )}, {(data.position ? "position" : "no position" )}.");
            Detected?.Invoke(this, data); // A detection was found, so send out that there was a detection.
            currentDetection = true;
            _detectionTimeoutTimer = 0f;
        }
    }
    
}
