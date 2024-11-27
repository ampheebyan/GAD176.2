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

    public List<BasePlayer> trapTagged = new List<BasePlayer>();

    public bool primed = false;
    public Vector3 objPos;
    BasePlayer testObject;
    public float damage = 25f;
    



    public virtual void TriggerTrap()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 15f);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<BasePlayer>(out BasePlayer player))
            {
                primed = true;
                trapTagged.Add(player);
            }
        }

    }
    public void OnTriggerExit (Collider other)
    {
        if (other.TryGetComponent<BasePlayer>(out BasePlayer player))
        {
            trapTagged.Remove(player);
        }
    }


   
    public void Update()
    {
        
    }
}
