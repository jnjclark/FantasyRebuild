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
    public Vector2 currentPos = transform.position;

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
            Attack();
    }

    void Start()
    {

    }

    //remove health from the dragon
    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(Dragon);                        //delete dragon
            increaseDay();
        }
    }

    //Attacks the closest building
    public void Attack()
    {
        Transform target = getClosestBuilding();    //gets closest building, set as target
        while (currentPos != target)
            moveToward(target);                     //calls moveToward function until position is equal to target

        target.health = target.health - damage;     //remove health from target
        cooldown = cooldownTime;                    //reset cooldown timer
    }

    //calculates the closest building
    public Transform getClosestBuilding(List<building>/* CHECK */ buildings)
    {
        Transform closestTarget = null;             //initialize closest target
        float closestDistanceSqr = Mathf.Infinity;
        foreach (GameObject potentialTarget in buildings)
        {
            Vector2 targetDirection = potentialTarget.position - currentPos;    //calculate distance between dragon and target
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;                              //set closestDistanceSqr to current building distance
                closestTarget = potentialTarget;                                //set closestTarget to current building
            }
        }
        return bestTarget;
    }

    //move toward the selected target(building)
    public void moveToward(Transform target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

}
