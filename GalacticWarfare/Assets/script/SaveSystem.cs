using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int highScore;
    public int totalEnemiesDestroyed;
}

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(path)) return new SaveData();
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }
}