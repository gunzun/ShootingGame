using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    [System.Serializable]           // ������ ����ȭ
    public class GameData
    {
        public string id;           // �÷��̾� ID
        public float playTime;      // �÷��� �ð�
        public int score;           // ȹ�� ����

        /// <summary>
        /// ���� �����͸� GameData�� ����� �߰����ش�.
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
