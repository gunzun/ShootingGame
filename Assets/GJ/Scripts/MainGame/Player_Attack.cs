using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Attack : MonoBehaviour
    {
        public GameObject Bullet;               // �Ѿ� ������
        public GameObject Snowball;             // ������ ������
        
        private Player_Input playerInput;

        public float attackSpd;                 // ���ݼӵ�
        private float count;                    // ���� ���� ī��Ʈ

        void Start()
        {
            // PlayerInput ��������
            playerInput = GetComponent<Player_Input>();

            // ���� �ӵ�, �پ����� ������
            attackSpd = 3f;
        }

        void Update()
        {
            // ���� �ӵ��� ����
            count += Time.deltaTime;

            //  �Ѿ� �߻��ư�� ������ ��
            if (playerInput.fire == true && count >= attackSpd * 0.1f)
            {
                Fire();
                count = 0.0f;
            }
        }

        private void Fire()
        {
            Instantiate(Bullet.transform , this.transform.position + new Vector3(0f, 0.5f, 0f), this.transform.rotation);
        }
    }
}
