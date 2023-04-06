using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GJ
{
    public class GameManager : MonoBehaviour
    {
        private GameManager() { }

        private static GameManager _instance = null;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogWarning("게임매니저 인스턴스가 없어요");
                    return null;
                }
                return _instance;
            }
        }

        private void Awake()
        {
            #region # 싱글톤
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.LogError("씬에 게임메니저가 두 개 이상인가봐요");
                Destroy(gameObject);
            }
            #endregion
        }

        void Update()
        {

        }
        public void GameOver()
        {
            // SceneManager.LoadScene(2);
        }
        public void OnBtn_GameStart()
        {
            // SceneManager.LoadScene(1);
            LoadingScene.LoadScene("Play");
        }
        public void OnBtn_Ranking()
        {
            SceneManager.LoadScene(2);
        }
        public void OnBtn_Credit()
        {
            SceneManager.LoadScene(3);
        }
        public void Restart()
        {
            SceneManager.LoadScene(1);
        }
        public void OnBtn_Exit()
        {
            
        }
    }
}
