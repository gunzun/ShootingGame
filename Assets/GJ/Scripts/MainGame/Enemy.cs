using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Enemy : MonoBehaviour
    {
        protected ENEMY_TYPE m_type;                        // 적의 종류
        private float enemyHp;                              // 적의 체력
        private float enemySpeed;                           // 적의 기본 이동 속도
        private float enemyAttSpeed;                        // 적의 공격 속도
        private int enemyAttPower;                          // 적의 공격 파워
        protected float delayValeue;                        // 적이 공격받았을 때 적에게 줄 딜레이 값
        protected bool isDie = false;                       // 적이 죽었나 살았나 확인
        protected bool isSnowball = false;                  // 적이 눈덩이가 됐는지 확인

        #region #Property
        protected float EnemyHp { get => enemyHp; set => enemyHp = value; }
        public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
        protected float EnemyAttSpeed { get => enemyAttSpeed; set => enemyAttSpeed = value; }
        protected int EnemyAttPower { get => enemyAttPower; set => enemyAttPower = value; }
        #endregion
        private void Update()
        {
            EnemyMove();
        }
        private void EnemyMove()
        {
            transform.position += Vector3.down * EnemySpeed * Time.deltaTime;
        }                         // 에너미가 움직인다.
        private void OnTriggerEnter(Collider other)
        {
            // 필요한가? // 
            isDie = false;
            ///////////// 
            switch (other.tag)
            {
                case "Player":                                  // 에너미가 Player과 충돌했을 때
                    {
                        if (isSnowball == false)                // 에너미가 Snowball이 되지 않았다면
                        {
                            EnemyCollidesPlayer();              // 함수를 실행한다.
                        }
                        break;
                    }
                case "Bullet":                                  // 에너미가 Bullet과 충돌했을 때
                    {
                        if (isSnowball == false)                // 에너미가 Snowball이 되지 않았다면
                        {
                            Destroy(other.gameObject);          // 충돌한 총알을 파괴하고
                            EnemyCollidesBullet();              // 함수를 실행한다.
                        }
                        break;
                    }
                case "Deadzone":                                // 에너미가 Deadzone과 충돌했을 때
                    {
                        isDie = true;                           // isDie를 true로 바꾼다.
                        break;
                    }
            }
            if (isDie)                                      // isDie가 true가 될 때
            {
                Destroy(gameObject);                        // 자신인 에너미 오브젝트를 파괴한다.
            }
            else if (isSnowball)                            // isSnowball이 true가 될 때
            {
                EnemySpeed = 0.0f;                          // 에너미의 속도를 없애고
                // 에너미를 눈덩이로 바꾼다.
            }

        }      // 다른 오브젝트와 충돌할 때
        private void EnemyCollidesPlayer()
        {
            Player_Stat.Instance.Hp -= 1;           // 플레이어 체력을 하나 뺀다.
            if (m_type == ENEMY_TYPE.ENEMY3)        // 만약 에너미의 타입이 ENEMY3 라면
            {
                Player_Stat.Instance.Hp -= 1;       // 플레이어 체력을 하나 더 깐다.
            }
            isDie = true;                           // isDie를 true로 바꾼다.
            // 자신은 터지는 이펙트를 준 이후

        }               // 에너미와 플레이어가 충돌할 때 처리할 함수
        private void EnemyCollidesBullet()
        {
            enemyHp -= 1;                           // 에너미 자신에게 대미지를 주고
            if (enemySpeed > delayValeue)           // 속도가 있다면 속도를 줄이고
            {
                enemySpeed -= delayValeue;
            }
            else                                    // 속도가 너무 느리다면 0으로 만든다
            {
                enemySpeed = 0f;
            }
            // 피격 애니메이션을 주고
            if (enemyHp <= 0)                       // 만약 에너미 체력이 없다면 눈사람으로 만든다.
            {
                isSnowball = true;
            }
        }               // 에너미와 총알이 충돌할 때 처리할 함수
    }
}
