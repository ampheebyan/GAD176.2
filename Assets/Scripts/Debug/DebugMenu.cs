using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.LocalPlayer;
using Detection;
using Movement;
using UnityEngine;
using TMPro;
// DebugMenu.cs
// Phoebe Faith (1033478)
/// <summary>
/// Contains debug menu handling and connector logic
/// </summary>
namespace PDebug
{
    public class DebugMenu : MonoBehaviour
    {
        private MovementHandler movementHandler;
        private LocalPlayer localPlayer;
        
        private AIControllerDebug[] aiControllerDebug;
        private Dictionary<AIControllerDebug, DebugMenuTile> debugMenuTiles = new Dictionary<AIControllerDebug, DebugMenuTile>();        
        
        [SerializeField]
        private GameObject debugMenuTilePrefab;
        
        [SerializeField] private Transform debugTileParent;
        
        [SerializeField] private TMP_Text movementHandlerDebugText;

        private float _timer;
        
        private void OnEnable()
        {
            if (FindObjectsByType<LocalPlayer>(FindObjectsSortMode.None).Length > 1)
            {
                throw new Exception("There should only be one LocalPlayer in your scene, and there is more than one. Please ensure the only LocalPlayer component in your scene is on the local controlling player.");
            }

            if (FindObjectsByType<MovementHandler>(FindObjectsSortMode.None).Length > 1)
            {
                throw new Exception("There should only be one MovementHandler in your scene, and there is more than one. Please ensure the only MovementHandler component in your scene is on the local controlling player.");
            }

            aiControllerDebug = GameObject.FindObjectsByType<AIControllerDebug>(FindObjectsSortMode.None);
            RenewAIControllerDebugTiles();
            
            localPlayer = FindObjectOfType<LocalPlayer>();
            movementHandler = FindObjectOfType<MovementHandler>();
            
            if(movementHandler) movementHandler.OnDebugInformationChanged += MovementHandlerDebug;
            else
            {
                throw new Exception("Missing MovementHandler in scene.");
            }
            if(localPlayer) localPlayer.OnBasePlayerDebugUpdate += BasePlayerDebug;
            else
            {
                throw new Exception("Missing LocalPlayer in scene.");
            }
        }
        
        private void OnDisable()
        {
            if(movementHandler) movementHandler.OnDebugInformationChanged -= MovementHandlerDebug;
            if(localPlayer) localPlayer.OnBasePlayerDebugUpdate -= BasePlayerDebug;
        }

        private void RenewAIControllerDebugTiles()
        {
            Debug.Log("New debug tile");

            Dictionary<AIControllerDebug, DebugMenuTile> debugTiles = debugMenuTiles;
            
            foreach (KeyValuePair<AIControllerDebug, DebugMenuTile> x in debugTiles)
            {
                if (x.Key == null)
                {
                    debugMenuTiles.Remove(x.Key);
                    Destroy(x.Value.gameObject);
                }
            }
                
            foreach (AIControllerDebug aiControllerDebug in aiControllerDebug)
            {
                Debug.Log("New debug tile 1");

                if (debugMenuTiles.TryGetValue(aiControllerDebug, out DebugMenuTile debugMenuTile))
                {
                        
                }
                else
                {
                    Debug.Log("New debug tile 2");
                    GameObject newDebugMenuTile = Instantiate(debugMenuTilePrefab, debugTileParent);
                    DebugMenuTile ndmTScript = newDebugMenuTile.GetComponent<DebugMenuTile>();
                    debugMenuTiles.Add(aiControllerDebug, ndmTScript);                        
                }
            }
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 2f)
            {
                AIControllerDebug[] previous = aiControllerDebug; 
                aiControllerDebug = GameObject.FindObjectsByType<AIControllerDebug>(FindObjectsSortMode.None);

                if (!Enumerable.SequenceEqual(previous, aiControllerDebug))
                {
                    RenewAIControllerDebugTiles();
                };
                _timer = 0f;
            }

            foreach (KeyValuePair<AIControllerDebug, DebugMenuTile> x in debugMenuTiles)
            {
                DebugMenuTile ndmTScript = x.Value;
                AIControllerDebug aiControllerDebug = x.Key;

                if (aiControllerDebug != null)
                {
                    AIController.AIControllerDebugData data = aiControllerDebug.data;
                    
                    ndmTScript.SetBody($"{aiControllerDebug.name}\n" +
                                       $"{(data.targetPlayer ? $"Targetting: {data.targetPlayer.name}" : data.target ? $"Targetting: {data.target.position}" : "Targetting nothing.")}\n" +
                                       $"{(data.player ? $"{(data.player.IsInvulnerable ? $"{data.player.GetCurrentHealth()} / {data.player.GetMaxHealth()}": "Invulnerable")}" : $"Missing BasePlayer?")}");
                }
            }
        }

        private void BasePlayerDebug(BasePlayer.BasePlayerDebug _info)
        {
            if(GlobalReference.isDebugLog) Debug.Log($"DebugMenu: BasePlayerDebug: {_info.health}");
        }
        
        private void MovementHandlerDebug(MovementHandler.MovementHandlerDebugInformation _info)
        {
            movementHandlerDebugText.SetText($"{_info.position.x}, {_info.position.y}, {_info.position.z}\n" +
                                             $"Speed: {_info.currentMovementState.currentWalkSpeed}\n" +
                                             $"Grounded: {_info.currentMovementState.isGrounded}\n" +
                                             $"Crouching: {_info.currentMovementState.isCrouching}\n" +
                                             $"Running: {_info.currentMovementState.isRunning}");
        }
    }
}
