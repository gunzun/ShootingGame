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
    // [CreateAssetMenu(fileName ="GameManager",menuName ="ScriptableObjects/GameManager", order = 1)]
    [System.Serializable]
    internal class GameManager : MonoBehaviour
    {
        public enum SceneType// : int // ??????
        {
            Intro,
            Start,
            Loading,
            Play,
            Gameover,
            Ranking,
            Credit,
            Config
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

        public int GetLoadingSceneNum { get => (int)SceneType.Loading; }

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

        #region #Scene관련 함수들
        // ****************** SCENE METHODS ****************** //
        public void EnterStartScene()
        {
            Player_Stat.Instance.SetPlayerStat();           // 플레이어 스탯을 초기화한다.
            SceneManager.LoadScene((int)SceneType.Start);
        }
        /// <summary>
        /// 로딩 씬이 끝난 이후 바로 StartScene을 호출한다.
        /// </summary>
        public void EnterPlaySceneViaLoadScene()
        {
            LoadingScene.LoadScene((int)SceneType.Play);
        }
        public void EnterGameoverScene()
        {
            SceneManager.LoadScene((int)SceneType.Gameover);
        }
        public void EnterRankingScene()
        {
            SceneManager.LoadScene(((int)SceneType.Ranking));
        }
        public void EnterCreditScene()
        {
            SceneManager.LoadScene((int)SceneType.Credit);
        }
        public void EnterConfigScene()
        {
            SceneManager.LoadScene((int)SceneType.Config);
        }
        // ***************************************************** //
        #endregion

        /// <summary>
        /// 프로그램을 종료한다.
        /// </summary>
        public void QuitProgram()
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
        /// <param name="m_time">'초' 단위의 플레이타임</param>
        /// <returns></returns>
        public string PlayTimeToString(float m_time)
        {
            int minute = 0;
            int second = 0;
            minute = (int)(m_time / 60);
            second = (int)(m_time % 60);
            return string.Format("{0:D2}:{1:D2}", minute, second);
            // timeText.text = minute + ":" + second;
        }
    }
}
