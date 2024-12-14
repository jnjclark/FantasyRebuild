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

    public override void RemoveResource(int amount)
    {
        inventory.AddStone(amount);

        base.RemoveResource(amount);
    }
}
