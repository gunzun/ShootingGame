using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public enum ITEM_TYPE
    {
        HP,
        ATTACKSPEED,
        ATTACKPOWER,
        BOMB,
        NONE
    }

    public class Item_Spawn : Spawner
    {
        #region #Public ItemPrefabs
        public GameObject HP_Prefab;                // Hp 아이템 프리팹
        public GameObject ATTACKSPEED_Prefab;       // AttackSpeed 아이템 프리팹
        public GameObject ATTACKPOWER_Prefab;       // Hp아이템 프리팹
        public GameObject BOMB_Prefab;              // Hp아이템 프리팹
        #endregion

        private int randomNum;                          // 아이템을 랜덤하게 생성하기 위한 랜덤값
        private float spawnTime;                        // 아이템이 생성되는 시간
        private float ItemXPos;                         // 아이템이 생성될 포지션의 x값
        private ITEM_TYPE m_type;                       // 아이템 타입을 받을 변수

        private void Start()
        {
            spawnTime = 6.0f;                           // 스폰 시간
            ItemXPos = SpawnPositionRandomization();    // 스폰위치 랜덤화
        }

        void Update()
        {
            count += Time.deltaTime;                                    // 카운트가 차면 아이템 생성
            if (count >= spawnTime)
            {
                m_type = ITEM_TYPE.NONE;                                // 아이템을 NONE으로 초기화 한 후에
                RandomizeType();                                        // 랜덤한 아이템 타입으로 바꾼다.
                Generator();                                            // 아이템 생성
                ItemXPos = SpawnPositionRandomization();                // 스폰위치 랜덤하게 업데이트
                count = 0.0f;                                           // 카운트 초기화
            }
        }

        /// <summary>
        /// 아이템 생성
        /// </summary>
        override protected void Generator()
        {
            #region # 타입에따라 아이템 오브젝트를 생성한다.
            switch (m_type)
            {
                case ITEM_TYPE.HP:
                    Instantiate(HP_Prefab, transform.position + new Vector3(ItemXPos, 0.0f, -0.3f), 
                        transform.rotation);
                    break;
                case ITEM_TYPE.ATTACKSPEED:
                    Instantiate(ATTACKSPEED_Prefab, transform.position + new Vector3(ItemXPos, 0.0f, -0.3f),
                        transform.rotation);
                    break;
                case ITEM_TYPE.ATTACKPOWER:
                    Instantiate(ATTACKPOWER_Prefab, transform.position + new Vector3(ItemXPos, 0.0f, -0.3f),
                        transform.rotation);
                    break;
                case ITEM_TYPE.BOMB:
                    Instantiate(BOMB_Prefab, transform.position + new Vector3(ItemXPos, 0.0f, -0.3f),
                        transform.rotation);
                    break;
                case ITEM_TYPE.NONE:
                    break;
                default:
                    Debug.LogWarning("지정하지 않은 아이템 타입입니다.");
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 아이템 타입을 랜덤하게 선택
        /// </summary>
        override protected void RandomizeType()
        {
            randomNum = Random.Range(0, 100);       // 랜덤값에 0 ~ 99의 값을 랜덤하게 준다.
            randomNum++;                            // 가독성을 위해 1 ~ 100으로 바꾼다.

            #region #확률에 따라 아이템 타입을 설정한다.
            if (randomNum <= 0)
            {
                Debug.LogWarning("아이템 랜덤값이 0보다 작아요");
            }
            if(randomNum <= 5)
            {
                // 5%의 확률로 생성
                m_type = ITEM_TYPE.BOMB;
            }
            else if (randomNum <= 20)
            {
                // 20% 확률로 생성
                m_type = ITEM_TYPE.ATTACKPOWER;
            }
            else if (randomNum <= 40)
            {
                // 20% 확률로 생성
                m_type = ITEM_TYPE.HP;
            }
            else if (randomNum <= 80)
            {
                // 40% 확률로 생성
                m_type = ITEM_TYPE.NONE;
            }
            else if(randomNum <= 100)
            {
                // 20퍼센트 확률로 생성
                m_type = ITEM_TYPE.ATTACKSPEED;
            }
            else
            {
                Debug.LogWarning("아이템 랜덤값이 너무 커요");
            }
            #endregion
            #region SwitchNotWorking
            /* switch (randomNum)
            {
                case 0:
                case 5:     // 6% 확률로 생성                             
                    m_type = ITEM_TYPE.BOMB;
                    Debug.Log("아이템 생성!");
                    break;
                case 20:    // 15% 확률로 생성
                    m_type = ITEM_TYPE.ATTACKPOWER;
                    Debug.Log("아이템 생성!");
                    break;
                case 40:    // 20% 확률로 생성
                    m_type = ITEM_TYPE.HP;
                    Debug.Log("아이템 생성!");
                    break;
                case 80:    // 40% 확률로 생성
                    m_type = ITEM_TYPE.NONE;
                    Debug.Log("아이템 생성!");
                    break;
                case 99:    // 19% 확률로 생성
                    m_type = ITEM_TYPE.ATTACKSPEED;
                    Debug.Log("아이템 생성!");
                    break;
                    default:
                        Debug.LogWarning("아이템 랜덤값이 이상해요!");
                        break;
            }
            */
            #endregion
        }
    }
}