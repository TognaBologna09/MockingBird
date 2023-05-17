using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;
using static Scoreboard.ScoreboardStruct;

public class ScoreboardData : MonoBehaviour
{
    public static void SaveScoreStream(string name, int score)
    {
        string path = Application.dataPath + "/scoreboardScoresStream.txt";

        Debug.Log("Saving name and score to scoreboard!");

        ScoreData data = new ScoreData();
        data.name = name;
        data.score = score;
        string json = JsonUtility.ToJson(data);

        
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(json);  
        }

    }

    public static ScoreData[] LoadScoresStream()
    {
        string path = Application.dataPath + "/scoreboardScoresStream.txt";

        List<ScoreData> scores = new List<ScoreData>();
        
        Debug.Log("Loading Scores Stream File..");
        using (StreamReader reader = new StreamReader(path))
        {
            Debug.Log("Reading the Scores Stream from the File...");
            while (!reader.EndOfStream)
            {
                Debug.Log("LoadScoresStream Code taking action!");
                string json = reader.ReadLine();
                ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                scores.Add(data);
            }  
        }

        return scores.ToArray();
    }
}
