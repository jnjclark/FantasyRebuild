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
        refreshRate = 60;
        health = 100;
        score = 100;
        woodCost = 75;
        stoneCost = 25;
        magicCost = 0;
    }
    private void Update()
    {
        //if building is not full
        if (stock! >= capacity)
            //add to stock every refreshRate seconds
            if (Time.deltaTime % refreshRate == 0) stock += 10 + (int)player.totalProductionBoost;
    }
    public override void Collect()
    {
        inventory.AddWood(stock);

        base.Collect();
    }
}
