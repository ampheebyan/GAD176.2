using System;
using UnityEngine;

// Phoebe Faith

/// <summary>
/// Exists to set PlayerType to npc.
/// </summary>
namespace Characters.StandardNonPlayer
{
    public class StandardNonPlayer : BasePlayer
    {
        private void Awake()
        {
            // Set playerType to NPC
            this.playerType = PlayerTypes.npc;
        }
    }
}