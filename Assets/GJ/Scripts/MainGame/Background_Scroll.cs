using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Background_Scroll : MonoBehaviour
    {
        private float height;     // 배경의 가로 길이
        private float scrollSpeed = 2f;

        private void Awake()
        {
            // 가로 길이를 측정
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
        /// 위치를 재배치 
        /// </summary>
        private void RePosition()
        {
            Vector3 offset = new Vector3(0f, height * 2f, 0f);
            transform.position = transform.position + offset;
        }

        /// <summary>
        /// 아래로 무한 스크롤
        /// </summary>
        public void ScrollingObject()
        {
            // 초당 speed의 속도로 아래로 스크롤
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }
}
