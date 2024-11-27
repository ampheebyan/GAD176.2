using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phoebe Faith (1033478)
/// <summary>
/// Holds all shared functions between all "player"/"character" types (eg. EnemyPlayer, LocalPlayer, and StandardNonPlayer)
/// Contains handling for health/death.
///
/// Used to derive EnemyPlayer, LocalPlayer, and StandardNonPlayer.
/// </summary>
public class BasePlayer : MonoBehaviour
{
    [Header("BasePlayer")] 
        [Header("Health")]
        [Tooltip("x = current, y = maximum.")]

    [SerializeField] private Vector2 health = new Vector2(100, 100); // x = current, y = maximum. easily assumed that x has a minimum of 0.
    [SerializeField] private bool invulnerable = false; // This will allow us to just make it so calls to health functions are pretty much ignored. For test dummies, and the like.

    public event EventHandler<BasePlayer> OnDeath; // You can hook into this for character specific death functionality.
    
    #region Health Handling
    public float GetCurrentHealth()
    {
        return health.x;
    }

    public float GetMaxHealth()
    {
        return health.y; 
    }
    // I don't want to return them together, because I feel as if handling them independently will prevent confusion from other people who have to touch this code.
    // Often, people won't need to use anything that relies on calculating something from the maximum health, but I'm leaving it there just in case.
    // Maybe it could be used to weight damage as like a pseudo-"strength modifier" (eg. less health = less damage that the enemy does.). Just a neat idea for anyone who reads this.
    
    // To utilise:
    /*
     * if(collider.TryGetComponent<BasePlayer>(out BasePlayer player) {
     *  player.TakeDamage(10);
     * } else {
     *  There is no player, so nothing can be done.
     * }
     */
    
    // No other special handling for checking if the hit player is about to die, as this function should handle it all for you.
    public void TakeDamage(float damage)
    {
        if (health.x <= 0 || invulnerable) return; // If health.x is already 0 or character is invulnerable, no health should be taken, and this function should be ignored.
        health.x = Mathf.Clamp(health.x - damage, 0, health.y); // Take health and clamp it as 0 min, health.y max.
        if (health.x <= 0) // Check if health.x is below 0.
        {
            // If so, let's call some events.
            OnDeath?.Invoke(this, this);
        }
    }

    // To utilise:
    /*
     * if(collider.TryGetComponent<BasePlayer>(out BasePlayer player) {
     *  bool _success = player.AddHealth(10);
     *  if (_success) {
     *      Health was successfully added.
     *  } else {
     *      The player was likely at full health, so no health was added.
     *  }
     * } else {
     *  There is no player, so nothing can be done.
     * }
     */
    public bool AddHealth(float amount)
    {
        if (Mathf.Approximately(health.x, health.y)) return false; 
        // I want this function to fail if the player is at full health.
        // There are reasons for this:
        //      - Health packs shouldn't get consumed if health is at full, so this allows them to do a check before actually going ahead and destroying the pack.
        //      - Don't need to waste any more time than needed. Frame times matter, yo.
        
        health.x = Mathf.Clamp(health.x + amount, 0, health.y);
        return true;
    }
    #endregion
}
