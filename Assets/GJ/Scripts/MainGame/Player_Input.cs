using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public string moveAxisName = "Vertical";                // 앞뒤 움직임을 위한 입력축 이름
    public string leftAxisName = "Horizontal";              // 좌우 움직임을 위한 입력축 이름
    public string fireBtn = "Fire1";                        // 발사를 위한 입력 버튼 이름

    // 23.4/4 GJ. 이 문자열로 GetKeyDown을 하면 에러가 뜬다. 왜지?
    // public string skillBtn = "Skill";                       // 스킬 사용을 위한 입력 버튼 이름

    #region Property
    public float forwardMove { get; set; }
    public float leftMove { get; set; }
    public bool fire { get; set; }
    public bool skill { get; set; }
    #endregion

    void Update()
    {
        forwardMove = Input.GetAxis(moveAxisName);
        leftMove = Input.GetAxis(leftAxisName);
        fire = Input.GetButton(fireBtn);
        skill = Input.GetKeyDown(KeyCode.Space);
    }
}
