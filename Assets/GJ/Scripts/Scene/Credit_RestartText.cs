using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Credit_RestartText : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.EnterStartScene();
            }
        }
    }
}
