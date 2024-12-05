using System;
using Detection;
using UnityEngine;
using UnityEngine.AI;


// Phoebe Faith

///<summary>
/// Contains code relating to AI
/// </summary>


namespace Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIController : MonoBehaviour
    {
        public enum AIType
        {
            npc,
            enemy
        }

        public class AIControllerDebugData
        {
            public AIType aiType;
            public Transform target;
            public BasePlayer targetPlayer;
            public BasePlayer player;
        }
        
        public Action<AIControllerDebugData> OnAIUpdate;
        
        private AIControllerDebugData _debugData = new AIControllerDebugData();
        
        public AIType aiType = AIType.npc;
        [SerializeField]
        private DetectionMiddleman detectionMiddleman;
        
        private NavMeshAgent agent;
        private BasePlayer player;

        private BasePlayer targetPlayer;
        private Transform target;
        private void Awake()
        {
            TryGetComponent<NavMeshAgent>(out agent);
            if(agent == null) throw new Exception("NavMeshAgent component is null");
            TryGetComponent<BasePlayer>(out player);
            if(player == null) throw new Exception("BasePlayer component is null");
            
            _debugData.aiType = aiType;
            _debugData.player = player;
        }

        private void OnEnable()
        {
            detectionMiddleman.Detected += DetectionMiddlemanOnDetected;
            detectionMiddleman.DetectedClear += DetectedClear;
        }

        private void DetectedClear()
        {
            targetPlayer = null;
            target = null;
            _debugData.target = null;
            _debugData.targetPlayer = null;
            OnAIUpdate?.Invoke(_debugData);
        }

        private void Update()
        {
            if(targetPlayer != null) agent.SetDestination(targetPlayer.transform.position);
        }

        public AIControllerDebugData GetDebugData()
        {
            // Obsolete
            return null;
        }
        
        private void DetectionMiddlemanOnDetected(object sender, DetectionBase.DetectionEventData data)
        {
            if (data.player)
            {
                agent.SetDestination(data.player.transform.position);
                targetPlayer = data.player;
                _debugData.targetPlayer = targetPlayer;
                OnAIUpdate?.Invoke(_debugData);

            }
            else if(data.position)
            {
                agent.SetDestination(data.position.position);
                target = data.position;
                _debugData.target = target;
                OnAIUpdate?.Invoke(_debugData);

            }
        }
    }
}