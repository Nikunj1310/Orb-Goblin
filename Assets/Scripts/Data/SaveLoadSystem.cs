using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    private static string filePath = Application.persistentDataPath + "/playerdata.json";

    public static void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public static PlayerData Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            return new PlayerData(); // Default start from Level 1
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}
