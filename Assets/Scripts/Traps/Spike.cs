using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Spike : TrapBase
{

    public float bleedDamage = 1f;


    // Update is called once per frame
    void Update()
    {
        /// Triggers the trap + Deals damage to tagged objects
        /// trapTagged tags each object within a zone and if tagged will take damage
        /// 
        if (primed == true && trapTagged.Count >= 1)
        {
            Debug.Log("In Range");

            foreach (BasePlayer player in trapTagged)
            {
                player.TakeDamage(bleedDamage);

            }

            

        }
    }
}
