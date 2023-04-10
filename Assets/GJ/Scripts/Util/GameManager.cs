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
                    Debug.LogWarning("���ӸŴ��� �ν��Ͻ��� �����");
                    return null;
                }
                return _instance;
            }
        }

        private Scene PlayScene;                    // �÷��̾��� ���� ����


        private void Awake()
        {
            #region # �̱���
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.LogError("���� ���Ӹ޴����� �� �� �̻��ΰ�����");
                Destroy(gameObject);
            }
            #endregion
        }

        void Update()
        {
            if (SceneManager.GetActiveScene() == PlayScene)         // �÷��̾��� ���� ������ ��
            {

            }
        }
        public void GameOver()
        {
            SceneManager.LoadScene((int)SceneType.Gameover);
        }

        // ****************** ���� �޴� UI ���� ****************** //
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
            // �����Ϳ����� �÷��̸� ���߰�, ���� �ÿ��� �����Ѵ�.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        /// <summary>
        /// ��ƾ �ð��� ���̶� �ʷ� �����Ͽ� ���ڿ��� ��ȯ�Ѵ�.
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
