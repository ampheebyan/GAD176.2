using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// Phoebe Faith

///<summary>
/// Contains logic relating to "sound" emission
/// </summary>
namespace Detection.Sound
{
    public class SoundEmitter : MonoBehaviour
    {
        public SoundTypeObject soundType;
        private Collider[] previous;

        public void TriggerSound()
        {
            
            if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: trigger sound.");
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, soundType.soundRadius); // Find all colliders in a radius
            foreach (Collider collider in colliders) // Iterate through colliders
            {
                if (previous != null) // If there is a previous array
                {
                    if (Enumerable.SequenceEqual(previous, colliders)/* Check if they're identical*/)
                    {
                        break; // If they are, stop this.
                    };
                }
                if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: {collider.name}: hit");

                if (collider.TryGetComponent<SoundReceiver>(out SoundReceiver soundReceiver)) // Look for a sound receiver
                {

                    if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: {collider.name}: found receiver.");
                    
                    if (Physics.Linecast(transform.position, collider.transform.position, out RaycastHit hit)) // Check if there's an object in the way
                    {
                        if(GlobalReference.isDebugLog) Debug.DrawLine(transform.position, hit.point, Color.red, 2f);
                        if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: hit. {hit.collider.name}");
                        
                        if (hit.collider.gameObject == collider.gameObject || soundType.canPassThroughWalls) // If it's the object, there isn't anything in the way, or if it can pass through walls
                        {
                            if (soundReceiver.volumeThreshold >= soundType.soundVolume) continue; // Is it above the threshold to alert this?
                            if(GlobalReference.isDebug) Debug.Log($"SoundEmitter: {collider.name}: heard sound.");
                            soundReceiver.HeardSound(transform); // Trigger heard
                        }
                        else 
                        {
                            if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: {collider.name}: blocked by {hit.collider.name}.");
                        }
                    }
                }
            }

            previous = colliders;
        }

        private void OnDrawGizmos()
        {
            if (soundType != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(transform.position, soundType.soundRadius);
            }
        }
    }
}
