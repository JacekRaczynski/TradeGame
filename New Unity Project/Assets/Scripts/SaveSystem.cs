
using UnityEngine;
using System.IO; // biblioteka pozwalajaca operowaæ na piliach
using System.Runtime.Serialization.Formatters.Binary; //biblioteka odpowiedzialna za szyfrowanie 
public static class SaveSystem 
{
    public static void SavePlayer(GameManager player)
    {
        Debug.Log("Save");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(player);
        Debug.Log("Save data saved : " + data.ToString());
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GameData LoadPlayer()
    {
      
        string path = Application.persistentDataPath + "/player.txt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
           //sad
            GameData data = formatter.Deserialize(stream) as GameData;
            data.loadGameData();
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
    public static void resetPlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(new GameManager());
        Debug.Log("Reset data : " + data.ToString());
        data.loadGameData();
        formatter.Serialize(stream, data);
        stream.Close();
    }
}

