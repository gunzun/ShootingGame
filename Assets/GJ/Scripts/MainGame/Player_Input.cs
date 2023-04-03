using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    public string moveAxisName = "Vertical";                // �յ� �������� ���� �Է��� �̸�
    public string leftAxisName = "Horizontal";              // �¿� �������� ���� �Է��� �̸�
    public string fireBtn = "Fire1";                        // �߻縦 ���� �Է� ��ư �̸�
    public string skillBtn = "Skill";                       // ��ų ����� ���� �Է� ��ư �̸�

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
        skill = Input.GetButtonDown(skillBtn);
    }
}
