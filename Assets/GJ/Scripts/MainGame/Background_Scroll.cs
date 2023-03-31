using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Background_Scroll : MonoBehaviour
    {
        private float height;     // ����� ���� ����
        private float scrollSpeed = 2f;

        private void Awake()
        {
            // ���� ���̸� ����
            BoxCollider backgroundCollider = GetComponent<BoxCollider>();
            height = backgroundCollider.size.y;
        }


        void Update()
        {
            ScrollingObject();
            if (transform.position.y <= -height)
            {
                RePosition();
            }
            this.transform.position -= new Vector3(0, Time.deltaTime * scrollSpeed, 0);
        }

        /// <summary>
        /// ��ġ�� ���ġ 
        /// </summary>
        private void RePosition()
        {
            Vector3 offset = new Vector3(0f, height * 2f, 0f);
            transform.position = transform.position + offset;
        }

        /// <summary>
        /// �Ʒ��� ���� ��ũ��
        /// </summary>
        public void ScrollingObject()
        {
            // �ʴ� speed�� �ӵ��� �Ʒ��� ��ũ��
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }
}
