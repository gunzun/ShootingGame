using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GJ
{
    public class Ranking : MonoBehaviour
    {
        public TextMeshProUGUI RankTMPro;
        public TextMeshProUGUI UserTMPro;
        public TextMeshProUGUI ScoreTMPro;
        public TextMeshProUGUI TimeTMPro;
        private string userID;
        private float time;
        private int score;
        private int maxCount = 50;              // ǥ���� ��ŷ�� ��
        private int maxDatasCount = 0;          // JSON���Ͽ��� �ε�� ������ �迭�� ����

        void Start()
        {
            // ���� �ִ� �ؽ�Ʈ���� �ߺ����� �ʵ��� �����ش�.
            RankTMPro.text = "";
            UserTMPro.text = "";
            ScoreTMPro.text = "";
            TimeTMPro.text = "";

            // ���� �����Ͱ� ����Ǿ��� ���� ���Ͽ��� �����͸� �޾ƿ´�.
            if (GameDataManager.Instance.gameDataGroup.rank.Length > maxDatasCount)
            {
                maxDatasCount = GameDataManager.Instance.gameDataGroup.rank.Length;     // ���Ϸ� ����� ����Ʈ �迭�� ����
                GameDataManager.Instance.SortListInDescendingOrderByScore();            // ����Ʈ�� ����� �迭�� ���� ���� ������������ �����Ѵ�
            }
            // JSON���Ͽ� �����Ͱ� �ִٸ� ������ ������ ȭ�鿡 ����ش�.
            // Foreach �ᵵ ������ ����.
            for (int i = 0; i < maxCount; i++)
            {
                RankTMPro.text += (i + 1).ToString() + "\n";

                if (i < maxDatasCount)
                {
                    // ��ŷ
                    //RankTMPro.text += (i + 1).ToString() + "\n";

                    // UserID
                    userID = GameDataManager.Instance.gameDatas[i].id;
                    UserTMPro.text += userID + "\n";

                    // ����
                    score = GameDataManager.Instance.gameDatas[i].score;
                    ScoreTMPro.text += score.ToString() + "\n";

                    // �÷���Ÿ��
                    time = GameDataManager.Instance.gameDatas[i].playTime;
                    TimeTMPro.text += GameManager.Instance.PlayTimeToString(time) + "\n";
                }
                else
                {
                    // Json ���Ͽ� �����Ͱ� ���ٸ� �������� �д�.
                    //RankTMPro.text += "..\n";
                    UserTMPro.text += "..\n";
                    ScoreTMPro.text += "..\n";
                    TimeTMPro.text += "..\n";
                }
            }
        }
    }
}
