using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
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
        public int gameDatasAmount = 0;                                 // 로드한 데이터의 배열 개수

        public int isMusic = 0;
        public int isSound = 0;
        public int gameTime = 0;
        public int gameScore = 0;

        /// <summary>
        /// 게임의 데이터 파일을 Json으로 저장
        /// </summary>
        /// <param name="_gameData"></param>
        private void SaveData(GameDataGroup _gameData)
        {
            Debug.Log(path);
            string toJsonData = JsonUtility.ToJson(_gameData, true);
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
            LoadData();                                         // 미리 있던 파일을 저장
        }
        public void LoadData()
        {
            Debug.Log("load");

            // path파일이 존재한다면
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                gameDataGroup = JsonUtility.FromJson<GameDataGroup>(jsonData);
                for (int i = 0; i < gameDataGroup.rank.Length; i++)
                {
                    gameDatasAmount += 1;
                    gameDatas.Add(gameDataGroup.rank[i]);
                }
            }
        }
    }
}
