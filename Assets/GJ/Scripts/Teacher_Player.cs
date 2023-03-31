using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class TeacherPlayer : MonoBehaviour
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

            // �ػ��� ���� ������ ����
            // World : Unity�� ���� ������ ��ǥ��
            // Screen : ������� ��ǥ��
            // Camera : �̵��Ǵ� ī�޶��� ��ǥ��
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
