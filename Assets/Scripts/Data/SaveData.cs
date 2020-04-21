using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveData {
    private static string path = Application.persistentDataPath + "/";

    public static void SavePlayer(PlayerData player) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path + player.NamePlayer, FileMode.Create);

        formatter.Serialize(stream, player);
        stream.Close();
    }

    public static bool IsLoadPlayers() {
        if (Directory.Exists(path)) {
            // This path is a directory
            return HasSaveGame(path);
        }
        return false;

    }
    public static List<PlayerData> LoadPlayers() {
        if (Directory.Exists(path)) {
            // This path is a directory
            return ProcessDirectory(path);
        }
        return null;

    }

    public static PlayerData ProcessFile(string path) {

        if (File.Exists(path)) {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;

        } else {
            Debug.Log("Não tem nenhum arquivo para esse caminho: " + path);
            return null;
        }
    }

    public static List<PlayerData> ProcessDirectory(string targetDirectory) {
        List<PlayerData> result = new List<PlayerData>();
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries) {
            string[] v = fileName.Split('/');
            if (!v[v.Length - 1].StartsWith(".")) {
                result.Add(ProcessFile(fileName));
            }
        }
        return result;
    }

    public static bool HasSaveGame(string targetDirectory) {
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        if (fileEntries.Length > 0) {
            return true;
        }

        return false;
    }

}
