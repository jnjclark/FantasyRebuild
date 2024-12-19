using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Game Manager script
public class Player : MonoBehaviour, iDataPersistence
{
    public int happiness { get; private set; }
    [SerializeField] private float happyRange;  //at this percentage or higher, town is considered "happy"
    [SerializeField] private float sadRange;    //at this percentage or lower, town is considered "sad"
    public int population;
    public int buildingScore;
    public float minDistanceBetweenBuildings = 5.0f; // Minimum distance between buildings
    public List<Building> buildingList = new List<Building>(); // Initialize the list
    public List<House> houseList = new List<House>();        //List of all houses for population functions
    

    public float totalProductionBoost;
    public int nodeChargesPerCollect;       //how many charges are removed from a resource node when it's clicked

    private GameObject selectedBuildingPrefab;
    private bool buildModeEnabled = false;

    [Header("Score Weights")]               //how many points received for each of these
    public int pointsPerPopulation = 1;
    public int pointsPerDay = 1;

    //single instances of other classes
    DayCycle daycycle;
    Inventory inventory;
    UI ui;
    Grid grid;

    public List<Transform> buildingPositions;
    public static Transform[] buildingPosArray;

    // References to building prefabs
    public GameObject housePrefab;
    public GameObject farmPrefab;
    public GameObject wallPrefab;
    public GameObject turretPrefab;
    public GameObject tavernPrefab;
    public GameObject magicBuildingPrefab;
    public GameObject woodBuildingPrefab;
    public GameObject stoneBuildingPrefab;

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
        //set references to other classes
        inventory = Inventory.instance;
        daycycle = DayCycle.instance;
        ui = UI.instance;
        grid = Grid.instance;

        buildingPositions = new List<Transform>();
        foreach (Building building in FindObjectsOfType<Building>())
        {
            buildingPositions.Add(building.transform);
            buildingList.Add(building); // Add existing buildings to the list
        }
        buildingPosArray = buildingPositions.ToArray();
    }

    private void Update()
    {
        //don't click on things through UI
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //place building
            if (hit.collider.tag == "ground" && buildModeEnabled)
            {
                Vector2 position = grid.GetGridSnapLocation(hit.point);

                PlaceBuilding(selectedBuildingPrefab, position);

                EnableBuildMode(false);
            }
        }
    }

    //triggers node to collect their resources
    public void CollectResourceNode(ResourceNode node)
    {
        node.RemoveResource(nodeChargesPerCollect);
    }

    //triggers building to collect their resources
    public void CollectResourceBuilding(ResourceBuilding building)
    {
        building.Collect();
    }

    //public void PlaceBuilding(Building building, Vector2 position)
    public void PlaceBuilding(GameObject buildingPrefab, Vector2 position)
    {
        if (CanPlaceBuilding(position))
        {
            Building building = buildingPrefab.GetComponent<Building>();

            // Check if the player has enough resources
            if (inventory.wood >= building.woodCost && inventory.stone >= building.stoneCost && inventory.magic >= building.magicCost)
            {
                // Deduct resources
                Purchase(building.woodCost, building.stoneCost, building.magicCost);

                // Instantiate the building at the specified position
                Instantiate(building, position, Quaternion.identity);

                // Add the building to the list
                AddBuildingList(building);

                // Add it to the House list if it's a house
                if (building is House)
                    AddHouseList(building.GetComponent<House>());

                // Add the building's position to the array
                List<Transform> buildingPositions = new List<Transform>(buildingPosArray);
                buildingPositions.Add(building.transform);
                buildingPosArray = buildingPositions.ToArray();

                // Add the building's score
                AddBuildingScore(building.score);
            }
            else
            {
                Debug.Log("Not enough resources to place the building.");
            }
        }
        else
        {
            Debug.Log("Cannot place building too close to another building.");
        }

        EnableBuildMode(false); //don't place any more buildings

        ui.SetMoodText();   //update UI
    }
    public bool CanPlaceBuilding(Vector2 position)
    {
        return true;
        
        
        
        //foreach (Transform buildingPos in buildingPosArray)
        //{
        //    if (Vector2.Distance(position, buildingPos.position) < minDistanceBetweenBuildings)
        //    {
        //        return false;
        //    }
        //}
        //return true;
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
        int newPop = population;  //holds value that will be the population

        //sad
        if (percent < sadRange)
        {
            //figure out how many to remove until happiness is within "normal" range
            while (HappinessPercent(newPop) < sadRange)
            {
                newPop -= 1;
            }
        }
        
        //happy
        else if (percent >= happyRange)
        {
            //figure out how many to add until happiness is within "normal" range
            while (HappinessPercent(newPop) > happyRange)
            {
                newPop += 1;
            }
        }

        //normal - do nothing
        else
        {
            //continue on
        }

        if (newPop <= 0)
        {
            Debug.Log("Lost all population");
        }

        RedistributePopulation(newPop);   //now that there's enough people, make sure they're evenly distributed 

        ui.SetPopText();    //update UI
    }

    public float HappinessPercent()
    {
        return happiness / population;
    }

    //used to calculate theoretical percent with a new population value
    public float HappinessPercent(int newPopulation)
    {
        return happiness / newPopulation;
    }

    public string GetMood()
    {
        string mood = "";

        float h = HappinessPercent();

        if (h < sadRange)
            mood = "Sad";
        else if (h >= happyRange)
            mood = "Happy";
        else
            mood = "Normal";

        return mood;
    }

    //Evenly distribute population over every house in buildingList
    //if there's more population than is possible to house, ignore them
    public void RedistributePopulation(int newPopulation)
    {
        int totalCapacity = 0;

        //how many people can be housed
        foreach (House house in houseList)
        {
            totalCapacity += house.capacity;
        }

        //discard extra people
        if (newPopulation > totalCapacity)
            newPopulation = totalCapacity;

        //set value of population
        SetPopulation(newPopulation);

        //distribute into houses
        int i = population;

        foreach (House house in houseList)
        {
            //there's enough to fill the house
            int cap = house.capacity;
            if (i >= cap)
            {
                house.SetOccupation(house.capacity);
                i -= house.capacity;
            }

            //not enough to fill the house
            else
            {
                house.SetOccupation(i);
                i = 0;
            }
        }
    }

    #region Add & Remove from variables

    public void AddHappiness(int amount) => happiness += amount;
    public void SubtractHappiness(int amount) => happiness -= amount;
    public void AddBuildingScore(int score) => buildingScore += score;
    public void SubtractBuildingScore(int score) => buildingScore -= score;
    public void AddBuildingList(Building building)
    {
        buildingList.Add(building);
    }
    public void RemoveBuildingList(Building building)
    {
        buildingList.Remove(building);
    }
    public void AddHouseList(House house)
    {
        houseList.Add(house);
    }
    public void RemoveHouseList(House house)
    {
        houseList.Remove(house);
    }
    public void AddProductionBoost(float amount) => totalProductionBoost += amount;
    public void SubtractProductionBoost(float amount) => totalProductionBoost -= amount;
    void SetPopulation(int newPopulation)
    {
        population = newPopulation;
    }

    #endregion

    public int CalculateScore()
    {
        int score = 0;

        //how many days player survived
        score += (daycycle.currentDay * pointsPerDay);

        //population
        score += (population * pointsPerPopulation);

        //buildingScore
        score += buildingScore;

        return score;
    }


    public void EnableBuildMode(bool boolean)
    {
        buildModeEnabled = boolean;
    }

    public void SetSelectedBuilding(string str)
    {
        switch (str)
        {
            case ("h"):
                selectedBuildingPrefab = housePrefab;
                break;
            case ("ta"):
                selectedBuildingPrefab = tavernPrefab;
                break;
            case ("f"):
                selectedBuildingPrefab = farmPrefab;
                break;
            case ("w"):
                selectedBuildingPrefab = wallPrefab;
                break;
            case ("tu"):
                selectedBuildingPrefab = turretPrefab;
                break;
            case ("wb"):
                selectedBuildingPrefab = woodBuildingPrefab;
                break;
            case ("sb"):
                selectedBuildingPrefab = stoneBuildingPrefab;
                break;
            case ("mb"):
                selectedBuildingPrefab = magicBuildingPrefab;
                break;
            default:
                Debug.Log("no building selected");
                break;

        }
    }

    //interface method
    public void LoadData(customGameData data)
    {
        this.population = data.population;
        this.buildingList = data.buildingList;
        this.houseList = data.houseList;
        this.buildingPositions = data.buildingPositions;
    }

    //interface method
    public void SaveData(ref customGameData data)
    {
        data.population = this.population;
        data.buildingList = this.buildingList;
        data.houseList = this.houseList;
        data.buildingPositions = this.buildingPositions;
    }
}
