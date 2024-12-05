using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detection
{
    public class DetectionMiddleman : MonoBehaviour
    {
        public DetectionBase[] detectionTypes;
        public bool currentDetection = false;

        public float detectionTimeout = 5f;

        private float _detectionTimeoutTimer = 0f;
        
        private void OnEnable()
        {
            foreach (DetectionBase detectionType in detectionTypes)
            {
                detectionType.OnDetected += PlayerDetectionHandler;
            }
        }

        private void Update()
        {
            if (currentDetection)
            {
                if (_detectionTimeoutTimer >= detectionTimeout)
                {
                    Debug.Log($"DetectionMiddleman: timed out detection in {detectionTimeout} (full game time: {Time.time}).");
                    _detectionTimeoutTimer = 0f;
                    currentDetection = false;
                    return;
                }
                _detectionTimeoutTimer += Time.deltaTime;
            }
        }

        private void PlayerDetectionHandler(object sender, DetectionBase.DetectionEventData data)
        {
            Debug.Log($"DetectionMiddleman: detected: {(data.player ? "player" : "no player" )}, {(data.position ? "position" : "no position" )}.");
            currentDetection = true;
            _detectionTimeoutTimer = 0f;
        }
    }
    
}
