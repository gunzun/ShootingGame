/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Unity.VisualScripting.YamlDotNet.Core;

namespace GJ
{
    public class GameDataManager_T : Singleton<GameDataManager_T>
    {
        public string fileName = "GameData.json";
        string path;
        // public GameData gameData = new GameData();
        public List<GameData> gameDatas = new List<GameData>();
        public GameDataGroup gameDataGroup = new GameDataGroup();

        public int isMusic = 0;
        public int issound = 0;
        public int gameTime = 0;


        public void SaveData(GameDataGroup gameData)
        {
            Debug.Log(path);
            string toJsonData = JsonUtility.ToJson(gameData, true);
            Debug.Log(toJsonData);
            File.WriteAllText(path, toJsonData); ;
            gameTime = 0;
        }
        public GameData CreateGameData(string id, float stageTime, int stageScore)
        {
            GameData gameData = new GameData();
            gameData.SetData(id, stageTime, stageScore);
            return gameData;
        }

        public void SaveData(string id, float stageTime, int stageScore)
        {
            gameDatas.Add(CreateGameData(id, stageTime, stageScore));
            gameDataGroup.rank = gameDatas.ToArray();
            SaveData(gameDataGroup);
        }

        void Start()
        {
            path = Application.dataPath + "/" + fileName;

            // SaveData("0", 10000.0f, 10000);

            LoadData();

            if (!PlayerPrefs.HasKey("Music"))
            {
                PlayerPrefs.SetInt("Music", 1);
            }
            if(!PlayerPrefs.HasKey("Sound"))
            {
                PlayerPrefs.SetInt("Sound", 1);
            }
            isMusic = PlayerPrefs.GetInt("Music");
            issound = PlayerPrefs.GetInt("Sound");
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LoadData()
        {
            Debug.Log("load");
            if(File.Exists(path)) { 
                string jsonData  = File.ReadAllText(path);
                gameDataGroup = JsonUtility.FromJson<GameDataGroup>(jsonData);

                for (int i = 0; i < gameDataGroup.rank.Length; i++)
                {
                    gameDatas.Add(gameDataGroup.rank[i]);
                }
            }
        }
    }
}
*/