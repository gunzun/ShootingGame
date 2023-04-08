using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Lazer : MonoBehaviour
    {
        // public GameObject LazerObj;
        public GameObject PlayerObj;

        private float timer;
        public float delayTime = 0.2f;
        public int lazerCount = 500;

        private Vector3 PlayerPos;
        private Vector3 Direction;


        void Start()
        {
            PlayerObj = GameObject.FindGameObjectWithTag("Player");
            PlayerPos = GetPlayerPosition(PlayerObj);
            // PlayerPos = PlayerObj.transform.position;
            Direction = EnemyToPlayerDir(PlayerPos);
            LazerLaunch();
        }

        public Vector3 GetPlayerPosition(GameObject _player)
        {
            Vector3 playerPos;
            playerPos = _player.transform.position;
            return playerPos;
        }
        private Vector3 EnemyToPlayerDir(Vector3 _playerPos)
        {
            Vector3 dir;
            dir = (_playerPos - this.transform.position).normalized;
            return dir;
        }
        public void LazerLaunch()
        {
            // 레이저처럼 연속되게 발사
            timer += Time.deltaTime;
            if (timer > delayTime)
            {
                timer = 0;
                for (int i = 0; i < lazerCount; i++)
                    {
                    // this.gameObject = Instantiate(this.gameObject, new Vector3( this.transform.position.x + 0.1f, 0, this.transform.position.z + 0.1f), this.transform.rotation);
                }
            }
            Destroy(this.gameObject, 10);
        }
    }
}
