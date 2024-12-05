using System;
using Movement;
using UnityEngine;
// Phoebe Faith (1033478)

/// <summary>
/// Contains all logic specific to _only_ the local player.
/// This should only ever be in a scene ONCE and only for the actual person playing.
/// </summary>


namespace Characters.LocalPlayer
{
    public class LocalPlayer : BasePlayer
    {
        protected void OnEnable()
        {
            // Set playerType to local, and check if LocalPlayer is improperly used.
            this.playerType = PlayerTypes.local;
            if (TryGetComponent<MovementHandler>(out MovementHandler movementHandler))
            {
                // Failsafe in the event that localPlayer is put on something with no locally controlled movement.
                if(GlobalReference.localPlayer == null) GlobalReference.localPlayer = this; 
            }
            else
            {
                this.enabled = false;
                throw new Exception("LocalPlayer is on something without locally controlled movement. Disabling LocalPlayer. It should only be in a scene once, and only for the locally controlling player.");
            }
        }

        private void OnDisable()
        {
            if(GlobalReference.localPlayer == this) GlobalReference.localPlayer = null; // Unassign it but only if it's this.
        }
    }
}
