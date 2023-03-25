using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Enemy_Spawn : MonoBehaviour
    {
        public GameObject enemyPrefab;      // 생성할 에너미 프리팹

        private float spawnTime;
        public GameObject RandomSpanwPosMin;        // 랜덤 스폰 위치 최솟값
        public GameObject RandomSpanwPosMax;        // 랜덤 스폰 위치 최댓값
        private float EnemyPos_X;                   // 생성된 적 위치값

        private float count;                

        void Start()
        {
            // 스폰위치 랜덤
            EnemyPos_X = Random.Range(RandomSpanwPosMin.transform.position.x, RandomSpanwPosMax.transform.position.x);

            // 스폰타임
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
