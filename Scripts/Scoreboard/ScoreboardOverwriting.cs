using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ScoreboardOverwriting : MonoBehaviour
{
    public static void ClearScoresStream()
    {

        System.Threading.Thread.Sleep(0350);

        string path = Application.dataPath + "/scoreboardScoresStream.txt";

        
        Debug.Log("Loading Scores Stream File..");
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.Close();
        }
        
    }
}
