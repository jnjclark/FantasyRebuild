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

    public override void RemoveResource(int amount)
    {
        inventory.AddMagic(amount);

        base.RemoveResource(amount);
    }
}
