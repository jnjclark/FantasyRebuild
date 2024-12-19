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

    public override void Collect()
    {
        inventory.AddWood(20);

        base.Collect();
    }
}
