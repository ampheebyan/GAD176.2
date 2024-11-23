using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem
{
    // Base class for spawning systems
    public abstract class SpawnManagerBase : MonoBehaviour
    {
        public abstract void SpawnItems();
        protected abstract Vector3 GetValidSpawnPosition();
    }

    // Walkable spawn manager derived from the base
    public class WalkableSpawnManager : SpawnManagerBase
    {
        [SerializeField] private GameObject[] itemsToSpawn;  // Prefabs to spawn
        [SerializeField] private GameObject[] spawnAreas;   // Objects defining spawn areas
        [SerializeField] private int numberOfItemsToSpawn = 10;  // Number of items to spawn
        [SerializeField] private float spawnHeight = 1f;    // Y position for spawning
        [SerializeField] private float collisionCheckRadius = 0.5f; // Collision check radius
        [SerializeField] private LayerMask exclusionLayer;  // Layers to avoid during spawning
        [SerializeField] private LayerMask wallLayer;       // Layer for walls to raycast against

        public static WalkableSpawnManager Instance { get; private set; } // Singleton instance

        public event Action<GameObject> OnItemSpawned; // Event for notifying when an item is spawned

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple instances of WalkableSpawnManager detected!");
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            SpawnItems();
        }

        public override void SpawnItems()
        {
            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();

                if (spawnPosition != Vector3.zero)
                {
                    GameObject spawnedItem = Instantiate(
                        itemsToSpawn[UnityEngine.Random.Range(0, itemsToSpawn.Length)],
                        spawnPosition,
                        Quaternion.identity
                    );

                    // Notify listeners about the spawned item
                    OnItemSpawned?.Invoke(spawnedItem);
                }
                else
                {
                    Debug.LogWarning("Failed to find a valid spawn position after multiple attempts.");
                }
            }
        }

        protected override Vector3 GetValidSpawnPosition()
        {
            int attempts = 50;
            while (attempts > 0)
            {
                GameObject randomArea = spawnAreas[UnityEngine.Random.Range(0, spawnAreas.Length)];
                Vector3 randomPosition = GenerateRandomPointInArea(randomArea);

                if (IsPositionValid(randomPosition) && !IsBlockedByWall(randomPosition))
                {
                    return randomPosition;
                }
                attempts--;
            }

            return Vector3.zero; // Return zero if no valid position found
        }

        private Vector3 GenerateRandomPointInArea(GameObject area)
        {
            Collider areaCollider = area.GetComponent<Collider>();
            if (areaCollider == null || !areaCollider.isTrigger)
            {
                Debug.LogError("Spawn area must have a trigger collider!");
                return Vector3.zero;
            }

            Bounds bounds = areaCollider.bounds;
            return new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                spawnHeight,
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private bool IsPositionValid(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, collisionCheckRadius, exclusionLayer);
            return hitColliders.Length == 0; // Valid if no colliders are detected
        }

        private bool IsBlockedByWall(Vector3 position)
        {
            Ray ray = new Ray(position + Vector3.up * 2f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 4f, wallLayer))
            {
                Debug.Log($"Position blocked by wall: {hit.collider.name}");
                return true;
            }
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (GameObject area in spawnAreas)
            {
                Collider areaCollider = area.GetComponent<Collider>();
                if (areaCollider != null)
                {
                    Gizmos.DrawWireCube(areaCollider.bounds.center, areaCollider.bounds.size);
                }
            }

            Gizmos.color = Color.red;
            foreach (GameObject area in spawnAreas)
            {
                if (area != null)
                {
                    Gizmos.DrawWireSphere(area.transform.position, collisionCheckRadius);
                }
            }
        }
    }
}
