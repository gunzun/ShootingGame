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
        private int maxCount = 50;              // 표시할 랭킹의 수
        private int maxDatasCount = 0;          // JSON파일에서 로드된 데이터 배열의 개수

        void Start()
        {
            // 전에 있던 텍스트들이 중복되지 않도록 지워준다.
            RankTMPro.text = "";
            UserTMPro.text = "";
            ScoreTMPro.text = "";
            TimeTMPro.text = "";

            // 게임 데이터가 변경되었을 때만 파일에서 데이터를 받아온다.
            if (GameDataManager.Instance.gameDataGroup.rank.Length > maxDatasCount)
            {
                maxDatasCount = GameDataManager.Instance.gameDataGroup.rank.Length;     // 파일로 저장된 리스트 배열의 개수
                GameDataManager.Instance.SortListInDescendingOrderByScore();            // 리스트로 저장된 배열을 점수 기준 내림차순으로 정리한다
            }
            // JSON파일에 데이터가 있다면 정보를 가져와 화면에 띄워준다.
            // Foreach 써도 됐을거 같다.
            for (int i = 0; i < maxCount; i++)
            {
                RankTMPro.text += (i + 1).ToString() + "\n";

                if (i < maxDatasCount)
                {
                    // 랭킹
                    //RankTMPro.text += (i + 1).ToString() + "\n";

                    // UserID
                    userID = GameDataManager.Instance.gameDatas[i].id;
                    UserTMPro.text += userID + "\n";

                    // 점수
                    score = GameDataManager.Instance.gameDatas[i].score;
                    ScoreTMPro.text += score.ToString() + "\n";

                    // 플레이타임
                    time = GameDataManager.Instance.gameDatas[i].playTime;
                    TimeTMPro.text += GameManager.Instance.PlayTimeToString(time) + "\n";
                }
                else
                {
                    // Json 파일에 데이터가 없다면 공백으로 둔다.
                    //RankTMPro.text += "..\n";
                    UserTMPro.text += "..\n";
                    ScoreTMPro.text += "..\n";
                    TimeTMPro.text += "..\n";
                }
            }
        }
    }
}
