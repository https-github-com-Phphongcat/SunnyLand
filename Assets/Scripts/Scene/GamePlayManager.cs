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
    [SerializeField] private ItemSaveSystem itemSaveSystem;
    [SerializeField] private PlayerSaveSystem playerSaveSystem;
    
    private PlayerControl player;

    void Awake()
    {
        Application.targetFrameRate = frameRate;

        listItem = FindObjectsOfType<ItemControl>();
        player = FindObjectOfType<PlayerControl>();

        OnLoad();
    }

    void ShowScore(){
        textScore.text = "Score: " + player.Score.ToString();
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
    }

    void FixedUpdate()
    {
        ShowScore();
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
            player.Score = 0;
            OnLoad();
            cinemachineVirtualCamera.Follow = player.transform;
        }
    }

    void OnSave()
    {
        playerSaveSystem.SavePlayerData(player);
        itemSaveSystem.SaveData(listItem);
    }

    void OnLoad()
    {
        playerSaveSystem.LoadPlayerData(player);
        itemSaveSystem.LoadData(listItem, itemPrefab);
    }
}
