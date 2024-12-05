using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

// Phoebe Faith

///<summary>
/// Contains logic for detection of players visually.
/// </summary>

namespace Detection.Vision
{
    public class VisionDetection : DetectionBase
    {
        [SerializeField]
        private Transform raycastOrigin;
        [SerializeField]
        private Transform eyePosition;
        private List<GameObject> ignoredObjects = new List<GameObject>();
        private Collider[] previous;
        private void OnEnable()
        {
            this.type = DetectionType.player;
        }

        public void FixedUpdate()
        {
            Collider[] sphereRaycast = Physics.OverlapSphere(eyePosition.position, detectionRadius); // Look for all colliders in a radius

            foreach (var rObj in sphereRaycast) // Iterate through them
            {
                if (previous != null) // Is there a previous array?
                {
                    if (Enumerable.SequenceEqual(previous, sphereRaycast)) // Check if the previous array and the current array are identical
                    {
                        break; // If so, stop.
                    };
                }
                if (rObj.gameObject == gameObject) continue; // If the currently iterated object is the same as the calling object, stop.
                if (ignoredObjects.Contains(rObj.gameObject)) continue; // Skip ignored objects.
                if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} hit, checking for BasePlayer."); 
                if (rObj.TryGetComponent<BasePlayer>(out BasePlayer player)) // Find basePlayer
                {
                    if (!player.IsDetectable) // Find if they can be detected
                    {
                        if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} has BasePlayer, but is not listed as detectable.");
                        continue;
                    }
                    if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} has BasePlayer, checking line of sight."); 
                
                    if (Physics.Linecast(raycastOrigin.transform.position, rObj.transform.position, out RaycastHit hit)) // Check line of sight
                    {
                        Debug.DrawLine(raycastOrigin.transform.position, hit.collider.transform.position, Color.red, 5f); // Draw lines.
                        if (hit.collider.gameObject == player.gameObject) // If it is the same as the player detected, is found.
                        {
                            OnDetection(player); // If BasePlayer is found, call event.
                            if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} in line of sight.");
                        }
                        else // Not found.
                        {
                            if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} not in line of sight.");
                        }
                    }
                }
                else
                {
                    if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} does not have a class derived from BasePlayer.");
                    ignoredObjects.Add(rObj.gameObject);
                }
            }
            previous = sphereRaycast; // Set previous.
        }
        private void OnDrawGizmos()
        {
            if(eyePosition) Gizmos.DrawWireSphere(eyePosition.position, detectionRadius);
        }
    }
    
}
