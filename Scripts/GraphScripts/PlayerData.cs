using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public IntVariable score;

    public ListVariable listScores;

    public void CacheScore()
    {
        PlayerPrefs.SetInt("score", score.Value);

        listScores.AddToListInt(score.Value);
    }

    public void ClearCachedScores()
    {
        listScores.ListIntReset();
    }
}
