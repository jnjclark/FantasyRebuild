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
    public Player player;
    public GameObject dragon;
    public Vector2 dragonStart;
    public GameObject Grid;

    //references to resource node prefabs
    public GameObject woodNode;
    public GameObject stoneNode;
    public GameObject magicNode;

    // Start is called before the first frame update
    void Start()
    {
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

    public void IncreaseDay()
    {
        currentDay++;
        if (currentDay % dragonCount == 0)
            TriggerDragon();
        else if (currentDay == gameLength)
            EndGame();//EndGame()?
    }

    public void AddResources()
    {
        Grid gridComponent = Grid.GetComponent<Grid>();

        //wood
        Vector2 place = gridComponent.RandomFreeTransform();
        Instantiate(woodNode, place, Quaternion.identity);
        gridComponent.SetOccupied(place, true); // Mark the position as occupied

        //stone
        place = gridComponent.RandomFreeTransform();
        Instantiate(stoneNode, place, Quaternion.identity);
        gridComponent.SetOccupied(place, true); // Mark the position as occupied

        //magic
        place = gridComponent.RandomFreeTransform();
        Instantiate(magicNode, place, Quaternion.identity);
        gridComponent.SetOccupied(place, true); // Mark the position as occupied
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
