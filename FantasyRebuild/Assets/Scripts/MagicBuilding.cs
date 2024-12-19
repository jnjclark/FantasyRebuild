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

    private void Update()
    {
        //if building is not full
        if (stock! >= capacity)
            //add to stock every refreshRate seconds
            if (Time.deltaTime % refreshRate == 0) stock += 2 + (int)player.totalProductionBoost;
    }
    public override void Collect()
    {
        inventory.AddMagic(stock);

        base.Collect();
    }
}
