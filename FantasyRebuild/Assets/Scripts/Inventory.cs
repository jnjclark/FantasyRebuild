using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int wood;
    public int stone;
    public int magic;

    //references
    UI ui;

    #region Singleton
    public static Inventory instance;

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
        //set references
        ui = UI.instance;
    }

    public void AddWood(int amount)
    {
        wood += amount;

        ui.SetWoodText();
    }

    public void RemoveWood(int amount)
    {
        wood -= amount;

        ui.SetWoodText();
    }

    public void AddStone(int amount)
    {
        stone += amount;

        ui.SetStoneText();
    }

    public void RemoveStone(int amount)
    {
        stone -= amount;

        ui.SetStoneText();
    }

    public void AddMagic(int amount)
    {
        magic += amount;

        ui.SetMagicText();
    }

    public void RemoveMagic(int amount)
    {
        magic -= amount;

        ui.SetMagicText();
    }

}
