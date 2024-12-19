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

    public override void Collect()
    {
        inventory.AddStone(15);

        base.Collect();
    }
}
