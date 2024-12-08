using System;
using Characters;
using UnityEngine;
// Phoebe Faith

///<summary>
/// Simple trigger type that is based on OnTriggerEnter
/// </summary>
namespace Detection
{
    public class TriggerDetectionZone : DetectionBase
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BasePlayer>(out BasePlayer player))
            { // Look for BasePlayer on object
                if (player.IsDetectable) // Is the player detectable?
                {
                    this.OnDetection(player, null); // If so, call it.
                }
            }
        }
    }
}