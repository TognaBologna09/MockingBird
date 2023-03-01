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
    [SerializeField]
    private GameObject playSixView;

    [Space(8)]
    [Header("Default Game variables")]
    public IntVariable level;
    public IntVariable lives;
    public IntVariable score;

    List<string> optionsFour = new List<string>() { "a", "b", "c", "d" };
    List<string> optionsSix = new List<string>() { "z", "a", "b", "c", "d", "e" };
    List<string> rhtyhmOptions = new List<string>() { "qn", "en", "sn" };
    List<(string, string)> rhythmSequence = new List<(string, string)>();

    public List<string> sequenceBasic = new List<string>(); 
    public List<string> userSequenceBasic = new List<string>();

    static System.Random random = new System.Random();


    [Space(8)]
    [Header(" Default Booleans to sequence Update Loop")]
    public BoolVariable volumeMute;
    public BoolVariable gameSequencing;
    public BoolVariable playerSequencing;
 
    public bool accuracyCheckComplete = false;
    public bool accurateSequenceCase = false;
    public bool trialCase = false;
    public bool lengthCheckComplete = false;
    public bool asyncTaskComplete = false;

    [Space(8)]
    [Header("Default Sequence Timer variables")]
    public IntVariable periodMs;
    public FloatVariable tempo;

    private float beatDuration;

    [Space(8)]
    [Header("Six Mode Game Variables")]
    public BoolVariable sixMode;

    //[Space(8)]
    //[Header("Chaos Mode Game Variables")]
    //public BoolVariable chaosMode;
    //public int chaoticOptionCount = 0;
    //private bool chaoticOptionComplete = false;
    //public bool chaosRoundOne = true;
    //public int summedRNG = 0;

    //[Space(8)]
    //[Header("Audio, Animators")]

    //[Space(4)]
    //// Animators
    //public Animator redLight;
    //public Animator greenLight;

    // Cancellation Tokens and Sources
    CancellationTokenSource source = null;
    //CancellationToken token = source.Token;

    [Space(8)]
    [Header("Debug")]
    public bool isDebugMode;

    private void Start()
    {
        level.Value = 0;
        lives.Value = 2;
        tempo.Value = 60;

        volumeMute.Value = false;
        sixMode.Value = false;
       
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

    public async Task MainSixBasic()
    {

        int randomOption = random.Next(optionsSix.Count);

        source = new CancellationTokenSource();

        if (gameSequencing)
        {

            if (sequenceBasic.Count < level.Value + 1 && lives.Value > 0)
            {
                if (isDebugMode)
                {
                    Debug.Log("MainSixBasic Adding to Sequence Statement");
                }

                // the sequence length is not sufficient for the level; add a random tone to sequence
                sequenceBasic.Add(optionsSix[randomOption]);

                playerSequencing.Value = false;
                gameSequencing.Value = true;

                await AsyncSequence(sequenceBasic, beatDuration, lives.Value, source, isDebugMode);

                gameSequencing.Value = false;
                playerSequencing.Value = true;

            }


        }
    }

    //public async Task MainChaos()
    //{

    //    source = new CancellationTokenSource();

    //    if (gameSequencing)
    //    {

    //        if (sequenceBasic.Count < (summedRNG ) && playView.activeInHierarchy && lives.Value > 0)
    //        {
    //            if (isDebugMode)
    //            {
    //                Debug.Log("MainChaos begin Task");
    //                Debug.Log("MainChaos sequence count, chaotic option count: " + sequenceBasic.Count + " & " + (chaoticOptionCount));
    //                Debug.Log("The summed RNG is: " + summedRNG);
    //            }

                
    //            // the sequence length is not sufficient for the level; add a random tone to sequence
    //            for (int i = 0; i < chaoticOptionCount; i++)
    //            {
    //                int randomOption = random.Next(optionsFour.Count);
    //                sequenceBasic.Add(optionsFour[randomOption]);
    //            }

    //            playerSequencing.Value = false;
    //            gameSequencing.Value = true;

    //            await AsyncSequence(sequenceBasic, beatDuration, lives.Value, source, isDebugMode);
                
    //            gameSequencing.Value = false;
    //            playerSequencing.Value = true;

    //            chaoticOptionComplete = false;

    //            if (isDebugMode)
    //            {
    //                Debug.Log("MainChaos end of Task");
    //                Debug.Log("MainChaos truth: " + sequenceBasic.Count + " < " + (summedRNG + 1));
    //            }

    //        }

    //    }

    //}

    //private void MainChaosThread()
    //{
    //    //if (!chaoticOptionComplete)
    //    //{
    //    //    chaoticOptionCount = UnityEngine.Random.Range(1, 3);
    //    //    summedRNG += chaoticOptionCount;
    //    //    chaoticOptionComplete = true;
    //    //}

    //    //if (isDebugMode)
    //    //{
    //    //    Debug.Log("The current RNG is: " + chaoticOptionCount);
    //    //    Debug.Log("The summed RNG is: " + summedRNG);
    //    //}
   
    //}

    public void Reset()
    {
        if (playView.activeInHierarchy)
        {
            Animator animN = GameObject.Find("NoiseButton 1").GetComponent<Animator>();
            animN.Play("Normal");
            Animator animE = GameObject.Find("NoiseButton 2").GetComponent<Animator>();
            animE.Play("Normal");
            Animator animS = GameObject.Find("NoiseButton 3").GetComponent<Animator>();
            animS.Play("Normal");
            Animator animW = GameObject.Find("NoiseButton 4").GetComponent<Animator>();
            animW.Play("Normal");
        }

        userSequenceBasic.Clear();
        sequenceBasic.Clear();

        lives.Value = 2;
        level.Value = 0;

        //summedRNG = 0;
        //chaosRoundOne = true;

        source = new CancellationTokenSource();

    }

    private void Update()
    {
        
        score.Value = sequenceBasic.Count;

        beatDuration = 60.0f / tempo.Value;
        
        if(!sixMode.Value)
        {

            if (!gameSequencing.Value && sequenceBasic.Count < level.Value + 1 && playView.activeInHierarchy && lives.Value >= 1)
            {

                if (isDebugMode)
                {
                    Debug.Log("UpdateBasic truth: " + !gameSequencing.Value + " --> " + sequenceBasic.Count + " < " + (level.Value + 1));
                }

                gameSequencing.Value = true;

                // generate new tones
                MainBasic();

            }

        }

        if(sixMode.Value)
        {

            if (!gameSequencing.Value && sequenceBasic.Count < level.Value + 1 && playSixView.activeInHierarchy && lives.Value >= 1)
            {

                if (isDebugMode)
                {
                    Debug.Log("UpdateBasic truth: " + !gameSequencing.Value + " --> " + sequenceBasic.Count + " < " + (level.Value + 1));
                }

                gameSequencing.Value = true;

                // generate new tones
                MainSixBasic();

            }
        }

        //else
        //{
        //    if (!gameSequencing.Value && level.Value == 0 && playView.activeInHierarchy && lives.Value >= 1 && chaosRoundOne)
        //    {
        //        GenerateRNG();

        //        gameSequencing.Value = true;

        //        // generate new tones
        //        MainChaos();

        //        chaosRoundOne = false;
        //    }

        //    if (summedRNG == 0)
        //    {
        //        GenerateRNG();
        //    }

        //    if (!gameSequencing.Value && sequenceBasic.Count < (summedRNG + 1) && chaosUserSuccess && playView.activeInHierarchy && lives.Value >= 1)
        //    {
        //        if (isDebugMode)
        //        {
        //            Debug.Log("UpdateMainChaos truth: " + !gameSequencing.Value + "," + chaosUserSuccess + " --> " + sequenceBasic.Count + " < " + (summedRNG + 1));
                   
        //        }

        //        gameSequencing.Value = true;
                
                

        //        // generate new tones
        //        MainChaos();
               

        //    }
        //}
         
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

                
                // Read the randomly generated list of tones, play them
                if (tones[i] == "z")
                {
                    AudioSource source0 = GameObject.Find("NoiseButton 0").GetComponent<AudioSource>();
                    Animator anim0 = GameObject.Find("NoiseButton 0").GetComponent<Animator>();

                    source0.Play();
                    anim0.Play("Pressed");
                }

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
                if (tones[i] == "e")
                {
                    AudioSource source5 = GameObject.Find("NoiseButton 5").GetComponent<AudioSource>();
                    Animator anim5 = GameObject.Find("NoiseButton 5").GetComponent<Animator>();

                    source5.Play();
                    anim5.Play("Pressed");
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

            
            await Task.Delay((int)Math.Round(period * 250));
            greenLight.SetTrigger("greenLit");
            redLight.SetTrigger("redDefault");

           
        }

    }

    private void SequenceAccuracyCheck()
    {
        if (userSequenceBasic.Count <= sequenceBasic.Count && !accuracyCheckComplete)
        {
            if(userSequenceBasic.Count > 0)
            {
                source.Cancel();
            }
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

                    //if (chaosMode.Value)
                    //{
                    //    chaosUserSuccess = false;
                    //}

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

                    //if (chaosMode.Value)
                    //{
                    //    chaosUserSuccess = true;
                    //}
                   
                }

            }

            accuracyCheckComplete = true;

        }
    }

    private void AccuracyCaseThread()
    {
        // if not accurate, and play view is active
        if (!accurateSequenceCase && trialCase)
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

                //if (chaosMode.Value)
                //{
                //    chaosUserSuccess = false;
                //}
            }

        }

        trialCase = false;
        gameSequencing.Value = false;

    }

    //private void GenerateRNG()
    //{
    //    if (!chaoticOptionComplete)
    //    {
    //        if (isDebugMode)
    //        {
    //            Debug.Log("Generating RNG #s");
    //        }

    //        chaoticOptionCount = UnityEngine.Random.Range(1, 3);
    //        summedRNG += chaoticOptionCount;
    //        chaoticOptionComplete = true;
    //    }
    //}

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

                //if(chaosMode.Value)
                //{
                //    chaosUserSuccess = true;
                //    GenerateRNG();
                    
                //}
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

    public void AddTone0()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("z");

    }

    public void AddTone1()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("a");
       
    }

    public void AddTone2()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("b");
        
    }

    public void AddTone3()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("c");
        
    }

    public void AddTone4()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("d");
        
    }

    public void AddTone5()
    {
        accuracyCheckComplete = false;
        lengthCheckComplete = false;

        userSequenceBasic.Add("e");

    }
}
