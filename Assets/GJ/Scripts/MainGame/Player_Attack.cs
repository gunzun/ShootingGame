using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Player_Attack : Player
    {
        public GameObject Bullet;               // �Ѿ� ������
        public GameObject Snowball;             // ������ ������

        private float count;                    // ���� ���� ī��Ʈ
        private bool isSkill = false;    // ��ų�� ����߳�?
        public bool PlayerSkilled { get => isSkill; set => isSkill = value; }

        void Update()
        {
            count += Time.deltaTime;                                                    // ī��Ʈ�� ���� ��Ű�� ���� �ӵ����� �����ϸ� ���� �� 0���� �����Ѵ�.
            if (playerInput.fire == true && count >= Player_Stat.Instance.AttackSpeed)  // "Fire" Ű�� ������ ��, ī��Ʈ�� ���� �����Ѵ�.
            {
                Fire();                                                                 // �Լ� ����
                count = 0.0f;                                                           // �Ѿ� �߻� �� ī��Ʈ�� 0���� �����Ѵ�.
            }
        }
        private void Fire()
        {
            // �÷��̾� ���� �Ѿ� ������ ����
            Instantiate(Bullet.transform, this.transform.position + new Vector3(0f, 0.5f, 0f), this.transform.rotation);
        }               // �÷��̾ �Ѿ��� �߻��Ѵ�.
        private void OnTriggerStay(Collider other)
        {
            if (playerInput.skill && other.CompareTag("Snowball"))
            {
                isSkill = true;
            }
        }
    }
}
