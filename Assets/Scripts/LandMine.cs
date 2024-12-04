using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LandMine : TrapBase
{

    public float impactMod = 2f;
    



    void Update()
    {
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
