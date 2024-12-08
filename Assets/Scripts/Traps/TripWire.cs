using System.Collections;
using System.Collections.Generic;
using Characters;
using UnityEngine;

public class TripWire : TrapBase
{
  

    
    void Update()
    {

        // Triggers the trap + Deals damage to tagged objects
        if (primed == true && trapTagged.Count >= 1)
        {
            Debug.Log("In Range");

            foreach (BasePlayer player in trapTagged)
            {
                player.TakeDamage(damage);

            }

            Destroy(this.gameObject);

        }
    }
}
