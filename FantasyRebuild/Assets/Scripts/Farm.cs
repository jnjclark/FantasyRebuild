using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public int happiness;
    public float productionIncrease;

    Player player;

    private void Awake()
    {
        player = Player.instance;
    }

    public override void OnPlaced()
    {
        base.OnPlaced();

        player.AddHappiness(happiness);
        player.AddProductionBoost(productionIncrease);
    }

    public override void DestroySelf()
    {
        player.SubtractHappiness(happiness);
        player.SubtractProductionBoost(productionIncrease);

        base.DestroySelf();
    }
}
