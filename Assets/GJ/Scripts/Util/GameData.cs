using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    [System.Serializable]           // 데이터 직렬화
    public class GameData
    {
        public string id;           // 플레이어 ID
        public float playTime;      // 플레이 시간
        public int score;           // 획득 점수

        /// <summary>
        /// 들어온 데이터를 GameData의 멤버에 추가해준다.
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_playTime"></param>
        /// <param name="_score"></param>
        public void SetData(string _id, float _playTime, int _score)
        {
            id = _id;
            playTime = _playTime;
            score = _score;
        }
    }
    [System.Serializable]
    public class GameDataGroup
    {
        public GameData[] rank;
    }
}
