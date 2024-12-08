using System;
using UnityEngine;

// Phoebe Faith

///<summary>
/// Contains code relating to debug of AIController as a middleman.
/// </summary>
namespace Characters
{
    public class AIControllerDebug : MonoBehaviour
    {
        private AIController aiController;
        public AIController.AIControllerDebugData data;
        private void Awake()
        {
            TryGetComponent<AIController>(out aiController);
            if(aiController == null) throw new MissingComponentException("Missing AIController");

            data = new AIController.AIControllerDebugData();
        }
        // Hook into aiController update
        private void OnEnable()
        {
            aiController.OnAIUpdate += OnAIUpdate;
        }

        // Unhook from aiController update
        private void OnDisable()
        {
            aiController.OnAIUpdate -= OnAIUpdate;
        }

        // Set obj to data
        private void OnAIUpdate(AIController.AIControllerDebugData obj)
        {
            data = obj;
        }
    }
}