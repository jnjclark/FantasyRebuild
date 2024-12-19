using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : Building
{
    public int happiness = 15;

    public override void OnPlaced()
    {
        base.OnPlaced();

        player.AddHappiness(happiness);

        health = 100;
        score = 150;
        woodCost = 100;
        stoneCost = 30;
        magicCost = 5;
    }

    public override void DestroySelf()
    {
        player.SubtractHappiness(happiness);

        base.DestroySelf();
    }
}
