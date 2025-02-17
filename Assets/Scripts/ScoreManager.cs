using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public string currentPlayer;
    public string name;
    public int points;


   
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
        Data data = new Data();
        data.name = ScoreManager.Instance.currentPlayer;
        data.points = ScoreManager.Instance.points;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "score.json"), json);

    }
    public void Load()
    {
        string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "score.json"));
        Data data = JsonUtility.FromJson<Data>(json);
        ScoreManager.Instance.name = data.name;
        ScoreManager.Instance.points = data.points;

    }
    [System.Serializable]
    class Data
    {
        public string name;
        public int points;
    }
}
