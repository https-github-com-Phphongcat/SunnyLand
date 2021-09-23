using System.Collections;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class PlayerData
{
    public int Score;

    public int Health;
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

        BinaryFormatter formatter = new BinaryFormatter();

        PlayerData playerData = new PlayerData(player);

        FileStream file = File.Open(path, FileMode.Open);
        formatter.Serialize(file, playerData);
        file.Close();
    }

    public void LoadPlayerData(PlayerControl player)
    {
        string path = Application.persistentDataPath + "/" + PlayerFile;
        if(!File.Exists(path))
        {
            FirstSave(player, path);
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);
        PlayerData playerData = formatter.Deserialize(file) as PlayerData;
        file.Close();

        Vector3 position = new Vector3(playerData.Position[0], playerData.Position[1], playerData.Position[2]);

        player.characterStats.Score(playerData.Score);
        player.transform.position = position;
    }

    void FirstSave(PlayerControl player, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        PlayerData playerData = new PlayerData(player);

        FileStream file = File.Open(path, FileMode.Create);
        formatter.Serialize(file, playerData);
        file.Close();
    }
}