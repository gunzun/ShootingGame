using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Stat : MonoBehaviour
    {
        // 싱글톤으로 선언
        #region #Singleton
        private Player_Stat() { }
        private static Player_Stat instance;
        public static Player_Stat Instance { get => instance; }
        #endregion

        private float playerSpeed = 5.0f;                   // 플레이어 이동 속도
        // public float rotateSpd = 180.0f;
        private int playerHp;                               // 플레이어 체력
        private float playerAttSpeed;                       // 플레이어 공격 속도
        private float m_playerSpeed;                        // 플레이어 공격 속도를 set 으로 받을 변수
        private int playerAttPower;                         // 플레이어 공격 파워
        private int numberOfBombs;                          // 플레이어가 가진 폭탄의 개수
        private bool m_isDie;                               // 플레이어가 죽었는지 확인하는 bool 값


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
                // Player_Stat를 다른 객체도 가지고 있을 경우
                Debug.LogError("Player_Stat 이 다른 오브젝트에도 있나봐요");          // 에러 메시지를 띄우고
                Destroy(gameObject);                                                // 객체를 파괴한다.
            }
            #endregion

            playerHp = 5;
            playerAttSpeed = 0.5f;
            playerAttPower = 1;
            numberOfBombs = 3;
            m_isDie = false;
        }
    }
}
