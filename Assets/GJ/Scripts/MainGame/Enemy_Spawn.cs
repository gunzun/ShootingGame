using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    // 적의 종류 
    public enum ENEMY_TYPE
    {
        ENEMY1,
        ENEMY2,
        ENEMY3
    }
    public class Enemy_Spawn : Spawner
    {
        private ENEMY_TYPE m_type;                  // 적의 타입
        public GameObject enemy1Prefab;             // 생성할 에너미1 프리팹
        public GameObject enemy2Prefab;             // 생성할 에너미2 프리팹
        public GameObject enemy3Prefab;             // 생성할 에너미3 프리팹
        private float spawnTime;                    // 에너미 스폰 시간
        private float EnemyPos_x;                   // 생성된 적 위치값
        public float SpawnTime { get; set; }        // 스폰 시간 프로퍼티
        private void Start()
        {
            spawnTime = 2.0f;                                   // 스폰타임
        }
        void Update()
        {
            count += Time.deltaTime;                            // 카운트가 스폰타임보다 커지면
            if (count >= spawnTime && !Player_Stat.Instance.IsDie)
            {
                RandomizeType();                                // 생성될 에너미 타입을 랜덤하게 업데이트
                EnemyPos_x = SpawnPositionRandomization();      // 스폰위치를 랜덤하게 업데이트
                Generator();                                    // 에너미 생성 하고
                count = 0f;                                     // 카운트를 리셋한다.
            }
        }

        override protected void Generator()
        {
            #region # 정해진 적 생성
            switch (m_type)
            {
                case ENEMY_TYPE.ENEMY1:
                    Instantiate(enemy1Prefab, this.transform.position + new Vector3(EnemyPos_x, 0.0f, -0.1f), 
                        transform.rotation);
                    break;
                case ENEMY_TYPE.ENEMY2:
                    Instantiate(enemy2Prefab, this.transform.position + new Vector3(EnemyPos_x, 0.0f, -0.1f), 
                        transform.rotation);
                    break;
                case ENEMY_TYPE.ENEMY3:
                    Instantiate(enemy3Prefab, this.transform.position + new Vector3(EnemyPos_x, 0.0f, -0.1f), 
                        transform.rotation);
                    break;
                default:
                    Debug.LogWarning("에너미 타입값이 이상해요!");
                    break;
            }
            #endregion
        }

        override protected void RandomizeType()
        {
            // 타입 랜덤화
            m_type = (ENEMY_TYPE)Random.Range(0, System.Enum.GetValues(typeof(ENEMY_TYPE)).Length);
        }
    }

}