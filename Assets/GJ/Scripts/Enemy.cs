using GJ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
            Debug.Log("플레이어가 존재하지 않습니다.");
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
        // 조작탄
        // GameObject bullet = Instantiate(Bullet, this.transform.position, Bullet.transform.rotation);
        // bullet.GetComponent<Bullet>().SetBullet(Player.transform.position);

        // 확산탄
        /*for (int i = 0; i < 12; i++)
        {
            GameObject bullet = Instantiate(Bullet, this.transform.position, Bullet.transform.rotation);
            bullet.GetComponent<Bullet>().SetBullet(Player.transform.position, i);
        }*/

        // 레이저
        // if(Lazer.lazer)
        //GameObject LazerObj = Instantiate(Lazer, this.transform.position, this.transform.rotation);
    }
}