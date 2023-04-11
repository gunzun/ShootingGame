using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GJ
{
    public class Gameover : MonoBehaviour
    {
        public TextMeshProUGUI userNameText;    // 유저명 텍스트 UI
        public TextMeshProUGUI timeText;        // 시간 텍스트 UI
        public TextMeshProUGUI scoreText;       // 점수 텍스트 UI

        private int score;                      // 플레이어의 스코어
        private float f_score;                  // 플레이어의 스코어까지 증가시킬 스코어
        private int i_score = 0;                // 증가시킨 스코어를 인트로 받아올 스코어
        private bool scoreAnimisEnd = false;    // 점수 올리는 애니메이션이 끝났나?

        private float playTime;                 // 플레이 타임

        void Start()
        {
            userNameText.text = "Insu";
            score = Player_Stat.Instance.CurrentScore;

            // 시간 확인
            playTime = Player_Stat.Instance.PlayTime;
            timeText.text = GameManager.Instance.PlayTimeToString(playTime);
        }
        void Update()
        {
            if (scoreAnimisEnd == false)
            {
                ScoreAnimUI();
            }
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.Restart();
            }
        }

        /// <summary>
        /// 점수 텍스트가 유저의 점수까지 올라가는 연출을 준다.
        /// </summary>
        private void ScoreAnimUI()
        {
            if(i_score >= Player_Stat.Instance.CurrentScore)
            {
                i_score = Player_Stat.Instance.CurrentScore;
                scoreText.text = i_score.ToString();
                scoreAnimisEnd = true;
            }
            else
            {
                f_score += Time.deltaTime * score / 2.0f;           // 2초만에 현재 스코어까지 증가
                i_score = (int)f_score;
                scoreText.text = i_score.ToString();
            }
        }
    }
}
