using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBuilding : ResourceBuilding
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
            if (Time.deltaTime % refreshRate == 0) stock += 5 + (int)player.totalProductionBoost;
    }

    public override void Collect()
    {
        inventory.AddStone(stock);

        base.Collect();
    }
}
