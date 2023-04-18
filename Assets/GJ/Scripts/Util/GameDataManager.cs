using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using Unity.VisualScripting.YamlDotNet.Core;


namespace GJ
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public string fileName = "GameData.json";
        string path;
        // public GameData gameData = new GameData();
        public List<GameData> gameDatas = new List<GameData>();         // �����͸� �ε��� �� ������ ����
        public GameDataGroup gameDataGroup = new GameDataGroup();
        // List<GameData> sortedList;                                   // �����͸� ������ ���� ������������ ������ ����Ʈ
        public string jsonData = "";
        byte[] bytes;                                                   // JSON ������ ������ ����Ʈ�� ��ȯ�� �迭

        #region Error@!!
        // 23.04.14.GJ : ���� �����͸� Sort �ϸ� ���ڱ� gameDatasAmount�� 0�� ��.... �������� ���ڰ� �־��µ�... �ٵ� �� ������Ƽ�� ����� ���� �۵���
        /* public int gameDatasAmount = 0;                                // �ε��� �������� �迭 ����
                                                                       // int tempGameDatasAmount = 0;
        public int GameDatasAmount
        {
            get
            {
                int tempGameDatasAmount = gameDatasAmount;              // �ε��� �������� �迭 ������ ��ŷ ������ �θ� ������ ���� �����͸� 0���� �ʱ�ȭ�Ѵ�.
                if (gameDatasAmount != 0)
                {
                    gameDatasAmount = 0;                                    // �ε��� ������ ���Ʈ�� �ߺ��� �Ǿ �߰��� �Ǳ� ����...
                }
                return tempGameDatasAmount;
            }
        }*/
        #endregion
        public GameData bestScoreData;                                  // �ְ� ���� ������
        public int bestScore = -1;                                      // ������ ���Ͽ� ����� �ְ� ���� ������ �迭�� ���ھ�
        public string bestScoreUserName;                                // ������ ���Ͽ� ����� �ְ� ���� ������ �迭�� ���� �̸�

        public int isMusic = 0;
        public int isSound = 0;
        public int gameTime = 0;
        public int gameScore = 0;

        /// <summary>
        /// ������ ������ ������ Json���� ����
        /// </summary>
        /// <param name="_gameDataGroup"></param>
        private void SaveData(GameDataGroup _gameDataGroup)
        {
            Debug.Log(path);
            string toJsonData = JsonUtility.ToJson(_gameDataGroup, true);
            File.WriteAllText(path, toJsonData);
            gameTime = 0;
        }

        /// <summary>
        /// ������ ���� �����͸� �����մϴ�.
        /// </summary>
        /// <param name="_id">������ �÷��̾��� ID</param>
        /// <param name="_stageTime">������ �÷��� Ÿ��</param>
        /// <param name="_Score">������ ���ھ�</param>
        /// <returns></returns>
        public GameData CreateGameData(string _id, float _stageTime, int _Score)
        {
            GameData gameData = new GameData();
            gameData.SetData(_id, _stageTime, _Score);
            return gameData;
        }


        /// <summary>
        /// ���ӵ����͸� �����մϴ�.
        /// </summary>
        /// <param name="_id">������ �÷��̾��� ID</param>
        /// <param name="_stageTime">������ �÷��� Ÿ��</param>
        /// <param name="_Score">������ ���ھ�</param>
        public void SaveData(string _id, float _stageTime, int _Score)
        {
            gameDatas.Add(CreateGameData(_id, _stageTime, _Score));
            gameDataGroup.rank = gameDatas.ToArray();
            SaveData(gameDataGroup);
        }
        void Start()
        {
            path = Application.dataPath + "/" + fileName;       // Json ���� ���� ���
            LoadData();                                         // �̸� �ִ� ������ ����
            SortListInDescendingOrderByScore();                 // ����Ʈ�� ���ھ �������� �������� ����
        }
        public void LoadData()
        {
            Debug.Log("load");

            // path������ �����Ѵٸ�
            if (File.Exists(path))
            {
                jsonData = File.ReadAllText(path);
                bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);               // ����Ʈ ������ JSON������ �޴´�.
                gameDataGroup = JsonUtility.FromJson<GameDataGroup>(jsonData);
                for (int i = 0; i < gameDataGroup.rank.Length; i++)
                {
                    if (gameDataGroup.rank[i] == null)
                    {
                        continue;
                    }
                    else
                    {
                        gameDatas.Add(gameDataGroup.rank[i]);
                    }
                }
            }
        }
        /// <summary>
        /// ������ ���� ���� ������ ����Ʈ�� ������������ �����Ѵ�.
        /// </summary>
        public void SortListInDescendingOrderByScore()
        {
            // Gamedatas�� score�� Key�Ͽ� ������ ��� ������������ �����Ѵ�. Linq ó���ẻ��
            List<GameData> newGameDatas = gameDatas.OrderByDescending<GameData, int>(p => p.score).ToList<GameData>();
            gameDatas = newGameDatas;

            // ���ĵ� gameDatas ����Ʈ�� JSON���ٰ� �������ش�.
            gameDataGroup.rank = gameDatas.ToArray();
            SaveData(gameDataGroup);
        }

        public bool isFileExist()
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // 23.04.13 GJ -> ������ ����Ʈ 0��°�� �ְ� �����ϰǵ� �ְ� ������ �˾ƾ� �� �ʿ䰡 �ֳ�?
        /*public void BestRecordScore()
        {
            if (File.Exists(path))
            {
                for (int i = 0; i < gameDataGroup.rank.Length; i++)
                {
                    if (gameDatas[i].score > bestScore)
                    {
                        bestScoreData = gameDatas[i];
                    }
                }
                bestScoreUserName = bestScoreData.id;
            }
        }*/


        private GameData GetGameData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("Http");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = bytes.Length;

            using(var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close(); 
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            GameData info = JsonUtility.FromJson<GameData>(json);
            return info;
        }


        public void SendDataToServer()
        {
            try
            {
                string JsonData = File.ReadAllText(path);
                byte[] arr = System.Text.Encoding.UTF8.GetBytes(JsonData);
                // NetworkSession.Instance.SendPacket(arr, arr.Length);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }

    }
}
