using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Enemy : MonoBehaviour
    {
        protected ENEMY_TYPE m_type;                        // ���� ����
        private float enemyHp;                              // ���� ü��
        protected float enemyOriginHp;                      // ���� �⺻ ü���� ���� ����
        protected float enemySpeed;                         // ���� �⺻ �̵� �ӵ�
        private float enemyAttSpeed;                        // ���� ���� �ӵ�
        private int enemyAttPower;                          // ���� ���� �Ŀ�
        protected float delayValeue;                        // ���� ���ݹ޾��� �� ������ �� ������ ��
        protected bool isDie = false;                       // ���� �׾��� ��ҳ� Ȯ��
        protected bool isSnowball = false;                  // ���� �����̰� �ƴ��� Ȯ��

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
        }                         // ���ʹ̰� �����δ�.
        private void OnTriggerEnter(Collider other)
        {
            // �ʿ��Ѱ�? // 
            isDie = false;
            ///////////// 
            switch (other.tag)
            {
                case "Player":                                  // ���ʹ̰� Player�� �浹���� ��
                    {
                        if (isSnowball == false)                // ���ʹ̰� Snowball�� ���� �ʾҴٸ�
                        {
                            EnemyCollidesPlayer();              // �Լ��� �����Ѵ�.
                        }
                        break;
                    }
                case "Bullet":                                  // ���ʹ̰� Bullet�� �浹���� ��
                    {
                        if (isSnowball == false)                // ���ʹ̰� Snowball�� ���� �ʾҴٸ�
                        {
                            Destroy(other.gameObject);          // �浹�� �Ѿ��� �ı��ϰ�
                            EnemyCollidesBullet();              // �Լ��� �����Ѵ�.
                        }
                        break;
                    }
                case "Deadzone":                                // ���ʹ̰� Deadzone�� �浹���� ��
                    {
                        isDie = true;                           // isDie�� true�� �ٲ۴�.
                        break;
                    }
            }
            if (isDie)                                      // isDie�� true�� �� ��
            {
                Destroy(gameObject);                        // �ڽ��� ���ʹ� ������Ʈ�� �ı��Ѵ�.
            }
            else if (isSnowball)                            // isSnowball�� true�� �� ��
            {
                EnemySpeed = 0.0f;                          // ���ʹ��� �ӵ��� ���ְ�
                // ���ʹ̸� �����̷� �ٲ۴�.
            }

        }      // �ٸ� ������Ʈ�� �浹�� ��
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
            enemyHp -= 1;                           // ���ʹ� �ڽſ��� ������� �ְ�
            if (enemySpeed > delayValeue)           // �ӵ��� �ִٸ� �ӵ��� ���̰�
            {
                enemySpeed -= delayValeue;
            }
            else                                    // �ӵ��� �ʹ� �����ٸ� 0���� �����
            {
                enemySpeed = 0f;
            }

            if (enemyHp >= enemyOriginHp)
            {
                Debug.LogError("Enemy�� Hp�� �̻��ؿ�");
                return;
            }
            else
            {
                float num = enemyHp / 6 * 100;
                if (num >= 100)
                {
                    num = 100f;
                }
                else if (num <= 0)
                {
                    num = 0;
                }
                if (num <= (100 / enemyOriginHp))
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (num <= (100 / enemyOriginHp) * 2)
                {
                    transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (num <= (100 / enemyOriginHp) * 3)
                {
                    transform.GetChild(2).gameObject.SetActive(true);
                }
                else if (num <= (100 / enemyOriginHp) * 3)
                {
                    transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (num <= (100 / enemyOriginHp) * 4)
                {
                    transform.GetChild(4).gameObject.SetActive(true);
                }
                else if (num <= (100 / enemyOriginHp) * 5)
                {
                    transform.GetChild(5).gameObject.SetActive(true);
                }
                else if (num <= (100 / enemyOriginHp) * 6)
                {
                    transform.GetChild(6).gameObject.SetActive(true);
                }
            }
            // �ǰ� �ִϸ��̼��� �ְ�
            if (enemyHp <= 0)                       // ���� ���ʹ� ü���� ���ٸ� ��������� �����.
            {
                isSnowball = true;
            }
        }               // ���ʹ̿� �Ѿ��� �浹�� �� ó���� �Լ�
    }
}
