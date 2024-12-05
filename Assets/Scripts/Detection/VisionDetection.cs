using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

namespace Detection.Vision
{
    public class VisionDetection : DetectionBase
    {
        [SerializeField]
        private Transform raycastOrigin;

        private List<GameObject> ignoredObjects = new List<GameObject>();
        private Collider[] previous;
        private void OnEnable()
        {
            this.type = DetectionType.player;
        }

        public void FixedUpdate()
        {
            Collider[] sphereRaycast = Physics.OverlapSphere(transform.position + transform.forward * 1.2f, detectionRadius);

            foreach (var rObj in sphereRaycast)
            {
                if (previous != null)
                {
                    if (Enumerable.SequenceEqual(previous, sphereRaycast))
                    {
                        break;
                    };
                }
                if (rObj.gameObject == gameObject) continue;
                if (ignoredObjects.Contains(rObj.gameObject)) continue;
                if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} hit, checking for BasePlayer.");
                if (rObj.TryGetComponent<BasePlayer>(out BasePlayer player))
                {
                    if (!player.IsDetectable)
                    {
                        if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} has BasePlayer, but is not listed as detectable.");
                        continue;
                    }
                    if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} has BasePlayer, checking line of sight.");
                
                    if (Physics.Linecast(raycastOrigin.transform.position, rObj.transform.position, out RaycastHit hit))
                    {
                        Debug.DrawLine(raycastOrigin.transform.position, hit.collider.transform.position, Color.red, 5f);
                        if (hit.collider.gameObject == player.gameObject)
                        {
                            OnDetection(player); // If BasePlayer is found, call event.
                            if(GlobalReference.isDebugLog) Debug.Log($"VisionDetection: {rObj.name} in line of sight.");
                        }
                        else
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
            previous = sphereRaycast;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + transform.forward * 1.2f, detectionRadius);
        }
    }
    
}
