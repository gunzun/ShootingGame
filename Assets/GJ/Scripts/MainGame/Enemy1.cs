using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace GJ
{
    public class Enemy1 : Enemy
    {
        private void Start()
        {
            m_type = ENEMY_TYPE.ENEMY1;
            EnemySpeed = 3.0f;
            EnemyAttSpeed = 1.5f;
            EnemyAttPower = 1;
            EnemyHp = 4;
            enemyOriginHp = EnemyHp;                        // 적의 변경되지 않은 기본 체력


        delayValeue = (EnemySpeed / EnemyHp) * 0.8f;        // 딜레이값 초기화
        }
    }
}

