using System.Collections;
using UnityEngine;

namespace GJ
{
    public class Enemy : MonoBehaviour
    {
        protected ENEMY_TYPE m_type;                        // 적의 종류
        private float enemyHp;                              // 적의 현재 체력
        private float enemyHpOrigin;                        // 적의 원래 체력
        protected float enemySpeed;                         // 적의 기본 이동 속도
        private float enemyAttSpeed;                        // 적의 공격 속도
        private int enemyAttPower;                          // 적의 공격 파워
        protected float delayValeue;                        // 적이 공격받았을 때 적에게 줄 딜레이 값
        protected bool isDie = false;                       // 적이 죽었나 살았나 확인
        protected bool isSnowball = false;                  // 적이 눈덩이가 됐는지 확인
        private float hpPercentage;                         // 적의 현재 Hp를 눈덩이 프리팹의 개수만큼 백분율로 나타낸다.
        private int snowballPrefabAmount = 6;               // 눈덩이 프리팹의 개수
        private float snowballPercentage;                   // 눈덩이의 개수를 백분율로 나타낸 값 중 최솟값
        private float[] snowballPercentages;                // 눈덩이의 개수를 백분율로 나타낸 모든 값을 받는 배열
        private bool isSnowballRolling = false;             // 눈덩이가 굴러갔는가?
        private float snowballDestroyCount = 3.0f;          // 눈덩이가 파괴되기까지의 카운트

        protected GameObject player;       // 가져올 플레이어 오브젝트

        #region #Property
        protected float EnemyHp { get => enemyHp; set => enemyHp = value; }
        public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
        protected float EnemyAttSpeed { get => enemyAttSpeed; set => enemyAttSpeed = value; }
        protected int EnemyAttPower { get => enemyAttPower; set => enemyAttPower = value; }
        #endregion

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");            // 플레이어 오브젝트를 가져오고
            enemyHpOrigin = enemyHp;                                        // enemyHpOrigin을 초기 체력으로 초기화
            snowballPercentages = new float[snowballPrefabAmount];          // 스노우볼 배열을 프리팹 개수만큼 초기화
            // 스노우볼 프리팹을 백분율로 나타내기 위해 초기화
            snowballPercentage = 100 / snowballPrefabAmount;                // 스노우볼 프리팹 중 최솟값
            for (int i = 0; i < snowballPrefabAmount; i++)
            {
                snowballPercentages[i] = snowballPercentage * (i + 1);      // 전체 스노우볼 프리팹을 백분율로 나타낸 값들
            }
        }
        private void Update()
        {
            EnemyMove();

            // 플레이어가 스킬을 사용했다면
            if (player.GetComponent<Player_Attack>().PlayerSkilled == true)
            {
                isSnowballRolling = true;                   // 눈덩이를 굴릴 수 있도록 isSnowballRolling 을 true로 한다.
            }
            // isSnowball이 true가 될 때
            if (isSnowball == true)
            {
                EnemySpeed = 0.0f;                          // 에너미의 속도를 없애고
                StartCoroutine("EnemyDie");                 // 눈덩이는 3초 있다가 파괴된다.
            }
            // isDie가 true가 될 때
            else if (isDie)
            {
                Destroy(gameObject);                        // 자신인 에너미 오브젝트를 파괴한다.
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isSnowball == false)                            // 눈덩이가 되지 않았을 때만 실행
            {
                switch (other.tag)
                {
                    case "Player":                              // 에너미가 Player과 충돌했을 때
                        {
                            EnemyCollidesPlayer();              // 함수를 실행한다.
                            break;
                        }
                    case "Bullet":                              // 에너미가 Bullet과 충돌했을 때
                        {
                            Destroy(other.gameObject);          // 충돌한 총알을 파괴하고
                            EnemyCollidesBullet();              // 함수를 실행한다.
                            break;
                        }
                    case "Deadzone":                            // 에너미가 Deadzone과 충돌했을 때
                        {
                            isDie = true;                       // isDie를 true로 바꾼다.
                            break;
                        }
                }
            }
            else                                            // 눈덩이가 되었다면
            {
                // 뒤에 나온 에너미가 가려지는 것을 대비해 에너미 위치를 뒤로 이동시킨다.
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.6f);

                if (isSnowballRolling == true)                  // isSnowballRolling이 true이고
                {
                    if (other.CompareTag("Enemy"))              // 다른 에너미와 충돌할 때
                    {
                        Destroy(other.gameObject);              // 충돌한 에너미를 파괴한다.
                    }
                    else if (other.CompareTag("Spawner"))       // 스포너와 충돌할 때
                    {
                        Destroy(gameObject);                    // 자신을 파괴한다.
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }      // 다른 오브젝트와 충돌할 때
        private void EnemyMove()
        {
            transform.position += Vector3.down * EnemySpeed * Time.deltaTime;
        }                         // 에너미가 움직인다.
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
            enemyHp -= 1;                                   // 에너미 자신에게 대미지를 주고
            #region Delay
            if (enemySpeed > delayValeue)                   // 속도가 있다면 속도를 줄이고
            {
                enemySpeed -= delayValeue;
            }
            else                                            // 속도가 너무 느리다면 0으로 만든다
            {
                enemySpeed = 0f;
            }
            #endregion
            // 적의 Hp값을 눈덩이 프리팹 개수만큼의 백분율로 나타내기 위한 값
            hpPercentage = (enemyHp / enemyHpOrigin) * 100.0f;

            // 총알에 맞을 때마다 에너미가 눈덩이에 쌓인다.
            #region hpPercentage 값이 0 ~ 100이 아닐 경우
            if (hpPercentage >= 100)
            {
                Debug.LogWarning("Hp Percentage가 100이 넘어요");
                hpPercentage = 100f;
            }
            else if (hpPercentage <= 0)
            {
                // Debug.LogWarning("Hp Percentage가 0보다 작아요");
                hpPercentage = 0;
            }
            #endregion
            // 에너미의 HpPercentage가 눈덩이의 Percentage 보다 낮아지면 거기에 따른 눈덩이 프리팹을 활성화 한다.
            for (int i = snowballPrefabAmount - 1; i >= 0; i--)
            {
                if (hpPercentage <= snowballPercentages[i])
                {
                    transform.GetChild(snowballPrefabAmount - i - 1).gameObject.SetActive(true);
                }
            }
            // 피격 애니메이션을 주고
            if (enemyHp <= 0 && isSnowball == false)        // 만약 에너미 체력이 없다면 눈사람으로 만든다.
            {
                isSnowball = true;
            }
        }               // 에너미와 총알이 충돌할 때 처리할 함수
        private void SnowballRolling()
        {
            transform.position += Vector3.up * Player_Stat.Instance.AttackPower * Time.deltaTime;
        }                   // 에너미가 눈덩이일 때 위로 굴러간다
        private IEnumerator EnemyDie()
        {
            if (isSnowballRolling == false)
            {
                yield return new WaitForSeconds(snowballDestroyCount);
                isDie = true;
            }
            else
            {
                SnowballRolling();                          // 파괴되지 않았다면 앞으로 굴러간다.
                yield break;
            }
        }                   // 에너미가 굴러가지 않는다면 지정된 시간 이후 파괴한다.
    }
}
