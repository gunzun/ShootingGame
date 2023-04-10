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
        public GameObject HP_Prefab;                // Hp ������ ������
        public GameObject ATTACKSPEED_Prefab;       // AttackSpeed ������ ������
        public GameObject ATTACKPOWER_Prefab;       // Hp������ ������
        public GameObject BOMB_Prefab;              // Hp������ ������
        #endregion

        private int randomNum;                          // �������� �����ϰ� �����ϱ� ���� ������
        private float spawnTime;                        // �������� �����Ǵ� �ð�
        private float ItemXPos;                         // �������� ������ �������� x��
        private ITEM_TYPE m_type;                       // ������ Ÿ���� ���� ����

        private void Start()
        {
            spawnTime = 6.0f;                           // ���� �ð�
            ItemXPos = SpawnPositionRandomization();    // ������ġ ����ȭ
        }

        void Update()
        {
            count += Time.deltaTime;                                    // ī��Ʈ�� ���� ������ ����
            if (count >= spawnTime && !Player_Stat.Instance.IsDie)
            {
                m_type = ITEM_TYPE.NONE;                                // �������� NONE���� �ʱ�ȭ �� �Ŀ�
                RandomizeType();                                        // ������ ������ Ÿ������ �ٲ۴�.
                Generator();                                            // ������ ����
                ItemXPos = SpawnPositionRandomization();                // ������ġ �����ϰ� ������Ʈ
                count = 0.0f;                                           // ī��Ʈ �ʱ�ȭ
            }
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        override protected void Generator()
        {
            #region # Ÿ�Կ����� ������ ������Ʈ�� �����Ѵ�.
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
                    Debug.LogWarning("�������� ���� ������ Ÿ���Դϴ�.");
                    break;
            }
            #endregion
        }

        /// <summary>
        /// ������ Ÿ���� �����ϰ� ����
        /// </summary>
        override protected void RandomizeType()
        {
            randomNum = Random.Range(0, 100);       // �������� 0 ~ 99�� ���� �����ϰ� �ش�.
            randomNum++;                            // �������� ���� 1 ~ 100���� �ٲ۴�.

            #region #Ȯ���� ���� ������ Ÿ���� �����Ѵ�.
            if (randomNum <= 0)
            {
                Debug.LogWarning("������ �������� 0���� �۾ƿ�");
            }
            if(randomNum <= 5)
            {
                // 5%�� Ȯ���� ����
                m_type = ITEM_TYPE.BOMB;
            }
            else if (randomNum <= 20)
            {
                // 20% Ȯ���� ����
                m_type = ITEM_TYPE.ATTACKPOWER;
            }
            else if (randomNum <= 40)
            {
                // 20% Ȯ���� ����
                m_type = ITEM_TYPE.HP;
            }
            else if (randomNum <= 80)
            {
                // 40% Ȯ���� ����
                m_type = ITEM_TYPE.NONE;
            }
            else if(randomNum <= 100)
            {
                // 20�ۼ�Ʈ Ȯ���� ����
                m_type = ITEM_TYPE.ATTACKSPEED;
            }
            else
            {
                Debug.LogWarning("������ �������� �ʹ� Ŀ��");
            }
            #endregion
            #region SwitchNotWorking
            /* switch (randomNum)
            {
                case 0:
                case 5:     // 6% Ȯ���� ����                             
                    m_type = ITEM_TYPE.BOMB;
                    Debug.Log("������ ����!");
                    break;
                case 20:    // 15% Ȯ���� ����
                    m_type = ITEM_TYPE.ATTACKPOWER;
                    Debug.Log("������ ����!");
                    break;
                case 40:    // 20% Ȯ���� ����
                    m_type = ITEM_TYPE.HP;
                    Debug.Log("������ ����!");
                    break;
                case 80:    // 40% Ȯ���� ����
                    m_type = ITEM_TYPE.NONE;
                    Debug.Log("������ ����!");
                    break;
                case 99:    // 19% Ȯ���� ����
                    m_type = ITEM_TYPE.ATTACKSPEED;
                    Debug.Log("������ ����!");
                    break;
                    default:
                        Debug.LogWarning("������ �������� �̻��ؿ�!");
                        break;
            }
            */
            #endregion
        }
    }
}