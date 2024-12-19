using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public int happiness = 10;
    public float productionIncrease;

    public override void OnPlaced()
    {
        base.OnPlaced();

        player.AddHappiness(happiness);
        player.AddProductionBoost(productionIncrease);

        health = 100;
        score = 200;
        woodCost = 100;
        stoneCost = 50;
        magicCost = 15;
    }

    public override void DestroySelf()
    {
        player.SubtractHappiness(happiness);
        player.SubtractProductionBoost(productionIncrease);

        base.DestroySelf();
    }
}
