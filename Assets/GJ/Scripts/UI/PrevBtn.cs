using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class PrevBtn : MonoBehaviour
    {
        /// <summary>
        /// Prev ��ư�� Ŭ���� �� Start ������ �̵��Ѵ�.
        /// </summary>
        public void PrevBtnClick()
        {
            GameManager.Instance.EnterStartScene();
        }
    }
}
