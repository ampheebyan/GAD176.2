using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisionDetection : DetectionBase
{
    [SerializeField]
    private Transform raycastOrigin;

    private List<GameObject> ignoredObjects = new List<GameObject>();
    public void FixedUpdate()
    {
        Collider[] sphereRaycast = Physics.OverlapSphere(transform.position + transform.forward * 1.2f, detectionRadius);
        
        foreach (var rObj in sphereRaycast)
        {
            if (rObj.gameObject == gameObject) continue;
            if (ignoredObjects.Contains(rObj.gameObject)) continue;
            Debug.Log($"VisionDetection: {rObj.name} hit, checking for BasePlayer.");
            if (rObj.TryGetComponent<BasePlayer>(out BasePlayer player))
            {
                if (!player.IsDetectable)
                {
                    Debug.Log($"VisionDetection: {rObj.name} has BasePlayer, but is not listed as detectable.");
                    continue;
                }
                Debug.Log($"VisionDetection: {rObj.name} has BasePlayer, checking line of sight.");
            
                if (Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, out RaycastHit hit, detectionRadius * 2))
                {
                    Debug.DrawRay(raycastOrigin.transform.position, raycastOrigin.transform.forward * hit.distance, Color.red, 5f);
                    if (hit.collider.gameObject == player.gameObject)
                    {
                        OnDetection(player); // If BasePlayer is found, call event.
                        Debug.Log($"VisionDetection: {rObj.name} in line of sight.");
                    }
                    else
                    {
                        Debug.Log($"VisionDetection: {rObj.name} not in line of sight.");
                    }
                }
            }
            else
            {
                Debug.Log($"VisionDetection: {rObj.name} does not have a class derived from BasePlayer.");
                ignoredObjects.Add(rObj.gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1.2f, detectionRadius);
    }
}
