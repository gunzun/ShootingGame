using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GJ
{
    public class Credit : MonoBehaviour
    {
        /*public void TouchToRestart()
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
        }*/

        public TextMeshProUGUI textMesh;

        /// <summary>
        /// 크레딧 애니메이션이 끝날 때 텍스트를 띄운다.
        /// </summary>
        public void CreditAnimEnd()
        {
            textMesh.gameObject.SetActive(true);
        }
    }
}
