using GJ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Teacher : MonoBehaviour
{
    float timer = 0;
    public float creatBulletTime = 2.0f;

    public GameObject Lazer;
    public GameObject Bullet;
    GameObject Player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            Player = playerObject;
        }
        else
        {
            Debug.Log("�÷��̾ �������� �ʽ��ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (creatBulletTime < timer)
        { 
            CreateBullet(); 
            timer = 0; 
        }
    }

    public void CreateBullet()
    {
        // ����ź
        // GameObject bullet = Instantiate(Bullet, this.transform.position, Bullet.transform.rotation);
        // bullet.GetComponent<Bullet>().SetBullet(Player.transform.position);

        // Ȯ��ź
        /*for (int i = 0; i < 12; i++)
        {
            GameObject bullet = Instantiate(Bullet, this.transform.position, Bullet.transform.rotation);
            bullet.GetComponent<Bullet>().SetBullet(Player.transform.position, i);
        }*/

        // ������
        // if(Lazer.lazer)
        //GameObject LazerObj = Instantiate(Lazer, this.transform.position, this.transform.rotation);
    }
}