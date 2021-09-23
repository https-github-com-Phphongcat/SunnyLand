using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private Button Continue;
    private bool isChangeButton;

    void Awake()
    {
        isChangeButton = true;
        ChangeActiveButton();
    }

    void ChangeActiveButton()
    {
        if(Continue == null){
            return;
        }

        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (FileInfo file in di.EnumerateFiles()){
            isChangeButton = false;
            break;
        }

        if(isChangeButton){
            Continue.gameObject.GetComponent<Image>().color = new Color32(251, 251, 251, 80);
            Continue.onClick = null;
        }
    }
}