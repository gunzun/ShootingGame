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
                    Debug.LogWarning("���ӸŴ��� �ν��Ͻ��� �����");
                    return null;
                }
                return _instance;
            }
        }

        public int GetLoadingSceneNum { get => (int)SceneType.Loading; }

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

        #region #Scene���� �Լ���
        // ****************** SCENE METHODS ****************** //
        public void EnterStartScene()
        {
            Player_Stat.Instance.SetPlayerStat();           // �÷��̾� ������ �ʱ�ȭ�Ѵ�.
            SceneManager.LoadScene((int)SceneType.Start);
        }
        /// <summary>
        /// �ε� ���� ���� ���� �ٷ� StartScene�� ȣ���Ѵ�.
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
        /// ���α׷��� �����Ѵ�.
        /// </summary>
        public void QuitProgram()
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
        /// <param name="m_time">'��' ������ �÷���Ÿ��</param>
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
