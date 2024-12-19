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
    }

    public override void DestroySelf()
    {
        player.SubtractHappiness(happiness);

        base.DestroySelf();
    }
}
