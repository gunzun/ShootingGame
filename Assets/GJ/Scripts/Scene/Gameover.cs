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

        private int score;                      // �÷��̾��� ���ھ�
        private float f_score;                  // �÷��̾��� ���ھ���� ������ų ���ھ�
        private int i_score = 0;                // ������Ų ���ھ ��Ʈ�� �޾ƿ� ���ھ�
        private bool scoreAnimisEnd = false;    // ���� �ø��� �ִϸ��̼��� ������?

        private float playTime;                 // �÷��� Ÿ��

        void Start()
        {
            userNameText.text = "Insu";
            score = Player_Stat.Instance.CurrentScore;

            // �ð� Ȯ��
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
        /// ���� �ؽ�Ʈ�� ������ �������� �ö󰡴� ������ �ش�.
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
                f_score += Time.deltaTime * score / 2.0f;           // 2�ʸ��� ���� ���ھ���� ����
                i_score = (int)f_score;
                scoreText.text = i_score.ToString();
            }
        }
    }
}
