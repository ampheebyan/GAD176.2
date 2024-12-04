using UnityEngine;

namespace SpawnSystem
{
    public abstract class SpawnManagerBase : MonoBehaviour
    {
        [SerializeField] protected GameObject[] itemsToSpawn; 
        [SerializeField] protected GameObject[] spawnAreas;   // Objects defining spawn areas
        [SerializeField] protected int numberOfItemsToSpawn = 10;  
        [SerializeField] protected float spawnHeight = 1f;    // Y position for spawning
        [SerializeField] protected float collisionCheckRadius = 0.5f; // Collision check radius
        [SerializeField] protected LayerMask exclusionLayer;  // Layers to avoid during spawning
        [SerializeField] protected LayerMask wallLayer;       // Layer for walls to raycast against

        public abstract void SpawnItems(); // Abstract method to spawn items
        protected abstract Vector3 GetValidSpawnPosition(); // Abstract method to find valid spawn positionss
    }
}
