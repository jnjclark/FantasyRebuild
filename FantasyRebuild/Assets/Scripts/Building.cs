using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int health;
    public int woodCost;
    public int stoneCost;
    public int magicCost;
    public int score;

    protected Player player;

    private void Start()
    {
        //set player
        player = Player.instance;
    }

    //Called when the building is placed, overridden by children
    public virtual void OnPlaced()
    { 

    }

    //when damaged, reduce health by given amount
    public void Damage(int amount)
    {
        health -= amount;
        
        if (health <= 0)
        {
            DestroySelf();
        }
    }

    public virtual void DestroySelf()
    {
        //remove building from player's list
        player.RemoveBuildingList(this);

        //Destroy object
        Destroy(gameObject);
    }
}
