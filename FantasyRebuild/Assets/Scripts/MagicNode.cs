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

    public override void Collect()
    {
        inventory.AddMagic(10);

        base.Collect();
    }
}
