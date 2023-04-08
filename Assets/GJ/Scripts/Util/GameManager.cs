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
            // SceneManager.LoadScene(2);
        }
        public void OnBtn_GameStart()
        {
            // SceneManager.LoadScene(1);
            LoadingScene.LoadScene("Play");
            PlayScene = SceneManager.GetActiveScene();
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
