using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int charges;    //how many times the resource can be collected

    public virtual void RemoveResource(int amount)
    {
        charges -= amount;

        if (charges <= 0) DestroySelf();
    }

    //remove resource node from game
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
