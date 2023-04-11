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
        public List<GameData> gameDatas = new List<GameData>();         // �����͸� �ε��� �� ������ ����
        public GameDataGroup gameDataGroup = new GameDataGroup();
        public int gameDatasAmount = 0;                                 // �ε��� �������� �迭 ����

        public int isMusic = 0;
        public int isSound = 0;
        public int gameTime = 0;
        public int gameScore = 0;

        /// <summary>
        /// ������ ������ ������ Json���� ����
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
        }
        public void LoadData()
        {
            Debug.Log("load");

            // path������ �����Ѵٸ�
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
