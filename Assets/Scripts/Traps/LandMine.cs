using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LandMine : TrapBase
{
    // Damage modifier
    public float impactMod = 2f;
    

    

    void Update()
    {
        /// Triggers the trap + Deals damage to tagged objects
        if (primed == true && trapTagged.Count >= 1)
        {
            Debug.Log("In Range");
            
            foreach (BasePlayer player in trapTagged)
            {
                player.TakeDamage(damage * impactMod);
                
            }
             
            Destroy(this.gameObject);
            
        }
    }



}
