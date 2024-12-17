using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Description: Class GM is a button handling class, responsible for loading, saving, and quitting the game
 * 
*/
public class GM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void continueGame()
    {
        //TODO
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void saveGame()
    {
        //TODO
    }
}
