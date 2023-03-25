using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class EnemyMove : MonoBehaviour
    {
        public GameObject explosion;
        public GameManager gameManager;

        // 메테오 회전
        public float tumble;
        // 메테오 속력
        public float speed;

        void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
            GetComponent<Rigidbody>().velocity = transform.forward * speed;

            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        }

        void Update()
        {
            
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Enemy")
            {
                return;
            }
            if(explosion != null) 
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            if(other.tag == "Player")
            {
                gameManager.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}