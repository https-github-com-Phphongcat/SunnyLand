using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private List<Text> namePlayers;
    [SerializeField] private List<Text> scores;

    private void Awake()
    {
        closeButton.onClick.AddListener(()=> gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        var rankSaveSystem = new RankSaveLoadSystem();
        for(int i = 0; i < rankSaveSystem.GetRankerList().Count; i++)
        {
            namePlayers[i].text = rankSaveSystem.GetRankerList()[i].Name;
            scores[i].text = "Score: " + rankSaveSystem.GetRankerList()[i].Score;
        }
    }

    public void Show() {gameObject.SetActive(true); }
}
