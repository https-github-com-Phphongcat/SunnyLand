using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public int Score;
    public float[] Position;
    
    public PlayerData(PlayerControl player)
    {
        Vector3 Pos3 = player.transform.position;

        this.Score = player.characterStats.Score();

        this.Position = new float[]{
            Pos3.x, Pos3.y, Pos3.z
        };
    }
}

[System.Serializable]
public class PlayerSaveSystem : MonoBehaviour
{
    private const string PlayerFile = "Player.save";

    public void SavePlayerData(PlayerControl player)
    {
        string path = Application.persistentDataPath + "/" + PlayerFile;
        PlayerData playerData = new PlayerData(player);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        formatter.Serialize(file, playerData);
        file.Close();
    }

    public void LoadPlayerData(PlayerControl player)
    {
        string path = Application.persistentDataPath + "/" + PlayerFile;
        if(!File.Exists(path))
        {
            SavePlayerData(player);
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        PlayerData playerData = (PlayerData)formatter.Deserialize(file);
        file.Close();

        Vector3 position = new Vector3(playerData.Position[0], playerData.Position[1], playerData.Position[2]);

        player.characterStats.Score(playerData.Score);
        player.transform.position = position;
    }
}