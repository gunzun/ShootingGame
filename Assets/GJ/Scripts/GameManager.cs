using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Serialization;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GJ
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] Enemies;
        public Vector3 spawnValue;
        public int enemyCount;

        public float spawnWait;
        public float startWait;
        public float waveWait;

        private bool gameOver = false;
        private bool restart = false;

        public Text TextRestart;
        public Text TextGameover;

        void Start()
        {
            gameOver = false;
            restart = false;
            TextRestart.text = "";
            TextGameover.text = "";
            StartCoroutine(SpawnEnemy());
        }

        // Update is called once per frame
        void Update()
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("Play");
                }
            }
        }

        IEnumerator SpawnEnemy()
        { 
            yield return new WaitForSeconds(startWait);

            while(true)
            {
                for(int i = 0; i<enemyCount; i++)
                {
                    GameObject enemy = Enemies[Random.Range(0, Enemies.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                    // ���ʹϾ��� ��Ȯ�� 0�̶�� ���� ���� �� ���� ������ �ִ�.
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(enemy, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                
                yield return new WaitForSeconds(waveWait);


                if (gameOver)
                {
                    TextRestart.text = "-R�� ������ ����� �ϼ���";
                    restart = true;
                    break;
                }
            }
        }

        public void GameOver()
        {
            TextGameover.text = "���� ����!";
            gameOver = true;
        }

        

    }
}
