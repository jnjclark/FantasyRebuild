using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //show inventory
    public Text woodAmtTxt;
    public Text stoneAmtTxt;
    public Text magicAmtTxt;

    //show game info
    public Text daysAmtTxt;
    public Text popAmtTxt;
    public Text moodTxt;

    //references
    Inventory inventory;
    DayCycle dayCycle;
    Player player;

    #region Singleton
    public static UI instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        InitiateUI();
    }
    #endregion

    private void Start()
    {
        //set references
        inventory = Inventory.instance;
        dayCycle = DayCycle.instance;
        player = Player.instance;
    }

    public void SetWoodText()
    {
        woodAmtTxt.text = inventory.wood.ToString();
    }

    public void SetStoneText()
    {
        stoneAmtTxt.text = inventory.stone.ToString();
    }

    public void SetMagicText()
    {
        magicAmtTxt.text = inventory.magic.ToString();
    }

    public void SetDaysText()
    {
        daysAmtTxt.text = dayCycle.currentDay.ToString();
    }

    public void SetPopText()
    {
        popAmtTxt.text = player.population.ToString();
    }

    public void SetMoodText()
    {
        moodTxt.text = player.GetMood();
    }

    public void SelectBuilding(string str)
    {
        player.EnableBuildMode(true);

        player.SetSelectedBuilding(str);

        Debug.Log("button");
    }

    public void InitiateUI()
    {
        SetWoodText();
        SetStoneText();
        SetMagicText();
        SetDaysText();
        SetPopText();
        SetMoodText();
    }
}
