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
            Debug.Log("boom");
            
            
        }
    }



}
