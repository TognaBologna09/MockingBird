using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Scoreboard.ScoreboardStruct;

public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] private RectTransform savedScoreContainerTemplate;

    [SerializeField] private RectTransform contentParent;

    void OnEnable()
    {
        Debug.Log("Starting Scoreboard Manager");
        VisualizeScoreboard();
    }

    
    public void VisualizeScoreboard()
    {
        System.Threading.Thread.Sleep(0200);

        //RectTransform contentParent = GameObject.Find("Scoreboard Content").GetComponent<RectTransform>();
        ScoreData[] scores = ScoreboardData.LoadScoresStream();
        Debug.Log(scores);

        for (int j = 0; j < contentParent.childCount; j++)
        {
            GameObject childTransform = contentParent.GetChild(j).gameObject;
            if (childTransform.name == "SavedScoreTemplate(Clone)")
            {

                Destroy(childTransform);
            }
        }

        foreach (ScoreData score in scores)
        {
            Debug.Log(score.name + ": " + score.score);
            
            RectTransform dataContainer = Instantiate(savedScoreContainerTemplate);
            dataContainer.SetParent(contentParent);
            dataContainer.gameObject.SetActive(true);

            for (int j = 0; j < dataContainer.childCount; j++)
            {
                Transform childTransform = dataContainer.GetChild(j);
                if (childTransform.name == "Score Text (TMP) Template")
                {
                    TextMeshProUGUI scoreText = childTransform.GetComponent<TextMeshProUGUI>();
                    scoreText.text = score.score.ToString();
                }
                if (childTransform.name == "Name Text (TMP) Template")
                {
                    TextMeshProUGUI nameText = childTransform.GetComponent<TextMeshProUGUI>();
                    nameText.text = score.name;
                }
            }

        }

    }
}
