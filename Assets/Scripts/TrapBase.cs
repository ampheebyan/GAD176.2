using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class TrapBase : MonoBehaviour
{
    public bool setOff = false;
    


    public virtual void TriggerTrap()
    {

    }
    public void AreaOfEffect(Vector3 location, float radius, float damage)
    {
        Collider[] dmgRange = Physics.OverlapSphere(location, radius);
        foreach (Collider col in dmgRange)
        {
            // Set Object to enter grab collider component of object 
            // if(object != null) 
            {
                // float area = (location - objects position x magnitude)
                // float effect = 1 - (area / radius)


                // Apply damage to object
            }//https://discussions.unity.com/t/how-to-apply-explosion-magic-grenade-damage-to-detect-objects-enemies-within-a-given-radius-and-apply-aoe-damage-to-each-enemy-based-on-its-distance/2524/2
        }
    }
}
