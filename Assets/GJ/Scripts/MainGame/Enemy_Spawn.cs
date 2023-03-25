using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Enemy_Spawn : MonoBehaviour
    {
        public GameObject enemyPrefab;      // ������ ���ʹ� ������

        private float spawnTime;
        public GameObject RandomSpanwPosMin;        // ���� ���� ��ġ �ּڰ�
        public GameObject RandomSpanwPosMax;        // ���� ���� ��ġ �ִ�
        private float EnemyPos_X;                   // ������ �� ��ġ��

        private float count;                

        void Start()
        {
            // ������ġ ����
            EnemyPos_X = Random.Range(RandomSpanwPosMin.transform.position.x, RandomSpanwPosMax.transform.position.x);

            // ����Ÿ��
            spawnTime = 1.5f;
        }

        void Update()
        {
            count += Time.deltaTime;
            if (count >= spawnTime)
            {
                Instantiate(enemyPrefab, this.transform.position + new Vector3(EnemyPos_X, 0, 0), this.transform.rotation);
                count = 0f;
            }

        }
    }
}
