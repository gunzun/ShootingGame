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
        public List<GameData> gameDatas = new List<GameData>();         // 데이터를 로드할 때 가져갈 변수
        public GameDataGroup gameDataGroup = new GameDataGroup();
        // List<GameData> sortedList;                                   // 데이터를 점수에 따라 내림차순으로 정렬한 리스트
        public string jsonData = "";
        public string ReceiveJsonData = "";                             // 서버에서 받아올 JSON 데이터 문자열
        byte[] bytes;                                                   // JSON 데이터 파일을 바이트로 변환한 배열

        #region Error@!!
        // 23.04.14.GJ : 게임 데이터를 Sort 하면 갑자기 gameDatasAmount가 0이 됨.... 그전까진 숫자가 있었는데... 근데 또 프로퍼티를 지우니 정상 작동함
        /* public int gameDatasAmount = 0;                                // 로드한 데이터의 배열 개수
                                                                       // int tempGameDatasAmount = 0;
        public int GameDatasAmount
        {
            get
            {
                int tempGameDatasAmount = gameDatasAmount;              // 로드한 데이터의 배열 개수를 랭킹 씬에서 부른 다음에 게임 데이터를 0으로 초기화한다.
                if (gameDatasAmount != 0)
                {
                    gameDatasAmount = 0;                                    // 로드할 때마다 어마운트가 중복이 되어서 추가가 되기 때문...
                }
                return tempGameDatasAmount;
            }
        }*/
        #endregion
        public GameData bestScoreData;                                  // 최고 점수 데이터
        public int bestScore = -1;                                      // 데이터 파일에 저장된 최고 점수 데이터 배열의 스코어
        public string bestScoreUserName;                                // 데이터 파일에 저장된 최고 점수 데이터 배열의 유저 이름

        public int isMusic = 0;
        public int isSound = 0;
        public int gameTime = 0;
        public int gameScore = 0;

        /// <summary>
        /// 게임의 데이터 파일을 Json으로 저장
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
        /// 저장할 게임 데이터를 생성합니다.
        /// </summary>
        /// <param name="_id">생성할 플레이어의 ID</param>
        /// <param name="_stageTime">생성할 플레이 타임</param>
        /// <param name="_Score">생성할 스코어</param>
        /// <returns></returns>
        public GameData CreateGameData(string _id, float _stageTime, int _Score)
        {
            GameData gameData = new GameData();
            gameData.SetData(_id, _stageTime, _Score);
            return gameData;
        }


        /// <summary>
        /// 게임데이터를 저장합니다.
        /// </summary>
        /// <param name="_id">저장할 플레이어의 ID</param>
        /// <param name="_stageTime">저장할 플레이 타임</param>
        /// <param name="_Score">저장할 스코어</param>
        public void SaveData(string _id, float _stageTime, int _Score)
        {
            gameDatas.Add(CreateGameData(_id, _stageTime, _Score));
            gameDataGroup.rank = gameDatas.ToArray();
            SaveData(gameDataGroup);
        }
        void Start()
        {
            path = Application.dataPath + "/" + fileName;       // Json 파일 저장 경로
            LoadData();                                         // 미리 있던 파일을 리스트로 들고온다.
            SortListInDescendingOrderByScore();                 // 리스트를 스코어를 기준으로 내림차순 정렬
        }
        public void LoadData()
        {
            Debug.Log("load");

            // path파일이 존재한다면
            if (File.Exists(path))
            {
                jsonData = File.ReadAllText(path);
                bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);               // 바이트 단위로 JSON파일을 받는다.
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
        /// 점수에 따라 유저 데이터 리스트를 내림차순으로 정렬한다.
        /// </summary>
        public void SortListInDescendingOrderByScore()
        {
            // Gamedatas의 score를 Key하여 기준을 잡고 내림차순으로 정렬한다. Linq 처음써본다
            List<GameData> newGameDatas = gameDatas.OrderByDescending<GameData, int>(p => p.score).ToList<GameData>();
            gameDatas = newGameDatas;

            // 정렬된 gameDatas 리스트를 JSON에다가 저장해준다.
            gameDataGroup.rank = gameDatas.ToArray();
            SaveData(gameDataGroup);
        }

        /// <summary>
        /// 서버의 JSON_string 데이터를 List<Gamedata>로 바꾼 값을 반환한다.
        /// </summary>
        /// <param name="_data">서버에서 받아온 JSON_string 데이터</param>
        /// <returns></returns>
        public GameDataGroup ServerData_To_List(string _data)
        {
            GameDataGroup gamedatas_Server = new GameDataGroup();
            gamedatas_Server = JsonUtility.FromJson<GameDataGroup>(_data);
            return gamedatas_Server;
        }

        /// <summary>
        /// 서버에서 받아온 데이터와 로컬에 있는 데이터를 비교 후 중복되는 값을 제거한다.
        /// <paramref name="_data"/>서버에서 받아온 JSON_string 데이터</paramref>
        /// </summary>
        public void CheckDuplicate_ServerData(string _data)
        {
            // 데이터가 비어있을 시 리턴
            if (System.String.IsNullOrEmpty(_data) == false)
            {
                GameDataGroup serverData = new GameDataGroup();
                serverData = ServerData_To_List(_data);             // _data를 리스트로 변환

                gameDataGroup.rank = gameDataGroup.rank.Except(serverData.rank).ToArray();  // 중복 검사 후 데이터 저장

                // var substractaction = gameDataGroup.rank.Select(item => new { item.id, item.playTime, item.score }).Except(serverData.rank.Select(item => new { item.id, item.playTime, item.score }));
                // var substractaction = (from rank in serverData.rank select new { rank.id, rank.playTime, rank.score }).Except(gameDataGroup.rank.Select(field => new { field.id, field.playTime, field.score })).ToArray<GameData>();

                // foreach (var data in substractaction)
                // {
                //     System.Console.WriteLine(data.ToString());
                // }

                gameDatas.AddRange(serverData.rank);

                gameDatas = gameDatas.Distinct().ToList();
                gameDataGroup.rank = gameDatas.ToArray();
            }
            else
            {
                Debug.LogWarning("Server Data is Empty");
                return;
            }
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

        // 23.04.13 GJ -> 어차피 리스트 0번째가 최고 점수일건데 최고 점수를 알아야 할 필요가 있나?
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
        /*
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
        */
    }
}
