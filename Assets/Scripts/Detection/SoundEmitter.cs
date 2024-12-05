using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detection.Sound
{
    public class SoundEmitter : MonoBehaviour
    {
        public SoundTypeObject soundType;

        public void TriggerSound()
        {
            if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: trigger sound.");
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, soundType.soundRadius);
            foreach (Collider collider in colliders)
            {
                if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: {collider.name}: hit");

                if (collider.TryGetComponent<SoundReceiver>(out SoundReceiver soundReceiver))
                {

                    if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: {collider.name}: found receiver.");
                    
                    if (Physics.Linecast(transform.position, collider.transform.position, out RaycastHit hit))
                    {
                        if(GlobalReference.isDebugLog) Debug.DrawLine(transform.position, hit.point, Color.red, 2f);
                        if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: hit. {hit.collider.name}");
                        
                        if (hit.collider.gameObject == collider.gameObject || soundType.canPassThroughWalls)
                        {
                            if (soundReceiver.volumeThreshold >= soundType.soundVolume) continue;
                            if(GlobalReference.isDebug) Debug.Log($"SoundEmitter: {collider.name}: heard sound.");
                            soundReceiver.HeardSound(transform);
                        }
                        else
                        {
                            if(GlobalReference.isDebugLog) Debug.Log($"SoundEmitter: {collider.name}: blocked by {hit.collider.name}.");
                        }
                    }
                }
            }
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
