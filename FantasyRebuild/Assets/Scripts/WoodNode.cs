using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodNode : ResourceNode
{
    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.instance;
    }

    public override void RemoveResource(int amount)
    {
        inventory.AddWood(amount);

        base.RemoveResource(amount);
    }
}
