using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

   private customGameData gameData;
   private List<iDataPersistence> dataPersistenceObjects;
   private FileDataHandler dataHandler;
   public static DataPersistenceManager instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        
    }

    public void NewGame()
    {
        this.gameData = new customGameData(); // set as a new gameData object
        SceneManager.LoadScene("Main");
    }

    public void LoadGame()
    {
        //load any saved data from a file using a data handler
        this.gameData = dataHandler.Load();

        // if no data can be found, initialize for a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults");
            NewGame();
        }

        //push the loaded data to all other scripts that need it
        foreach (iDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //pass the data to other scripts so they can update it
        foreach (iDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        //save the data to a file w/ data handler

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<iDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<iDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<iDataPersistence>();

        return new List<iDataPersistence>(dataPersistenceObjects);
    }
}
