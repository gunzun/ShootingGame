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
        [SerializeField]
        protected bool isSnowball = false;                  // 적이 눈덩이가 됐는지 확인
        private float hpPercentage;                         // 적의 현재 Hp를 눈덩이 프리팹의 개수만큼 백분율로 나타낸다.
        private int snowballPrefabAmount = 6;               // 눈덩이 프리팹의 개수
        private float snowballPercentage;                   // 눈덩이의 개수를 백분율로 나타낸 값 중 최솟값
        private float[] snowballPercentages;                // 눈덩이의 개수를 백분율로 나타낸 모든 값을 받는 배열
        private float snowballDestroyCount = 4.0f;          // 눈덩이가 파괴되기까지의 카운트
        [SerializeField]
        protected bool IsSnowballRolling = false;           // 눈덩이를 굴릴것인가?

        protected GameObject player;        // 가져올 플레이어 오브젝트
        Player_Attack playerAttack;         // 가져올 플레이어어택 클래스
        private bool isPlayerSkilled;       // 플레이어가 스킬을 썼는가?
        private Coroutine coroutine;        // 코루틴을 담을 변수

        #region #Property
        protected float EnemyHp { get => enemyHp; set => enemyHp = value; }
        public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
        protected float EnemyAttSpeed { get => enemyAttSpeed; set => enemyAttSpeed = value; }
        protected int EnemyAttPower { get => enemyAttPower; set => enemyAttPower = value; }
        #endregion

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");            // 플레이어 오브젝트를 찾고
            playerAttack = player.GetComponent<Player_Attack>();            // 플레이어어택 클래스를 받는다.
            isPlayerSkilled = playerAttack.PlayerSkilled;                   // 플레이어가 스킬을 썼는지 받아온다.

            enemyHpOrigin = enemyHp;                                        // enemyHpOrigin을 초기 체력으로 초기화
            snowballPercentages = new float[snowballPrefabAmount];          // 스노우볼 배열을 프리팹 개수만큼 초기화
            #region # 스노우볼 프리팹을 백분율로 나타내기 위해 초기화
            snowballPercentage = 100 / snowballPrefabAmount;                // 스노우볼 프리팹 중 최솟값
            for (int i = 0; i < snowballPrefabAmount; i++)
            {
                snowballPercentages[i] = snowballPercentage * (i + 1);      // 전체 스노우볼 프리팹을 백분율로 나타낸 값들
            }
            #endregion
        }
        private void Update()
        {
            EnemyMove();                                                                    // 에너미가 움직인다
            if (isDie)
            {
                Destroy(gameObject);
            }                                                                   // isDie가 true가 될 때 에너미 파괴한다
            if (isSnowball)
            {

                // coroutine = StartCoroutine(EnemyDie());                         // EnemyDie()를 실행시키고, 그걸 coroutine 변수에 넣어준다.
            }                                                              // isSnowball이 true가 될 때 에너미 속도를 없애고, 약간 뒤로 배치한다.
            if (player.GetComponent<Player_Attack>().PlayerSkilled == true && isSnowball)   // 플레이어가 스킬을 사용했다면
            {
                IsSnowballRolling = true;                                                   // SnowballIsRolling은 true
                player.GetComponent<Player_Attack>().PlayerSkilled = false;                 // 다음 눈덩이가 굴러가지 않도록 PlayerSkilled를 false로 만든다.
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);                                               // 눈덩이를 파괴하지 않고
                }
            }
            if (IsSnowballRolling)
            {
                SnowballRolling();
            }                                                       // snowballIsRolling이라면 눈덩이를 계속 앞으로 굴린다.
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
                if (IsSnowballRolling == true)                  // snowballIsRolling이 true이고
                {
                    if (other.CompareTag("Enemy"))              // 다른 에너미와 충돌할 때
                    {
                        Destroy(other.gameObject);              // 충돌한 에너미를 파괴한다.
                    }
                    else if (other.CompareTag("Spawner"))       // 스포너와 충돌할 때
                    {
                        isDie = true;                           // 자신을 파괴한다.
                    }
                }
            }
        }
        private void EnemyMove()
        {
            transform.position += Vector3.down * EnemySpeed * Time.deltaTime;
        }
        /// <summary>
        /// 에너미와 플레이어가 충돌할 때 처리할 함수
        /// </summary>
        private void EnemyCollidesPlayer()
        {
            Player_Stat.Instance.Hp -= 1;           // 플레이어 체력을 하나 뺀다.
            if (m_type == ENEMY_TYPE.ENEMY3)        // 만약 에너미의 타입이 ENEMY3 라면
            {
                Player_Stat.Instance.Hp -= 1;       // 플레이어 체력을 하나 더 깐다.
            }
            isDie = true;                           // isDie를 true로 바꾼다.
                                                    // 자신은 터지는 이펙트를 준 이후
        }
        /// <summary>
        /// 에너미와 총알이 충돌할 때 처리할 함수
        /// </summary>
        private void EnemyCollidesBullet()
        {
            enemyHp -= 1;                                   // 에너미 자신에게 대미지를 주고
            if (enemyHp <= 0)                               // 에너미의 체력이 0보다 적으면
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = -snowballPrefabAmount;
                for (int i = snowballPrefabAmount - 1; i >= 0; --i)
                {
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = i - snowballPrefabAmount;
                }
                enemySpeed = 0.0f;
                isSnowball = true;
                this.gameObject.tag = "Snowball";
                // coroutine = StartCoroutine(EnemyDie());              // 눈덩이는 3초 있다가 파괴된다.
            }
            #region # 맞을 때마다 체력에 비례해 에너미의 속도에 딜레이를 준다
            if (enemySpeed > delayValeue)                   // 속도가 있다면 속도를 줄이고
            {
                enemySpeed -= delayValeue;
            }
            else                                            // 속도가 너무 느리다면 0으로 만든다
            {
                enemySpeed = 0f;
            }
            #endregion                                          //                                           // 적의 Hp값을 눈덩이 프리팹 개수만큼의 백분율로 나타내기 위한 값    
            #region # 에너미의 줄어든 체력을 퍼센티지로 변환한다
            hpPercentage = (enemyHp / enemyHpOrigin) * 100.0f;
            // hpPercentage 값이 0 ~ 100이 아닐 경우
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
            #region # 에너미가 데미지를 입을 때마다 눈덩이프리팹을 활성화한다
            for (int i = snowballPrefabAmount - 1; i >= 0; i--)     // snowballPrefab의 개수가 6이라면 i = 5 ~ 0 의 범위로 반복된다.
            {
                if (hpPercentage <= snowballPercentages[i])             // 불릿에 충돌 후 Hp 퍼센티지가 snowball의 퍼센티지보다 작아질 경우
                {
                    transform.GetChild(snowballPrefabAmount - i - 1).gameObject.SetActive(true);    // 자식을 0부터 5까지 활성화한다.
                }
            }
            #endregion

        }
        /// <summary>
        /// 에너미가 눈덩이일 때 위로 굴러간다
        /// </summary>
        private void SnowballRolling()
        {
            transform.position += Vector3.up * Player_Stat.Instance.AttackPower * 3.0f * Time.deltaTime;
        }

        // 22.4/4_GJ : 눈덩이를 코루틴으로 딜레이 준 후 파괴하는 코드에서 Update에서 카운트 주는 것으로 변경
        /*private IEnumerator EnemyDie()
        {
            EnemySpeed = 0.0f;                              // 에너미의 속도를 없애고
            yield return new WaitForSeconds(snowballDestroyCount);
            isDie = true;
        }*/                   // 지정된 시간 이후 파괴한다.
    }
}
