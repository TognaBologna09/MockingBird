using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Threading;
using System.Threading.Tasks;

public class PlayView : View
{
    // Play UI Navigation
    [SerializeField]
    private Button homeButton;

    // Gamer Buttons
    [SerializeField]
    private Button musicButtonA;
    [SerializeField]
    private Button musicButtonB;
    [SerializeField]
    private Button musicButtonC;
    [SerializeField]
    private Button musicButtonD;

    // Data
    [SerializeField]
    private IntVariable lives;

    [SerializeField]
    private BoolVariable isRhythmMode;

    [SerializeField]
    private BoolVariable isWithMetronome;

    // Scorecard UI
    [SerializeField]
    private GameObject objectScorecardPanel;

    // Scorecard UI Navigation
    [SerializeField]
    private Button scorecardReplayButton;

    [SerializeField]
    private Button scorecardExitToMainButton;

    [SerializeField]
    private Button scorecardShareButton;

    // UI Methods
    public override void Initialize()
    {
        // play view button listeners
        homeButton.onClick.AddListener(() => ViewManager.Instance.Show<MainView>());
        
        // scorecard panel button listeners

        scorecardReplayButton.onClick.AddListener(() => objectScorecardPanel.SetActive(false));
        scorecardReplayButton.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<GameManager>().Reset());
        scorecardReplayButton.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<GameManager>().MainBasic());

        scorecardExitToMainButton.onClick.AddListener(() => ViewManager.Instance.Show<MainView>());

        scorecardShareButton.onClick.AddListener(() => ViewManager.Instance.Show<MainView>());

        if (!isRhythmMode)
        {
            musicButtonA.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<GameManager>().AddToneA());
            musicButtonB.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<GameManager>().AddToneB());
            musicButtonC.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<GameManager>().AddToneC());
            musicButtonD.onClick.AddListener(() => GameObject.Find("GameManager").GetComponent<GameManager>().AddToneD());
        }
        else
        {

        }

        base.Initialize();

    }

    void Update()
    {
        if (lives.Value < 1)
        {
            objectScorecardPanel.SetActive(true);
        }
    }

    public override void Show(object args = null)
    {
        base.Show(args);
    }

    

}
