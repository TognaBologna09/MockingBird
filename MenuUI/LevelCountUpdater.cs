using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    public IntVariable level;
   
    // Update is called once per frame
    void Update()
    {
        levelText.text = "LEVEL " + level.Value.ToString("0");
    }
}
