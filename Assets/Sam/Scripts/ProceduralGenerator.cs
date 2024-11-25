using UnityEngine;

namespace SpawnSystem
{
    public class ProceduralGenerator : SpawnManagerBase
    {
        public enum SpawnMode
        {
            CubeShaped, // Use cube-shaped zones
            Irregular   // Use irregular polygon-based zones, was recommended to do just in case, echnically of no use though
        }

        [Header("Spawn Settings")]
        [SerializeField] private SpawnMode spawnMode = SpawnMode.CubeShaped; // Choose spawn mode

        public override void SpawnItems()
        {
            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();

                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(
                        itemsToSpawn[Random.Range(0, itemsToSpawn.Length)],
                        spawnPosition,
                        Quaternion.identity
                    );
                }
                else
                {
                    Debug.LogWarning("Failed to find a valid spawn position after multiple attempts.");
                }
            }
        }
        private void Start()
        {
        SpawnItems();
        }   

        protected override Vector3 GetValidSpawnPosition()
        {
            int attempts = 50; // Limit attempts to prevent infinite loops
            while (attempts > 0)
            {
                Vector3 randomPosition = spawnMode switch
                {
                    SpawnMode.CubeShaped => GetRandomPointInCubeArea(),
                    SpawnMode.Irregular => GetRandomPointInIrregularArea(),
                    _ => Vector3.zero
                };

                if (IsPositionValid(randomPosition) && !IsBlockedByWall(randomPosition))
                {
                    return randomPosition;
                }
                attempts--;
            }

            return Vector3.zero; // Return zero if no valid position is found
        }

        private Vector3 GetRandomPointInCubeArea()
        {
            // Choose a random cube-shaped spawn area
            GameObject randomArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            Collider areaCollider = randomArea.GetComponent<Collider>();

            if (areaCollider == null || !areaCollider.isTrigger)
            {
                Debug.LogError("Cube-shaped spawn area must have a trigger collider!");
                return Vector3.zero;
            }

            // Generate a random point within the bounds
            Bounds bounds = areaCollider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                spawnHeight,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private Vector3 GetRandomPointInIrregularArea()
        {
            // Choose a random irregular-shaped spawn area
            GameObject randomArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            Collider areaCollider = randomArea.GetComponent<Collider>();

            if (areaCollider == null || !areaCollider.isTrigger)
            {
                Debug.LogError("Irregular-shaped spawn area must have a trigger collider!");
                return Vector3.zero;
            }

            // Generate a random point within the bounds of the irregular area
            Bounds bounds = areaCollider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                spawnHeight,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private bool IsPositionValid(Vector3 position)
        {
            // Check for overlaps with other objects
            Collider[] hitColliders = Physics.OverlapSphere(position, collisionCheckRadius, exclusionLayer);
            return hitColliders.Length == 0; // Valid if no colliders are detected
        }

        private bool IsBlockedByWall(Vector3 position)
        {
            // Use raycasting to ensure the position isn't blocked by a wall
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
            // Visualize spawn areas in the editor
            Gizmos.color = Color.green;
            foreach (GameObject area in spawnAreas)
            {
                Collider areaCollider = area.GetComponent<Collider>();
                if (areaCollider != null)
                {
                    Gizmos.DrawWireCube(areaCollider.bounds.center, areaCollider.bounds.size);
                }
            }

            // Visualize collision check radius
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
