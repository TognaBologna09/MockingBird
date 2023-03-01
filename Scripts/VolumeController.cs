using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource BGAudio;
    private AudioSource UISelect;
    private AudioSource UISlider;
    private AudioSource UICheck;

    public BoolVariable muteBool;
    public ListVariable audioSettingsList;

    void Awake()
    {
        BGAudio = GameObject.Find("BGAudio").GetComponent<AudioSource>();
        UISelect = GameObject.Find("UISelect").GetComponent<AudioSource>();
        UISlider = GameObject.Find("UISlider").GetComponent<AudioSource>();
        UICheck = GameObject.Find("UICheck").GetComponent<AudioSource>();
    }

    public void AudioMuteController()
    {
        if (muteBool.Value)
        {
            BGAudio.volume = 0;
            UISelect.volume = 0;
            UISlider.volume = 0;
            UICheck.volume = 0;
        }
        else
        {
            BGAudio.volume = audioSettingsList.listFloat[0];
            UISelect.volume = audioSettingsList.listFloat[1];
            UISlider.volume = audioSettingsList.listFloat[2];
            UICheck.volume = audioSettingsList.listFloat[3];
        }
        
    }
}
