using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Bullet2 : MonoBehaviour
    {
        bool isThrow = false;
        public float speed = 2.0f;
        Vector3 dir;
        Vector3 destination;
        GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (isThrow)
            {
                dir = player.transform.position - this.transform.position;
                dir = dir.normalized;
            }
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(0, 1, 0);
            this.transform.position += dir * speed * Time.deltaTime;
        }


        public void SetBullet(Vector3 _destination, int _number = 0)
        {
            destination = _destination;
            isThrow = true;

            dir = destination - this.transform.position;
        }
    }
}
