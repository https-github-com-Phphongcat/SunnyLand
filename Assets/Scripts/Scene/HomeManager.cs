using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private Button continueGame;
    [SerializeField] private Button rank;

    void Awake()
    {
        var path = Application.persistentDataPath;
        var pathGameData = $"{path}/Game";
        
        CreateDataPathIfNotExist(pathGameData);
        SetStatusButton(pathGameData, continueGame);
        SetStatusButton(path, rank);
    }

    private void CreateDataPathIfNotExist(string path)
    {
        var isCreatePath = false;
        try
        {
            var di = new DirectoryInfo(path);
            if (di.Exists == false) isCreatePath = true;
        }
        catch (Exception) { isCreatePath = true; }

        if (isCreatePath) Directory.CreateDirectory(path);
    }

    private void SetStatusButton(string path, Button button)
    {
        if(button == null) return;

        var di = new DirectoryInfo(path);
        if(di.EnumerateFiles().Any()) return;

        button.enabled = false; 
        button.gameObject.GetComponent<Image>().color = new Color32(251, 251, 251, 80); 
        button.onClick.AddListener(() => { });
    }
}