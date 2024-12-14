using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game Manager script
public class Player : MonoBehaviour
{
    public int happiness { get; private set; }
    [SerializeField] private float happyRange;  //at this percentage or higher, town is considered "happy"
    [SerializeField] private float sadRange;    //at this percentage or lower, town is considered "sad"
    public int population;
    public int buildingScore;
    //public List<Building> buildingList;
    public float totalProductionBoost;
    public int nodeChargesPerCollect;       //how many charges are removed from a resource node when it's clicked

    Inventory inventory;

    #region Singleton
    public static Player instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start()
    {
        //set inventory reference
        inventory = Inventory.instance;
    }

    //triggers node to collect their resources
    public void CollectResource(ResourceNode node)
    {
        node.RemoveResource(nodeChargesPerCollect);
    }

    public void PlaceBuilding()
    {
        //Puts a selected building in the town while making sure it doesn’t overlap other buildings,
        //then calls Purchase() with that building’s resource costs and AddBuildingScore() with that building’s score,
        //and adds the building to buildingList with AddBuildingList()
    }

    //removes given resources from inventory
    public void Purchase(int woodCost, int stoneCost, int magicCost)
    {
        inventory.RemoveWood(woodCost);
        inventory.RemoveStone(stoneCost);
        inventory.RemoveMagic(magicCost);
    }

    //Determines how many population should be added or removed based on Player’s happiness and the housing space available in buildings
    public void AdjustPopulation()
    {
        float percent = HappinessPercent();
        
        //sad
        if (percent < sadRange)
        {
            //look through each house and remove a person until happiness is within "normal" range
        }
        
        //happy
        else if (percent >= happyRange)
        {
            //look through each house and add a person until happiness is within "normal" range
        }
       
        //normal
        else
        {
            //continue on
        }

        RedistributePopulation();   //now that there's enough people, make sure they're evenly distributed 
        //set population = number of people in all the houses
    }

    public float HappinessPercent()
    {
        return happiness / population;
    }

    //Evenly distribute population over every house in buildingList
    public void RedistributePopulation()
    {

    }

    #region Add & Remove from variables

    public void AddHappiness(int amount) => happiness += amount;
    public void SubtractHappiness(int amount) => happiness -= amount;
    public void AddBuildingScore(int score) => buildingScore += score;
    public void SubtractBuildingScore(int score) => buildingScore -= score;
    public void AddBuildingList(Building building)
    {

    }
    public void RemoveBuildingList(Building building)
    {

    }
    public void AddProductionBoost(int amount) => totalProductionBoost += amount;
    public void SubtractProductionBoost(int amount) => totalProductionBoost -= amount;

    #endregion
}
