using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private Button continueGame;
    [SerializeField] private Button rank;
    private bool _isChangeContinueButton;
    private bool _isChangeRankButton;

    void Awake()
    {
        _isChangeRankButton = true;
        _isChangeContinueButton = true;

        ChangeActiveContinueButton();
        ChangeActiveRankButton();
    }

    void ChangeActiveContinueButton()
    {
        if(continueGame == null) return;

        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/Game");
        foreach (FileInfo file in di.EnumerateFiles()){
            _isChangeContinueButton = false;
            break;
        }

        if(_isChangeContinueButton)
        {
            continueGame.enabled = false;
            continueGame.gameObject.GetComponent<Image>().color = new Color32(251, 251, 251, 80);
            continueGame.onClick.AddListener(() => { });
        }
    }
    
    void ChangeActiveRankButton()
    {
        if(rank == null) return;

        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (FileInfo file in di.EnumerateFiles()){
            _isChangeRankButton = false;
            break;
        }

        if(_isChangeRankButton)
        {
            rank.enabled = false;
            rank.gameObject.GetComponent<Image>().color = new Color32(251, 251, 251, 80);
            rank.onClick.AddListener(() => { });
        }
    }
}