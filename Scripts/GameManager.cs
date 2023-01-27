using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    // UI Elements
    [SerializeField]
    private GameObject playView;

    // Default Game variables
    public IntVariable level;
    public IntVariable lives;

    List<string> optionsFour = new List<string>() { "a", "b", "c", "d" };
    List<string> rhtyhmOptions = new List<string>() { "qn", "en", "sn" };
    List<(string, string)> rhythmSequence = new List<(string, string)>();

    public List<string> sequenceBasic = new List<string>(); 
    public List<string> userSequenceBasic = new List<string>();

    static System.Random random = new System.Random();

    private int optionAdditionCount = 1;

    // Default Booleans to sequence Update Loop    
    public BoolVariable gameSequencing;
    public BoolVariable playerSequencing;
 
    public bool accuracyCheckComplete = false;
    public bool accurateSequenceCase = false;
    public bool trialCase = false;
    public bool lengthCheckComplete = false;

    // Default Sequence Timer variables
    public IntVariable periodMs;
    public FloatVariable tempo;

    private float beatDuration;

    // Chaos Mode Game Variables
    public BoolVariable chaosMode;
    private int previousChaoticOptionCount = 0;
    private int chaoticOptionCount = 0;
    private bool chaoticOptionComplete = false;

    // Audio
    public AudioSource sourceN;
    public AudioSource sourceE;
    public AudioSource sourceS;
    public AudioSource sourceW;

    // Animators
    public Animator redLight;
    public Animator greenLight;

    // Cancellation Tokens and Sources
    CancellationTokenSource source = null;
    //CancellationToken token = source.Token;

    public bool isDebugMode;

    private void Start()
    {
        level.Value = 0;
        lives.Value = 2;
        tempo.Value = 60;
        optionAdditionCount = 1;
        chaosMode.Value = false;
    }

    public async Task MainBasic()
    { 

        int randomOption = random.Next(optionsFour.Count);

        source = new CancellationTokenSource();

        if (gameSequencing)
        {
           
            if (sequenceBasic.Count < level.Value + 1 && lives.Value > 0)
            {
                if (isDebugMode)
                {
                    Debug.Log("MainBasic Adding to Sequence Statement");
                }
                
                // the sequence length is not sufficient for the level; add a random tone to sequence
                sequenceBasic.Add(optionsFour[randomOption]);

                playerSequencing.Value = false;
                gameSequencing.Value = true;

                await AsyncSequence(sequenceBasic, beatDuration, lives.Value, source, isDebugMode);
                
                gameSequencing.Value = false;
                playerSequencing.Value = true;

            }


        }

    }

    public async Task MainChaos()
    {

        source = new CancellationTokenSource();

        if (gameSequencing)
        {
            if (sequenceBasic.Count < level.Value + chaoticOptionCount && lives.Value > 0)
            {
                if (isDebugMode)
                {
                    Debug.Log("MainChaos Adding to Sequence Statement");
                }

                // the sequence length is not sufficient for the level; add a random tone to sequence
                for(int i = 0; i < chaoticOptionCount; i++)
                {
                    int randomOption = random.Next(optionsFour.Count);
                    sequenceBasic.Add(optionsFour[randomOption]);
                }

                playerSequencing.Value = false;
                gameSequencing.Value = true;

                await AsyncSequence(sequenceBasic, beatDuration, lives.Value, source, isDebugMode);
                
                gameSequencing.Value = false;
                playerSequencing.Value = true;

                chaoticOptionComplete = false;

            }

        }

    }

    private void MainChaosThread()
    {
        if (!chaoticOptionComplete)
        {
            chaoticOptionCount = UnityEngine.Random.Range(1, 3);
            optionAdditionCount = chaoticOptionCount;
            chaoticOptionComplete = true;
        }
        
   
    }

    public void Reset()
    {
        Animator animN = GameObject.Find("NoiseButton 1").GetComponent<Animator>();
        animN.Play("Normal"); 
        Animator animE = GameObject.Find("NoiseButton 2").GetComponent<Animator>();
        animE.Play("Normal");
        Animator animS = GameObject.Find("NoiseButton 3").GetComponent<Animator>();
        animS.Play("Normal");
        Animator animW = GameObject.Find("NoiseButton 4").GetComponent<Animator>();
        animW.Play("Normal");

        userSequenceBasic.Clear();
        sequenceBasic.Clear();

        lives.Value = 2;
        level.Value = 0;

        source = new CancellationTokenSource();
    }

    private void Update()
    {

        beatDuration = 60.0f / tempo.Value;

        
        
        if(!chaosMode.Value)
        {
            optionAdditionCount = 1;

            if (!gameSequencing.Value && sequenceBasic.Count < level.Value + optionAdditionCount && playView.activeInHierarchy && lives.Value >= 1)
            {
                if (isDebugMode)
                {
                    Debug.Log("Update loop MainBasic Method Call");
                    Debug.Log("The number of tones added is: " + optionAdditionCount);
                }

                gameSequencing.Value = true;

                MainBasic();

            }
        }
        else
        {
            if (!chaoticOptionComplete)
            {
                previousChaoticOptionCount = chaoticOptionCount;
            }

            MainChaosThread();
            
            

            if(level.Value == 0)
            {
                if(!gameSequencing.Value && playView.activeInHierarchy && lives.Value >= 1)
                {
                    gameSequencing.Value = true;
                    
                    if (isDebugMode)
                    {

                        Debug.Log("The number of chaotic tones added is: " + chaoticOptionCount);
                        Debug.Log(sequenceBasic.Count + "should be less than" + (level.Value + chaoticOptionCount));
                    }
                    MainChaos(); 
                }
               
            }

            if (!gameSequencing.Value && sequenceBasic.Count < level.Value + previousChaoticOptionCount && playView.activeInHierarchy && lives.Value >= 1)
            {
                if (isDebugMode)
                {
                    Debug.Log("Update loop MainChaos Method Call");
                    Debug.Log("The number of tones added is: " + chaoticOptionCount);
                }

                gameSequencing.Value = true;

                MainChaos();

            }
        }
        

        SequenceAccuracyCheck();

        AccuracyCaseThread();

        SequenceLengthCheck();


    }

    public void RepeatSequence()
    {
        
        if (lives.Value > 0)
        {
            playerSequencing.Value = false;
            gameSequencing.Value = true;

            source = new CancellationTokenSource();
            var token = source.Token;

            AsyncSequence(sequenceBasic, beatDuration, lives.Value, source, isDebugMode);
            gameSequencing.Value = false;
            playerSequencing.Value = true;
        }
        
    }

    //async static Task DelayedRepeatSequence(List<string> tones, float period, int lifeCount, CancellationTokenSource tokenSource, bool isDebug)
    //{

    //    Task.Delay(750);

    //    if (lifeCount > 0)
    //    {
    //        //playerSequencing.Value = false;
    //        //gameSequencing.Value = true;

    //        tokenSource = new CancellationTokenSource();
    //        var token = tokenSource.Token;

    //        AsyncSequence(tones, period, lifeCount, tokenSource, isDebug);
    //        //gameSequencing.Value = false;
    //        //playerSequencing.Value = true;
    //    }
    //}

    async static Task AsyncSequence(List<string> tones, float period, int lifeCount, CancellationTokenSource tokenSource, bool isDebug)
    {
        if (isDebug)
        {
            Debug.Log("Async Commencing");
            Debug.Log("Lives: " + lifeCount);
        }
        
        // First check if there are enough lives available, then commence
        if (lifeCount > 0)
        {
            if (isDebug)
            {
                Debug.Log("Async 1st Statement");
                Debug.Log("Token Source Status 1: " + tokenSource.Token.IsCancellationRequested);
            }

            Animator redLight = GameObject.Find("RedLight").GetComponent<Animator>();
            Animator greenLight = GameObject.Find("GreenLight").GetComponent<Animator>();

            redLight.SetTrigger("redLit");
            greenLight.SetTrigger("greenDefault");

            await Task.Delay((int)Math.Round(period * 500));

            for (var i = 0; i < tones.Count; i++)
            {
                if (isDebug)
                {
                    Debug.Log("Async For Loop");
                }
                

                await Task.Delay((int)Math.Round(period * 500), tokenSource.Token);

                //Debug.Log(i);
                // Read the randomly generated list of tones, play them
                if (tones[i] == "a")
                {
                    AudioSource sourceN = GameObject.Find("NoiseButton 1").GetComponent<AudioSource>();
                    Animator animN = GameObject.Find("NoiseButton 1").GetComponent<Animator>();

                    sourceN.Play();
                    animN.Play("Pressed");
                }
                if (tones[i] == "b")
                {
                    AudioSource sourceE = GameObject.Find("NoiseButton 2").GetComponent<AudioSource>();
                    Animator animE = GameObject.Find("NoiseButton 2").GetComponent<Animator>();

                    sourceE.Play();
                    animE.Play("Pressed");
                }
                if (tones[i] == "c")
                {
                    AudioSource sourceS = GameObject.Find("NoiseButton 3").GetComponent<AudioSource>();
                    Animator animS = GameObject.Find("NoiseButton 3").GetComponent<Animator>();

                    sourceS.Play();
                    animS.Play("Pressed");
                }
                if (tones[i] == "d")
                {
                    AudioSource sourceW = GameObject.Find("NoiseButton 4").GetComponent<AudioSource>();
                    Animator animW = GameObject.Find("NoiseButton 4").GetComponent<Animator>();

                    sourceW.Play();
                    animW.Play("Pressed");
                }

                if (isDebug)
                {
                    Debug.Log("End of Async For Loop");
                }

                if(tokenSource.Token.IsCancellationRequested)
                {
                    if (isDebug)
                    {
                        Debug.Log("Token was cancelled and you probably fucked up the sequence");
                        Debug.Log("Token Source Status 2: " + tokenSource.Token.IsCancellationRequested);
                    }

                    //tokenSource.Dispose();
                    await Task.Delay(1000);

                }
            }

            await Task.Delay((int)Math.Round(period * 1000));
            greenLight.SetTrigger("greenLit");
            redLight.SetTrigger("redDefault");

        }

    }

    private void SequenceAccuracyCheck()
    {
        if (userSequenceBasic.Count <= sequenceBasic.Count && !accuracyCheckComplete)
        {
            // check that the inputs match the sequence up to the current input
            for (var i = 0; i < userSequenceBasic.Count; i++)
            {
                if (sequenceBasic[i] != userSequenceBasic[i])
                {
                    // ~give information to user
                    if (isDebugMode)
                    {
                        Debug.Log("The user didn't match the sequence");
                    }
                    // error audio
                    AudioSource errorAudio = GameObject.Find("ErrorAudio").GetComponent<AudioSource>();
                    errorAudio.Play();

                    
                    
                    // ~perform operations
                    
                    // rewrite the userSequence to empty the cache
                    userSequenceBasic.Clear();

                    accurateSequenceCase = false;
                    trialCase = true;

                    playerSequencing.Value = false;
                    accuracyCheckComplete = true;

                    // stop the sequence because they don't match

                    throw new OperationCanceledException(source.Token);
                     
                    break;

                }

                else
                {
                    if (isDebugMode)
                    {
                        Debug.Log("The user matched the sequence");
                    }

                    accurateSequenceCase = true;
                
                }

            }

            accuracyCheckComplete = true;

        }
    }

    private void AccuracyCaseThread()
    {
        // if not accurate, and play view is active
        if (!accurateSequenceCase && trialCase && playView.activeInHierarchy)
        {
            // cancel the token
            source.Cancel();

            // check how many 'lives' are left


            if (lives.Value > 0)
            {
                // if there are more lives, restart the loop at the current level

                // after decrementing the lives count
                lives.Value -= 1;
  
                source = new CancellationTokenSource();
                var token = source.Token;

                RepeatSequence();
                //AsyncSequence(sequenceBasic, beatDuration, lives.Value, source);
                
            }


            else
            {
                // else if no more lives, restart the loop at the base level

                // rewrite the sequence to empty the cache
                sequenceBasic.Clear();

                // reset level count
                level.Value = 0;

                // reset lives count
                lives.Value = 2;
                
            }

        }

        trialCase = false;
        gameSequencing.Value = false;

    }

    private void SequenceLengthCheck()
    {
        if (sequenceBasic.Count == userSequenceBasic.Count && !lengthCheckComplete)
        {
            if (isDebugMode)
            {
                Debug.Log("Sequence-count is equivalent");
            }

            playerSequencing.Value = false;

            if (accurateSequenceCase)
            {
                if (isDebugMode)
                {
                    Debug.Log("You matched the sequence length, and it was accurate. Incrementing level & clearing userSequence");
                }
                
                level.Value++;

                // rewrite the userSequence to empty the cache
                userSequenceBasic.Clear();

            }

        }

        lengthCheckComplete = true;

    }

    private void LifeReplenishCheck()
    {
        if (lives.Value == 0)
        {
            lives.Value = 2;
        }
    }

    public void AddToneA()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("a");
        //Debug.Log("userSequence Count :" + userSequenceBasic.Count);

        for (var i = 0; i < userSequenceBasic.Count; i++)
        {
            //Debug.Log("userSequence :" + userSequenceBasic[i]);
        }
    }

    public void AddToneB()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("b");
        //Debug.Log("userSequence Count :" + userSequenceBasic.Count);

        for (var i = 0; i < userSequenceBasic.Count; i++)
        {
            //Debug.Log("userSequence :" + userSequenceBasic[i]);
        }
    }

    public void AddToneC()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("c");
        //Debug.Log("userSequence Count :" + userSequenceBasic.Count);

        for (var i = 0; i < userSequenceBasic.Count; i++)
        {
            //Debug.Log("userSequence :" + userSequenceBasic[i]);
        }
    }

    public void AddToneD()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("d");
        //Debug.Log("userSequence Count :" + userSequenceBasic.Count);

        for (var i = 0; i < userSequenceBasic.Count; i++)
        {
           //Debug.Log("userSequence :" + userSequenceBasic[i]);
        }
    }
}
