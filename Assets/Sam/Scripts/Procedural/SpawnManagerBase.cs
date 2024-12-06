/// <summary>
/// Abstract base class for managing the spawning of items in a scene. 
/// Provides a framework for determining valid spawn positions and ensuring collision constraints.
/// </summary>
using UnityEngine;

namespace SpawnSystem
{
    public abstract class SpawnManagerBase : MonoBehaviour
    {
        [SerializeField] protected GameObject[] itemsToSpawn; // Array of items to spawn
        [SerializeField] protected GameObject[] spawnAreas;   // Objects defining spawn areas
        [SerializeField] protected int numberOfItemsToSpawn = 10; // Total number of items to spawn
        [SerializeField] protected float spawnHeight = 1f;    // Y-axis position offset for spawning items
        [SerializeField] protected float collisionCheckRadius = 0.5f; // Radius for collision checks
        [SerializeField] protected LayerMask exclusionLayer;  // Layers to avoid when spawning items
        [SerializeField] protected LayerMask wallLayer;       // Layer for walls to raycast against

        /// <summary>
        /// Abstract method to handle the spawning of items. 
        /// Must be implemented in derived classes.
        /// </summary>
        public abstract void SpawnItems();

        /// <summary>
        /// Abstract method to determine a valid spawn position based on constraints.
        /// implemented in the derived classes.
        /// returnsvalid spawn position, or Vector3.zero if none is found.
        protected abstract Vector3 GetValidSpawnPosition();
    }
}
