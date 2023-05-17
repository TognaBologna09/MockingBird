using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scoreboard
{
    [System.Serializable]

    public class ScoreboardStruct : MonoBehaviour
    {
        public struct ScoreData
        {
            public string name;
            public int score;
        }
    }
}