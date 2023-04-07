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
        [SerializeField]
        private bool isSkill = false;           // 스킬을 사용했나?
        public bool PlayerSkilled { get => isSkill; set => isSkill = value; }

        void Update()
        {
            count += Time.deltaTime;                                                    // 카운트를 증가 시키고 공격 속도값에 도달하면 공격 후 0으로 리셋한다.
            if (playerInput.fire == true && count >= Player_Stat.Instance.AttackSpeed)  // "Fire" 키를 눌렀을 때, 카운트가 차면 공격한다.
            {
                Fire();                                                                 // 함수 실행
                count = 0.0f;                                                           // 총알 발사 후 카운트를 0으로 리셋한다.
            }
            isSkill = playerInput.skill;                                                // 스페이스 바를 누를 때 isSkill를 true로 반환한다.
            
        }
        private void Fire()
        {
            // 플레이어 위에 총알 프리팹 생성
            Instantiate(Bullet.transform, this.transform.position + new Vector3(0f, 0.5f, 0f), this.transform.rotation);
        }               // 플레이어가 총알을 발사한다.

        private void Skill()
        {

        }           // 플레이어가 스킬을 사용한다.


        // 23.4/4_GJ : 스페이스바 누를 시 처음엔 PlayerSkilled가 true가 됐는데 Enemy에서 false로 처리한 이후부터는 변화가 없어서 그냥 스페이스바 눌렀는지만 Enemy에 전해주기로 함
        /*private void OnTriggerStay(Collider other)
        {
            if (playerInput.skill)
            {
                Debug.Log("아아아아아ㅏㅇ아ㅏ아ㅏ ");
            }
            if (playerInput.skill && other.CompareTag("Snowball"))
            {
                Debug.Log("가가가가가ㅏ가가가");
                 PlayerSkilled = true;
            }
        }*/
    }
}
