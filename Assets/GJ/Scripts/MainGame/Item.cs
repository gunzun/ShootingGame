using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Item : MonoBehaviour
    {
        protected float speed = 2.0f;           // �������� �������� �ӵ�
        protected ITEM_TYPE m_Type;             // �ڽ��� Ÿ���� ������.

        private void Update()
        {
            ItemFall();                         // �������� ����߸���.
        }
        // �������� ��������.
        private void ItemFall()
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        protected void OnTriggerEnter(Collider other)
        {
            // �÷��̾�� �浹���� ��
            if (other.CompareTag("Player"))
            {
                switch (m_Type)
                {
                    case ITEM_TYPE.HP:
                        Player_Stat.Instance.Hp += 1;
                        break;
                    case ITEM_TYPE.ATTACKSPEED:
                        Player_Stat.Instance.AttackSpeed = 1.0f;
                        break;
                    case ITEM_TYPE.ATTACKPOWER:
                        Player_Stat.Instance.AttackPower += 1;
                        break;
                    case ITEM_TYPE.BOMB:
                        Player_Stat.Instance.NumberOfBombs += 1;
                        break;
                    default:
                        break;
                }
               Destroy(gameObject);
            }
            // ����� �������� DeadZone�� �浹���� ��
            else if (other.CompareTag("Deadzone"))
            {
                Destroy(gameObject);
            }
        }
    }
}
