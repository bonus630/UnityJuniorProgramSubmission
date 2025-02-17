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
    public Datas playersScores;


   
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
        if (string.IsNullOrEmpty(ScoreManager.Instance.currentPlayer))
            return;
        if (playersScores == null)
            playersScores =new  Datas();
        int index = -1;

        Data data = new Data();
        data.name = ScoreManager.Instance.currentPlayer;
        data.points = ScoreManager.Instance.points;
        
        for (int i = playersScores.Count-1; i >= 0; i--)
        {
            if (playersScores[i].points < data.points)
                index = i;
        }
        Debug.Log("1:" + index);
        if (index > -1)
        {
            playersScores.PlayerScores.Insert(index, data);

        }
        if (playersScores.Count < totalScore) {
            Debug.Log("2:" + index);
            if (index == -1)
                playersScores.PlayerScores.Add(data);
        }
        else 
        {
            playersScores.PlayerScores.RemoveAt(5);
        }
        string json = JsonUtility.ToJson(playersScores);
        Debug.Log(json);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "score.json"), json);

    }
    public void Load()
    {
        string path = Path.Combine(Application.persistentDataPath, "score.json");
        if (!File.Exists(path))
            return;
        string json = File.ReadAllText(path);
        playersScores = JsonUtility.FromJson<Datas>(json);
      

    }
    [System.Serializable]
    public class Datas
    {
        public List<Data> PlayerScores;
        public Datas()
        {
            if (PlayerScores == null)
                PlayerScores = new List<Data>();
        }
        public Data this[int index] { get { return PlayerScores[index]; } set { PlayerScores[index] = value; } }
        public int Count { get { return PlayerScores.Count; } }
    }

    [System.Serializable]
    public class Data
    {
        public string name;
        public int points;
    }
}
