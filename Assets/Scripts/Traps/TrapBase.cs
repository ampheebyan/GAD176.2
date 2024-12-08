using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class TrapBase : MonoBehaviour
{

    public List<BasePlayer> trapTagged = new List<BasePlayer>();

    public bool primed = false;
    public Vector3 objPos;
    BasePlayer basePlayer;
    public float damage = 25f;
    



    public virtual void TriggerTrap()
    {
        
    }

    /// <summary>
    /// If PlayerBase script is detected on objects adds object to list
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        {
            if (other.TryGetComponent<BasePlayer>(out BasePlayer player))
            {
                primed = true;
                trapTagged.Add(player);
                Debug.Log("Tagged: " +  player.name);
            }
        }

    }

    /// <summary>
    /// If PlayerBase script is no longeer detected removes tag from object
    /// 
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit (Collider other)
    {
        if (other.TryGetComponent<BasePlayer>(out BasePlayer player))
        {
            trapTagged.Remove(player);
            Debug.Log("UnTagged: " + player.name);
        }
    }


   
    
}
