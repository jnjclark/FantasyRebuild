using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileDataHandler
{
    //data directory path
    private string dataDirPath = "";

    //data file name
    private string dataFileName = "";

    //constructor initializes the file name and the path
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    //loads game data from file
    public customGameData Load()
    {
        //gets path of save file, different OS use different methods
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        customGameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                //load the serialized data from the file
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //read data and store in variable
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from Json back into C# object
                loadedData = JsonUtility.FromJson<customGameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.Log("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    //saves game data to file
    public void Save(customGameData data)
    {
        //gets path of save file, different OS use different methods
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //create the directory the file will be written to in the event it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize C# gamedata into JSON string, true means it will format it for us - improves readability.
            string dataToStore = JsonUtility.ToJson(data, true);

            //write serialized data to the file, using() ensures data file is closed when done writing to it
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log("Error occured when trying to save data to file: " + fullPath + "\n" +  e);
        }
    }

}
