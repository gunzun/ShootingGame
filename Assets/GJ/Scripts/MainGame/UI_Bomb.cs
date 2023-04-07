using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class UI_Bomb : MonoBehaviour
    {
        /// <summary>
        /// 활성화 되지 않은 자식 개체 중 가장 위의 자식 하나를 활성화한다.
        /// </summary>
        private void ActiveChildren()
        {
            for (int i = 0; i < transform.childCount; i++)                  // 자식의 수만큼 반복한다.
            {
                if (transform.GetChild(i).gameObject.activeSelf == true)    // active가 된 자식은 패스하고
                {
                    continue;
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);       // active가 안된 자식을 활성화하고
                    break;                                                  // 반복문을 끝낸다.
                }
            }
        }
        /// <summary>
        /// 활성화 된 자식 개체 중 가장 아래의 자식 하나를 비활성화한다.
        /// </summary>
        private void DeactiveChildren()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)             // 자식의 수만큼 반복한다.
            {
                if (transform.GetChild(i).gameObject.activeSelf == false)   // active가 안된 자식은 패스하고
                {
                    continue;
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);      // active가 된 자식을 비활성화 하고
                    break;                                                  // 반복문을 끝낸다.
                }
            }
        }
    }
}
