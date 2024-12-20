using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour, iDataPersistence
{
    public int currentDay;
    public float dayLength;

    private float currentTime;
    public int dragonCount;
    public int gameLength;
    public GameObject dragon;
    public Vector2 dragonStart = new Vector2(0,0);

    //references to resource node prefabs
    public GameObject woodNode;
    public GameObject stoneNode;
    public GameObject magicNode;

    //references to major components
    Player player;
    Grid grid;
    UI ui;
    GM gm;
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
        ui = UI.instance;
        gm = GM.instance;

        dayLength = 120f;
        gameLength = 21;

        AddResources();
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
        Debug.Log("Current Population: " + player.population);
        //adjust population
        player.AdjustPopulation();
        AddResources();

        //set UI
        ui.SetDaysText();

        //check if spawn dragon
        if (currentDay % dragonCount == 0)
            TriggerDragon();
        //check if game end
        else if (currentDay == gameLength)
            EndGame();
    }

    public void AddResources()
    {
        //wood
        Vector2 place = grid.RandomFreeTransform();
        Instantiate(woodNode, place, Quaternion.identity);
        //grid.SetOccupied(place, true); // Mark the position as occupied

        //stone
        place = grid.RandomFreeTransform();
        Instantiate(stoneNode, place, Quaternion.identity);
        //grid.SetOccupied(place, true); // Mark the position as occupied

        //magic
        place = grid.RandomFreeTransform();
        Instantiate(magicNode, place, Quaternion.identity);
        //grid.SetOccupied(place, true); // Mark the position as occupied
    }

    public void TriggerDragon()
    {
        Instantiate(dragon, dragonStart, Quaternion.identity);
    }

    void EndGame()
    {
        int score = player.CalculateScore();
        gm.displayStats(score, currentDay);                           
    }

    public void SaveData(ref customGameData data)
    {
        data.currentTime = this.currentTime;
        data.currentDay = this.currentDay;
    }

    public void LoadData(customGameData data)
    {
        this.currentTime = data.currentTime;
        this.currentDay = data.currentDay;
    }

}
