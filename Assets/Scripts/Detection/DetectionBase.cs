using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;

// Phoebe Faith (1033478)

/// <summary>
/// Base of detection classes.
/// </summary>
namespace Detection
{
    public class DetectionBase : MonoBehaviour
    {
        public enum DetectionType
        {
            player,
            position
        }

        public class DetectionEventData
        {
            public BasePlayer player;
            public Transform position;
        }
        public delegate void DetectedEventHandler(object sender, DetectionEventData data);
        public event DetectedEventHandler OnDetected;
        public float detectionRadius;
        public DetectionType type;

        protected void OnDetection(BasePlayer detectedPlayer = null, Transform detectionPosition = null)
        {
            if (detectionPosition == null && detectedPlayer == null) throw new Exception("No detection data."); // If no data throw exception, there should always be at least ONE and if there isn't, something really wrong has happened.
            DetectionEventData data = new DetectionEventData(); // Create new data packet
            if(GlobalReference.isDebugLog) Debug.Log($"DetectionBase: detected."); 
            if(detectedPlayer) data.player = detectedPlayer; // Set player
            if(detectionPosition) data.position = detectionPosition; // Set position
            // Can't call events from derived classes, so call this.
            OnDetected?.Invoke(this, data); // Call event
        }
    }
    
}
