using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerEventUI : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;

    public void OnRightButtonDown() { characterStats.IsRight(true); }
    public void OnRightButtonUp() { characterStats.IsRight(false); }

    public void OnLeftButtonDown() { characterStats.IsLeft(true); }
    public void OnLeftButtonUp() { characterStats.IsLeft(false); }

    public void OnJumpButtonDown() { characterStats.IsJump(true); }
    public void OnJumpButtonUp() { characterStats.IsJump(false); }

    public void GoHome() {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);}

    public void WinGameGoHome() 
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (FileInfo file in di.EnumerateFiles()){
            file.Delete(); 
        }
        foreach (DirectoryInfo dir in di.EnumerateDirectories()){
            dir.Delete(true); 
        }
        Time.timeScale = 1;
        GoHome();
    }
}
