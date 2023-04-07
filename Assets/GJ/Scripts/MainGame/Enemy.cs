using System.Collections;
using UnityEngine;

namespace GJ
{
    public class Enemy : MonoBehaviour
    {
        protected ENEMY_TYPE m_type;                        // ���� ����
        private float enemyHp;                              // ���� ���� ü��
        private float enemyHpOrigin;                        // ���� ���� ü��
        protected float enemySpeed;                         // ���� �⺻ �̵� �ӵ�
        private float enemyAttSpeed;                        // ���� ���� �ӵ�
        private int enemyAttPower;                          // ���� ���� �Ŀ�
        protected float delayValeue;                        // ���� ���ݹ޾��� �� ������ �� ������ ��
        protected bool isDie = false;                       // ���� �׾��� ��ҳ� Ȯ��
        protected bool isCrash;                             // ���� �÷��̾�� �浹�߳�?
        [SerializeField]
        protected bool isSnowball = false;                  // ���� �����̰� �ƴ��� Ȯ��
        private float hpPercentage;                         // ���� ���� Hp�� ������ �������� ������ŭ ������� ��Ÿ����.
        private int snowballPrefabAmount = 6;               // ������ �������� ����
        private float snowballPercentage;                   // �������� ������ ������� ��Ÿ�� �� �� �ּڰ�
        private float[] snowballPercentages;                // �������� ������ ������� ��Ÿ�� ��� ���� �޴� �迭
        private float snowballDestroyCount = 4.0f;          // �����̰� �ı��Ǳ������ ī��Ʈ
        [SerializeField]
        protected bool IsSnowballRolling = false;           // �����̸� �������ΰ�?
        private int EnemyPoint = 1;                         // ���� ���� �� ���� ����Ʈ


        protected GameObject player;        // ������ �÷��̾� ������Ʈ
        Player_Attack playerAttack;         // ������ �÷��̾���� Ŭ����
        private bool isPlayerSkilled;       // �÷��̾ ��ų�� ��°�?
        private Coroutine coroutine;        // �ڷ�ƾ�� ���� ����

        #region #Property
        protected float EnemyHp { get => enemyHp; set => enemyHp = value; }
        public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
        protected float EnemyAttSpeed { get => enemyAttSpeed; set => enemyAttSpeed = value; }
        protected int EnemyAttPower { get => enemyAttPower; set => enemyAttPower = value; }
        #endregion

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");            // �÷��̾� ������Ʈ�� ã��
            if (player != null)
            {
                playerAttack = player.GetComponent<Player_Attack>();            // �÷��̾���� Ŭ������ �޴´�.
                isPlayerSkilled = playerAttack.PlayerSkilled;                   // �÷��̾ ��ų�� ����� �޾ƿ´�.
            }   
            this.GetComponent<SpriteRenderer>().sortingOrder = 8;           // ���� �Ѿ˿� �±� ������ ȭ�� �� ���� �ִ�.

            enemyHpOrigin = enemyHp;                                        // enemyHpOrigin�� �ʱ� ü������ �ʱ�ȭ
            snowballPercentages = new float[snowballPrefabAmount];          // ����캼 �迭�� ������ ������ŭ �ʱ�ȭ
            #region # ����캼 �������� ������� ��Ÿ���� ���� �ʱ�ȭ
            snowballPercentage = 100 / snowballPrefabAmount;                // ����캼 ������ �� �ּڰ�
            for (int i = 0; i < snowballPrefabAmount; i++)
            {
                snowballPercentages[i] = snowballPercentage * (i + 1);      // ��ü ����캼 �������� ������� ��Ÿ�� ����
            }
            #endregion
        }
        private void Update()
        {
            EnemyMove();                                                                    // ���ʹ̰� �����δ�
            if (isDie)
            {
                Player_Stat.Instance.CurrentScore += EnemyPoint;                            // ���� ���� �� ���� �߰��ϰ�
                Destroy(gameObject);                                                        // ���� �ı��Ѵ�.
            }
            if (isSnowball)                                                                 // isSnowball�� �Ǹ�
            {
                coroutine = StartCoroutine(EnemyDie());                                     // EnemyDie()�� �����Ű��, �װ� coroutine ������ �־��ش�.
            }
            if (player.GetComponent<Player_Attack>().PlayerSkilled == true && isSnowball)   // �÷��̾ ��ų�� ����ߴٸ�
            {
                IsSnowballRolling = true;                                                   // SnowballIsRolling�� true
                player.GetComponent<Player_Attack>().PlayerSkilled = false;                 // ���� �����̰� �������� �ʵ��� PlayerSkilled�� false�� �����.
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);                                               // �����̸� �ı����� �ʴ´�.
                }
            }
            if (IsSnowballRolling)
            {
                SnowballRolling();                                                          // snowballIsRolling�̶�� �����̸� ��� ������ ������.
            }                                                       
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isSnowball == false)                            // �����̰� ���� �ʾ��� ���� ����
            {
                switch (other.tag)
                {
                    case "Player":                              // ���ʹ̰� Player�� �浹���� ��
                        {
                            EnemyCollidesPlayer();              // �Լ��� �����Ѵ�.
                            break;
                        }
                    case "Bullet":                              // ���ʹ̰� Bullet�� �浹���� ��
                        {
                            Destroy(other.gameObject);          // �浹�� �Ѿ��� �ı��ϰ�
                            EnemyCollidesBullet();              // �Լ��� �����Ѵ�.
                            break;
                        }
                    case "Deadzone":                            // ���ʹ̰� Deadzone�� �浹���� ��
                        {
                            isDie = true;                       // isDie�� true�� �ٲ۴�.
                            break;
                        }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (isSnowball == true && IsSnowballRolling)                // �����̰� �Ǿ���, �������ٸ�
            {
                if (other.CompareTag("Enemy"))                          // �ٸ� ���ʹ̿� �浹�� ��
                {
                    Player_Stat.Instance.CurrentScore += EnemyPoint;    // ��ŷ�� ���ʹ�����Ʈ�� �����ش�. 
                    Destroy(other.gameObject);                          // �浹�� ���ʹ̸� �ı��Ѵ�.
                }
            }
        }
        private void EnemyMove()
        {
            transform.position += Vector3.down * EnemySpeed * Time.deltaTime;
        }
        /// <summary>
        /// ���ʹ̿� �÷��̾ �浹�� �� ó���� �Լ�
        /// </summary>
        private void EnemyCollidesPlayer()
        {
            Player_Stat.Instance.Hp -= 1;           // �÷��̾� ü���� �ϳ� ����.
            if (m_type == ENEMY_TYPE.ENEMY3)        // ���� ���ʹ��� Ÿ���� ENEMY3 ���
            {
                Player_Stat.Instance.Hp -= 1;       // �÷��̾� ü���� �ϳ� �� ���.
            }
            isDie = true;                           // isDie�� true�� �ٲ۴�.
                                                    // �ڽ��� ������ ����Ʈ�� �� ����
        }
        /// <summary>
        /// ���ʹ̿� �Ѿ��� �浹�� �� ó���� �Լ�
        /// </summary>
        private void EnemyCollidesBullet()
        {
            enemyHp -= 1;                                                                       // ���ʹ� �ڽſ��� ������� �ְ�

            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            if (enemyHp <= 0)                                                                   // ���ʹ��� ü���� 0���� ������
            {
                this.GetComponent<SpriteRenderer>().sortingOrder = -snowballPrefabAmount;       // �ٸ� ���� ������ �ʵ��� ���̾ �ٲ��ش�.
                for (int i = snowballPrefabAmount - 1; i >= 0; --i)
                {
                    transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 
                        i - snowballPrefabAmount;                                               // �����̵鵵 ���̾ �ٲ��ش�.
                }
                enemySpeed = 0.0f;
                isSnowball = true;
                this.gameObject.tag = "Snowball";
                // coroutine = StartCoroutine(EnemyDie());              // �����̴� 3�� �ִٰ� �ı��ȴ�.
            }
            #region # ���� ������ ü�¿� ����� ���ʹ��� �ӵ��� �����̸� �ش�
            if (enemySpeed > delayValeue)                   // �ӵ��� �ִٸ� �ӵ��� ���̰�
            {
                enemySpeed -= delayValeue;
            }
            else                                            // �ӵ��� �ʹ� �����ٸ� 0���� �����
            {
                enemySpeed = 0f;
            }
            #endregion                                          //                                           // ���� Hp���� ������ ������ ������ŭ�� ������� ��Ÿ���� ���� ��    
            #region # ���ʹ��� �پ�� ü���� �ۼ�Ƽ���� ��ȯ�Ѵ�
            hpPercentage = (enemyHp / enemyHpOrigin) * 100.0f;
            // hpPercentage ���� 0 ~ 100�� �ƴ� ���
            if (hpPercentage >= 100)
            {
                Debug.LogWarning("Hp Percentage�� 100�� �Ѿ��");
                hpPercentage = 100f;
            }
            else if (hpPercentage <= 0)
            {
                // Debug.LogWarning("Hp Percentage�� 0���� �۾ƿ�");
                hpPercentage = 0;
            }
            #endregion
            #region # ���ʹ̰� �������� ���� ������ �������������� Ȱ��ȭ�Ѵ�
            for (int i = snowballPrefabAmount - 1; i >= 0; i--)     // snowballPrefab�� ������ 6�̶�� i = 5 ~ 0 �� ������ �ݺ��ȴ�.
            {
                if (hpPercentage <= snowballPercentages[i])             // �Ҹ��� �浹 �� Hp �ۼ�Ƽ���� snowball�� �ۼ�Ƽ������ �۾��� ���
                {
                    transform.GetChild(snowballPrefabAmount - i - 1).gameObject.SetActive(true);    // �ڽ��� 0���� 5���� Ȱ��ȭ�Ѵ�.
                }
            }
            #endregion

        }
        /// <summary>
        /// ���ʹ̰� �������� �� ���� ��������
        /// </summary>
        private void SnowballRolling()
        {
            transform.position += Vector3.up * Player_Stat.Instance.AttackPower * 3.0f * Time.deltaTime;
        }

        // 22.4/4_GJ : �����̸� �ڷ�ƾ���� ������ �� �� �ı��ϴ� �ڵ忡�� Update���� ī��Ʈ �ִ� ������ ����
        private IEnumerator EnemyDie()
        {
            EnemySpeed = 0.0f;                              // ���ʹ��� �ӵ��� ���ְ�
            yield return new WaitForSeconds(snowballDestroyCount);
            isDie = true;
        }                   // ������ �ð� ���� �ı��Ѵ�.
    }
}
