using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public int health;

    public void Damage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            //die
        }
    }
}
