using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpBase : MonoBehaviour
{
    // Code by Joshua Stapleton, 1032020

    // public UIScript UILog;
    // public MovementHandler movementHandler;
    public Sam_PlayeMovement movementHandler;
    public GameObject player;
    public string powerUp;
    public string[] powerUpNames = new string[4] { "Jump Boost", "Jump Boost+", "Speed Boost", "Speed Boost+" };

    private float rotateSpeed;
    public float effectLength; 


    void Start()
    {
        // movementHandler = FindObjectOfType<MovementHandler>();
        movementHandler = FindObjectOfType<Sam_PlayeMovement>();
        player = movementHandler.gameObject;
        powerUp = powerUpNames[Random.Range(0, 4)];
        rotateSpeed = 50f;
    }



    void Update()
    {
        // Adds a passive spin to the pick up in gameworld
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.Self);
    }




    // Detects player collision and does "pick-up" from game world.
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player)
        {
            Debug.Log("Player collision detected");
            PowerUpEffect();
            // Sound effect to pick up here
            Destroy(gameObject);
        }
    }


    // Override template for inheriting script
    protected virtual void PowerUpEffect()
    {
        Debug.Log("Blank power up");
    }
}



