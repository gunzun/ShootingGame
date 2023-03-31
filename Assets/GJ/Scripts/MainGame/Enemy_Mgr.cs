using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// 싱글톤으로 생성
public class Enemy_Mgr : MonoBehaviour
{
    #region singleton 으로 선언
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
            // instance가 이미 할당되어 있다면 에러를 띄우고 객체를 삭제한다.
            Debug.LogWarning("씬에 두 개 이상의 에너미 매니저가 존재합니다.");
            Destroy(gameObject);
        }
    }


    

    private void Update()
    {

    }
}
