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
        protected bool isSnowball = false;                  // ���� �����̰� �ƴ��� Ȯ��
        private float hpPercentage;                         // ���� ���� Hp�� ������ �������� ������ŭ ������� ��Ÿ����.
        private int snowballPrefabAmount = 6;               // ������ �������� ����
        private float snowballPercentage;                   // �������� ������ ������� ��Ÿ�� �� �� �ּڰ�
        private float[] snowballPercentages;                // �������� ������ ������� ��Ÿ�� ��� ���� �޴� �迭
        private bool isSnowballRolling = false;             // �����̰� �������°�?
        private float snowballDestroyCount = 3.0f;          // �����̰� �ı��Ǳ������ ī��Ʈ

        protected GameObject player;       // ������ �÷��̾� ������Ʈ

        #region #Property
        protected float EnemyHp { get => enemyHp; set => enemyHp = value; }
        public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
        protected float EnemyAttSpeed { get => enemyAttSpeed; set => enemyAttSpeed = value; }
        protected int EnemyAttPower { get => enemyAttPower; set => enemyAttPower = value; }
        #endregion

        protected virtual void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");            // �÷��̾� ������Ʈ�� ��������
            enemyHpOrigin = enemyHp;                                        // enemyHpOrigin�� �ʱ� ü������ �ʱ�ȭ
            snowballPercentages = new float[snowballPrefabAmount];          // ����캼 �迭�� ������ ������ŭ �ʱ�ȭ
            // ����캼 �������� ������� ��Ÿ���� ���� �ʱ�ȭ
            snowballPercentage = 100 / snowballPrefabAmount;                // ����캼 ������ �� �ּڰ�
            for (int i = 0; i < snowballPrefabAmount; i++)
            {
                snowballPercentages[i] = snowballPercentage * (i + 1);      // ��ü ����캼 �������� ������� ��Ÿ�� ����
            }
        }
        private void Update()
        {
            EnemyMove();

            // �÷��̾ ��ų�� ����ߴٸ�
            if (player.GetComponent<Player_Attack>().PlayerSkilled == true)
            {
                isSnowballRolling = true;                   // �����̸� ���� �� �ֵ��� isSnowballRolling �� true�� �Ѵ�.
            }
            // isSnowball�� true�� �� ��
            if (isSnowball == true)
            {
                EnemySpeed = 0.0f;                          // ���ʹ��� �ӵ��� ���ְ�
                StartCoroutine("EnemyDie");                 // �����̴� 3�� �ִٰ� �ı��ȴ�.
            }
            // isDie�� true�� �� ��
            else if (isDie)
            {
                Destroy(gameObject);                        // �ڽ��� ���ʹ� ������Ʈ�� �ı��Ѵ�.
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
            else                                            // �����̰� �Ǿ��ٸ�
            {
                // �ڿ� ���� ���ʹ̰� �������� ���� ����� ���ʹ� ��ġ�� �ڷ� �̵���Ų��.
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.6f);

                if (isSnowballRolling == true)                  // isSnowballRolling�� true�̰�
                {
                    if (other.CompareTag("Enemy"))              // �ٸ� ���ʹ̿� �浹�� ��
                    {
                        Destroy(other.gameObject);              // �浹�� ���ʹ̸� �ı��Ѵ�.
                    }
                    else if (other.CompareTag("Spawner"))       // �����ʿ� �浹�� ��
                    {
                        Destroy(gameObject);                    // �ڽ��� �ı��Ѵ�.
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }      // �ٸ� ������Ʈ�� �浹�� ��
        private void EnemyMove()
        {
            transform.position += Vector3.down * EnemySpeed * Time.deltaTime;
        }                         // ���ʹ̰� �����δ�.
        private void EnemyCollidesPlayer()
        {
            Player_Stat.Instance.Hp -= 1;           // �÷��̾� ü���� �ϳ� ����.
            if (m_type == ENEMY_TYPE.ENEMY3)        // ���� ���ʹ��� Ÿ���� ENEMY3 ���
            {
                Player_Stat.Instance.Hp -= 1;       // �÷��̾� ü���� �ϳ� �� ���.
            }
            isDie = true;                           // isDie�� true�� �ٲ۴�.
                                                    // �ڽ��� ������ ����Ʈ�� �� ����

        }               // ���ʹ̿� �÷��̾ �浹�� �� ó���� �Լ�
        private void EnemyCollidesBullet()
        {
            enemyHp -= 1;                                   // ���ʹ� �ڽſ��� ������� �ְ�
            #region Delay
            if (enemySpeed > delayValeue)                   // �ӵ��� �ִٸ� �ӵ��� ���̰�
            {
                enemySpeed -= delayValeue;
            }
            else                                            // �ӵ��� �ʹ� �����ٸ� 0���� �����
            {
                enemySpeed = 0f;
            }
            #endregion
            // ���� Hp���� ������ ������ ������ŭ�� ������� ��Ÿ���� ���� ��
            hpPercentage = (enemyHp / enemyHpOrigin) * 100.0f;

            // �Ѿ˿� ���� ������ ���ʹ̰� �����̿� ���δ�.
            #region hpPercentage ���� 0 ~ 100�� �ƴ� ���
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
            // ���ʹ��� HpPercentage�� �������� Percentage ���� �������� �ű⿡ ���� ������ �������� Ȱ��ȭ �Ѵ�.
            for (int i = snowballPrefabAmount - 1; i >= 0; i--)
            {
                if (hpPercentage <= snowballPercentages[i])
                {
                    transform.GetChild(snowballPrefabAmount - i - 1).gameObject.SetActive(true);
                }
            }
            // �ǰ� �ִϸ��̼��� �ְ�
            if (enemyHp <= 0 && isSnowball == false)        // ���� ���ʹ� ü���� ���ٸ� ��������� �����.
            {
                isSnowball = true;
            }
        }               // ���ʹ̿� �Ѿ��� �浹�� �� ó���� �Լ�
        private void SnowballRolling()
        {
            transform.position += Vector3.up * Player_Stat.Instance.AttackPower * Time.deltaTime;
        }                   // ���ʹ̰� �������� �� ���� ��������
        private IEnumerator EnemyDie()
        {
            if (isSnowballRolling == false)
            {
                yield return new WaitForSeconds(snowballDestroyCount);
                isDie = true;
            }
            else
            {
                SnowballRolling();                          // �ı����� �ʾҴٸ� ������ ��������.
                yield break;
            }
        }                   // ���ʹ̰� �������� �ʴ´ٸ� ������ �ð� ���� �ı��Ѵ�.
    }
}
