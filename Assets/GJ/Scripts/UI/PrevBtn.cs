using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class PrevBtn : MonoBehaviour
    {
        /// <summary>
        /// Prev 버튼을 클릭할 시 Start 씬으로 이동한다.
        /// </summary>
        public void PrevBtnClick()
        {
            GameManager.Instance.EnterStartScene();
        }
    }
}
