using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    public int scoreVal;
    public int damage;      //how much damage the turret does to the dragon per attack
    public int fireRate;    //how often, in seconds, the turret attacks the dragon

    public Dragon dragon;

    private void Start()
    {
        health = 100;       //health 100
        damage = 10;        //base damage of 10
        fireRate = 1;       //fires once every second
        woodCost = 25;      //cost of the building
        stoneCost = 0;
        magicCost = 0;
        scoreVal = 100;     //score value of building
    }
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
