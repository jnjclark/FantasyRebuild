using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * Description: Class GM is a button handling class, responsible for loading, saving, and quitting the game
 * 
*/
public class GM : MonoBehaviour
{
    public DataPersistenceManager dataManager;
    public Text scoreBox;
    public Text dayBox;
    public GameObject dragon;
    #region Singleton
    public static GM instance;

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
        dataManager = DataPersistenceManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        dataManager.NewGame();
    }

    public void continueGame()
    {
        dataManager.LoadGame();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void saveGame()
    {
        dataManager.SaveGame();
    }

    public void displayStats(int score, int days)
    {
        scoreBox.text = "Score: " + score;
        dayBox.text = "Days Survived: " + days;
    }

    public void spawnDragon()
    {
        DayCycle.instance.TriggerDragon();
    }
}
