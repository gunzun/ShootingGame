using Codice.Client.BaseCommands.CheckIn.Progress;
using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Move : MonoBehaviour
    {
        public float playerSpd = 5.0f;
        // public float rotateSpd = 180.0f;

        private Animator playeranimator;
        private Player_Input playerInput;

        private Vector3 playerDir;

        private float playerForwardAxis;
        private float playerLeftAxis;

        void Start()
        {
            // playerInput ������Ʈ ����
            playerInput = GetComponent<Player_Input>();
        }

        void Update()
        {
            PlayerMove();
        }

        public void PlayerMove()
        {
            // player�� ���� ������ �̵�
            playerForwardAxis = playerInput.forwardMove;
            playerLeftAxis = playerInput.leftMove;
            Debug.Log(playerLeftAxis);

            // player�� ũ�Ⱑ 1�� ���⺤��
            playerDir = new Vector3(playerLeftAxis, playerForwardAxis).normalized;
            // player �̵�
            transform.position += playerDir * playerSpd * Time.deltaTime;
            
            // player�� ȭ������� �Ѿ�� �ʵ��� ����
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            Vector3 WorldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = WorldPos;
        }
    }
}
