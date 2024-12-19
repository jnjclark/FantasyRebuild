using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    public int damage;      //how much damage the turret does to the dragon per attack
    public int fireRate;    //how often, in seconds, the turret attacks the dragon

    public Dragon dragon;

    private void Update()
    {
        //every fireRate seconds
        if (Time.deltaTime % fireRate == 0) Fire();
    }

    //turret shoots dragon
    private void Fire()
    {
        dragon.Damage(damage);
    }

    //TODO need a way to set the turret's target as the current dragon
}
