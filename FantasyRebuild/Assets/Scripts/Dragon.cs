using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private SpriteRenderer spriteRenderer;

    public Transform target;

    DayCycle daycycle;

    private void Awake()
    {
        health = 100;
        damage = 10;
        speed = 1;

        anim = GetComponent<Animator>();
    }
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
            Attack();
    }

    void Start()
    {
        //set reference
        daycycle = DayCycle.instance;

        currentPos = transform.position;
        buildingPosArray = Player.buildingPosArray;
        anim.SetBool("Hover", true); // Start with the hover animation
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    
        //get attackval, health, set speed
        damage = getAttackVal();
        health = getHealthVal();
        speed = 1;
    }

    //remove health from the dragon, next day if dragon dead
    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(this);                          //delete dragon
            daycycle.IncreaseDay();
        }
    }

    //Attacks the closest building
    public void Attack()
    {
        target = getClosestBuilding(buildingPosArray);    //gets closest building, set as target
        while (!currentPos.Equals(target.transform.position)) {
            Debug.Log("move toward");
        moveToward(target); }                 //calls moveToward function until position is equal to target

        // Hover -> land animation
        anim.SetBool("Hover", false);
        anim.SetBool("Land", true);

        StartCoroutine(WaitForAnimation("Land", () =>
        {
            // Set the attacking animation
            anim.SetBool("Land", false);
            anim.SetBool("Attack", true);

            //damage building
            Building targetBuilding = target.transform.gameObject.GetComponent<Building>();
            if (targetBuilding != null)
            {
                //remove health
                targetBuilding.Damage(damage);
            }
            // Reset cooldown timer
            cooldown = cooldownTime;

            // After the attack -> transition to launch
            StartCoroutine(WaitForAnimation("Attack", () =>
            {
                anim.SetBool("Attack", false);
                anim.SetBool("Launch", true);

                // launch animation -> back to hover
                StartCoroutine(WaitForAnimation("Launch", () =>
                {
                    anim.SetBool("Launch", false);
                    anim.SetBool("Hover", true);
                }));
            }));
        }));




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
        Vector2 targetPosition = target.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);


        //Changes direction of dragon based on x-axis movement
        if (targetPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true; // Face left
        }
        else if (targetPosition.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Face right
        }

        anim.SetBool("Hover", true); // Set hover animation while moving
    
}
    // routine to wait for an animation to finish
    IEnumerator WaitForAnimation(string animationName, System.Action onComplete)
    {
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        onComplete?.Invoke();
    }

    //based on which dragon attack it is, assigns attack value
    public int getAttackVal()
    {
        int val = 0;

        if (daycycle.currentDay == 5)
            val = 25;
        else if (daycycle.currentDay == 10)
            val = 35;
        else if (daycycle.currentDay == 15)
            val = 45;
        else if (daycycle.currentDay == 20)
            val = 55;

        return val;
    }

    public int getHealthVal()
    {
        int val = 0;

        if (daycycle.currentDay == 5)
            val = 100;
        else if (daycycle.currentDay == 10)
            val = 125;
        else if (daycycle.currentDay == 15)
            val = 150;
        else if (daycycle.currentDay == 20)
            val = 175;

        return val;
    }

}
