using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class UI_Hp : MonoBehaviour
    {
        private void Update()
        {
            if (Player_Stat.Instance.IsHpUp == true)        // �÷��̾� Hp�� �þ��ٸ�
            {
                ActiveChildren();                           // �ڽ� ������Ʈ�� �ϳ� Ȱ��ȭ �����ش�.
                Player_Stat.Instance.IsHpUp = false;        // �ٽ� Hp�� �ø��� ���� false�� �ٲ۴�.
            }
            if (Player_Stat.Instance.IsHpDown == true)      // �÷��̾� Hp�� �پ��ٸ�
            {
                DeactiveChildren();                         // �ڽ� ������Ʈ�� �ϳ� ��Ȱ��ȭ �����ش�.
                Player_Stat.Instance.IsHpDown = false;      // �ٽ� Hp�� ���̱� ���� false�� �ٲ۴�.
            }
        }

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
