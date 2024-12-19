using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneNode : ResourceNode
{
    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.instance;
    }

    public override void RemoveCharge()
    {
        base.RemoveCharge();
        AddResource();
        if (base.charges <= 0) DestroySelf();
    }

    public override void AddResource()
    {
        inventory.AddStone(15);
    }
}
