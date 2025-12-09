using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string fileName = "/savegame.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + fileName, json);
        Debug.Log("Jogo salvo em: " + Application.persistentDataPath + fileName);
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + fileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        Debug.Log("Nenhum arquivo de save encontrado.");
        return null;
    }
}
