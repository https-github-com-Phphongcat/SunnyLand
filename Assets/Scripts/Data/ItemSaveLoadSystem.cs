using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ItemData
{
    public bool isDead;
    public ItemType itemType;

    public float[] Position;
}

[System.Serializable]
public enum ItemType
{
    DIAMOND = 0,
}

[System.Serializable]
public class ItemSaveLoadSystem : MonoBehaviour
{
    private const string itemsFile = "/Items.save";

    public void SaveData(ItemControl[] listItem)
    {
        string path = Application.persistentDataPath + "/Game/" + itemsFile;

        BinaryFormatter formatter = new BinaryFormatter();

        for(int Count = 0; Count < listItem.Length; Count++)
        {
            ItemData itemData = new ItemData();

            if(listItem[Count] == null)
            {
                itemData.isDead = true;
            }
            else
            {
                Vector3 v3 = listItem[Count].transform.position;

                itemData.isDead = false;
                itemData.itemType = listItem[Count].ItemType();
                itemData.Position = new float[]{ v3.x, v3.y, v3.z };
            }
            
            FileStream file = File.Open(path + Count.ToString(), FileMode.Open);
            formatter.Serialize(file, itemData);
            file.Close();
        }
    }

    public void LoadData(ItemControl[] listItem, List<ItemControl> itemPrefab)
    {
        string path = Application.persistentDataPath + "/Game/" + itemsFile;

        BinaryFormatter formatter = new BinaryFormatter();

        for(int Count = 0; Count < listItem.Length; Count++)
        {
            if(!File.Exists(path + Count.ToString()))
            {
                FirstSave(listItem[Count], path + Count.ToString());
            }
            else
            {
                FileStream file = File.Open(path + Count.ToString(), FileMode.Open);
                ItemData itemData = formatter.Deserialize(file) as ItemData;
                file.Close();

                if(itemData.isDead)
                {
                    if(listItem[Count] != null)
                    {
                        Destroy(listItem[Count].gameObject);
                    }
                }
                else
                {
                    if(listItem[Count] == null)
                    {
                        int index = ((int)listItem[Count].ItemType());
                        listItem[Count] = Instantiate<ItemControl>(itemPrefab[index]);
                    }
                    listItem[Count].transform.position = new Vector3(itemData.Position[0], itemData.Position[1], itemData.Position[2]);
                }
            }
        }
    }

    void FirstSave(ItemControl item, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        ItemData itemData = new ItemData();

        Vector3 v3 = item.transform.position;

        itemData.isDead = false;
        itemData.itemType = item.ItemType();
        itemData.Position = new float[]{ v3.x, v3.y, v3.z };

        FileStream file = File.Open(path, FileMode.Create);
        formatter.Serialize(file, itemData);
        file.Close();
    }
}