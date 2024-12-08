using UnityEngine;

// Phoebe Faith

/// <summary>
/// Exists to set PlayerType to enemy.
/// </summary>

namespace Characters.EnemyPlayer
{
    public class EnemyPlayer : BasePlayer
    {
        private void Awake()
        {
            // Set playerType to enemy
            this.playerType = PlayerTypes.enemy;
        }
    }
}