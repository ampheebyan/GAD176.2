using System;
using UnityEngine;

namespace Characters
{
    public class AIControllerDebug : MonoBehaviour
    {
        private AIController aiController;
        public AIController.AIControllerDebugData data;
        
        private float _timer;
        private void Awake()
        {
            TryGetComponent<AIController>(out aiController);
            if(aiController == null) throw new MissingComponentException("Missing AIController");

            data = new AIController.AIControllerDebugData();
        }

        private void OnEnable()
        {
            aiController.OnAIUpdate += OnAIUpdate;
        }

        private void OnDisable()
        {
            aiController.OnAIUpdate -= OnAIUpdate;
        }

        private void OnAIUpdate(AIController.AIControllerDebugData obj)
        {
            data = obj;
        }
    }
}