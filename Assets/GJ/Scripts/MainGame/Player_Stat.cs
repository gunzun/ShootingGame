using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Stat : MonoBehaviour
    {
        // �̱������� ����
        #region #Singleton
        private Player_Stat() { }
        private static Player_Stat instance;
        public static Player_Stat Instance { get => instance; }
        #endregion

        private float playerSpeed = 5.0f;                   // �÷��̾� �̵� �ӵ�
        // public float rotateSpd = 180.0f;
        private int playerHp;                               // �÷��̾� ü��
        private float playerAttSpeed;                       // �÷��̾� ���� �ӵ�
        private float m_playerSpeed;                        // �÷��̾� ���� �ӵ��� set ���� ���� ����
        private int playerAttPower;                         // �÷��̾� ���� �Ŀ�
        private int numberOfBombs;                          // �÷��̾ ���� ��ź�� ����
        private bool m_isDie;                               // �÷��̾ �׾����� Ȯ���ϴ� bool ��      


        #region #Property
        public int Hp { get => playerHp; set => playerHp = value; }
        public float MoveSpeed { get => playerSpeed; set => playerSpeed = value; }
        public float AttackSpeed
        {
            get
            {
                return playerAttSpeed;
            }
            set
            {
                playerAttSpeed -= value * 0.1f;
                if (playerAttSpeed <= 0.08f)
                {
                    playerAttSpeed = 0.08f;
                    return;
                }
            }
        }
        public int AttackPower { get => playerAttPower; set => playerAttPower = value; }
        public int NumberOfBombs { get => numberOfBombs; set => numberOfBombs = value; }
        public bool IsDie { get => m_isDie; set => m_isDie = value; }
        #endregion

        float count;
        void Start()
        {
            #region #Singleton
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                // Player_Stat�� �ٸ� ��ü�� ������ ���� ���
                Debug.LogError("Player_Stat �� �ٸ� ������Ʈ���� �ֳ�����");          // ���� �޽����� ����
                Destroy(gameObject);                                                // ��ü�� �ı��Ѵ�.
            }
            #endregion

            playerHp = 5;
            playerAttSpeed = 0.5f;
            playerAttPower = 1;
            numberOfBombs = 3;
            m_isDie = false;
        }

        private void Update()
        {
            count += Time.deltaTime;
            if (count >= 1.0f)
            {
                Debug.Log(playerAttSpeed);
            }
        }
    }
}
