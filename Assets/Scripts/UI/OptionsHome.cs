using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class OptionsHome : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/Game");

        foreach (FileInfo file in di.EnumerateFiles()) file.Delete();
        foreach (DirectoryInfo dir in di.EnumerateDirectories()) dir.Delete(true);

        PlayGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
