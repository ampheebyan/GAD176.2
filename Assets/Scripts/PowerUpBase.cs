using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpBase : MonoBehaviour
{

    // public MessageLog messageLog;
    // public MovementHandler movementHandler;
    public Sam_PlayeMovement movementHandler;
    public GameObject player;
    public string powerUp;
    public string[] powerUpNames = new string[4] { "Jump Boost", "Jump Boost+", "Speed Boost", "Speed Boost+" };

    private float rotateSpeed;




    // Start is called before the first frame update
    void Start()
    {
        // movementHandler = FindObjectOfType<MovementHandler>();
        movementHandler = FindObjectOfType<Sam_PlayeMovement>();
        player = movementHandler.gameObject;
        powerUp = powerUpNames[Random.Range(0, 4)];
        rotateSpeed = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        // adds constant spin to object
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player)
        {
            Debug.Log("Player collision detected");
            Destroy(gameObject);
        }
    }
}



