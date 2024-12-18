using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface for other classes to implement
public interface iDataPersistence
{
    public void LoadData(customGameData data);
    public void SaveData(ref customGameData data);
}
