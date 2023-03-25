using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Attack : MonoBehaviour
    {
        public GameObject Bullet;               // 총알 프리팹
        public GameObject Snowball;             // 눈덩이 프리팹
        
        private Player_Input playerInput;

        public float attackSpd;                 // 공격속도
        private float count;                    // 공격 사이 카운트

        void Start()
        {
            // PlayerInput 가져오기
            playerInput = GetComponent<Player_Input>();

            // 공격 속도, 줄어들수록 빠르다
            attackSpd = 3f;
        }

        void Update()
        {
            // 공격 속도에 따라
            count += Time.deltaTime;

            //  총알 발사버튼을 눌렀을 때
            if (playerInput.fire == true && count >= attackSpd * 0.1f)
            {
                Fire();
                count = 0.0f;
            }
        }

        private void Fire()
        {
            Instantiate(Bullet.transform , this.transform.position + new Vector3(0f, 0.5f, 0f), this.transform.rotation);
        }
    }
}
