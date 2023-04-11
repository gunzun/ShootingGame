using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Opening : MonoBehaviour
    {
        public void OnEnterNextScene()
        {
            GameManager.Instance.EnterStartSceneViaLoadScene();
        }
    }
}
