using UnityEngine;
using System.IO;
using System.Text;

namespace GJ
{
    public class Player_Stat : MonoBehaviour
    {
        // �̱������� ����
        #region #Singleton
        private Player_Stat() { }
        private static Player_Stat _instance;
        public static Player_Stat Instance { get => _instance; }
        #endregion

        private float playerSpeed = 5.0f;                   // �÷��̾� �̵� �ӵ�
        private float playerSpeedMax = 10.0f;               // �÷��̾� �ִ� �̵� �ӵ�
        // public float rotateSpd = 180.0f;
        [SerializeField]
        private int playerHp;                               // �÷��̾� ü��
        private int playerHpMax = 3;                        // �÷��̾� �ִ� ü��
        private float playerAttSpeed;                       // �÷��̾� ���� �ӵ�
        private float playerAttSpeedMax = 0.08f;            // �÷��̾� �ִ� ���� �ӵ�
        private int playerAttPower;                         // �÷��̾� ���� �Ŀ�
        private int playerAttPowerMax = 3;                  // �÷��̾� �ִ� ���� �Ŀ�
        private int numberOfBombs;                          // �÷��̾ ���� ��ź�� ����
        private int numberOfBombsMax = 3;                   // �÷��̾ ���� ��ź�� �ִ� ����
        private bool m_isDie;                               // �÷��̾ �׾����� Ȯ���ϴ� bool ��
        private int currentScore = 0;                       // �÷��̾��� ���� ����
        private int maxScore;                               // �÷��̾��� �ִ� ����
        private float playTime;                             // �÷��̾� �÷���Ÿ��
        public string userName { get; set; } = "User1";

        // UI��
        private bool isHpUp = false;                        // �÷��̾� Hp�� �þ���?
        private bool isHpDown = false;                      // �÷��̾� Hp�� �پ���?

        #region #Property
        public int Hp //{ get => playerHp; set => playerHp = value; }
        {
            get
            {
                return playerHp;
            }
            set
            {
                if (value > playerHpMax)            // ���� ���� HpMax���� ũ�ٸ�
                {
                    value = playerHpMax;            // ���� ���� HpMax�� �ٲ۴�.
                }
                else
                {
                    if (playerHp > value)           // ���� ���� ���� ������ �۴ٸ�
                    {
                        isHpDown = true;            // �÷��̾� Hp�� �پ�����
                    }
                    else if (playerHp < value)      // ���� ���� ���� ������ ũ�ٸ�
                    {
                        isHpUp = true;              // �÷��̾� Hp�� �þ��.
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
                    return;     // ���� ���ص� �ǳ�???
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
                // ���ݼӵ��� ������ ���� ������ ���� 0.1�� ũ��� �޾Ƽ� �� ���� ���� ���ݼӵ����ٰ� ���ش�.
                // ���ݼӵ� ������Ƽ�� 1�� �����ָ� ���� ���ݼӵ� ���� 0.1�� ������ ����
                playerAttSpeed -= (value - playerAttSpeed) * 0.1f;
                Debug.Log("���⺸����!!! ���ݼӵ� �ٲ����!!!  :  " + playerAttSpeed);
                if (playerAttSpeed <= playerAttSpeedMax)
                {
                    // ���ݼӵ� �ִ밪�� 0.08�ʿ� �� �� ������ �ɷ� ����
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
        public float PlayTime { get => playTime; set => playTime = value; }
        #endregion

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
                // Player_Stat�� �ٸ� ��ü�� ������ ���� ���
                Debug.LogError("Player_Stat �� �ٸ� ������Ʈ���� �ֳ�����");          // ���� �޽����� ����
                Destroy(gameObject);                                                // ��ü�� �ı��Ѵ�.
            }
            #endregion

            SetPlayerStat();
        }
        /// <summary>
        /// �÷��̾� ���� �ʱ�ȭ
        /// </summary>
        public void SetPlayerStat()
        {
            playerHp = 3;
            playerAttSpeed = 0.5f;
            playerAttPower = 1;
            numberOfBombs = 3;
            m_isDie = false;
            currentScore = 0;
        }

        /*public void Save()
        {
            PlayerState myPlayerState = new PlayerState();
            myPlayerState.playerName = "����1";
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
