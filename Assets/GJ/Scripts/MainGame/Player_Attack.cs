using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Attack : Player
    {
        public GameObject Bullet;               // 총알 프리팹
        public GameObject Snowball;             // 눈덩이 프리팹

        private float count;                    // 공격 사이 카운트
        private bool isSkill = false;           // 스킬을 사용했나?
        public bool PlayerSkilled { get => isSkill; }

        void Update()
        {
            count += Time.deltaTime;                                                    // 카운트를 증가 시키고 공격 속도값에 도달하면 공격 후 0으로 리셋한다.
            if (playerInput.fire == true && count >= Player_Stat.Instance.AttackSpeed)  // "Fire" 키를 눌렀을 때, 카운트가 차면 공격한다.
            {
                Fire();                                                                 // 함수 실행
                count = 0.0f;                                                           // 총알 발사 후 카운트를 0으로 리셋한다.
            }
            if (playerInput.skill == true)
            {
                Debug.Log("스킬 발동!");
            }
        }
        private void Fire()
        {
            // 플레이어 위에 총알 프리팹 생성
            Instantiate(Bullet.transform , this.transform.position + new Vector3(0f, 0.5f, 0f), this.transform.rotation);
        }               // 플레이어가 총알을 발사한다.

        private void OnTriggerEnter(Collider other)
        {
            if (playerInput.skill == true && other.CompareTag("Enemy"))
            {
                isSkill = true;
            }
        }
    }
}
