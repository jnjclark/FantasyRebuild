using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public int currentDay;
    public float dayLength;

    private float currentTime;
    public int dragonCount;
    public int gameLength;
    public GameObject dragon;
    public Vector2 dragonStart;

    //references to resource node prefabs
    public GameObject woodNode;
    public GameObject stoneNode;
    public GameObject magicNode;

    //references to major components
    Player player;
    Grid grid;

    #region Singleton
    public static DayCycle instance;

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

    // Start is called before the first frame update
    void Start()
    {
        //set references
        player = Player.instance;
        grid = Grid.instance;

        /* TODO
         
         * Set gamelength (days) last dragon battle day + 1
        */

        dayLength = 120f;
        gameLength = 11;
    }
    
    // Update is called once per frame
    void Update()
    {
        //timer for length of day
        if (currentTime <= dayLength)
            currentTime += Time.deltaTime;
        else
        {
            currentTime = 0;
            IncreaseDay();
        }
    }

    public void IncreaseDay()
    {
        currentDay++;

        //adjust population
        player.AdjustPopulation();

        //check if spawn dragon
        if (currentDay % dragonCount == 0)
            TriggerDragon();
        //check if game end
        else if (currentDay == gameLength)
            EndGame();//EndGame()?
    }

    public void AddResources()
    {
        //wood
        Vector2 place = grid.RandomFreeTransform();
        Instantiate(woodNode, place, Quaternion.identity);
        grid.SetOccupied(place, true); // Mark the position as occupied

        //stone
        place = grid.RandomFreeTransform();
        Instantiate(stoneNode, place, Quaternion.identity);
        grid.SetOccupied(place, true); // Mark the position as occupied

        //magic
        place = grid.RandomFreeTransform();
        Instantiate(magicNode, place, Quaternion.identity);
        grid.SetOccupied(place, true); // Mark the position as occupied
    }

    void TriggerDragon()
    {
        Instantiate(dragon, dragonStart, Quaternion.identity);
    }

    void EndGame()
    {
        player.CalculateScore();
    }

}
