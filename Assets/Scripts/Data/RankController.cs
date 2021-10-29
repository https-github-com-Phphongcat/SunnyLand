using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class RankController : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private InputField inputField;
    [SerializeField] private PlayerControl player;

    private void Awake()
    {
        button.onClick.AddListener(WinGameGoHome);
    }

    private void Update()
    {
        if (player == null) player = FindObjectOfType<PlayerControl>();
        
        button.enabled = inputField.text != "";
    }

    private void GoHome() {SceneManager.LoadScene(0);}

    private void WinGameGoHome()
    {
        SaveRank();
        
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/Game");
        if (di.EnumerateFiles().Any())
        {
            foreach (FileInfo file in di.EnumerateFiles()) file.Delete();
            foreach (DirectoryInfo dir in di.EnumerateDirectories()) dir.Delete(true);
        }

        Time.timeScale = 1;
        GoHome();
    }

    void SaveRank()
    {
        var rankSystem = new RankSaveSystem();
        rankSystem.SaveRank(inputField.text, player.Score);
    }
}

[System.Serializable]
public class DataRanker
{
    public string Name;
    public int Score;

    public DataRanker(string name, int score)
    {
        Name = name;
        Score = score;
    }
}

[System.Serializable]
public class RankSaveSystem
{
    private const string RankFile = "Rank.save";
    private List<DataRanker> _listRanker;

    public RankSaveSystem()
    {
        _listRanker = new List<DataRanker>();
        LoadRank();
    }

    public void SaveRank(string name, int score)
    {
        var dataRanker = new DataRanker(name, score);
        
        if(_listRanker.Count <= 0) _listRanker.Add(dataRanker);
        else
        {
            var data = new DataRanker(_listRanker[_listRanker.Count - 1].Name,
                _listRanker[_listRanker.Count - 1].Score);
            
            var isChange = false;
            var indexList = 0;
            while (isChange == false && indexList < _listRanker.Count)
            {
                if(_listRanker[indexList].Score < dataRanker.Score)
                {
                    isChange = true;
                    for (var j = _listRanker.Count - 2; j >= indexList; j--)
                    {
                        _listRanker[j + 1].Name = _listRanker[j].Name;
                        _listRanker[j + 1].Score = _listRanker[j].Score;
                    }

                    _listRanker[indexList].Name = dataRanker.Name;
                    _listRanker[indexList].Score = dataRanker.Score;
                }

                indexList += 1;
            }

            if (_listRanker.Count < 4)
                _listRanker.Add(isChange == false ? dataRanker : data);
        }

        var index = 0;
        foreach (var ranker in _listRanker)
        {
            File.WriteAllText(Application.persistentDataPath + "/" + index + RankFile, JsonUtility.ToJson(ranker));
            index++;
        }
    }

    private void LoadRank()
    {
        _listRanker.Clear();
        for (int i = 0; i < 5; i++)
        {
            var path = Application.persistentDataPath + "/" + i + RankFile;
            if (File.Exists(path))
            {
                var data = JsonUtility.FromJson<DataRanker>(File.ReadAllText(path));
                _listRanker.Add(data);
            }
        }
    }

    public List<DataRanker> GetRankerList()
    {
        return _listRanker;
    }
}
