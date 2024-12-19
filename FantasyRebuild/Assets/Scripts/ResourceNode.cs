using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int charges = 2;   //how many times the resource can be collected

    public virtual void RemoveCharge()
    {
        charges = charges - 1;
        if (charges <= 0) DestroySelf();
    }

    //remove resource node from game
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public virtual void AddResource()
    {
        
    }
}
