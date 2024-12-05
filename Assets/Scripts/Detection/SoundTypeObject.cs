using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Detection.Sound
{
    [CreateAssetMenu(fileName = "Data", menuName = "SoundEmitter/SoundType", order = 1)]
    public class SoundTypeObject : ScriptableObject
    {
        [Tooltip("Turn this off if you don't want it to pass through walls. The map will need to be on another layer.")]
        public bool canPassThroughWalls = false;
        public LayerMask ignoreLayers;
        
        // This is just the radius.
        public float soundVolume = 1f;
        public float soundRadius = 2.5f;
    }
}
