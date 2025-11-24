using UnityEngine;
using System.IO;

[System.Serializable] // To and from JSON for saving
public class SaveData
{
    public string pancakeStatus; // Saves whether the pancake is raw, cooked, or burnt
    public int totalPancakes;   // Saves how many pancake were made in total
    public string lastQuality;  // Saves the quality of the last pancake (raw, golden, burnt)
}

public static class SaveSystem  
{
    private static string savePath = Application.persistentDataPath + "/save.json"; // Location where the save file will be stored

    public static void SaveGame(string status, int total, string quality)   // Saves the current game data into a JSON file
    {
        SaveData data = new SaveData(); // Creates a new SaveData object to gold currrent game info
        data.pancakeStatus = status;    // Fills it with the current pancake status
        data.totalPancakes = total;     // Fills in total pancake count
        data.lastQuality = quality;     // Fills in the last pancake quality

        string json = JsonUtility.ToJson(data, true);   // Converts the data into a JSON text format
        File.WriteAllText(savePath, json);  // Writes the JSON string into the save fie
        Debug.Log("Game saved to " + savePath); // Confimration message in Unity
    }

    public static SaveData LoadGame() // Loads game data back from the saved file
    {
        if (File.Exists(savePath))  // Checks if the save file actually exists 
        {
            string json = File.ReadAllText(savePath);   // Reads all text from the fie
            SaveData data = JsonUtility.FromJson<SaveData>(json);   // Converts JSON back into a SaveData object
            Debug.Log("Game loaded from " + savePath);  // Prints a message saying it worked
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found!");    // Else there is no save file that exists
            return null;
        }
    }
}
