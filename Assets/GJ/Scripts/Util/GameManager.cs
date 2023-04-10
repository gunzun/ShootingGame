using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEditorInternal;

namespace GJ
{
    public class GameManager : MonoBehaviour
    {
        public enum SceneType : int // ??????
        {
            Intro,
            Start,
            Loading,
            Play,
            Gameover,
            Ranking,
            Credit,
        }

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

        private Scene PlayScene;                    // 플레이씬을 받을 변수


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
            if (SceneManager.GetActiveScene() == PlayScene)         // 플레이씬이 켜진 상태일 때
            {

            }
        }
        public void GameOver()
        {
            SceneManager.LoadScene((int)SceneType.Gameover);
        }

        // ****************** 메인 메뉴 UI 관련 ****************** //
        public void OnBtn_GameStart()
        {
            // SceneManager.LoadScene(1);
            LoadingScene.LoadScene(/*"Play"*/);
        }
        public void OnBtn_Ranking()
        {
            SceneManager.LoadScene(((int)SceneType.Ranking));
        }
        public void OnBtn_Credit()
        {
            SceneManager.LoadScene((int)SceneType.Credit);
        }
        // ***************************************************** //
        public void Restart()
        {
            SceneManager.LoadScene((int)SceneType.Start);
        }
        public void OnBtn_Exit()
        {
            // 에디터에서는 플레이를 멈추고, 빌드 시에는 종료한다.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        /// <summary>
        /// 버틴 시간을 분이랑 초로 구분하여 문자열로 반환한다.
        /// </summary>
        public string PlayTimeToString(/*out int minute, out int second*/)
        {
            float playTime = Player_Stat.Instance.PlayTime;
            int minute = 0;
            int second = 0;
            minute = (int)(playTime / 60);
            second = (int)(playTime % 60);

            return string.Format("{0:D2}:{1:D2}", minute, second);
            // timeText.text = minute + ":" + second;
        }
    }
}
