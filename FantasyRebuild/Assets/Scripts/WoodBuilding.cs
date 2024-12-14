using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBuilding : ResourceBuilding
{
    Inventory inventory;

    private void Awake()
    {
        //set inventory
        inventory = Inventory.instance;
    }

    public override void Collect()
    {
        inventory.AddWood(stock);

        base.Collect();
    }
}
