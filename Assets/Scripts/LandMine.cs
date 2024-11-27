using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : TrapBase
{

    public float impactMod = 2f;

    
   

    public void OnTriggerEnter(Collider other)
    {
        if(primed == true && trapTagged.Count >= 1)
        {
            Debug.Log("boom");
        }
    }



}
