using Codice.Client.BaseCommands.CheckIn.Progress;
using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Behaviour : Player
    {
        private Vector3 playerDir;                  // �÷��̾� ��ǲ�� ���� ���Ⱚ

        private float playerForwardAxis;            // �÷��̾� ��ǲ�� ���� �� �Ĺ� ��
        private float playerLeftAxis;               // �÷��̾� ��ǲ�� ���� �� ��� ��

        void Update()
        {
            PlayerMove();

            if (Player_Stat.Instance.Hp <= 0)
            {
                PlayerDie();                        // �÷��̾� ü���� 0���� �������� PlayerDie �Լ� ȣ��
            }
        }

        private void PlayerMove()
        {
            // player�� ���� ������ �̵�
            playerForwardAxis = playerInput.forwardMove;
            playerLeftAxis = playerInput.leftMove;

            // player�� ũ�Ⱑ 1�� ���⺤��
            playerDir = new Vector3(playerLeftAxis, playerForwardAxis).normalized;
            // player �̵�
            transform.position += playerDir * Player_Stat.Instance.MoveSpeed * Time.deltaTime;


            // player�� ȭ������� �Ѿ�� �ʵ��� ����
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            Vector3 WorldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = WorldPos;
        }

        private void PlayerDie()
        {
            Player_Stat.Instance.IsDie = true;                         // �÷��̾ ���ó���Ѵ�.
            // �״� �ִϸ��̼��� ����Ѵ�.

            Destroy(gameObject);        // �׽�Ʈ
        }

    }
}