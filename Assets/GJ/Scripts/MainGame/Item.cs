using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Item : MonoBehaviour
    {
        protected float speed = 2.0f;           // 아이템이 떨어지는 속도
        protected ITEM_TYPE m_Type;             // 자신의 타입을 가진다.

        private void Update()
        {
            ItemFall();                         // 아이템을 떨어뜨린다.
        }
        // 아이템이 떨어진다.
        private void ItemFall()
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        protected void OnTriggerEnter(Collider other)
        {
            // 플레이어와 충돌했을 때
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
            // 충분히 떨어져서 DeadZone과 충돌했을 때
            else if (other.CompareTag("Deadzone"))
            {
                Destroy(gameObject);
            }
        }
    }
}
