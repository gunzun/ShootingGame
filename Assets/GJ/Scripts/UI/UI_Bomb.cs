using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class UI_Bomb : MonoBehaviour
    {
        /// <summary>
        /// Ȱ��ȭ ���� ���� �ڽ� ��ü �� ���� ���� �ڽ� �ϳ��� Ȱ��ȭ�Ѵ�.
        /// </summary>
        private void ActiveChildren()
        {
            for (int i = 0; i < transform.childCount; i++)                  // �ڽ��� ����ŭ �ݺ��Ѵ�.
            {
                if (transform.GetChild(i).gameObject.activeSelf == true)    // active�� �� �ڽ��� �н��ϰ�
                {
                    continue;
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);       // active�� �ȵ� �ڽ��� Ȱ��ȭ�ϰ�
                    break;                                                  // �ݺ����� ������.
                }
            }
        }
        /// <summary>
        /// Ȱ��ȭ �� �ڽ� ��ü �� ���� �Ʒ��� �ڽ� �ϳ��� ��Ȱ��ȭ�Ѵ�.
        /// </summary>
        private void DeactiveChildren()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)             // �ڽ��� ����ŭ �ݺ��Ѵ�.
            {
                if (transform.GetChild(i).gameObject.activeSelf == false)   // active�� �ȵ� �ڽ��� �н��ϰ�
                {
                    continue;
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);      // active�� �� �ڽ��� ��Ȱ��ȭ �ϰ�
                    break;                                                  // �ݺ����� ������.
                }
            }
        }
    }
}
