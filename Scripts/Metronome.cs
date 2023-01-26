using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metronome : MonoBehaviour
{
    // Variables to store the tempo, beat duration, and current time

    public FloatVariable tempo;
    private float beatDuration;
    private float currentTime;

    public BoolVariable isRhythmMode;
    public BoolVariable isWithMet;

    public BoolVariable isGameSequencing;
    public BoolVariable isPlayerSequencing;

    // Audio clip to play for each beat
    public AudioSource beatSound;

    void Start()
    {
        // Calculate the beat duration based on the tempo
        beatDuration = 60.0f / tempo.Value;

        // Load the audio clip
        beatSound = GameObject.Find("MetronomeAudio").GetComponent<AudioSource>();
    }

    void Update()
    {

        beatDuration = 60.0f / tempo.Value;
        
        if (!isRhythmMode)
        {
            
        }
        else
        {
            if(!isWithMet)
            {

            }
            else
            {

                if(isGameSequencing)
                {

                    if (isPlayerSequencing)
                    {

                    }

                    else
                    {
                        // Update the current time based on the elapsed time since the last frame
                        currentTime += Time.deltaTime;


                        // If the current time has reached the beat duration, play the audio clip and reset the current time
                        if (currentTime >= beatDuration)
                        {
                            currentTime = 0.0f;
                            //if(beatDuration < 0.5f)
                            //{
                            //    beatSound.Play(Convert.ToUInt64(beatDuration));
                            //}

                            beatSound.PlayScheduled(Convert.ToDouble(beatDuration));

                        }
                    }
                }
               
            }
        }
    }

    // Function to change the tempo
    public void SetTempo(float newTempo)
    {
        tempo.Value = newTempo;
        beatDuration = 60.0f / tempo.Value;
    }
}
