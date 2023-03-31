using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nothing
{
    public class Teacher_Player : MonoBehaviour
    {
        Rigidbody thisRB;
        public float speed = 0.2f;

        void Start()
        {
            thisRB = this.GetComponent<Rigidbody>();
        }
        public void Player_Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, 0, moveZ);
            thisRB.velocity = move * speed;

            // 해상도의 대한 변동성 관리
            // World : Unity가 만든 가상의 좌표계
            // Screen : 모니터의 좌표계
            // Camera : 이동되는 카메라의 좌표계
            Vector3 posInWorld = Camera.main.WorldToScreenPoint(this.transform.position);

            float posX = Mathf.Clamp(posInWorld.x, 0, Screen.width);
            float posZ = Mathf.Clamp(posInWorld.y, 0, Screen.height);

            Vector3 posInScreen = Camera.main.ScreenToWorldPoint(new Vector3(posX, posZ, 0));

            thisRB.position = new Vector3(posInScreen.x, 0, posInScreen.z);
        }

        // Update is called once per frame
        void Update()
        {
            Player_Move();
        }
    }
}
