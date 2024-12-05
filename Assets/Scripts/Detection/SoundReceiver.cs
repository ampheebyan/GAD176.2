using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("SoundReceiver: Heard sound");
            OnDetection(null, position);
        }
    }
}
