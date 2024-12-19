using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicNode : ResourceNode
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
        inventory.AddMagic(10);
    }
}
