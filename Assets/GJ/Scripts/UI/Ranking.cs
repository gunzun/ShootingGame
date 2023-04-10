using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GJ
{
    public class Ranking : MonoBehaviour
    {
        public TextMeshProUGUI rankingText;
        private string userID;
        private float time;
        private int score;
        
        void Start()
        {
            userID = GameDataManager.Instance.gameDataGroup.rank[0].id;
            time = GameDataManager.Instance.gameDataGroup.rank[0].playTime;
            score = GameDataManager.Instance.gameDataGroup.rank[0].score;
            for (int i = 1; i <= 100; i++)
            {
                if (userID != null)
                {
                    rankingText.text = i + ". " + userID + "    " + time + "    " + score;
                }
                else
                {
                    rankingText.text = i + ". " + "...      ...      ...";
                }
            }
        }

        void Update()
        {

        }
    }
}
