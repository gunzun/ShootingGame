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
        public TextMeshProUGUI BestRankText;    // 최고 점수 텍스트
        public TextMeshProUGUI CongratulationText;  // 축하 텍스트

        private string userName;                // 유저의 이름
        private int score;                      // 플레이어의 스코어
        private float f_score;                  // 플레이어의 스코어까지 증가시킬 스코어
        private int i_score = 0;                // 증가시킨 스코어를 인트로 받아올 스코어
        private bool scoreAnimisEnd = false;    // 점수 올리는 애니메이션이 끝났나?

        private float playTime;                 // 플레이 타임

        private GameData prefBestScore;         // 플레이 이전의 최고기록 데이터

        void Start()
        {
            // 현재 유저 이름
            userName = Player_Stat.Instance.userName;
            userNameText.text = userName;
            // 현재 점수
            score = Player_Stat.Instance.CurrentScore;
            // 시간 확인
            playTime = Player_Stat.Instance.PlayTime;
            timeText.text = GameManager.Instance.PlayTimeToString(playTime);

            // path에 게임 데이터 파일이 존재한다면
            if (GameDataManager.Instance.isFileExist())
            {
                // 플레이 이전 저장된 최고 기록 데이터를 가져온다.
                prefBestScore = GameDataManager.Instance.gameDatas[0];

                // 최고 점수를 갱신하지 못했다면
                if (score <= prefBestScore.score)
                {
                    // 이전 최고 기록의 유저 데이터를 가져온다.
                    BestRankText.text = prefBestScore.id + " : " + prefBestScore.score;
                }
                // 베스트 레코드 경신
                else
                {
                    BestRankText.text = userName + " : " + score;
                    CongratulationText.gameObject.SetActive(true);
                }
            }
            // path에 게임 데이터 파일이 없다면
            else
            {
                // 베스트 레코드 경신
                BestRankText.text = userName + " : " + score;
                CongratulationText.gameObject.SetActive(true);
            }
            // 게임 매니저에 현재 유저 데이터 추가
            // GameDataManager.Instance.LoadData();
        }
        void Update()
        {
            if (scoreAnimisEnd == false)
            {
                ScoreAnimUI();
            }
            else
            {

            }
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.EnterStartScene();
            }
        }

        /// <summary>
        /// 점수 텍스트가 유저의 점수까지 올라가는 연출을 준다.
        /// </summary>
        private void ScoreAnimUI()
        {
            if (i_score >= Player_Stat.Instance.CurrentScore)
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
