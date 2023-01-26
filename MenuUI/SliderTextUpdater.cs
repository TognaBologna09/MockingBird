using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextUpdater : MonoBehaviour
{

    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI sliderText;

    [SerializeField] private FloatVariable tempoVariable;

    void Start()
    {
        slider.onValueChanged.AddListener((v) => sliderText.text = v.ToString("0.0"));
        slider.onValueChanged.AddListener((w) => tempoVariable.Value = (float) w);
    }

}
