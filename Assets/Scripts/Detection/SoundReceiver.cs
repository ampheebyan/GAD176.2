using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Phoebe Faith

///<summary>
/// Contains logic for receiving the "sound" from SoundEmitter.cs
/// </summary>
namespace Detection.Sound
{
    public class SoundReceiver : DetectionBase
    {
        public float volumeThreshold = 0.5f;

        private void OnEnable()
        {
            this.type = DetectionType.position;
        }

        public void HeardSound(Transform position)
        {
            if(GlobalReference.isDebugLog) Debug.Log("SoundReceiver: Heard sound");
            OnDetection(null, position); // Trigger DetectionBase's onDetection
        }
    }
}
