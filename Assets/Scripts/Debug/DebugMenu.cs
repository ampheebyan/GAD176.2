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
            // Handle looking to see if there are too many of the Local specific objects. Throw exception if too many.
            
            if (FindObjectsByType<LocalPlayer>(FindObjectsSortMode.None).Length > 1)
            {
                throw new Exception("There should only be one LocalPlayer in your scene, and there is more than one. Please ensure the only LocalPlayer component in your scene is on the local controlling player.");
            }

            if (FindObjectsByType<MovementHandler>(FindObjectsSortMode.None).Length > 1)
            {
                throw new Exception("There should only be one MovementHandler in your scene, and there is more than one. Please ensure the only MovementHandler component in your scene is on the local controlling player.");
            }

            // Find all AIControllers at start of scene and create debug tiles for them.
            aiControllerDebug = GameObject.FindObjectsByType<AIControllerDebug>(FindObjectsSortMode.None);
            RenewAIControllerDebugTiles();
            
            // Find localPlayer and movementHandler
            localPlayer = FindObjectOfType<LocalPlayer>();
            movementHandler = FindObjectOfType<MovementHandler>();
            
            // Hook into relevant debug events
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
            // Unhook
            if(movementHandler) movementHandler.OnDebugInformationChanged -= MovementHandlerDebug;
            if(localPlayer) localPlayer.OnBasePlayerDebugUpdate -= BasePlayerDebug;
        }

        private void RenewAIControllerDebugTiles()
        {
            Dictionary<AIControllerDebug, DebugMenuTile> debugTiles = debugMenuTiles; // Temporary array
            
            foreach (KeyValuePair<AIControllerDebug, DebugMenuTile> x in debugTiles)
            {
                if (x.Key == null) // Remove AIControllerDebug tiles that don't exist now.
                {
                    debugMenuTiles.Remove(x.Key); 
                    Destroy(x.Value.gameObject);
                }
            }
                
            foreach (AIControllerDebug aiControllerDebug in aiControllerDebug) // Run through the updated array
            {
                if (!debugMenuTiles.TryGetValue(aiControllerDebug, out DebugMenuTile debugMenuTile)) // If there isn't a value, make one!
                {
                    GameObject newDebugMenuTile = Instantiate(debugMenuTilePrefab, debugTileParent);
                    DebugMenuTile ndmTScript = newDebugMenuTile.GetComponent<DebugMenuTile>();
                    debugMenuTiles.Add(aiControllerDebug, ndmTScript);                        
                }
            }
        }
        
        private void Update()
        {
            // Every 2 seconds look for new AIControllers, and if there's a difference between the previous and current, run through renewal of AIControllerDebugTiles.
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
            // Output of BasePlayer info
            if(GlobalReference.isDebugLog) Debug.Log($"DebugMenu: BasePlayerDebug: {_info.health}");
        }
        
        private void MovementHandlerDebug(MovementHandler.MovementHandlerDebugInformation _info)
        {
            // Set movementhandler debug text
            movementHandlerDebugText.SetText($"{_info.position.x}, {_info.position.y}, {_info.position.z}\n" +
                                             $"Speed: {_info.currentMovementState.currentWalkSpeed}\n" +
                                             $"Grounded: {_info.currentMovementState.isGrounded}\n" +
                                             $"Crouching: {_info.currentMovementState.isCrouching}\n" +
                                             $"Running: {_info.currentMovementState.isRunning}");
        }
    }
}
