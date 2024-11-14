using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrapBase : TrapBase
{
    public float impactDmg = 50f;
    

    public override void TriggerTrap()
    {
        base.TriggerTrap();
        
        




    }
    public void OnTriggerEnter (Collider other) 
    {
        if(other.CompareTag("Player"))
        {

        }
    }



}
