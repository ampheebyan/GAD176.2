using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Phoebe Faith

/// <summary>
/// Handles giving AIControllers new patrolling positions.
/// </summary>

namespace Characters
{
    public class PatrolController : MonoBehaviour
    {
        public AIController[] aiControllers;
        public Transform[] patrolPositions;
        
        private void OnEnable()
        {
            foreach (AIController aiController in aiControllers)
            {
                if (patrolPositions.Length != 0) // Ensure there's positions
                {
                    // Set new random position
                    aiController.SetNewPatrolTarget(patrolPositions[Random.Range(0, patrolPositions.Length)]);
                }
                // Hook
                aiController.PatrolTargetReached += PatrolTargetReached;
            }
        }

        private void OnDisable()
        {
            foreach (AIController aiController in aiControllers)
            {
                // Unhook
                aiController.PatrolTargetReached += PatrolTargetReached;
            }
        }

        private void PatrolTargetReached(AIController aiController)
        {
            if(GlobalReference.isDebugLog) Debug.Log($"{aiController.name}: Patrol Target Reached"); 
            if (patrolPositions.Length != 0) // Ensure there's positions
            {
                // Set new random position
                aiController.SetNewPatrolTarget(patrolPositions[Random.Range(0, patrolPositions.Length)]);
            }
        }
    }
}