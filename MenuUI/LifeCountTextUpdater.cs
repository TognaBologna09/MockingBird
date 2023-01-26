using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeCountTextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeText;

    public IntVariable lives;

    // Update is called once per frame
    void Update()
    {
        lifeText.text = lives.Value.ToString("0");
    }
}
