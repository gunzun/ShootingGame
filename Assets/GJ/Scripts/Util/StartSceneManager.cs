using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class StartSceneManager : MonoBehaviour
    {
        public void OnBtn_Play()
        {
            GameManager.Instance.EnterPlaySceneViaLoadScene();
        }
        public void OnBtn_Ranking()
        {
            GameManager.Instance.EnterRankingScene();
        }
        public void OnBtn_Config()
        {
            GameManager.Instance.EnterConfigScene();
        }
        public void OnBtn_Credit()
        {
            GameManager.Instance.EnterCreditScene();
        }
        public void OnBtn_Exit()
        {
            GameManager.Instance.QuitProgram();
        }
    }
}
