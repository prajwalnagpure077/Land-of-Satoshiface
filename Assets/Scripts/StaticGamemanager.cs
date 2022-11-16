using Newtonsoft.Json;
using UnityEngine;

public static class StaticGamemanager
{
    static StaticConstructor staticConstructor = new();
    public static GameDataStructure gameDataStructure = new();
    public static S_Collections s_Collections;

    public static void loadGameDataStructure()
    {
        string loadedString = PlayerPrefs.GetString("GameDataStructure");
        if (loadedString.Length > 0)
        {
            try
            {
                gameDataStructure = JsonConvert.DeserializeObject<GameDataStructure>(loadedString);
            }
            catch (System.Exception)
            {
                SaveGameDataStructure(true);
            }
        }
        else
        {
            SaveGameDataStructure(true);
        }

        s_Collections = Resources.Load<S_Collections>("S_Collections");
    }

    public static void SaveGameDataStructure(bool newObj = false)
    {
        string serializedData = JsonConvert.SerializeObject((gameDataStructure == null || newObj) ? new GameDataStructure() : gameDataStructure);
        PlayerPrefs.SetString("GameDataStructure", serializedData);
    }
}


public class StaticConstructor
{
    public StaticConstructor()
    {
        StaticGamemanager.loadGameDataStructure();
    }
}