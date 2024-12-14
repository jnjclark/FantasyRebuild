using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBuilding : ResourceBuilding
{
    Inventory inventory;

    private void Awake()
    {
        //set inventory
        inventory = Inventory.instance;
    }

    public override void Collect()
    {
        inventory.AddMagic(stock);

        base.Collect();
    }
}
