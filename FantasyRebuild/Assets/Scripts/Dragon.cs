using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public int health;
    public int damage;
    public int speed;
    public float cooldownTime = 3f;         //attack cooldown time in seconds
    public float cooldown = 0f;             //current cooldown time
    public Vector2 currentPos;
    public Transform[] buildingPosArray;
    public Animator anim;                   //reference to animator of dragon

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
            Attack();
    }

    void Start()
    {
        currentPos = transform.position;
        buildingPosArray = Player.buildingPosArray;
    }

    //remove health from the dragon, next day if dragon dead
    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(this);                          //delete dragon
            Player.daycycle.IncreaseDay();
        }
    }

    //Attacks the closest building
    public void Attack()
    {
        //TODO: in player class need to initialize buildingPosArray/List
        Transform target = getClosestBuilding(buildingPosArray);    //gets closest building, set as target
        while (! currentPos.Equals(target.transform.position))
            moveToward(target);                     //calls moveToward function until position is equal to target

        //TODO: need to get building gameobject at target position and remove health from the building object
        
        cooldown = cooldownTime;                    //reset cooldown timer
    }

    //calculates the closest building
    public Transform getClosestBuilding(Transform[] buildingList)
    {
        Transform closestTarget = null;             //initialize closest target
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Transform potentialTarget in buildingList)
        {
            Vector2 targetDirection = (Vector2) potentialTarget.transform.position - currentPos;    //calculate direction between dragon and target
            float dSqrToTarget = targetDirection.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;                              //set closestDistanceSqr to current building distance
                closestTarget = potentialTarget;                                //set closestTarget to current building
            }
        }
        return closestTarget;
    }
    
    //move toward the selected target(building)
    public void moveToward(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    
}
