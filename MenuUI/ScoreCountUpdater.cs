using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public IntVariable level;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + level.Value.ToString("0");
    }
}
