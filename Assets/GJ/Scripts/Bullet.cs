using Codice.Client.Common.GameUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Bullet : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private Vector3 destination;
        [UnityEngine.SerializeField]
        private bool isThrow;
        public float speed;
        float angle = 0;
        Vector3 dir;            // ����

        // ���° �Ѿ�����
        [SerializeField]
        int number;

        float radius = 1.0f;
        float cAngle = 0;
        float rotAngle = 30.0f;
        Vector3 hideCenter;

        public void SetBullet(Vector3 _destination, int _number = 0)
        {
            destination = _destination;
            isThrow = true;

            // �ޱ� ���
            angle = Mathf.Atan2(destination.z - this.transform.position.z, destination.x - this.transform.position.x);

            // ������
            dir = destination - this.transform.position;
            dir = dir.normalized;
            number = _number;
            rotAngle = number * rotAngle;

            hideCenter = this.transform.position;
        }
        void Start()
        {

        }

        void Update()
        {
            if (isThrow)
            {
                this.transform.position += dir * Time.deltaTime * speed;

                // float mx = Mathf.Cos(angle) * speed * Time.deltaTime;
                // float mz = Mathf.Sin(angle) * speed * Time.deltaTime;
                // this.transform.position += new Vector3(mx, 0, mz);

                float mx = Mathf.Cos(angle) * speed * Time.deltaTime;
                float mz = Mathf.Sin(angle) * speed * Time.deltaTime;

                Vector3 centerPos = new Vector3(mx, 0, mz);
                centerPos += hideCenter;
                hideCenter = centerPos;

                rotAngle += (40.0f * Time.deltaTime);
                cAngle = Mathf.Deg2Rad * rotAngle;

                float curX = centerPos.x + radius * Mathf.Cos(cAngle);
                float curZ = centerPos.z + radius * Mathf.Sin(cAngle);

                this.transform.position = new Vector3(curX, 0, curZ);
            }
        }
    }
}
