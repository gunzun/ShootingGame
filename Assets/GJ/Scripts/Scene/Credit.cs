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
        /// ũ���� �ִϸ��̼��� ���� �� �ؽ�Ʈ�� ����.
        /// </summary>
        public void CreditAnimEnd()
        {
            textMesh.gameObject.SetActive(true);
        }
    }
}
