using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    public int capacity;    //max amount of people in the house
    public int occupation;  //current amount of people in the house

    public bool AddPerson()
    {
        //there's enough room to add a person
        if (occupation < capacity)
        {
            occupation++;
            return true;
        }

        //not enough room, failed to add a person
        return false;
    }

    public bool RemovePerson()
    {
        //able to remove a person
        if (occupation > 0)
        {
            occupation--;
            return true;
        }

        //empty, failed to remove a person
        return false;
    }

    public void ClearPeople()
    {
        occupation = 0;
    }
    
    public void SetOccupation(int i)
    {
        occupation = i;
    }

    public override void DestroySelf()
    {
        //remove from house list
        player.RemoveHouseList(this);

        base.DestroySelf();
    }

}
