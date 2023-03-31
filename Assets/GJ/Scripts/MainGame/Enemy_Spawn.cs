using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    // ���� ���� 
    public enum ENEMY_TYPE
    {
        ENEMY1,
        ENEMY2,
        ENEMY3
    }
    public class Enemy_Spawn : Spawner
    {
        private ENEMY_TYPE m_type;                  // ���� Ÿ��
        public GameObject enemy1Prefab;             // ������ ���ʹ�1 ������
        public GameObject enemy2Prefab;             // ������ ���ʹ�2 ������
        public GameObject enemy3Prefab;             // ������ ���ʹ�3 ������
        private float spawnTime;                    // ���ʹ� ���� �ð�
        private float EnemyPos_x;                   // ������ �� ��ġ��
        public float SpawnTime { get; set; }        // ���� �ð� ������Ƽ
        private void Start()
        {
            spawnTime = 2.0f;                                   // ����Ÿ��
        }
        void Update()
        {
            count += Time.deltaTime;                            // ī��Ʈ�� ����Ÿ�Ӻ��� Ŀ����
            if (count >= spawnTime)
            {
                RandomizeType();                                // ������ ���ʹ� Ÿ���� �����ϰ� ������Ʈ
                EnemyPos_x = SpawnPositionRandomization();      // ������ġ�� �����ϰ� ������Ʈ
                Generator();                                    // ���ʹ� ���� �ϰ�
                count = 0f;                                     // ī��Ʈ�� �����Ѵ�.
            }
        }

        override protected void Generator()
        {
            #region # ������ �� ����
            switch (m_type)
            {
                case ENEMY_TYPE.ENEMY1:
                    Instantiate(enemy1Prefab, this.transform.position + new Vector3(EnemyPos_x, 0.0f, 0.0f), 
                        transform.rotation);
                    break;
                case ENEMY_TYPE.ENEMY2:
                    Instantiate(enemy2Prefab, this.transform.position + new Vector3(EnemyPos_x, 0.0f, 0.0f), 
                        transform.rotation);
                    break;
                case ENEMY_TYPE.ENEMY3:
                    Instantiate(enemy3Prefab, this.transform.position + new Vector3(EnemyPos_x, 0.0f, 0.0f), 
                        transform.rotation);
                    break;
                default:
                    Debug.LogWarning("���ʹ� Ÿ�԰��� �̻��ؿ�!");
                    break;
            }
            #endregion
        }

        override protected void RandomizeType()
        {
            // Ÿ�� ����ȭ
            m_type = (ENEMY_TYPE)Random.Range(0, System.Enum.GetValues(typeof(ENEMY_TYPE)).Length);
        }
    }

}