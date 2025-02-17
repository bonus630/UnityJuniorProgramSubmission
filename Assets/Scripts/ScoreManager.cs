using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string currentPlayer;
    public int points;
    private int totalScore = 5;
    public List<Data> playersScores;


   
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        if (playersScores == null)
            playersScores = new List<Data>();
        int index = -1;

        Data data = new Data();
        data.name = ScoreManager.Instance.currentPlayer;
        data.points = ScoreManager.Instance.points;
        
        for (int i = playersScores.Count; i >= 0; i--)
        {
            if (playersScores[i].points < data.points)
                index = i;
        }
        if (index > -1)
        {
            playersScores.Insert(index, data);

        }
        if (playersScores.Count < totalScore) { 
            if(index == -1)
                playersScores.Add(data);
        }
        else 
        {
            playersScores.RemoveAt(5);
        }
        string json = JsonUtility.ToJson(playersScores);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "score.json"), json);

    }
    public void Load()
    {
        string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "score.json"));
        playersScores = JsonUtility.FromJson<List<Data>>(json);
      

    }
    [System.Serializable]
    public class Data
    {
        public string name;
        public int points;
    }
}
