/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sam_PolySpawning : MonoBehaviour
{
    public GameObject[] itemsToSpawn;        // Items to spawn
    public Transform[] spawnRegions;        // Defined spawn regions (e.g., empty GameObjects)
    public int numberOfItemsToSpawn = 10;   // Total items to spawn
    public float spawnHeight = 1f;          // Adjust height of spawn items
    public LayerMask wallLayer;             // Layer assigned to walls for collision checks
    public float collisionCheckRadius = 0.5f; // Radius for collision checking

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
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

    Vector3 GetValidSpawnPosition()
    {
        int attempts = 50; // Avoid infinite loops by limiting attempts
        while (attempts > 0)
        {
            // Get a random spawn region
            Transform randomRegion = spawnRegions[Random.Range(0, spawnRegions.Length)];

            // Generate a random position within the region
            Vector3 randomPosition = GenerateRandomPointInRegion(randomRegion);

            // Check for collisions with walls or invalid spaces
            if (IsPositionValid(randomPosition))
            {
                return randomPosition;
            }

            attempts--;
        }

        return Vector3.zero; // Return zero vector if no valid position is found
    }

    Vector3 GenerateRandomPointInRegion(Transform region)
    {
        Vector3 center = region.position;
        Vector3 size = region.localScale / 2; // Assuming region is a cube or similar shape

        return new Vector3(
            Random.Range(center.x - size.x, center.x + size.x),
            spawnHeight, // Set the height for the spawned object
            Random.Range(center.z - size.z, center.z + size.z)
        );
    }

    bool IsPositionValid(Vector3 position)
    {
        // Check for collisions with the wall layer
        Collider[] hitColliders = Physics.OverlapSphere(position, collisionCheckRadius, wallLayer);

        // Position is valid if no colliders are detected
        return hitColliders.Length == 0;
    }

    void OnDrawGizmos()
    {
        // Visualize spawn regions in the editor
        Gizmos.color = Color.green;
        if (spawnRegions != null)
        {
            foreach (Transform region in spawnRegions)
            {
                Gizmos.DrawWireCube(region.position, region.localScale);
            }
        }
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableSpawnManager : MonoBehaviour
{
    public GameObject[] itemsToSpawn;        // Prefabs to spawn
    public GameObject[] spawnAreas;         // Objects defining spawn areas
    public int numberOfItemsToSpawn = 10;   // Number of items to spawn
    public float spawnHeight = 1f;          // Y position for spawning
    public float collisionCheckRadius = 0.5f; // Radius to check for collisions
    public LayerMask exclusionLayer;        // Layer of objects to avoid during spawning (e.g., walls)
    public LayerMask wallLayer;             // Layer specifically for walls to raycast against

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
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

    Vector3 GetValidSpawnPosition()
    {
        int attempts = 50; // Limit the number of attempts to prevent infinite loops
        while (attempts > 0)
        {
            // Choose a random spawn area
            GameObject randomArea = spawnAreas[Random.Range(0, spawnAreas.Length)];

            // Generate a random position within the bounds of the area
            Vector3 randomPosition = GenerateRandomPointInArea(randomArea);

            // Check if the position is valid (no collisions and no walls blocking)
            if (IsPositionValid(randomPosition) && !IsBlockedByWall(randomPosition))
            {
                return randomPosition;
            }

            attempts--;
        }

        return Vector3.zero; // Return zero if no valid position found
    }

    Vector3 GenerateRandomPointInArea(GameObject area)
    {
        // Get the bounds of the area (using the object's collider or Transform bounds)
        Collider areaCollider = area.GetComponent<Collider>();
        if (areaCollider == null || !areaCollider.isTrigger)
        {
            Debug.LogError("Spawn area must have a trigger collider!");
            return Vector3.zero;
        }

        // Generate a random point within the bounds of the trigger collider
        Bounds bounds = areaCollider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            spawnHeight, // Set the Y position for the spawned object
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    bool IsPositionValid(Vector3 position)
    {
        // Check for collisions using Physics.OverlapSphere
        Collider[] hitColliders = Physics.OverlapSphere(position, collisionCheckRadius, exclusionLayer);

        // Position is valid if no colliders are detected
        return hitColliders.Length == 0;
    }

    bool IsBlockedByWall(Vector3 position)
    {
        // Cast a ray downward to ensure the position is not blocked by a wall
        Ray ray = new Ray(position + Vector3.up * 2f, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f, wallLayer))
        {
            // If the ray hits a wall, return true (blocked)
            Debug.Log($"Position blocked by wall: {hit.collider.name}");
            return true;
        }

        return false; // No wall blocking
    }

    void OnDrawGizmos()
    {
        // Visualize spawn areas
        Gizmos.color = Color.green;
        if (spawnAreas != null)
        {
            foreach (GameObject area in spawnAreas)
            {
                Collider areaCollider = area.GetComponent<Collider>();
                if (areaCollider != null)
                {
                    Gizmos.DrawWireCube(areaCollider.bounds.center, areaCollider.bounds.size);
                }
            }
        }

        // Visualize the collision check radius
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
