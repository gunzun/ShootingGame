using Codice.Client.BaseCommands.CheckIn.Progress;
using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Behaviour : Player
    {
        private Vector3 playerDir;                  // 플레이어 인풋에 따른 방향값

        private float playerForwardAxis;            // 플레이어 인풋에 따른 전 후방 축
        private float playerLeftAxis;               // 플레이어 인풋에 따른 좌 우방 축

        void Update()
        {
            PlayerMove();

            if (Player_Stat.Instance.Hp <= 0)
            {
                PlayerDie();                        // 플레이어 체력이 0보다 떨어지면 PlayerDie 함수 호출
            }
        }

        private void PlayerMove()
        {
            // player의 왼쪽 오른쪽 이동
            playerForwardAxis = playerInput.forwardMove;
            playerLeftAxis = playerInput.leftMove;

            // player의 크기가 1인 방향벡터
            playerDir = new Vector3(playerLeftAxis, playerForwardAxis).normalized;
            // player 이동
            transform.position += playerDir * Player_Stat.Instance.MoveSpeed * Time.deltaTime;


            // player가 화면밖으로 넘어가지 않도록 방지
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            Vector3 WorldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = WorldPos;
        }

        private void PlayerDie()
        {
            Player_Stat.Instance.IsDie = true;                         // 플레이어를 사망처리한다.
            // 죽는 애니메이션을 재생한다.

            Destroy(gameObject);        // 테스트
        }

    }
}
