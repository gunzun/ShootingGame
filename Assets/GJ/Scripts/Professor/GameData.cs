/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    [Serializable]      // 유니티에선 무조건 데이터 직렬화 필요, 아니면 데이터를 읽을 수 없다.
    public class GameData
    {
        // ID
        public string id;
        // 스테이지 플레이 시간
        public float stageTime;
        // 스테이지별 획득 점수
        public int stageScore;

        public void SetData(string _id, float _stageTime, int _stageScore)
        {
            id = _id;
            stageTime = _stageTime;
            stageScore = _stageScore;
        }
    }
    [Serializable]  
    public class GameDataGroup
    {
        public GameData[] rank;
    }
}
*/