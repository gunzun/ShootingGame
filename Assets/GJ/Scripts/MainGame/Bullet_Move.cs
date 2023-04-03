using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Bullet_Move : MonoBehaviour
    {
        public float speed = 10.0f;      // ÃÑ¾Ë ½ºÇÇµå

        void Update()
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
    }
}
