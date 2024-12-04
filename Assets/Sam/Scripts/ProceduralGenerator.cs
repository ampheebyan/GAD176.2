
using UnityEngine;

namespace SpawnSystem
{
    public class ProceduralGenerator : SpawnManagerBase
    {
        public enum SpawnMode
        {
            CubeShaped, // Use cube-shaped zones
            Irregular   // Use irregular polygon-based zones
        }

        [Header("Spawn Settings")]
        [SerializeField] private SpawnMode spawnMode = SpawnMode.CubeShaped; // Choose spawn mode
        [SerializeField] private float minSpawnDistance = 1.0f;             // Minimum distance between objects

        public override void SpawnItems()
        {
            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();

                if (spawnPosition != Vector3.zero)
                {
                    var newObject = Instantiate(
                        itemsToSpawn[Random.Range(0, itemsToSpawn.Length)],
                        spawnPosition,
                        Quaternion.identity
                    );

                    Debug.Log($"Spawned {newObject.name} at {spawnPosition}");
                }
                else
                {
                    Debug.LogWarning("Failed to find a valid spawn position after multiple attempts.");
                }
            }
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
            GameObject randomArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            Collider areaCollider = randomArea.GetComponent<Collider>();

            if (areaCollider == null || !areaCollider.isTrigger)
            {
                Debug.LogError("Cube-shaped spawn area must have a trigger collider!");
                return Vector3.zero;
            }

            Bounds bounds = areaCollider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                spawnHeight,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private Vector3 GetRandomPointInIrregularArea()
        {
            GameObject randomArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            Collider areaCollider = randomArea.GetComponent<Collider>();

            if (areaCollider == null || !areaCollider.isTrigger)
            {
                Debug.LogError("Irregular-shaped spawn area must have a trigger collider!");
                return Vector3.zero;
            }

            Bounds bounds = areaCollider.bounds;
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                spawnHeight,
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }

        private bool IsPositionValid(Vector3 position)
        {
            // Ensure no collisions and enforce minimum spawn distance
            Collider[] hitColliders = Physics.OverlapSphere(position, collisionCheckRadius, exclusionLayer);
            if (hitColliders.Length > 0)
                return false;

            foreach (var obj in FindObjectsOfType<GameObject>())
            {
                if (Vector3.Distance(obj.transform.position, position) < minSpawnDistance)
                    return false;
            }

            return true;
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
            // Visualize spawn areas
            Gizmos.color = spawnMode == SpawnMode.CubeShaped ? Color.blue : Color.yellow;

            foreach (GameObject area in spawnAreas)
            {
                Collider areaCollider = area.GetComponent<Collider>();
                if (areaCollider != null)
                {
                    if (spawnMode == SpawnMode.CubeShaped)
                        Gizmos.DrawWireCube(areaCollider.bounds.center, areaCollider.bounds.size);
                    else
                        Gizmos.DrawWireSphere(areaCollider.bounds.center, collisionCheckRadius);
                }
            }
        }

        private void Start()
        {
            SpawnItems();
        }
    }
}
