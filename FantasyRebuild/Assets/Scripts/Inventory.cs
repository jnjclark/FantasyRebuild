using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int wood { get; private set; }
    public int stone { get; private set; }
    public int magic { get; private set; }

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

    public void AddWood(int amount) => wood += amount;

    public void RemoveWood(int amount) => wood -= amount;

    public void AddStone(int amount) => stone += amount;

    public void RemoveStone(int amount) => stone -= amount;

    public void AddMagic(int amount) => magic += amount;

    public void RemoveMagic(int amount) => magic -= amount;
}
