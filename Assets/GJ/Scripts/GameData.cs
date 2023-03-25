using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    [Serializable]      // ����Ƽ���� ������ ������ ����ȭ �ʿ�, �ƴϸ� �����͸� ���� �� ����.
    public class GameData
    {
        // ID
        public string id;
        // �������� �÷��� �ð�
        public float stageTime;
        // ���������� ȹ�� ����
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
