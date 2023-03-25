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
            // playerInput 컴포넌트 접근
            playerInput = GetComponent<Player_Input>();
        }

        void Update()
        {
            PlayerMove();
        }

        public void PlayerMove()
        {
            // player의 왼쪽 오른쪽 이동
            playerForwardAxis = playerInput.forwardMove;
            playerLeftAxis = playerInput.leftMove;
            Debug.Log(playerLeftAxis);

            // player의 크기가 1인 방향벡터
            playerDir = new Vector3(playerLeftAxis, playerForwardAxis).normalized;
            // player 이동
            transform.position += playerDir * playerSpd * Time.deltaTime;
            
            // player가 화면밖으로 넘어가지 않도록 방지
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            Vector3 WorldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = WorldPos;
        }
    }
}
