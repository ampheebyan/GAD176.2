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

    public List<GameObject> trapTagged = new List<GameObject>();

    public bool primed = false;
    public Vector3 objPos;
    TestObject testObject;
    public float damage = 50f;
    public GameObject trap;



    public virtual void TriggerTrap()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TestObject>() != null)
        {
            trapTagged.Add(gameObject);
            primed = true;
            
        }
        if (primed == true && trapTagged.Count >= 1)
        {
            AreaOfEffect();
            Destroy(this.gameObject);
        }
    }
    public void AreaOfEffect() 
    {
        testObject.health = - damage;
        
        
    }
    public void Update()
    {
        
    }
}
