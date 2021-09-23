using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    public PlayerSaveSystem playerSaveSystem;
    public ItemSaveSystem itemSaveSystem;

    public DataManager()
    {
        playerSaveSystem = new PlayerSaveSystem();
        itemSaveSystem = new ItemSaveSystem();
    }
}