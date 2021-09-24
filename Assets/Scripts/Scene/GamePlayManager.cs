using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using Cinemachine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class GamePlayManager : MonoBehaviour
{
    private ItemControl[] listItem;

    [SerializeField] private int frameRate;
    [SerializeField] private PlayerControl playerPrefab;
    [SerializeField] private List<ItemControl> itemPrefab;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private Text textScore;

    private PlayerControl player;
    private DataManager dataManager;

    void Awake()
    {
        Application.targetFrameRate = frameRate;

        listItem = FindObjectsOfType<ItemControl>();
        player = FindObjectOfType<PlayerControl>();

        dataManager = new DataManager();
        OnLoad();
    }

    void UpdateScore(){
        textScore.text = "Score: " + player.characterStats.Score().ToString();
    }

    void FixedHandlePlayer()
    {
        if(cinemachineVirtualCamera.Follow == null){
            cinemachineVirtualCamera.Follow = FindObjectOfType<PlayerControl>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SavePointPlayer();
        PlayerRepawn();
        FixedHandlePlayer();
        UpdateScore();
    }

    void SavePointPlayer()
    {
        if(player.characterStats.SavePoint)
        {
            player.characterStats.SavePoint = false;
            OnSave();
        }
    }

    void PlayerRepawn()
    {
        if(player.characterStats.IsDead())
        {
            Destroy(player.gameObject);
            player = Instantiate<PlayerControl>(playerPrefab);
            OnLoad();
            cinemachineVirtualCamera.Follow = player.transform;
        }
    }

    void OnSave()
    {
        dataManager.playerSaveSystem.SavePlayerData(player);
        dataManager.itemSaveSystem.SaveData(listItem);
    }

    void OnLoad()
    {
        dataManager.playerSaveSystem.LoadPlayerData(player);
        dataManager.itemSaveSystem.LoadData(listItem, itemPrefab);
    }
}
