using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    private ItemControl[] listItem;

    [SerializeField] private int frameRate;
    [SerializeField] private PlayerControl playerPrefab;
    [SerializeField] private List<ItemControl> itemPrefab;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private Text textScore;
    [SerializeField] private ItemSaveLoadSystem itemSaveLoadSystem;
    [SerializeField] private PlayerSaveLoadSystem playerSaveLoadSystem;
    
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
        playerSaveLoadSystem.SavePlayerData(player);
        itemSaveLoadSystem.SaveData(listItem);
    }

    void OnLoad()
    {
        playerSaveLoadSystem.LoadPlayerData(player);
        itemSaveLoadSystem.LoadData(listItem, itemPrefab);
    }
}
