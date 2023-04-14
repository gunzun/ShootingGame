using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GJ
{
    public class Gameover : MonoBehaviour
    {
        public TextMeshProUGUI userNameText;    // ������ �ؽ�Ʈ UI
        public TextMeshProUGUI timeText;        // �ð� �ؽ�Ʈ UI
        public TextMeshProUGUI scoreText;       // ���� �ؽ�Ʈ UI
        public TextMeshProUGUI BestRankText;    // �ְ� ���� �ؽ�Ʈ
        public TextMeshProUGUI CongratulationText;  // ���� �ؽ�Ʈ

        private string userName;                // ������ �̸�
        private int score;                      // �÷��̾��� ���ھ�
        private float f_score;                  // �÷��̾��� ���ھ���� ������ų ���ھ�
        private int i_score = 0;                // ������Ų ���ھ ��Ʈ�� �޾ƿ� ���ھ�
        private bool scoreAnimisEnd = false;    // ���� �ø��� �ִϸ��̼��� ������?

        private float playTime;                 // �÷��� Ÿ��

        private GameData prefBestScore;         // �÷��� ������ �ְ��� ������

        void Start()
        {
            // ���� ���� �̸�
            userName = Player_Stat.Instance.userName;
            userNameText.text = userName;
            // ���� ����
            score = Player_Stat.Instance.CurrentScore;
            // �ð� Ȯ��
            playTime = Player_Stat.Instance.PlayTime;
            timeText.text = GameManager.Instance.PlayTimeToString(playTime);

            // path�� ���� ������ ������ �����Ѵٸ�
            if (GameDataManager.Instance.isFileExist())
            {
                // �÷��� ���� ����� �ְ� ��� �����͸� �����´�.
                prefBestScore = GameDataManager.Instance.gameDatas[0];

                // �ְ� ������ �������� ���ߴٸ�
                if (score <= prefBestScore.score)
                {
                    // ���� �ְ� ����� ���� �����͸� �����´�.
                    BestRankText.text = prefBestScore.id + " : " + prefBestScore.score;
                }
                // ����Ʈ ���ڵ� ���
                else
                {
                    BestRankText.text = userName + " : " + score;
                    CongratulationText.gameObject.SetActive(true);
                }
            }
            // path�� ���� ������ ������ ���ٸ�
            else
            {
                // ����Ʈ ���ڵ� ���
                BestRankText.text = userName + " : " + score;
                CongratulationText.gameObject.SetActive(true);
            }
            // ���� �Ŵ����� ���� ���� ������ �߰�
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
        /// ���� �ؽ�Ʈ�� ������ �������� �ö󰡴� ������ �ش�.
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
                f_score += Time.deltaTime * score / 2.0f;           // 2�ʸ��� ���� ���ھ���� ����
                i_score = (int)f_score;
                scoreText.text = i_score.ToString();
            }
        }
    }
}
