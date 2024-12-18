using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class customGameData
{
    //Data to save
    public int currentDay;
    public int woodResources;
    public int stoneResources;
    public int magicResources;
    public int population;
    public float currentTime;
    public int woodCharges;
    public int stoneCharges;
    public int magicCharges;

    //Lists to save
    public List<Building> buildingList;
    public List<House> houseList;
    public List<Transform> buildingPositions;

    //constructor for a new game
    //stores default values when a new game is created
    public customGameData()
    {
        //initialize vars to default vals
        this.currentDay = 0;
        this.woodResources = 0;
        this.stoneResources = 0;
        this.magicResources = 0;
        this.population = 10;

        //create new empty lists
        this.buildingList = new List<Building>();
        this.houseList = new List<House>();
        this.buildingPositions = new List<Transform>();
    }
}
