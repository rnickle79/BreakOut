using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public string HighScoreName { get; private set; }
    public int HighScorePoints { get; private set; }
    public string PlayerName { get; set; }

    void Awake()
    {
        if(Instance == null)
        {
            HighScoreName = "";
            HighScorePoints = 0;

            Instance = this;
            LoadHighScore();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    class HighScoreData
    {
        public string name;
        public int points;
    }

    public void SaveHighScore(string name, int points)
    {
        HighScoreData data = new HighScoreData();
        data.name = name;
        data.points = points; 
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            HighScoreName = data.name;
            HighScorePoints = data.points;
        }
    }
}
