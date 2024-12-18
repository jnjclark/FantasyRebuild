using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    public float refreshRate;     //how often add to stock
    public int capacity;        //maximum capacity of the building
    public int stock;           //current amount of resources in the building

    private void Update()
    {
        //add one to stock every refreshRate seconds
        if (Time.deltaTime % refreshRate == 0) stock += 1;
    }

    public virtual void Collect()
    {
        stock = 0;
    }
}
