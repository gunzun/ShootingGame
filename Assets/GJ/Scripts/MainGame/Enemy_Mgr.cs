using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// �̱������� ����
public class Enemy_Mgr : MonoBehaviour
{
    #region singleton ���� ����
    public static Enemy_Mgr instance;
    private Enemy_Mgr() { }
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // instance�� �̹� �Ҵ�Ǿ� �ִٸ� ������ ���� ��ü�� �����Ѵ�.
            Debug.LogWarning("���� �� �� �̻��� ���ʹ� �Ŵ����� �����մϴ�.");
            Destroy(gameObject);
        }
    }


    

    private void Update()
    {

    }
}
