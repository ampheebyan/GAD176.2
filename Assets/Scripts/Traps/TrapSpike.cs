using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine;

public class TrapSpike : TrapBase
{
    public GameObject spike;
    public GameObject trapPad;
    public Vector3 trapPos;

    private void Start()
    {
        trapPos = trapPad.transform.position;
    }
    void Update()
    {
        if (primed == true && trapTagged.Count > 0)
        {
            StartCoroutine(SpikeTrap());
            
        }
        
    }

    
    IEnumerator SpikeTrap()
    {
        yield return new WaitForSeconds (1);
        Instantiate(spike, trapPos, Quaternion.identity);
       yield return null;
    }
        
    





}
