using UnityEngine;
using System.IO;
using System.Text;

namespace GJ
{
    public class Player_Stat : MonoBehaviour
    {
        // 싱글톤으로 선언
        #region #Singleton
        private Player_Stat() { }
        private static Player_Stat _instance;
        public static Player_Stat Instance { get => _instance; }
        #endregion

        private float playerSpeed = 5.0f;                   // 플레이어 이동 속도
        private float playerSpeedMax = 10.0f;               // 플레이어 최대 이동 속도
        // public float rotateSpd = 180.0f;
        private int playerHp;                               // 플레이어 체력
        private int playerHpMax = 5;                        // 플레이어 최대 체력
        private float playerAttSpeed;                       // 플레이어 공격 속도
        private float playerAttSpeedMax = 0.08f;            // 플레이어 최대 공격 속도
        private int playerAttPower;                         // 플레이어 공격 파워
        private int playerAttPowerMax = 3;                  // 플레이어 최대 공격 파워
        private int numberOfBombs;                          // 플레이어가 가진 폭탄의 개수
        private int numberOfBombsMax = 3;                   // 플레이어가 가진 폭탄의 최대 개수
        private bool m_isDie;                               // 플레이어가 죽었는지 확인하는 bool 값
        private int currentScore = 0;                       // 플레이어의 현재 점수
        private int maxScore;                               // 플레이어의 최대 점수

        // UI용
        private bool isHpUp = false;                        // 플레이어 Hp가 늘었나?
        private bool isHpDown = false;                      // 플레이어 Hp가 줄었나?

        #region #Property
        public int Hp //{ get => playerHp; set => playerHp = value; }
        {
            get
            {
                return playerHp;
            }
            set
            {
                if (value >= playerHpMax)           // 들어온 값이 HpMax보다 크다면
                {
                    value = playerHpMax;            // 들어온 값을 HpMax로 바꾼다.
                }
                else
                {
                    if (playerHp > value)           // 들어온 값이 현재 값보다 작다면
                    {
                        isHpDown = true;            // 플레이어 Hp가 줄어들었다
                    }
                    else if (playerHp < value)      // 들어온 값이 현재 값보다 크다면
                    {
                        isHpUp = true;              // 플레이어 Hp가 늘어났다.
                    }
                }
                playerHp = value;
            }
        }
        public float MoveSpeed //{ get => playerSpeed; set => playerSpeed = value; }
        {
            get
            {
                return playerSpeed;
            }
            set
            {
                playerSpeed = value;
                if (playerSpeed >= playerSpeedMax)
                {
                    playerSpeed = playerSpeedMax;
                    return;     // 리턴 안해도 되나???
                }
            }
        }
        public float AttackSpeed
        {
            get
            {
                return playerAttSpeed;
            }
            set
            {
                // 공격속도로 더해진 값을 더해진 값만 0.1의 크기로 받아서 그 값을 현재 공격속도에다가 빼준다.
                // 공격속도 프로퍼티에 1을 더해주면 현재 공격속도 값에 0.1을 빼도록 설정
                playerAttSpeed -= (value - playerAttSpeed) * 0.1f;
                Debug.Log("여기보세요!!! 공격속도 바꼈어요!!!  :  " + playerAttSpeed);
                if (playerAttSpeed <= playerAttSpeedMax)
                {
                    // 공격속도 최대값을 0.08초에 한 번 나가는 걸로 변경
                    playerAttSpeed = playerAttSpeedMax;
                    return;
                }
            }
        }
        public int AttackPower //{ get => playerAttPower; set => playerAttPower = value; }
        {
            get
            {
                return playerAttPower;
            }
            set
            {
                playerAttPower = value;
                if (playerAttPower >= playerAttPowerMax)
                {
                    playerAttPower = playerAttPowerMax;
                    return;
                }
            }
        }
        public int NumberOfBombs
        {
            get
            {
                return numberOfBombs;
            }
            set
            {
                numberOfBombs = value;
                if (numberOfBombs >= numberOfBombsMax)
                {
                    numberOfBombs = numberOfBombsMax;
                }
            }
        }
        public bool IsDie { get => m_isDie; set => m_isDie = value; }
        public bool IsHpUp { get => isHpUp; set => isHpUp = value; }
        public bool IsHpDown { get => isHpDown; set => isHpDown = value; }
        public int CurrentScore { get => currentScore; set => currentScore = value; }
        public int MaxScore { get => maxScore; set => maxScore = value; }
        #endregion

        float count;
        void Awake()
        {
            #region #Singleton
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
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
            currentScore = 0;
        }

        /*public void Save()
        {
            PlayerState myPlayerState = new PlayerState();
            myPlayerState.playerName = "유저1";
            myPlayerState.score = CurrentScore;
            myPlayerState.ranking = new int[100];

            string json = JsonUtility.ToJson(myPlayerState);
            Debug.Log(json);

            string fileName = "Player1";
            string path = Application.dataPath + "/" + fileName + ".Json";

            FileStream fileStream = new FileStream(path, FileMode.Create);
            byte[] data = Encoding.UTF8.GetBytes(json);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }


    public class PlayerState
    {
        public string playerName;
        public int score;
        public int[] ranking;
    }*/
    }
}
