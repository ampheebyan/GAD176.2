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
        public float detectionLockTime = 5f;
        
        private bool _detectionLock;
        private float _detectionLockTimer = 0;

        public DetectionType type;

        private void Update()
        {
            /*if (_detectionLock)
            {
                if (_detectionLockTimer >= detectionLockTime)
                {
                    Debug.Log($"DetectionBase: unlocked detection lock at {_detectionLockTimer} (full game time: {Time.time}).");
                    _detectionLock = false;
                    _detectionLockTimer = 0f;
                    return;
                }
                _detectionLockTimer += Time.deltaTime;
            }*/
            // Opted to keep this code here, but I'm going to leave this timeout to DetectionMiddleman. This shouldn't go here I think.
        }

        protected void OnDetection(BasePlayer detectedPlayer = null, Transform detectionPosition = null)
        {
            if (detectionPosition == null && detectedPlayer == null) throw new Exception("No detection data.");
            /*_detectionLockTimer = 0f;
            if (_detectionLock) return;*/
            DetectionEventData data = new DetectionEventData();
            Debug.Log($"DetectionBase: detected.");
            if(detectedPlayer) data.player = detectedPlayer;
            if(detectionPosition) data.position = detectionPosition;
            // Can't call events from derived classes, so call this.
            OnDetected?.Invoke(this, data);
            //_detectionLock = true;
        }
    }
    
}
